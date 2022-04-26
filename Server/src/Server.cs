using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        //read default settings from App.config
        internal static int DEFAULT_TCP_PORT = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_TCP_PORT"));
        internal static int DEFAULT_UDP_PORT = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_UDP_PORT"));

        internal static int DEFAULT_TABLE_CAPACITY = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_TABLE_CAPACITY"));

        //logs
        public delegate void LogMessageDelegate(string msg);
        public static LogMessageDelegate LogMessage { get; set; } = null;

        //thread signal 
        private static ManualResetEvent allDoneTcp = new ManualResetEvent(false);
        private static ManualResetEvent allDoneUdp = new ManualResetEvent(false);


        //server models
        internal static IDModel userIDModel = null;
        internal static UserModel userModel = null;
        internal static QRModel qrModel = null;
        internal static WIFISpotModel wifiSpotModel = null;
        internal static byte[] wifiBuffer = null;

        //thread for listeners
        private static Task tcpListener;
        private static Task udpListener;

        //while run == true server run
        public static bool Run { get; private set; }

        /// <summary>
        /// start TcpListener and UdpListener
        /// </summary>
        public static int Start()
        {
            if (Server.Run)
                return 0;

            try
            {
                tcpListener = new Task(StartTcpListener);
                udpListener = new Task(StartUdpListener);

                Server.Run = true;

                tcpListener.Start();
                udpListener.Start();
            }
            catch (Exception ex)
            {
                //stop server
                Server.Stop();
            }

            return 0;
        }

        /// <summary>
        /// stop TcpListener and UdpListener
        /// flush temp storage 
        /// </summary>
        public static int Stop()
        {
            if (!Server.Run)
                return 0;

            Server.Run = false;

            //signal to end accept
            allDoneTcp.Set();
            allDoneUdp.Set();

            //
            tcpListener.Wait();
            udpListener.Wait();

            //Force save data from temporrary storage to the file to all current using ID
            Server.Flush();

            Console.WriteLine("Server is stoped");
            LogMessage("Server is stoped");

            return 0;
        }

        /// <summary>
        /// flush the temp storage of the server
        /// </summary>
        /// <returns></returns>
        public static int Flush()
        {
            try
            {
                userModel.FlushTempStorage();

                Console.WriteLine("Server is flushed");
                LogMessage("Server is flushed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return 0;
        }

        /// <summary>
        /// create and start TcpListener
        /// </summary>
        private static void StartTcpListener()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, DEFAULT_TCP_PORT);

            try
            {
                listener.Start();
                Console.WriteLine("TcpListener started");
                LogMessage("TcpListener started");

                while (Server.Run)
                {
                    //set the event to nonsignaled state
                    allDoneTcp.Reset();

                    //start an asynchronous socket to listen for connections
                    listener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClient), listener);

                    //wait until a connection is made before continuing
                    allDoneTcp.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Stop();
            }
        }

        /// <summary>
        /// asynchronous accept tcp client
        /// </summary>
        /// <param name="ar">async result</param>
        private static async void AcceptTcpClient(IAsyncResult ar)
        {
            //signal the main thread to continue
            allDoneTcp.Set();

            if (!Server.Run)
                return;

            //get the socket that handles the client request
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient handler = listener.EndAcceptTcpClient(ar);
            try
            {
                //get user stream
                NetworkStream stream = handler.GetStream();

                //receive data from user
                byte[] buffer = new byte[Packet.CommandPacket.CMD_PACKET_SIZE];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                //get struct of the command
                Packet.CommandPacket cmdPacket = Packet.CommandPacket.getStruct(buffer);

                Console.WriteLine($"[TCP receive] {cmdPacket.ToString()}");
                LogMessage($"[TCP receive] {cmdPacket.ToString()}");

                //get server response
                byte[] response;
                switch ((Packet.CommandCode)cmdPacket.command)
                {
                    //user registration
                    case Packet.CommandCode.USER_REGISTRATION:
                        response = UserRegistration(cmdPacket.parameter, cmdPacket.time);
                        Console.WriteLine($"[TCP    send] {Packet.UDPPacket.getStruct(response).ToString()}");
                        LogMessage($"[TCP    send] {Packet.UDPPacket.getStruct(response).ToString()}");
                        break;

                    //Wi-Fi request
                    case Packet.CommandCode.WIFI_REQUEST:
                        response = WIFIRequest(cmdPacket.parameter);
                        if (response != null)
                        {
                            Console.WriteLine($"[TCP    send] Wi-Fi file: {Server.wifiSpotModel._xmlFileName}");
                            LogMessage($"[TCP    send] Wi-Fi file: {Server.wifiSpotModel._xmlFileName}");
                        }
                        else
                            response = buffer;
                        break;
                    default:
                        response = buffer;
                        break;
                }

                //send answer to user
                await stream.WriteAsync(response, 0, response.Length);
                await stream.FlushAsync();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                handler.Close();
            }
        }

        /// <summary>
        /// create and start UdpListener
        /// </summary>
        private static void StartUdpListener()
        {
            UdpClient listener = new UdpClient(DEFAULT_UDP_PORT);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, DEFAULT_UDP_PORT);
            Console.WriteLine("UdpListener started");
            LogMessage("UdpListener started");

            try
            {
                while (Server.Run)
                {
                    //set the event to nonsignaled state
                    allDoneUdp.Reset();

                    //start an asynchronous socket to listen for connections
                    listener.BeginReceive(new AsyncCallback(AcceptUdpClient), listener);

                    //wait until a connection is made before continuing
                    allDoneUdp.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }

        /// <summary>
        /// asynchronous accept udp client
        /// </summary>
        /// <param name="ar">async result</param>
        private static void AcceptUdpClient(IAsyncResult ar)
        {
            //signal the main thread to continue
            allDoneUdp.Set();

            if (!Server.Run)
                return;

            UdpClient listener = (UdpClient)ar.AsyncState;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, DEFAULT_UDP_PORT);
            try
            {
                //receive data from cliet
                byte[] data = listener.EndReceive(ar, ref endPoint);

                Packet.UDPPacket packet = Packet.UDPPacket.getStruct(data);

                //userID exist
                if (userIDModel.ExistUserID(packet.userID, userModel))
                {
                    Console.WriteLine("[UDP receive] {0}", packet.ToString());
                    LogMessage($"[UDP receive] {packet.ToString()}");
                    userIDModel.UpdateUserTime(packet.userID, packet.time);
                    userModel.AppendUserData(packet.userID, packet.x, packet.y, packet.time);
                }

                //userID not exist
                else
                {
                    Console.WriteLine("[UDP unauthorized] {0}", packet.ToString());
                    LogMessage($"[UDP unauthorized] {packet.ToString()}");
                    //ignore this packet
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString(), e);
            }
        }

        //user registration on server
        private static byte[] UserRegistration(int QRID, long regTime)
        {
            Packet.UDPPacket packet = new Packet.UDPPacket();

            //try to get QR coordinates
            int Result = Server.qrModel.GetQRCoord(QRID, ref packet.x, ref packet.y);

            //getting coordinates successfully
            if (Result >= 0)
            {
                //try to get userID 
                packet.userID = Server.userIDModel.GetUserID(regTime, Server.userModel);
                packet.time = regTime;

                //try to add user to UserModel
                Server.userModel.AddUserID(packet.userID, packet.x, packet.y, packet.time);
            }
            else
            {
                packet.userID = -1;
                packet.time = -1;
            }

            //answer to user
            byte[] answer = Packet.UDPPacket.getBytes(packet);

            return answer;
        }

        private static byte[] WIFIRequest(int userID) 
        {
            byte[] answer = null;

            //check user registration
            if (Server.userIDModel.ExistUserID(userID, Server.userModel))
            {
                //get wifi buffer
                if (Server.wifiBuffer != null)
                    answer = Server.wifiBuffer;
            }
            return answer;
        }
    }

    
}
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
        private static int DEFAULT_TCP_PORT = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_TCP_PORT"));
        private static int DEFAULT_UDP_PORT = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_UDP_PORT"));

        private static int DEFAULT_TABLE_CAPACITY = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_TABLE_CAPACITY"));

        //logs
        public delegate void LogMessageDelegate(string msg);
        public static LogMessageDelegate LogMessage { get; set; }

        //thread signal 
        private static ManualResetEvent allDoneTcp = new ManualResetEvent(false);
        private static ManualResetEvent allDoneUdp = new ManualResetEvent(false);


        //server models
        internal static IDModel userIDModel = new IDModel(DEFAULT_TABLE_CAPACITY);
        private static UserModel userModel = new UserModel(DEFAULT_TABLE_CAPACITY, 2);

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
                byte[] buffer = new byte[UserPacket.PACKET_SIZE];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                UserPacket packet = UserPacket.getStruct(buffer);
                Console.WriteLine("[TCP receive] {0}", packet.ToString());
                LogMessage($"[TCP receive] {packet.ToString()}");

                //if user not authorized
                if (!userIDModel.ExistUserID(packet.userID, userModel))
                {
                    //try to get userID
                    packet.userID = userIDModel.GetUserID(packet.time, userModel);
                }

                //try to add user to UserModel
                userModel.AddUserID(packet.userID, packet.x, packet.y, packet.time);

                //send answer to user
                byte[] answer = UserPacket.getBytes(packet);

                await stream.WriteAsync(answer, 0, answer.Length);
                await stream.FlushAsync();

                Console.WriteLine("[TCP    send] {0}", packet.ToString());
                LogMessage($"[TCP    send] {packet.ToString()}");
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

                UserPacket packet = UserPacket.getStruct(data);

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
    }
}
using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Server
    {
        //read default settings from App.config
        private static int DEFAULT_TCP_PORT = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_TCP_PORT"));
        private static int DEFAULT_UDP_PORT = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_UDP_PORT"));

        private static int DEFAULT_TABLE_CAPACITY = int.Parse(ConfigurationManager.AppSettings.Get("DEFAULT_TABLE_CAPACITY"));

        //thread signal 
        private static ManualResetEvent allDoneTcp = new ManualResetEvent(false);
        private static ManualResetEvent allDoneUdp = new ManualResetEvent(false);

        //server models
        private static IDModel userIDModel = new IDModel(DEFAULT_TABLE_CAPACITY);
        private static UserModel userModel = new UserModel(DEFAULT_TABLE_CAPACITY, 1000);

        public static void Main(string[] args)
        {
            Server.Start();
        }

        /// <summary>
        /// start TcpListener & UdpListener
        /// </summary>
        public static int Start()
        {
            Thread tcpListener = new Thread(StartTcpListener);
            Thread udpListener = new Thread(StartUdpListener);

            tcpListener.Start();
            udpListener.Start();

            return 0;
        }

        /// <summary>
        /// create and start TcpListener
        /// </summary>
        private static void StartTcpListener()
        {
            var listener = new TcpListener(IPAddress.Any, DEFAULT_TCP_PORT);

            try
            {
                listener.Start();
                Console.WriteLine("TcpListener started");

                while (true)
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

            //get the socket that handles the client request
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient handler = listener.EndAcceptTcpClient(ar);
            try
            {
                //get user stream
                var stream = handler.GetStream();

                //receive data from user
                using var reader = new StreamReader(stream);
                char[] buffer = new char[UserPacket.PACKET_SIZE];
                await reader.ReadAsync(buffer, 0, UserPacket.PACKET_SIZE);
                byte[] request = Encoding.Latin1.GetBytes(buffer);
                UserPacket packet = UserPacket.getStruct(request);
                Console.WriteLine("[TCP receive] {0}", packet.ToString());

                //try to get userID
                packet.userID = userIDModel.GetUserID(packet.time);

                //try to add user to UserModel
                userModel.AddUserID(packet.userID, packet.x, packet.y, packet.time);

                //send answer to user
                byte[] answer = UserPacket.getBytes(packet);
                using var writer = new StreamWriter(stream);
                await writer.WriteLineAsync(Encoding.Latin1.GetString(answer));
                await writer.FlushAsync();
                Console.WriteLine("[TCP    send] {0}", packet.ToString());

                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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

            try
            {
                while (true)
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

                //Force save data from temporrary storage to the file to all current using ID
                userModel.FlushTempStorage();
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

            UdpClient listener = (UdpClient)ar.AsyncState;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, DEFAULT_UDP_PORT);

            //receive data from cliet
            byte[] data = listener.EndReceive(ar, ref endPoint);
            
            UserPacket packet = UserPacket.getStruct(data);
            
           

            //userID exist
            if (userIDModel.ExistUserID(packet.userID))
            {
                Console.WriteLine("[UDP receive] {0}", packet.ToString());
                userIDModel.UpdateUserTime(packet.userID, packet.time);
                userModel.AppendUserData(packet.userID, packet.x, packet.y, packet.time);
            }

            //userID not exist
            else
            {
                Console.WriteLine("[UDP unauthorized] {0}", packet.ToString());
                //ignore this packet
            }
        }
    }
}
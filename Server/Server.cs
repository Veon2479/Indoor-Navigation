using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Server
    {
        const int DEFAULT_TCP_PORT = 4444;
        const int DEFAULT_UDP_PORT = 4445;

        static async Task Main(string[] args)
        {
            StartTcpListener();
            //startUdpListener();
        }

        static async void StartTcpListener()
        {
            var tcpListener = new TcpListener(IPAddress.Any, DEFAULT_TCP_PORT);
            tcpListener.Start();

            try
            {
                await Console.Out.WriteLineAsync("Server started");

                while (true)
                {
                    var tcpClient = await tcpListener.AcceptTcpClientAsync();
                    await ProcessRequest(tcpClient);
                }
            }
            finally
            {
                tcpListener.Stop();
                await Console.Out.WriteLineAsync("Server finished");
            }
        }

        static async Task ProcessRequest(TcpClient tcpClient)
        {
            //yields back to the current context when awaited
            await Task.Yield();

            //get stream of the client
            var stream = tcpClient.GetStream();

            //read data
            using var reader = new StreamReader(stream);
            var data = await reader.ReadLineAsync();
            Console.WriteLine(data);

            //send answer
            using var writer = new StreamWriter(stream);
            await writer.WriteLineAsync($"'{data}' recieved");
            await writer.FlushAsync();

            //closing connection
            tcpClient.Close();
        }

        static async void StartUdpListener()
        {
            try
            {
                UdpClient udpClient = new UdpClient(DEFAULT_UDP_PORT);
                IPEndPoint remotePoint = null;

                byte[] bytes = udpClient.Receive(ref remotePoint);
                string results = Encoding.UTF8.GetString(bytes);


                udpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

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
            Server server = new Server();
            await server.StartTcpListener();

            //startUdpListener();
        }

        async Task StartTcpListener()
        {
            var listener = new TcpListener(IPAddress.Any, DEFAULT_TCP_PORT);
            listener.Start();
            Console.WriteLine("TcpListener started");
            try
            {
                while (true)
                    await Accept(await listener.AcceptTcpClientAsync());
            }
            finally { listener.Stop(); }
        }

        async Task Accept(TcpClient tcpClient)
        {
            await Task.Yield();
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

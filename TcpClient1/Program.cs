using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient1
{
    class Program
    {
        //private const int portNum = 13;
        //private const string hostName = "localhost";

        public static void Main(String[] args)
        {
            #region just Connect
            //try
            //{
            //    var client = new TcpClient(hostName, portNum);

            //    NetworkStream ns = client.GetStream();

            //    byte[] bytes = new byte[1024];
            //    int bytesRead = ns.Read(bytes, 0, bytes.Length);

            //    Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

            //    client.Close();

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}

            //Console.ReadLine();
            //return 0;
            #endregion

            TcpClient socketForServer;
            bool status = true;
            try
            {
                socketForServer = new TcpClient("localhost", 8100);
                Console.WriteLine("Connected to Server");
            }
            catch 
            {
                Console.WriteLine("Failed to Connect to Server{0}:999", "localhost");
                return;
            }
            NetworkStream networkStream = socketForServer.GetStream();
            StreamReader streamReader = new StreamReader(networkStream);
            StreamWriter streamWriter = new StreamWriter(networkStream);

            try
            {
                string clientMessage = "";
                string serverMessage = "";

                while(status)
                {
                    Console.WriteLine("Client:");
                    clientMessage = Console.ReadLine();
                    if((clientMessage == "bye") || (clientMessage == "BYE"))
                    {
                        status = false;
                        streamWriter.WriteLine("bye");
                        streamWriter.Flush();
                    }

                    if((clientMessage != "bye") && (clientMessage != "BYE"))
                    {
                        streamWriter.WriteLine(clientMessage);
                        streamWriter.Flush();
                        serverMessage = streamReader.ReadLine();
                        Console.WriteLine($"Server: {serverMessage} ");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception reading from the server");
            }
            streamReader.Close();
            networkStream.Close();
            streamWriter.Close();
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Client
{
    class MainProgram
    {
        static void Main()
        {

 

            Console.WriteLine("Socket Client ...");

            try
            {
                TcpClient tcpClient = new TcpClient("127.0.0.1",1302);

                string message = Console.ReadLine();

                int byteCount = Encoding.ASCII.GetByteCount(message+1);

                byte[] messageData = Encoding.ASCII.GetBytes(message);

                NetworkStream networkStream = tcpClient.GetStream();

                networkStream.Write(messageData, 0, messageData.Length);

                StreamReader streamReader = new StreamReader(networkStream);

                string response = streamReader.ReadLine();

                streamReader.Close();

                tcpClient.Close();

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }


        }
    }
}

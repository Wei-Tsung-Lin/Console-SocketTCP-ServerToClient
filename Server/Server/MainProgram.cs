using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace Server
{
    class MainProgram
    {
        static void Main()
        {
          
            Console.WriteLine("Get Local IP ...");

            var LocalIp = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in LocalIp.AddressList)
            {
                if(ip.AddressFamily==AddressFamily.InterNetwork)
                {
                    Console.WriteLine("IPV4 Adress ="+ip.ToString());
                }
            }

            Console.WriteLine("Socket Server ...");

            TcpListener listener = new TcpListener(System.Net.IPAddress.Any,1302);

            listener.Start();

            while(true)
            {

                TcpClient tcpClient = listener.AcceptTcpClient();

                NetworkStream networkStream = tcpClient.GetStream();

                StreamReader streamReader = new StreamReader(tcpClient.GetStream());

                StreamWriter streamWriter = new StreamWriter(tcpClient.GetStream());

                try
                {


                    byte[] buffer = new byte[1024];

                    networkStream.Read(buffer,0,buffer.Length);

                    int recev = 0;

                    foreach (byte _byte in buffer)
                    {
                        if(_byte!=0)
                        {
                            recev++;
                        }
                    }

                    string request = Encoding.UTF8.GetString(buffer,0,recev);

                    Console.WriteLine(request);

                    streamWriter.WriteLine(request);

                    streamWriter.Flush();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("error");

                    streamWriter.WriteLine(ex.ToString());
                }
            }
        }
    }
}

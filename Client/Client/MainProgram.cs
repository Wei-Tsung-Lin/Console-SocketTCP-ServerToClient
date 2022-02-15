using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;

namespace Client
{
    class MainProgram
    {

        static void SendMessage(Object _clientsocketobj)
        {

            while (true)
            {
                Socket socketclient = _clientsocketobj as Socket;

                string message = Console.ReadLine();

                socketclient.Send(Encoding.UTF8.GetBytes(message));
            }

        }

        static void ReceiveMessage(Object _clientSocketobj)
        {

            while (true)
            {
                Socket socketclient = _clientSocketobj as Socket;

                byte[] messageBuffer = new byte[1024];

                int messagecount = socketclient.Receive(messageBuffer);

                string message = Encoding.UTF8.GetString(messageBuffer, 0, messagecount);/*bytes,index and count*/

                DateTime localDate = DateTime.Now;

                Console.WriteLine(localDate + "   " + message);
            }


        }

        public static void MultiExecuteSendandReceive(Object _clientsocketobj)
        {
            ParameterizedThreadStart pts1, pts2;

            Thread mThread1, mThread2;

            pts1 = new ParameterizedThreadStart(SendMessage);

            pts2 = new ParameterizedThreadStart(ReceiveMessage);

            mThread1 = new Thread(pts1);

            mThread2 = new Thread(pts2);

            mThread1.Start(_clientsocketobj);

            mThread2.Start(_clientsocketobj);
        }

        static void Main()
        {

            Console.WriteLine("Socket Client");

            Socket socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketclient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000));

            Object clientsocketobj = socketclient;

            MultiExecuteSendandReceive(clientsocketobj); //put Object client into MultiThread to execute send and received from client...

        }

    }
}

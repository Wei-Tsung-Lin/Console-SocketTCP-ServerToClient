using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;


namespace Server
{
    public class MainProgram
    {
        public static void SendMessage(Object _clientsocketobj) /*Thread just only use object*/
        {
            while (true) /*Loop to execute*/
            {
                Socket socketclient = _clientsocketobj as Socket;

                string message = Console.ReadLine();                       

                socketclient.Send(Encoding.UTF8.GetBytes(message));
            }

        }

        public static void ReceiveMessage(Object _clientsocketobj)
        {

            while (true) /*Loop to execute*/
            {
                Socket socketclient = _clientsocketobj as Socket;

                byte[] messageBuffer = new byte[1024];

                int count = socketclient.Receive(messageBuffer);

                string message = System.Text.Encoding.UTF8.GetString(messageBuffer, 0, count);

                DateTime localDate = DateTime.Now;

                Console.WriteLine(localDate+"   "+message);
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

        public static void Main()
        {

            Console.WriteLine("Socket Server");

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//address,stream,tcp
            
            IPAddress ipaddress = IPAddress.Parse("127.0.0.1"); //ipv4, this is a local IP

            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, 7000); //address and port

            serverSocket.Bind(ipendpoint); //Bind

            serverSocket.Listen(1); //Listening

            Socket clientSocket = serverSocket.Accept(); //Accept a client

            Object clientsocketobj = clientSocket; //put client into Object

            MultiExecuteSendandReceive(clientsocketobj); //put Object client into MultiThread to execute send and received from client...

        }
    }
}

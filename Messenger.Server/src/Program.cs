using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Messenger.Server {
    class Program {

        public static string data = null;
        public static IPAddress IP = IPAddress.Parse("192.168.1.108");
        public static int PORT = 55000;
        static void Main(string[] args) {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress add in ipHostInfo.AddressList) {
                Console.WriteLine(add);
            }
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IP, PORT);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(IP.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true) {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;
                    // An incoming connection needs to be processed.  
                    //while (true) {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    //Thread.Sleep(5000);
                    
                        //if (data.IndexOf("<EOF>") > -1) {
                        //    break;
                        //}
                    //}

                    // Show the data on the console.  
                    Console.WriteLine($"Text received : {data} [{data.Length}]");

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.UTF8.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        public static void WriteLog(string logTxt) {
            DateTime time = DateTime.Now;
            //add logTxt to queue and that another thread writes
            //also attach time to it
        }
    }
}

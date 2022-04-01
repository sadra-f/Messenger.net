using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using Messenger.Server.src.Logic;
using System.Collections.Concurrent;

namespace Messenger.Server {
    public enum ELogType {
        ERROR,
        INFO
    }

    class Program {
        public static BigInteger ReqCounter = new BigInteger(0);
        public static IPAddress IP = IPAddress.Parse("192.168.1.108");
        public static int PORT = 55000;

        public static ConcurrentDictionary<string, IPEndPoint> onlineUsers;

        static void Main(string[] args) {
            onlineUsers = new ConcurrentDictionary<string, IPEndPoint>();
            // Data buffer for incoming data.

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
                    Socket initSocket = listener.Accept();
                    
                    new Thread(handler).Start(initSocket);
                    
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        private static void handler(object socket) {
            BigInteger reqNum = ReqCounter++;
            Socket respSocket = (Socket)socket;
            byte[] bytes = new byte[1024];
            int bytesRec = respSocket.Receive(bytes);
            string reqTxt = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);;

            Program.WriteLog($"[REQUEST] Req Number ({reqNum}) : [{reqTxt.Length}]{reqTxt} from : {'{'}" +
                $"{((IPEndPoint)respSocket.RemoteEndPoint).Address.ToString() + ":" +((IPEndPoint)respSocket.RemoteEndPoint).Port}{'}'}", reqNum, ELogType.INFO);

            string response = null;
            switch (reqTxt.Split(' ')[0]) {
                case "Make":
                    response = ReqHandler.Signup(reqTxt, reqNum);
                    break;
                case "Connect":
                    bool addOnline = false;
                    string onlineUser = null;
                    response = ReqHandler.Login(reqTxt, reqNum, out addOnline, out onlineUser);
                    if (addOnline) onlineUsers.TryAdd(onlineUser, ((IPEndPoint)respSocket.RemoteEndPoint));
                    break;
                case "Pm":
                    response = ReqHandler.PrivateMessage(reqTxt, reqNum);
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
                    break;

            }
            respSocket.Send(Encoding.UTF8.GetBytes(response));
            Program.WriteLog($"[RESPONSE] Req Number ({reqNum}) : [{response.Length}]{response} To : {'{'}" +
            $"{((IPEndPoint)respSocket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)respSocket.RemoteEndPoint).Port}{'}'}", reqNum, ELogType.INFO);
            respSocket.Shutdown(SocketShutdown.Both);
            respSocket.Close();
        }
        public static void WriteLog(string logTxt,BigInteger reqNum ,ELogType logType = ELogType.ERROR) {
            DateTime time = DateTime.Now;
            Console.WriteLine($"[{logType}][{time}] : ReqNum ({reqNum}): {logTxt}");
            //add logTxt to queue and that another thread writes
            //also attach time to it
        }
    }
}

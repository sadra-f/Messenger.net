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
using System.IO;

namespace Messenger.Server {
    public enum ELogType {
        ERROR,
        INFO
    }

    class Program {
        public static BigInteger ReqCounter = new BigInteger(0);
        public static IPAddress IP = IPAddress.Parse("192.168.1.108");
        public static readonly int PORT = 55000;

        public static ConcurrentDictionary<string, MUserEndpoint> onlineUsers;
        public static int ReadMessageCount = 10;//TODO increse this and send the mesage in more than one message to client

        static void Main(string[] args) {
            onlineUsers = new ConcurrentDictionary<string, MUserEndpoint>();  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress add in ipHostInfo.AddressList) {
                Console.WriteLine(add);
            }
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IP, PORT);
  
            Socket listener = new Socket(IP.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true) {
                    Console.WriteLine("Waiting for a connection...");
                    Socket initSocket = listener.Accept();
                    
                    new Thread(handler).Start(initSocket);
                    
                }

            }
            catch (Exception e) {
                WriteLog(e.ToString(), -1, ELogType.ERROR);
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        private static void handler(object socket) {
            BigInteger reqNum = ReqCounter++;
            Socket respSocket = (Socket)socket;
            byte[] bytes = new byte[1024];
            int bytesRec = respSocket.Receive(bytes);
            string reqTxt = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);

            Program.WriteLog($"[REQUEST] Req Number ({reqNum}) : [{reqTxt.Length}]{reqTxt} from : {'{'}" +
                $"{((IPEndPoint)respSocket.RemoteEndPoint).Address} : " +
                $"{ ((IPEndPoint)respSocket.RemoteEndPoint).Port}{'}'}", reqNum, ELogType.INFO);

            string response = null;
            try {

                switch (reqTxt.Split(' ')[0]) {
                    case "Make":
                        response = ReqHandler.Signup(reqTxt, reqNum);
                        break;
                    case "Connect": {
                            bool addOnline = false;
                            string onlineUser = null;
                            int userPort = -1;
                            response = ReqHandler.Login(reqTxt, reqNum, out addOnline, out onlineUser, out userPort);
                            if (addOnline) onlineUsers.TryAdd(onlineUser, new MUserEndpoint((IPEndPoint)respSocket.RemoteEndPoint, userPort));
                            break;
                        }
                    case "Pm": {
                            string reciverUsername = null;
                            string msg = null;

                            response = ReqHandler.PrivateMessage(reqTxt, reqNum, out reciverUsername, out msg);
                            if (onlineUsers.Keys.Contains(reciverUsername)) {
                                SendMessage(onlineUsers[reciverUsername], msg, reqNum);
                            }
                            break;
                        }
                    case "Contacts":
                        response = ReqHandler.Contacts(reqTxt, reqNum);
                        break;
                    case "ChatList":
                        response = ReqHandler.ContactChat(reqTxt, reqNum);
                        break;
                    case "CreateGp": { 
                            response = ReqHandler.CreateNewGroup(reqTxt, reqNum);
                            bool sendMessage = false;
                            string username = "";
                            string gName = "";
                            ReqHandler.AddGroupMember(reqTxt, reqNum, out sendMessage, out username, out gName);
                        }
                        break;
                    case "GroupsLst":
                        response = ReqHandler.Groups(reqTxt, reqNum);
                        break;
                    case "GroupChatLst":
                        response = ReqHandler.GroupChat(reqTxt, reqNum);
                        break;
                    case "Users":
                        response = ReqHandler.GroupUsers(reqTxt, reqNum);
                        break;
                    case "GM": {
                            List<string> recivers = null;
                            string msg = null;
                            bool canSend = false;
                            response = ReqHandler.GroupMsg(reqTxt, reqNum, out recivers, out msg, out canSend);
                            if (canSend) {
                                if(msg != null)
                                foreach(string str in recivers) {
                                    if (onlineUsers.Keys.Contains(str)) {
                                        new Thread(() => SendMessage(onlineUsers[str], msg, reqNum)).Start();
                                    }
                                }
                            }
                            break;
                        }
                    case "AddMember": {
                            bool sendMessage = false;
                            string username = "";
                            string gName = "";
                            response = ReqHandler.AddGroupMember(reqTxt, reqNum, out sendMessage, out username, out gName);
                            if (sendMessage) {
                                List<string> recivers = null;
                                string msg = null;
                                bool canSend = false;
                                string FakeReqTxt = $"GM -Option<gname:{gName}> -Option<user:Server> -Option<len:{$"{username} join the chat room.".Length}> -Option<body:<{username}> join the chat room.>";
                                ReqHandler.GroupMsg(FakeReqTxt, reqNum, out recivers, out msg, out canSend);
                                if (canSend) {
                                    if (msg != null)
                                        foreach (string str in recivers) {
                                            if (onlineUsers.Keys.Contains(str)) {
                                                new Thread(() => SendMessage(onlineUsers[str], msg, reqNum)).Start();
                                            }
                                        }
                                }
                            }
                            break;
                        }
                    case "END": {
                            bool sendMessage = false;
                            string username = "";
                            string gName = "";
                            response = ReqHandler.RemoveGroupMember(reqTxt, reqNum, out sendMessage, out username, out gName);
                            if (sendMessage) {
                                List<string> recivers = null;
                                string msg = null;
                                bool canSend = false;
                                string FakeReqTxt = $"GM -Option<gname:{gName}> -Option<user:Server> -Option<len:" +
                                    $"{$"<{username}> Left the chat room.".Length}> -Option<body:<{username}> Left the chat room.>";

                                ReqHandler.GroupMsg(FakeReqTxt, reqNum, out recivers, out msg, out canSend);
                                if (canSend) {
                                    if (msg != null)
                                        foreach (string str in recivers) {
                                            if (onlineUsers.Keys.Contains(str)) {
                                                new Thread(() => SendMessage(onlineUsers[str], msg, reqNum)).Start();
                                            }
                                        }
                                }
                            }
                            break;
                        }
                    case "13":
                        break;

                }
                respSocket.SendTimeout = 1000;
                respSocket.Send(Encoding.UTF8.GetBytes(response));
                Program.WriteLog($"[RESPONSE] Req Number ({reqNum}) : [{response.Length}]{response} To : {'{'}" +
                $"{((IPEndPoint)respSocket.RemoteEndPoint).Address} : " +
                $"{((IPEndPoint)respSocket.RemoteEndPoint).Port}{'}'}", reqNum, ELogType.INFO);

                respSocket.Shutdown(SocketShutdown.Both);
                respSocket.Close();
            }catch (Exception e) {
                WriteLog(e.Message, reqNum, ELogType.ERROR);
            }
        }

        private static void SendMessage(MUserEndpoint userEndpoint, string msg, BigInteger reqNum) {
            Socket socket = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.SendTimeout = 1000;
            try {
                Program.WriteLog($"[Message] Req Number ({reqNum}) : [{msg.Length}]{msg} to : {'{'}" +
                $"{userEndpoint.userIp} : { userEndpoint.ListenerPort}{'}'}", reqNum, ELogType.INFO);
                socket.Connect(userEndpoint.userIp.Address, userEndpoint.ListenerPort);
                
                socket.Send(Encoding.UTF8.GetBytes(msg));
                
            }catch (Exception e) {
                WriteLog(e.Message, reqNum, ELogType.ERROR);
            }
            finally {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        public static void WriteLog(string logTxt,BigInteger reqNum ,ELogType logType = ELogType.ERROR) {
            DateTime time = DateTime.Now;
            Console.WriteLine($"[{logType}][{time}] : ReqNum ({reqNum}): {logTxt}");
            //File.AppendAllText("C:\\Users\\TOP\\Desktop\\New Text Document.txt", $"[{logType}][{time}] : ReqNum ({reqNum}): {logTxt}\r\n");
            //add logTxt to queue and that another thread writes
            //also attach time to it
        }

    }
}

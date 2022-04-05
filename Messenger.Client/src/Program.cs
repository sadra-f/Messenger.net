﻿using Messenger.Client.src.Forms;
using Messenger.Client.src.Models.ConnectionModels;
using Messenger.Client.src.Models.DBModels.Messaging;
using Messenger.Client.src.Models.DBModels.People;
using Messenger.Client.src.ServerConnection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger.Client {

    enum LogType {
        INFO,
        ERROR,
    }

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static Form currentForm;

        public static MPerson user;
        public static bool isLoggedIn;
        public static List<string> contacts;
        public static List<string> groups;

        private static Thread listener;

        public static IPAddress SEERVER_IP = IPAddress.Parse("192.168.1.108");
        public static readonly  int PORT = 55000;
        public static int messagePort = 55001;


        [STAThread]
        static void Main() {
            listen();
            contacts = new List<string>();
            groups = new List<string>();
            isLoggedIn = false;
            user = null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            currentForm = new frmLogin();
            Application.Run(currentForm);
        }


        public static void show(string value, LogType type = LogType.ERROR) {
            MessageBox.Show(currentForm, value, type.ToString(), MessageBoxButtons.OK,
                (type == LogType.ERROR ? MessageBoxIcon.Error : MessageBoxIcon.Information));
        }

        public async static Task<MLoginResponse> LoginReq(string username, string pass) {
            string resp = await Server.Login(new MUser(username, pass));
            var res = new MLoginResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if(res.result.ToLower().Trim() == "connected") {
                res.resultType = EResultType.SUCCESS;
                res.user = new MPerson(int.Parse(res.options["id"]), username, pass);
                Program.user = new MPerson(res.user);
                isLoggedIn = true;
            }
            else if(res.result.ToLower().Trim() == "error") {
                res.resultType = EResultType.FAIL;
                res.user = null;
            }
            return res;
        }
        
        public async static Task ContactsReq() {
            string resp = await Server.ContactList();
            resp = resp.Replace("\0", String.Empty);
            string[] splitted = resp.Split('|');
            string res = splitted[0];
            if(res.Trim().ToLower() == "contacts") {
                for (int i = 1; i < splitted.Length; i++) {
                    contacts.Add(splitted[i]);
                }
            }
                        
        }
        
        public async static Task<List<MContactChat>> ContactChatReq(string username) {
            var resp = await Server.ContactChats(username);
            Dictionary<string, string> options = ExtractOptions(resp);
            var res = new List<MContactChat>();
            if(resp.Split(' ')[0].Trim().ToLower() == "chats") {
                string[] splittedMsgs = options["msgs"].Split('|');
                for (int i = 0; i < splittedMsgs.Length; i++) {
                    res.Add(new MContactChat(splittedMsgs[i].Split(":".ToCharArray(), 2)[0], splittedMsgs[i].Split(":".ToCharArray(), 2)[1]));
                }
            }
            return res;    
        }

        internal async static Task<AResponse> CreateGroupReq(string gpName, string gpDesc) {
            string resp = await Server.CreateGroup(gpName, gpDesc);
            var res = new AResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if (res.result.ToLower().Trim() == "created") {
                groups.Add(gpName);
                res.resultType = EResultType.SUCCESS;
            }
            else if (res.result.ToLower().Trim() == "not created") {
                Program.show(res.options["reason"], LogType.INFO);
                res.resultType = EResultType.FAIL;
            }
            return res;
        }

        public async static Task<MSignupResponse> SignupReq(string username, string pass) {
            string resp = await Server.Signup(new MUser(username, pass));
            var res = new MSignupResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if (res.result.ToLower().Trim() == "user accepted") {
                res.resultType = EResultType.SUCCESS;                
            }
            else if (res.result.ToLower().Trim() == "user not accepted") {
                res.resultType = EResultType.FAIL;
            }
            return res;
        }
        public async static Task<AResponse> PMReq(string to, string message) {
            string resp = await Server.PMessage(new MPrivateMessage(to, message));
            var res = new AResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if (res.result.ToLower().Trim() == "sent") {
                res.resultType = EResultType.SUCCESS;
            }
            else if (res.result.ToLower().Trim() == "not sent") {
                res.resultType = EResultType.FAIL;
            }
            return res;
        }


        public static void messageLinstener() {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            

            // Create a TCP/IP socket.  

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            Socket listener = null;
            while (true) {
                try {
                    IPEndPoint localEndPoint = new IPEndPoint(SEERVER_IP, messagePort);
                    listener = new Socket(SEERVER_IP.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                    listener.Bind(localEndPoint);
                    listener.Listen(1);
                    break;
                }
                catch (SocketException e) {
                    messagePort += 1;
                    if (messagePort > 65000) {
                        MessageBox.Show($"All Ports are Taken Cant Function lastTried:({messagePort})");
                        //throw new Exception("You Dont Seem to Want me Here all ports are used");
                        return;
                    }
                    continue;
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                    return;
                }
                //MessageBox.Show($"listening on port:({messagePort})");
            }
                //Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  
            while (true) {
                Socket initSocket = listener.Accept();
                try {
                    byte[] bytes = new byte[1024];
                    int bytesRec = initSocket.Receive(bytes);
                    //string reqTxt = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);
                    string[] splitted = Encoding.UTF8.GetString(bytes).Split('\0');
                    if (splitted[0] == "KILLYOURSELF") return;
                    if (splitted[2] != user.Username) continue;
                    if (currentForm is frmChat) {
                        if (((frmChat)currentForm).EndUser.Username == splitted[1]) {
                            ((frmChat)currentForm).Invoke((MethodInvoker)delegate () {
                                ((frmChat)currentForm).addMessage(splitted[4]);
                            });
                        }
                        continue;
                    }
                    else if (currentForm is frmHome) {
                        ((frmHome)currentForm).Invoke((MethodInvoker)delegate () {
                            ((frmHome)currentForm).NewMessage(splitted[1], splitted[4].Replace("\0", string.Empty));
                        });
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                    break;
                }
                initSocket.Shutdown(SocketShutdown.Both);
                initSocket.Close();
                initSocket = null;
            }
            listener.Close();
            
        }

        public static Dictionary<string, string> ExtractOptions(string response) {
            var res = new Dictionary<string, string>();
            response = response.Replace("\0", String.Empty);
            string[] separated = response.Split(new string[] {"-Option"},StringSplitOptions.None);
            res.Add("result", separated[0]);
            foreach(string str in separated) {
                if (str.Trim().StartsWith("<")) {
                    string key = (str.Trim().Substring(1, str.Trim().IndexOf(":"))).Replace(":", string.Empty);
                    string value = (str.Trim().Substring(str.Trim().IndexOf(":") + 1, str.Trim().LastIndexOf(">") - (str.Trim().IndexOf(":") + 1))).Trim();
                    res.Add(key.Trim(), value.Trim());
                }
            }
            return res;
        }

        //[Obsolete]
        public static void killListener() {
            if (listener == null) return;
            if (listener.IsAlive) {
                try {
                    //MessageBox.Show("killing");
                    Socket socket = new Socket(SEERVER_IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(Dns.GetHostEntry(Dns.GetHostName()).AddressList[Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length-1], messagePort);

                    socket.Send(Encoding.UTF8.GetBytes("KILLYOURSELF"));
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }catch(Exception e) {
                    listener.Abort();
                    MessageBox.Show(e.Message);
                    return;
                }
                //MessageBox.Show("killed");
                listener = null;
                return;
            }
        }
        public static void listen() {
            if(listener != null) {
                if (listener.IsAlive) {
                    return;
                }
                else {
                    listener.Start();
                    return;
                }
            }
            listener = new Thread(messageLinstener);
            listener.Start();
        }
    }
}

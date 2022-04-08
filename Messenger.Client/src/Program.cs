using Messenger.Client.src.Forms;
using Messenger.Client.src.Models;
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
        public static Dictionary<string, MGroupICU> groups;

        private static Thread listener;

        public static IPAddress SEERVER_IP = IPAddress.Parse("192.168.1.108");
        public static readonly int PORT = 55000;
        public static int messagePort = 55001;


        [STAThread]
        static void Main() {
            listen();
            contacts = new List<string>();
            groups = new Dictionary<string, MGroupICU>();
            isLoggedIn = false;
            user = null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            currentForm = new frmLogin();
            Application.Run(currentForm);
        }

        public async static Task<AResponse> LeaveGroupReq(string groupName) {
            string resp = await Server.LeaveGroup(groupName, user.Username);
            var res = new AResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if (res.result.ToLower().Trim() == "removed") {
                res.resultType = EResultType.SUCCESS;
            }
            else if (res.result.ToLower().Trim() == "not removed") {
                res.resultType = EResultType.FAIL;
            }
            return res;
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

            if (res.result.ToLower().Trim() == "connected") {
                res.resultType = EResultType.SUCCESS;
                res.user = new MPerson(int.Parse(res.options["id"]), username, pass);
                Program.user = new MPerson(res.user);
                isLoggedIn = true;
            }
            else if (res.result.ToLower().Trim() == "error") {
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
            if (res.Trim().ToLower() == "contacts") {
                for (int i = 1; i < splitted.Length; i++) {
                    contacts.Add(splitted[i]);
                }
            }

        }

        public async static Task GroupsReq() {
            string resp = await Server.GroupList();
            // check lines below
            resp = resp.Replace("\0", String.Empty);
            string[] splitted = resp.Split('|');
            string res = splitted[0];
            if (res.Trim().ToLower() == "groups") {
                for (int i = 1; i < splitted.Length; i++) {
                    groups.Add(splitted[i], new MGroupICU(splitted[i++], splitted[i++], splitted[i]));
                }
            }
        }

        public async static Task<List<MChat>> GroupChatReq(string gName) {
            var resp = await Server.GroupChat(gName);
            Dictionary<string, string> options = ExtractOptions(resp);
            var res = new List<MChat>();
            if (options["result"].Trim().ToLower() == "chats") {
                string[] splittedMsgs = options["msgs"].Split('|');
                for (int i = 0; i < splittedMsgs.Length; i++) {
                    res.Add(new MChat(splittedMsgs[i].Split(":".ToCharArray(), 2)[0], splittedMsgs[i].Split(":".ToCharArray(), 2)[1]));
                }
            }
            return res;
        }

        public async static Task GroupUsersReq(string group) {
            if (!groups.Keys.Contains(group)) throw new Exception("You Are Not A Member Of This Group!");
            var resp = await Server.GroupUsers(group);
            var options = ExtractOptions(resp);
            if (options["result"].Trim().ToLower() == "users_list") {
                groups[group].Users = new List<string>();
                foreach (string str in options["users"].Split('|')) {
                    if (str.Length < 1) continue;
                    groups[group].Users.Add(str);
                }
            }

        }

        public async static Task<AResponse> AddGroupMemberReq(string groupName, string username) {
            string resp = await Server.AddGroupMember(groupName, username);
            var res = new AResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if (res.result.ToLower().Trim() == "added") {
                res.resultType = EResultType.SUCCESS;
            }
            else if (res.result.ToLower().Trim() == "not added") {
                res.resultType = EResultType.FAIL;
            }
            return res;
        }

        public async static Task<List<MChat>> ContactChatReq(string username) {
            var resp = await Server.ContactChats(username);
            Dictionary<string, string> options = ExtractOptions(resp);
            var res = new List<MChat>();
            if(resp.Split(' ')[0].Trim().ToLower() == "chats") {
                string[] splittedMsgs = options["msgs"].Split('|');
                for (int i = 0; i < splittedMsgs.Length; i++) {
                    res.Add(new MChat(splittedMsgs[i].Split(":".ToCharArray(), 2)[0], splittedMsgs[i].Split(":".ToCharArray(), 2)[1]));
                }
            }
            return res;    
        }

        public async static Task<AResponse> GMReq(string gName, string msg) {
            string resp = await Server.GroupMsg(gName, msg);
            var res = new AResponse();
            res.options = ExtractOptions(resp);
            res.result = res.options["result"].Trim();
            res.resultType = EResultType.FAIL;
            
            if(res.result.ToLower() == "sent") {
                res.resultType = EResultType.SUCCESS;
            }
            return res;
        }

        public async static Task<AResponse> CreateGroupReq(string gpName, string gpDesc) {
            string resp = await Server.CreateGroup(gpName, gpDesc);
            var res = new AResponse();

            res.options = ExtractOptions(resp);
            res.result = res.options["result"];

            if (res.result.ToLower().Trim() == "created") {
                groups.Add(gpName, new MGroupICU(gpName, gpDesc, user.Username));
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

            bool doLoop = true;
            while (doLoop) {
                Socket initSocket = listener.Accept();
                try {
                    byte[] bytes = new byte[1024];
                    int bytesRec = initSocket.Receive(bytes);
                    //string reqTxt = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);
                    //string[] splitted = Encoding.UTF8.GetString(bytes).Split('\0');
                    Dictionary<string, string> options = ExtractOptions(Encoding.UTF8.GetString(bytes));
                    switch (options["result"].Trim().ToLower()) {
                        case "killyourself": {
                                initSocket.Shutdown(SocketShutdown.Both);
                                initSocket.Close();
                                doLoop = false;
                                break;
                            }
                        case "pm": {
                                if (options["to"] != user.Username) continue;
                                if (currentForm is frmChat) {
                                    if (((frmChat)currentForm).EndUser.Username == options["from"]) {
                                        ((frmChat)currentForm).Invoke((MethodInvoker)delegate () {
                                            ((frmChat)currentForm).addMessage(options["body"]);
                                        });
                                    }
                                    continue;
                                }
                                else if (currentForm is frmHome) {
                                    ((frmHome)currentForm).Invoke((MethodInvoker)delegate () {
                                        ((frmHome)currentForm).NewPrivateMessage(options["from"], options["body"].Replace("\0", string.Empty));
                                    });
                                }
                                break;
                            }
                        case "gm": {
                                if (currentForm is frmGroupChat) {
                                    currentForm.Invoke((MethodInvoker)delegate () {
                                        ((frmGroupChat)currentForm).addMessage(options["body"], options["from"], options["gname"]);
                                    });
                                }else if (currentForm is frmHome) {
                                    ((frmHome)currentForm).Invoke((MethodInvoker)delegate () {
                                        ((frmHome)currentForm).NewGroupMessage(options["body"], options["from"], options["gname"]);
                                    });
                                }

                                break;
                            }
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                    break;
                }
                
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

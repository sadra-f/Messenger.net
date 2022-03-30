using Messenger.Client.src.Forms;
using Messenger.Client.src.Models.ConnectionModels;
using Messenger.Client.src.Models.DBModels.People;
using Messenger.Client.src.ServerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger.Client {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new frmChat());*/
        public static IPAddress IP = IPAddress.Parse("192.168.1.108");
        public static int PORT = 55000;

        public static MPerson user;
        public static bool isLoggedIn;

        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
            isLoggedIn = false;
            user = null;
        }

        public static void show(string value) {
            MessageBox.Show(value);
        }

        public async static Task<MLoginResponse> LoginReq(string username, string pass) {
            string resp = await Server.Login(new MUser(username, pass));
            var separted = resp.Split(' ');
            var res = new MLoginResponse();
            res.options = ExtractOptions(resp);
            res.result = res.options["result"];
            if(res.result.ToLower() == "connected") {
                res.resultType = EResultType.SUCCESS;
                res.user = new MPerson(int.Parse(res.options["id"]), username, pass);
                Program.user = new MPerson(res.user);
                isLoggedIn = true;
            }
            else if(res.result.ToLower() == "error") {
                res.resultType = EResultType.FAIL;
                res.user = null;
            }
            return res;
        }
        
        public async static Task<MSignupResponse> SignupReq(string username, string pass) {
            string resp = await Server.Signup(new MUser(username, pass));
            var separted = resp.Split(' ');
            var res = new MSignupResponse();
            res.options = ExtractOptions(resp);
            res.result = res.options["result"];
            if (res.result.ToLower() == "user accepted") {
                res.resultType = EResultType.SUCCESS;                
            }
            else if (res.result.ToLower() == "user not accepted") {
                res.resultType = EResultType.FAIL;
            }
            return res;
        }

        public static Dictionary<string, string> ExtractOptions(string response) {
            var res = new Dictionary<string, string>();
            response = response.Replace("\0", String.Empty);
            string[] separated = response.Split(new string[] {"-Option"},StringSplitOptions.None);
            res.Add("result", separated[0]);
            foreach(string str in separated) {
                if (str.Trim().StartsWith("<")) {
                    string key = str.Trim().Substring(1, str.Trim().IndexOf(":"));
                    string value = str.Trim().Substring(str.Trim().IndexOf(":") + 1, str.Trim().LastIndexOf(">") - (str.Trim().IndexOf(":") + 1));
                    res.Add(key.Trim(), value.Trim());
                }
            }
            return res;
        }
    }
}

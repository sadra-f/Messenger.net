using Messenger.Server.src.Database;
using Messenger.Server.src.Database.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Logic {
    static class ReqHandler {
        public static string Signup(string req, BigInteger ReqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(req);
                if(DbAccess.PostPerson(new Database.Models.People.MPerson(options["user"], options["pass"])) == ActionResult.SUCCESS) {
                    return $"User Accepted -Option<id:>";
                }
                return "User not Accepted -Option<reason:Username Already Exists>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, ReqNum, ELogType.ERROR);
                return "User not Accepted -Option<reason:Failed To Added User to Database>";
            }
        }

        public static string Login(string req, BigInteger ReqNum, out bool addToOnline, out string username) {
            try {
                Dictionary<string, string> options = ExtractOptions(req);
                var person = new MPerson(options["user"], options["pass"]);
                addToOnline = false;
                username = null;
                if (Program.onlineUsers.Keys.Contains(options["user"])) {
                    return $"Error -Option <reason:User Not Found>";
                }
                if (DbAccess.ReadLogin(ref person) == ActionResult.SUCCESS) {
                    addToOnline = true;
                    username = options["user"];
                    return $"Connected -Option <id:{person.ID}> -Option<SID:>";
                }
                return $"Error -Option <reason:User Not Found>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, ReqNum, ELogType.ERROR);
                addToOnline = false;
                username = null;
                return $"Error -Option <reason:User Not Found>";
            }
        }

        public static string PrivateMessage(string reqTxt, BigInteger reqNum) {
            throw new NotImplementedException();
        }

        public static Dictionary<string, string> ExtractOptions(string data) {
            var res = new Dictionary<string, string>();
            data = data.Replace("\0", String.Empty);
            string[] separated = data.Split(new string[] { "-Option" }, StringSplitOptions.None);
            res.Add("req", separated[0]);
            foreach (string str in separated) {
                if (str.Trim().StartsWith("<")) {
                    string key = (str.Trim().Substring(1, str.Trim().IndexOf(":"))).Replace(":", string.Empty);
                    string value = (str.Trim().Substring(str.Trim().IndexOf(":") + 1, str.Trim().LastIndexOf(">") - (str.Trim().IndexOf(":") + 1))).Trim();
                    res.Add(key.Trim(), value.Trim());
                }
            }
            return res;
        }
    }
}

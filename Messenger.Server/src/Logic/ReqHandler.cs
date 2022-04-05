using Messenger.Server.src.Database;
using Messenger.Server.src.Database.Models.Clustering;
using Messenger.Server.src.Database.Models.Messaging;
using Messenger.Server.src.Database.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Logic {
    static class ReqHandler {
        public static string Signup(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                if(DbAccess.CreatePerson(new Database.Models.People.MPerson(options["user"], options["pass"])) == ActionResult.SUCCESS) {
                    return $"User Accepted -Option<id:>";
                }
                return "User not Accepted -Option<reason:Username Already Exists>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                return "User not Accepted -Option<reason:Failed To Added User to Database>";
            }
        }

        public static string Login(string reqTxt, BigInteger reqNum, out bool addToOnline, out string username, out int listenPort) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                var person = new MPerson(options["user"], options["pass"]);
                addToOnline = false;
                username = null;
                listenPort = -1;
                if (Program.onlineUsers.Keys.Contains(options["user"])) {
                    return $"Error -Option <reason:User Not Found>";
                }
                if (DbAccess.ReadLogin(ref person) == ActionResult.SUCCESS) {
                    addToOnline = true;
                    username = options["user"];
                    listenPort = int.Parse(options["port"]);
                    return $"Connected -Option <id:{person.ID}> -Option<SID:>";
                }
                return $"Error -Option <reason:User Not Found>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                addToOnline = false;
                username = null;
                listenPort = -1;
                return $"Error -Option <reason:User Not Found>";
            }
        }

        public static string PrivateMessage(string req, BigInteger reqNum, out string messageReciver, out string msg) {
            try {
                Dictionary<string, string> options = ExtractOptions(req);
                MContacts contacts = null;
                messageReciver = null;
                msg = null;
                bool canSend = false;
                if (DbAccess.ReadContact(options["from"], options["to"], out contacts) == ActionResult.SUCCESS) {
                    if(contacts != null) {
                        canSend = true;
                    }
                    else if (DbAccess.CreateContact(options["from"], options["to"]) == ActionResult.SUCCESS) {
                        DbAccess.ReadContact(options["from"], options["to"], out contacts);
                        canSend = true;
                    }
                    else {
                        return "Not Sent";
                    }
                }
                if (canSend) {
                    MContactMsg msgMdl = new MContactMsg(contacts.ID, options["body"]);
                    if(DbAccess.CreateMessage(msgMdl, options["from"]) == ActionResult.SUCCESS) {
                        messageReciver = options["to"];
                        msg = $"Pm\0{options["from"]}\0{options["to"]}\0{options["body"].Length}\0{options["body"]}";
                        return "Sent";
                    }
                    return "not Sent";
                }
                
                return $"Error -Option <reason:Failed To Add To DB>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                messageReciver = null;
                msg = null;
                return $"Error -Option <reason:Server Error>";
            }
        }

        public static string Contacts(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                List<string> contacts = null;
                if (DbAccess.ReadContacts(options["user"], out contacts) == ActionResult.SUCCESS) {
                    StringBuilder strB = new StringBuilder("contacts|");
                    if(contacts.Count > 0) {
                        for (int i = 0; i < contacts.Count; i++) {
                            strB.Append(contacts[i]);
                            if(i < contacts.Count - 1) strB.Append('|');
                        }
                        return strB.ToString();
                    }
                }
                return "None Found";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                return "None Found";
            }
        }

        public static string ContactChat(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                MContacts contacts = null;
                if (DbAccess.ReadContact(options["user1"], options["user2"], out contacts) == ActionResult.SUCCESS) {
                    string msgs = null;
                    if (contacts != null) { 
                        if (DbAccess.ReadContactMsg(contacts.ID, Program.ReadMessageCount, out msgs) == ActionResult.SUCCESS) {
                            return $"Chats -Option<user1:{options["user1"]}> -Option<user2:{options["user2"]}> -Option<msgs:{msgs}>";
                        }
                    }
                }
                return "None Found";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                return "None Found";
            }
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

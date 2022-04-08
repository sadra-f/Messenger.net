using Messenger.Server.src.Database;
using Messenger.Server.src.Database.Models.Clustering;
using Messenger.Server.src.Database.Models.Messaging;
using Messenger.Server.src.Database.Models.People;
using Messenger.Server.src.Logic.ICUModels;
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

        public static string PrivateMessage(string reqTxt, BigInteger reqNum, out string messageReciver, out string msg) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
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
                        msg = $"Pm  -Option<from:{options["from"]}> -Option<to:{options["to"]}> -Option<len:{options["body"].Length}> -Option<body:{options["body"]}>";
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

        public static string CreateNewGroup(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                bool didCreate = false;
                if (DbAccess.CreateGroup(options["user"], options["name"], options["desc"], out didCreate) == ActionResult.SUCCESS) {
                    if (didCreate) {
                        return "Created";
                    }
                }
                return "Not Created -Option<reason:Failed To Add To DB>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                return $"Not Created -Option<reason:{e.Message}>";
            }
        }

        public static string GroupMsg(string reqTxt, BigInteger reqNum, out List<string> recivers, out string msg, out bool canSend) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                recivers = null;
                msg = null;
                bool isMember = false;
                canSend = false;
                if (DbAccess.ReadGpMembership(options["gname"], options["user"], out isMember) == ActionResult.SUCCESS) {
                    if (isMember) {
                        canSend = true;
                    }
                    else {
                        return "Not Sent -Option <reason:Credentials Failed At DB>";
                    }
                }
                if (canSend) {
                    if (DbAccess.CreateGroupMsg(options["gname"], options["user"], options["body"]) == ActionResult.SUCCESS) {

                        if(DbAccess.ReadGroupUsers(options["gname"], out recivers) == ActionResult.SUCCESS) {
                            if (recivers.Contains(options["user"])) {
                                recivers.Remove(options["user"]);
                            }
                            msg = $"GM -Option<from:{options["user"]}> -Option<gname:{options["gname"]}> -Option<len:{options["body"].Length}> -Option<body:{options["body"]}>";
                            return "Sent";
                        }
                    }
                    return "not Sent -Option <reason:Failed To Send To Users>";
                }
                return $"Error -Option <reason:Credentials Failed At DB>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                recivers = null;
                msg = null;
                canSend = false;
                return $"Error -Option <reason:Server Error>";
            }
        }

        public static string AddGroupMember(string reqTxt, BigInteger reqNum, out bool sendMessage, out string username, out string gName) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                sendMessage = false;
                username = "";
                gName = "";
                if (DbAccess.CreateGroupMember(options["gname"], options["user"]) == ActionResult.SUCCESS) {
                    sendMessage = true;
                    username = options["user"];
                    gName = options["gname"];
                    return "Added";
                }
                return "Not Added -Option<reason:Failed To Add to DB>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                sendMessage = false;
                username = "";
                gName = "";
                return $"Not Added -Option<reason:{e.Message}>";
            }
        }

        public  static string GroupUsers(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                List<string> users = null;
                if (DbAccess.ReadGroupUsers(options["name"], out users) == ActionResult.SUCCESS) {
                    StringBuilder strB = new StringBuilder("USERS_LIST -Option<users:");
                    if (users == null) return "None Found";
                    if (users.Contains("Server")) users.Remove("Server");
                    if (users.Count > 0) {
                        for (int i = 0; i < users.Count; i++) {
                            strB.Append(users[i]);
                            if (i < users.Count - 1) strB.Append('|');
                        }
                        strB.Append('>');
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

        public static string Groups(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                List<MGroupICU> Groups = null;
                if (DbAccess.ReadGroups(options["user"], out Groups) == ActionResult.SUCCESS) {
                    StringBuilder strB = new StringBuilder("Groups|");
                    if(Groups != null) {
                        if (Groups.Count > 0) {
                            for (int i = 0; i < Groups.Count; i++) {
                                strB.Append(Groups[i].ToString());
                                if (i < Groups.Count - 1) strB.Append('|');
                            }
                            return strB.ToString();
                        }
                    }
                }
                return "None Found -Option<reason:Failed To Read Any From Database>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                return $"None Found -Option<reason:{e.Message}>";
            }
        }

        public static string GroupChat(string reqTxt, BigInteger reqNum) {
            try {
                Dictionary<string, string> options = ExtractOptions(reqTxt);
                List<string> chat = null;
                if (DbAccess.ReadGroupMsg(options["name"], Program.ReadMessageCount, out chat) == ActionResult.SUCCESS) {
                    StringBuilder strB = new StringBuilder("chats -Option<msgs:");
                    if (chat != null) {
                        for (int i = 0; i < chat.Count; i++) {
                            strB.Append(chat[i]);
                            if (i < chat.Count - 1) strB.Append('|');
                        }
                        strB.Append('>');
                        return strB.ToString();
                    }
                }
                return "None Found -Option<reason:Failed To Read Any From DB>";
            }
            catch (Exception e) {
                Program.WriteLog(e.Message, reqNum, ELogType.ERROR);
                return $"None Found -Option<reason : {e.Message}>";
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

using Messenger.Server.src.Database.Models.Clustering;
using Messenger.Server.src.Database.Models.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Database {
    public enum ActionResult{ 
        SUCCESS,
        FAILURE,
        EXCEPTION,
    }

    enum QueryType {
        CREATE,
        READ_ALL,
        READ_SCALAR,
        UPDATE,
        DELETE,

    }
    //perform CRUD to database using ado
    static class DbAccess {
        private readonly static string CONNECTION_STRING = 
            "Integrated Security=SSPI;Persist Security Info=False;" +
            "Initial Catalog=Messenger;Data Source=SADRA";

        private static Object Execute(SqlCommand command, QueryType queryType) {
            using(SqlConnection connection = new SqlConnection(CONNECTION_STRING)) {
                command.Connection = connection;
                connection.Open();
                Object res = null;
                switch (queryType) {
                    case QueryType.CREATE:
                        res = command.ExecuteNonQuery();
                        break;
                    case QueryType.READ_ALL:
                        res = new DataTable();
                        ((DataTable)res).Load(command.ExecuteReader());
                        break;
                    case QueryType.READ_SCALAR:
                        res = new DataTable();
                        ((DataTable)res).Load(command.ExecuteReader());
                        break;
                    case QueryType.UPDATE:
                        break;
                    case QueryType.DELETE:
                        break;
                }
                connection.Close();
                return res;
            }
        }

        public static ActionResult CreatePerson(Models.People.MPerson person) {
            SqlCommand command = new SqlCommand($"INSERT INTO People.Person (Username, Pass) values (@username, @pass)");
            command.Parameters.Add(new SqlParameter("@username", person.Username));
            command.Parameters.Add(new SqlParameter("@pass", person.Pass));
            if((int)Execute(command, QueryType.CREATE) > 0) {
                return ActionResult.SUCCESS;
            }
            return ActionResult.FAILURE;
            
        }

        public static ActionResult ReadPersonID( ref int result, string username = null) {
            if(username == null) {
                result = -1;
                return ActionResult.FAILURE;
            }
            try {
                SqlCommand cmnd = new SqlCommand($"Select ID from People.Person where username = @username");
                cmnd.Parameters.Add(new SqlParameter("@username", username));

                DataTable res = (DataTable)Execute(cmnd, QueryType.READ_ALL);
                if(res.Rows.Count == 1) {
                    result = (int)res.Rows[0]["ID"];
                    return ActionResult.SUCCESS;
                }
                return ActionResult.FAILURE;
            }catch (Exception e) {
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }
        }

        public static ActionResult ReadLogin(ref Models.People.MPerson person) {
            try {
                SqlCommand cmnd = new SqlCommand($"Select * from People.Person where " +
                    $"Person.Username = @username and Person.Pass = @pass");

                cmnd.Parameters.Add(new SqlParameter ("@username", person.Username));
                cmnd.Parameters.Add(new SqlParameter("@pass", person.Pass));

                DataTable res = ((DataTable)Execute(cmnd, QueryType.READ_ALL));
                if (res.Rows.Count == 1) {
                    person.ID = (int)res.Rows[0]["ID"];
                    person.CreatedAt = (DateTime)res.Rows[0]["CreatedAt"];
                    return ActionResult.SUCCESS;
                }

                return ActionResult.FAILURE;
            }
            catch (Exception e){
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }
        }

        public static ActionResult ReadContact (string username1, string username2, out MContacts contact) {
            try {
                SqlCommand cmnd = new SqlCommand($"Select * from Clustering.Contacts where " +
                    $"((User1 = (Select ID from People.Person where Username = @username1) AND " +
                    $"User2 = (Select ID from People.Person where Username = @username2)) " +
                    $"OR (User1 = (Select ID from People.Person where Username = @username2) AND " +
                    $"User2 = (Select ID from People.Person where Username = @username1)))");

                cmnd.Parameters.Add(new SqlParameter("@username1", username1));
                cmnd.Parameters.Add(new SqlParameter("@username2", username2));

                DataTable res = ((DataTable)Execute(cmnd, QueryType.READ_SCALAR));
                if (res.Rows.Count > 0) {
                    contact = new MContacts();
                    contact.ID = (int)res.Rows[0]["ID"];
                    contact.User1 = (int)res.Rows[0]["User1"];
                    contact.User2 = (int)res.Rows[0]["User2"];
                    contact.CreatedAt = (DateTime)res.Rows[0]["CreatedAt"];
                    return ActionResult.SUCCESS;
                }
                contact = null;
                return ActionResult.SUCCESS;
            }
            catch (Exception e) {
                contact = null;
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }
        }

        public static ActionResult ReadContacts(string username, out List<string> contacts) {
            try {
                SqlCommand cmnd = new SqlCommand($"Select username from People.Person as person " +
                    $"inner join((select User1 as userID from Clustering.Contacts where User2=(" +
                    $"Select ID from People.Person where username = @username))" +
                    $"Union(select User2 as userID from Clustering.Contacts where User1 = (" +
                    $"Select ID from People.Person where username = @username))) as tbl " +
                     $"on person.ID = tbl.userID");

                cmnd.Parameters.Add(new SqlParameter("@username", username));
                contacts = new List<string>();
                var rows = (DataTable)Execute(cmnd, QueryType.READ_ALL);
                foreach(DataRow row in rows.Rows) {
                    contacts.Add(row["username"].ToString());
                }
                return ActionResult.SUCCESS;
                
            }
            catch (Exception e) {
                //Program.WriteLog(e.Message);
                contacts = null;
                return ActionResult.EXCEPTION;
            }
        }

        public static ActionResult ReadContactMsg(int contactID, int messageCount, out string messages) {
            try {
                SqlCommand cmnd = new SqlCommand($"Select Username, Msg from (Select TOP {messageCount} Username, " +
                    $"Msg, ContactMsg.CreatedAt from Messaging.ContactMsg inner join People.Person on " +
                    $"Person.ID = ContactMsg.SenderID where ContactID = @contactID " +
                    $"order by ContactMsg.CreatedAt desc) as tbl order by tbl.CreatedAt");

                cmnd.Parameters.Add(new SqlParameter("@contactID", contactID));
                var rows = (DataTable)Execute(cmnd, QueryType.READ_ALL);//TODO query is right continue the rest need double string to return the sender and msg
                StringBuilder strB = new StringBuilder();
                messages = null;
                for (int i = 0; i < rows.Rows.Count; i++) {
                    strB.Append(rows.Rows[i]["Username"].ToString());
                    strB.Append(':');
                    strB.Append(rows.Rows[i]["Msg"].ToString());
                    if (i < rows.Rows.Count - 1) strB.Append('|');
                }
                messages = strB.ToString();
                return ActionResult.SUCCESS;

            }
            catch (Exception e) {
                messages = null;
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }

            //INSERT INTO Messaging.ContactMsg (ContactID, Msg, SenderID) values (1, 'manualTest', (select ID from People.Person where Username='sadra'))
        }

        public static ActionResult CreateContact(string username1, string username2) {
            try {
                SqlCommand cmnd = new SqlCommand($"INSERT INTO Clustering.Contacts (User1, User2) values ( " +
                    $"(Select ID from People.Person where ID = @username1)," +
                    $"(Select ID from People.Person where ID = @username2))");

                cmnd.Parameters.Add(new SqlParameter("@username1", username1));
                cmnd.Parameters.Add(new SqlParameter("@username2", username2));

                int rows = (int)Execute(cmnd, QueryType.CREATE);
                if (rows > 0) {
                    return ActionResult.SUCCESS;
                }
                return ActionResult.FAILURE;
            }
            catch (Exception e) {
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }
        }

        public static ActionResult CreateMessage(MContactMsg msg, string senderUsername) {
            try {
                SqlCommand cmnd = new SqlCommand($"INSERT INTO Messaging.ContactMsg (ContactID, Msg, SenderID)" +
                    $" values (@contactID, @msg, (select ID from People.Person where Username=@username))");

                cmnd.Parameters.Add(new SqlParameter("@contactID", msg.ContactID));
                cmnd.Parameters.Add(new SqlParameter("@msg", msg.Msg));
                cmnd.Parameters.Add(new SqlParameter("@username", senderUsername));

                int rows = (int)Execute(cmnd, QueryType.CREATE);
                if (rows > 0) {
                    return ActionResult.SUCCESS;
                }
                return ActionResult.FAILURE;
            }
            catch (Exception e) {
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }
        }

    }
}

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

        public static ActionResult PostPerson(Models.People.MPerson person) {
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

        public static ActionResult CreateMessage(MContactMsg msg) {
            try {
                SqlCommand cmnd = new SqlCommand($"INSERT INTO Messaging.ContactMsg (ContactID, Msg)" +
                    $" values (@contactID, @msg)");

                cmnd.Parameters.Add(new SqlParameter("@contactID", msg.ContactID));
                cmnd.Parameters.Add(new SqlParameter("@msg", msg.Msg));

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

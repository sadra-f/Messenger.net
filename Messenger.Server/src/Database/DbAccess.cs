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
                    person.UpdatedAt = (DateTime)res.Rows[0]["UpdatedAt"];
                    return ActionResult.SUCCESS;
                }

                return ActionResult.FAILURE;
            }
            catch (Exception e){
                //Program.WriteLog(e.Message);
                return ActionResult.EXCEPTION;
            }
        }

    }
}

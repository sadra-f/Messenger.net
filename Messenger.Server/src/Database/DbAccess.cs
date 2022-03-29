using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Database {
    public enum ActionResult{ 
        SUCCESS,
        FAIL,
    }

    enum QueryType {
        CREATE,
        READ,
        UPDATE,
        DELETE,

    }
    //perform CRUD to database using ado
    class DbAccess {
        private readonly static string CONNECTION_STRING = 
            "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;" +
            "Initial Catalog=Messenger;Data Source=SADRA";
        private SqlConnection sqlConnection;
        public DbAccess() {
            sqlConnection = new SqlConnection(CONNECTION_STRING);
            sqlConnection.Open();
        }

        private static Object Execute(string command, QueryType queryType) {
            switch (queryType) {
                case QueryType.CREATE:
                    break;
                case QueryType.READ:
                    break;
                case QueryType.UPDATE:
                    break;
                case QueryType.DELETE:
                    break;
            }
            return null;
        }

        public static ActionResult PostPerson(Models.People.MPerson person) {
            
            return ActionResult.FAIL;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Database.Models.Messaging {
    class MContactMsg {
        private long _ID;
        private int _ContactID;
        private string _Msg;
        private DateTime _CreatedAt;

        public MContactMsg() {
        }

        public MContactMsg(int contactID, string msg) {
            ContactID = contactID;
            Msg = msg;
        }

        public MContactMsg(long iD, int contactID, string msg, DateTime createdAt) {
            ID = iD;
            ContactID = contactID;
            Msg = msg;
            CreatedAt = createdAt;
        }

        public long ID { get => _ID; set => _ID = value; }
        public int ContactID { get => _ContactID; set => _ContactID = value; }
        public string Msg { get => _Msg; set => _Msg = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
    }
}

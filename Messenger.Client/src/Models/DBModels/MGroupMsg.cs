using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.DBModels.Messaging {
    class MGroupMsg {
        private long _ID;
        private int _GroupID;
        private int _SenderID;
        private string _Msg;
        private DateTime _CreatedAt;

        public MGroupMsg() {
        }

        public MGroupMsg(long iD, int groupID, int senderID, string msg, DateTime createdAt) {
            ID = iD;
            GroupID = groupID;
            SenderID = senderID;
            Msg = msg;
            CreatedAt = createdAt;
        }

        public long ID { get => _ID; set => _ID = value; }
        public int GroupID { get => _GroupID; set => _GroupID = value; }
        public int SenderID { get => _SenderID; set => _SenderID = value; }
        public string Msg { get => _Msg; set => _Msg = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
    }
}

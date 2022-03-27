using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Database.Models.Clustering {
    class MContacts {
        private int _ID;
        private int _User1;
        private int _User2;
        private DateTime _CreatedAt;

        public MContacts() {
        }

        public MContacts(int iD, int user1, int user2, DateTime createdAt) {
            ID = iD;
            User1 = user1;
            User2 = user2;
            CreatedAt = createdAt;
        }

        public int ID { get => _ID; set => _ID = value; }
        public int User1 { get => _User1; set => _User1 = value; }
        public int User2 { get => _User2; set => _User2 = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
    }
}

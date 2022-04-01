using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Database.Models.People {
    class MPerson {
        private int _ID;
        private string _Username;
        private string _Pass;
        private DateTime _CreatedAt;
        private DateTime _UpdatedAt;

        public MPerson(string username, string pass) {
            Username = username;
            Pass = pass;
        }

        public MPerson() {
        }

        public MPerson(int iD, string username, string pass, DateTime createdAt, DateTime updatedAt) {
            ID = iD;
            Username = username;
            Pass = pass;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int ID { get => _ID; set => _ID = value; }
        public string Username { get => _Username; set => _Username = value; }
        public string Pass { get => _Pass; set => _Pass = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
        public DateTime UpdatedAt { get => _UpdatedAt; set => _UpdatedAt = value; }
    }
}

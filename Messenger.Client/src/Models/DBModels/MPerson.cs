using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.DBModels.People {
    class MPerson {
        private int _ID;
        private string _Username;
        private string _Pass;
        private DateTime _CreatedAt;
        private DateTime _UpdatedAt;

        public MPerson(MPerson user) {
            ID = user.ID;
            Username = user.Username;
            Pass = user.Pass;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
        }

        public MPerson(int iD, string username, string pass) {
            ID = iD;
            Username = username;
            Pass = pass;
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

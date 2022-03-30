using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    class MUser {
        private string _Username;
        private string _Pass;

        public MUser(string username, string pass) {
            Username = username;
            Pass = pass;
        }

        public string Username { get => _Username; set => _Username = value; }
        public string Pass { get => _Pass; set => _Pass = value; }
    }
}

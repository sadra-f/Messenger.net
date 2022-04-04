using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    class MContactChat {
        private string _Sender;
        private string _Reciver;
        private string msg;
        private DateTime _CreatedAt;

        public MContactChat() {
        }

        public MContactChat(string sender, string reciver, string msg) {
            Sender = sender;
            Reciver = reciver;
            Msg = msg;
        }

        public MContactChat(string sender, string reciver, string msg, DateTime createdAt) {
            Sender = sender;
            Reciver = reciver;
            Msg = msg;
            CreatedAt = createdAt;
        }

        public string Sender { get => _Sender; set => _Sender = value; }
        public string Reciver { get => _Reciver; set => _Reciver = value; }
        public string Msg { get => msg; set => msg = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
    }
}

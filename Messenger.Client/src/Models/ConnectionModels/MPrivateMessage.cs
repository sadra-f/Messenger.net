using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    class MPrivateMessage {
        private string _to;
        private string _message;

        public MPrivateMessage() {
        }

        public MPrivateMessage(string to, string message) {
            To = to;
            Message = message;
        }

        public string To { get => _to; set => _to = value; }
        public string Message { get => _message; set => _message = value; }
    }
}

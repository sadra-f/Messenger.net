﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    class MContactChat {
        private string _Sender;
        private string msg;
        private DateTime _CreatedAt;

        public MContactChat() {
        }

        public MContactChat(string sender, string msg) {
            Sender = sender;
            Msg = msg;
        }

        public MContactChat(string sender, string msg, DateTime createdAt) : this(sender, msg) {
            CreatedAt = createdAt;
        }

        public string Sender { get => _Sender; set => _Sender = value; }
        public string Msg { get => msg; set => msg = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
    }
}

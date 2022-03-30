using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.DBModels.Clustering {
    class MGroups {
        private int _ID;
        private string _GName;
        private string _Intro;
        private DateTime _CreatedAt;
        private DateTime _UpdatedAt;

        public MGroups() {
        }

        public MGroups(int iD, string gName, string intro, DateTime createdAt, DateTime updatedAt) {
            ID = iD;
            GName = gName;
            Intro = intro;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int ID { get => _ID; set => _ID = value; }
        public string GName { get => _GName; set => _GName = value; }
        public string Intro { get => _Intro; set => _Intro = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
        public DateTime UpdatedAt { get => _UpdatedAt; set => _UpdatedAt = value; }
    }
}

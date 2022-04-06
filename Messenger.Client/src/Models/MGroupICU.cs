using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models {
    //model Group (for) internal(inbetween methods in project) communication use
    class MGroupICU {
        private string _GName;
        private string _Intro;
        private string _User;

        public MGroupICU(string gName, string intro, string user) {
            GName = gName;
            Intro = intro;
            User = user;
        }

        public string GName { get => _GName; set => _GName = value; }
        public string Intro { get => _Intro; set => _Intro = value; }
        public string User { get => _User; set => _User = value; }

        public override string ToString() {
            return $"{GName}|{Intro}|{User}";
        }

    }
}

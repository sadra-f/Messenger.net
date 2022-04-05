using Messenger.Client.src.Models.DBModels.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    
    class MLoginResponse : AResponse {
        public MPerson user;
        public MLoginResponse() {
        }

        public MLoginResponse(MPerson user, string result, EResultType resultType, 
            Dictionary<string, string> options = null) {
            this.user = user;
            this.result = result;
            this.resultType = resultType;
            this.options = options;
        }
    }
}

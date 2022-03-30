using Messenger.Client.src.Models.DBModels.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    class MSignupResponse : AResponse {
        
        public MSignupResponse() {
        }

        public MSignupResponse(string result, EResultType resultType, 
            Dictionary<string, string> options = null) {
            this.result = result;
            this.resultType = resultType;
            this.options = options;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.Models.ConnectionModels {
    class AResponse {
        public string result;
        public Dictionary<string, string> options;
        public EResultType resultType;

        public AResponse() {
        }

        public AResponse(string result, Dictionary<string, string> options, EResultType resultType) {
            this.result = result;
            this.options = options;
            this.resultType = resultType;
        }
    }
}

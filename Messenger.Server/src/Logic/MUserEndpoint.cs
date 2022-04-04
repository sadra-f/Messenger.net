using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.src.Logic {
    class MUserEndpoint {
        public IPEndPoint userIp;
        public int ListenerPort;

        public MUserEndpoint(IPEndPoint userIp) {
            this.userIp = userIp;
        }

        public MUserEndpoint(IPEndPoint userIp, int listenerPort) {
            this.userIp = userIp;
            ListenerPort = listenerPort;
        }
    }
}

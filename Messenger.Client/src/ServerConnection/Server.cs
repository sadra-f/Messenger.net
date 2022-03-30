using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.src.ServerConnection {
    static class Server {
        public static readonly IPAddress IP = IPAddress.Parse("192.168.1.108");
        public static readonly int PORT = 55000;
        public static async Task CreateAccount(string username, string pass) {
            string request = $"MAKE -OPTION<User:{username}> -OPTION<Pass:{pass}> -OPTION<ID:>";
            Socket socket = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try {
                socket.Connect(new IPEndPoint(IP, PORT));
                socket.Send(Encoding.ASCII.GetBytes(request));
                byte[] res = new byte[1024];
                ArraySegment<byte> res2 = new ArraySegment<byte>(res);
                await socket.ReceiveAsync(res2, SocketFlags.None);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Program.show(Encoding.ASCII.GetString(res2.Array));
            }
            catch (Exception e) {
                Program.show(e.Message);

                return;
            }
        }

    }
}

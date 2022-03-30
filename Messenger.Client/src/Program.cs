using Messenger.Client.src.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger.Client {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new frmChat());*/
        public static IPAddress IP = IPAddress.Parse("192.168.1.108");
        public static int PORT = 55000;
        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmCreateAccount());


            /*byte[] bytes = new byte[1024];

            //// Connect to a remote device.  
            //try {
            //    // Establish the remote endpoint for the socket.  
            //    // This example uses port 11000 on the local computer.  
            //    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //    //IPAddress ipAddress = ipHostInfo.AddressList[0];
            //    //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            //    IPEndPoint remoteEP = new IPEndPoint(IP, PORT);

            //    // Create a TCP/IP  socket.  
            //    Socket sender = new Socket(IP.AddressFamily,
            //        SocketType.Stream, ProtocolType.Tcp);

            //    // Connect the socket to the remote endpoint. Catch any errors.  
            //    try {
            //        sender.Connect(remoteEP);

            //        Console.WriteLine("Socket connected to {0}",
            //            sender.RemoteEndPoint.ToString());

            //        // Encode the data string into a byte array.  
            //        byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

            //        // Send the data through the socket.  
            //        int bytesSent = sender.Send(msg);

            //        // Receive the response from the remote device.  
            //        int bytesRec = sender.Receive(bytes);
            //        Console.WriteLine("Echoed test = {0}",
            //            Encoding.ASCII.GetString(bytes, 0, bytesRec));

            //        // Release the socket.  
            //        sender.Shutdown(SocketShutdown.Both);
            //        sender.Close();
            //        Console.Read();

            //    }
            //    catch (ArgumentNullException ane) {
            //        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            //    }
            //    catch (SocketException se) {
            //        Console.WriteLine("SocketException : {0}", se.ToString());
            //    }
            //    catch (Exception e) {
            //        Console.WriteLine("Unexpected exception : {0}", e.ToString());
            //    }

            //}
            //catch (Exception e) {
            //    Console.WriteLine(e.ToString());
            //}*/
        }

        public static void show(string value) {
            MessageBox.Show(value);
        }
    }
}

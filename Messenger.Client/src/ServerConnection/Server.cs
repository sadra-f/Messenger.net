﻿using Messenger.Client.src.Models.ConnectionModels;
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
        public static int BUFFER_SIZE = 1024;

        private static async Task<byte[]> MakeRequest(string req, bool waitForResp = true) {
            Socket socket = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IP, PORT));
            socket.Send(Encoding.UTF8.GetBytes(req));
            ArraySegment<byte> res2 = new ArraySegment<byte>();
            if (waitForResp) {
                byte[] res = new byte[BUFFER_SIZE];
                res2 = new ArraySegment<byte>(res);
                await socket.ReceiveAsync(res2, SocketFlags.None);
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return res2.Array;
        }

        public static async Task<string> Signup(MUser user) {
            string request = $"Make -Option <user:{user.Username}> -Option <pass:{user.Pass}> -Option <id:>";
            try {
                return Encoding.UTF8.GetString((await MakeRequest(request))?? new byte[BUFFER_SIZE]);
                //!!^
            }
            catch (Exception e) {
                Program.show(e.Message);
                return "";
            }
        }

        public static async Task<string> Login(MUser user) { 
            string request = $"Connect -Option <user:{user.Username}> -Option <pass:{user.Pass}> -Option<port:{Program.messagePort}>";
            try {
                return Encoding.UTF8.GetString((await MakeRequest(request)) ?? new byte[BUFFER_SIZE]);
            }
            catch (Exception e) {
                Program.show(e.Message);
                return "";
            }
        }

        public static async Task<string> ContactList() {
            string request = $"Contacts -Option<user:{Program.user.Username}>";
            try {
                return Encoding.UTF8.GetString((await MakeRequest(request)) ?? new byte[BUFFER_SIZE]);
            }
            catch (Exception e) {
                Program.show(e.Message);
                return "";
            }
        }

        public static async Task<string> ContactChats(string username) {
            string request = $"ChatList -Option<user1:{Program.user.Username}> -Option<user2:{username}>";
            try {
                return Encoding.UTF8.GetString((await MakeRequest(request)) ?? new byte[BUFFER_SIZE]);
            }
            catch (Exception e) {
                Program.show(e.Message);
                return "";
            }
        }

        public static async Task<string> PMessage(MPrivateMessage message) {
            message.Message = message.Message ?? "hi";
            string request = $"Pm -Option <len:{message.Message.Length}> -Option <from:{Program.user.Username}> " +
                $"-Option <to:{message.To}> -Option <body:{message.Message}>";
            try {
                
                return Encoding.UTF8.GetString((await MakeRequest(request, false)) ?? new byte[BUFFER_SIZE]);
            }
            catch (Exception e) {
                Program.show(e.Message);
                return "";
            }
        }

        public static async Task<string> CreateGroup(string name, string desc) {
            string request = $"CreateG -Option<user:{Program.user.Username}> -Option<name:{name}> -Option<desc:{desc}>";
            try {

                return Encoding.UTF8.GetString((await MakeRequest(request, true)) ?? new byte[BUFFER_SIZE]);
            }
            catch (Exception e) {
                Program.show(e.Message);
                return "";
            }
        }

    }
}

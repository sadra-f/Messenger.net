using Messenger.Client.src.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger.Client.src.Forms {
    public partial class frmGroupChat : Form {
        private MGroupICU _group;
        public frmGroupChat(string gName) {
            _group =  Program.groups[gName];
            InitializeComponent();
        }
        public void addMessage(string msg, string sender, string toGroup) {
            if (toGroup != _group.GName) return;
            this.lbChat.Items.Add($"[{sender}] : {msg}");
        }
        private async void frmGroupChat_Shown(object sender, EventArgs e) {
            this.lblGroupName.Text = _group.GName;
            this.Enabled = false;
            var res = await Program.GroupChatReq(_group.GName);
            if (res != null) {
                foreach (var obj in res) {
                    this.addMessage(obj.Msg, obj.Sender, _group.GName);
                }
            }
            this.Enabled = true;
        }

        private void btnInfo_Click(object sender, EventArgs e) {
            this.Hide();
            //Program.currentForm = new frmGroupInfo(_group.GName);
            //Program.currentForm.ShowDialog();
            new frmGroupInfo(_group.GName).ShowDialog();
            this.Show();
        }

        private async void btnSend_Click(object sender, EventArgs e) {
            if (tbMessage.Text == null || tbMessage.Text == "") {
                return;
            }
            string message = this.tbMessage.Text;
            this.tbMessage.Text = String.Empty;
            var res = await Program.GMReq(_group.GName, message);
            if (res.resultType == Models.ConnectionModels.EResultType.SUCCESS) addMessage(message, Program.user.Username, _group.GName);
            else System.Media.SystemSounds.Asterisk.Play();
        }
    }
}

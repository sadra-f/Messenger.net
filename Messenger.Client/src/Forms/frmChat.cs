using Messenger.Client.src.Models.DBModels.People;
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
    public partial class frmChat : Form {
        private MPerson _endUser;

        public MPerson EndUser { get => _endUser; set => _endUser = value; }

        public frmChat(MPerson endUser) {
            _endUser = endUser;
            InitializeComponent();

        }

        private void frmChat_Load(object sender, EventArgs e) {
            this.label1.Text = EndUser.Username;
        }

        public void addMessage(string message, bool ByMe = false) {
            this.lbChat.Items.Add($"[{(ByMe? Program.user.Username : EndUser.Username)}] : {message}");
        }

        private async void btnSend_Click(object sender, EventArgs e) {
            await sendMesage();
        }
        private async Task sendMesage() {
            if(tbMessage.Text == null || tbMessage.Text == "") {
                return;
            }
            string message = this.tbMessage.Text;
            await Program.PMReq(_endUser.Username, message);
            addMessage(message ?? "hi", true);
            this.tbMessage.Text = String.Empty;
        }
        private async void frmChat_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                await sendMesage();
            }
        }
    }
}

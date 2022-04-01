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
        public frmChat(MPerson endUser) {
            InitializeComponent();

        }

        private void frmChat_Load(object sender, EventArgs e) {

        }

        public void addMessage (string sender, string message) {
            this.lbChat.Items.Add($"[{sender}] : {message}");
        }

        private async void btnSend_Click(object sender, EventArgs e) {
            string message = this.tbMessage.Text;
            this.tbMessage.Text = String.Empty;
            await Program.PMReq(_endUser.Username, message);
        }
    }
}

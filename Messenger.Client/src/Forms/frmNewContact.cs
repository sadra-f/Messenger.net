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
    public partial class frmNewContact : Form {
        private frmHome home;
        internal bool didCreateAccount;
        internal string username;
        public frmNewContact(frmHome home) {
            this.home = home;
            this.Owner = home;
            didCreateAccount = false;
            username = null;
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e) {
            if (tbUsername.Text.Length < 1) {
                System.Media.SystemSounds.Asterisk.Play();
                tbUsername.Focus();
                return;
            }
            if(tbMessage.Text.Length < 1) {
                tbMessage.Text = "hi";
            }
            var res = await Program.PMReq(tbUsername.Text, tbMessage.Text);
            if(res.resultType == Models.ConnectionModels.EResultType.SUCCESS) {
                didCreateAccount = true;
                username = tbUsername.Text;
                this.Close();
            }
            else {
                Program.show("Failed to Send Message");
            }
            //TODO : if seccuss add new chat to frmHome else ...
        }
    }
}

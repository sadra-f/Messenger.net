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
        public frmNewContact(frmHome home) {
            this.home = home;
            this.Owner = home;
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
                this.Hide();
                Program.currentForm = new frmChat(new MPerson(-1,tbUsername.Text, null));
                Program.currentForm.ShowDialog();
                Program.currentForm = home;
            }
            else {
                Program.show("Failed to Send Message");
            }
            //TODO : if seccuss add new chat to frmHome else ...
            this.Close();
        }
    }
}

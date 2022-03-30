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
            await Program.NewContactReq(tbUsername.Text, tbMessage.Text);
            //TODO : if seccuss add new chat to frmHome else ...
            this.Close();
        }
    }
}

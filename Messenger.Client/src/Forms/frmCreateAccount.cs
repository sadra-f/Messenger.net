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
    public partial class frmCreateAccount : Form {
        frmLogin login;
        public frmCreateAccount(frmLogin login) {
            this.login = login;
            this.Owner = login;
            InitializeComponent();
        }

        private async void  btnCreateAccount_Click(object sender, EventArgs e) {
            await ServerConnection.Server.CreateAccount(tbUsername.Text, tbPassword.Text);
        }

        private void frmCreateAccount_Shown(object sender, EventArgs e) {
            this.login.Hide();
        }

        private void frmCreateAccount_FormClosing(object sender, FormClosingEventArgs e) {
            this.login.Show();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}

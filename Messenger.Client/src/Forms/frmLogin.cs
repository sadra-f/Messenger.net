using Messenger.Client.src.Models.ConnectionModels;
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
    public partial class frmLogin : Form {
        public frmLogin() {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e) {
            this.Hide();
            new frmSignup(this).ShowDialog();
            this.Show();
        }

        private void frmLogin_Load(object sender, EventArgs e) {
            this.CenterToScreen();
        }

        private async void btnLogin_Click(object sender, EventArgs e) {
            if(tbUsername.Text.Length < 1 || tbPassword.Text.Length < 1) {
                System.Media.SystemSounds.Asterisk.Play();
                if(tbUsername.Text.Length < 1) {
                    tbUsername.Focus();
                }
                else {
                    tbPassword.Focus();
                }
                return;
            }
            MLoginResponse response = await Program.LoginReq(this.tbUsername.Text, this.tbPassword.Text);
            if(response.resultType == EResultType.SUCCESS) {
                this.Close();
                new frmHome().Show();
            }
            else{
                MessageBox.Show(this, response.options["reason"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

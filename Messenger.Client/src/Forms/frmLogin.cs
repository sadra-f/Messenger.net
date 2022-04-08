using Messenger.Client.src.Models.ConnectionModels;
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
    public partial class frmLogin : Form {
        public frmLogin() {
            InitializeComponent();
            this.tbUsername.Text = "sadra";
            this.tbPassword.Text = "01234567";
            this.btnLogin.Focus();
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
                this.Hide();
                Program.currentForm = new frmHome();
                Program.currentForm.Closed += (s, args) => this.Close();
                Program.currentForm.Show();
            }
            else{
                MessageBox.Show(this, response.options["reason"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e) {
            if (!Program.isLoggedIn) {
                Program.killListener();
            }
        }
    }
}

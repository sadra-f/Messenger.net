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
    public partial class frmSignup : Form {
        frmLogin login;
        public frmSignup(frmLogin login) {
            this.login = login;
            this.Owner = login;
            InitializeComponent();
            this.CenterToParent();
        }
        public frmSignup() {
            InitializeComponent();
            this.CenterToParent();
        }

        private async void  btnCreateAccount_Click(object sender, EventArgs e) {
            if (tbUsername.Text.Length < 1 || tbPassword.Text.Length < 1) {
                System.Media.SystemSounds.Asterisk.Play();
                if (tbUsername.Text.Length < 1) {
                    tbUsername.Focus();
                }
                else {
                    tbPassword.Focus();
                }
                return;
            }
            var response = await Program.SignupReq(tbUsername.Text, tbPassword.Text);
            if(response.resultType == Models.ConnectionModels.EResultType.SUCCESS) {
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show(this, $"A new Account Has Been Created Under the username {tbUsername.Text}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else {
                MessageBox.Show(this, $"Failed To Create New Account under The Username : {tbUsername.Text}",
                    "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void frmCreateAccount_Shown(object sender, EventArgs e) {
            //this.login.Hide();//TODO UNComment
        }

        private void frmCreateAccount_FormClosing(object sender, FormClosingEventArgs e) {
            //this.login.Show();//TODO UNComment
        }

        private void btnBack_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}

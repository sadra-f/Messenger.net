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
            await Program.SignupReq(tbUsername.Text, tbPassword.Text);//TODOD : move this to program.c and take resopnse model from it
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

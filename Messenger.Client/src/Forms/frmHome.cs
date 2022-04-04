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
    public partial class frmHome : Form {
        public frmHome() {
            InitializeComponent();
            this.TopLevel = true;
            this.Parent = null;
        }

        private void btnNewChat_Click(object sender, EventArgs e) {
            this.Hide();
            new frmNewContact(this).ShowDialog();
            this.Show();
        }

        private void frmHome_Load(object sender, EventArgs e) {
            if (Program.isLoggedIn) {
                this.lblUsername.Text = Program.user.Username;
                Program.listen();
                return;
            }
            this.lblUsername.Text = "**NOT LOGGED IN**";
        }

        internal void NewMessage(string from, string msg) {
            //TODO : fix this s**t
            this.listBox1.Items.Add(from);

        }

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e) {
            Program.killListener();
        }
    }
}

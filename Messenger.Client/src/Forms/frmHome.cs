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
    public partial class frmHome : Form {
        public frmHome() {
            InitializeComponent();
            this.TopLevel = true;
            this.Parent = null;
        }

        private void btnNewChat_Click(object sender, EventArgs e) {
            this.Hide();
            var tmpFrm = new frmNewContact(this);
            Program.currentForm = tmpFrm;
            tmpFrm.ShowDialog();
            Program.currentForm = this;
            if (tmpFrm.didCreateAccount) {
                Program.currentForm = new frmChat(new MPerson(-1, tmpFrm.username, null));
                Program.currentForm.ShowDialog();
            }
            this.Show();
        }

        private async void frmHome_Load(object sender, EventArgs e) {
            if (Program.isLoggedIn) {
                this.lblUsername.Text = Program.user.Username;
                await Program.ContactsReq();
                foreach(var contact in Program.contacts) {
                    lbContacts.Items.Add(contact);
                }
                return;
            }
            this.lblUsername.Text = "**NOT LOGGED IN**";
        }


        internal void NewMessage(string from, string msg) {
            if (lbContacts.Items.Contains(from)) {
                lbContacts.Items[lbContacts.Items.IndexOf(from)] = lbContacts.Items[lbContacts.Items.IndexOf(from)].ToString() + '*';
            }
            else {
                Program.contacts.Add("from");
                this.lbContacts.Items.Add(from + '*');
            }

        }

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e) {
            Program.killListener();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) {
            if (lbContacts.SelectedItem == null) return;
            this.Hide();
            lbContacts.Items[lbContacts.SelectedIndex] = lbContacts.SelectedItem.ToString().Replace("*", string.Empty);
            Program.currentForm = new frmChat(new Models.DBModels.People.MPerson(-1, lbContacts.SelectedItem.ToString().Replace("*", string.Empty), ""));
            Program.currentForm.ShowDialog();
            Program.currentForm = this;
            this.Show();
        }

        private void btnNewGroup_Click(object sender, EventArgs e) {
            this.Hide();
            var tmpFrm = new frmCreateGroup();
            Program.currentForm = tmpFrm;
            tmpFrm.ShowDialog();
            Program.currentForm = this;
            if (tmpFrm.didCreate) {
                this.lbGroups.Items.Add(tmpFrm.name);
            }
            this.Show();
        }
    }
}

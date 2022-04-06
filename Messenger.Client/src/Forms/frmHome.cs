﻿using Messenger.Client.src.Models.DBModels.People;
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
                // !! if moving the add calls to another method make sure you delete the already existing ones !!
                await Program.GroupsReq();
                foreach (var group in Program.groups) {
                    lbGroups.Items.Add(group.GName);
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

        private void openContactChat() {
            if (lbContacts.SelectedItem == null) return;
            this.Hide();
            lbContacts.Items[lbContacts.SelectedIndex] = lbContacts.SelectedItem.ToString().Replace("*", string.Empty);
            Program.currentForm = new frmChat(new Models.DBModels.People.MPerson(-1, lbContacts.SelectedItem.ToString().Replace("*", string.Empty), ""));
            Program.currentForm.ShowDialog();
            Program.currentForm = this;
            this.Show();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) {
            openContactChat();
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

        private void btnChat_Click(object sender, EventArgs e) {
            openContactChat();
        }
    }
}

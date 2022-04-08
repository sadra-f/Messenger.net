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
    public partial class frmGroupInfo : Form {
        private string groupName;
        public frmGroupInfo(string gName) {
            groupName = gName;
            InitializeComponent();
            this.tbGName.Text = Program.groups[groupName].GName;
            this.tbDesc.Text = Program.groups[groupName].Intro;
            this.tbCreatorUsername.Text = Program.groups[groupName].User;
        }

        private async void frmGroupInfo_Load(object sender, EventArgs e) {
            await Program.GroupUsersReq(groupName);
            foreach (string str in Program.groups[groupName].Users) {
                this.lbMemebers.Items.Add(str);
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e) {
            this.Enabled = false;
            new frmAddGroupMember(groupName, this).ShowDialog();
            this.Enabled = true;
        }

        internal void AddNewUserToList(string username) {
            lbMemebers.Items.Add(username);
        }
    }
}

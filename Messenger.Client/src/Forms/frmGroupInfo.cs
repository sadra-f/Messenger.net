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
    public partial class frmGroupInfo : Form {
        private string groupName;
        frmGroupChat frmParent;
        public frmGroupInfo(string gName, frmGroupChat frmPrnt) {
            groupName = gName;
            this.frmParent = frmPrnt;
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

        private async void btnLeaveGroup_Click(object sender, EventArgs e) {
            AResponse res = await Program.LeaveGroupReq(groupName);
            if(res.resultType == EResultType.SUCCESS) {
                MessageBox.Show(this, "Left this Group seccessfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.groups.Remove(groupName);
                frmParent.doClose = true;
                this.Close();
            }else {
                MessageBox.Show(this, "Failed To Leave this Group seccessfully", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmParent.doClose = false;
            }
        }
    }
}

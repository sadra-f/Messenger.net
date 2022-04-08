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
    public partial class frmAddGroupMember : Form {
        string gname;
        frmGroupInfo gInfo;
        public frmAddGroupMember(string gname, frmGroupInfo gInfo) {
            this.gname = gname;
            this.gInfo = gInfo;
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e) {
            if (tbUsername.Text.Length < 3) {
                tbUsername.Focus();
                System.Media.SystemSounds.Asterisk.Play();
                return;
            }
            this.Enabled = false;
            AResponse res = await Program.AddGroupMemberReq(this.gname, tbUsername.Text.Trim());
            if (res.resultType == EResultType.SUCCESS) {
                MessageBox.Show(this, $"sucesscully added User {tbUsername.Text.Trim()}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gInfo.AddNewUserToList(tbUsername.Text.Trim());
                Program.groups[gname].Users.Add(tbUsername.Text.Trim());
            }
            else {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show(this, res.options["reason"], "Failed To Add User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Enabled = true;
            this.Close();
        }
    }
}

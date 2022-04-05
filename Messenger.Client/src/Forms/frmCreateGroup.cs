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
    public partial class frmCreateGroup : Form {
        internal bool didCreate;
        internal string name;
        public frmCreateGroup() {
            didCreate = false;
            name = null;
            InitializeComponent();
        }

        private async void btnCreate_Click(object sender, EventArgs e) {
            if(tbGroupName.Text == null || tbGroupName.Text == "") {
                System.Media.SystemSounds.Asterisk.Play();
                tbGroupName.Focus();
                return;
            }
            this.Enabled = false;
            var res = await Program.CreateGroupReq(tbGroupName.Text, tbGroupDesc.Text);//TODO : continue
            if(res.resultType == Models.ConnectionModels.EResultType.SUCCESS) {
                didCreate = true;
                name = tbGroupName.Text;
                System.Media.SystemSounds.Beep.Play();
                this.Close();
            }
            else {
                this.Enabled = true;
                System.Media.SystemSounds.Asterisk.Play();
                MessageBox.Show(this, res.options["reason"], "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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
        public frmCreateGroup() {
            InitializeComponent();
        }

        private async void btnCreate_Click(object sender, EventArgs e) {
            if(tbGroupName.Text == null || tbGroupName.Text == "") {
                System.Media.SystemSounds.Asterisk.Play();
                tbGroupName.Focus();
                return;
            }
            await Program.CreateGroupReq(tbGroupName.Text, tbGroupDesc.Text);
        }
    }
}

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
        }

        private void btnNewChat_Click(object sender, EventArgs e) {
            this.Hide();
            new frmNewContact(this).ShowDialog();
            this.Show();
        }
    }
}

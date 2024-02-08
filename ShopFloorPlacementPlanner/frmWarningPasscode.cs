using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmWarningPasscode : Form
    {
        public frmWarningPasscode()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text == "paper")
            {
                frmStaffWarnings frm = new frmStaffWarnings();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("INCORRECT PASSWORD","401",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtPassword.Text = "";
                return;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnConfirm.PerformClick();
        }
    }
}

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
    public partial class frmShiftHours : Form
    {
        public double _shiftHours { get; set; }
        public frmShiftHours()
        {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtManual.Text))
            {
                MessageBox.Show("Please enter the number of hours for this shift");
                return;
            }
            _shiftHours = Convert.ToDouble(txtManual.Text);
            this.Close();
        }

        private void txtManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            //this one is broke for whatever reason
        }

        private void txtManual_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}

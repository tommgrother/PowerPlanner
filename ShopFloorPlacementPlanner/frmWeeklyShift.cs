using System;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmWeeklyShift : Form
    {
        private DateTime _tempDate;
        private int _staffID;
        private string _dept;

        public frmWeeklyShift(DateTime tempDate, int staffID, string dept)
        {
            InitializeComponent();
            _tempDate = tempDate;
            _staffID = staffID;
            _dept = dept;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtManual.Text == "")
            {
                txtManual.Text = "0";
            }

            shiftHours shiftHours = new shiftHours(Convert.ToDouble(txtManual.Text), _tempDate, _staffID, _dept);
            if (shiftHours.validation == 0)
            {
                MessageBox.Show("User cannot be assigned for more than " + shiftHours._maxHours.ToString()); //+ " hours, they are already assigned for " + shiftHours._alreadyAssignedHours.ToString() + " hours");
                return;
            }
            else if (shiftHours.validation == -1)
            {
                //its fine so we can close this
                this.Close();
            }
        }

        private void txtManual_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            shiftHours.validation = 0;
            this.Close();
        }
    }
}
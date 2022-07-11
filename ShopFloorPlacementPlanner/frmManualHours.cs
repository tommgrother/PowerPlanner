using System;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmManualHours : Form
    {
        public double _manualHours { get; set; }

        public frmManualHours()
        {
            InitializeComponent();
        }

        private void frmManualHours_Load(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _manualHours = Convert.ToDouble(txtManual.Text);
            this.Close();
        }

        private void txtManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
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
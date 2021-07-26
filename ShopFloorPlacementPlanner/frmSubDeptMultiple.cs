using System;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmSubDeptMultiple : Form
    {
        public string location { get; set; }

        public frmSubDeptMultiple()
        {
            InitializeComponent();
            //add items to the combobox
            cmbSubDept.Items.Add("Up");
            cmbSubDept.Items.Add("Wash/Wipe");
            cmbSubDept.Items.Add("Etch");
            cmbSubDept.Items.Add("Sand");
            cmbSubDept.Items.Add("Powder Prime");
            cmbSubDept.Items.Add("Powder Coat");
            cmbSubDept.Items.Add("Oven");
            cmbSubDept.Items.Add("Wet Prep");
            cmbSubDept.Items.Add("Wet Paint");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //force him to pick one
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSubDept.Text.Length > 0)
            {
                location = cmbSubDept.Text;
                this.Close();
            }
            else
                MessageBox.Show("Please select a department!", "ERROR", MessageBoxButtons.OK);
        }
    }
}
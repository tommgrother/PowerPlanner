using System;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmSubDept : Form
    {
        public int placement_ID { get; set; }
        public bool alreadyPlaced { get; set; }

        public frmSubDept(int _placement_ID)
        {
            InitializeComponent();
            placement_ID = _placement_ID;

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
            cmbSubDept.Items.Add("Oven 2");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSubDept.Text.Length > 0)
            {
                //first we gotta check if they are already in the table
                SubDeptClass check = new SubDeptClass();
                check.checkPlacement(placement_ID);
                //check.add_placement(placement_ID, cmbSubDept.Text); //this seems to work fine
                this.Close();
            }
            else
                MessageBox.Show("Please select a department!", "ERROR", MessageBoxButtons.OK);
        }
    }
}
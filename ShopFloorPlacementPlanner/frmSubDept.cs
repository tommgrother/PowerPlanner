using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            //first we gotta check if they are already in the table
            SubDeptClass check = new SubDeptClass();
            check.checkPlacement(placement_ID);
            check.add_placement(placement_ID, cmbSubDept.Text); //this seems to work fine
            this.Close();
        }


    }
}

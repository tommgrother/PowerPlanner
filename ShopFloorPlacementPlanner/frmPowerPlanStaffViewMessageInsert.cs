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
    public partial class frmPowerPlanStaffViewMessageInsert : Form
    {
        public string kevinNote { get; set; }

        public frmPowerPlanStaffViewMessageInsert()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            kevinNote = "Cancel Button";

            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            txtKevinNote.Text = txtKevinNote.Text.Replace("'", "");

            kevinNote = txtKevinNote.Text;

            this.Close();
        }
    }
}

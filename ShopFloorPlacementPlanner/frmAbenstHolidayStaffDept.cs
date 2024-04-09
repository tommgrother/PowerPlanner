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
    public partial class frmAbenstHolidayStaffDept : Form
    {
        public frmAbenstHolidayStaffDept()
        {
            InitializeComponent();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            frmAbsentHolidaySearch frm = new frmAbsentHolidaySearch();
            frm.ShowDialog();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            frmDepartmentLateAbsent frm = new frmDepartmentLateAbsent();
            frm.ShowDialog();
        }
    }
}

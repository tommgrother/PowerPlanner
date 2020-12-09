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
    public partial class frmShiftHours : Form
    {
        public double _shiftHours { get; set; }
        public int _staffID { get; set; }
        public int _date { get; set; }
        public string _dept { get; set; }


        public frmShiftHours(int staffID,DateTime tempDate,string dept)
        {
            InitializeComponent();
            _staffID = staffID;
            _dept = dept;
            Placement placement = new Placement(tempDate, 0, "", "", 0);
        _date = placement._dateID;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            double getData;
            getData = 0;

            if (String.IsNullOrWhiteSpace(txtManual.Text))
            {
                MessageBox.Show("Please enter the number of hours for this shift");
                return;
            }
            //check if they go over 6.4 hours
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                string sql = "select sum(hours) as temp from dbo.power_plan_staff WHERE staff_id = " + _staffID.ToString() + " AND date_id = " + _date.ToString() + " AND department <> '" + _dept + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    try
                    {
                             getData = Convert.ToDouble(cmd.ExecuteScalar());
                    }
                    catch
                    {
                             getData = 0;
                    }

                
                    if (getData  + Convert.ToDouble(txtManual.Text) > 6.4)
                    {
                        MessageBox.Show("Staff is already in for " + getData.ToString() + " hours, the total cannot exceed 6.4");
                        return;
                    }
                }
                conn.Close();
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

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
    public partial class frmManualExtraHours : Form
    {
        public double _hours { get; set; }
        public string _department { get; set; }
        public string _staff_name { get; set; }
        public DateTime _selected_date { get; set; }
        public frmManualExtraHours(double max_hours, int staff_id, DateTime selected_date)
        {
            InitializeComponent();
            _selected_date = selected_date;
            txtManual.Text = max_hours.ToString();

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                string sql = "SELECT forename + ' ' + surname FROM [user_info].dbo.[user] WHERE id = " + staff_id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    _staff_name = cmd.ExecuteScalar().ToString();
                    conn.Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtManual.Text))
            {
                MessageBox.Show("Please enter a valid amount of hours before saving.", "Invalid Hours", MessageBoxButtons.OK);
                return;
            }
            if (cmbDept.Text == "")
            {
                MessageBox.Show("Please select a valid department before saving.", "Invalid Hours", MessageBoxButtons.OK);
                return;
            }

            try
            {
                _hours = Convert.ToDouble(txtManual.Text);
                _department = cmbDept.Text;
                //
                department_changed d = new department_changed();
                if (cmbDept.Text == "Punching")
                    d.setDepartment("Punching");
                else if (cmbDept.Text == "Laser")
                    d.setDepartment("Laser");
                else if (cmbDept.Text == "Stores")
                    d.setDepartment("Stores");
                else if (cmbDept.Text == "Bending")
                    d.setDepartment("Bending");
                else if (cmbDept.Text == "Dressing")
                    d.setDepartment("Dressing");
                else if (cmbDept.Text == "Welding")
                    d.setDepartment("Welding");
                else if (cmbDept.Text == "Painting")
                    d.setDepartment("Painting");
                else if (cmbDept.Text == "Packing")
                    d.setDepartment("Packing");
                else if (cmbDept.Text == "Dispatch")
                    d.setDepartment("Dispatch");


                //
                this.Close();
            }
            catch { }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if this user has a placement for this dept

            string sql = "SELECT [Staff Name] FROM dbo.view_planner_punch_staff WHERE date_plan = '" + _selected_date.ToString("yyyyMMdd") + "' AND department = '" + cmbDept.Text + "' AND  [Staff Name] = '" + _staff_name + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var getdata = (string)cmd.ExecuteScalar();
                    if (getdata != null)
                    {
                        MessageBox.Show(_staff_name + " has already been placed in " + cmbDept.Text + ". Please select another department.");
                        cmbDept.SelectedIndex = 0;
                    }
                }
                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _department = "Cancel";
            this.Close();
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmProductivityPlacement : Form
    {

        public string staff_name { get; set; }
        public int staff_id { get; set; }
        public DateTime placement_date { get; set; }
        public int placement_id { get; set; }

        public string department { get; set; }

        public frmProductivityPlacement(string _staff_name, DateTime _placement_date, string _department)
        {
            InitializeComponent();

            staff_name = _staff_name;
            placement_date = _placement_date;
            department = _department;

            lblStaff.Text = _staff_name + " - " + placement_date.ToString("dd/MM/yyyy");

            get_hours();

        }


        private void get_hours()
        {

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT id FROM [user_info].dbo.[user] where forename + ' ' + surname = '" + staff_name + "'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());

                using (SqlCommand cmd = new SqlCommand("SELECT id from dbo.power_plan_date where date_plan = '" + placement_date.ToString("yyyyMMdd") + "' ", conn))
                    placement_id = (Int32)cmd.ExecuteScalar();


                //get the hours of this user
                sql = "select [hours] FROM dbo.power_plan_staff where staff_id = " + staff_id.ToString() + " AND date_id = " + placement_id.ToString() + " AND " +
                    "department = '" + department + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    txtHours.Text = cmd.ExecuteScalar().ToString();

                conn.Close();
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtHours.Text.Length < 1)
                return;

            login.productivity_hours = Convert.ToDouble(txtHours.Text);

            string sql = "update dbo.power_plan_staff set hours = " + txtHours.Text + " " +
                "where staff_id = " + staff_id + " AND date_id = " + placement_id + " AND department = '" + department + "' ";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }


                //update daily goals

                using (SqlCommand cmd = new SqlCommand("usp_daily_goals", conn))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = placement_date;

                    cmd.ExecuteNonQuery();
                }




                conn.Close();
            }

            this.Close();
        }

        private void txtHours_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtHours_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnUpdate.PerformClick();
        }
    }
}

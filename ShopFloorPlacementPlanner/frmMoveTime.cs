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
    public partial class frmMoveTime : Form
    {
        public double time_for_part { get; set; }
        public int door_id { get; set; }
        public string dept { get; set; }
        public int staff_id { get; set; }
        public string part_name { get; set; }
        public DateTime date { get; set; }
        public frmMoveTime(int door_id, string dept, double time_for_part, int staff_id, string part_name,string date)
        {
            InitializeComponent();

            lblDoor.Text = "Door ID: " + door_id;
            lblTime.Text = "Current Time: " + time_for_part + " (minutes)";

            this.time_for_part = time_for_part;
            this.door_id = door_id;
            this.dept = dept;
            this.staff_id = staff_id;
            this.part_name = part_name;
            this.date = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", null);
        }

        private void txtTimeToMove_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            //validation
            if (txtTimeToMove.Text.Length == 0)
            {
                return;
            }
            double time_to_move = Convert.ToDouble(txtTimeToMove.Text);
            if (time_to_move > time_for_part - 1)
            {
                MessageBox.Show("The time to move cannot be more than " + (time_for_part - 1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (time_to_move == 0)
            {
                MessageBox.Show("The time to move must be more than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //work out the difference 
            double difference = time_for_part - time_to_move;

            //update the current record
            string sql = "UPDATE dbo.door_part_completion_log SET time_for_part = " + difference + " where door_id = " + door_id + " AND op = '" + dept + "' AND part = '" + part_name + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();

                sql = "INSERT INTO dbo.door_part_completion_log (door_id,part_complete_date,time_for_part,part_status,op,staff_id,part) " +
                    "VALUES (" + door_id + ",dateadd(HOUR,17,dbo.func_work_days(CAST('" + date.ToString("yyyyMMdd") + "' as date),0))," + time_to_move + ",'Complete','" + dept + "'," + staff_id + ",'" + part_name + " - MOVED')";
                //insert new record
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();

                conn.Close();
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

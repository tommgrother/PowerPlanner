using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmPowerPlanStaffViewMessage : Form
    {
        public string note_time { get; set; }
        public DateTime log_date { get; set; }
        public string department { get; set; }
        public int staff_id { get; set; }

        public frmPowerPlanStaffViewMessage(string time, DateTime log_date, int staff_id, string department, string label)
        {
            InitializeComponent();

            note_time = time;
            this.log_date = log_date;
            this.staff_id = staff_id;
            this.department = department;
            lblDept.Text = department;

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT forename + ' ' + surname FROM [user_info].dbo.[user] WHERE id = " + staff_id, conn))
                    lblTitle.Text = label + " Note for " + cmd.ExecuteScalar().ToString();

                //load the note 
                string sql = "Select " + note_time + " FROM  dbo.power_plan_staff_percent_log WHERE log_date = '" + log_date.ToString("yyyyMMdd") + "' AND " +
                    "staff_id = " + staff_id + " AND department = '" + department + "'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var fuga = cmd.ExecuteScalar();
                    if (fuga != null)
                        txtNote.Text = fuga.ToString();

                    txtNote.ReadOnly = true;
                }

                

                conn.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            frmPowerPlanStaffViewMessageInsert frm = new frmPowerPlanStaffViewMessageInsert();
            frm.ShowDialog();

            string fuga = frm.kevinNote;

            if (fuga == "Cancel Button")
            {
                this.Close();
            }
            else
            {

                //insert the note 
                //save the note
                string sql = "UPDATE dbo.power_plan_staff_percent_log SET " + note_time + " = '" + txtNote.Text + Environment.NewLine + fuga +  "' " +
                    "WHERE log_date = '" + log_date.ToString("yyyyMMdd") + "' AND staff_id = " + staff_id + " AND department = '" + department + "'";

                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
                this.Close();

            }


        }
    }
}

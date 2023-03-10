using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmProductivityEmail : Form
    {
        public DateTime _action_time { get; set; }
        public DateTime _action_time_end { get; set; }
        public string _staff { get; set; }
        public frmProductivityEmail(string staff)
        {
            InitializeComponent();
            _staff = staff;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length < 1)
            {
                MessageBox.Show("Please enter a message before sending this email.", "No Text", MessageBoxButtons.OK);
                return;
            }
            string email = "";
            string message = txtMessage.Text;
            message = message.Replace("'", "");
            if (chkAll.Checked == true)
                email = "Damian@designandsupply.co.uk;Simon@designandsupply.co.uk;Richard@designandsupply.co.uk";
            else
            {
                if (chkSimon.Checked == true)
                    email = email + "Simon@designandsupply.co.uk;";
                if (chkRichard.Checked == true)
                    email = email + "Richard@designandsupply.co.uk;";
                if (chkDamian.Checked == true)
                    email = email + "Damian@designandsupply.co.uk;";
            }

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("usp_power_planner_productivity_email", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@action_time", SqlDbType.Date).Value = _action_time;
            //cmd.Parameters.AddWithValue("@action_time_end", SqlDbType.Date).Value = _action_time_end;
            cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.AddWithValue("@staff", SqlDbType.VarChar).Value = _staff;
            cmd.Parameters.AddWithValue("@message", SqlDbType.NVarChar).Value = message;
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Email Sent.");
            this.Close();

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                chkSimon.Checked = false;
                chkRichard.Checked = false;
                chkDamian.Checked = false;
            }
        }

        private void chkSimon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSimon.Checked == true)
                chkAll.Checked = false;
            if (chkSimon.Checked == false && chkDamian.Checked == false && chkRichard.Checked == false)
                chkAll.Checked = true;
        }

        private void chkRichard_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRichard.Checked == true)
                chkAll.Checked = false;
            if (chkSimon.Checked == false && chkDamian.Checked == false && chkRichard.Checked == false)
                chkAll.Checked = true;
        }

        private void chkDamian_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamian.Checked == true)
                chkAll.Checked = false;
            if (chkSimon.Checked == false && chkDamian.Checked == false && chkRichard.Checked == false)
                chkAll.Checked = true;
        }
    }
}

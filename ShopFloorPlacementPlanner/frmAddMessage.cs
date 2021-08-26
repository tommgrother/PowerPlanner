using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmAddMessage : Form
    {
        public frmAddMessage()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string temp = txtNote.Text;
            temp = temp.Replace("'", "");

            string sql = "";
            if (chkAll.Checked == true)
                sql ="INSERT INTO dbo.kevinMessage ([message],message_date) VALUES ('" + temp + "',cast(getdate() as date))";
            else
            {
                string sqlStart = "INSERT INTO dbo.kevinMessage ([message],message_date";
                string sqlEnd =  " VALUES ('" + temp + "',cast(getdate() as date)";
                if (chkSimon.Checked == false)
                {
                    sqlStart = sqlStart + ",simon";
                    sqlEnd = sqlEnd + ",-2";
                }
                if (chkRichard.Checked == false)
                {
                    sqlStart = sqlStart + ",richard";
                    sqlEnd = sqlEnd + ",-2";
                }
                if (chkDamian.Checked == false)
                {
                    sqlStart = sqlStart + ",damian";
                    sqlEnd = sqlEnd + ",-2";
                }

                sql = sqlStart + ")" + sqlEnd + ")";
            }

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    email();
                    MessageBox.Show("Note added!", " ", MessageBoxButtons.OK);

                    this.Close();
                }
            }
        }
        private void email()
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_kevin_message_email", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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

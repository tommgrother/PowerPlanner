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
using System.Threading;
using System.Activities.Statements;

namespace ShopFloorPlacementPlanner
{
    public partial class frmDepartmentManagement : Form
    {
        public frmDepartmentManagement()
        {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser))
            {
                string sql = "SELECT forename + ' ' + surname as fullname FROM dbo.[user] WHERE ([current] = 1) AND (ShopFloor = -1) and (forename NOT LIKE '%weld%' AND forename NOT LIKE '%Allocation%') ORDER BY ppSlimline,dbo.[user].forename";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                        cmbStaff.Items.Add(dt.Rows[i][0].ToString());
                    conn.Close();
                }
            }

        }
        private void fillCmb()
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser))
            {
                conn.Open();
                string sql = "SELECT ID FROM [user_info].dbo.[user] WHERE forename +  ' ' + surname = '" + cmbStaff.Text + "'";
                int tempID = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    tempID = Convert.ToInt32(cmd.ExecuteScalar());
                sql = "SELECT dbo.[user].actual_department as [First Department],dbo.[user].allocation_dept_2 as [Second Department], dbo.[user].allocation_dept_3 as [Third Department], dbo.[user].allocation_dept_4 as [Fourth Department], dbo.[user].allocation_dept_5 as [Fifth Department], " +
                    "dbo.[user].slimline_staff as [Slimline],[default_in_department] FROM dbo.[user]   WHERE  id = " + tempID.ToString();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbFirst.Text = dt.Rows[0][0].ToString();
                    cmbSecond.Text = dt.Rows[0][1].ToString();
                    cmbThird.Text = dt.Rows[0][2].ToString();
                    cmbFourth.Text = dt.Rows[0][3].ToString();
                    cmbFifth.Text = dt.Rows[0][4].ToString();
                    if (dt.Rows[0][5].ToString() == "0" || String.IsNullOrEmpty(dt.Rows[0][5].ToString()))
                        chkSlimline.Checked = false;
                    else
                        chkSlimline.Checked = true;

                    cmbDefault.Text = dt.Rows[0][6].ToString();

                    cmbFirst.Enabled = true;
                    cmbSecond.Enabled = true;
                    cmbThird.Enabled = true;
                    cmbFourth.Enabled = true;
                    cmbFifth.Enabled = true;
                    chkSlimline.Enabled = true;
                    cmbDefault.Enabled = true;

                }
                    conn.Close();
            }
        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCmb();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser))
            {
                conn.Open();
                string sql = "SELECT ID FROM [user_info].dbo.[user] WHERE forename +  ' ' + surname = '" + cmbStaff.Text + "'";
                int tempID = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    tempID = Convert.ToInt32(cmd.ExecuteScalar());

                sql = "UPDATE dbo.[user] SET ";


                if (cmbFirst.Text == "")
                    sql = sql + "actual_department = NULL";
                else
                    sql = sql + "actual_department = '" + cmbFirst.Text + "'";

                if (cmbSecond.Text == "")
                    sql = sql + ", allocation_dept_2 = NULL";
                else
                    sql = sql + ", allocation_dept_2 = '" + cmbSecond.Text + "'";

                if (cmbThird.Text == "")
                    sql = sql + ", allocation_dept_3 = NULL";
                else
                    sql = sql + ", allocation_dept_3 = '" + cmbThird.Text + "'";

                if (cmbFourth.Text == "")
                    sql = sql + ", allocation_dept_4 = NULL";
                else
                    sql = sql + ", allocation_dept_4 = '" + cmbFourth.Text + "'";

                if (cmbFifth.Text == "")
                    sql = sql + ", allocation_dept_5 = NULL";
                else
                    sql = sql + ", allocation_dept_5 = '" + cmbFifth.Text + "'";

                if (chkSlimline.Checked == true)
                    sql = sql + ", slimline_staff = -1";
                else
                    sql = sql + ", slimline_staff = 0";

                if (cmbDefault.Text == "")
                    sql = sql + ", default_in_department = NULL";
                else
                    sql = sql + ", default_in_department = '" + cmbDefault.Text + "'";




                sql = sql + " WHERE id = " + tempID;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to update " + cmbStaff.Text + "'s placed departments?", "Department Management", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Departments updated!", "Department Management", MessageBoxButtons.OK);
                    }
                }
                conn.Close();
            }
        }
    }
}

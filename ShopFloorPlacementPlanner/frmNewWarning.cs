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
    public partial class frmNewWarning : Form
    {
        public frmNewWarning()
        {
            InitializeComponent();

            //get the data for the combobox
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT forename + ' ' + surname FROM [user_info].dbo.[user] " +
                    "where [current] = 1  AND ShopFloor = -1 and (non_user = 0 or non_user is null) AND id <> 9 and id <> 213 order by forename + ' ' + surname";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        cmbStaff.Items.Add(dr[0].ToString());
                    }
                }

                sql = "SELECT forename + ' ' + surname FROM [user_info].dbo.[user] " +
                      "where [current] = 1  AND actual_department = 'Management'  and shopfloor = -1 order by forename + ' ' + surname";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        cmbWarningGivenBy.Items.Add(dr[0].ToString());
                    }
                }


                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validation checks

            txtWarning.Text = txtWarning.Text.Replace("'", "");

            if (cmbStaff.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a staff member before saving this warning.", "Missing Staff!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbWarningGivenBy.SelectedIndex < 0)
            {
                MessageBox.Show("Please select the staff member who gave this warning.", "Missing Staff!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbDepartment.SelectedIndex < 0)
            {
                MessageBox.Show("Please select the department that the staff member was in.", "Missing Staff!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtWarning.Text.Length < 5)
            {
                MessageBox.Show("Please enter a description before clicking save.", "Missing Description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                int staff_id = 260;
                string sql = "select id FROM [user_info].dbo.[user] where forename + ' ' + surname = '" + cmbStaff.Text + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }

                //get the warning number
                int warning_number = 0;
                sql = "select max(warning_number) FROM dbo.staff_warnings WHERE staff_id = " + staff_id.ToString(); ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var temp = cmd.ExecuteScalar();
                    if (temp.GetType() == typeof(int))
                        warning_number = Convert.ToInt32(temp);

                }

                warning_number++; //add one tho the warning number

                sql = "INSERT INTO dbo.staff_warnings (staff_id,warning_number,warning_note,warning_date,warning_department,warning_given_by,date_submitted)" +
                             " VALUES (" + staff_id.ToString() + "," + warning_number.ToString() + ",'" + txtWarning.Text + "','" + dteWarningDate.Value.ToString("yyyyMMdd") + "'," +
                             "'" + cmbDepartment.Text + "','" + cmbWarningGivenBy.Text + "',GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Warning added successfully!", "Warning", MessageBoxButtons.OK);
                }
                conn.Close();
                this.Close();
            }
        }
    }
}

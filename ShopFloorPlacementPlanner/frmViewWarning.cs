using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmViewWarning : Form
    {
        public int staff_id { get; set; }
        public int warning_number { get; set; }
        public int remove_tabs { get; set; }
        public frmViewWarning(int _staff_id, int _warning_number)
        {
            InitializeComponent();
            staff_id = _staff_id;
            warning_number = _warning_number;
            remove_tabs = -1;
            load_cmb();
            load_data();
            dteStart.Value = new DateTime(2023, 01, 01);
            dteEnd.Value = DateTime.Now;
            load_grids();


        }

        private void load_data()
        {
            string sql = "";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                //get this users max warning number
                sql = "select max(warning_number) as max_warning FROM dbo.staff_warnings where staff_id = " + staff_id;
                int max_warning = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    max_warning = (int)cmd.ExecuteScalar();
                }

                if (remove_tabs == -1)
                {
                    //tabControl.TabPages.Remove()
                    tabControl.TabPages.Clear();


                    //loop max warning and insert a tab page for each one
                    for (int i = 0; i < max_warning; i++)
                    {
                        TabPage tabPage = new TabPage
                        {
                            Name = "Warning " + i.ToString(),
                            Text = "Warning " + (i + 1).ToString()
                        };
                        tabControl.TabPages.Add(tabPage);
                    }
                    remove_tabs = 0;
                }
                //select the tab that needs to be in focus (this will always be 0 unless its the first time opening and they clicked warning 3)
                tabControl.SelectedIndex = warning_number - 1;

                //load the data
                sql = "select u.forename + ' ' + u.surname as fullName,warning_note,warning_department,warning_date,warning_given_by,date_submitted " +
                    "FROM dbo.staff_warnings s " +
                    "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                    "where staff_id = " + staff_id.ToString() + " and warning_number = " + warning_number.ToString();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbStaff.Text = dt.Rows[0][0].ToString();
                    remove_tabs = 0;
                    txtWarning.Text = dt.Rows[0][1].ToString();
                    txtDepartment.Text = dt.Rows[0][2].ToString();
                    txtDateGiven.Text = dt.Rows[0][3].ToString();
                    txtWarningGivenBy.Text = dt.Rows[0][4].ToString();
                    txtDateLogged.Text = dt.Rows[0][5].ToString();


                }


                conn.Close();
            }

        }

        private void load_cmb()
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //get the data for the combobox
                string sql = "select distinct forename + ' ' + surname as staff FROM dbo.staff_warnings s " +
                    "left join[user_info].dbo.[user] u on s.staff_id = u.id " +
                    "WHERE[current] = 1 order by forename + ' ' + surname asc";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtStaff = new DataTable();
                    da.Fill(dtStaff);
                    cmbStaff.Items.Clear();
                    foreach (DataRow dr in dtStaff.Rows)
                    {
                        cmbStaff.Items.Add(dr[0].ToString());
                    }
                }
                conn.Close();
            }
        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update the staff_id based on what was picked
            remove_tabs = -1;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT id FROM [user_info].dbo.[user] where forename + ' ' + surname = '" + cmbStaff.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    int temp_staff_id = 0;
                    temp_staff_id = (int)cmd.ExecuteScalar();

                    if (staff_id != temp_staff_id)
                    {
                        staff_id = temp_staff_id;
                        load_data();
                        load_grids();

                    }
                }

                conn.Close();
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (remove_tabs == 0)
            {
                warning_number = tabControl.SelectedIndex + 1;
                load_data();
            }
        }

        private void load_grids()
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                staff_id = 0;
                string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + cmbStaff.Text + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());


                //absent total
                sql = "select  coalesce(sum(1),0) as [Total Absent] from dbo.absent_holidays " +
                         "left join[user_info].dbo.[user] u on u.id = staff_id " +
                         "where(absent_type = 5 or absent_type = 8) AND " +
                         "staff_id = " + staff_id.ToString() +
                         " AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    lblAbsent.Text = "Total Absent Days: " + cmd.ExecuteScalar().ToString();
                }

                //late total
                sql = "select  coalesce(sum(1),0) as [ Total Late] from dbo.absent_holidays " +
                        "left join[user_info].dbo.[user] u on u.id = staff_id " +
                        "where(absent_type = 7) AND " +
                        "staff_id = " + staff_id.ToString() +
                        "AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    lblLate.Text = "Total Late Days: " + cmd.ExecuteScalar().ToString();

                //absences
                sql = "select  Convert(char,date_absent,103)  as [Absent Date],datename(WEEKDAY,date_absent) as [Day of Week],sum(1) [Absent] from dbo.absent_holidays " +
                    "left join[user_info].dbo.[user] u on u.id = staff_id " +
                    "where(absent_type = 5 or absent_type = 8) AND " +
                    "staff_id =  " + staff_id.ToString() +
                    " AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                    "group by date_absent";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAbsent.DataSource = dt;
                }
                //lates 
                sql = "select  Convert(char,date_absent,103)  as [Late Date],datename(WEEKDAY,date_absent) as [Day of Week],sum(1) [Late] from dbo.absent_holidays " +
                    "left join[user_info].dbo.[user] u on u.id = staff_id " +
                    "where(absent_type = 7) AND " +
                    "staff_id =  " + staff_id.ToString() +
                    "AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                    "group by date_absent";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvLate.DataSource = dt;
                }

                conn.Close();
            }

            format();

        }
        private void format()
        {
            foreach (DataGridViewColumn col in dgvAbsent.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataGridViewColumn col in dgvLate.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

    }
}

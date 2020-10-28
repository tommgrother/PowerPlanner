using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    public partial class frmWeeklyOverTime : Form
    {
        //indexs
        public int overtimeIndex { get; set; }
        public int staffIDIndex { get; set; }
        public int dateIDIndex { get; set; }
        public int validation { get; set; }
        public int dateID { get; set; }
        public string department { get; set; }
        public DateTime startDate { get; set; }
        public DateTime tempDate { get; set; }
        public DateTime monday { get; set; } //should only need monday (we can date add based on what tab is selected and this should always be correct providing MONDAY is the correct date


        public decimal totalOvertime { get; set; }
        public frmWeeklyOverTime(DateTime _tempDate, string _department)
        {
            InitializeComponent();
            validation = 0;
            startDate = _tempDate;
            department = _department;
            lblTitle.Text = "Over Time for " + department;
            while (_tempDate.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                _tempDate = _tempDate.AddDays(-1);
            monday = _tempDate;

            // MessageBox.Show(monday.ToString());
            whichTab();

        }

        private void whichTab()
        {
            //if @validation = -1 thendata was changed so before we swap the dgv around we need to commit the data that was changed 
            if (validation == -1)
            {
                commitData();
                validation = 0;
            }


            //based on what tab it is we will add days to @monday
            //get the currently selected Tab
            if (tabControl1.SelectedIndex == 0) //monday
                tempDate = monday;
            if (tabControl1.SelectedIndex == 1)//tuesday
                tempDate = monday.AddDays(1);
            if (tabControl1.SelectedIndex == 2)//wednesday
                tempDate = monday.AddDays(2);
            if (tabControl1.SelectedIndex == 3)//thursday
                tempDate = monday.AddDays(3);
            if (tabControl1.SelectedIndex == 4)//friday
                tempDate = monday.AddDays(4);
            if (tabControl1.SelectedIndex == 5)//saturday
                tempDate = monday.AddDays(5);
            if (tabControl1.SelectedIndex == 6)//sunday
                tempDate = monday.AddDays(6);

            //MessageBox.Show(tempDate.ToString());
            //i think here we need to check if someone has entered data into the day and has swapped to anther - if they have then we need to commit it but this is a problem for later
            fillDGV();
        }
        private void fillDGV()
        {
            if (dataGridView1.Columns.Contains("Over Time") == true)
                dataGridView1.Columns.Remove("Over Time");


            //if dgv has content then null it
            //dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();
            if (dataGridView1.DataSource != null)
                dataGridView1.DataSource = null;
            //before entering this void we gotta hit whichTab so we have the unique date to use in for the sql

            //grab dateid quickly cause this bugs out :}
            using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT id FROM dbo.power_plan_date WHERE CAST(date_plan as date) = '" + tempDate.ToString("yyyy-MM-dd") + "'", connection))
                {
                    connection.Open();
                    dateID = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
            }

            string sql = "select d.forename + ' ' + d.surname as [Full Name],placement_type as [Placement],b.id as [date_id], a.staff_id as [staff_id] from dbo.power_plan_staff  a " +
                "LEFT JOIN dbo.power_plan_date b on a.date_id = b.id " +
                "LEFT JOIN [user_info].dbo.[user] d ON d.id = a.staff_id " +
                "WHERE b.id = " + dateID.ToString() + " AND a.department = '" + department + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dt.Columns.Add("Over Time");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].Visible = false;

                } 
                //dataGridView1.Columns.Add("Over Time", "Over Time");
                if (dataGridView1.Columns.Contains("Over Time") == true)
                    overtimeIndex = dataGridView1.Columns["Over Time"].Index;
                if (dataGridView1.Columns.Contains("date_id") == true)
                    dateIDIndex = dataGridView1.Columns["date_id"].Index;
                if (dataGridView1.Columns.Contains("staff_id") == true)
                    staffIDIndex = dataGridView1.Columns["staff_id"].Index;
              
                //now get the overtime for these dudes

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sql = "SELECT COALESCE(overtime,0) as [Over Time] FROM dbo.power_plan_overtime_remake WHERE staff_id = " + dataGridView1.Rows[i].Cells[staffIDIndex].Value.ToString() + " AND date_id = " + dateID;
                    //sql = "SELECT CASE WHEN exists (SELECT COALESCE(overtime,0) as [Over Time] FROM dbo.power_plan_overtime_remake WHERE staff_id = " + dataGridView1.Rows[i].Cells[staffIDIndex].Value.ToString() + " AND date_id = " + dateID + ") then COALESCE(overtime,0) else 0 end FROM dbo.power_plan_overtime_remake";
                    using (SqlCommand insertOT = new SqlCommand(sql, conn))
                    {
                        var getData = insertOT.ExecuteScalar();
                        if (getData != null)
                            dataGridView1.Rows[i].Cells[overtimeIndex].Value = Convert.ToString(getData);
                        else
                            dataGridView1.Rows[i].Cells[overtimeIndex].Value = "0";
                    }
                }
                conn.Close();
                dataGridView1.Refresh();
            }


            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[overtimeIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;



        }

        private void commitData()
        {
            string sql = "";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("usp_power_plan_add_overtime_remake", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@date_id", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[dateIDIndex].Value);
                        cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[staffIDIndex].Value);
                        cmd.Parameters.Add("@overtime", SqlDbType.Float).Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[overtimeIndex].Value);
                        cmd.Parameters.Add("@department", SqlDbType.NVarChar).Value = department;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            whichTab();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            validation = -1;
        }

        private void frmWeeklyOverTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close the form on todays date!!!
            //count the days between monday and startdate 
            double countDays = (monday - startDate).Days;
            if (countDays < 0)
                countDays = countDays * -1;
            tabControl1.SelectedIndex = Convert.ToInt32(countDays);
            //MessageBox.Show(tabControl1.TabPages[Convert.ToInt32(countDays)].Text.ToString());

            //if they close the overtime on a day where there is no one in it crashes
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                whichTab();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    totalOvertime = totalOvertime + Convert.ToDecimal(dataGridView1.Rows[i].Cells[overtimeIndex].Value);
            }
            department_changed dc = new department_changed();
            dc.setDepartment(department);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == overtimeIndex) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }

            }
        }
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmWeeklyOverTime_Shown(object sender, EventArgs e)
        {
            double countDays = (monday - startDate).Days;
            if (countDays < 0)
                countDays = countDays * -1;
            tabControl1.SelectedIndex = Convert.ToInt32(countDays);
        }
    }
}

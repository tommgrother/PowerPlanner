using DocumentFormat.OpenXml.Bibliography;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmWeeklyOverTimeRemake : Form
    {

        public DateTime trueMonday { get; set; }
        public DateTime tempDate { get; set; }
        public DateTime startDate { get; set; }
        public string department { get; set; }
        public int dataChanged { get; set; }
        public double totalOvertime { get; set; }

        //indexes
        public int full_name_index { get; set; }
        public int placement_index { get; set; }
        public int date_id_index { get; set; }
        public int staff_id_index { get; set; }
        public int am_index { get; set; }
        public int pm_index { get; set; }
        /////

        public frmWeeklyOverTimeRemake(DateTime passedDate, string _department)
        {
            InitializeComponent();

            department = _department;
            startDate = passedDate;
            lblTitle.Text = "Overtime for " + department;

            //open tab to todays date then fire whichtab

            //find the monday from the passed over date
            while (passedDate.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                passedDate = passedDate.AddDays(-1);

            trueMonday = passedDate;
        }

        private void whichTab()
        {
            //if @validation = -1 then data was changed so before we swap the dgv around we need to commit the data that was changed
            if (dataChanged == -1)
            {
                commitData();
                dataChanged = 0;
            }

            //based on what tab it is we will add days to trueMonday
            //get the currently selected Tab
            if (tabControl1.SelectedIndex == 0) //monday
                tempDate = trueMonday;
            if (tabControl1.SelectedIndex == 1)//tuesday
                tempDate = trueMonday.AddDays(1);
            if (tabControl1.SelectedIndex == 2)//wednesday
                tempDate = trueMonday.AddDays(2);
            if (tabControl1.SelectedIndex == 3)//thursday
                tempDate = trueMonday.AddDays(3);
            if (tabControl1.SelectedIndex == 4)//friday
                tempDate = trueMonday.AddDays(4);
            if (tabControl1.SelectedIndex == 5)//saturday
                tempDate = trueMonday.AddDays(5);
            if (tabControl1.SelectedIndex == 6)//sunday
                tempDate = trueMonday.AddDays(6);
            
            fillDGV();
        }

        private void commitData()
        {
            if (dataGridView1.Rows.Count < 1)
                return;

            string sql = "";
            double totalHours = 0;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("usp_power_plan_add_overtime_remake_am_pm", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@date_id", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[date_id_index].Value);
                        cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[staff_id_index].Value);
                        cmd.Parameters.Add("@am", SqlDbType.Float).Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[am_index].Value);
                        cmd.Parameters.Add("@pm", SqlDbType.Float).Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[pm_index].Value);
                        cmd.Parameters.Add("@department", SqlDbType.NVarChar).Value = department;
                        cmd.ExecuteNonQuery();
                        totalHours = totalHours + (Convert.ToDouble(dataGridView1.Rows[i].Cells[am_index].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells[pm_index].Value));
                    }
                }
                //update it here because it needs the full TOTAL of the overtime
                Overtime overtime = new Overtime();
                overtime.updateOT(tempDate, department, totalHours);
                conn.Close();
            }
        }


        private void fillDGV()
        {
            int date_id = 0;
            string sql = "SELECT id FROM dbo.power_plan_date WHERE CAST(date_plan as date) = '" + tempDate.ToString("yyyy-MM-dd") + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    date_id = Convert.ToInt32(cmd.ExecuteScalar());


                //get everyone who is in for the SELECTED day 
                sql = "select d.forename + ' ' + d.surname as [Full Name],placement_type as [Placement],b.id as [date_id], a.staff_id as [staff_id],COALESCE(am_pm.am,0) as am,COALESCE(am_pm.pm,0) as pm " +
                      "from dbo.power_plan_staff  a LEFT JOIN dbo.power_plan_date b on a.date_id = b.id " +
                      "LEFT JOIN [user_info].dbo.[user] d ON d.id = a.staff_id " +
                      "left join dbo.power_plan_overtime_remake ot on a.staff_id = ot.staff_id AND a.date_id = ot.date_id and a.department = ot.department  " +
                      "left join dbo.power_plan_overtime_remake_am_pm am_pm on ot.id = am_pm.overtime_remake_id " +
                      "WHERE b.id = " + date_id.ToString() + " AND a.department = '" + department + "' order by d.forename";


                using (SqlCommand cmd = new SqlCommand(sql,conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

                column_index();
                format();

                conn.Close();

            }
        }
        private void column_index()
        {
            if (dataGridView1.Columns.Contains("Full Name") == true)
                full_name_index = dataGridView1.Columns["Full Name"].Index;
            if (dataGridView1.Columns.Contains("Placement") == true)
                placement_index = dataGridView1.Columns["Placement"].Index;
            if (dataGridView1.Columns.Contains("date_id") == true)
                date_id_index = dataGridView1.Columns["date_id"].Index;
            if (dataGridView1.Columns.Contains("staff_id") == true)
                staff_id_index = dataGridView1.Columns["staff_id"].Index;
            if (dataGridView1.Columns.Contains("am") == true)
                am_index = dataGridView1.Columns["am"].Index;
            if (dataGridView1.Columns.Contains("pm") == true)
                pm_index = dataGridView1.Columns["pm"].Index;
        }

        private void format()
        {
            dataGridView1.Columns[date_id_index].Visible = false;
            dataGridView1.Columns[staff_id_index].Visible = false;
            dataGridView1.Columns[am_index].HeaderText = "AM Overtime";
            dataGridView1.Columns[pm_index].HeaderText = "PM Overtime";

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[full_name_index].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            whichTab();
        }

        private void frmWeeklyOverTimeRemake_Shown(object sender, EventArgs e)
        {
            double countDays = (trueMonday - startDate).Days;
            if (countDays < 0)
                countDays = countDays * -1;
            tabControl1.SelectedIndex = Convert.ToInt32(countDays);
            if (countDays == 0)
                tabControl1.SelectedIndex = Convert.ToInt32(countDays + 1);
            else
                tabControl1.SelectedIndex = Convert.ToInt32(countDays - 1);
            tabControl1.SelectedIndex = Convert.ToInt32(countDays);

            whichTab();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == am_index || dataGridView1.CurrentCell.ColumnIndex == pm_index) //Desired Column
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataChanged = -1;
        }

        private void frmWeeklyOverTimeRemake_FormClosing(object sender, FormClosingEventArgs e)
        {
            commitData();

            //need to close the form on the date it originally opened to - (this sets the overtime on the staff selection form
            double countDays = (trueMonday - startDate).Days;
            if (countDays < 0)
                countDays = countDays * -1;
            tabControl1.SelectedIndex = Convert.ToInt32(countDays);

            //if they close the overtime on a day where there is no one in it crashes
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                whichTab();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    totalOvertime = totalOvertime + (Convert.ToDouble(dataGridView1.Rows[i].Cells[am_index].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells[pm_index].Value));
            }




            department_changed dc = new department_changed();
            dc.setDepartment(department);
        }
    }
}

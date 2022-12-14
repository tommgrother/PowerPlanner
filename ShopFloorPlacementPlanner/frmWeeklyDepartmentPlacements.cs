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
    public partial class frmWeeklyDepartmentPlacements : Form
    {
        public frmWeeklyDepartmentPlacements()
        {
            InitializeComponent();

            //get the monday of /this/ week
            DateTime monday = DateTime.Now;
            while (monday.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                monday = monday.AddDays(-1);
            dtePicker.Value = monday;

        }

        private void frmWeeklyDepartmentView_Shown(object sender, EventArgs e)
        {
            load_data(); //dsjbsd
        }

        private void load_data()
        {
            DateTime planner_date = dtePicker.Value;
            while (planner_date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                planner_date = planner_date.AddDays(-1);

            for (int i = 0; i <= 6; i++)
            {
                //string sql = "select [Full Placement],round([Hours] + (c.overtime * 0.8),2) as [Hours] from dbo.view_planner_punch_staff a " +
                //    "left join dbo.power_plan_date b on a.date_plan = b.date_plan " +
                //    "left join dbo.power_plan_overtime_remake c on b.id = c.date_id AND c.staff_id = a.staff_id " +
                //    "where  a.department = '" + cmbDepartment.Text + "' AND c.department = '" + cmbDepartment.Text + "' AND a.date_plan = '" + planner_date.ToString("yyyyMMdd") + "' and [Staff Name] <> 'Allocation Block' ORDER BY [Staff Name]";

                string sql = "SELECT u.forename + N' ' + u.surname + N' ' + CHAR(13) + a.placement_type AS [Full Placement]," +
                    "round([Hours] + (COALESCE(ot.overtime,0) * 0.8),2) as [hours] " +
                    "FROM dbo.power_plan_staff AS a " +
                    "LEFT JOIN dbo.power_plan_date d ON a.date_id = d.id " +
                    "LEFT JOIN user_info.dbo.[user] AS u ON a.staff_id = u.id  " +
                    "LEFT JOIN dbo.power_plan_overtime_remake ot on a.staff_id = ot.staff_id AND a.department = ot.department AND d.id = ot.date_id " +
                    "WHERE a.department = '" + cmbDepartment.Text + "'  AND d.date_plan = '" + planner_date.ToString("yyyyMMdd") + "' AND (u.non_user = 0 or u.non_user is null) ORDER BY u.forename + N' ' + u.surname";

                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (i == 0)
                            dgvMonday.DataSource = dt;
                        if (i == 1)
                            dgvTuesday.DataSource = dt;
                        if (i == 2)
                            dgvWednesday.DataSource = dt;
                        if (i == 3)
                            dgvThursday.DataSource = dt;
                        if (i == 4)
                            dgvFriday.DataSource = dt;
                        if (i == 5)
                            dgvSaturday.DataSource = dt;
                        if (i == 6)
                            dgvSunday.DataSource = dt;

                        planner_date = planner_date.AddDays(1);
                    }
                }
            }
            format();
        }

        private void format()
        {

            dgvMonday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTuesday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvWednesday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvThursday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFriday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSaturday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSunday.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvMonday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTuesday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvWednesday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvThursday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFriday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSaturday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSunday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvMonday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTuesday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvWednesday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvThursday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvFriday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSaturday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSunday.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvMonday.ClearSelection();
            dgvTuesday.ClearSelection();
            dgvWednesday.ClearSelection();
            dgvThursday.ClearSelection();
            dgvFriday.ClearSelection();
            dgvSaturday.ClearSelection();
            dgvSunday.ClearSelection();

        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data();
        }

        private void dtePicker_CloseUp(object sender, EventArgs e)
        {
            load_data();
        }
    }
}



using ExcelNumberFormat;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    public partial class frmDepartmentActivityTracker : Form
    {
        public frmDepartmentActivityTracker()
        {
            InitializeComponent();

            fillDepartments();
        }

        private void fillDepartments()
        {

            string sql = "select " +
                         "coalesce(round(sum(actual_hours_punch) / nullif(sum(goal_hours_punch),0),2),0) * 100, " +
                         "coalesce(round(sum(actual_hours_bend)  / nullif(sum(goal_hours_bend),0),2),0) * 100, " +
                         "coalesce(round(sum(actual_hours_weld)  / nullif(sum(goal_hours_weld),0),2),0) * 100, " +
                         "coalesce(round(sum(actual_hours_buff)  / nullif(sum(goal_hours_buff),0),2),0) * 100, " +
                         "coalesce(round(sum(actual_hours)       / nullif(sum(goal_hours),0),2),0) * 100, " +
                         "coalesce(round(sum(actual_hours_pack)  / nullif(sum(goal_hours_pack),0),2),0) * 100, " +
                         "coalesce(round(sum(actual_hours_slimline)  / nullif(sum(goal_hours_slimline),0),2),0) * 100 " +
                         "from dbo.daily_department_goal " +
                         "where date_goal >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND date_goal <= '" + dteEnd.Value.ToString("yyyyMMdd") + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    txtPunching.Text = dt.Rows[0][0].ToString();
                    txtBending.Text = dt.Rows[0][1].ToString();
                    txtWelding.Text = dt.Rows[0][2].ToString();
                    txtBuffing.Text = dt.Rows[0][3].ToString();
                    txtPainting.Text = dt.Rows[0][4].ToString();
                    txtPacking.Text = dt.Rows[0][5].ToString();
                    txtSlimline.Text = dt.Rows[0][6].ToString();

                }
                conn.Close();
            }
            format();

        }

        private void dteStart_CloseUp(object sender, EventArgs e)
        {
            fillDepartments();
        }

        private void dteEnd_CloseUp(object sender, EventArgs e)
        {
            fillDepartments();
        }
        private void format()
        {
            if (Convert.ToDouble(txtPunching.Text) >= 100)
                txtPunching.BackColor = Color.LightSeaGreen;
            else
                txtPunching.BackColor = Color.PaleVioletRed;

            if (Convert.ToDouble(txtBending.Text) >= 100)
                txtBending.BackColor = Color.LightSeaGreen;
            else
                txtBending.BackColor = Color.PaleVioletRed;

            if (Convert.ToDouble(txtWelding.Text) >= 100)
                txtWelding.BackColor = Color.LightSeaGreen;
            else
                txtWelding.BackColor = Color.PaleVioletRed;

            if (Convert.ToDouble(txtBuffing.Text) >= 100)
                txtBuffing.BackColor = Color.LightSeaGreen;
            else
                txtBuffing.BackColor = Color.PaleVioletRed;

            if (Convert.ToDouble(txtPainting.Text) >= 100)
                txtPainting.BackColor = Color.LightSeaGreen;
            else
                txtPainting.BackColor = Color.PaleVioletRed;

            if (Convert.ToDouble(txtPacking.Text) >= 100)
                txtPacking.BackColor = Color.LightSeaGreen;
            else
                txtPacking.BackColor = Color.PaleVioletRed;

            if (Convert.ToDouble(txtSlimline.Text) >= 100)
                txtSlimline.BackColor = Color.LightSeaGreen;
            else
                txtSlimline.BackColor = Color.PaleVioletRed;

        }

        private void print_staff_sheet(string department)
        {
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\DEPARTMENT_ACTIVITY.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");



            //get all of the distinct staff in the department

            //vvv we need to loop through the staff 
            string sql = "select distinct u.forename + ' ' + u.surname as fullName from dbo.power_plan_staff s " +
                         "left join dbo.power_plan_date d on s.date_id = d.id " +
                         "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                         "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND (u.non_user = 0 or u.non_user is null) " +
                         "AND d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' order by u.forename + ' ' + u.surname asc ";


            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                DataTable dt_staff = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_staff);
                }

                int current_excel_row = 1;

                xlWorksheet.Cells[1][current_excel_row].Value2 = department + " Activity";

                current_excel_row++;

                //loop for each distinct staff
                for (int staff_row = 0; staff_row < dt_staff.Rows.Count; staff_row++)
                {

                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;

                    //MERGE THESE ROWS
                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                    //insert the staff members name into the excel sheet
                    xlWorksheet.Cells[1][current_excel_row].Value2 = dt_staff.Rows[staff_row][0].ToString();

                    current_excel_row++;

                    //column headers
                    xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                    xlWorksheet.Cells[2][current_excel_row].Value2 = "Day of Week";
                    xlWorksheet.Cells[3][current_excel_row].Value2 = "Hours";
                    xlWorksheet.Cells[4][current_excel_row].Value2 = "Actual";
                    xlWorksheet.Cells[5][current_excel_row].Value2 = "%";

                    current_excel_row++;

                    //get all the days this staff member is in this department between the selected dates

                    sql = "select d.date_plan from dbo.power_plan_staff s " +
                          "left join dbo.power_plan_date d on s.date_id = d.id " +
                          "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                          "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND " +
                          "d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                          "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[staff_row][0] + "' order by d.date_plan";

                    DataTable dt_date = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt_date);
                    }

                    int date_row_count = 0;
                    date_row_count = dt_date.Rows.Count;

                    for (int date_row = 0; date_row < dt_date.Rows.Count; date_row++)
                    {
                        //now we get the day of week/hours/actual/%

                        sql = "select '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("dd/MM/yyyy") + "',datename(WEEKDAY,'" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "'),sum([hours]) as [hours],sum([actual]) as actual,coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent] from (" +
                            "select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours],0 as actual from dbo.power_plan_staff s " +
                            "left join dbo.power_plan_date d on s.date_id = d.id " +
                            "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                            "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                            "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND d.date_plan = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                            "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[staff_row][0] + "' " +
                            "union all " +
                            "select 0 as [hours],sum(l.time_for_part) / 60 as actual from dbo.door_part_completion_log l " +
                            "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                            "where op = '" + department + "' AND cast(part_complete_date as date) = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                            "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[staff_row][0] + "') as a";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            xlWorksheet.Cells[1][current_excel_row].Value2 = dt.Rows[0][0].ToString();
                            xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                            xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                            xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                            xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][4].ToString();

                            //add conditional formatting to the last row (%)
                            Excel.FormatCondition formatGreen = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                                   Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                                   Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing));

                            formatGreen.Font.Bold = true;
                            formatGreen.Font.Color = Color.DarkGreen;
                            formatGreen.Interior.Color = Color.LimeGreen;

                            Excel.FormatCondition formatRed = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                               Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                               Excel.XlFormatConditionOperator.xlLess, 1,
                                               Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing));

                            formatRed.Font.Bold = true;
                            formatRed.Font.Color = Color.DarkRed;
                            formatRed.Interior.Color = Color.PaleVioletRed;
                        }

                        current_excel_row++;
                    }
                    //average %
                    xlWorksheet.Cells[4][current_excel_row].Value2 = "AVERAGE %";
                    xlWorksheet.Cells[5][current_excel_row].Value2 = "=AVERAGE(E" + (current_excel_row - 1 - date_row_count).ToString() + ":E" + (current_excel_row - 1).ToString() + ")";
                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 3]].Merge();
                    current_excel_row++;
                }
                xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row - 1, 5]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                conn.Close();
            }



            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);




            //xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
            }


        }


        private void print_staff_dropped_sheet(string department)
        {
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\DEPARTMENT_ACTIVITY.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");



            //get all of the distinct staff in the department

            //vvv we need to loop through the staff 
            string sql = "select distinct u.forename + ' ' + u.surname as fullName from dbo.power_plan_staff s " +
                         "left join dbo.power_plan_date d on s.date_id = d.id " +
                         "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                         "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND (u.non_user = 0 or u.non_user is null) " +
                         "AND d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' order by u.forename + ' ' + u.surname asc ";


            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                DataTable dt_staff = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_staff);
                }

                int current_excel_row = 1;

                xlWorksheet.Cells[1][current_excel_row].Value2 = department + " Activity";

                current_excel_row++;

                //loop for each distinct staff
                for (int staff_row = 0; staff_row < dt_staff.Rows.Count; staff_row++)
                {

                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;

                    //MERGE THESE ROWS
                    xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                    //insert the staff members name into the excel sheet
                    xlWorksheet.Cells[1][current_excel_row].Value2 = dt_staff.Rows[staff_row][0].ToString();

                    current_excel_row++;

                    //column headers
                    xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                    xlWorksheet.Cells[2][current_excel_row].Value2 = "Day of Week";
                    xlWorksheet.Cells[3][current_excel_row].Value2 = "Hours";
                    xlWorksheet.Cells[4][current_excel_row].Value2 = "Actual";
                    xlWorksheet.Cells[5][current_excel_row].Value2 = "%";

                    current_excel_row++;

                    //get all the days this staff member is in this department between the selected dates

                    sql = "select d.date_plan from dbo.power_plan_staff s " +
                          "left join dbo.power_plan_date d on s.date_id = d.id " +
                          "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                          "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND " +
                          "d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                          "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[staff_row][0] + "' order by d.date_plan";

                    DataTable dt_date = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt_date);
                    }

                    int date_row_count = 0;
                    date_row_count = dt_date.Rows.Count;

                    int dropped_row_added = 0;
                    for (int date_row = 0; date_row < dt_date.Rows.Count; date_row++)
                    {
                        //now we get the day of week/hours/actual/%

                        sql = "select '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("dd/MM/yyyy") + "',datename(WEEKDAY,'" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "'),sum([hours]) as [hours],sum([actual]) as actual,coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent] from (" +
                            "select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours],0 as actual from dbo.power_plan_staff s " +
                            "left join dbo.power_plan_date d on s.date_id = d.id " +
                            "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                            "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                            "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND d.date_plan = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                            "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[staff_row][0] + "' " +
                            "union all " +
                            "select 0 as [hours],sum(l.time_for_part) / 60 as actual from dbo.door_part_completion_log l " +
                            "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                            "where op = '" + department + "' AND cast(part_complete_date as date) = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                            "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[staff_row][0] + "') as a";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            //if percent is > 1 we place it otherwise skip this row

                            if (Convert.ToDecimal(dt.Rows[0][4].ToString()) >= 1)
                            {
                                continue; //skip to next iteration
                            }
                            else
                                dropped_row_added++;


                            xlWorksheet.Cells[1][current_excel_row].Value2 = dt.Rows[0][0].ToString();
                            xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                            xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                            xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                            xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][4].ToString();

                            //add conditional formatting to the last row (%)
                            Excel.FormatCondition formatGreen = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                                   Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                                   Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing));

                            formatGreen.Font.Bold = true;
                            formatGreen.Font.Color = Color.DarkGreen;
                            formatGreen.Interior.Color = Color.LimeGreen;

                            Excel.FormatCondition formatRed = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                               Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                               Excel.XlFormatConditionOperator.xlLess, 1,
                                               Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing));

                            formatRed.Font.Bold = true;
                            formatRed.Font.Color = Color.DarkRed;
                            formatRed.Interior.Color = Color.PaleVioletRed;
                        }

                        current_excel_row++;
                    }
                    if (dropped_row_added == 0)
                    {
                        current_excel_row = current_excel_row - 2;
                        ((Excel.Range)xlWorksheet.Rows[current_excel_row, Missing.Value]).Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        ((Excel.Range)xlWorksheet.Rows[current_excel_row, Missing.Value]).Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else
                    {
                        //average %
                        xlWorksheet.Cells[4][current_excel_row].Value2 = "AVERAGE %";
                        xlWorksheet.Cells[5][current_excel_row].Value2 = "=AVERAGE(E" + (current_excel_row - 1 - date_row_count).ToString() + ":E" + (current_excel_row - 1).ToString() + ")";
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 3]].Merge();
                        current_excel_row++;
                    }
                }
                xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row - 1, 5]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                conn.Close();
            }



            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);




            //xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
            }


        }


        private void print_new_staff_dropped_sheet(string department)
        {
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string filName = @"\\designsvr1\public\Kevin Power Planner\DEPARTMENT_ACTIVITY.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(filName);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");



            //get all of the distinct staff in the department

            //vvv we need to loop through the staff 
            string sql = "select distinct u.forename + ' ' + u.surname as fullName,coalesce(u.[start_date],'') as [start_date],u.id  from dbo.power_plan_staff s " +
                         "left join dbo.power_plan_date d on s.date_id = d.id " +
                         "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                         "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND (u.non_user = 0 or u.non_user is null) AND s.hours > 0 " +
                         "AND d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' order by u.forename + ' ' + u.surname asc ";


            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                DataTable dt_staff = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_staff);
                }

                int current_excel_row = 1;

                xlWorksheet.Cells[1][current_excel_row].Value2 = department + " Activity";
                current_excel_row++;

                //insert the overall complete and overall dropped 

                sql = "SELECT AVG([percent]) as [percent] FROM (select datename(WEEKDAY,group_date) as [dayOfWeek],sum([hours]) as [hours],sum([actual]) as actual," +
                      "coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent],group_date from " +
                      "(select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours], 0 as actual,d.date_plan as group_date " +
                      "from dbo.power_plan_staff s " +
                      "left join dbo.power_plan_date d on s.date_id = d.id  " +
                      "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                      "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                      "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "'  group by d.date_plan " +
                      "union all " +
                      "select 0 as [hours],sum(l.time_for_part) / 60 as actual,cast(part_complete_date as date) as group_date from dbo.door_part_completion_log l " +
                      "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                      "where op = '" + department + "' AND cast(part_complete_date as date) >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and cast(part_complete_date as date) <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                      "group by part_complete_date ) as a group by group_date) as temp where ([hours] >  0 or actual > 0)";

                decimal overall_percent = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var temp = cmd.ExecuteScalar();
                    if (temp != null)
                        overall_percent = Convert.ToDecimal(temp);
                }

                xlWorksheet.Cells[1][current_excel_row].Value2 = "Overall Complete";
                xlWorksheet.Cells[4][current_excel_row].Value2 = "Overall Dropped";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;
                current_excel_row++;
                xlWorksheet.Cells[1][current_excel_row].Value2 = overall_percent;

                decimal overall_dropped = 0;

                if (overall_percent < 1)
                    overall_dropped = 1 - overall_percent;

                xlWorksheet.Cells[4][current_excel_row].Value2 = overall_dropped;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;


                //add conditional formatting to the last row (%)
                Excel.FormatCondition formatGreen = (Excel.FormatCondition)(xlWorksheet.Range("A" + current_excel_row.ToString(),
                    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                       Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                       Type.Missing, Type.Missing, Type.Missing,
                                       Type.Missing, Type.Missing));

                formatGreen.Font.Bold = true;
                formatGreen.Font.Color = Color.DarkGreen;
                formatGreen.Interior.Color = Color.LimeGreen;

                Excel.FormatCondition formatRed = (Excel.FormatCondition)(xlWorksheet.Range("A" + current_excel_row.ToString(),
                                   Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                   Excel.XlFormatConditionOperator.xlLess, 1,
                                   Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing));

                formatRed.Font.Bold = true;
                formatRed.Font.Color = Color.DarkRed;
                formatRed.Interior.Color = Color.PaleVioletRed;

                Excel.FormatCondition formatGreen2 = (Excel.FormatCondition)(xlWorksheet.Range("D" + current_excel_row.ToString(),
    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                       Excel.XlFormatConditionOperator.xlEqual, 0,
                       Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing));

                formatGreen2.Font.Bold = true;
                formatGreen2.Font.Color = Color.DarkGreen;
                formatGreen2.Interior.Color = Color.LimeGreen;

                Excel.FormatCondition formatRed2 = (Excel.FormatCondition)(xlWorksheet.Range("D" + current_excel_row.ToString(),
                                   Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                   Excel.XlFormatConditionOperator.xlGreater, 0,
                                   Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing));

                formatRed2.Font.Bold = true;
                formatRed2.Font.Color = Color.DarkRed;
                formatRed2.Interior.Color = Color.PaleVioletRed;


                current_excel_row++;

                //need to workout the average for each of the staff

                //if they are < 100 then we show everyday that they are placed

                //else we go next

                int sheet_counter = 1;
                int skipSheetIncrement = -1;
                for (int i = 0; i < dt_staff.Rows.Count; i++)
                {
                    if (skipSheetIncrement == 0)
                    {
                        current_excel_row = 1;
                        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Worksheets.Add
                            (System.Reflection.Missing.Value, xlWorkbook.Worksheets[xlWorkbook.Worksheets.Count],
                            System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                        xlWorksheet = xlWorkbook.Sheets[sheet_counter];

                        xlWorkSheet.Name = "Sheet " + sheet_counter.ToString();

                        // Get the range of the column you want to format
                        Excel.Range range2 = xlWorksheet.Columns[5];
                        // Apply percentage format to the range
                        range2.NumberFormat = "0.00%";

                    }

                    //vvv this is being removed 2024-09-17 because kevin doesnt want the average percent to decide if they are printed or not
                    //needs to print if they drop any hours (so that it matches productivity)

                    ////sql = "SELECT AVG([percent]) as [percent] FROM (" +
                    ////      "select datename(WEEKDAY,group_date) as [dayOfWeek],sum([hours]) as [hours]," +
                    ////      "sum([actual]) as actual,coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent],group_date from (" +
                    ////      "select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours],0 as actual,d.date_plan as group_date from dbo.power_plan_staff s " +
                    ////      "left join dbo.power_plan_date d on s.date_id = d.id " +
                    ////      "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                    ////      "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                    ////      "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND " +
                    ////      "d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                    ////      "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "' " +
                    ////      "group by d.date_plan " +
                    ////      "union all " +
                    ////      "select 0 as [hours],sum(l.time_for_part) / 60 as actual,cast(part_complete_date as date) as group_date from dbo.door_part_completion_log l " +
                    ////      "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                    ////      "where op = '" + department + "' AND " +
                    ////      "cast(part_complete_date as date) >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and cast(part_complete_date as date) <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                    ////      "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "' " +
                    ////      "group by part_complete_date ) as a " +
                    ////      "group by group_date) as temp where ([hours] >  0 or actual > 0)";

                    sql = "SELECT " +
                        "(SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] " +
                        "FROM dbo.door_part_completion_log " +
                        "WHERE staff_id = " + dt_staff.Rows[i][2] + " AND " +
                        "CAST(part_complete_date as DATE) >= '" + dteStart.Value.ToString("yyyyMMdd") + "' " +
                        "AND CAST(part_complete_date as DATE) <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                        "AND (part_status = 'Complete' or part_status = 'Partial')  AND op = '" + department + "' " +
                        "GROUP BY staff_id),0) " +
                        "- " +
                        "(SELECT sum(a.[hours]) as [set_hours] " +
                        "FROM dbo.power_plan_staff a " +
                        "LEFT JOIN dbo.power_plan_date b on a.date_id = b.id  " +
                        "left join dbo.power_plan_staff_percent_log p on b.date_plan = p.log_date AND a.staff_id = p.staff_id AND a.department = p.department " +
                        "WHERE a.staff_id = " + dt_staff.Rows[i][2] + " AND " +
                        "CAST(b.date_plan as DATE)>= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND " +
                        "CAST(b.date_plan as DATE)<= '" + dteEnd.Value.ToString("yyyyMMdd") + "' AND " +
                        "a.department = '" + department + "' " +
                        "GROUP BY  a.staff_id))";

                    int skip_staff = 0;
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        var temp = cmd.ExecuteScalar();
                        if (temp == null)
                            skip_staff = -1;
                        else
                        {
                           // MessageBox.Show(dt_staff.Rows[i][2].ToString());
                            if (Convert.ToDecimal(temp) < 0)
                                skip_staff = 0;
                            else
                                skip_staff = -1;
                        }
                    }

                    if (skip_staff == -1)
                        skipSheetIncrement = -1;
                    else
                        skipSheetIncrement = 0;

                    if (skip_staff == 0)
                    {

                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;

                        //MERGE THESE ROWS
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                        //insert the staff members name (AND ADD THE START DATE) into the excel sheet
                        xlWorksheet.Cells[1][current_excel_row].Value2 = dt_staff.Rows[i][0].ToString() + " - Start Date: " + Convert.ToDateTime(dt_staff.Rows[i][1].ToString()).ToString("dd/MM/yyyy");

                        current_excel_row++;

                        //column headers
                        xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                        xlWorksheet.Cells[2][current_excel_row].Value2 = "Day of Week";
                        xlWorksheet.Cells[3][current_excel_row].Value2 = "Hours";
                        xlWorksheet.Cells[4][current_excel_row].Value2 = "Actual";
                        xlWorksheet.Cells[5][current_excel_row].Value2 = "%";

                        current_excel_row++;

                        //we need to list all of the days this staff was in over the dates selected 

                        //get all the days this staff member is in this department between the selected dates

                        sql = "select d.date_plan from dbo.power_plan_staff s " +
                              "left join dbo.power_plan_date d on s.date_id = d.id " +
                              "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                              "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND " +
                              "d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                              "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "' order by d.date_plan";

                        DataTable dt_date = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt_date);
                        }

                        int date_row_count = 0;
                        date_row_count = dt_date.Rows.Count;

                        double hours_goal_total = 0;
                        double hours_actual_total = 0;

                        for (int date_row = 0; date_row < dt_date.Rows.Count; date_row++)
                        {
                            //now we get the day of week/hours/actual/%
                            if (department == "Slimline")
                            {
                                sql = "select Convert(varchar,CAST('" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' as date),103),datename(WEEKDAY,'" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "'),sum([hours]) as [hours],sum([actual]) as actual,coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent] from (" +
                                    "select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours],0 as actual from dbo.power_plan_staff s " +
                                    "left join dbo.power_plan_date d on s.date_id = d.id " +
                                    "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                                    "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                                    "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND d.date_plan = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                                    "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "' " +
                                    "union all " +
                                    "select 0 as [hours],sum(l.time_for_part) / 60 as actual from dbo.door_part_completion_log l " +
                                    "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                                    "where (op = 'Prepping' OR op = 'Assembly' OR op = 'Cutting' or op = 'SL_pack' or op = 'SL_Buff' or op = 'Prep') " +
                                    "AND cast(part_complete_date as date) = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                                    "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "') as a";
                            }
                            else
                            {
                                sql = "select Convert(varchar,CAST('" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' as date),103),datename(WEEKDAY,'" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "'),sum([hours]) as [hours],sum([actual]) as actual,coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent] from (" +
                                   "select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours],0 as actual from dbo.power_plan_staff s " +
                                   "left join dbo.power_plan_date d on s.date_id = d.id " +
                                   "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                                   "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                                   "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND d.date_plan = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                                   "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "' " +
                                   "union all " +
                                   "select 0 as [hours],sum(l.time_for_part) / 60 as actual from dbo.door_part_completion_log l " +
                                   "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                                   "where op = '" + department + "' AND cast(part_complete_date as date) = '" + Convert.ToDateTime(dt_date.Rows[date_row][0]).ToString("yyyyMMdd") + "' " +
                                   "AND u.forename + ' ' + u.surname = '" + dt_staff.Rows[i][0] + "') as a";
                            }

                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                xlWorksheet.Cells[1][current_excel_row].Value2 = /*Convert.ToDateTime(*/dt.Rows[0][0].ToString();//).ToString("dd/MM/yyyy");
                                xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                                xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                                hours_goal_total = hours_goal_total + Convert.ToDouble(dt.Rows[0][2].ToString());
                                xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                                hours_actual_total = hours_actual_total + Convert.ToDouble(dt.Rows[0][3].ToString());
                                xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][4].ToString();

                                //add conditional formatting to the last row (%)
                                Excel.FormatCondition formatGreen3 = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                                       Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                                       Type.Missing, Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing));

                                formatGreen3.Font.Bold = true;
                                formatGreen3.Font.Color = Color.DarkGreen;
                                formatGreen3.Interior.Color = Color.LimeGreen;

                                Excel.FormatCondition formatRed3 = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                                   Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                                   Excel.XlFormatConditionOperator.xlLess, 1,
                                                   Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing));

                                formatRed3.Font.Bold = true;
                                formatRed3.Font.Color = Color.DarkRed;
                                formatRed3.Interior.Color = Color.PaleVioletRed;
                            }

                            current_excel_row++;
                        }
                        //average %
                        xlWorksheet.Cells[3][current_excel_row].Value2 = hours_goal_total.ToString();
                        xlWorksheet.Cells[4][current_excel_row].Value2 = hours_actual_total.ToString();
                        xlWorksheet.Cells[5][current_excel_row].Value2 = "=AVERAGE(E" + (current_excel_row - 1 - date_row_count).ToString() + ":E" + (current_excel_row - 1).ToString() + ")";
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 2]].Merge();
                        xlWorksheet.Cells[1][current_excel_row].Value2 = "TOTAL/AVERAGE";
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 12;
                        current_excel_row++;
                        //sales value of labor lost
                        xlWorksheet.Cells[1][current_excel_row].Value2 = "TOTAL DROPPED:";
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                        xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 12;
                        xlWorksheet.Cells[4][current_excel_row].Value2 = Math.Round(hours_goal_total - hours_actual_total,2).ToString();
                        xlWorksheet.Cells[5][current_excel_row].Value2 = Math.Round((hours_goal_total - hours_actual_total) * 68,2).ToString();
                        xlWorksheet.Cells[5][current_excel_row].NumberFormat = "£#,##0.00";
                    }

                    //print
                    if (skipSheetIncrement == 0)
                    {
                        xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row, 5]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                        xlWorksheet.Columns.AutoFit();
                        xlWorksheet.Rows.AutoFit();

                        Excel.PageSetup xlPageSetUp2 = xlWorksheet.PageSetup;
                        xlPageSetUp2.Zoom = false;
                        xlPageSetUp2.FitToPagesWide = 1;
                        xlPageSetUp2.Orientation = Excel.XlPageOrientation.xlPortrait;


                        xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        sheet_counter++;
                    }
                }



                //supervisor attendence
                current_excel_row = 1;
                //sheet_counter++;
                xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Worksheets.Add
                    (System.Reflection.Missing.Value, xlWorkbook.Worksheets[xlWorkbook.Worksheets.Count],
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xlWorksheet = xlWorkbook.Sheets[sheet_counter];

                xlWorksheet.Name = "Sheet " + sheet_counter.ToString();

                //we need to count how many days there are between the selected dates 
                int supervisor_dates = 0;

                xlWorksheet.Cells[1][current_excel_row].Value2 = " Supervisor Attendence";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                current_excel_row++;

                supervisor_dates = (dteEnd.Value - dteStart.Value).Days + 1;

                DateTime current_supervisor_date = dteStart.Value;
                for (int i = 0; i < supervisor_dates; i++)
                {
                    if (current_supervisor_date.DayOfWeek == DayOfWeek.Saturday || current_supervisor_date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        current_supervisor_date = current_supervisor_date.AddDays(1);
                        continue;
                    }

                    sql = "select coalesce([9_30],'Not Pressed'),coalesce([11_30],'Not Pressed'),coalesce([2_30],'Not Pressed'),coalesce([eos],'Not Pressed') " +
                             "from dbo.supervisor_log where supervisor_date = '" + current_supervisor_date.ToString("yyyyMMdd") + "' AND department = '" + department.Replace("ing", "") + "'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                        xlWorksheet.Cells[2][current_excel_row].Value2 = "9:30";
                        xlWorksheet.Cells[3][current_excel_row].Value2 = "11:30";
                        xlWorksheet.Cells[4][current_excel_row].Value2 = "14:30";
                        xlWorksheet.Cells[5][current_excel_row].Value2 = "End of Shift";
                        current_excel_row++;

                        xlWorksheet.Cells[1][current_excel_row].Value2 = current_supervisor_date.ToString("dd/MM/yyyy");

                        if (dt.Rows.Count < 1) //null rows
                        {

                            xlWorksheet.Cells[2][current_excel_row].Value2 = "Not Pressed";
                            xlWorksheet.Cells[3][current_excel_row].Value2 = "Not Pressed";
                            xlWorksheet.Cells[4][current_excel_row].Value2 = "Not Pressed";
                            xlWorksheet.Cells[5][current_excel_row].Value2 = "Not Pressed";
                        }
                        else
                        {
                            xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][0].ToString();
                            if (dt.Rows[0][0].ToString() != "Not Pressed" && dt.Rows[0][0].ToString() != "Nobody" && dt.Rows[0][0].ToString() != "No Management")
                                xlWorksheet.Cells[2][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][0].ToString() == "No Management")
                                xlWorksheet.Cells[2][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;

                            xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                            if (dt.Rows[0][1].ToString() != "Not Pressed" && dt.Rows[0][1].ToString() != "Nobody" && dt.Rows[0][1].ToString() != "No Management")
                                xlWorksheet.Cells[3][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][1].ToString() == "No Management")
                                xlWorksheet.Cells[3][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;

                            xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                            if (dt.Rows[0][2].ToString() != "Not Pressed" && dt.Rows[0][2].ToString() != "Nobody" && dt.Rows[0][2].ToString() != "No Management")
                                xlWorksheet.Cells[4][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][2].ToString() == "No Management")
                                xlWorksheet.Cells[4][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;

                            xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                            if (dt.Rows[0][3].ToString() != "Not Pressed" && dt.Rows[0][3].ToString() != "Nobody" && dt.Rows[0][3].ToString() != "No Management")
                                xlWorksheet.Cells[5][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][3].ToString() == "No Management")
                                xlWorksheet.Cells[5][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;
                        }

                    }
                    current_excel_row++;
                    current_supervisor_date = current_supervisor_date.AddDays(1);
                }





                xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row - 1, 5]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                //autofit all rows
                Microsoft.Office.Interop.Excel.Worksheet ws = xlApp.ActiveWorkbook.Worksheets[sheet_counter];
                Microsoft.Office.Interop.Excel.Range range = ws.UsedRange;
                ws.Columns.AutoFit();
                ws.Rows.AutoFit();


                conn.Close();
            }


            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);




            //xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
            }
        }



        private void print_department_sheet(string department)
        {
            int average_percent = 0;
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\DEPARTMENT_ACTIVITY.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");



            //get all of the distinct staff in the department

            //vvv we need to loop through the staff 
            string sql = "select CAST(date_goal as date) " +
                "from dbo.daily_department_goal where date_goal >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND date_goal <= '" + dteEnd.Value.ToString("yyyyMMdd") + "'" +
                "group by date_goal order by date_goal";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                DataTable dt_department = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_department);
                }

                int current_excel_row = 1;

                xlWorksheet.Cells[1][current_excel_row].Value2 = department + " Activity";

                current_excel_row++;

                if (department != "Punching")
                {
                    sql = "SELECT AVG([percent]) as [percent] FROM (select datename(WEEKDAY,group_date) as [dayOfWeek],sum([hours]) as [hours],sum([actual]) as actual," +
                          "coalesce(sum([actual]) / nullif(sum([hours]),0),0) as [percent],group_date from " +
                          "(select sum(s.[hours]) + sum((coalesce(ot.overtime,0) * 0.8)) as [hours], 0 as actual,d.date_plan as group_date " +
                          "from dbo.power_plan_staff s " +
                          "left join dbo.power_plan_date d on s.date_id = d.id  " +
                          "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND ot.department = s.department AND s.staff_id = ot.staff_id " +
                          "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                          "where s.department = '" + department.Replace("Buffing", "Dressing") + "' AND d.date_plan >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and d.date_plan <= '" + dteEnd.Value.ToString("yyyyMMdd") + "'  group by d.date_plan " +
                          "union all " +
                          "select 0 as [hours],sum(l.time_for_part) / 60 as actual,cast(part_complete_date as date) as group_date from dbo.door_part_completion_log l " +
                          "left join [user_info].dbo.[user] u on l.staff_id = u.id " +
                          "where op = '" + department + "' AND cast(part_complete_date as date) >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and cast(part_complete_date as date) <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' " +
                          "group by part_complete_date ) as a group by group_date) as temp where ([hours] >  0 or actual > 0)";

                }
                else
                {
                    sql = "select " +
                          "coalesce(round(sum(actual_hours_punch) / nullif(sum(goal_hours_punch),0),2),0)  " +
                          "from dbo.daily_department_goal " +
                          "where date_goal >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND date_goal <= '" + dteEnd.Value.ToString("yyyyMMdd") + "'";
                }
                decimal overall_percent = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var temp2 = cmd.ExecuteScalar();
                    if (temp2 != null)
                        overall_percent = Convert.ToDecimal(temp2);
                }

                xlWorksheet.Cells[1][current_excel_row].Value2 = "Overall Complete";
                xlWorksheet.Cells[4][current_excel_row].Value2 = "Overall Dropped";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;
                current_excel_row++;
                xlWorksheet.Cells[1][current_excel_row].Value2 = overall_percent;

                decimal overall_dropped = 0;

                if (overall_percent < 1)
                    overall_dropped = 1 - overall_percent;

                xlWorksheet.Cells[4][current_excel_row].Value2 = overall_dropped;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;


                //add conditional formatting to the last row (%)
                Excel.FormatCondition formatGreen = (Excel.FormatCondition)(xlWorksheet.Range("A" + current_excel_row.ToString(),
                    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                       Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                       Type.Missing, Type.Missing, Type.Missing,
                                       Type.Missing, Type.Missing));

                formatGreen.Font.Bold = true;
                formatGreen.Font.Color = Color.DarkGreen;
                formatGreen.Interior.Color = Color.LimeGreen;

                Excel.FormatCondition formatRed = (Excel.FormatCondition)(xlWorksheet.Range("A" + current_excel_row.ToString(),
                                   Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                   Excel.XlFormatConditionOperator.xlLess, 1,
                                   Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing));

                formatRed.Font.Bold = true;
                formatRed.Font.Color = Color.DarkRed;
                formatRed.Interior.Color = Color.PaleVioletRed;

                Excel.FormatCondition formatGreen2 = (Excel.FormatCondition)(xlWorksheet.Range("D" + current_excel_row.ToString(),
    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                       Excel.XlFormatConditionOperator.xlEqual, 0,
                       Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing));

                formatGreen2.Font.Bold = true;
                formatGreen2.Font.Color = Color.DarkGreen;
                formatGreen2.Interior.Color = Color.LimeGreen;

                Excel.FormatCondition formatRed2 = (Excel.FormatCondition)(xlWorksheet.Range("D" + current_excel_row.ToString(),
                                   Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                   Excel.XlFormatConditionOperator.xlGreater, 0,
                                   Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing));

                formatRed2.Font.Bold = true;
                formatRed2.Font.Color = Color.DarkRed;
                formatRed2.Interior.Color = Color.PaleVioletRed;


                current_excel_row++;


                current_excel_row++;

                xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                xlWorksheet.Cells[2][current_excel_row].Value2 = "Day of Week";
                xlWorksheet.Cells[3][current_excel_row].Value2 = "Hours";
                xlWorksheet.Cells[4][current_excel_row].Value2 = "Actual";
                xlWorksheet.Cells[5][current_excel_row].Value2 = "%";

                double hours_goal_total = 0;
                double hours_actual_total = 0;

                //loop for each distinct date
                for (int department_row = 0; department_row < dt_department.Rows.Count; department_row++)
                {

                    //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                    //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;

                    ////MERGE THESE ROWS
                    //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                    ////insert the staff members name into the excel sheet
                    //xlWorksheet.Cells[1][current_excel_row].Value2 = dt_department.Rows[department_row][0].ToString();

                    //current_excel_row++;

                    //column headers


                    current_excel_row++;

                    //get all the days this staff member is in this department between the selected dates




                    //now we get the day of week/hours/actual/%



                    sql = "select  '" + Convert.ToDateTime(dt_department.Rows[department_row][0]).ToString("dd/MM/yyyy") + "'," +
                       "datename(WEEKDAY,'" + Convert.ToDateTime(dt_department.Rows[department_row][0]).ToString("yyyyMMdd") + "')," +
                       "sum([goal_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + "]) as [hours]," +
                       "sum([actual_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + "]) as actual, " +
                       " coalesce(round(sum(actual_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + ") / nullif(sum(goal_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + "),0),2),0) " +
                       "from dbo.daily_department_goal where date_goal = '" + Convert.ToDateTime(dt_department.Rows[department_row][0]).ToString("yyyyMMdd") + "' " +
                       "group by date_goal order by date_goal";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        xlWorksheet.Cells[1][current_excel_row].Value2 = dt.Rows[0][0].ToString();
                        xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                        xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                        hours_goal_total = hours_goal_total + Convert.ToDouble(dt.Rows[0][2].ToString());
                        xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                        hours_actual_total = hours_actual_total + Convert.ToDouble(dt.Rows[0][3].ToString());
                        xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][4].ToString();
                        xlWorksheet.Cells[5][current_excel_row].NumberFormat = "0.00%";

                        //add conditional formatting to the last row (%)
                        Excel.FormatCondition formatGreen3 = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                            Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                               Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                               Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing));

                        formatGreen3.Font.Bold = true;
                        formatGreen3.Font.Color = Color.DarkGreen;
                        formatGreen3.Interior.Color = Color.LimeGreen;

                        Excel.FormatCondition formatRed3 = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                           Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                           Excel.XlFormatConditionOperator.xlLess, 1,
                                           Type.Missing, Type.Missing, Type.Missing,
                                           Type.Missing, Type.Missing));

                        formatRed3.Font.Bold = true;
                        formatRed3.Font.Color = Color.DarkRed;
                        formatRed3.Interior.Color = Color.PaleVioletRed;
                    }


                    average_percent = department_row;

                }
                //average %
                current_excel_row++;
                //xlWorksheet.Cells[4][current_excel_row].Value2 = "AVERAGE %";
                //xlWorksheet.Cells[5][current_excel_row].Value2 = "=AVERAGE(E" + (current_excel_row - 1 - average_percent).ToString() + ":E" + (current_excel_row - 1).ToString() + ")";
                //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 3]].Merge();
                //current_excel_row++;
                xlWorksheet.Cells[3][current_excel_row].Value2 = hours_goal_total.ToString();
                xlWorksheet.Cells[4][current_excel_row].Value2 = hours_actual_total.ToString();
                xlWorksheet.Cells[5][current_excel_row].Value2 = "=AVERAGE(E" + (current_excel_row - 1 - average_percent).ToString() + ":E" + (current_excel_row - 1).ToString() + ")";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 2]].Merge();
                xlWorksheet.Cells[1][current_excel_row].Value2 = "TOTAL/AVERAGE";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 12;
                current_excel_row++;

                //supervisor attendence

                //we need to count how many days there are between the selected dates 
                int supervisor_dates = 0;

                xlWorksheet.Cells[1][current_excel_row].Value2 = " Supervisor Attendence";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                current_excel_row++;

                supervisor_dates = (dteEnd.Value - dteStart.Value).Days + 1;

                DateTime current_supervisor_date = dteStart.Value;
                for (int i = 0; i < supervisor_dates; i++)
                {
                    current_excel_row++;
                    if (current_supervisor_date.DayOfWeek == DayOfWeek.Saturday || current_supervisor_date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        current_supervisor_date = current_supervisor_date.AddDays(1);
                        continue;
                    }

                    sql = "select coalesce([9_30],'Not Pressed'),coalesce([11_30],'Not Pressed'),coalesce([2_30],'Not Pressed'),coalesce([eos],'Not Pressed') " +
                        "from dbo.supervisor_log where supervisor_date = '" + current_supervisor_date.ToString("yyyyMMdd") + "' AND department = '" + department.Replace("ing", "") + "'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                        xlWorksheet.Cells[2][current_excel_row].Value2 = "9:30";
                        xlWorksheet.Cells[3][current_excel_row].Value2 = "11:30";
                        xlWorksheet.Cells[4][current_excel_row].Value2 = "14:30";
                        xlWorksheet.Cells[5][current_excel_row].Value2 = "End of Shift";
                        current_excel_row++;

                        xlWorksheet.Cells[1][current_excel_row].Value2 = current_supervisor_date.ToString("dd/MM/yyyy");

                        if (dt.Rows.Count < 1) //null rows
                        {

                            xlWorksheet.Cells[2][current_excel_row].Value2 = "Not Pressed";
                            xlWorksheet.Cells[3][current_excel_row].Value2 = "Not Pressed";
                            xlWorksheet.Cells[4][current_excel_row].Value2 = "Not Pressed";
                            xlWorksheet.Cells[5][current_excel_row].Value2 = "Not Pressed";
                        }
                        else
                        {
                            xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][0].ToString();
                            if (dt.Rows[0][0].ToString() != "Not Pressed" && dt.Rows[0][0].ToString() != "Nobody" && dt.Rows[0][0].ToString() != "No Management")
                                xlWorksheet.Cells[2][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][0].ToString() == "No Management")
                                xlWorksheet.Cells[2][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;

                            xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                            if (dt.Rows[0][1].ToString() != "Not Pressed" && dt.Rows[0][1].ToString() != "Nobody" && dt.Rows[0][1].ToString() != "No Management")
                                xlWorksheet.Cells[3][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][1].ToString() == "No Management")
                                xlWorksheet.Cells[3][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;

                            xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                            if (dt.Rows[0][2].ToString() != "Not Pressed" && dt.Rows[0][2].ToString() != "Nobody" && dt.Rows[0][2].ToString() != "No Management")
                                xlWorksheet.Cells[4][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][2].ToString() == "No Management")
                                xlWorksheet.Cells[4][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;

                            xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                            if (dt.Rows[0][3].ToString() != "Not Pressed" && dt.Rows[0][3].ToString() != "Nobody" && dt.Rows[0][3].ToString() != "No Management")
                                xlWorksheet.Cells[5][current_excel_row].Interior.Color = System.Drawing.Color.LimeGreen;
                            else if (dt.Rows[0][3].ToString() == "No Management")
                                xlWorksheet.Cells[5][current_excel_row].Interior.Color = System.Drawing.Color.PaleVioletRed;
                        }

                    }
                    current_supervisor_date = current_supervisor_date.AddDays(1);

                }




                xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row, 5]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                Microsoft.Office.Interop.Excel.Worksheet ws = xlApp.ActiveWorkbook.Worksheets[1];
                Microsoft.Office.Interop.Excel.Range range = ws.UsedRange;
                ws.Columns.AutoFit();
                ws.Rows.AutoFit();

                conn.Close();
            }



            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);




            // xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
            }


        }

        private void print_department_dropped_sheet(string department)
        {
            int average_percent = 0;
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\DEPARTMENT_ACTIVITY.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");



            //get all of the distinct staff in the department

            //vvv we need to loop through the staff 
            string sql = "select CAST(date_goal as date) " +
                "from dbo.daily_department_goal where date_goal >= '" + dteStart.Value.ToString("yyyyMMdd") + "' AND date_goal <= '" + dteEnd.Value.ToString("yyyyMMdd") + "'" +
                "group by date_goal order by date_goal";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                DataTable dt_department = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_department);
                }

                int current_excel_row = 1;

                xlWorksheet.Cells[1][current_excel_row].Value2 = department + " Activity";

                current_excel_row++;

                xlWorksheet.Cells[1][current_excel_row].Value2 = "Date";
                xlWorksheet.Cells[2][current_excel_row].Value2 = "Day of Week";
                xlWorksheet.Cells[3][current_excel_row].Value2 = "Hours";
                xlWorksheet.Cells[4][current_excel_row].Value2 = "Actual";
                xlWorksheet.Cells[5][current_excel_row].Value2 = "%";

                //loop for each distinct date
                for (int department_row = 0; department_row < dt_department.Rows.Count; department_row++)
                {

                    //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Bold = true;
                    //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Font.Size = 14;

                    ////MERGE THESE ROWS
                    //xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 5]].Merge();
                    ////insert the staff members name into the excel sheet
                    //xlWorksheet.Cells[1][current_excel_row].Value2 = dt_department.Rows[department_row][0].ToString();

                    //current_excel_row++;

                    //column headers


                    current_excel_row++;

                    //get all the days this staff member is in this department between the selected dates




                    //now we get the day of week/hours/actual/%



                    sql = "select  '" + Convert.ToDateTime(dt_department.Rows[department_row][0]).ToString("dd/MM/yyyy") + "'," +
                       "datename(WEEKDAY,'" + Convert.ToDateTime(dt_department.Rows[department_row][0]).ToString("yyyyMMdd") + "')," +
                       "sum([goal_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + "]) as [hours]," +
                       "sum([actual_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + "]) as actual, " +
                       " coalesce(round(sum(actual_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + ") / nullif(sum(goal_hours" + department.Replace("Punching", "_punch").Replace("Painting", "") + "),0),2),0) " +
                       "from dbo.daily_department_goal where date_goal = '" + Convert.ToDateTime(dt_department.Rows[department_row][0]).ToString("yyyyMMdd") + "' " +
                       "group by date_goal order by date_goal";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        xlWorksheet.Cells[1][current_excel_row].Value2 = dt.Rows[0][0].ToString();
                        xlWorksheet.Cells[2][current_excel_row].Value2 = dt.Rows[0][1].ToString();
                        xlWorksheet.Cells[3][current_excel_row].Value2 = dt.Rows[0][2].ToString();
                        xlWorksheet.Cells[4][current_excel_row].Value2 = dt.Rows[0][3].ToString();
                        xlWorksheet.Cells[5][current_excel_row].Value2 = dt.Rows[0][4].ToString();

                        //add conditional formatting to the last row (%)
                        Excel.FormatCondition formatGreen = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                            Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                               Excel.XlFormatConditionOperator.xlGreaterEqual, 1,
                                               Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing));

                        formatGreen.Font.Bold = true;
                        formatGreen.Font.Color = Color.DarkGreen;
                        formatGreen.Interior.Color = Color.LimeGreen;

                        Excel.FormatCondition formatRed = (Excel.FormatCondition)(xlWorksheet.Range("E" + current_excel_row.ToString(),
                                           Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlCellValue,
                                           Excel.XlFormatConditionOperator.xlLess, 1,
                                           Type.Missing, Type.Missing, Type.Missing,
                                           Type.Missing, Type.Missing));

                        formatRed.Font.Bold = true;
                        formatRed.Font.Color = Color.DarkRed;
                        formatRed.Interior.Color = Color.PaleVioletRed;
                    }


                    average_percent = department_row;

                }
                //average %
                current_excel_row++;
                xlWorksheet.Cells[4][current_excel_row].Value2 = "AVERAGE %";
                xlWorksheet.Cells[5][current_excel_row].Value2 = "=AVERAGE(E" + (current_excel_row - 1 - average_percent).ToString() + ":E" + (current_excel_row - 1).ToString() + ")";
                xlWorksheet.Range[xlWorksheet.Cells[current_excel_row, 1], xlWorksheet.Cells[current_excel_row, 3]].Merge();

                xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row, 5]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                conn.Close();
            }



            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);




            // xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
            }


        }


        private void txtWelding_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtWelding_Click(object sender, EventArgs e)
        {
            print_new_staff_dropped_sheet("Welding");
        }

        private void txtBending_Click(object sender, EventArgs e)
        {
            print_new_staff_dropped_sheet("Bending");
        }

        private void txtBuffing_Click(object sender, EventArgs e)
        {
            print_new_staff_dropped_sheet("Buffing");
        }

        private void txtPacking_Click(object sender, EventArgs e)
        {
            print_new_staff_dropped_sheet("Packing");
        }

        private void txtPunching_Click(object sender, EventArgs e)
        {
            print_department_sheet("Punching");
        }

        private void txtPainting_Click(object sender, EventArgs e)
        {
            print_department_sheet("Painting");
        }

        private void txtBending_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPainting_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPacking_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPunching_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSlimline_MouseClick(object sender, MouseEventArgs e)
        {
            //print_department_sheet("Slimline");
            print_new_staff_dropped_sheet("Slimline");
        }
    }
}

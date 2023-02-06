

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
                         "coalesce(round(sum(actual_hours_pack)  / nullif(sum(goal_hours_pack),0),2),0) * 100 " +
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
                            "select sum(s.[hours]) + sum((ot.overtime * 0.8)) as [hours],0 as actual from dbo.power_plan_staff s " +
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
            print_staff_dropped_sheet("Welding");
        }

        private void txtBending_Click(object sender, EventArgs e)
        {
            print_staff_dropped_sheet("Bending");
        }

        private void txtBuffing_Click(object sender, EventArgs e)
        {
            print_staff_dropped_sheet("Buffing");
        }

        private void txtPacking_Click(object sender, EventArgs e)
        {
            print_staff_dropped_sheet("Packing");
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
    }
}

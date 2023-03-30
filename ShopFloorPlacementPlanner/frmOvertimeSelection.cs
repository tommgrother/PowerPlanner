using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    public partial class frmOvertimeSelection : Form
    {

        public frmOvertimeSelection()
        {
            InitializeComponent();
        }


        private void toggle_buttons(bool value)
        {
            chkPunching.Enabled = value;
            chkBending.Enabled = value;
            chkWelding.Enabled = value;
            ChkBuffing.Enabled = value;
            chkPainting.Enabled = value;
            chkPacking.Enabled = value;
            chkToolroom.Enabled = value;
            chkDispatch.Enabled = value;
            chkStores.Enabled = value;
            chkSlimline.Enabled = value;
            btnPrint.Enabled = value;
            btnAll.Enabled = value;
            dteDate.Enabled = value;
            btnSupervisorSheet.Enabled = value;
            btnSummary.Enabled = value;

            if (value == true)
            {
                chkPunching.Checked = false;
                chkBending.Checked = false;
                chkWelding.Checked = false;
                ChkBuffing.Checked = false;
                chkPainting.Checked = false;
                chkPacking.Checked = false;
                chkToolroom.Checked = false;
                chkDispatch.Checked = false;
                chkStores.Checked = false;
                chkSlimline.Checked = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            toggle_buttons(false);
            if (chkPunching.Checked)
            {
                printSheet("PUNCHING");
                printSheet("LASER");
            }
            if (chkBending.Checked)
                printSheet("BENDING");
            if (chkWelding.Checked)
                printSheet("WELDING");
            if (ChkBuffing.Checked)
                printSheet("DRESSING");
            if (chkPainting.Checked)
                printSheet("PAINTING");
            if (chkPacking.Checked)
                printSheet("PACKING");
            if (chkToolroom.Checked)
                printSheet("toolroom");
            if (chkDispatch.Checked)
                printSheet("Dispatch");
            if (chkStores.Checked)
                printSheet("Stores");
            if (chkSlimline.Checked)
                printSheet("Slimline");

            toggle_buttons(true);

            MessageBox.Show("Overtime Sheets printed!", "Default Printer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printSheet(string department)
        {
            //get the MONDAY and the FRIDAY of this week
            DateTime date = Convert.ToDateTime(dteDate.Value);
            DateTime Monday = new DateTime();
            DateTime Friday = new DateTime();
            while (date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);
            Monday = date;
            //get end of week
            Friday = date.AddDays(4);

            //now we just need the finishing touches ¬   finishing touches even tho we are at the start of the button press :p
            //the name of the day and date for each one
            string mondaySTR = "", tuesdaySTR = "", wednesdaySTR = "", thursdaySTR = "", fridaySTR = "", saturdaySTR = "", sundaySTR = "", fileName = "";
            fileName = date.ToShortDateString();
            fileName = fileName.Replace("/", "-");
            mondaySTR = "MON - " + Monday.ToString("dd/MM");
            DateTime stringDate = Monday.AddDays(0);
            tuesdaySTR = "TUE - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(1);
            tuesdaySTR = "TUE - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(2);
            wednesdaySTR = "WED - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(3);
            thursdaySTR = "THUR - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(4);
            fridaySTR = "FRI - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(5);
            saturdaySTR = "SAT - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(6);
            sundaySTR = "SUN - " + stringDate.ToString("dd/MM");



            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\OVERTIME_SHEET.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //[row][column]
                xlWorksheet.Cells[1][1].Value2 = department + " OVERTIME";
                xlWorksheet.Cells[2][2].Value2 = mondaySTR;
                xlWorksheet.Cells[4][2].Value2 = tuesdaySTR;
                xlWorksheet.Cells[6][2].Value2 = wednesdaySTR;
                xlWorksheet.Cells[8][2].Value2 = thursdaySTR;
                xlWorksheet.Cells[10][2].Value2 = fridaySTR;
                xlWorksheet.Cells[12][2].Value2 = saturdaySTR;
                xlWorksheet.Cells[13][2].Value2 = sundaySTR;

                //GET everyone 
                //vv OLD STRING
                string sql = "select distinct forename + ' ' + surname from dbo.power_plan_staff s " +
                    "left join dbo.power_plan_date d on d.id = s.date_id " +
                    "LEFT JOIN[user_info].dbo.[user] u on u.id = s.staff_id " +
                    "where (u.non_user = 0 or u.non_user is null) AND S.department = '" + department + "'" +
                    "AND date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "' and u.[current] = 1";

                //new ugly string
                ////string sql = "Select distinct forename + ' ' + surname as fullname from [user_info].dbo.[user] where " +
                ////    "([actual_department] = '" + department + "' or[allocation_dept_2] = '" + department + "' or[allocation_dept_3] = '" + department + "' or " +
                ////    "[allocation_dept_4] = '" + department + "' or[allocation_dept_5] = '" + department + "' or[allocation_dept_6] = '" + department + "' ) and " +
                ////    "[current] = 1 AND  (non_user = 0 or non_user is null) order by fullname";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int staff_row = 4;
                    foreach (DataRow row in dt.Rows)
                    {
                        xlWorksheet.Cells[1][staff_row].Value2 = row[0].ToString();
                        //also work out if that this staff member is absent etc
                        string absent_sql = "select max(monday),max(monday2),max(tuesday),max(tuesday2),max(wednesday),max(wednesday2),max(thursday),max(thursday2),max(friday),max(friday2) from (select  " +
                            "case when date_absent = '" + Monday.ToString("yyyyMMdd") + "' AND absent_type is not null then 'X' else '' end as monday," +
                            "case when date_absent = '" + Monday.ToString("yyyyMMdd") + "' AND absent_type is not null then 'X' else '' end as monday2, " +
                            "case when date_absent = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as tuesday, " +
                            "case when date_absent = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as tuesday2, " +
                            "case when date_absent = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as wednesday, " +
                            "case when date_absent = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as wednesday2, " +
                            "case when date_absent = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as thursday, " +
                            "case when date_absent = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as thursday2, " +
                            "case when date_absent = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as friday, " +
                            "case when date_absent = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as friday2 " +
                            "from dbo.absent_holidays h left join [user_info].dbo.[user] u on h.staff_id = u.id " +
                            "where forename +' ' + surname = '" + row[0].ToString() + "' and date_absent >= '" + Monday.ToString("yyyyMMdd") + "' AND date_absent <= '" + Friday.ToString("yyyyMMdd") + "') as a";
                        using (SqlCommand absent_cmd = new SqlCommand(absent_sql, conn))
                        {
                            SqlDataAdapter da2 = new SqlDataAdapter(absent_cmd);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            int absent_column = 2;
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][0].ToString();//mon
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][1].ToString(); //mon
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][2].ToString();//tues
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][3].ToString();//tues
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][4].ToString();//wed
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][5].ToString();//wed
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][6].ToString();//thur
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][7].ToString();//thur
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][8].ToString();//fri
                                absent_column = absent_column + 1;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][9].ToString();//fri
                            }
                        }

                        staff_row++;
                    }
                }

                //border them all
                Excel.Range xlRange = xlWorksheet.UsedRange;
                xlRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                //autosize the first row
                xlWorksheet.Columns.AutoFit();

                //make the x's middle
                Microsoft.Office.Interop.Excel.Range RangeYour = xlWorksheet.Range["B4:k100"];
                RangeYour.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RangeYour.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //print it
                xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                xlWorkbook.Close(false); //close the excel sheet without saving
                                         // xlApp.Quit();


                // Manual disposal because of COM
                xlApp.Quit();

                // Now find the process id that was created, and store it.
                int processID = 0;
                foreach (Process process in processesAfter)
                {
                    if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    {
                        processID = process.Id;
                    }
                }

                // And now kill the process.
                if (processID != 0)
                {
                    Process process = Process.GetProcessById(processID);
                    process.Kill();
                }
                conn.Close();
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (chkPunching.Checked == true)
            {
                chkPunching.Checked = false;
                chkBending.Checked = false;
                chkWelding.Checked = false;
                ChkBuffing.Checked = false;
                chkPainting.Checked = false;
                chkPacking.Checked = false;
                chkToolroom.Checked = false;
                chkDispatch.Checked = false;
                chkStores.Checked = false;
                chkSlimline.Checked = false;
            }
            else
            {
                chkPunching.Checked = true;
                chkBending.Checked = true;
                chkWelding.Checked = true;
                ChkBuffing.Checked = true;
                chkPainting.Checked = true;
                chkPacking.Checked = true;
                chkToolroom.Checked = true;
                chkDispatch.Checked = true;
                chkStores.Checked = true;
                chkSlimline.Checked = true;
            }

            //toggle_buttons(false);
            //printSheet("PUNCHING");
            //printSheet("LASER");
            //printSheet("BENDING");
            //printSheet("WELDING");
            //printSheet("DRESSING");
            //printSheet("PAINTING");
            //printSheet("PACKING");
            //printSheet("toolroom");
            //printSheet("Dispatch");
            //printSheet("Stores");
            //toggle_buttons(true);
            //MessageBox.Show("Overtime Sheets printed!", "Default Printer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            //get the MONDAY and the FRIDAY of this week
            DateTime date = Convert.ToDateTime(dteDate.Value);
            DateTime Monday = new DateTime();
            DateTime Friday = new DateTime();
            while (date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);
            Monday = date;
            //get end of week
            Friday = date.AddDays(6);

            //now we just need the finishing touches ¬   finishing touches even tho we are at the start of the button press :p
            //the name of the day and date for each one
            string mondaySTR = "", tuesdaySTR = "", wednesdaySTR = "", thursdaySTR = "", fridaySTR = "", saturdaySTR = "", sundaySTR = "", fileName = "";
            fileName = date.ToShortDateString();
            fileName = fileName.Replace("/", "-");
            mondaySTR = "MON - " + Monday.ToString("dd/MM");
            DateTime stringDate = Monday.AddDays(0);
            tuesdaySTR = "TUE - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(1);
            tuesdaySTR = "TUE - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(2);
            wednesdaySTR = "WED - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(3);
            thursdaySTR = "THUR - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(4);
            fridaySTR = "FRI - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(5);
            saturdaySTR = "SAT - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(6);
            sundaySTR = "SUN - " + stringDate.ToString("dd/MM");



            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\OVERTIME_SHEET_SUMMARY_remake.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //[row][column]
                xlWorksheet.Cells[1][1].Value2 = "PLANNED OVERTIME";
                xlWorksheet.Cells[2][2].Value2 = mondaySTR;
                xlWorksheet.Cells[4][2].Value2 = tuesdaySTR;
                xlWorksheet.Cells[6][2].Value2 = wednesdaySTR;
                xlWorksheet.Cells[8][2].Value2 = thursdaySTR;
                xlWorksheet.Cells[10][2].Value2 = fridaySTR;
                xlWorksheet.Cells[12][2].Value2 = saturdaySTR;
                xlWorksheet.Cells[14][2].Value2 = sundaySTR;

                //GET everyone 
                //vv OLD STRING
                string sql = "select distinct forename + ' ' + surname from dbo.power_plan_staff s " +
                    "left join dbo.power_plan_date d on d.id = s.date_id " +
                    "LEFT JOIN[user_info].dbo.[user] u on u.id = s.staff_id " +
                     "left join dbo.power_plan_overtime_remake ot on s.staff_id = ot.staff_id AND s.date_id = ot.date_id " +
                    "where (u.non_user = 0 or u.non_user is null) AND ot.overtime > 0 " +
                    "AND date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "' and u.[current] = 1";

                //new ugly string
                ////string sql = "Select distinct forename + ' ' + surname as fullname from [user_info].dbo.[user] where " +
                ////    "([actual_department] = '" + department + "' or[allocation_dept_2] = '" + department + "' or[allocation_dept_3] = '" + department + "' or " +
                ////    "[allocation_dept_4] = '" + department + "' or[allocation_dept_5] = '" + department + "' or[allocation_dept_6] = '" + department + "' ) and " +
                ////    "[current] = 1 AND  (non_user = 0 or non_user is null) order by fullname";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int staff_row = 4;
                    foreach (DataRow row in dt.Rows)
                    {
                        xlWorksheet.Cells[1][staff_row].Value2 = row[0].ToString();
                        //also work out if that this staff member is absent etc
                        string absent_sql = "select " +
                                            "case when max(monday_am) = '0' then '' else max(monday_am) end, " +
                                            "case when max(monday_pm) = '0' then '' else max(monday_pm) end, " +
                                            "case when max(tuesday_am) = '0' then '' else max(tuesday_am) end, " +
                                            "case when max(tuesday_pm) = '0' then '' else max(tuesday_pm) end, " +
                                            "case when max(wednesday_am) = '0' then '' else max(wednesday_am) end, " +
                                            "case when max(wednesday_pm) = '0' then '' else max(wednesday_pm) end, " +
                                            "case when max(thursday_am) = '0' then '' else max(thursday_am) end, " +
                                            "case when max(thursday_pm) = '0' then '' else max(thursday_pm) end, " +
                                            "case when max(friday_am) = '0' then '' else max(friday_am) end, " +
                                            "case when max(friday_pm) = '0' then '' else max(friday_pm) end, " +
                                            "case when max(saturday_am) = '0' then '' else max(saturday_am) end, " +
                                            "case when max(saturday_pm) = '0' then '' else max(saturday_pm) end, " +
                                            "case when max(sunday_am)  = '0' then '' else max(sunday_am) end, " +
                                            "case when max(sunday_pm)  = '0' then '' else max(sunday_pm) end " +
                                            "from(select " +
                                              "cast(case when date_plan = '" + Monday.ToString("yyyyMMdd") + "' AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as monday_am," +
                                              "cast(case when date_plan = '" + Monday.ToString("yyyyMMdd") + "' AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as monday_pm, " +
                                              "cast(case when date_plan = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as tuesday_am," +
                                              "cast(case when date_plan = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as tuesday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as wednesday_am," +
                                              "cast(case when date_plan = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as wednesday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as thursday_am, " +
                                              "cast(case when date_plan = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as thursday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as friday_am," +
                                              "cast(case when date_plan = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as friday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 5, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as saturday_am, " +
                                              "cast(case when date_plan = DATEADD(day, 5, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as saturday_pm, " +
                                              "cast(case when date_plan = DATEADD(day, 6, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as sunday_am," +
                                              "cast(case when date_plan = DATEADD(day, 6, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as sunday_pm " +
                                              "from dbo.power_plan_overtime_remake r " +
                                              "left join dbo.power_plan_overtime_remake_am_pm am_pm on r.id = am_pm.overtime_remake_id " +
                                              "left join dbo.power_plan_date d on r.date_id = d.id " +
                                              "left join [user_info].dbo.[user] u on r.staff_id = u.id " + //Monday.ToString("yyyyMMdd")
                                              "where forename + ' ' + surname = '" + row[0].ToString() + "' and " +
                                              "date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "') as a";
                        using (SqlCommand absent_cmd = new SqlCommand(absent_sql, conn))
                        {
                            SqlDataAdapter da2 = new SqlDataAdapter(absent_cmd);
                            System.Data.DataTable dt2 = new System.Data.DataTable();
                            da2.Fill(dt2);

                            dt2.Columns.Add("Total", typeof(System.String));
                            foreach (DataRow total in dt2.Rows)
                            {
                                //need to set value to NewColumn column
                                double total_ot = 0;
                                if (string.IsNullOrEmpty(total[0].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[0].ToString());
                                if (string.IsNullOrEmpty(total[1].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[1].ToString());
                                if (string.IsNullOrEmpty(total[2].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[2].ToString());
                                if (string.IsNullOrEmpty(total[3].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[3].ToString());
                                if (string.IsNullOrEmpty(total[4].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[4].ToString());
                                if (string.IsNullOrEmpty(total[5].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[5].ToString());
                                if (string.IsNullOrEmpty(total[6].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[6].ToString());
                                if (string.IsNullOrEmpty(total[7].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[7].ToString());
                                if (string.IsNullOrEmpty(total[8].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[8].ToString());
                                if (string.IsNullOrEmpty(total[9].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[9].ToString());
                                if (string.IsNullOrEmpty(total[10].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[10].ToString());
                                if (string.IsNullOrEmpty(total[11].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[11].ToString());
                                if (string.IsNullOrEmpty(total[12].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[12].ToString());
                                if (string.IsNullOrEmpty(total[13].ToString()) == false)
                                    total_ot = total_ot + Convert.ToDouble(total[13].ToString());


                                total["Total"] = total_ot.ToString();   // or set it to some other value
                            }

                            int absent_column = 2;
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][0].ToString();//mon am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][1].ToString();//mon pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][2].ToString();//tues am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][3].ToString();//tues pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][4].ToString();//wed am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][5].ToString();//wed pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][6].ToString();//thur am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][7].ToString();//thur pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][8].ToString();//fri am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][9].ToString();//fri pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][10].ToString();//sat am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][11].ToString();//sat pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][12].ToString();//sun am
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][13].ToString();//sun pm
                                absent_column++;
                                xlWorksheet.Cells[absent_column][staff_row].Value2 = dt2.Rows[0][14].ToString();//total OT
                            }
                        }
                        staff_row++;
                    }
                }

                //border them all
                Excel.Range xlRange = xlWorksheet.UsedRange;
                xlRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                //autosize the first row
                xlWorksheet.Columns["A"].AutoFit();

                //make the x's middle
                Microsoft.Office.Interop.Excel.Range RangeYour = xlWorksheet.Range["B4:k100"];
                RangeYour.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RangeYour.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //print it
                //pageSetup.PrintArea = "A1:J100";
                // This will force the workbook to be printed on one page.
                Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
                xlPageSetUp.Zoom = false;
                xlPageSetUp.FitToPagesWide = 1;
                //xlPageSetUp.FitToPagesTall= 2

                xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                xlWorkbook.Close(false); //close the excel sheet without saving
                                         // xlApp.Quit();


                // Manual disposal because of COM
                xlApp.Quit();

                // Now find the process id that was created, and store it.
                int processID = 0;
                foreach (Process process in processesAfter)
                {
                    if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    {
                        processID = process.Id;
                    }
                }

                // And now kill the process.
                if (processID != 0)
                {
                    Process process = Process.GetProcessById(processID);
                    process.Kill();
                }
                conn.Close();
            }
            MessageBox.Show("Print out has been sent to your default printer.", "Printed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSupervisorSheet_Click(object sender, EventArgs e)
        {
            login.errorList.Clear();

            toggle_buttons(false);
            if (chkPunching.Checked)
            {
                supervisor_print("PUNCHING");
                supervisor_print("LASER");
            }
            if (chkBending.Checked)
                supervisor_print("BENDING");
            if (chkWelding.Checked)
                supervisor_print("WELDING");
            if (ChkBuffing.Checked)
                supervisor_print("DRESSING");
            if (chkPainting.Checked)
                supervisor_print("PAINTING");
            if (chkPacking.Checked)
                supervisor_print("PACKING");
            if (chkToolroom.Checked)
                supervisor_print("toolroom");
            if (chkDispatch.Checked)
                supervisor_print("Dispatch");
            if (chkStores.Checked)
                supervisor_print("Stores");
    
            toggle_buttons(true);

            //supervisor_print("LASER");
            //supervisor_print("PUNCHING");
            //supervisor_print("BENDING");
            //supervisor_print("WELDING");
            //supervisor_print("DRESSING");
            //supervisor_print("PAINTING");
            //supervisor_print("PACKING");
            //supervisor_print("TOOLROOM");
            //supervisor_print("DISPATCH");
            //supervisor_print("STORES");

            if (login.errorList.Count > 0)
            {
                string note = "The following departments have no overtime planned:- ";
                for (int i = 0; i < login.errorList.Count; i++)
                    note = note + Environment.NewLine + login.errorList[i].ToString();

                note = note + Environment.NewLine + " The other sheets have been sent to your default printer";
                MessageBox.Show(note, "Error List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Supervisor sheets have been sent to your default printer", "Printed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void supervisor_print(string department)
        {

            //get the MONDAY and the FRIDAY of this week
            DateTime date = Convert.ToDateTime(dteDate.Value);
            DateTime Monday = new DateTime();
            DateTime Friday = new DateTime();
            while (date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);
            Monday = date;
            //get end of week
            Friday = date.AddDays(6);

            //now we just need the finishing touches ¬   finishing touches even tho we are at the start of the button press :p
            //the name of the day and date for each one
            string mondaySTR = "", tuesdaySTR = "", wednesdaySTR = "", thursdaySTR = "", fridaySTR = "", saturdaySTR = "", sundaySTR = "", fileName = "";
            fileName = date.ToShortDateString();
            fileName = fileName.Replace("/", "-");
            mondaySTR = "MON - " + Monday.ToString("dd/MM");
            DateTime stringDate = Monday.AddDays(0);
            tuesdaySTR = "TUE - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(1);
            tuesdaySTR = "TUE - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(2);
            wednesdaySTR = "WED - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(3);
            thursdaySTR = "THUR - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(4);
            fridaySTR = "FRI - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(5);
            saturdaySTR = "SAT - " + stringDate.ToString("dd/MM");
            stringDate = Monday.AddDays(6);
            sundaySTR = "SUN - " + stringDate.ToString("dd/MM");



            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\OVERTIME_SUPERVISOR_SHEET.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //[row][column]

                //quickly loop through the supervisors and concat them onto the end of the below [1][1] cell
                string sql = "select distinct forename + ' ' + surname from dbo.power_plan_staff s " +
                    "left join dbo.power_plan_date d on d.id = s.date_id " +
                    "LEFT JOIN[user_info].dbo.[user] u on u.id = s.staff_id " +
                     "left join dbo.power_plan_overtime_remake ot on s.staff_id = ot.staff_id AND s.date_id = ot.date_id " +
                    "where (u.non_user = 0 or u.non_user is null) AND s.department = '" + department + "' AND ot.department = '" + department + "' " +
                    "AND date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "'  and u.supervisor = -1 ";

                string supervisor = "";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                            supervisor = supervisor + dt.Rows[i][0].ToString();
                        else
                            supervisor = supervisor + " / " + dt.Rows[i][0].ToString();
                    }


                }

                xlWorksheet.Cells[1][1].Value2 = "PLANNED " + department.Replace("Dressing", "BUFFING") + " OVERTIME - SUPERVISOR: " + supervisor;
                xlWorksheet.Cells[2][2].Value2 = mondaySTR;
                xlWorksheet.Cells[4][2].Value2 = tuesdaySTR;
                xlWorksheet.Cells[6][2].Value2 = wednesdaySTR;
                xlWorksheet.Cells[8][2].Value2 = thursdaySTR;
                xlWorksheet.Cells[10][2].Value2 = fridaySTR;
                xlWorksheet.Cells[12][2].Value2 = saturdaySTR;
                xlWorksheet.Cells[14][2].Value2 = sundaySTR;

                //GET everyone 
                //vv OLD STRING
                sql = "select distinct forename + ' ' + surname from dbo.power_plan_staff s " +
                    "left join dbo.power_plan_date d on d.id = s.date_id " +
                    "LEFT JOIN[user_info].dbo.[user] u on u.id = s.staff_id " +
                     "left join dbo.power_plan_overtime_remake ot on s.staff_id = ot.staff_id AND s.date_id = ot.date_id " +
                    "where (u.non_user = 0 or u.non_user is null) AND s.department = '" + department + "' AND ot.department = '" + department + "' " +
                    "AND date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "' and u.[current] = 1";

                //new ugly string
                ////string sql = "Select distinct forename + ' ' + surname as fullname from [user_info].dbo.[user] where " +
                ////    "([actual_department] = '" + department + "' or[allocation_dept_2] = '" + department + "' or[allocation_dept_3] = '" + department + "' or " +
                ////    "[allocation_dept_4] = '" + department + "' or[allocation_dept_5] = '" + department + "' or[allocation_dept_6] = '" + department + "' ) and " +
                ////    "[current] = 1 AND  (non_user = 0 or non_user is null) order by fullname";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    //if there is no rows we need to error out 
                    if (dt.Rows.Count < 1)
                    {
                        login.errorList.Add(department.Replace("Dressing", "Buffing"));

                        //exit excel
                        // Manual disposal because of COM
                        xlApp.Quit();

                        // Now find the process id that was created, and store it.
                        int processID2 = 0;
                        foreach (Process process in processesAfter)
                        {
                            if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                            {
                                processID2 = process.Id;
                            }
                        }

                        // And now kill the process.
                        if (processID2 != 0)
                        {
                            Process process = Process.GetProcessById(processID2);
                            process.Kill();
                        }
                        return;
                    }


                    int staff_row = 4;
                    foreach (DataRow row in dt.Rows)
                    {
                        xlWorksheet.Cells[1][staff_row].Value2 = row[0].ToString();
                        string data_sql = "select " +
                                            "case when max(monday_am) = '0' then '' else max(monday_am) end, " +
                                            "case when max(monday_pm) = '0' then '' else max(monday_pm) end, " +
                                            "case when max(tuesday_am) = '0' then '' else max(tuesday_am) end, " +
                                            "case when max(tuesday_pm) = '0' then '' else max(tuesday_pm) end, " +
                                            "case when max(wednesday_am) = '0' then '' else max(wednesday_am) end, " +
                                            "case when max(wednesday_pm) = '0' then '' else max(wednesday_pm) end, " +
                                            "case when max(thursday_am) = '0' then '' else max(thursday_am) end, " +
                                            "case when max(thursday_pm) = '0' then '' else max(thursday_pm) end, " +
                                            "case when max(friday_am) = '0' then '' else max(friday_am) end, " +
                                            "case when max(friday_pm) = '0' then '' else max(friday_pm) end, " +
                                            "case when max(saturday_am) = '0' then '' else max(saturday_am) end, " +
                                            "case when max(saturday_pm) = '0' then '' else max(saturday_pm) end, " +
                                            "case when max(sunday_am)  = '0' then '' else max(sunday_am) end, " +
                                            "case when max(sunday_pm)  = '0' then '' else max(sunday_pm) end " +
                                            "from(select " +
                                              "cast(case when date_plan = '" + Monday.ToString("yyyyMMdd") + "' AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as monday_am," +
                                              "cast(case when date_plan = '" + Monday.ToString("yyyyMMdd") + "' AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as monday_pm, " +
                                              "cast(case when date_plan = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as tuesday_am," +
                                              "cast(case when date_plan = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as tuesday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as wednesday_am," +
                                              "cast(case when date_plan = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as wednesday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as thursday_am, " +
                                              "cast(case when date_plan = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as thursday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as friday_am," +
                                              "cast(case when date_plan = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as friday_pm," +
                                              "cast(case when date_plan = DATEADD(day, 5, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as saturday_am, " +
                                              "cast(case when date_plan = DATEADD(day, 5, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as saturday_pm, " +
                                              "cast(case when date_plan = DATEADD(day, 6, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.am > 0 then am_pm.am else '' end as nvarchar) as sunday_am," +
                                              "cast(case when date_plan = DATEADD(day, 6, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND am_pm.pm > 0 then am_pm.pm else '' end as nvarchar) as sunday_pm " +
                                              "from dbo.power_plan_overtime_remake r " +
                                              "left join dbo.power_plan_overtime_remake_am_pm am_pm on r.id = am_pm.overtime_remake_id " +
                                              "left join dbo.power_plan_date d on r.date_id = d.id " +
                                              "left join [user_info].dbo.[user] u on r.staff_id = u.id " + //Monday.ToString("yyyyMMdd")
                                              "where forename +' ' + surname = '" + row[0].ToString() + "' AND r.department = '" + department + "' and date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "') as a";
                        using (SqlCommand data_cmd = new SqlCommand(data_sql, conn))
                        {
                            SqlDataAdapter da2 = new SqlDataAdapter(data_cmd);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);

                            dt2.Columns.Add("Total", typeof(System.String));
                            foreach (DataRow total in dt2.Rows)
                            {
                                //need to set value to NewColumn column
                                //double total_ot = 0;
                                //if (string.IsNullOrEmpty(total[0].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[0].ToString());
                                //if (string.IsNullOrEmpty(total[1].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[1].ToString());
                                //if (string.IsNullOrEmpty(total[2].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[2].ToString());
                                //if (string.IsNullOrEmpty(total[3].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[3].ToString());
                                //if (string.IsNullOrEmpty(total[4].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[4].ToString());
                                //if (string.IsNullOrEmpty(total[5].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[5].ToString());
                                //if (string.IsNullOrEmpty(total[6].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[6].ToString());
                                //if (string.IsNullOrEmpty(total[7].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[7].ToString());
                                //if (string.IsNullOrEmpty(total[8].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[8].ToString());
                                //if (string.IsNullOrEmpty(total[9].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[9].ToString());
                                //if (string.IsNullOrEmpty(total[10].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[10].ToString());
                                //if (string.IsNullOrEmpty(total[11].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[11].ToString());
                                //if (string.IsNullOrEmpty(total[12].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[12].ToString());
                                //if (string.IsNullOrEmpty(total[13].ToString()) == false)
                                //    //total_ot = total_ot + Convert.ToDouble(total[13].ToString());


                                total["Total"] = "=(SUM(B" + staff_row + ":O" + staff_row + "))";   // or set it to some other value
                            }

                            //handle absents here
                            foreach (DataRow absent_row in dt.Rows)
                            {
                                xlWorksheet.Cells[1][staff_row].Value2 = row[0].ToString();
                                //also work out if that this staff member is absent etc
                                string absent_sql = "select max(monday),max(monday2),max(tuesday),max(tuesday2),max(wednesday),max(wednesday2),max(thursday),max(thursday2),max(friday),max(friday2) from (select  " +
                                    "case when date_absent = '" + Monday.ToString("yyyyMMdd") + "' AND absent_type is not null then 'X' else '' end as monday," +
                                    "case when date_absent = '" + Monday.ToString("yyyyMMdd") + "' AND absent_type is not null then 'X' else '' end as monday2, " +
                                    "case when date_absent = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as tuesday, " +
                                    "case when date_absent = DATEADD(day, 1, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as tuesday2, " +
                                    "case when date_absent = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as wednesday, " +
                                    "case when date_absent = DATEADD(day, 2, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as wednesday2, " +
                                    "case when date_absent = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as thursday, " +
                                    "case when date_absent = DATEADD(day, 3, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as thursday2, " +
                                    "case when date_absent = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as friday, " +
                                    "case when date_absent = DATEADD(day, 4, cast('" + Monday.ToString("yyyyMMdd") + "' as date)) AND absent_type is not null then 'X' else '' end as friday2 " +
                                    "from dbo.absent_holidays h left join [user_info].dbo.[user] u on h.staff_id = u.id " +
                                    "where forename +' ' + surname = '" + row[0].ToString() + "' and date_absent >= '" + Monday.ToString("yyyyMMdd") + "' AND date_absent <= '" + Friday.ToString("yyyyMMdd") + "') as a";
                                using (SqlCommand absent_cmd = new SqlCommand(absent_sql, conn))
                                {
                                    SqlDataAdapter absent_da = new SqlDataAdapter(absent_cmd);
                                    DataTable absent_dt = new DataTable();
                                    absent_da.Fill(absent_dt);
                                    int absent_column = 2;
                                    for (int i = 0; i < absent_dt.Rows.Count; i++)
                                    {
                                        //xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][0].ToString();//mon
                                        if (absent_dt.Rows[0][0].ToString() == "X")
                                            dt2.Rows[0][0] = absent_dt.Rows[0][0].ToString();//mon

                                        if (absent_dt.Rows[0][1].ToString() == "X")
                                            dt2.Rows[0][1] = absent_dt.Rows[0][1].ToString();//mon

                                        if (absent_dt.Rows[0][2].ToString() == "X")
                                            dt2.Rows[0][2] = absent_dt.Rows[0][2].ToString();//tue
                                        if (absent_dt.Rows[0][3].ToString() == "X")
                                            dt2.Rows[0][3] = absent_dt.Rows[0][3].ToString();//tue
                                        if (absent_dt.Rows[0][4].ToString() == "X")
                                            dt2.Rows[0][4] = absent_dt.Rows[0][4].ToString();//wed
                                        if (absent_dt.Rows[0][5].ToString() == "X")
                                            dt2.Rows[0][5] = absent_dt.Rows[0][5].ToString();//wed

                                        if (absent_dt.Rows[0][6].ToString() == "X")
                                            dt2.Rows[0][6] = absent_dt.Rows[0][6].ToString();//thur
                                        if (absent_dt.Rows[0][7].ToString() == "X")
                                            dt2.Rows[0][7] = absent_dt.Rows[0][7].ToString();//thur

                                        if (absent_dt.Rows[0][8].ToString() == "X")
                                            dt2.Rows[0][8] = absent_dt.Rows[0][8].ToString();//fri
                                        if (absent_dt.Rows[0][9].ToString() == "X")
                                            dt2.Rows[0][9] = absent_dt.Rows[0][9].ToString();//fri

                                        //vv cant be absent for the weekend
                                        ////if (absent_dt.Rows[0][10].ToString() == "X")
                                        ////    dt2.Rows[0][10] = absent_dt.Rows[0][10].ToString();//sat
                                        ////if (absent_dt.Rows[0][11].ToString() == "X")
                                        ////    dt2.Rows[0][11] = absent_dt.Rows[0][11].ToString();//sat

                                        ////if (absent_dt.Rows[0][12].ToString() == "X")
                                        ////    dt2.Rows[0][12] = absent_dt.Rows[0][12].ToString();//sun
                                        ////if (absent_dt.Rows[0][2].ToString() == "X")
                                        ////    dt2.Rows[0][13] = absent_dt.Rows[0][13].ToString();//sun

                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][1].ToString(); //mon
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][2].ToString();//tues
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][3].ToString();//tues
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][4].ToString();//wed
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][5].ToString();//wed
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][6].ToString();//thur
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][7].ToString();//thur
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][8].ToString();//fri
                                        ////absent_column = absent_column + 1;
                                        ////xlWorksheet.Cells[absent_column][staff_row].Value2 = absent_dt.Rows[0][9].ToString();//fri
                                    }
                                }
                            }


                            int data_column = 2;
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][0].ToString();//mon am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][1].ToString();//mon pm
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][2].ToString();//tues am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][3].ToString();//tues pm
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][4].ToString();//wed am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][5].ToString();//wed pm
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][6].ToString();//thur am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][7].ToString();//thur pm
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][8].ToString();//fri am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][9].ToString();//fri pm
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][10].ToString();//sat am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][11].ToString();//sat pm
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][12].ToString();//sun am
                                data_column++;
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][13].ToString();//sun pm
                                data_column++;
                                // MessageBox.Show(dt2.Rows[0][14].ToString());
                                xlWorksheet.Cells[data_column][staff_row].Value2 = dt2.Rows[0][14].ToString();//total OT
                            }
                        }
                        staff_row++;
                    }
                    //add the sum for all the rows
                    ////staff_row = staff_row - 1;
                    xlWorksheet.Cells[1][staff_row].Value2 = "Totals:";
                    xlWorksheet.Cells[2][staff_row].Value2 = "=SUM(B4:C" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[4][staff_row].Value2 = "=SUM(D4:E" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[6][staff_row].Value2 = "=SUM(F4:G" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[8][staff_row].Value2 = "=SUM(H4:I" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[10][staff_row].Value2 = "=SUM(J4:K" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[12][staff_row].Value2 = "=SUM(L4:M" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[14][staff_row].Value2 = "=SUM(N4:O" + (staff_row - 1).ToString() + ")";
                    xlWorksheet.Cells[16][staff_row].Value2 = "=SUM(P4:P" + (staff_row - 1).ToString() + ")";

                    //merge
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 2], xlWorksheet.Cells[staff_row, 3]].Merge();
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 4], xlWorksheet.Cells[staff_row, 5]].Merge();
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 6], xlWorksheet.Cells[staff_row, 7]].Merge();
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 8], xlWorksheet.Cells[staff_row, 9]].Merge();
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 10], xlWorksheet.Cells[staff_row, 11]].Merge();
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 12], xlWorksheet.Cells[staff_row, 13]].Merge();
                    xlWorksheet.Range[xlWorksheet.Cells[staff_row, 14], xlWorksheet.Cells[staff_row, 15]].Merge();
                }

                //border them all
                Excel.Range xlRange = xlWorksheet.UsedRange;
                xlRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                //autosize the first row
                xlWorksheet.Columns["A"].AutoFit();

                //make the x's middle
                Microsoft.Office.Interop.Excel.Range RangeYour = xlWorksheet.Range["B4:P100"];
                RangeYour.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RangeYour.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //print it
                Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
                xlPageSetUp.Zoom = false;
                xlPageSetUp.FitToPagesWide = 1;

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
                    {
                        processID = process.Id;
                    }
                }

                // And now kill the process.
                if (processID != 0)
                {
                    Process process = Process.GetProcessById(processID);
                    process.Kill();
                }
                conn.Close();
            }
        }
    }
}
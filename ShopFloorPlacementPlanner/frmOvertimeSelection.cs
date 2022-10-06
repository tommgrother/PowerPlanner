﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

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
            btnPrint.Enabled = value;
            btnAll.Enabled = value;
            dteDate.Enabled = value;
            if (value == true)
            {
                chkPunching.Checked = false;
                chkBending.Checked = false;
                chkWelding.Checked = false;
                ChkBuffing.Checked = false;
                chkPainting.Checked = false;
                chkPacking.Checked = false;
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
            string mondaySTR = "", tuesdaySTR = "", wednesdaySTR = "", thursdaySTR = "", fridaySTR = "",saturdaySTR = "",sundaySTR = "", fileName = "";
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

                string sql = "select distinct forename + ' ' + surname from dbo.power_plan_staff s " +
                    "left join dbo.power_plan_date d on d.id = s.date_id " +
                    "LEFT JOIN[user_info].dbo.[user] u on u.id = s.staff_id " +
                    "where(u.non_user = 0 or u.non_user is null) AND S.department = '" + department + "'" +
                    "AND date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Friday.ToString("yyyyMMdd") + "'";

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
                            for (int i = 0; i < dt2.Rows.Count;i++)
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
            toggle_buttons(false);
            printSheet("PUNCHING");
            printSheet("LASER");
            printSheet("BENDING");
            printSheet("WELDING");
            printSheet("DRESSING");
            printSheet("PAINTING");
            printSheet("PACKING");
            toggle_buttons(true);
            MessageBox.Show("Overtime Sheets printed!", "Default Printer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
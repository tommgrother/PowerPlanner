using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    public partial class frmProductivity : Form
    {
        public int skip_combo_box { get; set; }
        public frmProductivity() //productivity_email 
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            //fill combobox
            string sql = "SELECT [forename] + ' ' + [surname] AS full_name FROM dbo.[user] WHERE dbo.[user].ShopFloor = -1 AND dbo.[user].[current] = 1 AND forename<> 'Weld' AND forename <> 'Allocation' ORDER BY[forename] +' ' + [surname] ";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        cmbEmployee.Items.Add(row[0].ToString());
                    conn.Close();
                }
            }
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            skip_combo_box = 0;
            cmbDepartment.Text = "";
            fillDataGrid();
        }

        private void dteStart_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dteEnd_ValueChanged(object sender, EventArgs e)
        {
        }

        private void fillDataGrid()
        {
            if (dataGridView1.DataSource != null)
            {
                //dataGridView1.Rows.Clear();
                dataGridView1.DataSource = null;
            }
            //get the user ID from the fullname
            string sql = "SELECT id FROM [user_info].dbo.[user] where forename + ' ' + surname = '" + cmbEmployee.Text + "'";
            int staff_id = 0;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());


                sql = "SELECT cast([start_date] as date) FROM [user_info].dbo.[user] WHERE id = '" + staff_id + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var temp = cmd.ExecuteScalar();
                    if (temp != null)
                        lblStartDate.Text = "Start Date: " + Convert.ToDateTime(cmd.ExecuteScalar().ToString()).ToString("dd/MM/yyyy");
                    else
                        lblStartDate.Text = "No Start Date";
                }

                //get all the departments that this user has been in between the selected dates

                sql = "select distinct (s.department) from dbo.power_plan_staff  s " +
                    "left join dbo.power_plan_date d on s.date_id = d.id " +
                    "left join dbo.power_plan_overtime_remake ot on ot.date_id = d.id AND s.department = ot.department and s.staff_id = ot.staff_id " +
                    "where  s.staff_id = " + staff_id + " AND cast(date_plan as DATE) >= '" + Convert.ToDateTime(dteStart.Value).ToString("yyyy-MM-dd") + "' AND " +
                    "CAST(date_plan as DATE) <= '" + Convert.ToDateTime(dteEnd.Value).ToString("yyyy-MM-dd") + "'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (skip_combo_box == 0)
                        cmbDepartment.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (cmbDepartment.Items.Contains(row[0].ToString()) == false)
                            cmbDepartment.Items.Add(row[0].ToString());
                    }
                    if (cmbDepartment.Items.Count > 1)
                    {
                        lblDepartment.Visible = true;
                        cmbDepartment.Visible = true;
                    }
                    else
                    {
                        lblDepartment.Visible = false;
                        cmbDepartment.Visible = false;
                    }
                }


                //sql = "SELECT  CAST(max(part_complete_date) as date) as [Date],MAX(DATENAME(dw,part_complete_date)) as [day],max(op) as [department], MAX(dbo.power_plan_staff.[hours]) as [set_hours], " +
                //    " '0' as [overtime],'0' as [total_set_hours],'0' as [actual_hours] FROM dbo.door_part_completion_log LEFT JOIN dbo.power_plan_date on CAST(dbo.door_part_completion_log.part_complete_date as date) = dbo.power_plan_date.date_plan " +
                //    "LEFT JOIN dbo.power_plan_staff on dbo.power_plan_date.id = dbo.power_plan_staff.date_id AND dbo.power_plan_staff.department = dbo.door_part_completion_log.op " +
                //    "WHERE dbo.door_part_completion_log.staff_id = " + staff_id.ToString() + " AND CAST(part_complete_date as DATE)>= '" + Convert.ToDateTime(dteStart.Value).ToString("yyyy-MM-dd") + "' " +
                //    "AND CAST(part_complete_date as DATE)<= '" + Convert.ToDateTime(dteEnd.Value).ToString("yyyy-MM-dd") + "' AND part_status = 'Complete' " +
                //    "GROUP BY op,CAST(part_complete_date as date),dbo.door_part_completion_log.staff_id";

                sql = "SELECT max(b.date_plan) as [Date],MAX(DATENAME(dw,b.date_plan)) as [day],max(department) as [department], MAX(a.[hours]) as [set_hours],'0' as [overtime],'0' as [total_set_hours],'0' as [actual_hours],max(a.id) as [placement],max(placement_note) as [note] " +
                    "FROM dbo.power_plan_staff a LEFT JOIN dbo.power_plan_date b on a.date_id = b.id " +
                    "WHERE a.staff_id = " + staff_id.ToString() + " AND CAST(b.date_plan as DATE)>= '" + Convert.ToDateTime(dteStart.Value).ToString("yyyy-MM-dd") + "' AND CAST(b.date_plan as DATE)<= '" + Convert.ToDateTime(dteEnd.Value).ToString("yyyy-MM-dd") + "' " +
                    "AND department <> 'Punching' AND department <> 'Stores' AND department<> 'Dispatch' AND department<> 'HS' AND department<> 'Cleaning' AND department<> 'ToolRoom' AND department<> 'Management' ";

                if (string.IsNullOrEmpty(cmbDepartment.Text) == false)
                    sql = sql + " AND department = '" + cmbDepartment.Text + "' ";

                sql = sql + " GROUP BY department,b.date_plan,a.staff_id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dt.Columns.Add(" ");
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    dataGridView1.DataSource = dt;
                }

                //Am currently having issues where time_for_part / 60 is not giving me the correct number, going to manually add it here instead~
                //need to do the same for overtime

                int actualHoursIndex = dataGridView1.Columns["actual_hours"].Index;
                int dateIndex = dataGridView1.Columns["Date"].Index;
                int setHoursIndex = dataGridView1.Columns["set_hours"].Index;
                int totalSetHoursIndex = dataGridView1.Columns["Total_set_hours"].Index;
                int departmentIndex = dataGridView1.Columns["department"].Index;
                int overtimeIndex = dataGridView1.Columns["overtime"].Index;
                int placementIndex = dataGridView1.Columns["placement"].Index;
                int noteIndex = dataGridView1.Columns["note"].Index;
                double runningSet = 0, runningOvertime = 0, runningActual = 0, runningSetAndOvertime = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index == dataGridView1.Rows.Count - 1)
                    {
                        //add to totals~
                        row.Cells[actualHoursIndex].Value = runningActual.ToString();
                        row.Cells[setHoursIndex].Value = runningSet.ToString();
                        row.Cells[totalSetHoursIndex].Value = runningSetAndOvertime.ToString();
                        row.Cells[overtimeIndex].Value = runningOvertime.ToString();
                        row.Cells[setHoursIndex - 1].Value = "TOTALS:";
                        if (runningSetAndOvertime > runningActual)  //gained / dropped
                        {
                            double temp = runningSetAndOvertime - runningActual;
                            if (temp < 0)
                                temp = temp * -1;
                            lblDifference.BackColor = Color.PaleVioletRed;
                            lblDifference.Text = "Dropped " + Math.Round(temp, 2).ToString() + " Hours";
                        }
                        else
                        {
                            double temp = runningSetAndOvertime - runningActual;
                            if (temp < 0)
                                temp = temp * -1;

                            lblDifference.Text = "Gained " + Math.Round(temp, 2).ToString() + " Hours";
                            lblDifference.BackColor = Color.DarkSeaGreen;
                        }
                        // lblDifference.Text = "Set Hours - Actual Hours = " + Convert.ToString(runningSetAndOvertime - runningActual);
                    }
                    else
                    {

                        string temp = "";
                        temp = row.Cells[departmentIndex].Value.ToString();
                        if (temp == "Dressing")
                            temp = "buffing";
                        if (temp == "Slimline")
                            sql = "SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] FROM dbo.door_part_completion_log WHERE staff_id = " + staff_id.ToString() + " AND CAST(part_complete_date as DATE) = '" + Convert.ToDateTime(row.Cells[dateIndex].Value).ToString("yyyy-MM-dd") + "' AND part_percent_complete IS NOT NULL  GROUP BY staff_id),0)";
                        else
                            sql = " SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] FROM dbo.door_part_completion_log " +
                            "WHERE staff_id = " + staff_id.ToString() + " AND CAST(part_complete_date as DATE) = '" + Convert.ToDateTime(row.Cells[dateIndex].Value).ToString("yyyy-MM-dd") + "' AND part_status = 'Complete'  AND op = '" + temp + "' GROUP BY staff_id),0)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            temp = Convert.ToString(cmd.ExecuteScalar());
                            string test = row.Cells[actualHoursIndex].Value.ToString();
                            row.Cells[actualHoursIndex].Value = Convert.ToString(temp);
                        }
                        sql = "SELECT COALESCE(overtime,0) from dbo.power_plan_overtime_remake LEFT JOIN dbo.power_plan_date on dbo.power_plan_overtime_remake.date_id = dbo.power_plan_date.id " +
                            "WHERE staff_id = " + staff_id.ToString() + " AND CAST(date_plan as DATE) = '" + Convert.ToDateTime(row.Cells[dateIndex].Value).ToString("yyyy-MM-dd") + "' AND department = '" + row.Cells[departmentIndex].Value.ToString() + "'";
                        temp = "0";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            temp = Convert.ToString(cmd.ExecuteScalar());
                            if (temp == "")
                                temp = "0";
                            temp = Convert.ToString(Convert.ToDouble(temp) * 0.8);
                            row.Cells[overtimeIndex].Value = temp;
                        }

                        double hoursTemp = 0;
                        double actualTemp = 0;
                        hoursTemp = Convert.ToDouble(row.Cells[setHoursIndex].Value) + Convert.ToDouble(row.Cells[overtimeIndex].Value);
                        actualTemp = Convert.ToDouble(row.Cells[actualHoursIndex].Value);

                        if (hoursTemp < actualTemp)
                        {
                            row.Cells[9].Value = "✔";
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                        else
                        {
                            row.Cells[9].Value = "✖";
                            row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                        }

                        //note colour
                        int placementID = Convert.ToInt32(row.Cells[placementIndex].Value.ToString());
                        PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                        pnc.getNote();

                        if (pnc._hasNote == true)
                        {
                            row.Cells[0].Style.BackColor = Color.Yellow;
                        }

                        //add set hours + overtime together
                        double totalHours = Math.Round(Convert.ToDouble(row.Cells[setHoursIndex].Value) + Convert.ToDouble(row.Cells[overtimeIndex].Value), 2);
                        row.Cells[totalSetHoursIndex].Value = totalHours;

                        runningActual = runningActual + Convert.ToDouble(row.Cells[actualHoursIndex].Value);
                        runningOvertime = runningOvertime + Convert.ToDouble(row.Cells[overtimeIndex].Value);
                        runningSet = runningSet + Convert.ToDouble(row.Cells[setHoursIndex].Value);
                        runningSetAndOvertime = runningSetAndOvertime + Convert.ToDouble(row.Cells[totalSetHoursIndex].Value);
                    }
                }
                conn.Close();

                //format
                dataGridView1.Columns[1].HeaderText = "Day";
                dataGridView1.Columns[2].HeaderText = "Department";
                dataGridView1.Columns[3].HeaderText = "Set Hours";
                dataGridView1.Columns[4].HeaderText = "Overtime";
                dataGridView1.Columns[5].HeaderText = "Set Hours + Overtime";
                dataGridView1.Columns[6].HeaderText = "Actual Hours";
                dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[7].Visible = false;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                }
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dataGridView1.ClearSelection();
                dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
                dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                Rectangle bounds = this.Bounds;
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }
                    bitmap.Save(@"C:\temp\temp.jpg", ImageFormat.Jpeg);
                    printImage();
                }
            }
            catch
            {
            }
        }

        private void printImage()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (sender, args) =>
                {
                    Image i = Image.FromFile(@"C:\temp\temp.jpg");
                    Point p = new Point(100, 100);
                    args.Graphics.DrawImage(i, args.MarginBounds);
                };

                pd.DefaultPageSettings.Landscape = true;
                Margins margins = new Margins(50, 50, 50, 50);
                pd.DefaultPageSettings.Margins = margins;
                pd.Print();
            }
            catch
            {
            }
        }

        private void dteStart_CloseUp(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void dteEnd_CloseUp(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void frmProductivity_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int placementID = 0;
            placementID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            PlacementNote pn = new PlacementNote(placementID);
            pn.ShowDialog();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Please select a user before attempting to send an email.", "No Data", MessageBoxButtons.OK);
                return;
            }
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("There is not enough data to be able to send an email, please select a bigger date range.", "No Data", MessageBoxButtons.OK);
                return;
            }

            int date = 0, day = 0, dept = 0, set_hours = 0, overtime = 0, actual_hours = 0, set_hours_and_overtime = 0, misc = 0;
            date = 0;
            day = 1;
            dept = 2;
            set_hours = 3;
            overtime = 4;
            set_hours_and_overtime = 5;
            actual_hours = 6;
            misc = 8;

            //insert into >>> productivity_email
            //productivity_email
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                string sql = "DELETE FROM dbo.productivity_email";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string misc_temp = "";
                    if (row.Cells[misc].Value.ToString() == "✔")
                        misc_temp = "tick";
                    else if (row.Cells[misc].Value.ToString() == "✖")
                        misc_temp = "cross";
                    else
                        misc_temp = "";
                    string date_temp = "";
                    try
                    { date_temp = Convert.ToDateTime(row.Cells[date].Value.ToString()).ToString("dd-MM-yyyy"); }
                    catch
                    { date_temp = ""; }

                    sql = "INSERT INTO dbo.productivity_email ([date],[day],department,set_hours,overtime,set_hours_and_overtime,actual_hours,misc) VALUES ('" +
                        date_temp + "','" + row.Cells[day].Value.ToString() + "','" + row.Cells[dept].Value.ToString() + "','" + row.Cells[set_hours].Value.ToString() + "','" + row.Cells[overtime].Value.ToString() + "','" + row.Cells[set_hours_and_overtime].Value.ToString() + "'," +
                        "'" + row.Cells[actual_hours].Value.ToString() + "','" + misc_temp + "')";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
            frmProductivityEmail frm = new frmProductivityEmail(cmbEmployee.Text + " - " + lblDifference.Text);
            frm.ShowDialog();

        }


        private void print_excel()
        {

            int current_excel_row = 1;
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\Productivity.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");


            //add the title
            xlWorksheet.Cells[1][current_excel_row].Value2 = cmbEmployee.Text + " " + lblDifference.Text;
            //
            if (lblDifference.BackColor != Color.Empty)
                xlWorksheet.Range["A" + current_excel_row.ToString() + ":H" + current_excel_row.ToString()].Interior.Color = lblDifference.BackColor;
            current_excel_row++;

            //column headers
            current_excel_row++;

            //vvv we need to loop through dgv 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int noteIndex = dataGridView1.Columns["note"].Index;
                xlWorksheet.Cells[1][current_excel_row].Value2 = row.Cells[0].Value.ToString();
                xlWorksheet.Cells[2][current_excel_row].Value2 = row.Cells[1].Value.ToString();
                xlWorksheet.Cells[3][current_excel_row].Value2 = row.Cells[2].Value.ToString();
                xlWorksheet.Cells[4][current_excel_row].Value2 = row.Cells[3].Value.ToString();
                xlWorksheet.Cells[5][current_excel_row].Value2 = row.Cells[4].Value.ToString();
                xlWorksheet.Cells[6][current_excel_row].Value2 = row.Cells[5].Value.ToString();
                xlWorksheet.Cells[7][current_excel_row].Value2 = row.Cells[6].Value.ToString();
                xlWorksheet.Cells[8][current_excel_row].Value2 = row.Cells[noteIndex].Value.ToString();
                //paint the row based on what the dgv is
                if (row.DefaultCellStyle.BackColor != Color.Empty)
                    xlWorksheet.Range["A" + current_excel_row.ToString() + ":H" + current_excel_row.ToString()].Interior.Color = row.DefaultCellStyle.BackColor;
                current_excel_row++;
            }

            //border
            xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row - 1, 8]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


            xlWorksheet.Range["E:E"].NumberFormat = "@";

            xlWorksheet.Columns.AutoFit();
            xlWorksheet.Rows.AutoFit();

            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            //xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);



            string FileName = @"c:\temp\Productivity_" + DateTime.Now.ToString("mmss") + ".xlsx";
            xlWorkbook.SaveAs(@"c:\temp\Productivity_" + DateTime.Now.ToString("mmss") + ".xlsx");  // or book.Save();

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

            if (System.IO.File.Exists(FileName))
                System.Diagnostics.Process.Start(FileName);

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            print_excel();
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            skip_combo_box = -1;
            fillDataGrid();
        }
    }
}
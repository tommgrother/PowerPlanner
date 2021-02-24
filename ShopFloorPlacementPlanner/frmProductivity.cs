using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace ShopFloorPlacementPlanner
{
    public partial class frmProductivity : Form
    {
        public frmProductivity()
        {
            InitializeComponent();

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
                //sql = "SELECT  CAST(max(part_complete_date) as date) as [Date],MAX(DATENAME(dw,part_complete_date)) as [day],max(op) as [department], MAX(dbo.power_plan_staff.[hours]) as [set_hours], " +
                //    " '0' as [overtime],'0' as [total_set_hours],'0' as [actual_hours] FROM dbo.door_part_completion_log LEFT JOIN dbo.power_plan_date on CAST(dbo.door_part_completion_log.part_complete_date as date) = dbo.power_plan_date.date_plan " +
                //    "LEFT JOIN dbo.power_plan_staff on dbo.power_plan_date.id = dbo.power_plan_staff.date_id AND dbo.power_plan_staff.department = dbo.door_part_completion_log.op " +
                //    "WHERE dbo.door_part_completion_log.staff_id = " + staff_id.ToString() + " AND CAST(part_complete_date as DATE)>= '" + Convert.ToDateTime(dteStart.Value).ToString("yyyy-MM-dd") + "' " +
                //    "AND CAST(part_complete_date as DATE)<= '" + Convert.ToDateTime(dteEnd.Value).ToString("yyyy-MM-dd") + "' AND part_status = 'Complete' " +
                //    "GROUP BY op,CAST(part_complete_date as date),dbo.door_part_completion_log.staff_id";


                sql = "SELECT max(b.date_plan) as [Date],MAX(DATENAME(dw,b.date_plan)) as [day],max(department) as [department], MAX(a.[hours]) as [set_hours],'0' as [overtime],'0' as [total_set_hours],'0' as [actual_hours],max(a.id) as [placement] " +
                    "FROM dbo.power_plan_staff a LEFT JOIN dbo.power_plan_date b on a.date_id = b.id " +
                    "WHERE a.staff_id = " + staff_id.ToString() + " AND CAST(b.date_plan as DATE)>= '" + Convert.ToDateTime(dteStart.Value).ToString("yyyy-MM-dd") + "' AND CAST(b.date_plan as DATE)<= '" + Convert.ToDateTime(dteEnd.Value).ToString("yyyy-MM-dd") + "' " +
                    "AND department <> 'Punching' AND department<> 'Stores' AND department<> 'Dispatch' AND department<> 'HS' AND department<> 'Cleaning' AND department<> 'ToolRoom' AND department<> 'Management'" +
                    "GROUP BY department,b.date_plan,a.staff_id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dt.Columns.Add(" ");
                    // generate the data you want to insert
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
                            lblDifference.Text = "Dropped " +Math.Round(temp,2).ToString() + " Hours";
                        }
                        else
                        {
                            double temp = runningSetAndOvertime - runningActual;
                            if (temp < 0)
                                temp = temp * -1;

                            lblDifference.Text = "Gained " + Math.Round(temp,2).ToString() + " Hours";
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
                            row.Cells[8].Value = "✔";
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                        else
                        {
                            row.Cells[8].Value = "✖";
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
                    bitmap.Save(@"C:\Temp\temp.jpg", ImageFormat.Jpeg);
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
    }
}

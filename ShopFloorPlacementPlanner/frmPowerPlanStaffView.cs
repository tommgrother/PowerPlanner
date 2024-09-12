using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    public partial class frmPowerPlanStaffView : Form
    {
        public frmPowerPlanStaffView(DateTime dteTemp)
        {
            InitializeComponent();

            dteDateSelection.Value = dteTemp;

            fillGrids();
        }

        private void fillGrids()
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();//+ dteDateSelection.Value.ToString("yyyyMMdd") +
                //pack
                string sql = "SELECT [full placement] as 'Staff Placement'," +
                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [9_30_percent],2) as nvarchar(max)) + " +
                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.25 as nvarchar(max)) as [9:30]," +
                    "case when [9_30_percent] >= 0.25 then 'up' when  [9_30_percent] < 0.25 then 'down' else '' end as [ ]," +
                    "[9_30_note]," +

                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [11_30_percent],2) as nvarchar(max)) + " +
                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.5 as nvarchar(max)) as [11:30]," +
                    "case when [11_30_percent] >= 0.5 then 'up' when  [11_30_percent] < 0.5 then 'down' else '' end as [  ]," +
                    "[11_30_note]," +

                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [2_30_percent],2) as nvarchar(max)) + " +
                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.75 as nvarchar(max)) as [2:30]," +
                    "case when [2_30_percent] >= 0.75 then 'up' when  [2_30_percent] < 0.75 then 'down' else '' end as [   ]," +
                    "[2_30_note]," +

                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [end_of_shift_percent],2) as nvarchar(max)) + " +
                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 1 as nvarchar(max)) as [EOS]," +
                    "case when [end_of_shift_percent] >= 1 then 'up' when  [end_of_shift_percent] < 1 then 'down' else '' end as [    ], " +
                    "[end_of_shift_note] " +

                    "FROM view_planner_punch_staff s " +
                    "left join dbo.power_plan_date d on s.date_id = d.id " +
                    "left join dbo.power_plan_overtime_remake ot on s.date_id = ot.date_id AND s.staff_id = ot.staff_id AND s.department = ot.department " +
                    "left join dbo.[power_plan_staff_percent_log] p on s.staff_id = p.staff_id AND " +
                    "s.department = p.department  and s.date_plan = p.log_date " +
                    "where s.date_plan = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' and s.department = 'Packing' and [Full Placement] NOT LIKE '%Allocation Block%' ORDER BY [Staff Name]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgPack.DataSource = dt;

                    foreach (DataGridViewColumn col in dgPack.Columns)
                    {
                        if (col.Index == 0)
                            continue;
                        foreach (DataGridViewRow row in dgPack.Rows)
                        {
                            if (row.Cells[col.Index].Value.ToString() == "up")
                                row.Cells[col.Index].Value = "✔";
                            else if (row.Cells[col.Index].Value.ToString() == "down")
                                row.Cells[col.Index].Value = "✖";
                        }
                    }
                    foreach (DataGridViewColumn col in dgPack.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    dgPack.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgPack.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgPack.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                }


                //weld --view_planner_bend_staff
                sql = "SELECT [full placement] as 'Staff Placement'," +
                                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [9_30_percent],2) as nvarchar(max)) + " +
                                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.25 as nvarchar(max)) as [9:30]," +
                                    "case when [9_30_percent] >= 0.25 then 'up' when  [9_30_percent] < 0.25 then 'down' else '' end as [ ]," +
                                    "[9_30_note]," +

                                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [11_30_percent],2) as nvarchar(max)) + " +
                                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.5 as nvarchar(max)) as [11:30]," +
                                    "case when [11_30_percent] >= 0.5 then 'up' when  [11_30_percent] < 0.5 then 'down' else '' end as [  ]," +
                                    "[11_30_note]," +


                                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [2_30_percent],2) as nvarchar(max)) + " +
                                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.75 as nvarchar(max)) as [2:30]," +
                                    "case when [2_30_percent] >= 0.75 then 'up' when  [2_30_percent] < 0.75 then 'down' else '' end as [   ]," +
                                    "[2_30_note]," +


                                    "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [end_of_shift_percent],2) as nvarchar(max)) + " +
                                    "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 1 as nvarchar(max)) as [EOS]," +
                                    "case when [end_of_shift_percent] >= 1 then 'up' when  [end_of_shift_percent] < 1 then 'down' else '' end as [    ] ," +
                                    "[end_of_shift_note] " +

                                    "FROM view_planner_bend_staff s " +
                                    "left join dbo.power_plan_date d on s.date_id = d.id " +
                                    "left join dbo.power_plan_overtime_remake ot on s.date_id = ot.date_id AND s.staff_id = ot.staff_id AND s.department = ot.department " +
                                    "left join dbo.[power_plan_staff_percent_log] p on s.staff_id = p.staff_id AND " +
                                    "s.department = p.department  and s.date_plan = p.log_date " +
                                    "where s.date_plan = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' and s.department = 'Welding' and [Full Placement] NOT LIKE '%Allocation Block%' ORDER BY [Staff Name]";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgWeld.DataSource = dt;


                    foreach (DataGridViewColumn col in dgWeld.Columns)
                    {
                        if (col.Index == 0)
                            continue;
                        foreach (DataGridViewRow row in dgWeld.Rows)
                        {
                            if (row.Cells[col.Index].Value.ToString() == "up")
                                row.Cells[col.Index].Value = "✔";
                            else if (row.Cells[col.Index].Value.ToString() == "down")
                                row.Cells[col.Index].Value = "✖";
                        }
                    }
                    foreach (DataGridViewColumn col in dgWeld.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    dgWeld.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgWeld.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgWeld.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                }


                //buff
                sql = "SELECT [full placement] as 'Staff Placement'," +
                           "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [9_30_percent],2) as nvarchar(max)) + " +
                           "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.25 as nvarchar(max)) as [9:30]," +
                           "case when [9_30_percent] >= 0.25 then 'up' when  [9_30_percent] < 0.25 then 'down' else '' end as [ ]," +
                           "[9_30_note] ," +

                           "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [11_30_percent],2) as nvarchar(max)) + " +
                           "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.5 as nvarchar(max)) as [11:30]," +
                           "case when [11_30_percent] >= 0.5 then 'up' when  [11_30_percent] < 0.5 then 'down' else '' end as [  ]," +
                           "[11_30_note]," +

                           "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [2_30_percent],2) as nvarchar(max)) + " +
                           "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 0.75 as nvarchar(max)) as [2:30]," +
                           "case when [2_30_percent] >= 0.75 then 'up' when  [2_30_percent] < 0.75 then 'down' else '' end as [   ]," +
                           "[2_30_note] ," +

                           "CAST(round((coalesce(hours,0) + COALESCE(ot.overtime,0)) * [end_of_shift_percent],2) as nvarchar(max)) + " +
                           "' / ' +  CAST((coalesce(hours,0) + COALESCE(ot.overtime,0)) * 1 as nvarchar(max)) as [EOS]," +
                           "case when [end_of_shift_percent] >= 1 then 'up' when  [end_of_shift_percent] < 1 then 'down' else '' end as [    ], " +
                           "[end_of_shift_note] " +

                           "FROM view_planner_punch_staff s " +
                           "left join dbo.power_plan_date d on s.date_id = d.id " +
                           "left join dbo.power_plan_overtime_remake ot on s.date_id = ot.date_id AND s.staff_id = ot.staff_id AND s.department = ot.department " +
                           "left join dbo.[power_plan_staff_percent_log] p on s.staff_id = p.staff_id AND " +
                           "s.department = p.department  and s.date_plan = p.log_date " +
                           "where s.date_plan = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' and s.department = 'Dressing' and [Full Placement] NOT LIKE '%Allocation Block%' ORDER BY [Staff Name]";


                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgBuff.DataSource = dt;

                    foreach (DataGridViewColumn col in dgBuff.Columns)
                    {
                        if (col.Index == 0)
                            continue;
                        foreach (DataGridViewRow row in dgBuff.Rows)
                        {
                            if (row.Cells[col.Index].Value.ToString() == "up")
                                row.Cells[col.Index].Value = "✔";
                            else if (row.Cells[col.Index].Value.ToString() == "down")
                                row.Cells[col.Index].Value = "✖";
                        }
                    }
                    foreach (DataGridViewColumn col in dgBuff.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    dgBuff.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgBuff.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgBuff.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                conn.Close();
            }
        }

        private void frmPowerPlanStaffView_Shown(object sender, EventArgs e)
        {
            paint_grid();
        }

        private void paint_grid()
        {
            //pack
            foreach (DataGridViewColumn col in dgPack.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (col.Index == 0)
                    continue;
                foreach (DataGridViewRow row in dgPack.Rows)
                {
                    if (row.Cells[col.Index].Value.ToString() == "✔")
                    {
                        row.Cells[col.Index].Style.BackColor = Color.DarkSeaGreen;
                    }
                    else if (row.Cells[col.Index].Value.ToString() == "✖")
                    {
                        row.Cells[col.Index].Style.BackColor = Color.PaleVioletRed;
                    }

                    if (col.Index == 3 || col.Index == 6 || col.Index == 9 || col.Index == 12)
                    {
                        if (row.Cells[col.Index].Value.ToString().Length > 0)
                            row.Cells[col.Index -1].Style.BackColor = Color.Yellow;
                    }
                }
            }

            dgPack.Columns[3].Visible = false;
            dgPack.Columns[6].Visible = false;
            dgPack.Columns[9].Visible = false;
            dgPack.Columns[12].Visible = false;

            dgPack.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgPack.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgPack.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgPack.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //buff
            foreach (DataGridViewColumn col in dgBuff.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (col.Index == 0)
                    continue;
                foreach (DataGridViewRow row in dgBuff.Rows)
                {
                    if (row.Cells[col.Index].Value.ToString() == "✔")
                    {
                        row.Cells[col.Index].Style.BackColor = Color.DarkSeaGreen;
                    }
                    else if (row.Cells[col.Index].Value.ToString() == "✖")
                    {
                        row.Cells[col.Index].Style.BackColor = Color.PaleVioletRed;
                    }
 
                    if (col.Index == 3 || col.Index == 6 || col.Index == 9 || col.Index == 12)
                    {
                        if (row.Cells[col.Index].Value.ToString().Length > 0)
                            row.Cells[col.Index -1].Style.BackColor = Color.Yellow;
                    }
                }
            }

            dgBuff.Columns[3].Visible = false;
            dgBuff.Columns[6].Visible = false;
            dgBuff.Columns[9].Visible = false;
            dgBuff.Columns[12].Visible = false;

            dgBuff.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgBuff.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgBuff.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgBuff.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //weld
            foreach (DataGridViewColumn col in dgWeld.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (col.Index == 0)
                    continue;
                foreach (DataGridViewRow row in dgWeld.Rows)
                {
                    if (row.Cells[col.Index].Value.ToString() == "✔")
                    {
                        row.Cells[col.Index].Style.BackColor = Color.DarkSeaGreen;
                    }
                    else if (row.Cells[col.Index].Value.ToString() == "✖")
                    {
                        row.Cells[col.Index].Style.BackColor = Color.PaleVioletRed;
                    }
                    if (col.Index == 3 || col.Index == 6 || col.Index == 9 || col.Index == 12)
                    {
                        if (row.Cells[col.Index].Value.ToString().Length > 0)
                            row.Cells[col.Index -1].Style.BackColor = Color.Yellow;
                    }
                }
            }

            dgWeld.Columns[3].Visible = false;
            dgWeld.Columns[6].Visible = false;
            dgWeld.Columns[9].Visible = false;
            dgWeld.Columns[12].Visible = false;

            dgWeld.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgWeld.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgWeld.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgWeld.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            dgPack.ClearSelection();
            dgBuff.ClearSelection();
            dgWeld.ClearSelection();
        }

        private void dteDateSelection_CloseUp(object sender, EventArgs e)
        {
            fillGrids();
            paint_grid();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string file = @"C:\temp\power_plan_intervals_" + DateTime.Now.ToString("mmss") + ".xlsx";

            //opening method 

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(); //xlApp.Workbooks.Open(GT_input_filepath, 0, false); //< to open an already existing file 

            Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];

            xlApp.Visible = true; //make it visible for now 
            xlApp.WindowState = Excel.XlWindowState.xlMaximized;


            //set up first field
            int row_count = 2;
            xlWorksheet.Range["A1:I1"].Merge();
            xlWorksheet.Cells[1][1].Value2 = dteDateSelection.Value.ToString("dd/MM/yyyy");
            xlWorksheet.Range["A1:I1"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            xlWorksheet.Range["A1"].Cells.Font.Size = 22;

            //loop through each grid adding them to the excel sheet

            if (dgWeld.Rows.Count > 0) //welding
            {
                xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();
                xlWorksheet.Cells[1][row_count].Value2 = "Welding";
                xlWorksheet.Cells[1][row_count].Font.Size = 15;
                xlWorksheet.Cells[1][row_count].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_count++;
                xlWorksheet.Cells[1][row_count].Value2 = "Staff Placement";
                xlWorksheet.Cells[2][row_count].Value2 = "09:30";
                xlWorksheet.Cells[4][row_count].Value2 = "11:30";
                xlWorksheet.Cells[6][row_count].Value2 = "14:30";
                xlWorksheet.Cells[8][row_count].Value2 = "EOS";
                xlWorksheet.Range["B" + row_count.ToString() + ":I" + row_count.ToString() + ""].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_count++;
                for (int i = 0; i < dgWeld.Rows.Count; i++)
                {
                    xlWorksheet.Cells[1][row_count].Value2 = dgWeld.Rows[i].Cells[0].Value.ToString();
                    xlWorksheet.Cells[2][row_count].Value2 = dgWeld.Rows[i].Cells[1].Value.ToString();
                    //9:30 tick box
                    xlWorksheet.Cells[3][row_count].Value2 = dgWeld.Rows[i].Cells[2].Value.ToString();
                    if (dgWeld.Rows[i].Cells[2].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[3][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if(dgWeld.Rows[i].Cells[2].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[3][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[4][row_count].Value2 = dgWeld.Rows[i].Cells[3].Value.ToString();

                    //11:30 tick box
                    xlWorksheet.Cells[5][row_count].Value2 = dgWeld.Rows[i].Cells[4].Value.ToString();
                    if (dgWeld.Rows[i].Cells[4].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[5][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgWeld.Rows[i].Cells[4].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[5][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[6][row_count].Value2 = dgWeld.Rows[i].Cells[5].Value.ToString();
                    //2:30 tick box
                    xlWorksheet.Cells[7][row_count].Value2 = dgWeld.Rows[i].Cells[6].Value.ToString();
                    if (dgWeld.Rows[i].Cells[6].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[7][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if(dgWeld.Rows[i].Cells[6].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[7][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[8][row_count].Value2 = dgWeld.Rows[i].Cells[7].Value.ToString();
                    //eos tick box
                    xlWorksheet.Cells[9][row_count].Value2 = dgWeld.Rows[i].Cells[8].Value.ToString();
                    if (dgWeld.Rows[i].Cells[8].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[9][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgWeld.Rows[i].Cells[8].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[9][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);
                    xlWorksheet.Range["B" + row_count.ToString() + ":I" + row_count.ToString() + ""].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    row_count++;
                }
            }
            
            xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();
            row_count++;
            xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();

            if (dgBuff.Rows.Count > 0) //Buffing
            {
                xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();
                xlWorksheet.Cells[1][row_count].Value2 = "Buffing";
                xlWorksheet.Cells[1][row_count].Font.Size = 15;
                xlWorksheet.Cells[1][row_count].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_count++;
                xlWorksheet.Cells[1][row_count].Value2 = "Staff Placement";
                xlWorksheet.Cells[2][row_count].Value2 = "09:30";
                xlWorksheet.Cells[4][row_count].Value2 = "11:30";
                xlWorksheet.Cells[6][row_count].Value2 = "14:30";
                xlWorksheet.Cells[8][row_count].Value2 = "EOS";
                xlWorksheet.Range["B" + row_count.ToString() + ":I" + row_count.ToString() + ""].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_count++;
                for (int i = 0; i < dgBuff.Rows.Count; i++)
                {
                    xlWorksheet.Cells[1][row_count].Value2 = dgBuff.Rows[i].Cells[0].Value.ToString();
                    xlWorksheet.Cells[2][row_count].Value2 = dgBuff.Rows[i].Cells[1].Value.ToString();
                    //9:30 tick box
                    xlWorksheet.Cells[3][row_count].Value2 = dgBuff.Rows[i].Cells[2].Value.ToString();
                    if (dgBuff.Rows[i].Cells[2].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[3][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgBuff.Rows[i].Cells[2].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[3][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[4][row_count].Value2 = dgBuff.Rows[i].Cells[3].Value.ToString();

                    //11:30 tick box
                    xlWorksheet.Cells[5][row_count].Value2 = dgBuff.Rows[i].Cells[4].Value.ToString();
                    if (dgBuff.Rows[i].Cells[4].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[5][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgBuff.Rows[i].Cells[4].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[5][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[6][row_count].Value2 = dgBuff.Rows[i].Cells[5].Value.ToString();
                    //2:30 tick box
                    xlWorksheet.Cells[7][row_count].Value2 = dgBuff.Rows[i].Cells[6].Value.ToString();
                    if (dgBuff.Rows[i].Cells[6].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[7][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgBuff.Rows[i].Cells[6].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[7][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[8][row_count].Value2 = dgBuff.Rows[i].Cells[7].Value.ToString();
                    //eos tick box
                    xlWorksheet.Cells[9][row_count].Value2 = dgBuff.Rows[i].Cells[8].Value.ToString();
                    if (dgBuff.Rows[i].Cells[8].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[9][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgBuff.Rows[i].Cells[8].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[9][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);
                    xlWorksheet.Range["B" + row_count.ToString() + ":I" + row_count.ToString() + ""].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    row_count++;
                }
            }

            xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();
            row_count++;
            xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();

            if (dgPack.Rows.Count > 0) //Packing
            {
                xlWorksheet.Range["A" + row_count.ToString() + ":I" + row_count.ToString() + ""].Merge();
                xlWorksheet.Cells[1][row_count].Value2 = "Packing";
                xlWorksheet.Cells[1][row_count].Font.Size = 15;
                xlWorksheet.Cells[1][row_count].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_count++;
                xlWorksheet.Cells[1][row_count].Value2 = "Staff Placement";
                xlWorksheet.Cells[2][row_count].Value2 = "09:30";
                xlWorksheet.Cells[4][row_count].Value2 = "11:30";
                xlWorksheet.Cells[6][row_count].Value2 = "14:30";
                xlWorksheet.Cells[8][row_count].Value2 = "EOS";
                xlWorksheet.Range["B" + row_count.ToString() + ":I" + row_count.ToString() + ""].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_count++;
                for (int i = 0; i < dgPack.Rows.Count; i++)
                {
                    xlWorksheet.Cells[1][row_count].Value2 = dgPack.Rows[i].Cells[0].Value.ToString();
                    xlWorksheet.Cells[2][row_count].Value2 = dgPack.Rows[i].Cells[1].Value.ToString();
                    //9:30 tick box
                    xlWorksheet.Cells[3][row_count].Value2 = dgPack.Rows[i].Cells[2].Value.ToString();
                    if (dgPack.Rows[i].Cells[2].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[3][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgPack.Rows[i].Cells[2].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[3][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[4][row_count].Value2 = dgPack.Rows[i].Cells[3].Value.ToString();

                    //11:30 tick box
                    xlWorksheet.Cells[5][row_count].Value2 = dgPack.Rows[i].Cells[4].Value.ToString();
                    if (dgPack.Rows[i].Cells[4].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[5][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgPack.Rows[i].Cells[4].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[5][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[6][row_count].Value2 = dgPack.Rows[i].Cells[5].Value.ToString();
                    //2:30 tick box
                    xlWorksheet.Cells[7][row_count].Value2 = dgPack.Rows[i].Cells[6].Value.ToString();
                    if (dgPack.Rows[i].Cells[6].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[7][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgPack.Rows[i].Cells[6].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[7][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);

                    xlWorksheet.Cells[8][row_count].Value2 = dgPack.Rows[i].Cells[7].Value.ToString();
                    //eos tick box
                    xlWorksheet.Cells[9][row_count].Value2 = dgPack.Rows[i].Cells[8].Value.ToString();
                    if (dgPack.Rows[i].Cells[8].Style.BackColor == System.Drawing.Color.PaleVioletRed)
                        xlWorksheet.Cells[9][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleVioletRed);
                    else if (dgPack.Rows[i].Cells[8].Style.BackColor == System.Drawing.Color.DarkSeaGreen)
                        xlWorksheet.Cells[9][row_count].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSeaGreen);
                    xlWorksheet.Range["B" + row_count.ToString() + ":I" + row_count.ToString() + ""].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    row_count++;
                }
            }

            //formatting  



            //xlWorkSheet.Range["H2:H300"].NumberFormat = "£#,###,###.00"; < formats into currency 

            //xlWorksheet.Range["A1:D1"].Merge(); merging cells 

            //xlWorksheet.Range["A1"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft; < alignment 

            //xlWorksheet.Range["A1"].Cells.Font.Size = 22; < font size 



            //auto fit and rows  

            //Microsoft.Office.Interop.Excel.Worksheet ws = xlApp.ActiveWorkbook.Worksheets[1]; 

            Microsoft.Office.Interop.Excel.Range range = xlWorksheet.UsedRange;

            //xlWorksheet.Columns.ClearFormats(); 

            //xlWorksheet.Rows.ClearFormats(); 

            xlWorksheet.Columns.AutoFit();

            xlWorksheet.Rows.AutoFit();



            xlWorksheet.Columns[3].ColumnWidth = 5;
            xlWorksheet.Columns[5].ColumnWidth = 5;
            xlWorksheet.Columns[7].ColumnWidth = 5;
            xlWorksheet.Columns[9].ColumnWidth = 5;

            xlWorksheet.Columns[2].WrapText = true;



            //border active cells 

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            range.Borders.Color = ColorTranslator.ToOle(Color.Black);



            //xlWorkSheet.Columns[3].ColumnWidth = 98.14; //size and wrap columns 

            //xlWorkSheet.Columns[3].WrapText = true; 



            //Set print margins

            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;

            xlPageSetUp.Zoom = false;

            xlPageSetUp.FitToPagesWide = 1;

            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorkbook.PrintOut();

            //save the new file 

            xlApp.DisplayAlerts = false;

            xlWorkbook.SaveAs(file);


            //close the workbook 
            xlWorkbook.Close();
            xlApp.Quit();



            //release objects from memory 

            releaseObject(xlWorksheet);
            releaseObject(xlWorkbook);
            releaseObject(xlApp);
           // Process.Start(file);
        }


        //release objects void 

        private static void releaseObject(object obj)

        {

            try

            {

                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

                obj = null;

            }

            catch (Exception ex)

            {

                obj = null;

                Console.WriteLine("Error releasing object: " + ex.Message);

            }

            finally

            {

                GC.Collect();

            }

        }

        private void dgWeld_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgWeld_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgWeld.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Yellow)
                MessageBox.Show(dgWeld.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
        }

        private void dgBuff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBuff.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Yellow)
                MessageBox.Show(dgBuff.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
        }

        private void dgPack_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPack.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Yellow)
                MessageBox.Show(dgPack.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
        }





    }
}


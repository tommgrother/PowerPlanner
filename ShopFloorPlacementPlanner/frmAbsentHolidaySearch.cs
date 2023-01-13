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
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace ShopFloorPlacementPlanner
{
    public partial class frmAbsentHolidaySearch : Form
    {
        public frmAbsentHolidaySearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //get the unique dept
            string default_in_department = "";
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    default_in_department = "Punching";
                    break;

                case 1:
                    default_in_department = "Laser";
                    break;

                case 2:
                    default_in_department = "Bending";
                    break;

                case 3:
                    default_in_department = "Welding";
                    break;

                case 4:
                    default_in_department = "Buffing";
                    break;

                case 5:
                    default_in_department = "painting";
                    break;

                case 6:
                    default_in_department = "Packing";
                    break;

                case 7:
                    default_in_department = "Slimline";
                    break;

                case 8:
                    default_in_department = "ALL";
                    break;

                default:
                    default_in_department = "ALL";
                    break;
            }


            string sql = "select CAST(date_absent as nvarchar(max))  as Date," +
                "COALESCE(CAST(sum(case when absent_type = 2 then 1 else 0 end) as nvarchar(max)),0) as [Full Holiday]," +
                "COALESCE(CAST(sum(case when absent_type = 3 then 1 else 0 end) as nvarchar(max)),0) as [Half Day Holiday] ," +
                "COALESCE(CAST(sum(case when absent_type = 8 then 1 else 0 end) as nvarchar(max)),0)as [Absent Taken Holiday]," +
                "COALESCE(CAST(sum(case when absent_type = 5 then 1 else 0 end) as nvarchar(max)),0)as [Absent] ," +
                "COALESCE(CAST(sum(case when absent_type = 9 then 1 else 0 end) as nvarchar(max)),0)as [Unpaid], " +
                "COALESCE(CAST(sum(case when absent_type = 2 then 1 when absent_type = 3 then 1 when absent_type = 8 then 1 when absent_type = 5 then 1 when absent_type = 2 then 1 when absent_type = 9 then 1 end) as nvarchar(max)),0) as [Total]" +
                "from dbo.absent_holidays a " +
                "left join[user_info].dbo.[user] b on a.staff_id = b.id " +
                "where [current] = 1 AND date_absent >= '" + dteStart.Value.ToString("yyyyMMdd") + "' and date_absent <= '" + dteEnd.Value.ToString("yyyyMMdd") + "' and b.shopfloor = -1 ";


                if (default_in_department != "ALL")
                sql = sql + " AND b.default_in_department = '" + default_in_department + "' ";
                
                sql = sql + "group by date_absent";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    dataGridView1.DataSource = null;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int index = dt.Rows.Count;
                    //////TOTALS//////////////////////////////
                    DataRow totalRow = dt.NewRow();
                    dt.Rows.InsertAt(totalRow, index);
                    //loop through all the rows/columns 
                    dt.Rows[index][0] = "Total:";
                    for (int col = 1; col < dt.Columns.Count; col++)
                    {
                        int total = 0;
                        for (int row = 0; row < dt.Rows.Count - 1; row++)
                        {
                            total = total + Convert.ToInt32(dt.Rows[row][col]);
                        }
                        dt.Rows[index][col] = total;
                    }
                    //////HOURS//////////////////////////////
                    index = dt.Rows.Count;
                    DataRow hoursRow = dt.NewRow();
                    dt.Rows.InsertAt(hoursRow, index);
                    //loop through all the rows/columns 
                    dt.Rows[index][0] = "Hours:";
                    for (int col = 1; col < dt.Columns.Count; col++)
                    {
                        double hours = 0;
                        if (col == 2)
                        {
                            for (int row = 0; row < dt.Rows.Count - 2; row++)
                            {
                                // MessageBox.Show(dt.Rows[row][0].ToString());
                                DateTime temp = Convert.ToDateTime(dt.Rows[row][0].ToString());
                                if (temp.DayOfWeek == DayOfWeek.Friday)
                                {
                                    hours = hours + (Convert.ToInt32(dt.Rows[row][col]) * 2.8);
                                }
                                else
                                    hours = hours + (Convert.ToInt32(dt.Rows[row][col]) * 3.2);
                            }
                        }
                        else
                        {
                            for (int row = 0; row < dt.Rows.Count - 2; row++)
                            {
                                // MessageBox.Show(dt.Rows[row][0].ToString());
                                DateTime temp = Convert.ToDateTime(dt.Rows[row][0].ToString());
                                if (temp.DayOfWeek == DayOfWeek.Friday)
                                {
                                    hours = hours + (Convert.ToInt32(dt.Rows[row][col]) * 5.6);
                                }
                                else
                                    hours = hours + (Convert.ToInt32(dt.Rows[row][col]) * 6.4);
                            }
                        }
                        dt.Rows[index][col] = hours;
                    }
                    //////TOTAL COLUMN ON ITS OWN//////////////////
                    double finalTotal = 0;
                    for (int finalColumn = 1; finalColumn < dt.Columns.Count - 1; finalColumn++)
                    {
                        finalTotal = finalTotal + Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][finalColumn]);
                    }
                    dt.Rows[dt.Rows.Count - 1][dt.Columns.Count - 1] = finalTotal;
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnPrint_Click(object sender, EventArgs e)
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(tabControl1.SelectedIndex.ToString());
            btnSearch.PerformClick();
        }
    }
}

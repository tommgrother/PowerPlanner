using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmStockParts : Form
    {
        public DateTime date { get; set; }
        public frmStockParts(DateTime _date)
        {
            InitializeComponent();
            date = _date;
            loadGrid();
            fillCombo();
            lblTitle.Text = "Stock Parts for " + _date.ToString("dd/MM/yyyy");
        }

        private void loadGrid()
        {
            string sql = "select staff_string,part_string,date_started,date_complete,total_items_complete,total_time " +
                         "from dbo.view_bending_session where CAST(date_started as date) = '" + date.ToString("yyyyMMdd") + "' AND staff_string like '%" + cmbStaff.Text + "%' ";

            using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                connection.Close();
            }
            format();
        }

        private void format()
        {
            dataGridView1.Columns[0].HeaderText = "Staff";
            dataGridView1.Columns[1].HeaderText = "Stock Part";
            dataGridView1.Columns[2].HeaderText = "Date Started";
            dataGridView1.Columns[3].HeaderText = "Date Complete";
            dataGridView1.Columns[4].HeaderText = "Total Items";
            dataGridView1.Columns[5].HeaderText = "Total Time";

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void fillCombo()
        {

            cmbStaff.Items.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (cmbStaff.Items.Contains(row.Cells[0].Value.ToString()))
                { } //nothing
                else
                    cmbStaff.Items.Add(row.Cells[0].Value.ToString());
            }


        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            print_sheet();
        }

        private void print_sheet()
        {
            string file_name = @"C:\temp\temp" + DateTime.Now.ToString("mmss") + ".jpg";
            try
            {
                Image bit = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

                Graphics gs = Graphics.FromImage(bit);

                gs.CopyFromScreen(new Point(0, 0), new Point(0, 0), bit.Size);

                //bit.Save(@"C:\temp\temp.jpg");


                Rectangle bounds = this.Bounds;
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }
                    bitmap.Save(file_name);
                }


                //var frm = Form.ActiveForm;
                //using (var bmp = new Bitmap(frm.Width, frm.Height))
                //{
                //    frm.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                //    bmp.Save(@"C:\temp\temp.jpg");
                //}

                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (sender, args) =>
                {
                    Image i = Image.FromFile(file_name);
                    Rectangle m = args.MarginBounds;
                    if ((double)i.Width / (double)i.Height > (double)m.Width / (double)m.Height) // image is wider
                    {
                        m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                    }
                    else
                    {
                        m.Width = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                    }
                    args.Graphics.DrawImage(i, m);
                };

                pd.DefaultPageSettings.Landscape = false;
                //Margins margins = new Margins(50, 50, 50, 50);
                //pd.DefaultPageSettings.Margins = margins;
                pd.Print();
            }
            catch
            { }
        }

    }
}

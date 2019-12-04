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

namespace ShopFloorPlacementPlanner
{
    public partial class frmWeeklyInsert : Form
    {
        public int _staff_id { get; set; }
        public string _staff_fullname { get; set; }
        public DateTime _selectedDate { get; set; }
        public DateTime monday { get; set; }
        public DateTime sunday { get; set; }
        public DateTime passed_date { get; set; }
        public string _dept { get; set; }
        public frmWeeklyInsert(int staff_id, string staff_fullname, DateTime searchDate, string department)
        {
            InitializeComponent();
            // add all the variables into new props
            _staff_id = staff_id;
            _staff_fullname = staff_fullname;
            _selectedDate = searchDate;
            _dept = department;
            //now get date range
            getDates();
            DGV();
        }

        private void getDates()
        {
            passed_date = _selectedDate;
            while (_selectedDate.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                _selectedDate = _selectedDate.AddDays(-1);
            monday = _selectedDate;
            //MessageBox.Show("monday = " + monday.ToString());
            //get end of week
            sunday = _selectedDate.AddDays(6);
            // MessageBox.Show("sunday = " + sunday.ToString());
        }
        private void DGV()
        {
            string sql = "";
            using (SqlConnection CONNECT = new SqlConnection(connectionStrings.ConnectionString))
            {
                if (_dept == "Dressing")
                {
                    sql = "Select a.id,CAST(a.date_plan as date), b.buffing_OT" +
                             " FROM dbo.power_plan_date a " +
                             "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                             "WHERE date_plan >= '" + monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
                }
                else
                {
                    sql = "Select a.id,CAST(a.date_plan as date)" +
                              " FROM dbo.power_plan_date a " +
                              "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                              "WHERE date_plan >= '" + monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
                }
                using (SqlCommand COMMAND = new SqlCommand(sql, CONNECT))
                {
                    CONNECT.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(COMMAND);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    CONNECT.Close();
                }
                

                //format
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Calibri", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
                }
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
                //------------

                dataGridView1.Columns[1].HeaderText = "Date";

                //set the CURRENT dates checkbox to true
                DateTime dgv; //date in the DGV
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //row.Cells[2].Value = CheckState.Unchecked;
                    dgv = Convert.ToDateTime(dataGridView1.Rows[row.Index].Cells[1].Value);
                    if (dgv == passed_date)
                    {

                        //row.Cells[2].Value = CheckState.Checked;
                        dataGridView1.Rows.RemoveAt(row.Index);
                    }

                }

                dataGridView1.Refresh();
            }
        }




        //    sql = "Select id from  dbo.power_plan_date where date_plan = '" + passedDate.ToString("yyyyMMdd") + "'";
        //    using (SqlCommand cmd = new SqlCommand(sql, CONNECT))
        //    {
        //        CONNECT.Open();
        //        dateID = Convert.ToInt32(cmd.ExecuteScalar());
        //        // MessageBox.Show("dateID = " + dateID.ToString());
        //        CONNECT.Close();
        //    }
        //}
        //this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //dataGridView1.Columns[0].HeaderText = "ID";
        //dataGridView1.Columns[1].HeaderText = "Date";
        //dataGridView1.Columns[2].HeaderText = dept + " Over Time";
        //dataGridView1.Columns[0].ReadOnly = true;
        //dataGridView1.Columns[1].ReadOnly = true;


        private void frmWeeklyInsert_Load(object sender, EventArgs e)
        {
            lbl_title.Text = "Select Which days you want " + _staff_fullname + " in " + _dept;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSeaGreen;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //loop through every single colour 
            //if its green THEN run through absent-already placed-placement to make sure there are no doubles in this code.
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                //everything inside here is already within a connection string
               for (int i = 0; i < dataGridView1.Rows.Count;i++)
                { //loop for colour
                    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.LightSeaGreen)
                    { //the correct colour
                        Placement p = new Placement()

                    }//end of if back colour = green
                } //end of for loop
            }
        }
    }
}


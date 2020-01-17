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
    public partial class frmWeeklyAD : Form
    {

        public DateTime Monday { get; set; }
        public DateTime Sunday { get; set; }
        public DateTime passedDate { get; set; }
        public string dept { get; set; }
        public int dateID { get; set; }
        public int additionForSD { get; set; }
        public frmWeeklyAD(DateTime selectedDate, string department)
        {
            InitializeComponent();
            passedDate = selectedDate;
            dept = department;
            getDates(selectedDate);
        }

        public void getDates(DateTime date)
        {
            //use the passed over date to find the start of the week and then use that to count forward to saturday (maybe sunday)
            //get start of week
            while (date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);
            Monday = date;
            //MessageBox.Show(Monday.ToString());
            //get end of week
            Sunday = date.AddDays(6);
            // MessageBox.Show(Sunday.ToString());

            //from here fill DT based on sql using monday+sunday
            //dataGridView1.Columns[0].HeaderText = "Date";
            //dataGridView1.Columns[1].HeaderText = "Department Over Time";
            using (SqlConnection CONNECT = new SqlConnection(connectionStrings.ConnectionString))
            {
                string sql = "";
                if (dept == "Dressing")
                {
                    sql = "Select a.id,CAST(a.date_plan as date), b.buffing_AD" +
                             " FROM dbo.power_plan_date a " +
                             "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                             "WHERE date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
                }
                else
                {
                    sql = "Select a.id,CAST(a.date_plan as date), b." + dept + "_AD" +
                              " FROM dbo.power_plan_date a " +
                              "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                              "WHERE date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
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
                sql = "Select id from  dbo.power_plan_date where date_plan = '" + passedDate.ToString("yyyyMMdd") + "'";
                using (SqlCommand cmd = new SqlCommand(sql, CONNECT))
                {
                    CONNECT.Open();
                    dateID = Convert.ToInt32(cmd.ExecuteScalar());
                    // MessageBox.Show("dateID = " + dateID.ToString());
                    CONNECT.Close();
                }
            }
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Date";
            dataGridView1.Columns[2].HeaderText = dept + " Addition";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Calibri", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 13F, FontStyle.Regular, GraphicsUnit.Pixel);

            //if there is a blank entry fill it with 0 to avoid sql error
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "")
                    dataGridView1.Rows[i].Cells[2].Value = "0";

            }
        }




        private void FrmWeeklyAD_Load(object sender, EventArgs e)
        {

        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            //update each line here to add new overtime, even if it hasnt been changed then it should not matter because it pulls the current overtime into that cell
            using (SqlConnection CONNECT = new SqlConnection(connectionStrings.ConnectionString))
            {
                string sql = "";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    Overtime o = new Overtime();
                    o.updateAD(Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value), dept, Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));


                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == dateID.ToString())
                            additionForSD = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);

                   
                }
            }
            this.Close();
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

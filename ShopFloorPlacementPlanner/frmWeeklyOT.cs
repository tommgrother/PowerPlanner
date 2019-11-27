using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    public partial class frmWeeklyOT : Form
    {
        public DateTime Monday { get; set; }
        public DateTime Sunday { get; set; }
        public DateTime passedDate { get; set; }
        public string dept { get; set; }
        public frmWeeklyOT(DateTime selectedDate,string department)
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
                string sql = "Select CAST(a.date_plan as date), b." + dept + "_OT" +
                     " FROM dbo.power_plan_date a " +
                     "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                     "WHERE date_plan >= '" + Monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + Sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
                using (SqlCommand COMMAND = new SqlCommand(sql, CONNECT))
                {
                    CONNECT.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(COMMAND);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    CONNECT.Close();
                }
            }
        }

        //public static DateTime FirstDateInWeek(this DateTime dt)
        //{

        //    return dt;
        }
   }

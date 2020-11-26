using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmCopyWeek : Form
    {
        public int dataCopied { get; set; }
        public DateTime dateCopyFrom { get; set; }
        public DateTime dateCopyTo { get; set; }
        public frmCopyWeek(DateTime dateOnOpen)
        {
            InitializeComponent();
            dateCopyFrom = dateOnOpen;
            dateCopyTo = dateOnOpen;
            getMonday();
            //dateCopyTo =  dateCopyTo.AddDays(7);
            dteWeekCopyFrom.Value = dateCopyFrom;
            dteWeekCopyTo.Value = dateCopyFrom.AddDays(7); //this should always pick the date they were on (but the monday of that week) and +7 days for the next monday
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                for (int i = 0; i < 7; i++)
                {
                    string sql = "date FROM: " + dateCopyFrom.ToString() + " (" + dateCopyFrom.DayOfWeek.ToString() + ")";
                    sql = sql + Environment.NewLine;
                    sql = sql + "date TO: " + dateCopyTo.ToString() + " (" + dateCopyTo.DayOfWeek.ToString() + ")";
                    //MessageBox.Show(sql);
                    using (SqlCommand cmd = new SqlCommand("usp_power_planner_copy_week", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@copyFromDate", SqlDbType.Date).Value = dateCopyFrom.ToString("yyyy-MM-dd");
                        cmd.Parameters.AddWithValue("@copyToDate", SqlDbType.Date).Value = dateCopyTo.ToString("yyyy-MM-dd");
                        cmd.ExecuteNonQuery();
                    }
                    dateCopyFrom = dateCopyFrom.AddDays(1);
                    dateCopyTo = dateCopyTo.AddDays(1);
                }
                conn.Close();
        }
        dataCopied = -1;
            this.Close();
    }
        private void getMonday()
        {

            int diff = (7 + (dateCopyFrom.DayOfWeek - DayOfWeek.Monday)) % 7;
            dateCopyFrom =  dateCopyFrom.AddDays(-1 * diff);
            diff = (7 + (dateCopyTo.DayOfWeek - DayOfWeek.Monday)) % 7;
            dateCopyTo=  dateCopyTo.AddDays(-1 * diff);

        }

        private void dteWeekCopyFrom_ValueChanged(object sender, EventArgs e)
        {
            dateCopyFrom = dteWeekCopyFrom.Value;
            dateCopyTo = dteWeekCopyTo.Value;
            getMonday();
            dteWeekCopyFrom.Value = dateCopyFrom;
            dteWeekCopyTo.Value = dateCopyTo;
        }

        private void dteWeekCopyTo_ValueChanged(object sender, EventArgs e)
        {
            dateCopyFrom = dteWeekCopyFrom.Value;
            dateCopyTo = dteWeekCopyTo.Value;
            getMonday();
            dteWeekCopyFrom.Value = dateCopyFrom;
            dteWeekCopyTo.Value = dateCopyTo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dataCopied = 0;
            this.Close();
        }
    }
}

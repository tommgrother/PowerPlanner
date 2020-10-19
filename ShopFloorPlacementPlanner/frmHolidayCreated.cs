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
    public partial class frmHolidayCreated : Form
    {
        public frmHolidayCreated(int staff_id,DateTime tempDate,string name)
        {
            InitializeComponent();
            //grab the holiday
            label1.Text = name;
            string sql = "SELECT CAST(date_added as DATE) FROM DBO.absent_holidays where staff_id = " + staff_id + " AND date_absent = '" + tempDate.ToString("yyyy-MM-dd") + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.ClearSelection();
                }
            }
        }

        private void frmHolidayCreated_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}

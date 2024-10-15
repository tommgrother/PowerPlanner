using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmBendAvailable : Form
    {
        public frmBendAvailable()
        {
            InitializeComponent();

            //load the values

            string sql = "SELECT COALESCE((SELECT Sum(Round(([time_remaining_bend]*[quantity_same])/60,2)) " +
                         "FROM dbo.door " +
                         "WHERE (dbo.door.complete_punch='True') AND dbo.door.bend_staff_allocation is not null AND (dbo.door.status_id =1 Or dbo.door.status_id=2) AND " +
                         "(dbo.door.test_identifier=0 Or dbo.door.test_identifier Is Null) AND (dbo.door.complete_bend='False') AND (dbo.door.date_bend Is Not Null)),0)";

            txtBendingAllocated.Text = runSQL(sql);

            sql = "SELECT COALESCE((SELECT Sum(Round(([time_remaining_bend]*[quantity_same])/60,2)) " +
                  "FROM dbo.door " +
                  "WHERE (dbo.door.complete_punch='True') AND (dbo.door.status_id =1 Or dbo.door.status_id=2) AND " +
                  "(dbo.door.test_identifier=0 Or dbo.door.test_identifier Is Null) AND (dbo.door.complete_bend='False') AND (dbo.door.date_bend Is Not Null)),0)";

            txtBendingAvailable.Text = runSQL(sql);


            sql = "SELECT COALESCE((select round(CAST(SUM(d.time_bend) as float) / 60,2) as time_allocated from dbo.shaken_out_log s " +
                  "left join dbo.batch b on s.batch_id = b.batch_id " +
                  "left join dbo.door d on d.id = b.door_id " +
                  "where cast(s.logged_date as date) = CAST(GETDATE()  as date) " +
                  "group by cast(s.logged_date as date)),0)";

            txtWorkShakenOut.Text = runSQL(sql);



        }


        public string runSQL(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows[0][0].ToString();

                }
                conn.Close();
            }
        }

    }
}

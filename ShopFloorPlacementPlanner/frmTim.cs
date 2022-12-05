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
    public partial class frmTim : Form
    {
        public frmTim(DateTime temp)
        {
            InitializeComponent();

            string sql = "select department + ' ' + CAST((sum(overtime) * 0.8) as nvarchar(max)) as overtime from dbo.power_plan_date a " +
                "left join dbo.power_plan_overtime_remake b on a.id = b.date_id " +
                "where a.date_plan = cast('" + temp.ToString("yyyyMMdd")  + "' as date) AND(department = 'Punching' OR department = 'Bending') " +
                "group by department";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    lblPunch.Text = dt.Rows[0][0].ToString();
                    lblBend.Text = dt.Rows[1][0].ToString();
                    conn.Close();
                }
            }
        }
    }
}

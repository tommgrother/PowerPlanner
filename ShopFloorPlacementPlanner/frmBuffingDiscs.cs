using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmBuffingDiscs : Form
    {
        public DateTime ppDate { get; set; }
        public frmBuffingDiscs(DateTime temp)
        {
            InitializeComponent();
            ppDate = temp;
            fillGrid();
        }

        private void fillGrid()
        {
            //string sql = "usp_count_required_discs";

            //get the required discs
            string sql = "select quantity_required FROM dbo.view_buffing_discs_required_given where date_buff = '" + ppDate.ToString("yyyyMMdd") + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmdDiscs = new SqlCommand(sql, conn))
                {
                    //cmdDiscs.CommandType = CommandType.StoredProcedure;

                    lblRequired.Text = "BUFFING DISCS REQUIRED: " + cmdDiscs.ExecuteScalar().ToString();

                }

                //get the total discs given out
                sql = "select SUM(quantity) FROM dbo.stock_log s " +
                    "left join [user_info].dbo.[user] u on s.booked_out_by_id = u.id " +
                    "left join [user_info].dbo.[user] u_staff on s.staff_name = u_staff.forename + ' ' + u_staff.surname " +
                    "where stock_code = 11044 and CAST(transaction_date as date) = '" + ppDate.ToString("yyyyMMdd") + "'  and (u_staff.slimline_fulltime = 0 or u_staff.slimline_fulltime is null)";
                //sql = "select quantity_given FROM dbo.view_buffing_discs_required_given where date_buff = '" + ppDate.ToString("yyyyMMdd") + "'";
                using (SqlCommand cmdDiscsGiven = new SqlCommand(sql, conn))
                {
                    //need to fix the date formatting of this???
                    int discs = 0;
                    try
                    {
                        discs = Convert.ToInt32(cmdDiscsGiven.ExecuteScalar().ToString());
                        lblGivenOut.Text = "BUFFING DISCS GIVEN OUT: " + cmdDiscsGiven.ExecuteScalar().ToString() ?? "0";
                    }
                    catch
                    { discs = 0; }
                    
                }

                sql = "select item_name as [Stock Name],staff_name as [Staff Name],u.forename + ' ' + u.surname as [Booked out By],Quantity,transaction_date as [Transaction Date] " +
                    "FROM dbo.stock_log s " +
                    "left join [user_info].dbo.[user] u on s.booked_out_by_id = u.id " +
                    "left join [user_info].dbo.[user] u_staff on s.staff_name = u_staff.forename + ' ' + u_staff.surname " +
                    "where stock_code = 11044 and CAST(transaction_date as date) = '" + ppDate.ToString("yyyyMMdd") + "'  and (u_staff.slimline_fulltime = 0 or u_staff.slimline_fulltime is null) " +
                    "order by S.id desc";



                using (SqlCommand cmdDiscsStaff  = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmdDiscsStaff);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }

                conn.Close();
            }
        }
    }
}

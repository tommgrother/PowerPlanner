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
    public partial class frmCopyPlacements : Form
    {

        public DateTime _placementDate { get; set; }
        public frmCopyPlacements(DateTime placementDate)
        {
            InitializeComponent();
            _placementDate = placementDate;
            lblMessage.Text = "Adding placements to: " + _placementDate.ToShortDateString();
        }




        private void frmCopyPlacements_Load(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            using(SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("usp_power_planner_copy_placements", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@toDate", SqlDbType.Date).Value = _placementDate;
                    cmd.Parameters.AddWithValue("@fromDate", SqlDbType.Date).Value = dteNewDate.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


                using (SqlCommand cmd = new SqlCommand("usp_power_planner_copy_sub_dept", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@toDate", SqlDbType.Date).Value = _placementDate;
                    cmd.Parameters.AddWithValue("@fromDate", SqlDbType.Date).Value = dteNewDate.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }


            this.Close();


        }
    }
}

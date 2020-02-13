using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    class SubDeptClass
    {
        public bool alreadyPlaced { get; set; }
        public int dateID { get; set; }
        public DateTime _dateTime { get; set; }
        public int staffID { get; set; }


        public void add_placement(int placement_ID, string sub_dept)
        {
           // getDateID(placement_ID); dont ever need this now really...
            getStaffID(placement_ID);
            if (alreadyPlaced == false) //aslong as checkplacement has been run first running this right after selects the right one! just make sure to run the checkplacments again before adding/changing someone else!
            {
                string sql = "INSERT INTO dbo.[power_plan_paint_sub_dept_test_temp_2] (staff_id,sub_department,placement_id) VALUES (" +staffID + ",'" + sub_dept + "'," + placement_ID + ");";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            else
            {
                //update instead
                string sql = "UPDATE dbo.[power_plan_paint_sub_dept_test_temp_2] SET sub_department  = '" + sub_dept + "' WHERE placement_id = " + placement_ID + ";";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

            }
        }
        public void checkPlacement(int placement_ID)
        {
            int variable = 0;
            string sql = "SELECT COALESCE(placement_id,0) FROM  dbo.[power_plan_paint_sub_dept_test_temp_2] WHERE placement_id = " + placement_ID;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    variable = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
            if (variable == 0)
                alreadyPlaced = false;
            else
                alreadyPlaced = true;
        }


        private void getStaffID(int placementID)
        {
            //get staff id using the placement id from the main powerplan table
            string sql = "SELECT COALESCE(staff_id,0) FROM dbo.view_planner_punch_staff WHERE PlacementID = " + placementID;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    staffID = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
        }

        private void getDateID(int placement_ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT date_plan from dbo.view_planner_punch_staff where PlacementID = @PlacementID", conn))
                {
                    cmd.Parameters.AddWithValue("@PlacementID", placement_ID);
                    conn.Open();
                    _dateTime = (DateTime)cmd.ExecuteScalar();
                    conn.Close();
                }
                //now get the ID from thisss
                using (SqlCommand cmd = new SqlCommand("Select id from dbo.power_plan_date where date_plan = '" + _dateTime + "'", conn))
                {
                    conn.Open();
                    dateID = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
            }

        }
    }
}

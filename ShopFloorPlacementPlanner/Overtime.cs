using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ShopFloorPlacementPlanner
{
    class Overtime
    {
     

        public int _dateID { get; set; }
        public Overtime()
        {

            
        }


        public void getDateID(DateTime selectedDate)
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT id from dbo.power_plan_date where date_plan = @datePlan", conn))
            {
                cmd.Parameters.AddWithValue("@datePlan", selectedDate);
                conn.Open();
                _dateID = (Int32)cmd.ExecuteScalar();
                conn.Close();
            }
        }


        public void updateOT(DateTime selectedDate, string department, double amount)
        {
            getDateID(selectedDate);
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("usp_update_power_plan_OT", conn))
            {

                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dateId", _dateID);
                cmd.Parameters.AddWithValue("@dept", department);
                cmd.Parameters.AddWithValue("@amount", amount);

                cmd.ExecuteNonQuery();

            }

            conn.Close();

        }


    }
}

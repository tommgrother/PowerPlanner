using System;
using System.Data;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    internal class AdditionsDeductions
    {
        public int _dateID { get; set; }

        public void updateAD(DateTime selectedDate, string department, double amount)
        {
            getDateID(selectedDate);
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("usp_update_power_plan_ad", conn))
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

        public void getDateID(DateTime selectedDate)
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            bool dateExists;

            using (SqlCommand cmd = new SqlCommand("SELECT id from dbo.power_plan_date where date_plan = @datePlan", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@datePlan", selectedDate);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    dateExists = true;
                }
                else
                {
                    dateExists = false;
                    //ADDS DATE TABLE ENTRY
                }

                rdr.Close();
                if (dateExists == true)
                {
                    _dateID = (Int32)cmd.ExecuteScalar();
                    conn.Close();
                }
                else
                {
                    SqlConnection conn2 = new SqlConnection(connectionStrings.ConnectionString);
                    conn2.Open();
                    SqlCommand cmdDate = new SqlCommand("usp_update_planner_clear", conn2);
                    cmdDate.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdDate.Parameters.AddWithValue("@plannerDate", SqlDbType.Date).Value = selectedDate;
                    cmdDate.Parameters.AddWithValue("@department", SqlDbType.NVarChar).Value = "";
                    cmdDate.ExecuteNonQuery();
                }
            }
        }
    }
}
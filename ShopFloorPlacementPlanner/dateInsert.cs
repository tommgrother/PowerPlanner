using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ShopFloorPlacementPlanner
{
    class dateInsert
    {
        public DateTime passedDateTime { get; set; }
        public void check_date(DateTime dateTime)
        {
            //pass date over to procedure
            passedDateTime = dateTime;

           
            using (SqlConnection CONNECT = new SqlConnection(connectionStrings.ConnectionString)) //keep it all in one connection string
            {
                using (SqlCommand cmd = new SqlCommand("usp_power_plan_date_insert", CONNECT))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = passedDateTime;
                    CONNECT.Open();
                    cmd.ExecuteNonQuery();
                    CONNECT.Close();
                }
            }
        }
    }
}

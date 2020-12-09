using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    class shiftHours
    {
        public static int validation;
        public static double _hours;

        public shiftHours(double hours, DateTime tempDate, int staffID, string dept)
        {

            double getData;
            getData = 0;
            //check if they go over 6.4 here
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //grab date_id
                int _date = 0;
                string sql = "SELECT id FROM dbo.power_plan_date WHERE date_plan = '" + tempDate.ToString("yyyy-MM-dd") + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    _date = Convert.ToInt32(cmd.ExecuteScalar());
                }
                sql = "select sum(hours) as temp from dbo.power_plan_staff WHERE staff_id = " + staffID.ToString() + " AND date_id = " + _date.ToString() + " AND department <> '" + dept + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    try
                    {
                        getData = Convert.ToDouble(cmd.ExecuteScalar());
                    }
                    catch
                    {
                        getData = 0;
                    }


                    if ((getData + hours) > 6.4)
                    {
                        validation = 0; // cant allow it
                        return;
                    }
                }
                conn.Close();
            }
            _hours = hours;
            validation = -1;
        }



    }
}

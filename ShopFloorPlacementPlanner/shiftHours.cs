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
        public static double _alreadyAssignedHours;
        public static double _maxHours;

        public shiftHours(double hours, DateTime tempDate, int staffID, string dept)
        {

            double getData;
            
            getData = 0;
            //check if they go over 6.4 here  -- also needs to be reduced to 6.6 if its a friday 
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //check if that date is a friday and adjust hours around it
                if (tempDate.DayOfWeek == DayOfWeek.Friday)
                    _maxHours = 5.6;
                else
                    _maxHours = 6.4; 

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


                    if ((getData + hours) > _maxHours)
                    {
                        _alreadyAssignedHours = getData;
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

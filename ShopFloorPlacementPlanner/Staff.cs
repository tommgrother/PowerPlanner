using System;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    public class Staff
    {
        public string _fullname { get; set; }
        public int _staffID { get; set; }

        public Staff(string fullname)
        {
            _fullname = fullname;
            getStaffID();
        }

        private void getStaffID()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT id from dbo.view_full_name where fullname = @fullname", conn);
            cmd.Parameters.AddWithValue("@fullname", _fullname);

            _staffID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        }
    }
}
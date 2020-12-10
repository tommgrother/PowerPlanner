using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ShopFloorPlacementPlanner
{
    class Placement
    {


        public DateTime _selectedDate { get; set; }
        public int _dateID { get; set; }
        public int _staffID { get; set; }
        public string _department { get; set; }
        public string _placement_type { get; set; }
        public double _hours { get; set; }
        public int _notPresentType { get; set; }
        public int[] _weldTeamStaffID { get; set; }
        public int _weldTeamMembersPresent { get; set; }


        public bool _alreadyPlaced { get; set; }
        public string _existingPlacementType { get; set; }
        public double _existingPlacementHours { get; set; }

        public Placement(DateTime selectedDate, int staffID, string department, string placementType, double hours)
        {
            _selectedDate = selectedDate;
            _staffID = staffID;
            _department = department;
            _placement_type = placementType;
            _hours = hours;



            getDateID();
        }

        private void getDateID() 
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using(SqlCommand cmd = new SqlCommand("SELECT id from dbo.power_plan_date where date_plan = @datePlan", conn))
            {
                cmd.Parameters.AddWithValue("@datePlan", _selectedDate);
                conn.Open();
                _dateID = (Int32)cmd.ExecuteScalar();
                conn.Close();
            }
        }

        public void getWeldTeamUserID()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT * from dbo.allocation_welding_team where linked_staff_id= @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", _staffID);
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    _weldTeamStaffID = new int[2] {Convert.ToInt32(rdr["user_id_1"]), Convert.ToInt32(rdr["user_id_2"]) };
                }

                conn.Close();
               

            }
        }

        public void checkWeldTeamAbsence()
        {
            if (_staffID == 165)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                int members = 0;

                int userId1 = _weldTeamStaffID[0];
                int userID2 = _weldTeamStaffID[1];

                using (SqlCommand cmd = new SqlCommand("SELECT * from dbo.absent_holidays WHERE (staff_id = @staffID1 or staff_id = @staffID2) and date_absent = @dateAbsent", conn))
                {
                    cmd.Parameters.AddWithValue("@staffID1", _weldTeamStaffID[0]);
                    cmd.Parameters.AddWithValue("@staffID2", _weldTeamStaffID[1]);
                    cmd.Parameters.AddWithValue("@dateAbsent", _selectedDate);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            members++;
                        }
                    }

                    conn.Close();

                    _weldTeamMembersPresent = members;
                }

            }
        }


        public void addPlacment()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);


            using (SqlCommand cmd = new SqlCommand("insert into dbo.power_plan_staff (date_id,staff_id,department,placement_type,hours) VALUES(@dateID,@staffID,@department,@placementType,@hours)", conn))
            {
                cmd.Parameters.AddWithValue("@dateID", _dateID);
                cmd.Parameters.AddWithValue("@staffID", _staffID);
                cmd.Parameters.AddWithValue("@department", _department);
                cmd.Parameters.AddWithValue("@placementType", _placement_type);
                cmd.Parameters.AddWithValue("@hours", _hours);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
    
                
         }


        public void notPresent()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            using (SqlCommand cmd = new SqlCommand("select absent_type from dbo.absent_holidays where staff_id=@staffID and date_absent = @dateAbsent",conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@staffID", _staffID);
                cmd.Parameters.AddWithValue("@dateAbsent", _selectedDate);

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    _notPresentType =  Convert.ToInt32(rdr["absent_type"]);
                }
                else
                {
                    _notPresentType = 0;
                }

            }
           
        }



        public void checkPlacement()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT MAX(placement_type) as PT,sum(hours) as sumHours from dbo.power_plan_staff where date_id = @dateID and staff_id = @staffID", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@dateID", _dateID);
                cmd.Parameters.AddWithValue("@staffID", _staffID);

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    _alreadyPlaced = true;
                    _existingPlacementType = rdr["PT"].ToString();
                    try
                    {
                        _existingPlacementHours = Convert.ToDouble(rdr["sumHours"]);

                    }
                    catch
                    {
                        _existingPlacementHours = 0;
                    }

                }
                else
                {
                    _alreadyPlaced = false;
                }

            }
        }
    }
}

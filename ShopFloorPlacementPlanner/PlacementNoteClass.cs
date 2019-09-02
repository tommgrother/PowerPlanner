using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ShopFloorPlacementPlanner
{
    class PlacementNoteClass
    {

        public int _pID { get; set; }
        public bool _hasNote { get; set; }
        public bool _nonStandardPlacment { get; set; }


        public PlacementNoteClass(int pID)
        {
            _pID = pID;
        }

        public void checkNonStandard()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();


            SqlCommand cmd = new SqlCommand("Select * from dbo.view_planner_non_default_placements where id=@placementID", conn);
            cmd.Parameters.AddWithValue("@placementID", _pID);
         

            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {

                if (rdr["department"].ToString() != rdr["default_in_department"].ToString())
                {
                    _nonStandardPlacment = true;

                }
                else
                {
                    _nonStandardPlacment = false;
                }
            }
            else
            {

                _nonStandardPlacment = false;
            }

            conn.Close();
        }

        
        public void getNote()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select placement_note from dbo.power_plan_staff where id=@placementID", conn);
            cmd.Parameters.AddWithValue("@placementID", _pID);

            SqlDataReader rdr = cmd.ExecuteReader();

          
            if (rdr.Read())
            {
                
                if (string.IsNullOrWhiteSpace(rdr["placement_note"].ToString()))
                {
                    _hasNote = false;
                    
                }
                else
                {
                    _hasNote = true;
                }
            }
            else
            {
              
                _hasNote = false;
            }


            conn.Close();


        }




    }
}

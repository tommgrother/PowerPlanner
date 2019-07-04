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
    public partial class PlacementNote : Form
    {

        public int _pn { get; set; }

        public PlacementNote(int pn)
        {
            InitializeComponent();
            _pn = pn;
            getNote();

        }

        private void getNote()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select placement_note from dbo.power_plan_staff where id =@placementID", conn);
            cmd.Parameters.AddWithValue("@placementID", _pn);

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                txtNote.Text = rdr["placement_note"].ToString();
            }
                

            conn.Close();
        }

        private void PlacementNote_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update dbo.power_plan_staff set placement_note = @placementNote where id =@placementID", conn);
            cmd.Parameters.AddWithValue("@placementNote", txtNote.Text);
            cmd.Parameters.AddWithValue("@placementID", _pn);

            cmd.ExecuteNonQuery();
            conn.Close();
            this.Close();

        }
    }
}

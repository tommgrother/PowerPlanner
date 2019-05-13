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
    public partial class frmEmailPrint : Form
    {
        public DateTime _selectedDate { get; set; }

        public frmEmailPrint(DateTime selectedDate)
        {
            InitializeComponent();
            _selectedDate = selectedDate;
        }

        private void frmEmailPrint_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //CLEARS ALL EXISTING SELECTION FROM THE TABLE FOR THIS DEPARTMENT
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmdDate = new SqlCommand("ups_power_plan_email_placements", conn);
            cmdDate.CommandType = CommandType.StoredProcedure;
            cmdDate.Parameters.AddWithValue("@selectedDate", SqlDbType.Date).Value = _selectedDate;
            cmdDate.Parameters.AddWithValue("@emailAddress", SqlDbType.NVarChar).Value = cmbEmail.Text;


            cmdDate.ExecuteNonQuery();
        }
    }
}

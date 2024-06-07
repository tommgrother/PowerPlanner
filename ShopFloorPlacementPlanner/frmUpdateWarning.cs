using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmUpdateWarning : Form
    {
        public int staff_id { get; set; }
        public int warning_number { get; set; }
        public frmUpdateWarning(int staff_id, int warning_number)
        {
            InitializeComponent();
            this.staff_id = staff_id;
            this.warning_number = warning_number;

            string sql = "select warning_note FROM dbo.staff_warnings s " +
                "where staff_id = " + this.staff_id + " and warning_number = " + this.warning_number;

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    txtWarning.Text = cmd.ExecuteScalar().ToString();
                }

                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtWarning.Text.Replace("'", "");
            if (txtWarning.Text.Length < 5)
            {
                MessageBox.Show("Please enter a more detailed note!","Invalid Note",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string sql = "UPDATE dbo.staff_warnings SET warning_note = '" + txtWarning.Text + "' " +
                "WHERE staff_id = " + this.staff_id + " and warning_number = " + this.warning_number;

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();

                conn.Close();
            }
            this.Close();
        }
    }
}

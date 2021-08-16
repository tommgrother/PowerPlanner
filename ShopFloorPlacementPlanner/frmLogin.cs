using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            cmbStaff.Items.Add("Kevin Edwards");
            cmbStaff.Items.Add("Simon Plumb");
            cmbStaff.Items.Add("Richard Williams");
            cmbStaff.Items.Add("Damian Regis");
            cmbStaff.Items.Add("Other Staff");
            login.formIsOpen = -1;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbStaff.Text))
            {
                MessageBox.Show("Please select your name before logging in.", "User Not Selected", MessageBoxButtons.OK);
                return;
            }

            login Login = new login();
            login.userFullName = cmbStaff.Text;


            MenuMain frmMainMenu = new MenuMain();
            frmMainMenu.Show();

            this.Hide();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //first check if the form is open
            if (login.formIsOpen == 0)
            {
                string person = login.userFullName;
                person = person.Substring(0, person.IndexOf(" "));
                if (person == "Kevin")
                    return;
                if (person == "Other")
                    return;
                string sql = "SELECT top 1  COALESCE(" + person + ",0) FROM dbo.kevinMessage where  " + person + " is null order by id desc";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        conn.Close();
                        if (dt.Rows.Count > 0)
                        {
                            frmKevinMessage KM = new frmKevinMessage(0);
                            login.formIsOpen = -1;
                            KM.ShowDialog();
                        }
                        //otherwise skip
                    }
                }
            }
        }
    }
}

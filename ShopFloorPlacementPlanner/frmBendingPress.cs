using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace ShopFloorPlacementPlanner
{
    public partial class frmBendingPress : Form
    {
        public frmBendingPress()
        {
            InitializeComponent();

            string sql = "	select forename + ' ' + surname from dbo.power_plan_staff a    left join dbo.power_plan_date b on a.date_id = b.id" +
                "    left join[user_info].dbo.[user] c on c.id = a.staff_id    WHERE a.department = 'bending' AND b.date_plan = CAST(GETDATE() as date) and[hours] > 0";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbPress1.Items.Add(dt.Rows[i][0]);
                        cmbPress2.Items.Add(dt.Rows[i][0]);
                        cmbPress3.Items.Add(dt.Rows[i][0]);
                    }
                    conn.Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPress1.Text) == true || string.IsNullOrEmpty(cmbPress2.Text) == true || string.IsNullOrEmpty(cmbPress3.Text) == true)
            {
                MessageBox.Show("Please asign a user to each press before continuing");
                return;
            }
            string sql = "";
            int press1User = 0, press2User = 0, press3User = 0;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //get the id's of each press user
                sql = "select id  from [user_info].dbo.[user] where forename + ' ' + surname = '" + cmbPress1.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    press1User = Convert.ToInt32(cmd.ExecuteScalar());
                sql = "select id  from [user_info].dbo.[user] where forename + ' ' + surname = '" + cmbPress2.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    press2User = Convert.ToInt32(cmd.ExecuteScalar());
                sql = "select id  from [user_info].dbo.[user] where forename + ' ' + surname = '" + cmbPress3.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    press3User = Convert.ToInt32(cmd.ExecuteScalar());

                //update the press_user table
                sql = "update dbo.press_users set press1UserID = " + press1User.ToString() + ", press2UserID = " + press2User.ToString() + ",press3UserID = " + press3User.ToString();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();

                //update unfinished parts in part allocation 
                sql = "update dbo.bending_split_parts_allocation set allocatedTo = " + press1User.ToString() + " where isComplete = 0 and specificPress = 1";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
                sql = "update dbo.bending_split_parts_allocation set allocatedTo = " + press2User.ToString() + " where isComplete = 0 and specificPress = 2";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
                sql = "update dbo.bending_split_parts_allocation set allocatedTo = " + press3User.ToString() + " where isComplete = 0 and specificPress = 3";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();

                conn.Close();
            }
            this.Close();
        }

        private void cmbPress1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbPress1.Text == cmbPress2.Text || cmbPress1.Text == cmbPress3.Text)
            //{
            //    cmbPress1.SelectedIndex = -1;
            //    MessageBox.Show("You can't assign a user to more than one press!", "Error");
            //}
        }

        private void cmbPress2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbPress2.Text == cmbPress1.Text || cmbPress2.Text == cmbPress3.Text)
            //{
            //    cmbPress2.SelectedIndex = -1;
            //    MessageBox.Show("You can't assign a user to more than one press!", "Error");
            //}
        }

        private void cmbPress3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbPress3.Text == cmbPress1.Text || cmbPress3.Text == cmbPress2.Text)
            //{
            //    cmbPress3.SelectedIndex = -1;
            //    MessageBox.Show("You can't assign a user to more than one press!", "Error");
            //}
        }

        private void frmBendingPress_Shown(object sender, EventArgs e)
        {
            //select the users

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                string sql = "select b.forename + ' ' + b.surname as fullName from dbo.press_users left join[user_info].dbo.[user] b on b.id = press1UserID where press1UserID = b.id";
                //get the id's of each press user
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmbPress1.Text = Convert.ToString(cmd.ExecuteScalar());

                sql = "select b.forename + ' ' + b.surname as fullName from dbo.press_users left join[user_info].dbo.[user] b on b.id = press2UserID where press2UserID = b.id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmbPress2.Text = Convert.ToString(cmd.ExecuteScalar());

                sql = "select b.forename + ' ' + b.surname as fullName from dbo.press_users left join[user_info].dbo.[user] b on b.id = press3UserID where press3UserID = b.id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmbPress3.Text = Convert.ToString(cmd.ExecuteScalar());
            }
        }
    }
}

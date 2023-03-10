using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmMoveDept : Form
    {
        public DateTime date { get; set; }
        public int staffID { get; set; }
        public string dept { get; set; }
        public string PT { get; set; }
        public double hours { get; set; }

        public frmMoveDept(int userID, DateTime _date, string placementType, double _hours, int isSlimline)
        {
            InitializeComponent();
            PT = placementType;
            hours = _hours;
            date = _date;
            staffID = userID;

            //MessageBox.Show(placementType);
            //MessageBox.Show(hours.ToString());

            //fill combobox based on what the userID(allocationDept) allows him
            if (isSlimline == -1)
            {
                cmbDept.Items.Add("Slimline");
                cmbDept.Items.Add("SlimlineDispatch");
                cmbDept.Items.Add("SlimlineStores");
            }
            else
            {
                using (SqlConnection CONNECT = new SqlConnection(connectionStrings.ConnectionStringUser))
                {
                    string sql = "SELECT actual_department, allocation_dept_2, allocation_dept_3, allocation_dept_4, allocation_dept_5, allocation_dept_6 , * FROM dbo.[user] WHERE id = " + userID.ToString();
                    //MessageBox.Show(sql);
                    using (SqlCommand cmd = new SqlCommand(sql, CONNECT))
                    {
                        CONNECT.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Regex regularExpression = new Regex(@"^[a-zA-Z]+$");
                            if (regularExpression.IsMatch((reader["actual_department"].ToString())))
                                cmbDept.Items.Add(reader["actual_department"].ToString());
                            if (regularExpression.IsMatch((reader["allocation_dept_2"].ToString())))
                                cmbDept.Items.Add(reader["allocation_dept_2"].ToString());
                            if (regularExpression.IsMatch((reader["allocation_dept_3"].ToString())))
                                cmbDept.Items.Add(reader["allocation_dept_3"].ToString());
                            if (regularExpression.IsMatch((reader["allocation_dept_4"].ToString())))
                                cmbDept.Items.Add(reader["allocation_dept_4"].ToString());
                            if (regularExpression.IsMatch((reader["allocation_dept_5"].ToString())))
                                cmbDept.Items.Add(reader["allocation_dept_5"].ToString());
                            if (regularExpression.IsMatch((reader["allocation_dept_6"].ToString())))
                                cmbDept.Items.Add(reader["allocation_dept_6"].ToString());
                        }
                        CONNECT.Close();
                    }
                }
            }
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            //call placement
            if (cmbDept.SelectedItem != null)
                dept = cmbDept.Text;
            else
            {
                MessageBox.Show("Please select a new department before updating.");
                return;
            }
            //Check if there is a current placement for this dept
            Placement pDate = new Placement(date, staffID, dept, PT, hours);
            string sql = "DELETE  FROM dbo.power_plan_staff where staff_id = " + staffID.ToString() + " AND date_id = " + pDate._dateID + " AND department = '" + dept + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
                conn.Close();
            }

            DialogResult result = MessageBox.Show("Would you like to perform this operation over multiple dates?", "Multiple Dates?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //grab the dept and just roll the function
                department_changed dc = new department_changed();
                dc.setDepartment(dept);

                //INSERTS THE CURRENT DAY
                Placement p = new Placement(date, staffID, dept, PT, hours);
                p.addPlacment();

                //INSERTS REST OF WEEK
                string staffName;

                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT forename + ' '  + surname from dbo.[user] where id = @staffId", conn);
                cmd.Parameters.AddWithValue("@staffId", staffID);
                staffName = cmd.ExecuteScalar().ToString();
                conn.Close();
                //uhhhhhhh
                //does this also need the painting thing???
                //ye i think so, ask the user for the dept IF its painting then carry on as normal
                string subDept = "";
                if (dept == "Painting")
                {
                    //quickly grab the max placement type
                    int MAXplacementID = 0;
                    using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", connection))
                        {
                            connection.Open();
                            MAXplacementID = Convert.ToInt32(command.ExecuteScalar());
                            connection.Close();
                        }
                    }
                    //now prompt the user to select which area they want the user in
                    frmSubDeptMultiple frmSDM = new frmSubDeptMultiple(staffID, MAXplacementID);
                    frmSDM.ShowDialog();
                    subDept = frmSDM.location;
                    //SubDeptClass add = new SubDeptClass();
                    //add.checkPlacement(MAXplacementID);
                    //add.add_placement(MAXplacementID, subDept);
                }

                //if (subDept.Length < 1)
                //    subDept = "ERROR";

                frmWeeklyInsert wi = new frmWeeklyInsert(staffID, staffName, date, dept, subDept);
                wi.ShowDialog();
            }
            else
            {
                //grab the dept and just roll the function
                department_changed dc = new department_changed();
                dc.setDepartment(dept);

                Placement p = new Placement(date, staffID, dept, PT, hours);
                p.addPlacment();
                //only one date... and if its painting then select a dept
                string subDept = "";
                if (dept == "Painting")
                {
                    //quickly grab the max placement type
                    int MAXplacementID = 0;
                    using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", connection))
                        {
                            connection.Open();
                            MAXplacementID = Convert.ToInt32(command.ExecuteScalar());
                            connection.Close();
                        }
                    }
                    //now prompt the user to select which area they want the user in
                    frmSubDeptMultiple frmSDM = new frmSubDeptMultiple(staffID, MAXplacementID);
                    frmSDM.ShowDialog();
                    subDept = frmSDM.location;
                    SubDeptClass add = new SubDeptClass();
                    add.checkPlacement(MAXplacementID);
                    //add.add_placement(MAXplacementID, subDept);
                }
            }

            this.Close();
        }

        private void btn_cance_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
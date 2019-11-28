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
using System.Text.RegularExpressions;

namespace ShopFloorPlacementPlanner
{
    public partial class frmMoveDept : Form
    {
        public DateTime date { get; set; }
        public int staffID { get; set; }
        public string dept { get; set; }
        public string PT { get; set; }
        public double hours { get; set; }
        public frmMoveDept(int userID, DateTime _date,string placementType,double _hours)
        {
            InitializeComponent();
            PT = placementType;
            hours = _hours;
            date = _date;
            staffID = userID;

            //MessageBox.Show(placementType);
            //MessageBox.Show(hours.ToString()); 


            //fill combobox based on what the userID(allocationDept) allows him
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
            Placement p = new Placement(date, staffID, dept, PT, hours);
            p.addPlacment();
            this.Close();
        }

        private void btn_cance_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

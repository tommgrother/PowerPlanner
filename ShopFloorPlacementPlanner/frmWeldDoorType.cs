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
    public partial class frmWeldDoorType : Form
    {
        public int staff_id { get; set; }
        public frmWeldDoorType()
        {
            InitializeComponent();
            toggle_checkboxes(false);
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDepartment.Text))
                return;

            cmbStaff.Items.Clear();

            string sql = "select forename + ' ' + surname FROM [user_info].dbo.[user] where " +
                "(default_in_department = '" + cmbDepartment.Text + "' or allocation_dept_2 = '" + cmbDepartment.Text + "' or " +
                "allocation_dept_3 = '" + cmbDepartment.Text + "' or allocation_dept_4 = '" + cmbDepartment.Text + "' or " +
                "allocation_dept_5 = '" + cmbDepartment.Text + "'  or allocation_dept_6 = '" + cmbDepartment.Text + "') AND [current] = 1";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                        cmbStaff.Items.Add(dt.Rows[i][0].ToString());
                }

                conn.Close();
            }
        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbDepartment.Text))
            {
                toggle_checkboxes(false);
                staff_id = 260;
                return;
            }

            string sql = "select staff_id " +
                "FROM [user_info].dbo.[staff_door_type_master] s " +
                "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                "where u.forename + ' ' + u.surname = '" + cmbStaff.Text + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                //check if this exists -- if not then insert it into dbo.staff_door_type_master
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var getData = cmd.ExecuteScalar();
                    if (getData == null)
                    {
                        //need to get the staff id 
                        sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + cmbStaff.Text + "' ";

                        int staff_id = 0;

                        using (SqlCommand cmdStaffID = new SqlCommand(sql, conn))
                            staff_id = (int)cmdStaffID.ExecuteScalar();

                        sql = "INSERT INTO [user_info].dbo.[staff_door_type_master] (staff_id,department) VALUES (" + staff_id.ToString() + ",'" + cmbDepartment.Text + "')";
                        using (SqlCommand cmdInsert = new SqlCommand(sql, conn))
                            cmdInsert.ExecuteNonQuery();

                    }
                }

                conn.Close();
            }
            fillDoorTypes();

        }

        private void fillDoorTypes()
        {
            toggle_checkboxes(true);

            //match the chkboxes to the table

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                //need to get the staff id 
                string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + cmbStaff.Text + "' ";

                int staff_id = 0;

                using (SqlCommand cmdStaffID = new SqlCommand(sql, conn))
                    staff_id = (int)cmdStaffID.ExecuteScalar();

                sql = "SELECT" +
                    " [BS476],[thermal],[fire_protect],[SR1],[SR2],[SR3],[SR4],[flood],[SG],[TG],[acoustic],[GP_dufaylite]" +
                    ",[GP_rockwool],[GP_celotex],[everything_else] " +
                    "FROM [user_info].dbo.[staff_door_type_master] where department = '" + cmbDepartment.Text + "' AND staff_id = " + staff_id.ToString();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows[0]["BS476"].ToString() == "-1")
                        chkBS.Checked = true;
                    else
                        chkBS.Checked = false;

                    if (dt.Rows[0]["thermal"].ToString() == "-1")
                        chkThermal.Checked = true;
                    else
                        chkThermal.Checked = false;

                    if (dt.Rows[0]["fire_protect"].ToString() == "-1")
                        chkFireProtect.Checked = true;
                    else
                        chkFireProtect.Checked = false;

                    if (dt.Rows[0]["SR1"].ToString() == "-1")
                        chkSR1.Checked = true;
                    else
                        chkSR1.Checked = false;

                    if (dt.Rows[0]["SR2"].ToString() == "-1")
                        chkSR2.Checked = true;
                    else
                        chkSR2.Checked = false;

                    if (dt.Rows[0]["SR3"].ToString() == "-1")
                        chkSR3.Checked = true;
                    else
                        chkSR3.Checked = false;

                    if (dt.Rows[0]["SR4"].ToString() == "-1")
                        chkSR4.Checked = true;
                    else
                        chkSR4.Checked = false;

                    if (dt.Rows[0]["flood"].ToString() == "-1")
                        chkFlood.Checked = true;
                    else
                        chkFlood.Checked = false;

                    if (dt.Rows[0]["SG"].ToString() == "-1")
                        chkSG.Checked = true;
                    else
                        chkSG.Checked = false;

                    if (dt.Rows[0]["TG"].ToString() == "-1")
                        chkTG.Checked = true;
                    else
                        chkTG.Checked = false;

                    if (dt.Rows[0]["acoustic"].ToString() == "-1")
                        chkAcoutsic.Checked = true;
                    else
                        chkAcoutsic.Checked = false;

                    if (dt.Rows[0]["GP_dufaylite"].ToString() == "-1")
                        chkGPDufaylite.Checked = true;
                    else
                        chkGPDufaylite.Checked = false;

                    if (dt.Rows[0]["GP_rockwool"].ToString() == "-1")
                        chkGPRockwool.Checked = true;
                    else
                        chkGPRockwool.Checked = false;

                    if (dt.Rows[0]["GP_celotex"].ToString() == "-1")
                        chkGPCelotex.Checked = true;
                    else
                        chkGPCelotex.Checked = false;

                    if (dt.Rows[0]["everything_else"].ToString() == "-1")
                        chkEverythingElse.Checked = true;
                    else
                        chkEverythingElse.Checked = false;


                }

                conn.Close();
            }


        }

        private void toggle_checkboxes(bool state)
        {
            chkBS.Enabled = state;
            chkThermal.Enabled = state;
            chkFireProtect.Enabled = state;

            chkSR1.Enabled = state;
            chkSR2.Enabled = state;
            chkSR3.Enabled = state;
            chkSR4.Enabled = state;

            chkFlood.Enabled = state;
            chkSG.Enabled = state;

            chkTG.Enabled = state;
            chkAcoutsic.Enabled = state;
            chkGPDufaylite.Enabled = state;
            chkGPRockwool.Enabled = state;
            chkGPCelotex.Enabled = state;
            chkEverythingElse.Enabled = state;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //update the table to match the new checkboxes

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                //get staff id
                string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + cmbStaff.Text + "' ";
                int staff_id = 0;

                using (SqlCommand cmdStaffID = new SqlCommand(sql, conn))
                    staff_id = (int)cmdStaffID.ExecuteScalar();

                int BS = 0, thermal = 0, fireProtect = 0, sr1 = 0, sr2 = 0, sr3 = 0, sr4 = 0, flood = 0, sg = 0, gp = 0;
                int TG = 0, Acoustic = 0, GPDufaylite = 0, GPRockwool = 0, GPCelotex = 0, everythingElse = 0;

                if (chkBS.Checked == true)
                    BS = -1;
                else
                    BS = 0;

                if (chkThermal.Checked == true)
                    thermal = -1;
                else
                    thermal = 0;

                if (chkFireProtect.Checked == true)
                    fireProtect = -1;
                else
                    fireProtect = 0;

                if (chkSR1.Checked == true)
                    sr1 = -1;
                else
                    sr1 = 0;

                if (chkSR2.Checked == true)
                    sr2 = -1;
                else
                    sr2 = 0;

                if (chkSR3.Checked == true)
                    sr3 = -1;
                else
                    sr3 = 0;

                if (chkSR4.Checked == true)
                    sr4 = -1;
                else
                    sr4 = 0;

                if (chkFlood.Checked == true)
                    flood = -1;
                else
                    flood = 0;

                if (chkGPDufaylite.Checked == true)
                    gp = -1;
                else
                    gp = 0;

                if (chkSG.Checked == true)
                    sg = -1;
                else
                    sg = 0;

                //new additions
                if (chkTG.Checked == true)
                    TG = -1;
                else
                    TG = 0;

                if (chkAcoutsic.Checked == true)
                    Acoustic = -1;
                else
                    Acoustic = 0;

                if (chkGPDufaylite.Checked == true)
                    GPDufaylite = -1;
                else
                    GPDufaylite = 0;

                if (chkGPRockwool.Checked == true)
                    GPRockwool = -1;
                else
                    GPRockwool = 0;

                if (chkGPCelotex.Checked == true)
                    GPCelotex = -1;
                else
                    GPCelotex = 0;

                if (chkEverythingElse.Checked == true)
                    everythingElse = -1;
                else
                    everythingElse = 0;


                ///////////////

                sql = "UPDATE [user_info].dbo.[staff_door_type_master] SET [BS476] = " + BS + ",[thermal]= " + thermal + ",[fire_protect]= " + fireProtect + "," +
                    "[SR1]= " + sr1 + ",[SR2]= " + sr2 + ",[SR3]= " + sr3 + ",[SR4]= " + sr4 + ",[flood]= " + flood + ",[SG]= " + sg + ",[TG]= " + TG + "," +
                    "acoustic = " + Acoustic + ",GP_dufaylite = " + GPDufaylite + ", GP_rockwool = " + GPRockwool  + ", " +
                    "GP_celotex = " + GPCelotex + ",everything_else = " + everythingElse + " " +
                    "WHERE department = '" + cmbDepartment.Text + "' AND staff_id = " + staff_id.ToString();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Door Types for " + cmbStaff.Text + " in " + cmbDepartment.Text + " has been updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                conn.Close();
            }
        }
    }
}

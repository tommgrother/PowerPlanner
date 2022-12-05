using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmSubDeptMultiple : Form
    {
        public string location { get; set; }
        public int count { get; set; }
        public int skipCount { get; set; }
        public int _staff_id { get; set; }
        public int _placement_date { get; set; }
        public static int closeForm { get; set; }
        public frmSubDeptMultiple(int staff_id, int placment_date)
        {
            InitializeComponent();
            skipCount = 0;
            _staff_id = staff_id;
            _placement_date = placment_date;
            closeForm = 0;
            checkCount();

            //add items to the combobox
            //cmbSubDept.Items.Add("Up");
            //cmbSubDept.Items.Add("Wash/Wipe");
            //cmbSubDept.Items.Add("Etch");
            //cmbSubDept.Items.Add("Sand");
            //cmbSubDept.Items.Add("Powder Prime");
            //cmbSubDept.Items.Add("Powder Coat");
            //cmbSubDept.Items.Add("Oven");
            //cmbSubDept.Items.Add("Wet Prep");
            //cmbSubDept.Items.Add("Wet Paint");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //force him to pick one
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                MessageBox.Show("Please select up to 3 departments before continuing");
                return;
            }

            //do this here do they can be used in the weeklyinsert form
            SubDeptClass._up = Convert.ToInt32(chkUp.Checked);
            SubDeptClass._ww = Convert.ToInt32(chkWW.Checked);
            SubDeptClass._etch = Convert.ToInt32(chkEtch.Checked);
            SubDeptClass._sand = Convert.ToInt32(chkSand.Checked);
            SubDeptClass._pp = Convert.ToInt32(chkPP.Checked);
            SubDeptClass._pc = Convert.ToInt32(chkPC.Checked);
            SubDeptClass._oven = Convert.ToInt32(chkOven.Checked);
            SubDeptClass._wet_prep = Convert.ToInt32(chkWetPrep.Checked);
            SubDeptClass._wet_paint = Convert.ToInt32(chkWetPaint.Checked);


            SubDeptClass add = new SubDeptClass();
            add.checkPlacement(_placement_date);



            string sql = "";

            if (add.alreadyPlaced == false)
            {
                sql = "insert into dbo.power_plan_paint_sub_dept (staff_id,placement_id,up,wash_wipe,etch,sand,powder_prime,powder_coat,oven,wet_prep,wet_paint) VALUES (" +
                    _staff_id + "," + _placement_date + ", " + Convert.ToInt32(chkUp.Checked) + "," + Convert.ToInt32(chkWW.Checked) + "," + Convert.ToInt32(chkEtch.Checked) + "," + Convert.ToInt32(chkSand.Checked) + "," + Convert.ToInt32(chkPP.Checked) + ", " + Convert.ToInt32(chkPC.Checked) + "," + Convert.ToInt32(chkOven.Checked) + ", " + Convert.ToInt32(chkWetPrep.Checked) + "," + Convert.ToInt32(chkWetPaint.Checked) + ")";
            }
            else
            {
                sql = "UPDATE dbo.power_plan_paint_sub_dept SET up =" + Convert.ToInt32(chkUp.Checked) + ",wash_wipe = " + Convert.ToInt32(chkWW.Checked) + ", etch =" + Convert.ToInt32(chkEtch.Checked) + ",sand = " + Convert.ToInt32(chkSand.Checked) + ",powder_prime = " + Convert.ToInt32(chkPP.Checked) + "," +
                    "powder_coat = " + Convert.ToInt32(chkPC.Checked) + ",oven = " + Convert.ToInt32(chkOven.Checked) + ", wet_prep = " + Convert.ToInt32(chkWetPrep.Checked) + ",wet_paint = " + Convert.ToInt32(chkWetPaint.Checked) + " where placement_id = " + _placement_date;
            }
            //MessageBox.Show(sql, "ERROR", MessageBoxButtons.OK);

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                }
            }
        }
        private void checkCount()
        {
            if (count == 3)
            {
                if (chkUp.Checked == false)
                    chkUp.Enabled = false;
                if (chkWW.Checked == false)
                    chkWW.Enabled = false;
                if (chkEtch.Checked == false)
                    chkEtch.Enabled = false;
                if (chkSand.Checked == false)
                    chkSand.Enabled = false;
                if (chkPP.Checked == false)
                    chkPP.Enabled = false;
                if (chkPC.Checked == false)
                    chkPC.Enabled = false;
                if (chkOven.Checked == false)
                    chkOven.Enabled = false;
                if (chkWetPrep.Checked == false)
                    chkWetPrep.Enabled = false;
                if (chkWetPaint.Checked == false)
                    chkWetPaint.Enabled = false;
            }
            else
            {
                chkUp.Enabled = true;
                chkWW.Enabled = true;
                chkEtch.Enabled = true;
                chkSand.Enabled = true;
                chkPP.Enabled = true;
                chkPC.Enabled = true;
                chkOven.Enabled = true;
                chkWetPrep.Enabled = true;
                chkWetPaint.Enabled = true;
            }

            //after doing that count we also need to check if the departments have more than 3 people in them
            string sql = "";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                DateTime date_plan;
                sql = "select date_plan from dbo.power_plan_staff left join dbo.power_plan_date on power_plan_staff.date_id = power_plan_date.id where power_plan_staff.id = " + _placement_date;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                     date_plan = Convert.ToDateTime(cmd.ExecuteScalar());
                    conn.Close();
                }
                sql = "select coalesce(SUM(up),0) as up,coalesce(SUM(wash_wipe),0) as wash_wipe,coalesce(SUM(etch),0) as etch,coalesce(SUM(sand),0) as sand,coalesce(SUM(powder_prime),0) as powder_prime,coalesce(SUM(powder_coat),0) as powder_coat,coalesce(SUM(oven),0) as oven," +
               "coalesce(SUM(wet_prep),0) as wet_prep,coalesce(SUM(wet_paint),0) as wet_paint from dbo.power_plan_paint_sub_dept left join dbo.power_plan_staff on dbo.power_plan_paint_sub_dept.placement_id = dbo.power_plan_staff.id " +
               "left join dbo.power_plan_date on power_plan_date.id = dbo.power_plan_staff.date_id   where   dbo.power_plan_staff.id is not null and date_plan = '" + date_plan.ToString("yyyy-MM-dd") + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //go through each of the columns and if they are at 3 then they need to be locked
                    
                    if (Convert.ToInt32(dt.Rows[0][0]) > 2)//UP
                    {
                        chkUp.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][1]) > 2) //WASH/WIPE
                    {
                        chkWW.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][2]) > 2) //ETCH
                    {
                        chkEtch.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][3]) > 2)//SAND
                    {
                        chkSand.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][4]) > 2)//PP
                    {
                        chkPP.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][5]) > 2)//PC
                    {
                        chkPC.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][6]) > 2)//OVEN
                    {
                        chkOven.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][7]) > 2)//WETPREP
                    {
                        chkWetPrep.Enabled = false;
                        closeForm++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][8]) > 2)//WETPAINT
                    {
                        chkWetPaint.Enabled = false;
                        closeForm++;
                    }

                }
            }


        }

        private void chkUp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUp.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkWW_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWW.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkEtch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEtch.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkSand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSand.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkPP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPP.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkPC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPC.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkOven_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOven.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkWetPrep_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWetPrep.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void chkWetPaint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWetPaint.Checked == true)
            {
                count++;
            }
            else
            {
                count--;
            }
            checkCount();
        }

        private void frmSubDeptMultiple_Shown(object sender, EventArgs e)
        {
                        if (closeForm >= 9)
            {
                MessageBox.Show("There are no available sub departments left.");
                this.Close();
            }
        }
    }
}
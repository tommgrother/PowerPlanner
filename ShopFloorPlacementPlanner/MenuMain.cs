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
using System.Drawing.Printing;

namespace ShopFloorPlacementPlanner
{
    public partial class MenuMain : Form
    {
        public MenuMain()
        {
            InitializeComponent();
        
        }

        private void btnAddPunch_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Punching", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void btnAddBend_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Bending", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void btnAddWeld_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Welding", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void btnAddBuff_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Dressing", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void btnAddPaint_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Painting", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void btnAddPack_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Packing", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }


        private void MenuMain_Load(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void countGrid()
        {

            double slimlineHours = 0;
            double punchHours = 0;
            double laserHours = 0;
            double bendHours = 0;
            double weldHours = 0;
            double buffHours = 0;
            double paintHours = 0;
            double packHours = 0;

            double slimlineMen = 0;
            double punchMen = 0;
            double laserMen = 0;
            double bendMen = 0;
            double weldMen = 0;
            double buffMen = 0;
            double paintMen = 0;
            double packMen = 0;

            double slimlineOT = 0;
            double punchOT = 0;
            double laserOT = 0;
            double bendOT = 0;
            double weldOT = 0;
            double buffOT = 0;
            double paintOT = 0;
            double packOT = 0;


            double slimlineAD = 0;
            double punchAD = 0;
            double laserAD = 0;
            double bendAD = 0;
            double weldAD = 0;
            double buffAD = 0;
            double paintAD = 0;
            double packAD = 0;


            foreach (DataGridViewRow row in dgSlimline.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    slimlineMen = slimlineMen + 0.5;
                }
                else
                {
                    slimlineMen = slimlineMen + 1;
                }


                slimlineHours = slimlineHours + Convert.ToDouble(row.Cells[1].Value);

            }

            foreach (DataGridViewRow row in dgPunch.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    punchMen = punchMen + 0.5;
                }
                else
                {
                    punchMen = punchMen + 1;
                }


                punchHours = punchHours + Convert.ToDouble(row.Cells[1].Value);

            }

            foreach (DataGridViewRow row in dgLaser.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    laserMen = laserMen + 0.5;
                }
                else
                {
                    laserMen = laserMen + 1;
                }



                laserHours = laserHours + Convert.ToDouble(row.Cells[1].Value);
            }

            foreach (DataGridViewRow row in dgBend.Rows)
            {

                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    bendMen = bendMen + 0.5;
                }
                else
                {
                    bendMen = bendMen + 1;
                }


                bendHours = bendHours + Convert.ToDouble(row.Cells[1].Value);
            }

            foreach (DataGridViewRow row in dgWeld.Rows)
            {

                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    weldMen = weldMen + 0.5;
                }
                else
                {
                    weldMen = weldMen + 1;
                }

                weldHours = weldHours  + Convert.ToDouble(row.Cells[1].Value);
            }

            foreach (DataGridViewRow row in dgBuff.Rows)
            {

                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    buffMen = buffMen + 0.5;
                }
                else
                {
                    buffMen = buffMen + 1;
                }



                buffHours = buffHours + Convert.ToDouble(row.Cells[1].Value);
            }

            foreach (DataGridViewRow row in dgPaint.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    paintMen = paintMen + 0.5;
                }
                else
                {
                    paintMen = paintMen + 1;
                }


                paintHours = paintHours + Convert.ToDouble(row.Cells[1].Value);
            }

            foreach (DataGridViewRow row in dgPack.Rows)
            {

                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    packMen = packMen + 0.5;
                }
                else
                {
                    packMen = packMen + 1;
                }

                packHours  = packHours  + Convert.ToDouble(row.Cells[1].Value);
            }


            txtSlimlineHours.Text = slimlineHours.ToString();
            txtPunchHours.Text = punchHours.ToString();
            txtLaserHours.Text = laserHours.ToString();
            txtBendHours.Text = bendHours.ToString();
            txtWeldHours.Text = weldHours.ToString();
            txtBuffHours.Text = buffHours.ToString();
            txtPaintHours.Text = paintHours.ToString();
            txtPackHours.Text = packHours.ToString();

            txtSlimlineMen.Text = slimlineMen.ToString();
            txtPunchMen.Text = punchMen.ToString();
            txtLaserMen.Text = laserMen.ToString();
            txtBendMen.Text = bendMen.ToString();
            txtWeldMen.Text = weldMen.ToString();
            txtBuffMen.Text = buffMen.ToString();
            txtPaintMen.Text = paintMen.ToString();
            txtPackMen.Text = packMen.ToString();


            //////////ADDS OVERTIME AND ADDITIONS TO THE MAIN SCREEN

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            Overtime o = new Overtime();
            o.getDateID(Convert.ToDateTime(dteDateSelection.Text));
       
         
            using(SqlCommand cmd = new SqlCommand("SELECT * from dbo.power_plan_overtime where date_id = @dateID", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@dateID", o._dateID);


                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    slimlineOT = Convert.ToDouble(rdr["slimline_OT"]) * 0.8;
                    laserOT = Convert.ToDouble(rdr["laser_OT"]) * 0.8;
                    punchOT = Convert.ToDouble(rdr["punching_OT"]) * 0.8;
                    bendOT = Convert.ToDouble(rdr["bending_OT"]) * 0.8;
                    weldOT = Convert.ToDouble(rdr["welding_OT"]) * 0.8;
                    buffOT = Convert.ToDouble(rdr["buffing_OT"]) * 0.8;
                    paintOT = Convert.ToDouble(rdr["painting_OT"]) * 0.8;
                    packOT = Convert.ToDouble(rdr["packing_OT"]) * 0.8;



                    slimlineAD = Convert.ToDouble(rdr["slimline_ad"]);
                    punchAD = Convert.ToDouble(rdr["punching_AD"]);
                    laserAD = Convert.ToDouble(rdr["laser_AD"]);
                    bendAD = Convert.ToDouble(rdr["bending_AD"]);
                    weldAD = Convert.ToDouble(rdr["welding_AD"]);
                    buffAD = Convert.ToDouble(rdr["buffing_AD"]);
                    paintAD = Convert.ToDouble(rdr["painting_AD"]);
                    packAD = Convert.ToDouble(rdr["packing_AD"]);
                }


                conn.Close();

            }


            txtSlimlineOT.Text = slimlineOT.ToString();
            txtLaserOT.Text = laserOT.ToString();
            txtPunchOT.Text = punchOT.ToString();
            txtBendOT.Text = bendOT.ToString();
            txtWeldOT.Text = weldOT.ToString();
            txtBuffOT.Text = buffOT.ToString();
            txtPaintOT.Text = paintOT.ToString();
            txtPackOT.Text = packOT.ToString();



            txtSlimlineAD.Text = slimlineAD.ToString();
            txtLaserAD.Text = laserAD.ToString();
            txtPunchAD.Text = punchAD.ToString();
            txtBendAD.Text = bendAD.ToString();
            txtWeldAD.Text = weldAD.ToString();
            txtBuffAD.Text = buffAD.ToString();
            txtPaintAD.Text = paintAD.ToString();
            txtPackAD.Text = packAD.ToString();



            /////////////////////////////






            //TOTALS UP FINAL VALUES

            txtSlimlineTotal.Text = (Convert.ToDouble(txtSlimlineHours.Text) + Convert.ToDouble(slimlineOT) + Convert.ToDouble(slimlineAD)).ToString();
            txtLaserTotal.Text = (Convert.ToDouble(txtLaserHours.Text) + Convert.ToDouble(laserOT) + Convert.ToDouble(laserAD)).ToString();
            txtPunchingTotal.Text = (Convert.ToDouble(txtPunchHours.Text) + Convert.ToDouble(punchOT) + Convert.ToDouble(punchAD)).ToString();
            txtBendingTotal.Text = (Convert.ToDouble(txtBendHours.Text) + Convert.ToDouble(bendOT) + Convert.ToDouble(bendAD)).ToString();
            txtWeldingTotal.Text = (Convert.ToDouble(txtWeldHours.Text) + Convert.ToDouble(weldOT) + Convert.ToDouble(weldAD)).ToString();
            txtBuffingTotal.Text = (Convert.ToDouble(txtBuffHours.Text) + Convert.ToDouble(buffOT) + Convert.ToDouble(buffAD)).ToString();
            txtPaintingTotal.Text = (Convert.ToDouble(txtPaintHours.Text) + Convert.ToDouble(paintOT) + Convert.ToDouble(paintAD)).ToString();
            txtPackingTotal.Text = (Convert.ToDouble(txtPackHours.Text) + Convert.ToDouble(packOT) + Convert.ToDouble(packAD)).ToString();


        }


        private void paintGrid()
        {

            //slimline
            foreach (DataGridViewRow row in dgSlimline.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgSlimline.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            //PUNCH
            foreach (DataGridViewRow row in dgPunch.Rows)
                    if (row.Cells[0].Value.ToString().Contains("Shift"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                foreach (DataGridViewRow row in dgPunch.Rows)
                    if (row.Cells[0].Value.ToString().Contains("Half"))
                    {
                        row.DefaultCellStyle.BackColor = Color.MediumPurple;
                    }
            //LASER
            foreach (DataGridViewRow row in dgLaser.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgLaser.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }

            //Bend
            foreach (DataGridViewRow row in dgBend.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgBend.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }



            //Weld
            foreach (DataGridViewRow row in dgWeld.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgWeld.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }

            //Buff
            foreach (DataGridViewRow row in dgBuff.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgBuff.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }

            //Paint
            foreach (DataGridViewRow row in dgPaint.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgPaint.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }

            //Pack
            foreach (DataGridViewRow row in dgPack.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgPack.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }

            //NOT PLACED
            foreach (DataGridViewRow row in dgNotPlaced.Rows)
                try
                {
                    if (row.Cells[1].Value.ToString().Contains("HOLIDAY"))
                    {
                        row.DefaultCellStyle.BackColor = Color.MediumPurple;
                    }
                }
                catch
                {

                }
            foreach (DataGridViewRow row in dgNotPlaced.Rows)
                try
                {
                    if (row.Cells[1].Value.ToString().Contains("ABSENT"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Salmon;
                    }

                }
                catch
                {

                }






            dgSlimline.ClearSelection();
            dgPunch.ClearSelection();
            dgLaser.ClearSelection();
            dgBend.ClearSelection();
            dgWeld.ClearSelection();
            dgBuff.ClearSelection();
            dgPaint.ClearSelection();
            dgPack.ClearSelection();


        }


        private void fillgrid()
        {
            fillSlimline();
            fillPunch();
            fillLaser();
            fillBend();
            fillWeld();
            fillBuff();
            fillPaint();
            fillPack();
            fillNotPlaced();
            paintGrid();
            countGrid();


            DataGridViewColumn columnSlimline = dgSlimline.Columns[1];
            columnSlimline.Width = 40;

            DataGridViewColumn columnLaser = dgLaser.Columns[1];
            columnLaser.Width = 40;

            DataGridViewColumn columnPunch = dgPunch.Columns[1];
            columnPunch.Width = 40;

            DataGridViewColumn columnBend = dgBend.Columns[1];
            columnBend.Width = 40;

            DataGridViewColumn columnWeld = dgWeld.Columns[1];
            columnWeld.Width = 40;

            DataGridViewColumn columnBuff = dgBuff.Columns[1];
            columnBuff.Width = 40;

            DataGridViewColumn columnPaint = dgPaint.Columns[1];
            columnPaint.Width = 40;

            DataGridViewColumn columnPack = dgPack.Columns[1];
            columnPack.Width = 40;
        }



        private void fillSlimline()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Slimline");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgSlimline.DataSource = dt;

            conn.Close();

        }



        private void fillPunch()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Punching");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgPunch.DataSource = dt;

            conn.Close();

        }

        private void fillLaser()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Laser");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgLaser.DataSource = dt;

            conn.Close();

        }



        private void fillBend()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Bending");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgBend.DataSource = dt;

            conn.Close();


        }

        private void fillWeld()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Welding");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgWeld.DataSource = dt;

            conn.Close();




        }

        private void fillBuff()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Dressing");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgBuff.DataSource = dt;

            conn.Close();




        }

        private void fillPaint()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Painting");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgPaint.DataSource = dt;

            conn.Close();




        }

        private void fillPack()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Packing");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgPack.DataSource = dt;

            conn.Close();
        }


        private void fillNotPlaced()
        {
            //CLEARS ALL EXISTING SELECTION FROM THE TABLE FOR THIS DEPARTMENT
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmdDate = new SqlCommand("usp_get_unplaced_staff", conn);
            cmdDate.CommandType = CommandType.StoredProcedure;
            cmdDate.Parameters.AddWithValue("@placementDate", SqlDbType.Date).Value = dteDateSelection.Text;



            SqlDataAdapter da = new SqlDataAdapter(cmdDate);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dgNotPlaced.DataSource = dt;


        }





        private void dteDateSelection_ValueChanged(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Laser", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void copyPlacementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCopyPlacements cp = new frmCopyPlacements(Convert.ToDateTime(dteDateSelection.Text));
            cp.ShowDialog();
            fillgrid();
        }

        private void btnAddSlimline_Click(object sender, EventArgs e)
        {
            frmSelectStaff frmSS = new frmSelectStaff("Slimline", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void sendToDailyGoalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double goalHoursSlimline;
            double goalHoursLaser;
            double goalHoursPunch;
            double goalHoursBend;
            double goalHoursWeld;
            double goalHoursBuff;
            double goalHoursPaint;
            double goalHoursPack;

            double manPowerSlimline;
            double manPowerLaser;
            double manPowerPunch;
            double manPowerBend;
            double manPowerWeld;
            double manPowerBuff;
            double manPowerPaint;
            double manPowerPack;


            goalHoursSlimline = Convert.ToDouble(txtSlimlineHours.Text) + Convert.ToDouble(txtSlimlineOT.Text) + Convert.ToDouble(txtSlimlineAD.Text);
            goalHoursLaser = Convert.ToDouble(txtLaserHours.Text) + Convert.ToDouble(txtLaserOT.Text) + Convert.ToDouble(txtLaserAD.Text);
            goalHoursPunch = Convert.ToDouble(txtPunchHours.Text) + Convert.ToDouble(txtPunchOT.Text) + Convert.ToDouble(txtPunchAD.Text);
            goalHoursBend = Convert.ToDouble(txtBendHours.Text) + Convert.ToDouble(txtBendOT.Text) + Convert.ToDouble(txtBendAD.Text);
            goalHoursWeld = Convert.ToDouble(txtWeldHours.Text) + Convert.ToDouble(txtWeldOT.Text) + Convert.ToDouble(txtWeldAD.Text);
            goalHoursBuff = Convert.ToDouble(txtBuffHours.Text) + Convert.ToDouble(txtBuffOT.Text) + Convert.ToDouble(txtBuffAD.Text);
            goalHoursPaint = Convert.ToDouble(txtPaintHours.Text) + Convert.ToDouble(txtPaintOT.Text) + Convert.ToDouble(txtPaintAD.Text);
            goalHoursPack = Convert.ToDouble(txtPackHours.Text) + Convert.ToDouble(txtPackOT.Text) + Convert.ToDouble(txtPackAD.Text);


            manPowerSlimline = Convert.ToDouble(txtSlimlineMen.Text);
            manPowerLaser = Convert.ToDouble(txtLaserMen.Text);
            manPowerPunch = Convert.ToDouble(txtPunchMen.Text);
            manPowerBend = Convert.ToDouble(txtBendMen.Text);
            manPowerWeld = Convert.ToDouble(txtWeldMen.Text);
            manPowerBuff = Convert.ToDouble(txtBuffMen.Text);
            manPowerPaint = Convert.ToDouble(txtPaintMen.Text);
            manPowerPack = Convert.ToDouble(txtPackMen.Text);

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.daily_department_goal set " +
                "goal_hours_slimline = @goalHoursSlimline, " +
                "goal_hours_laser = @goalHoursLaser, " +
                "goal_hours_punch = @goalHoursPunch, " +
                "goal_hours_bend = @goalHoursBend, " +
                "goal_hours_weld = @goalHoursWeld, " +
                "goal_hours_buff = @goalHoursBuff, " +
                "goal_hours = @goalHoursPaint, " +
                "goal_hours_pack = @goalHoursPack, " +
                "man_power_slimline = @manPowerSlimline, " +
                "man_power_laser = @manPowerLaser, " +
                "man_power_punch = @manPowerPunch, " +
                "man_power_bend = @manPowerBend, " +
                "man_power_weld = @manPowerWeld, " +
                "man_power_buff = @manPowerBuff, " +
                "man_power_paint = @manPowerPaint, " +
                "man_power_pack = @manPowerPack " +
                " WHERE date_goal = @dateGoal",conn))
            {

                cmd.Parameters.AddWithValue("@goalHoursSlimline", goalHoursSlimline);
                cmd.Parameters.AddWithValue("@goalHoursLaser", goalHoursLaser);
                cmd.Parameters.AddWithValue("@goalHoursPunch", goalHoursPunch);
                cmd.Parameters.AddWithValue("@goalHoursBend", goalHoursBend);
                cmd.Parameters.AddWithValue("@goalHoursWeld", goalHoursWeld);
                cmd.Parameters.AddWithValue("@goalHoursBuff", goalHoursBuff);
                cmd.Parameters.AddWithValue("@goalHoursPaint", goalHoursPaint);
                cmd.Parameters.AddWithValue("@goalHoursPack", goalHoursPack);

                cmd.Parameters.AddWithValue("@manPowerSlimline", manPowerSlimline);
                cmd.Parameters.AddWithValue("@manPowerLaser", manPowerLaser);
                cmd.Parameters.AddWithValue("@manPowerPunch", manPowerPunch);
                cmd.Parameters.AddWithValue("@manPowerBend", manPowerBend);
                cmd.Parameters.AddWithValue("@manPowerWeld", manPowerWeld);
                cmd.Parameters.AddWithValue("@manPowerBuff", manPowerBuff);
                cmd.Parameters.AddWithValue("@manPowerPaint", manPowerPaint);
                cmd.Parameters.AddWithValue("@manPowerPack", manPowerPack);
                cmd.Parameters.AddWithValue("@dateGoal", dteDateSelection.Text);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hours successfully sent to daily goals!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("An error has occured, if this error persists please contact IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }


        }

        private void printDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmailPrint ep = new frmEmailPrint(Convert.ToDateTime(dteDateSelection.Text));
            ep.ShowDialog();
        }

        private void printScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                Image bit = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

                Graphics gs = Graphics.FromImage(bit);

                gs.CopyFromScreen(new Point(0, 0), new Point(0, 0), bit.Size);

                bit.Save(@"C:\temp\temp.jpg");

                printImage();
            }
            catch
            {

            }


        }

        private void printImage()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (sender, args) =>
                {
                    Image i = Image.FromFile(@"C:\temp\temp.jpg");
                    Point p = new Point(100, 100);
                    args.Graphics.DrawImage(i, args.MarginBounds);

                };

                pd.DefaultPageSettings.Landscape = true;
                Margins margins = new Margins(50, 50, 50, 50);
                pd.DefaultPageSettings.Margins = margins;
                pd.Print();
            }
            catch
            {

            }
        }

        private void updateAutomaticAllocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CLEARS ALL EXISTING SELECTION FROM THE TABLE FOR THIS DEPARTMENT
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("usp_power_planner_auto_placement", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            
        }

        private void loadDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("usp_power_planner_load_defaults", conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@placementDate", SqlDbType.Date).Value = dteDateSelection.Text;
                
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            fillgrid();



        }
    }
}

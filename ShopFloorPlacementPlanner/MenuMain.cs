﻿using System;
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
                    slimlineOT = Convert.ToDouble(rdr["slimline_OT"]);
                    laserOT = Convert.ToDouble(rdr["laser_OT"]);
                    punchOT = Convert.ToDouble(rdr["punching_OT"]);
                    bendOT = Convert.ToDouble(rdr["bending_OT"]);
                    weldOT = Convert.ToDouble(rdr["welding_OT"]);
                    buffOT = Convert.ToDouble(rdr["buffing_OT"]);
                    paintOT = Convert.ToDouble(rdr["painting_OT"]);
                    packOT = Convert.ToDouble(rdr["packing_OT"]);
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
    }
}

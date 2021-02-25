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
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    public partial class MenuMain : Form
    {
        public int skipMessageBox { get; set; }
        public MenuMain()
        {
            InitializeComponent();

        }

        private void btnAddPunch_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Punching", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();

        }

        private void btnAddBend_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Bending", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }

        private void btnAddWeld_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Welding", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }

        private void btnAddBuff_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Dressing", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }

        private void btnAddPaint_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Painting", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }

        private void btnAddPack_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Packing", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
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
            double storesMen = 0;

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

            foreach (DataGridViewRow row in dgStores.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    storesMen = storesMen + 0.5;
                }
                else
                {
                    storesMen = storesMen + 1;
                }
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


                punchHours = punchHours + Convert.ToDouble(row.Cells[1].Value); //123456

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
            int columnIndex = 0;
            columnIndex = dgWeld.Columns["Staff Placement"].Index;
            foreach (DataGridViewRow row in dgWeld.Rows)
            {
                // MessageBox.Show(row.Cells[columnIndex].Value.ToString());

                if (row.Cells[columnIndex].Value.ToString().Contains("Half"))
                {
                    weldMen = weldMen + 0.5;
                }
                else
                {
                    weldMen = weldMen + 1;
                }
                int hoursIndex = dgWeld.Columns["hours"].Index;
                weldHours = weldHours + Convert.ToDouble(row.Cells[hoursIndex].Value);
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

                packHours = packHours + Convert.ToDouble(row.Cells[1].Value);
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
            txtStoresMen.Text = storesMen.ToString();


            //////////ADDS OVERTIME AND ADDITIONS TO THE MAIN SCREEN

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            Overtime o = new Overtime();
            o.getDateID(Convert.ToDateTime(dteDateSelection.Text));


            using (SqlCommand cmd = new SqlCommand("SELECT * from dbo.power_plan_overtime where date_id = @dateID", conn))
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


            //add the actual hours 

            string sql = "SELECT ROUND(COALESCE(actual_hours_slimline,0),2) as slimline, ROUND(COALESCE(actual_hours_punch,0),2) as punch, ROUND(COALESCE(actual_hours_laser,0),2) as laser, ROUND(COALESCE(actual_hours_bend,0),2) as bend, ROUND(COALESCE(actual_hours_weld,0),2) as weld, ROUND(COALESCE(actual_hours_buff,0),2) as buff,ROUND(COALESCE(actual_hours,0),2) as paint , ROUND(COALESCE(actual_hours_pack,0),2) as boxes FROM dbo.daily_department_goal WHERE date_goal ='" + dteDateSelection.Text + "'";
            //actual_hours paint is tbc
            using (SqlConnection conn2 = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd2 = new SqlCommand(sql, conn2))
                {
                    conn2.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //now we go through each column and grab the data
                    foreach (DataRow row in dt.Rows)
                    {
                        txtSLActualHours.Text = row[0].ToString();
                        txtPunchActualHours.Text = row[1].ToString();
                        txtlaserActualHours.Text = row[2].ToString();
                        txtBendActualHours.Text = row[3].ToString();
                        txtWeldActualHours.Text = row[4].ToString();
                        txtBuffActualHours.Text = row[5].ToString();
                        txtPaintActualHours.Text = row[6].ToString();
                        txtPackActualHours.Text = row[7].ToString();
                    }



                    conn2.Close();
                }
            }
            // C O L O U R S
            //slimline
            if (Convert.ToDecimal(txtSLActualHours.Text) > Convert.ToDecimal(txtSlimlineTotal.Text))
                lblSLActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblSLActualHours.BackColor = Color.PaleVioletRed;
            //punch
            if (Convert.ToDecimal(txtPunchActualHours.Text) > Convert.ToDecimal(txtPunchingTotal.Text))
                lblPunchActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblPunchActualHours.BackColor = Color.PaleVioletRed;
            //laser
            if (Convert.ToDecimal(txtlaserActualHours.Text) > Convert.ToDecimal(txtLaserTotal.Text))
                lblLaserActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblLaserActualHours.BackColor = Color.PaleVioletRed;
            //bending
            if (Convert.ToDecimal(txtBendActualHours.Text) > Convert.ToDecimal(txtBendingTotal.Text))
                lblBendingActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblBendingActualHours.BackColor = Color.PaleVioletRed;
            //welding
            if (Convert.ToDecimal(txtWeldActualHours.Text) > Convert.ToDecimal(txtWeldingTotal.Text))
                lblWeldingHours.BackColor = Color.DarkSeaGreen;
            else
                lblWeldingHours.BackColor = Color.PaleVioletRed;
            //buffing
            if (Convert.ToDecimal(txtBuffActualHours.Text) > Convert.ToDecimal(txtBuffingTotal.Text))
                lblBuffingActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblBuffingActualHours.BackColor = Color.PaleVioletRed;
            //painting
            if (Convert.ToDecimal(txtPaintActualHours.Text) > Convert.ToDecimal(txtPaintingTotal.Text))
                lblPaintingActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblPaintingActualHours.BackColor = Color.PaleVioletRed;
            //packing
            if (Convert.ToDecimal(txtPackActualHours.Text) > Convert.ToDecimal(txtPackingTotal.Text))
                lblPackingActualHours.BackColor = Color.DarkSeaGreen;
            else
                lblPackingActualHours.BackColor = Color.PaleVioletRed;

        }


        private void paintGrid()
        {
            int placementID = 0;
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
            //corey added this 02/09/2020
            foreach (DataGridViewRow row in dgSlimline.Rows)
            {
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) > Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                }
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) < Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.DarkSeaGreen;
                }
            }



            foreach (DataGridViewRow row in dgSlimline.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
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
            foreach (DataGridViewRow row in dgPunch.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }

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
            foreach (DataGridViewRow row in dgLaser.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            //corey added this 02/09/2020
            foreach (DataGridViewRow row in dgLaser.Rows)
            {           //hours is less than worked
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) > Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                }
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) < Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.DarkSeaGreen;
                }
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
            foreach (DataGridViewRow row in dgBend.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            //corey added this 28/10/2020
            foreach (DataGridViewRow row in dgBend.Rows)
            {           //hours is less than worked
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) > Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                }
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) < Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.DarkSeaGreen;
                }
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
            foreach (DataGridViewRow row in dgWeld.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            //corey added this 25/08/2020
            foreach (DataGridViewRow row in dgWeld.Rows)
            {           //hours is less than worked
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) > Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                }
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) < Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.DarkSeaGreen;
                }
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
            foreach (DataGridViewRow row in dgBuff.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
                //hours is less than worked
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) > Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                }
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) < Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.DarkSeaGreen;
                }


            }
            //ryucxd PAINT IS COMMENTED OUT BECAUSE THERE IS A FUCK LOAD OF ERRORS RN
            //Paint
            foreach (DataGridViewRow row in dgPaint.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgPaint.Rows)
                if (row.Cells[1].Value.ToString().Contains("3.20") || row.Cells[1].Value.ToString().Contains("2.80"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgPaint.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
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
            foreach (DataGridViewRow row in dgPack.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
                //hours is less than worked
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) > Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                }
                if ((Convert.ToDouble(row.Cells["hours"].Value) + (Convert.ToDouble(row.Cells["overtime"].Value) * 0.8)) < Convert.ToDouble(row.Cells["worked"].Value))
                {
                    row.Cells[4].Style.BackColor = Color.DarkSeaGreen;
                }
            }

            //Stores
            foreach (DataGridViewRow row in dgStores.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgStores.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgStores.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            //Dispatch
            foreach (DataGridViewRow row in dgDispatch.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgDispatch.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgDispatch.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            //ToolRoom
            foreach (DataGridViewRow row in dgToolRoom.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgToolRoom.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgToolRoom.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }

            //Cleaning
            foreach (DataGridViewRow row in dgCleaning.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgCleaning.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgCleaning.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }

            //Management
            foreach (DataGridViewRow row in dgManagement.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgManagement.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgManagement.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
            }

            //Health and safety
            foreach (DataGridViewRow row in dgHS.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgHS.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            foreach (DataGridViewRow row in dgHS.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[2].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

                pnc.checkNonStandard();

                if (pnc._nonStandardPlacment == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                }
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
                    continue;
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
                    continue;
                }


            foreach (DataGridViewRow row in dgNotPlaced.Rows)
                try
                {
                    if (row.Cells[1].Value.ToString().Contains("UNPAID"))
                    {
                        row.DefaultCellStyle.BackColor = Color.DeepPink;
                    }

                }
                catch
                {
                    continue;
                }

            dgSlimline.ClearSelection();
            dgPunch.ClearSelection();
            dgLaser.ClearSelection();
            dgBend.ClearSelection();
            dgWeld.ClearSelection();
            dgBuff.ClearSelection();
            dgPaint.ClearSelection();
            dgPack.ClearSelection();
            dgStores.ClearSelection();
            dgDispatch.ClearSelection();
            dgToolRoom.ClearSelection();
            dgCleaning.ClearSelection();
            dgManagement.ClearSelection();
            dgHS.ClearSelection();
            dgNotPlaced.ClearSelection();

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
            fillStores();
            fillDispatch();
            fillToolroom();
            fillCleaning();
            fillManagement();
            fillHS();
            fillNotPlaced();
            paintGrid();
            countGrid();
            countMen();

            DataGridViewColumn columnSlimlineID = dgSlimline.Columns[2];
            columnSlimlineID.Visible = false;
            DataGridViewColumn columnSlimline = dgSlimline.Columns[1];
            columnSlimline.Width = 40;
            dgSlimline.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlimline.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnLaserID = dgLaser.Columns[2];
            columnLaserID.Visible = false;
            DataGridViewColumn columnLaser = dgLaser.Columns[1];
            columnLaser.Width = 40;
            dgLaser.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgLaser.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewColumn columnPunchID = dgPunch.Columns[2];
            columnPunchID.Visible = false;
            DataGridViewColumn columnPunch = dgPunch.Columns[1];
            columnPunch.Width = 40;
            dgPunch.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPunch.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnBendID = dgBend.Columns[2];
            columnBendID.Visible = false;
            DataGridViewColumn columnBend = dgBend.Columns[1];
            columnBend.Width = 40;
            dgBend.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgBend.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //here here here here
            DataGridViewColumn columnWeldID = dgWeld.Columns[2];
            columnWeldID.Visible = false;
            DataGridViewColumn columnWeld = dgWeld.Columns[1];
            columnWeld.Width = 40;

            DataGridViewColumn columnBuffID = dgBuff.Columns[2];
            columnBuffID.Visible = false;
            DataGridViewColumn columnBuff = dgBuff.Columns[1];
            columnBuff.Width = 40;
            dgBuff.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgBuff.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //dgBuff.RowTemplate.Height = 55;


            DataGridViewColumn columnPaintID = dgPaint.Columns[2];
            columnPaintID.Visible = false;
            DataGridViewColumn columnPaint = dgPaint.Columns[1];
            columnPaint.Width = 40;


            DataGridViewColumn columnPackID = dgPack.Columns[2];
            columnPackID.Visible = false;
            DataGridViewColumn columnPack = dgPack.Columns[1];
            columnPack.Width = 40;
            dgPack.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPack.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnStoresID = dgStores.Columns[2];
            columnStoresID.Visible = false;
            DataGridViewColumn columnStores = dgStores.Columns[1];
            columnStores.Width = 40;
            dgStores.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgStores.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnDispatchID = dgDispatch.Columns[2];
            columnDispatchID.Visible = false;
            DataGridViewColumn columnDispatch = dgDispatch.Columns[1];
            columnDispatch.Width = 40;
            dgDispatch.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgDispatch.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnToolRoomID = dgToolRoom.Columns[2];
            columnToolRoomID.Visible = false;
            DataGridViewColumn columnToolroom = dgToolRoom.Columns[1];
            columnToolroom.Width = 40;
            dgToolRoom.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgToolRoom.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnCleaningID = dgCleaning.Columns[2];
            columnCleaningID.Visible = false;
            DataGridViewColumn columnCleaning = dgCleaning.Columns[1];
            columnCleaning.Width = 40;
            dgCleaning.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgCleaning.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewColumn columnManagementID = dgManagement.Columns[2];
            columnManagementID.Visible = false;
            DataGridViewColumn columnManagement = dgManagement.Columns[1];
            columnManagement.Width = 40;
            dgManagement.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgManagement.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            DataGridViewColumn columnHSID = dgHS.Columns[2];
            columnHSID.Visible = false;
            DataGridViewColumn columnHS = dgHS.Columns[1];
            columnHS.Width = 40;
            dgHS.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgHS.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


        }

        private void countMen()
        {

            double menCount = 0;
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("usp_power_planner_count_men", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dateSelected", SqlDbType.Date).Value = dteDateSelection.Text;

            menCount = Convert.ToDouble(cmd.ExecuteScalar());
            conn.Close();

            lblMenCount.Text = "Total amount of men selected: " + menCount.ToString();

        }

        private void fillSlimline()
        {

            if (dgSlimline.Columns.Contains("worked") == true)
            {
                dgSlimline.Columns.Remove("worked");
            }
            if (dgSlimline.Columns.Contains("set/worked") == true)
            {
                dgSlimline.Columns.Remove("set/worked");
            }
            if (dgSlimline.Columns.Contains("overtime") == true)
            {
                dgSlimline.Columns.Remove("overtime");
            }


            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Slimline");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgSlimline.DataSource = dt;



            SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
            cmdryucxd.CommandType = CommandType.StoredProcedure;
            cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Slimline";
            cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var dataReader = cmdryucxd.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable workedHours = new DataTable();
            workedHours.Load(dataReader);
            //da2.Fill(workedHours);

            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Slimline";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var OTreader = cmdOT.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            overtimeHours.Load(OTreader);

            dgSlimline.Columns.Add("worked", "worked");
            dgSlimline.Columns.Add("set/worked", "set/worked");
            dgSlimline.Columns.Add("overtime", "overtime");

            for (int i = 0; i < dgSlimline.Rows.Count; i++)
            {
                dgSlimline[5, i].Value = overtimeHours.Rows[0][i].ToString();
            }



            for (int i = 0; i < dgSlimline.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                //MessageBox.Show(workedHours.Rows[0][i].ToString());
                dgSlimline[3, i].Value = workedHours.Rows[0][i].ToString();
            }
            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgSlimline.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgSlimline.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgSlimline.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));       //dgSlimline.Rows[i].Cells[1].Value.ToString();
                worked = dgSlimline.Rows[i].Cells[3].Value.ToString();
                dgSlimline[4, i].Value = hours + " / " + worked;
            }

            dgSlimline.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgSlimline.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlimline.Columns["hours"].Visible = false;
            dgSlimline.Columns["worked"].Visible = false;
            dgSlimline.Columns["overtime"].Visible = false;


            conn.Close();
        }

        private void fillPunch()
        {
            if (dgPunch.Columns.Contains("overtime") == true)
            {
                dgPunch.Columns.Remove("overtime");
            }

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Punching");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgPunch.DataSource = dt;

            dgPunch.Columns.Add("overtime", "overtime");
            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Punching";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            SqlDataAdapter OTreader = new SqlDataAdapter(cmdOT);
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            OTreader.Fill(overtimeHours);


            for (int i = 0; i < dgPunch.Rows.Count; i++)
            {
                //MessageBox.Show(overtimeHours.Rows[0][i].ToString());
                dgPunch[3, i].Value = overtimeHours.Rows[0][i].ToString();
            }

            for (int i = 0; i < dgPunch.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgPunch.Rows[i].Cells[3].Value) * 0.8;
                overtimeTemp = overtimeTemp + Convert.ToDouble(dgPunch.Rows[i].Cells[1].Value.ToString());
                dgPunch[2, i].Value = overtimeTemp;
            }


            conn.Close();
            dgPunch.Columns[3].Visible = false;

        }

        private void fillLaser()
        {

            if (dgLaser.Columns.Contains("worked") == true)
            {
                dgLaser.Columns.Remove("worked");
            }
            if (dgLaser.Columns.Contains("set/worked") == true)
            {
                dgLaser.Columns.Remove("set/worked");
            }
            if (dgLaser.Columns.Contains("overtime") == true)
            {
                dgLaser.Columns.Remove("overtime");
            }


            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Laser");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgLaser.DataSource = dt;





            SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
            cmdryucxd.CommandType = CommandType.StoredProcedure;
            cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Laser";
            cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var dataReader = cmdryucxd.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable workedHours = new DataTable();
            workedHours.Load(dataReader);
            //da2.Fill(workedHours);


            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Laser";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var OTreader = cmdOT.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            overtimeHours.Load(OTreader);

            dgLaser.Columns.Add("worked", "worked");
            dgLaser.Columns.Add("set/worked", "set/worked");
            dgLaser.Columns.Add("overtime", "overtime");

            for (int i = 0; i < dgLaser.Rows.Count; i++)
            {
                dgLaser[5, i].Value = overtimeHours.Rows[0][i].ToString();
            }


            for (int i = 0; i < dgLaser.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                //MessageBox.Show(workedHours.Rows[0][i].ToString());
                dgLaser[3, i].Value = workedHours.Rows[0][i].ToString();
            }
            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgLaser.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgLaser.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgLaser.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));        //dgLaser.Rows[i].Cells[1].Value.ToString();
                worked = dgLaser.Rows[i].Cells[3].Value.ToString();
                dgLaser[4, i].Value = hours + " / " + worked;
            }

            dgLaser.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgLaser.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgLaser.Columns["hours"].Visible = false;
            dgLaser.Columns["worked"].Visible = false;
            dgLaser.Columns["overtime"].Visible = false;

            conn.Close();

        }

        private void fillBend()
        {

            if (dgBend.Columns.Contains("worked") == true)
            {
                dgBend.Columns.Remove("worked");
            }
            if (dgBend.Columns.Contains("set/worked") == true)
            {
                dgBend.Columns.Remove("set/worked");
            }
            if (dgBend.Columns.Contains("overtime") == true)
            {
                dgBend.Columns.Remove("overtime");
            }
            ////////////////////////////////////////////////////
            /////this is the original code that loads the section
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Bending");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgBend.DataSource = dt;
            ////////////////////////////////////////////////////


            SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
            cmdryucxd.CommandType = CommandType.StoredProcedure;
            cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Bending";
            cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var dataReader = cmdryucxd.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable workedHours = new DataTable();
            workedHours.Load(dataReader);
            //da2.Fill(workedHours);

            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Bending";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var OTreader = cmdOT.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            overtimeHours.Load(OTreader);

            dgBend.Columns.Add("worked", "worked");
            dgBend.Columns.Add("set/worked", "set/worked");
            dgBend.Columns.Add("overtime", "overtime");

            for (int i = 0; i < dgBend.Rows.Count; i++)
            {
                dgBend[5, i].Value = overtimeHours.Rows[0][i].ToString();
            }



            for (int i = 0; i < dgBend.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                //MessageBox.Show(workedHours.Rows[0][i].ToString());
                dgBend[3, i].Value = workedHours.Rows[0][i].ToString();
            }
            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgBend.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgBend.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgBend.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));       //dgBend.Rows[i].Cells[1].Value.ToString();
                worked = dgBend.Rows[i].Cells[3].Value.ToString();
                dgBend[4, i].Value = hours + " / " + worked;
            }

            dgBend.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgBend.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgBend.Columns["hours"].Visible = false;
            dgBend.Columns["worked"].Visible = false;
            dgBend.Columns["overtime"].Visible = false;

            // this seems to work fine i think afaik


            conn.Close();
        }

        private void fillWeld()
        {
            if (dgWeld.Columns.Contains("worked") == true)
            {
                dgWeld.Columns.Remove("worked");
            }
            if (dgWeld.Columns.Contains("set/worked") == true)
            {
                dgWeld.Columns.Remove("set/worked");
            }
            if (dgWeld.Columns.Contains("overtime") == true)
            {
                dgWeld.Columns.Remove("overtime");
            }
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();


            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Welding");

            //
            //string sql = "SELECT  b.forename + ' ' + b.surname + CHAR(13) + COALESCE(a.placement_type,'')  AS [Staff Placement], a.hours, a.id " +
            //                    "FROM dbo.power_plan_staff AS a " +
            //                    "INNER JOIN user_info.dbo.[user] AS b ON a.staff_id = b.id " +
            //                    "INNER JOIN dbo.power_plan_date as c ON a.date_id = c.id " +
            //                    "WHERE c.date_plan = '" + dteDateSelection.Text + "' and a.department = 'Welding'  order by b.forename + ' ' + b.surname";
            //ryucxd
            //SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //var dataReader = cmd.ExecuteReader();
            // conn.Close();
            DataTable dt = new DataTable();
            //dt.Load(dataReader);
            da.Fill(dt);

            dgWeld.DataSource = dt;
            //dgWeld.Columns["hours"].Visible = false;
            //dgWeld.Columns["Set hours / worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgWeld.Columns["workedHours"].Visible = false;


            //this procedure works everything out, so if we just staple on another column and insert whatever we grab from the procedure life should be good
            //usp_power_planner_worked_hours
            SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
            cmdryucxd.CommandType = CommandType.StoredProcedure;
            cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Welding";
            cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var dataReader = cmdryucxd.ExecuteReader();
            //SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable workedHours = new DataTable();
            workedHours.Load(dataReader);
            //da2.Fill(workedHours);

            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Welding";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var OTreader = cmdOT.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            overtimeHours.Load(OTreader);


            dgWeld.Columns.Add("worked", "worked");
            dgWeld.Columns.Add("set/worked", "set/worked");
            dgWeld.Columns.Add("overtime", "overtime");

            for (int i = 0; i < dgWeld.Rows.Count; i++)
            {
                dgWeld[5, i].Value = overtimeHours.Rows[0][i].ToString();
            }
            //need to check the datatable for content




            for (int i = 0; i < dgWeld.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                // MessageBox.Show(workedHours.Rows[0][i].ToString());
                dgWeld[3, i].Value = workedHours.Rows[0][i].ToString();
            }
            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgWeld.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgWeld.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgWeld.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));   //dgWeld.Rows[i].Cells[1].Value.ToString();
                worked = dgWeld.Rows[i].Cells[3].Value.ToString();
                dgWeld[4, i].Value = hours + " / " + worked;
            }


            //dgWeld.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgWeld.Columns["hours"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgWeld.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgWeld.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgWeld.Columns["hours"].Visible = false;
            dgWeld.Columns["worked"].Visible = false;
            dgWeld.Columns["overtime"].Visible = false;

            // conn.Close();

        }

        private void fillBuff()
        {
            if (dgBuff.Columns.Contains("worked") == true)
            {
                dgBuff.Columns.Remove("worked");
            }
            if (dgBuff.Columns.Contains("set/worked") == true)
            {
                dgBuff.Columns.Remove("set/worked");
            }
            if (dgBuff.Columns.Contains("overtime") == true)
            {
                dgBuff.Columns.Remove("overtime");
            }

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Dressing");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgBuff.DataSource = dt;



            SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
            cmdryucxd.CommandType = CommandType.StoredProcedure;
            cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Dressing";
            cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var dataReader = cmdryucxd.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable workedHours = new DataTable();
            workedHours.Load(dataReader);
            //da2.Fill(workedHours);



            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Dressing";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;
            var OTreader = cmdOT.ExecuteReader();


            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            overtimeHours.Load(OTreader);

            dgBuff.Columns.Add("worked", "worked");
            dgBuff.Columns.Add("set/worked", "set/worked");
            dgBuff.Columns.Add("overtime", "overtime");


            for (int i = 0; i < dgBuff.Rows.Count; i++)
            {
                dgBuff[5, i].Value = overtimeHours.Rows[0][i].ToString();
            }





            for (int i = 0; i < dgBuff.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                //MessageBox.Show(workedHours.Rows[0][i].ToString());
                dgBuff[3, i].Value = workedHours.Rows[0][i].ToString();
            }
            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgBuff.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgBuff.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgBuff.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));    //dgBuff.Rows[i].Cells[1].Value.ToString();
                worked = dgBuff.Rows[i].Cells[3].Value.ToString();
                dgBuff[4, i].Value = hours + " / " + worked;
            }


            //dgBuff.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgBuff.Columns["hours"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgBuff.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgBuff.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgBuff.Columns["hours"].Visible = false;
            dgBuff.Columns["worked"].Visible = false;
            dgBuff.Columns["overtime"].Visible = false;



            conn.Close();

        }

        private void fillPaint()
        {
            //adjust dgv here cause why not? 

            if (dgPaint.Columns.Contains("overtime") == true)
            {
                dgPaint.Columns.Remove("overtime");
            }

            //ryucxd paint
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            string sql = "SELECT  b.forename + ' ' + b.surname + CHAR(13) + COALESCE(d.sub_department,'') + CHAR(13) + a.placement_type  AS [Staff Placement], a.hours, a.id " +
                                "FROM dbo.power_plan_staff AS a " +
                                "INNER JOIN user_info.dbo.[user] AS b ON a.staff_id = b.id " +
                                "INNER JOIN dbo.power_plan_date as c ON a.date_id = c.id " +
                                "LEFT JOIN dbo.power_plan_paint_sub_dept_test_temp_2 as d ON a.id = d.placement_id " +
                                "WHERE c.date_plan = '" + dteDateSelection.Text + "' and a.department = 'Painting'  order by b.forename + ' ' + b.surname";  //ORDER BY [Staff Name]

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgPaint.DataSource = dt;



            dgPaint.Columns.Add("overtime", "overtime");
            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Painting";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            SqlDataAdapter OTreader = new SqlDataAdapter(cmdOT);
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            OTreader.Fill(overtimeHours);


            for (int i = 0; i < dgPaint.Rows.Count; i++)
            {
                //MessageBox.Show(overtimeHours.Rows[0][i].ToString());
                dgPaint[3, i].Value = overtimeHours.Rows[0][i].ToString();
            }

            for (int i = 0; i < dgPaint.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgPaint.Rows[i].Cells[3].Value) * 0.8;
                overtimeTemp = overtimeTemp + Convert.ToDouble(dgPaint.Rows[i].Cells[1].Value.ToString());
                dgPaint[3, i].Value = overtimeTemp;
            }



            conn.Close();
            dgPaint.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPaint.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPaint.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgPaint.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgPaint.Columns[3].Visible = false;

        }

        private void fillPack()
        {
            if (dgPack.Columns.Contains("worked") == true)
            {
                dgPack.Columns.Remove("worked");
            }
            if (dgPack.Columns.Contains("set/worked") == true)
            {
                dgPack.Columns.Remove("set/worked");
            }
            if (dgPack.Columns.Contains("packValue"))
            {
                dgPack.Columns.Remove("packValue");
            }
            if (dgPack.Columns.Contains("overtime"))
            {
                dgPack.Columns.Remove("overtime");
            }
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Packing");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgPack.DataSource = dt;


            SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
            cmdryucxd.CommandType = CommandType.StoredProcedure;
            cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Packing";
            cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

            var dataReader = cmdryucxd.ExecuteReader();
            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable workedHours = new DataTable();
            workedHours.Load(dataReader);
            //da2.Fill(workedHours);



            //overtime -- usp_power_planner_overtime_hours
            SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
            cmdOT.CommandType = CommandType.StoredProcedure;
            cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Packing";
            cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;
            var OTreader = cmdOT.ExecuteReader();


            // SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
            DataTable overtimeHours = new DataTable();
            overtimeHours.Load(OTreader);

            dgPack.Columns.Add("worked", "worked");
            dgPack.Columns.Add("set/worked", "set/worked");
            dgPack.Columns.Add("packValue", "packValue");
            dgPack.Columns.Add("overtime", "overtime");



            for (int i = 0; i < dgPack.Rows.Count; i++)
            {
                dgPack[6, i].Value = overtimeHours.Rows[0][i].ToString();
            }





            for (int i = 0; i < dgPack.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                //MessageBox.Show(workedHours.Rows[0][i].ToString());
                dgPack[3, i].Value = workedHours.Rows[0][i].ToString();
            }

            //get pack value
            SqlCommand cmdryucxd2 = new SqlCommand("usp_power_planner_packed_value", conn);
            cmdryucxd2.CommandType = CommandType.StoredProcedure;
            cmdryucxd2.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;
            var dataReader2 = cmdryucxd2.ExecuteReader();
            DataTable packValue = new DataTable();
            packValue.Load(dataReader2);
            for (int i = 0; i < dgPack.Rows.Count; i++) //because this is ordered by staff i can use the max rows to get the number for columns needed :)
            {
                dgPack[5, i].Value = packValue.Rows[0][i].ToString();
            }


            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgPack.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgPack.Rows[i].Cells[6].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgPack.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));   // dgPack.Rows[i].Cells[1].Value.ToString();
                worked = dgPack.Rows[i].Cells[3].Value.ToString();
                dgPack[4, i].Value = hours + " / " + worked + Environment.NewLine + "£" + packValue.Rows[0][i].ToString();
            }


            //dgPack.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgPack.Columns["hours"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPack.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPack.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPack.Columns["set/worked"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPack.Columns["hours"].Visible = false;
            dgPack.Columns["worked"].Visible = false;
            dgPack.Columns["overtime"].Visible = false;
            dgPack.Columns["packValue"].Visible = false;

            //also add the label that shows how much has been packed

            string sql = "select round(convert(float,sum(line_total)),2)  from dbo.door as a  inner join dbo.view_door_value as b on a.id = b.id    inner join dbo.door_type as c on a.door_type_id = c.id " +
                                "where(date_pack_complete >= '" + Convert.ToDateTime(dteDateSelection.Text).ToString("yyyy-MM-dd") + "' and date_pack_complete <= dateadd(D, 1, '" + Convert.ToDateTime(dteDateSelection.Text).ToString("yyyy-MM-dd") + "')) and(c.slimline_y_n = 0 or c.slimline_y_n is null)";
            using (SqlCommand cmd2 = new SqlCommand(sql, conn))
            {
                string temp = "";

                temp = Convert.ToString(cmd2.ExecuteScalar());
                if (temp == "")
                    temp = "0";

                    lblTotalPacked.Text = "Total Packed: £" + temp;

            }

            conn.Close();
        }

        private void fillStores()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Stores");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgStores.DataSource = dt;

            conn.Close();
        }

        private void fillDispatch()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Dispatch");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgDispatch.DataSource = dt;

            conn.Close();
        }

        private void fillToolroom()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Toolroom");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgToolRoom.DataSource = dt;

            conn.Close();
        }

        private void fillCleaning()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Cleaning");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgCleaning.DataSource = dt;

            conn.Close();
        }

        private void fillManagement()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "Management");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgManagement.DataSource = dt;

            conn.Close();
        }

        private void fillHS()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "HS");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgHS.DataSource = dt;

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
            //taking this out because its annoying when looking forward months.
            // fillgrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Laser", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }

        private void copyPlacementsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmCopyPlacements cp = new frmCopyPlacements(Convert.ToDateTime(dteDateSelection.Text));
            cp.ShowDialog();
            fillgrid();
        }

        private void btnAddSlimline_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;

            department_changed department_Changed = new department_changed();

            frmSelectStaff frmSS = new frmSelectStaff("Slimline", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }

        private void sendToDailyGoalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateDailyGoals();

        }

        private void updateDailyGoals()
        {
            double goalHoursSlimline;
            double goalHoursLaser;
            double goalHoursPunch;
            double goalHoursBend;
            double goalHoursWeld;
            double goalHoursBuff;
            double goalHoursPaint;
            double goalHoursPack;
            double goalBoxes = 0;


            double manPowerSlimline;
            double manPowerLaser;
            double manPowerPunch;
            double manPowerBend;
            double manPowerWeld;
            double manPowerBuff;
            double manPowerPaint;
            double manPowerPack;
            double manPowerStores;

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
            manPowerStores = Convert.ToDouble(txtStoresMen.Text);

            if (manPowerStores == 1)
            {
                goalBoxes = 25;
            }
            if (manPowerStores == 2)
            {
                goalBoxes = 70;
            }
            if (manPowerStores == 3)
            {
                goalBoxes = 120;
            }

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
                "goal_boxes = @goalBoxes, " +
                "man_power_slimline = @manPowerSlimline, " +
                "man_power_laser = @manPowerLaser, " +
                "man_power_punch = @manPowerPunch, " +
                "man_power_bend = @manPowerBend, " +
                "man_power_weld = @manPowerWeld, " +
                "man_power_buff = @manPowerBuff, " +
                "man_power_paint = @manPowerPaint, " +
                "man_power_pack = @manPowerPack, " +
                "man_power_stores = @manPowerStores " +
                " WHERE date_goal = @dateGoal", conn))
            {

                cmd.Parameters.AddWithValue("@goalHoursSlimline", goalHoursSlimline);
                cmd.Parameters.AddWithValue("@goalHoursLaser", goalHoursLaser);
                cmd.Parameters.AddWithValue("@goalHoursPunch", goalHoursPunch);
                cmd.Parameters.AddWithValue("@goalHoursBend", goalHoursBend);
                cmd.Parameters.AddWithValue("@goalHoursWeld", goalHoursWeld);
                cmd.Parameters.AddWithValue("@goalHoursBuff", goalHoursBuff);
                cmd.Parameters.AddWithValue("@goalHoursPaint", goalHoursPaint);
                cmd.Parameters.AddWithValue("@goalHoursPack", goalHoursPack);
                cmd.Parameters.AddWithValue("@goalBoxes", goalBoxes);

                cmd.Parameters.AddWithValue("@manPowerSlimline", manPowerSlimline);
                cmd.Parameters.AddWithValue("@manPowerLaser", manPowerLaser);
                cmd.Parameters.AddWithValue("@manPowerPunch", manPowerPunch);
                cmd.Parameters.AddWithValue("@manPowerBend", manPowerBend);
                cmd.Parameters.AddWithValue("@manPowerWeld", manPowerWeld);
                cmd.Parameters.AddWithValue("@manPowerBuff", manPowerBuff);
                cmd.Parameters.AddWithValue("@manPowerPaint", manPowerPaint);
                cmd.Parameters.AddWithValue("@manPowerPack", manPowerPack);
                cmd.Parameters.AddWithValue("@manPowerStores", manPowerStores);
                cmd.Parameters.AddWithValue("@dateGoal", dteDateSelection.Text);
                try
                {
                    cmd.ExecuteNonQuery();
                    if (skipMessageBox == 0)
                        MessageBox.Show("Hours successfully sent to daily goals!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("An error has occured, if this error persists please contact IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    //UPDATES AUTOPLACEMENTS IN AUTOMATIC ALLOCATION
                    updateAutomaticAllocation();

                }
                catch
                {
                    MessageBox.Show("An error has occured with automatic allocation script, if this error persists please contact IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            lbl_time.Text = DateTime.Now.ToString();
            lbl_time.Refresh();
            //  lbl_time.Visible = true;
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
            lbl_time.Text = "";

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
            updateAutomaticAllocation();

        }

        private void updateAutomaticAllocation()
        {
            //CLEARS ALL EXISTING SELECTION FROM THE TABLE FOR THIS DEPARTMENT
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("usp_power_planner_auto_placement", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            if (skipMessageBox > 0)
                skipMessageBox = 0;
            else
                MessageBox.Show("Automatic allocation updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAddStores_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Stores", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();

        }

        private void btnAddDispatch_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Dispatch", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }


        private void btnAddToolRoom_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("toolroom", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog(); //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
                                // fillgrid();
            refreshSelectedDepartments();
        }


        private void btnAddCleaning_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Cleaning", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
        }



        private void BtnAddManagement_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Management", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog(); //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
                                // fillgrid();
            refreshSelectedDepartments();
        }

        private void BtnAddHS_Click(object sender, EventArgs e)
        {
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("HS", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
        }

        private void BatchOperationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBatchPlacement bp = new frmBatchPlacement();
            bp.ShowDialog();
            updateDailyGoals();
            fillgrid();
        }

        private void ClearPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CLEARS ALL EXISTING SELECTION
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("usp_power_planner_clear_day", conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@placementDate", SqlDbType.Date).Value = dteDateSelection.Text;

                cmd.ExecuteNonQuery();
                conn.Close();
            }

            fillgrid();



        }

        private void ryucxdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelClass excel = new ExcelClass();
            string sql = "";
            //from the current day, get all the days in that week (to iterate through
            DateTime date = Convert.ToDateTime(dteDateSelection.Value.ToShortDateString());
            DateTime Monday = new DateTime();
            DateTime Friday = new DateTime();
            while (date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);
            Monday = date;
            //get end of week
            Friday = date.AddDays(4);

            //now we just need the finishing touches ¬   finishing touches even tho we are at the start of the button press :p
            //the name of the day and date for each one
            string mondaySTR = "", tuesdaySTR = "", wednesdaySTR = "", thursdaySTR = "", fridaySTR = "", fileName = "";
            fileName = date.ToShortDateString();
            fileName = fileName.Replace("/", "-");
            mondaySTR = "Monday - " + Monday.ToShortDateString();
            DateTime stringDate = Monday.AddDays(0);
            tuesdaySTR = "Tuesday - " + stringDate.ToShortDateString();
            stringDate = Monday.AddDays(1);
            tuesdaySTR = "Tuesday - " + stringDate.ToShortDateString();
            stringDate = Monday.AddDays(2);
            wednesdaySTR = "Wednesday - " + stringDate.ToShortDateString();
            stringDate = Monday.AddDays(3);
            thursdaySTR = "Thursday - " + stringDate.ToShortDateString();
            stringDate = Monday.AddDays(4);
            fridaySTR = "Friday - " + stringDate.ToShortDateString();

            //check if the file already exsits and if it does then skip everything below maybe? or delete whats there


            //now we have monday - friday, get the DATEID of each of these
            //store these in a DT
            DataTable dt = new DataTable();// declare this outside so it can be called outside
            sql = "select id from dbo.power_plan_date WHERE date_plan >= '" + Monday.ToString("yyyy-MM-dd") + "' AND date_plan <= '" + Friday.ToString("yyyy-MM-dd") + "' ORDER BY date_plan ASC";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    conn.Close();
                }
            }
            //call the DataTable using this /dt.Rows[0][0]/
            //now we can zip through everyday of that week
            //all the variables we'll ever need for overtime
            double punching_OT = 0, punching_AD = 0, laser_OT = 0, laser_AD = 0, bending_OT = 0, bending_AD = 0, welding_OT = 0, welding_AD = 0, buffing_OT = 0, buffing_AD = 0, painting_OT = 0, painting_AD = 0, packing_AD = 0, packing_OT = 0;
            //variable for flicking through datatable
            int row = 0;
            for (int i = 4; i < 21; i = i + 4)
            {//one connectiong string to rule them all
                //MessageBox.Show(i.ToString());
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {

                    //get the figures for OT + AD
                    sql = "select date_id,punching_OT,punching_AD,laser_OT,laser_AD,bending_OT,bending_AD,welding_OT,welding_AD,buffing_OT,buffing_AD,painting_OT,painting_AD,packing_AD,packing_OT from dbo.power_plan_overtime WHERE date_id = " + dt.Rows[row][0];
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            punching_OT = Convert.ToDouble(reader["punching_OT"]) * 0.8; //this is broke for buffing i think
                            punching_AD = Convert.ToDouble(reader["punching_AD"]);
                            laser_OT = Convert.ToDouble(reader["laser_OT"]) * 0.8;
                            laser_AD = Convert.ToDouble(reader["laser_AD"]);
                            bending_OT = Convert.ToDouble(reader["bending_OT"]) * 0.8;
                            bending_AD = Convert.ToDouble(reader["bending_AD"]);
                            welding_OT = Convert.ToDouble(reader["welding_OT"]) * 0.8;
                            welding_AD = Convert.ToDouble(reader["welding_AD"]);
                            buffing_OT = Convert.ToDouble(reader["buffing_OT"]) * 0.8;
                            buffing_AD = Convert.ToDouble(reader["buffing_AD"]);
                            painting_OT = Convert.ToDouble(reader["painting_OT"]) * 0.8;
                            painting_AD = Convert.ToDouble(reader["painting_AD"]);
                            packing_OT = Convert.ToDouble(reader["packing_OT"]) * 0.8;
                            packing_AD = Convert.ToDouble(reader["packing_AD"]);
                        }
                        conn.Close();
                    }
                    //get normal hours here i think?
                    //variables for houys
                    double punching_hours = 0, laser_hours = 0, bending_hours = 0, welding_hours = 0, buffing_hours = 0, painting_hours = 0, packing_hours = 0;
                    //loop through each section and paste them into a variable thats declared above
                    string dept = "";
                    for (int z = 0; z <= 7; z++)
                    { //first get the department
                        if (z == 0)
                            dept = "Punching";
                        if (z == 1)
                            dept = "Laser";
                        if (z == 2)
                            dept = "Bending";
                        if (z == 3)
                            dept = "Welding";
                        if (z == 4)
                            dept = "Dressing";
                        if (z == 5)
                            dept = "Painting";
                        if (z == 6)
                            dept = "Packing";

                        sql = "select distinct sum([hours]) as [hours] from  dbo.power_plan_staff where department = '" + dept + "'  AND date_id = " + dt.Rows[row][0] + " group by department";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            if (z == 0)
                                punching_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            if (z == 1)
                                laser_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            if (z == 2)
                                bending_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            if (z == 3)
                                welding_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            if (z == 4)
                                buffing_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            if (z == 5)
                                painting_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            if (z == 6)
                                packing_hours = Convert.ToDouble(cmd.ExecuteScalar());
                            conn.Close();
                        }
                    }
                    int print = 0;
                    if (i == 20)
                        print = 1;
                    excel.openExcel(print, i, fileName, mondaySTR, tuesdaySTR, wednesdaySTR, thursdaySTR, fridaySTR, Convert.ToDouble(punching_hours), Convert.ToDouble(punching_OT), Convert.ToDouble(punching_AD),
                                               Convert.ToDouble(laser_hours), Convert.ToDouble(laser_OT), Convert.ToDouble(laser_AD),
                                               Convert.ToDouble(bending_hours), Convert.ToDouble(bending_OT), Convert.ToDouble(bending_AD),
                                               Convert.ToDouble(welding_hours), Convert.ToDouble(welding_OT), Convert.ToDouble(welding_AD),
                                               Convert.ToDouble(buffing_hours), Convert.ToDouble(buffing_OT), Convert.ToDouble(buffing_AD),
                                               Convert.ToDouble(painting_hours), Convert.ToDouble(painting_OT), Convert.ToDouble(painting_AD),
                                               Convert.ToDouble(packing_hours), Convert.ToDouble(packing_OT), Convert.ToDouble(packing_AD));


                    //excel.addData(Convert.ToDouble(punching_hours), Convert.ToDouble(punching_OT), Convert.ToDouble(punching_AD),
                    //                           Convert.ToDouble(laser_hours), Convert.ToDouble(laser_OT), Convert.ToDouble(laser_AD),
                    //                           Convert.ToDouble(bending_hours), Convert.ToDouble(bending_OT), Convert.ToDouble(bending_AD),
                    //                           Convert.ToDouble(welding_hours), Convert.ToDouble(welding_OT), Convert.ToDouble(welding_AD),
                    //                           Convert.ToDouble(buffing_hours), Convert.ToDouble(buffing_OT), Convert.ToDouble(buffing_AD),
                    //                           Convert.ToDouble(painting_hours), Convert.ToDouble(painting_OT), Convert.ToDouble(painting_AD),
                    //                           Convert.ToDouble(packing_hours), Convert.ToDouble(packing_OT), Convert.ToDouble(packing_AD)
                    //);



                    //excel.addDAtes(mondaySTR, tuesdaySTR, wednesdaySTR, thursdaySTR, fridaySTR);
                    // excel.closeExcel();


                    row++;
                }
            }
            //get the right fields into this and 
            //excel.print();
            //excel.closeExcel();
            MessageBox.Show("Printout has been sent to your default printer!");
        }

        private void dteDateSelection_CloseUp(object sender, EventArgs e)
        {
            //this event on fires when a date is selected  rather than everytime a month is switched about solving the slow swapping on months without selecting a date
            fillgrid();
        }


        private void refreshSelectedDepartments()
        {
            //this needs to go through each of the departments that are selected from the prior screen :)
            if (department_changed.slimlineSelected == -1)
                fillSlimline();
            if (department_changed.punchSelected == -1)
                fillPunch();
            if (department_changed.laserSelected == -1)
                fillLaser();
            if (department_changed.bendSelected == -1)
                fillBend();
            if (department_changed.weldSelected == -1)
                fillWeld();
            if (department_changed.buffSelected == -1)
                fillBuff();
            if (department_changed.paintSelected == -1)
                fillPaint();
            if (department_changed.packSelected == -1)
                fillPack();
            if (department_changed.storesSelected == -1)
                fillStores();
            if (department_changed.dispatchSelected == -1)
                fillDispatch();
            if (department_changed.toolSelected == -1)
                fillToolroom();
            if (department_changed.cleaningSelected == -1)
                fillCleaning();
            if (department_changed.managementSelected == -1)
                fillManagement();
            fillNotPlaced();
            paintGrid();
            countGrid();
            updateDailyGoals();

            department_changed dc = new department_changed();
            dc.resetData();
        }

        private void dgPunch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.ColumnIndex.ToString());
        }



        private void dgWeld_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgWeld.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgWeld.Rows[e.RowIndex].Cells[0].Value), "Welding", dteDateSelection.Value);
            frm.ShowDialog();
        }


        private void dgPack_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgPack.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgPack.Rows[e.RowIndex].Cells[0].Value), "Packing", dteDateSelection.Value);
            frm.ShowDialog();
        }

        private void dgBuff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgBuff.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgBuff.Rows[e.RowIndex].Cells[0].Value), "Buffing", dteDateSelection.Value);
            frm.ShowDialog();
        }

        private void dgNotPlaced_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgNotPlaced.Rows[e.RowIndex].Cells[1].Value.ToString().Contains("HOLIDAY"))
            {

                //if its a holiday show the form that displays when it was created 
                //grab user ud
                int id = 0;
                string sql = "SELECT id FROM dbo.[user] WHERE forename + ' ' + surname = '" + dgNotPlaced.Rows[e.RowIndex].Cells[0].Value + "'";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }


                frmHolidayCreated frm = new frmHolidayCreated(id, Convert.ToDateTime(dteDateSelection.Text), dgNotPlaced.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.ShowDialog();
            }

        }

        private void shopFloorInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opens a form to allow the end user to add temps
            frmCovidInput frm = new frmCovidInput(Convert.ToDateTime(dteDateSelection.Value.ToString()), -1);
            frm.ShowDialog();
        }

        private void officeInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opens a form to allow the end user to add temps
            frmCovidInput frm = new frmCovidInput(Convert.ToDateTime(dteDateSelection.Value.ToString()), 0);
            frm.ShowDialog();
        }

        private void cOPYWEEKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCopyWeek frm = new frmCopyWeek(dteDateSelection.Value);
            frm.ShowDialog();
            //maybe if copy is successful we should jump to that date ---
        } //

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            department_changed.slimlineSelected = -1;
            department_changed.punchSelected = -1;
            department_changed.laserSelected = -1;
            department_changed.bendSelected = -1;
            department_changed.weldSelected = -1;
            department_changed.buffSelected = -1;
            department_changed.paintSelected = -1;
            department_changed.packSelected = -1;
            department_changed.storesSelected = -1;
            department_changed.dispatchSelected = -1;
            department_changed.toolSelected = -1;
            department_changed.cleaningSelected = -1;
            department_changed.managementSelected = -1;
            skipMessageBox = 2;
            refreshSelectedDepartments();
        }

        private void productivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //mimic the productivity reports from access
            frmProductivity frm = new frmProductivity();
            frm.ShowDialog();

        }

        private void cOVIDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}



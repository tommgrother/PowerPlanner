using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class MenuMain : Form
    {
        public int skipMessageBox { get; set; }
        public int queueName { get; set; }
        public string printout_file_name { get; set; }

        public MenuMain()
        {
            InitializeComponent();
            kevinMessage();
            login.formIsOpen = 0;
        }



        private void btnAddPunch_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Punching", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddBend_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Bending", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddWeld_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Welding", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddBuff_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Dressing", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddPaint_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Painting", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddPack_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Packing", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void MenuMain_Load(object sender, EventArgs e)
        {
            fillgrid();
            currentAvailable();
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
                    if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    { }
                    else
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
                    if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    { }
                    else
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
                    if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    { }
                    else
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

            using (SqlCommand cmd = new SqlCommand("SELECT TOP (1000) [id] , COALESCE([date_id], 0) as [date_id], COALESCE([slimline_OT],0) as [slimline_OT],COALESCE([laser_OT],0) as [laser_OT],COALESCE([punching_OT],0) as [punching_OT]" +
                ",COALESCE([bending_OT],0) as [bending_OT],COALESCE([welding_OT],0) as [welding_OT],COALESCE([buffing_OT],0) as [buffing_OT],COALESCE([painting_OT],0) as [painting_OT],COALESCE([packing_OT],0) as [packing_OT]" +
                ",COALESCE([slimline_AD],0) as [slimline_AD],COALESCE([laser_AD],0) as [laser_AD],COALESCE([punching_AD],0) as [punching_AD],COALESCE([bending_AD],0) as [bending_AD],COALESCE([welding_AD],0) as [welding_AD]" +
                ",COALESCE([buffing_AD],0) as [buffing_AD],COALESCE([painting_AD],0) as [painting_AD],COALESCE([packing_AD],0) as [packing_AD],COALESCE([stores_OT],0) as [stores_OT],COALESCE([dispatch_OT],0) as [dispatch_OT]" +
                ",COALESCE([toolroom_OT],0) as [toolroom_OT],COALESCE([cleaning_OT],0) as [cleaning_OT],COALESCE([stores_AD],0) as [stores_AD],COALESCE([dispatch_AD],0) as [dispatch_AD],COALESCE([toolroom_AD],0) as [toolroom_AD]" +
                ",COALESCE([cleaning_AD],0) as [cleaning_AD],COALESCE([management_OT],0) as [management_OT],COALESCE([management_AD],0) as [management_AD],COALESCE([hs_OT],0) as [hs_OT],COALESCE([hs_AD],0) as [hs_AD]" +
                "FROM[order_database].[dbo].[power_plan_overtime] where date_id = @dateID", conn))
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

            txtSLActualHours.Text = "0";
            txtPunchActualHours.Text = "0";
            txtlaserActualHours.Text = "0";
            txtBendActualHours.Text = "0";
            txtWeldActualHours.Text = "0";
            txtBuffActualHours.Text = "0";
            txtPaintActualHours.Text = "0";
            txtPackActualHours.Text = "0";
            txtStockHours.Text = "0";


            string sql = "SELECT ROUND(COALESCE(actual_hours_slimline,0),2) as slimline, ROUND(COALESCE(actual_hours_punch,0),2) as punch, ROUND(COALESCE(actual_hours_laser,0),2) as laser, ROUND(COALESCE(actual_hours_bend,0),2) as bend, ROUND(COALESCE(actual_hours_weld,0),2) as weld, ROUND(COALESCE(actual_hours_buff,0),2) as buff,ROUND(COALESCE(actual_hours,0),2) as paint , ROUND(COALESCE(actual_hours_pack,0),2) as boxes,ROUND(COALESCE(actual_hours_bend_stock,0),2) as stock_parts  FROM dbo.daily_department_goal WHERE date_goal ='" + dteDateSelection.Text + "'";
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
                        txtStockHours.Text = row[8].ToString();

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

                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }

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
            foreach (DataGridViewRow row in dgSlDispatch.Rows)
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
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            }
            foreach (DataGridViewRow row in dgSlStores.Rows)
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
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                if (row.Cells[0].Value.ToString().Contains("Half"))
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
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

            //Paint
            foreach (DataGridViewRow row in dgPaint.Rows)
                if (row.Cells[0].Value.ToString().Contains("Shift"))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgPaint.Rows)
                if (row.Cells[0].Value.ToString().Contains("Half"))   //12321
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
                        row.DefaultCellStyle.BackColor = Color.MediumPurple;

                    if (row.Cells[1].Value.ToString().Contains("ABSENT"))
                        row.DefaultCellStyle.BackColor = Color.Salmon;

                    if (row.Cells[1].Value.ToString().Contains("ABSENT TAKEN HOLIDAY"))
                        row.DefaultCellStyle.BackColor = Color.LightSteelBlue;

                    if (row.Cells[1].Value.ToString().Contains("UNPAID"))
                        row.DefaultCellStyle.BackColor = Color.DeepPink;

                    if (row.Cells[1].Value.ToString().Contains("LATE"))
                        row.DefaultCellStyle.BackColor = Color.Yellow;

                    if (row.Cells[1].Value.ToString().Contains("MATERNITY/PATERNITY"))
                        row.DefaultCellStyle.BackColor = Color.Plum;

                    if (row.Cells[1].Value.ToString().Contains("BEREAVEMENT"))
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;

                    if (row.Cells[1].Value.ToString().Contains("AWOL"))
                        row.DefaultCellStyle.BackColor = Color.LightSkyBlue;

                    if (row.Cells[1].Value.ToString().Contains("TRAVEL/WEATHER"))
                        row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;

                    if (row.Cells[1].Value.ToString().Contains("DEPENDANT"))
                        row.DefaultCellStyle.BackColor = Color.Peru;


                }
                catch
                {
                    continue;
                }

            foreach (DataGridViewRow row in dgNotPlacementSL.Rows)
                try
                {
                    if (row.Cells[1].Value.ToString().Contains("HOLIDAY"))
                        row.DefaultCellStyle.BackColor = Color.MediumPurple;

                    if (row.Cells[1].Value.ToString().Contains("ABSENT"))
                        row.DefaultCellStyle.BackColor = Color.Salmon;

                    if (row.Cells[1].Value.ToString().Contains("ABSENT TAKEN HOLIDAY"))
                        row.DefaultCellStyle.BackColor = Color.LightSteelBlue;

                    if (row.Cells[1].Value.ToString().Contains("UNPAID"))
                        row.DefaultCellStyle.BackColor = Color.DeepPink;

                    if (row.Cells[1].Value.ToString().Contains("LATE"))
                        row.DefaultCellStyle.BackColor = Color.Yellow;

                    if (row.Cells[1].Value.ToString().Contains("MATERNITY/PATERNITY"))
                        row.DefaultCellStyle.BackColor = Color.Plum;

                    if (row.Cells[1].Value.ToString().Contains("BEREAVEMENT"))
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;

                    if (row.Cells[1].Value.ToString().Contains("AWOL"))
                        row.DefaultCellStyle.BackColor = Color.LightSkyBlue;

                    if (row.Cells[1].Value.ToString().Contains("TRAVEL/WEATHER"))
                        row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;

                    if (row.Cells[1].Value.ToString().Contains("DEPENDANT"))
                        row.DefaultCellStyle.BackColor = Color.Peru;


                }
                catch
                {
                    continue;
                }
            //foreach (DataGridViewRow row in dgNotPlaced.Rows)
            //    try
            //    {
            //        if (row.Cells[1].Value.ToString().Contains("ABSENT"))
            //        {
            //            row.DefaultCellStyle.BackColor = Color.Salmon;
            //        }
            //    }
            //    catch
            //    {
            //        continue;
            //    }

            //foreach (DataGridViewRow row in dgNotPlaced.Rows)
            //    try
            //    {
            //        if (row.Cells[1].Value.ToString().Contains("UNPAID"))
            //        {
            //            row.DefaultCellStyle.BackColor = Color.DeepPink;
            //        }
            //    }
            //    catch 
            //    {
            //        continue;
            //    }

            ///// same as above but for slimline version
            foreach (DataGridViewRow row in dgNotPlacementSL.Rows)
                try
                {
                    if (row.Cells[1].Value.ToString().Contains("HOLIDAY"))
                    {
                        row.DefaultCellStyle.BackColor = Color.MediumPurple;
                    }

                    if (row.Cells[1].Value.ToString().Contains("LATE"))
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                catch
                {
                    continue;
                }
            foreach (DataGridViewRow row in dgNotPlacementSL.Rows)
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

            foreach (DataGridViewRow row in dgNotPlacementSL.Rows)
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
            ////

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
            dgNotPlacementSL.ClearSelection();
            dgvHSManagement.ClearSelection();
            dgSlDispatch.ClearSelection();
            dgSlStores.ClearSelection();
        }

        private void fillgrid()
        {
            fillSlimline();
            fillSlimlineStores();
            fillSlimlineDispatch();
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
            fillHSManagement();
            countMen();
            currentAvailable();

            DataGridViewColumn columnSlimlineID = dgSlimline.Columns[2];
            columnSlimlineID.Visible = false;
            DataGridViewColumn columnSlimline = dgSlimline.Columns[1];
            columnSlimline.Width = 40;
            dgSlimline.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlimline.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewColumn columnSlimlineDispatchID = dgSlDispatch.Columns[2];
            columnSlimlineDispatchID.Visible = false;
            DataGridViewColumn columnSlimlineDispatch = dgSlDispatch.Columns[1];
            columnSlimlineDispatch.Width = 40;
            dgSlDispatch.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlDispatch.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewColumn columnSlimlineStoresID = dgSlStores.Columns[2];
            columnSlimlineStoresID.Visible = false;
            DataGridViewColumn columnSlimlineStores = dgSlStores.Columns[1];
            columnSlimlineStores.Width = 40;
            dgSlStores.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlStores.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

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

            DataGridViewColumn columnHSManagementID = dgvHSManagement.Columns[2];
            columnHSManagementID.Visible = false;
            DataGridViewColumn columnHSManagement = dgvHSManagement.Columns[1];
            columnHSManagement.Width = 40;
            dgvHSManagement.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvHSManagement.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            fillShopGoals();
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
                //MessageBox.Show(workedHours.Rows[0][i].ToString()); //
                dgSlimline[3, i].Value = workedHours.Rows[0][i].ToString();
            }


            string sql = "select [Staff Name] FROM view_planner_punch_staff WHERE department = 'Slimline' AND date_plan = cast('" + dteDateSelection.Value.ToString("yyyyMMdd") + "' as date) ORDER BY [Staff Name]"; //12324
            DataTable dtStaffID = new DataTable();
            using (SqlCommand cmdStaffID = new SqlCommand(sql, conn))
            {
                SqlDataAdapter daStaffID = new SqlDataAdapter(cmdStaffID);
                daStaffID.Fill(dtStaffID);
            }


            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgSlimline.Rows.Count; i++)
            {

                string allocated = "";
                try
                {
                    ////sql = "SELECT sum(hours) FROM ( " +
                    ////   "select round(cast((sum(time_remaining_cutting * quantity_same) + sum(time_remaining_prepping * quantity_same) + sum(time_remianing_assembly * quantity_same)) as float) /60,2) as hours from dbo.view_worked_hours da " +
                    ////   "left join dbo.door d on da.door_id = d.id " +
                    ////   "where (da.department = 'Assembly' or da.department = 'Cutting' or da.department = 'Prepping') and " +
                    ////   "(time_remaining_cutting > 0 or time_remaining_prepping > 0 or time_remianing_assembly > 0) and " +
                    ////   "(status_id = 1 or status_id = 2) and staff_id = " + dtStaffID.Rows[i][0].ToString() +
                    ////   "group by staff_id,da.door_id) as a";
                    ///
                    //old string
                    sql = "select SUM(time_remaining)  from dbo.c_view_slimline_allocation where [Allocated to] = '" + dtStaffID.Rows[i][0].ToString() + "'";

                    //and staff_id = " + dtStaffID.Rows[i][0].ToString() +        //
                    using (SqlCommand cmdAllocated = new SqlCommand(sql, conn))
                    {
                        allocated = (string)cmdAllocated.ExecuteScalar().ToString();
                        if (allocated == "")
                            allocated = "0";
                    }
                }
                catch
                {
                    allocated = "0";
                }


                double overtimeTemp = Convert.ToDouble(dgSlimline.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgSlimline.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));       //dgSlimline.Rows[i].Cells[1].Value.ToString();
                worked = dgSlimline.Rows[i].Cells[3].Value.ToString();
                dgSlimline[4, i].Value = hours + " / " + worked + " " + Environment.NewLine + "" + allocated + " Allo";
            }




            dgSlimline.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgSlimline.Columns["set/worked"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlimline.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgSlimline.Columns["hours"].Visible = false;
            dgSlimline.Columns["worked"].Visible = false;
            dgSlimline.Columns["overtime"].Visible = false;

            conn.Close();
        }

        private void fillSlimlineDispatch()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "SlimlineDispatch");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgSlDispatch.DataSource = dt;
        }

        private void fillSlimlineStores()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
            cmd.Parameters.AddWithValue("@datePlan", dteDateSelection.Text);
            cmd.Parameters.AddWithValue("@dept", "SlimlineStores");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgSlStores.DataSource = dt;
        }

        private void fillPunch()
        {
            if (dgPunch.Columns.Contains("overtime") == true)
            {
                dgPunch.Columns.Remove("overtime");
            }
            if (dgPunch.Columns.Contains("set") == true)
            {
                dgPunch.Columns.Remove("set");
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

            dgPunch.Columns.Add("set", "set");

            for (int i = 0; i < dgPunch.Rows.Count; i++)
            {
                // MessageBox.Show(dgPunch.Rows[0].Cells[i].Value.ToString());
                double overtimeTemp = Convert.ToDouble(dgPunch.Rows[i].Cells[3].Value) * 0.8;
                //overtimeTemp = overtimeTemp + Convert.ToDouble(dgPunch.Rows[i].Cells[1].Value.ToString());
                //MessageBox.Show(dgPunch[2, i].Value.ToString());
                dgPunch[4, i].Value = Convert.ToString(Convert.ToDecimal(dgPunch.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));
                //MessageBox.Show(dgPunch[2, i].Value.ToString());
            }

            conn.Close();
            dgPunch.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPunch.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPunch.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dgPunch.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgPunch.Columns[1].Visible = false;
            dgPunch.Columns[3].Visible = false;


            //go through each row and hide allocation block
            foreach (DataGridViewRow row in dgPunch.Rows)
            {
                //MessageBox.Show(row.Cells[0].Value.ToString());
                if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    row.Height = 0;
            }
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


            //go through each row and hide allocation block
            foreach (DataGridViewRow row in dgLaser.Rows)
            {
                //MessageBox.Show(row.Cells[0].Value.ToString());
                if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    row.Height = 0;
            }
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
            SqlCommand cmd = new SqlCommand("SELECT [full placement] as 'Staff Placement',hours,PlacementID FROM view_planner_bend_staff where date_plan = @datePlan and department = @dept ORDER BY [Staff Name]", conn);
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

            //go through each row and hide allocation block
            foreach (DataGridViewRow row in dgBend.Rows)
            {
                //MessageBox.Show(row.Cells[0].Value.ToString());
                if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    row.Height = 0;
            }
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

         //overtime -- usp_power_planner_overtime_hours~
         SqlCommand cmdOT = new SqlCommand("usp_power_planner_overtime_hours", conn);
         cmdOT.CommandType = CommandType.StoredProcedure;
         cmdOT.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Welding";
         cmdOT.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

         var OTreader = cmdOT.ExecuteReader();
         //SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
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
         string sql = "select staff_id FROM view_planner_punch_staff WHERE department = 'Welding' AND date_plan = cast('" + dteDateSelection.Value.ToString("yyyyMMdd") + "' as date) ORDER BY [Staff Name]"; //12324
         DataTable dtStaffID = new DataTable();
         using (SqlCommand cmdStaffID = new SqlCommand(sql, conn))
         {
             SqlDataAdapter daStaffID = new SqlDataAdapter(cmdStaffID);
             daStaffID.Fill(dtStaffID);
         }


         //put the columns together into one column! :D
         string hours = "";
         string worked = "";
         for (int i = 0; i < dgWeld.Rows.Count; i++)
         {
             string allocated = "";
             try
             {
                 sql = "SELECT sum(hours) FROM (select round(cast(sum(time_remaining_weld * quantity_same) as float) /60,2) as hours from dbo.door_allocation da " +
                     "left join dbo.door d on da.door_id = d.id where da.department = 'welding' and (time_remaining_weld > 0) and (status_id = 1 or status_id = 2) and complete_weld = 0 and staff_id = " + dtStaffID.Rows[i][0].ToString() +
                     "group by staff_id,da.door_id) as a";

                 //and staff_id = " + dtStaffID.Rows[i][0].ToString() +        //
                 using (SqlCommand cmdAllocated = new SqlCommand(sql, conn))
                 {
                     allocated = (string)cmdAllocated.ExecuteScalar().ToString();
                     if (allocated == "")
                         allocated = "0";
                 }
             }
             catch
             {
                 allocated = "0";
             }
             double overtimeTemp = Convert.ToDouble(dgWeld.Rows[i].Cells[5].Value) * 0.8;
             hours = Convert.ToString(Convert.ToDecimal(dgWeld.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));   //dgWeld.Rows[i].Cells[1].Value.ToString();
             worked = dgWeld.Rows[i].Cells[3].Value.ToString();
             dgWeld[4, i].Value = hours + " / " + worked + " " + Environment.NewLine + "" + allocated + " Allo";


         }

         //dgWeld.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
         //dgWeld.Columns["hours"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
         dgWeld.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
         dgWeld.Columns["set/worked"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
         dgWeld.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
         dgWeld.Columns["hours"].Visible = false;
         dgWeld.Columns["worked"].Visible = false;
         dgWeld.Columns["overtime"].Visible = false;

         // conn.Close();


         //go through each row and hide allocation block
         foreach (DataGridViewRow row in dgWeld.Rows)
         {
             //MessageBox.Show(row.Cells[0].Value.ToString());
             if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                 row.Height = 0;
         }
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
            //this is wy

            //get all the staff ids
            //get the total assigned hours aswell 
            string sql = "select staff_id FROM view_planner_punch_staff WHERE department = 'Dressing' AND date_plan = cast('" + dteDateSelection.Value.ToString("yyyyMMdd") + "' as date) ORDER BY [Staff Name]";
            DataTable dtStaffID = new DataTable();
            using (SqlCommand cmdStaffID = new SqlCommand(sql, conn))
            {
                SqlDataAdapter daStaffID = new SqlDataAdapter(cmdStaffID);
                daStaffID.Fill(dtStaffID);
            }
            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgBuff.Rows.Count; i++)
            {
                string allocated = "";
                try
                {
                    sql = "SELECT sum(hours) FROM(select round(cast(sum(time_remaining_buff * quantity_same) as float) / 60, 2) as hours from dbo.door_allocation da left join dbo.door d on da.door_id = d.id " +
                             "where da.department = 'dressing' and(status_id = 1 or status_id = 2) and complete_buff = 0 and time_remaining_buff > 0 and staff_id = " + dtStaffID.Rows[i][0].ToString() +
                             "group by staff_id, da.door_id) as a";

                    using (SqlCommand cmdAllocated = new SqlCommand(sql, conn))
                    {
                        allocated = (string)cmdAllocated.ExecuteScalar().ToString();
                        if (allocated == "")
                            allocated = "0";
                    }
                }
                catch
                {
                    allocated = "0";
                }
                double overtimeTemp = Convert.ToDouble(dgBuff.Rows[i].Cells[5].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgBuff.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));    //dgBuff.Rows[i].Cells[1].Value.ToString();
                worked = dgBuff.Rows[i].Cells[3].Value.ToString();

                dgBuff[4, i].Value = hours + " / " + worked + " " + Environment.NewLine + "" + allocated + " Allo";
            }

            //dgBuff.Columns["Staff Placement"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgBuff.Columns["hours"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgBuff.Columns["set/worked"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgBuff.Columns["set/worked"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgBuff.Columns["Staff Placement"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgBuff.Columns["hours"].Visible = false;
            dgBuff.Columns["worked"].Visible = false;
            dgBuff.Columns["overtime"].Visible = false;

            conn.Close();

            //go through each row and hide allocation block
            foreach (DataGridViewRow row in dgBuff.Rows)
            {
                //MessageBox.Show(row.Cells[0].Value.ToString());
                if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    row.Height = 0;
            }
        }

        private void fillPaint()
        {
            //adjust dgv here cause why not?

            if (dgPaint.Columns.Contains("overtime") == true)
            {
                dgPaint.Columns.Remove("overtime");
            }
            if (dgPaint.Columns.Contains("set") == true)
            {
                dgPaint.Columns.Remove("set");
            }

            //ryucxd paint   12321
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            //  vvv old (before remaking _paint_sub_dept_temp_test
            //string sql = "SELECT  b.forename + ' ' + b.surname + CHAR(13) + COALESCE(d.sub_department,'') + CHAR(13) + a.placement_type  AS [Staff Placement], a.hours, a.id " +
            //                    "FROM dbo.power_plan_staff AS a " +
            //                    "INNER JOIN user_info.dbo.[user] AS b ON a.staff_id = b.id " +
            //                    "INNER JOIN dbo.power_plan_date as c ON a.date_id = c.id " +
            //                    "LEFT JOIN dbo.power_plan_paint_sub_dept_test_temp_2 as d ON a.id = d.placement_id " +
            //                    "WHERE c.date_plan = '" + dteDateSelection.Text + "' and a.department = 'Painting'  order by b.forename + ' ' + b.surname";  //ORDER BY [Staff Name]

            string sql = "SELECT  b.forename + ' ' + b.surname + CHAR(13) + COALESCE(d.up,'') + COALESCE(d.wash_wipe,'')  + COALESCE(d.etch,'')  +  COALESCE(d.sand,'')  +  COALESCE(d.powder_prime,'')  + " +
                "COALESCE(d.powder_coat, '') + COALESCE(d.oven, '') + COALESCE(d.wet_prep, '') + COALESCE(d.wet_paint, '') + COALESCE(a.placement_type, '')   AS[Staff Placement], a.hours, a.id " +
                "FROM dbo.power_plan_staff AS a INNER JOIN user_info.dbo.[user] AS b ON a.staff_id = b.id INNER JOIN dbo.power_plan_date as c ON a.date_id = c.id " +
                "LEFT JOIN dbo.view_power_plan_paint_sub_dept as d ON a.id = d.placement_id WHERE c.date_plan = '" + dteDateSelection.Text + "' and a.department = 'Painting'  order by b.forename + ' ' + b.surname";  //ORDER BY [Staff Name]  
            //new version joins a view that displays all of the check boxes in a neat row (1 is the dept and 0 = ''
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

            dgPaint.Columns.Add("set", "set");

            for (int i = 0; i < dgPaint.Rows.Count; i++)
            {
                double overtimeTemp = Convert.ToDouble(dgPaint.Rows[i].Cells[3].Value) * 0.8;
                dgPaint[4, i].Value = Convert.ToString(Convert.ToDecimal(dgPaint.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));
            }
            //old unworking code for adding OT to hours
            //for (int i = 0; i < dgPaint.Rows.Count; i++)
            //{
            //    double overtimeTemp = Convert.ToDouble(dgPaint.Rows[i].Cells[3].Value) * 0.8;
            //    overtimeTemp = overtimeTemp + Convert.ToDouble(dgPaint.Rows[i].Cells[1].Value.ToString());
            //    dgPaint[3, i].Value = overtimeTemp;
            //}

            conn.Close();
            dgPaint.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgPaint.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPaint.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgPaint.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgPaint.Columns[3].Visible = false;
            dgPaint.Columns[1].Visible = false;


            //go through each row and hide allocation block
            foreach (DataGridViewRow row in dgPaint.Rows)
            {
                //MessageBox.Show(row.Cells[0].Value.ToString());
                if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    row.Height = 0;
            }

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

            string sql = "select staff_id FROM view_planner_punch_staff WHERE department = 'Packing' AND date_plan = cast('" + dteDateSelection.Value.ToString("yyyyMMdd") + "' as date) ORDER BY [Staff Name]";
            DataTable dtStaffID = new DataTable();
            using (SqlCommand cmdStaffID = new SqlCommand(sql, conn)) 
            {
                SqlDataAdapter daStaffID = new SqlDataAdapter(cmdStaffID);
                daStaffID.Fill(dtStaffID);
            }

            //put the columns together into one column! :D
            string hours = "";
            string worked = "";
            for (int i = 0; i < dgPack.Rows.Count; i++)
            {
                string allocated = "";
                try
                {
                    sql = "SELECT sum(hours) FROM (select round(cast(sum(time_remaining_pack * quantity_same) as float) /60,2) as hours from dbo.door_allocation da " +
                        "left join dbo.door d on da.door_id = d.id where da.department = 'packing' and(status_id = 1 or status_id = 2) and complete_pack = 0 and time_remaining_pack > 0 and staff_id  = " + dtStaffID.Rows[i][0].ToString() +
                        "group by staff_id,da.door_id) as a";
                    using (SqlCommand cmdAllocated = new SqlCommand(sql, conn))
                    {
                        allocated = (string)cmdAllocated.ExecuteScalar().ToString();
                        if (allocated == "")
                            allocated = "0";
                    }
                }
                catch
                {
                    allocated = "0";
                }
                double overtimeTemp = Convert.ToDouble(dgPack.Rows[i].Cells[6].Value) * 0.8;
                hours = Convert.ToString(Convert.ToDecimal(dgPack.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));   // dgPack.Rows[i].Cells[1].Value.ToString();
                worked = dgPack.Rows[i].Cells[3].Value.ToString();
                dgPack[4, i].Value = hours + " / " + worked + Environment.NewLine + "£" + packValue.Rows[0][i].ToString() + " " + Environment.NewLine + "" + allocated + " Allo";
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

            sql = "select round(convert(float,sum(line_total)),2)  from dbo.door as a  inner join" +
                " (SELECT dbo.door.id, COALESCE (dbo.door_payment.door_cost, 0) + COALESCE (dbo.door_payment.extra_cost_total, 0) AS line_total " +
                " FROM dbo.door LEFT OUTER JOIN dbo.door_payment ON dbo.door.id = dbo.door_payment.door_id) as b on a.id = b.id    inner join dbo.door_type as c on a.door_type_id = c.id " +
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

            //go through each row and hide allocation block
            foreach (DataGridViewRow row in dgPack.Rows)
            {
                //MessageBox.Show(row.Cells[0].Value.ToString());
                if (row.Cells[0].Value.ToString().Contains("Allocation Block"))
                    row.Height = 0;
            }
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

        private void fillHSManagement()
        {
            string sql = "SELECT [full placement] + ' - ' + department as 'Staff Placement',hours,PlacementID FROM view_planner_punch_staff where date_plan = '" + dteDateSelection.Text + "' and (department = 'HS' OR department = 'Management') ORDER BY department,[Staff Name]";
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvHSManagement.DataSource = dt;
            dgvHSManagement.ClearSelection();
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


            //slimline version
            SqlCommand cmdDateSlimline = new SqlCommand("usp_get_unplaced_staff_slimline", conn);
            cmdDateSlimline.CommandType = CommandType.StoredProcedure;
            cmdDateSlimline.Parameters.AddWithValue("@placementDate", SqlDbType.Date).Value = dteDateSelection.Text;

            SqlDataAdapter daSlimline = new SqlDataAdapter(cmdDateSlimline);

            DataTable dtSlimline = new DataTable();

            daSlimline.Fill(dtSlimline);

            dgNotPlacementSL.DataSource = dtSlimline;



        }

        private void dteDateSelection_ValueChanged(object sender, EventArgs e)
        {
            //taking this out because its annoying when looking forward months.
            // fillgrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Laser", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void copyPlacementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCopyPlacements cp = new frmCopyPlacements(Convert.ToDateTime(dteDateSelection.Text));
            cp.ShowDialog();
            fillgrid();
        }

        private void btnAddSlimline_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;

            department_changed department_Changed = new department_changed();

            frmSelectStaff frmSS = new frmSelectStaff("Slimline", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
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

        private void clear_times(bool value)
        {

            dgSlimline.Columns[4].Visible = value;
            dgLaser.Columns[4].Visible = value;
            dgPunch.Columns[4].Visible = value;
            dgBend.Columns[4].Visible = value;
            dgWeld.Columns[4].Visible = value;
            dgBuff.Columns[4].Visible = value;
            dgPaint.Columns[4].Visible = value;
            dgPack.Columns[4].Visible = value;

            if (value == false) //we are hiding times
            {
                dgSlimline.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgLaser.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgPunch.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgBend.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgWeld.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgBuff.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgPaint.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgPack.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                dgSlimline.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgLaser.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgPunch.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgBend.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgWeld.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgBuff.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgPaint.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgPack.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }


            dgSlimline.Refresh();
            dgLaser.Refresh();
            dgPunch.Refresh();
            dgBend.Refresh();
            dgWeld.Refresh();
            dgBuff.Refresh();
            dgPaint.Refresh();
            dgPack.Refresh();

        }
        private void printScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printout_file_name = @"C:\temp\temp" + DateTime.Now.ToString("mmss") + ".jpg";
            //prompt for times
            DialogResult result = MessageBox.Show("Do you want to show times", "Power Plan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                clear_times(false); //false turns them off


                System.Threading.Thread.Sleep(3000);


                lbl_time.Text = DateTime.Now.ToString();
                lbl_time.Refresh();
                //  lbl_time.Visible = true;
                try
                {
                    Image bit = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

                    Graphics gs = Graphics.FromImage(bit);

                    gs.CopyFromScreen(new Point(0, 0), new Point(0, 0), bit.Size);

                    bit.Save(printout_file_name);

                    printImage();
                }
                catch
                {
                }
                lbl_time.Text = "";

                clear_times(true); //turns them back on
            }
            else
            {
                lbl_time.Text = DateTime.Now.ToString();
                lbl_time.Refresh();
                //  lbl_time.Visible = true;
                System.Threading.Thread.Sleep(3000);
                try
                {
                    Image bit = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

                    Graphics gs = Graphics.FromImage(bit);

                    gs.CopyFromScreen(new Point(0, 0), new Point(0, 0), bit.Size);

                    bit.Save(printout_file_name);

                    printImage();
                }
                catch
                {
                }
                lbl_time.Text = "";
            }
        }

        private void printImage()
        {
            //try
            //{
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, args) =>
            {
                Image i = Image.FromFile(printout_file_name);//@"C:\temp\temp" + DateTime.Now.ToString("mmss") + ".jpg";
                Point p = new Point(100, 100);
                args.Graphics.DrawImage(i, args.MarginBounds);
            };

            pd.DefaultPageSettings.Landscape = true;
            Margins margins = new Margins(50, 50, 50, 50);
            pd.DefaultPageSettings.Margins = margins;
            pd.Print();
            pd.Dispose();
            //}
            //catch
            //{
            //}
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
            remove_absents();

            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Stores", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();

            add_absents();
        }

        private void btnAddDispatch_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Dispatch", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddToolRoom_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("toolroom", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog(); //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
                                // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnAddCleaning_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Cleaning", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void BtnAddManagement_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("Management", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog(); //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
                                // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void BtnAddHS_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;
            frmSelectStaff frmSS = new frmSelectStaff("HS", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();
            fillgrid();
            add_absents();
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
            DialogResult result = MessageBox.Show("Are you sure you want to clear the plan?", "Clear Plan", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
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
        }

        private void ryucxdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelClass excel = new ExcelClass();
            string sql = "";
            int absent_row_number = 22;
            int skipDT = 0;
            int dt_row_number = 0;
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

                    //get datatable for absences
                    DataTable dtAbsents = new DataTable();
                    using (SqlConnection conn2 = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        conn2.Open();
                        sql = "select u.forename + ' ' + u.surname  + ' - ' + CAST(CASE WHEN absent_type = 2 then 'Full Holiday' WHEN absent_type = 3 then 'Half Holiday' WHEN absent_type = 5 then 'Absent' " +
                            "WHEN absent_type = 9 then 'Unpaid' end as nvarchar(max))  + ' - ' + Convert(varchar,date_absent,103)  from dbo.absent_holidays a left join [user_info].dbo.[user] u on u.id = a.staff_id " +
                            "where(absent_type = 2 or  absent_type = 3 or absent_type = 5 or absent_type = 8 or absent_type = 9) AND " +
                            "date_absent >= '" + Monday.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + Friday.ToString("yyyy-MM-dd") + "' and[current] = 1 and ShopFloor = -1 order by absent_type asc";
                        using (SqlCommand cmd = new SqlCommand(sql, conn2))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dtAbsents);
                        }
                        conn2.Close();
                    }



                    excel.openExcel(print, i, fileName, mondaySTR, tuesdaySTR, wednesdaySTR, thursdaySTR, fridaySTR, Convert.ToDouble(punching_hours), Convert.ToDouble(punching_OT), Convert.ToDouble(punching_AD),
                                               Convert.ToDouble(laser_hours), Convert.ToDouble(laser_OT), Convert.ToDouble(laser_AD),
                                               Convert.ToDouble(bending_hours), Convert.ToDouble(bending_OT), Convert.ToDouble(bending_AD),
                                               Convert.ToDouble(welding_hours), Convert.ToDouble(welding_OT), Convert.ToDouble(welding_AD),
                                               Convert.ToDouble(buffing_hours), Convert.ToDouble(buffing_OT), Convert.ToDouble(buffing_AD),
                                               Convert.ToDouble(painting_hours), Convert.ToDouble(painting_OT), Convert.ToDouble(painting_AD),
                                               Convert.ToDouble(packing_hours), Convert.ToDouble(packing_OT), Convert.ToDouble(packing_AD), dt_row_number, skipDT); ;

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
                    dt_row_number++;
                    absent_row_number++;
                    skipDT = -1;
                }
            }
            //get the right fields into this and
            //excel.print();
            //excel.closeExcel();
            MessageBox.Show("Printout has been sent to your default printer!");
        }

        private void dteDateSelection_CloseUp(object sender, EventArgs e)
        {
            txtSlimlineHours.Text = "0";
            txtSlimlineAD.Text = "0";
            txtSlimlineMen.Text = "0";
            txtSlimlineTotal.Text = "0";
            txtSLActualHours.Text = "0";

            txtPaintActualHours.Text = "0";
            txtPaintAD.Text = "0";
            txtPaintHours.Text = "0";
            txtPaintingTotal.Text = "0";
            txtPaintMen.Text = "0";
            txtPaintOT.Text = "0";

            txtlaserActualHours.Text = "0";
            txtLaserAD.Text = "0";
            txtLaserHours.Text = "0";
            txtLaserTotal.Text = "0";
            txtLaserMen.Text = "0";
            txtLaserOT.Text = "0";

            txtBendActualHours.Text = "0";
            txtBendAD.Text = "0";
            txtBendHours.Text = "0";
            txtBendingTotal.Text = "0";
            txtBendMen.Text = "0";
            txtBendOT.Text = "0";

            txtWeldActualHours.Text = "0";
            txtWeldAD.Text = "0";
            txtWeldHours.Text = "0";
            txtWeldingTotal.Text = "0";
            txtWeldMen.Text = "0";
            txtWeldOT.Text = "0";

            txtBuffActualHours.Text = "0";
            txtBuffAD.Text = "0";
            txtBuffHours.Text = "0";
            txtBuffingTotal.Text = "0";
            txtBuffMen.Text = "0";
            txtBuffOT.Text = "0";

            txtPaintActualHours.Text = "0";
            txtPaintAD.Text = "0";
            txtPaintHours.Text = "0";
            txtPaintingTotal.Text = "0";
            txtPaintMen.Text = "0";
            txtPaintOT.Text = "0";

            txtPackActualHours.Text = "0";
            txtPackAD.Text = "0";
            txtPackHours.Text = "0";
            txtPackingTotal.Text = "0";
            txtPackMen.Text = "0";
            txtPackOT.Text = "0";

            fillgrid();
            add_absents();
        }

        private void refreshSelectedDepartments()
        {
            //this needs to go through each of the departments that are selected from the prior screen :)
            if (department_changed.slimlineDispatchSelected == -1)
                fillSlimlineDispatch();
            if (department_changed.slimlineStoresSelected == -1)
                fillSlimlineStores();
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
            {
                fillManagement();
                fillHSManagement();
            }
            if (department_changed.hsSelected == -1)
            {
                fillHS();
                fillHSManagement();
            }
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

            //need to work out the hours here to pass over
            string dropped_gained_hours = "";
            double hours = Convert.ToDouble(Convert.ToDecimal(dgWeld.Rows[e.RowIndex].Cells[1].Value) + Convert.ToDecimal((Convert.ToDouble(dgWeld.Rows[e.RowIndex].Cells[5].Value) * 0.8)));
            string staff_name = Convert.ToString(dgWeld.Rows[e.RowIndex].Cells[0].Value);
            double final_hours = 0;
            int staff_id = 0;
            staff_id = staff_name.IndexOf(" ", staff_name.IndexOf(" ") + 1); //staff id is being used as aa temp int var here
            staff_name = staff_name.Substring(0, staff_id);

            string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + staff_name + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                sql = "SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] FROM dbo.door_part_completion_log WHERE staff_id = " +
                    staff_id.ToString() + " AND CAST(part_complete_date as DATE) = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' AND (part_status = 'Complete' or part_status = 'partial') AND op = 'Welding'  GROUP BY staff_id),0)";
                double worked = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    worked = Convert.ToDouble(cmd.ExecuteScalar());

                final_hours = Math.Round(hours - worked,2);

                if (final_hours < 0)
                {
                    final_hours = final_hours * -1;
                    dropped_gained_hours = "Gained - " + final_hours;
                }
                else
                    dropped_gained_hours = "Dropped - " + final_hours;
                conn.Close();
            }


            frmChronological frm = new frmChronological(Convert.ToString(dgWeld.Rows[e.RowIndex].Cells[0].Value), "Welding", dteDateSelection.Value, dropped_gained_hours);
            frm.ShowDialog();
        }

        private void dgPack_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //need to work out the hours here to pass over
            string dropped_gained_hours = "";
            double hours = Convert.ToDouble(Convert.ToDecimal(dgPack.Rows[e.RowIndex].Cells[1].Value) + Convert.ToDecimal((Convert.ToDouble(dgPack.Rows[e.RowIndex].Cells[6].Value) * 0.8)));
            string staff_name = Convert.ToString(dgPack.Rows[e.RowIndex].Cells[0].Value);
            double final_hours = 0;
            int staff_id = 0;
            staff_id = staff_name.IndexOf(" ", staff_name.IndexOf(" ") + 1); //staff id is being used as aa temp int var here
            staff_name = staff_name.Substring(0, staff_id);

            string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + staff_name + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                sql = "SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] FROM dbo.door_part_completion_log WHERE staff_id = " +
                    staff_id.ToString() + " AND CAST(part_complete_date as DATE) = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' AND (part_status = 'Complete' or part_status = 'partial') AND op = 'Packing'  GROUP BY staff_id),0)";
                double worked = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    worked = Convert.ToDouble(cmd.ExecuteScalar());

                final_hours = Math.Round(hours - worked,2);

                if (final_hours < 0)
                {
                    final_hours = final_hours * -1;
                    dropped_gained_hours = "Gained - " + final_hours;
                }
                else
                    dropped_gained_hours = "Dropped - " + final_hours;
                conn.Close();
            }

            dgPack.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgPack.Rows[e.RowIndex].Cells[0].Value), "Packing", dteDateSelection.Value, dropped_gained_hours);
            frm.ShowDialog();
        }

        private void dgBuff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //need to work out the hours here to pass over
            string dropped_gained_hours = "";
            double hours = Convert.ToDouble(Convert.ToDecimal(dgBuff.Rows[e.RowIndex].Cells[1].Value) + Convert.ToDecimal((Convert.ToDouble(dgBuff.Rows[e.RowIndex].Cells[5].Value) * 0.8)));
            string staff_name = Convert.ToString(dgBuff.Rows[e.RowIndex].Cells[0].Value);
            double final_hours = 0;
            int staff_id = 0;
            staff_id = staff_name.IndexOf(" ", staff_name.IndexOf(" ") + 1); //staff id is being used as aa temp int var here
            staff_name = staff_name.Substring(0, staff_id);

            string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + staff_name + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                sql = "SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] FROM dbo.door_part_completion_log WHERE staff_id = " +
                    staff_id.ToString() + " AND CAST(part_complete_date as DATE) = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' AND (part_status = 'Complete' or part_status = 'partial') AND op = 'Buffing'  GROUP BY staff_id),0)";
                double worked = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    worked = Convert.ToDouble(cmd.ExecuteScalar());

                final_hours = Math.Round(hours - worked,2);

                if (final_hours < 0)
                {
                    final_hours = final_hours * -1;
                    dropped_gained_hours = "Gained - " + final_hours;
                }
                else
                    dropped_gained_hours = "Dropped - " + final_hours;
                conn.Close();
            }

            dgBuff.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgBuff.Rows[e.RowIndex].Cells[0].Value), "Buffing", dteDateSelection.Value, dropped_gained_hours);
            frm.ShowDialog();
        }

        private void dgNotPlaced_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgNotPlaced.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Empty)//(dgNotPlaced.Rows[e.RowIndex].Cells[1].Value.ToString().Contains("HOLIDAY"))
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
        }

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
            fillShopGoals();
            currentAvailable();

            add_absents();
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

        private void fillShopGoals()
        {
            //first we null everything
            slimline_9_30.Text = "";
            slimline_11_30.Text = "";
            slimline_2_30.Text = "";
            slimline_4_00.Text = "";
            laser_9_30.Text = "";
            laser_11_30.Text = "";
            laser_2_30.Text = "";
            laser_4_00.Text = "";
            punching_9_30.Text = "";
            punching_11_30.Text = "";
            punching_2_30.Text = "";
            punching_4_00.Text = "";
            bending_9_30.Text = "";
            bending_11_30.Text = "";
            bending_2_30.Text = "";
            bending_4_00.Text = "";
            welding_9_30.Text = "";
            welding_11_30.Text = "";
            welding_2_30.Text = "";
            welding_4_00.Text = "";
            buffing_9_30.Text = "";
            buffing_11_30.Text = "";
            buffing_2_30.Text = "";
            buffing_4_00.Text = "";
            painting_9_30.Text = "";
            painting_11_30.Text = "";
            painting_2_30.Text = "";
            painting_4_00.Text = "";
            packing_9_30.Text = "";
            packing_11_30.Text = "";
            packing_2_30.Text = "";
            packing_4_00.Text = "";

            //txtSlimlinePercent.Text = "";
            txtSlimlinePercent.BackColor = Color.Empty;

            //txtPunchPercent.Text = "";

            txtPunchPercent.BackColor = Color.Empty;

            //txtLaserPercent.Text = "";
            txtLaserPercent.BackColor = Color.Empty;

            //txtBendingPercent.Text = "";
            txtBendingPercent.BackColor = Color.Empty;

            //txtWeldPercent.Text = "";
            txtWeldPercent.BackColor = Color.Empty;

            //txtBuffPercent.Text = "";
            txtBuffPercent.BackColor = Color.Empty;

            //txtPaintPercent.Text = "";
            txtPaintPercent.BackColor = Color.Empty;

            //txtPackPercent.Text = "";
            txtPackPercent.BackColor = Color.Empty;

            //read allt he data from the selected dte and put them into thje 100 textbnoxes
            string sql = "SELECT COALESCE(round([9_30_slimline] * 100,2),'9999') as [9_30_slimline],COALESCE(round([11_30_slimline] * 100, 2),'9999') as [11_30_slimline],COALESCE(round([2_30_slimline] * 100, 2),'9999') as [2_30_slimline]," +
                "COALESCE(round([4_00_slimline] * 100, 2),'9999') as [4_00_slimline],COALESCE(round([9_30_punch] * 100, 2),'9999') as [9_30_punch] ,COALESCE(round([11_30_punch] * 100, 2),'9999') as [11_30_punch],COALESCE(round([2_30_punch] * 100, 2),'9999') as [2_30_punch]," +
                "COALESCE(round([4_00_punch] * 100, 2),'9999') as [4_00_punch],COALESCE(round([9_30_laser] * 100, 2),'9999') as [9_30_laser],COALESCE(round([11_30_laser] * 100, 2),'9999') as [11_30_laser],COALESCE(round([2_30_laser] * 100, 2),'9999') as [2_30_laser] ," +
                "COALESCE(round([4_00_laser] * 100, 2),'9999') as [4_00_laser] ,COALESCE(round([9_30_bend] * 100, 2),'9999') as [9_30_bend],COALESCE(round([11_30_bend] * 100, 2),'9999') as [11_30_bend],COALESCE(round([2_30_bend] * 100, 2),'9999') as [2_30_bend]," +
                "COALESCE(round([4_00_bend] * 100, 2),'9999') as [4_00_bend],COALESCE(round([9_30_weld] * 100, 2),'9999') as [9_30_weld],COALESCE(round([11_30_weld] * 100, 2),'9999') as [11_30_weld],COALESCE(round([2_30_weld] * 100, 2),'9999') as[2_30_weld] ," +
                "COALESCE(round([4_00_weld] * 100, 2),'9999') as [4_00_weld],COALESCE(round([9_30_buff] * 100, 2),'9999') as[9_30_buff] ,COALESCE(round([11_30_buff] * 100, 2),'9999') as [11_30_buff],COALESCE(round([2_30_buff] * 100, 2),'9999') as [2_30_buff]," +
                "COALESCE(round([4_00_buff] * 100, 2),'9999') as[4_00_buff] ,COALESCE(round([9_30_paint] * 100, 2),'9999') as[9_30_paint] ,COALESCE(round([11_30_paint] * 100, 2),'9999') as[11_30_paint],COALESCE(round([2_30_paint] * 100, 2),'9999') as [2_30_paint]," +
                "COALESCE(round([4_00_paint] * 100, 2),'9999') as [4_00_paint],COALESCE(round([9_30_pack] * 100, 2),'9999') as [9_30_pack],COALESCE(round([11_30_pack] * 100, 2),'9999') as [11_30_pack],COALESCE(round([2_30_pack] * 100, 2),'9999') as [2_30_pack]," +
                "COALESCE(round([4_00_pack] * 100, 2),'9999') as[4_00_pack] FROM dbo.power_plan_shop_goals WHERE CAST(date_goal as date) = '" + dteDateSelection.Value.ToString("yyyy-MM-dd") + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        slimline_9_30.Text = Convert.ToString((rdr["9_30_slimline"]));
                        slimline_11_30.Text = Convert.ToString((rdr["11_30_slimline"]));
                        slimline_2_30.Text = Convert.ToString((rdr["2_30_slimline"]));
                        slimline_4_00.Text = Convert.ToString((rdr["4_00_slimline"]));
                        laser_9_30.Text = Convert.ToString((rdr["9_30_laser"]));
                        laser_11_30.Text = Convert.ToString((rdr["11_30_laser"]));
                        laser_2_30.Text = Convert.ToString((rdr["2_30_laser"]));
                        laser_4_00.Text = Convert.ToString((rdr["4_00_laser"]));
                        punching_9_30.Text = Convert.ToString((rdr["9_30_punch"]));
                        punching_11_30.Text = Convert.ToString((rdr["11_30_punch"]));
                        punching_2_30.Text = Convert.ToString((rdr["2_30_punch"]));
                        punching_4_00.Text = Convert.ToString((rdr["4_00_punch"]));
                        bending_9_30.Text = Convert.ToString((rdr["9_30_bend"]));
                        bending_11_30.Text = Convert.ToString((rdr["11_30_bend"]));
                        bending_2_30.Text = Convert.ToString((rdr["2_30_bend"]));
                        bending_4_00.Text = Convert.ToString((rdr["4_00_bend"]));
                        welding_9_30.Text = Convert.ToString((rdr["9_30_weld"]));
                        welding_11_30.Text = Convert.ToString((rdr["11_30_weld"]));
                        welding_2_30.Text = Convert.ToString((rdr["2_30_weld"]));
                        welding_4_00.Text = Convert.ToString((rdr["4_00_weld"]));
                        buffing_9_30.Text = Convert.ToString((rdr["9_30_buff"]));
                        buffing_11_30.Text = Convert.ToString((rdr["11_30_buff"]));
                        buffing_2_30.Text = Convert.ToString((rdr["2_30_buff"]));
                        buffing_4_00.Text = Convert.ToString((rdr["4_00_buff"]));
                        painting_9_30.Text = Convert.ToString((rdr["9_30_paint"]));
                        painting_11_30.Text = Convert.ToString((rdr["11_30_paint"]));
                        painting_2_30.Text = Convert.ToString((rdr["2_30_paint"]));
                        painting_4_00.Text = Convert.ToString((rdr["4_00_paint"]));
                        packing_9_30.Text = Convert.ToString((rdr["9_30_pack"]));
                        packing_11_30.Text = Convert.ToString((rdr["11_30_pack"]));
                        packing_2_30.Text = Convert.ToString((rdr["2_30_pack"]));
                        packing_4_00.Text = Convert.ToString((rdr["4_00_pack"]));
                    }
                    rdr.Close();

                    //quickly fill the current %%%%%
                    sql = "SELECT ROUND(percentage_complete_slimline * 100, 2) as percentage_complete_slimline,ROUND(percentage_complete_laser * 100, 2) as percentage_complete_laser ," +
                        "ROUND(percentage_complete_punch * 100, 2) as percentage_complete_punch,ROUND(percentage_complete_bend * 100, 2) as percentage_complete_bend," +
                        "ROUND(percentage_complete_weld * 100, 2) as percentage_complete_weld,ROUND(percentage_complete_buff * 100, 2) as percentage_complete_buff," +
                        "ROUND(percentage_complete_paint * 100, 2) as percentage_complete_paint,ROUND(percentage_complete_pack * 100, 2) as percentage_complete_pack" +
                        " FROM dbo.view_daily_shop_goals WHERE CAST(date_goal as date) = '" + dteDateSelection.Value.ToString("yyyy-MM-dd") + "'";
                    using (SqlCommand cmd2 = new SqlCommand(sql, conn))
                    {
                        SqlDataReader rdr2 = cmd2.ExecuteReader();
                        if (rdr2.Read())
                        {
                            txtSlimlinePercent.Text = Convert.ToString((rdr2["percentage_complete_slimline"]));
                            txtLaserPercent.Text = Convert.ToString((rdr2["percentage_complete_laser"]));
                            txtPunchPercent.Text = Convert.ToString((rdr2["percentage_complete_punch"]));
                            txtBendingPercent.Text = Convert.ToString((rdr2["percentage_complete_bend"]));
                            txtWeldPercent.Text = Convert.ToString((rdr2["percentage_complete_weld"]));
                            txtBuffPercent.Text = Convert.ToString((rdr2["percentage_complete_buff"]));
                            txtPaintPercent.Text = Convert.ToString((rdr2["percentage_complete_paint"]));
                            txtPackPercent.Text = Convert.ToString((rdr2["percentage_complete_pack"]));
                        }
                        else
                        {
                            txtSlimlinePercent.Text = "0";
                            txtLaserPercent.Text = "0";
                            txtPunchPercent.Text = "0";
                            txtBendingPercent.Text = "0";
                            txtWeldPercent.Text = "0";
                            txtBuffPercent.Text = "0";
                            txtPaintPercent.Text = "0";
                            txtPackPercent.Text = "0";
                        }
                    }
                    conn.Close();
                }

                //if its 9999 then make the textbox null

                //slimline
                if (slimline_9_30.Text == "9999")
                    slimline_9_30.Text = "";
                if (slimline_11_30.Text == "9999")
                    slimline_11_30.Text = "";
                if (slimline_2_30.Text == "9999")
                    slimline_2_30.Text = "";
                if (slimline_4_00.Text == "9999")
                    slimline_4_00.Text = "";

                //punching
                if (punching_9_30.Text == "9999")
                    punching_9_30.Text = "";
                if (punching_11_30.Text == "9999")
                    punching_11_30.Text = "";
                if (punching_2_30.Text == "9999")
                    punching_2_30.Text = "";
                if (punching_4_00.Text == "9999")
                    punching_4_00.Text = "";

                //laser
                if (laser_9_30.Text == "9999")
                    laser_9_30.Text = "";
                if (laser_11_30.Text == "9999")
                    laser_11_30.Text = ""; //
                if (laser_2_30.Text == "9999")
                    laser_2_30.Text = "";
                if (laser_4_00.Text == "9999")
                    laser_4_00.Text = "";

                //bending
                if (bending_9_30.Text == "9999")
                    bending_9_30.Text = "";
                if (bending_11_30.Text == "9999")
                    bending_11_30.Text = "";
                if (bending_2_30.Text == "9999")
                    bending_2_30.Text = "";
                if (bending_4_00.Text == "9999")
                    bending_4_00.Text = "";

                //weld
                if (welding_9_30.Text == "9999")
                    welding_9_30.Text = "";
                if (welding_11_30.Text == "9999")
                    welding_11_30.Text = "";
                if (welding_2_30.Text == "9999")
                    welding_2_30.Text = "";
                if (welding_4_00.Text == "9999")
                    welding_4_00.Text = "";

                //buff
                if (buffing_9_30.Text == "9999")
                    buffing_9_30.Text = "";
                if (buffing_11_30.Text == "9999")
                    buffing_11_30.Text = "";
                if (buffing_2_30.Text == "9999")
                    buffing_2_30.Text = "";
                if (buffing_4_00.Text == "9999")
                    buffing_4_00.Text = "";

                //paint
                if (painting_9_30.Text == "9999")
                    painting_9_30.Text = "";
                if (painting_11_30.Text == "9999")
                    painting_11_30.Text = "";
                if (painting_2_30.Text == "9999")
                    painting_2_30.Text = "";
                if (painting_4_00.Text == "9999")
                    painting_4_00.Text = "";

                //pack
                if (packing_9_30.Text == "9999")
                    packing_9_30.Text = "";
                if (packing_11_30.Text == "9999")
                    packing_11_30.Text = "";
                if (packing_2_30.Text == "9999")
                    packing_2_30.Text = "";
                if (packing_4_00.Text == "9999")
                    packing_4_00.Text = "";

                //now we add colourssss

                //slimline
                if (slimline_9_30.Text == "")
                    slimline_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(slimline_9_30.Text) > 25)
                    slimline_9_30.BackColor = Color.DarkSeaGreen;
                else
                    slimline_9_30.BackColor = Color.PaleVioletRed;

                if (slimline_11_30.Text == "")
                    slimline_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(slimline_11_30.Text) > 50)
                    slimline_11_30.BackColor = Color.DarkSeaGreen;
                else
                    slimline_11_30.BackColor = Color.PaleVioletRed;

                if (slimline_2_30.Text == "")
                    slimline_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(slimline_2_30.Text) > 75)
                    slimline_2_30.BackColor = Color.DarkSeaGreen;
                else
                    slimline_2_30.BackColor = Color.PaleVioletRed;

                if (slimline_4_00.Text == "")
                    slimline_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(slimline_4_00.Text) > 100)
                    slimline_4_00.BackColor = Color.DarkSeaGreen;
                else
                    slimline_4_00.BackColor = Color.PaleVioletRed;

                //laser
                if (laser_9_30.Text == "")
                    laser_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(laser_9_30.Text) > 25)
                    laser_9_30.BackColor = Color.DarkSeaGreen;
                else
                    laser_9_30.BackColor = Color.PaleVioletRed;

                if (laser_11_30.Text == "")
                    laser_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(laser_11_30.Text) > 50)
                    laser_11_30.BackColor = Color.DarkSeaGreen;
                else
                    laser_11_30.BackColor = Color.PaleVioletRed;

                if (laser_2_30.Text == "")
                    laser_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(laser_2_30.Text) > 75)
                    laser_2_30.BackColor = Color.DarkSeaGreen;
                else
                    laser_2_30.BackColor = Color.PaleVioletRed;

                if (laser_4_00.Text == "")
                    laser_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(laser_4_00.Text) > 100)
                    laser_4_00.BackColor = Color.DarkSeaGreen;
                else
                    laser_4_00.BackColor = Color.PaleVioletRed;

                //punch
                if (punching_9_30.Text == "")
                    punching_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(punching_9_30.Text) > 25)
                    punching_9_30.BackColor = Color.DarkSeaGreen;
                else
                    punching_9_30.BackColor = Color.PaleVioletRed;

                if (punching_11_30.Text == "")
                    punching_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(punching_11_30.Text) > 50)
                    punching_11_30.BackColor = Color.DarkSeaGreen;
                else
                    punching_11_30.BackColor = Color.PaleVioletRed;

                if (punching_2_30.Text == "")
                    punching_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(punching_2_30.Text) > 75)
                    punching_2_30.BackColor = Color.DarkSeaGreen;
                else
                    punching_2_30.BackColor = Color.PaleVioletRed;

                if (punching_4_00.Text == "")
                    punching_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(punching_4_00.Text) > 100)
                    punching_4_00.BackColor = Color.DarkSeaGreen;
                else
                    punching_4_00.BackColor = Color.PaleVioletRed;

                //bend

                if (bending_9_30.Text == "")
                    bending_9_30.BackColor = Color.Empty;
                else if (bending_9_30.Text == "")
                    bending_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(bending_9_30.Text) > 25)
                    bending_9_30.BackColor = Color.DarkSeaGreen;
                else
                    bending_9_30.BackColor = Color.PaleVioletRed;

                if (bending_11_30.Text == "")
                    bending_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(bending_11_30.Text) > 50)
                    bending_11_30.BackColor = Color.DarkSeaGreen;
                else
                    bending_11_30.BackColor = Color.PaleVioletRed;

                if (bending_2_30.Text == "")
                    bending_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(bending_2_30.Text) > 75)
                    bending_2_30.BackColor = Color.DarkSeaGreen;
                else
                    bending_2_30.BackColor = Color.PaleVioletRed;

                if (bending_4_00.Text == "")
                    bending_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(bending_4_00.Text) > 100)
                    bending_4_00.BackColor = Color.DarkSeaGreen;
                else
                    bending_4_00.BackColor = Color.PaleVioletRed;

                //weld
                if (welding_9_30.Text == "")
                    welding_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(welding_9_30.Text) > 25)
                    welding_9_30.BackColor = Color.DarkSeaGreen;
                else
                    welding_9_30.BackColor = Color.PaleVioletRed;

                if (welding_11_30.Text == "")
                    welding_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(welding_11_30.Text) > 50)
                    welding_11_30.BackColor = Color.DarkSeaGreen;
                else
                    welding_11_30.BackColor = Color.PaleVioletRed;

                if (welding_2_30.Text == "")
                    welding_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(welding_2_30.Text) > 75)
                    welding_2_30.BackColor = Color.DarkSeaGreen;
                else
                    welding_2_30.BackColor = Color.PaleVioletRed;

                if (welding_4_00.Text == "")
                    welding_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(welding_4_00.Text) > 100)
                    welding_4_00.BackColor = Color.DarkSeaGreen;
                else
                    welding_4_00.BackColor = Color.PaleVioletRed;

                //buff
                if (buffing_9_30.Text == "")
                    buffing_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(buffing_9_30.Text) > 25)
                    buffing_9_30.BackColor = Color.DarkSeaGreen;
                else
                    buffing_9_30.BackColor = Color.PaleVioletRed;

                if (buffing_11_30.Text == "")
                    buffing_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(buffing_11_30.Text) > 50)
                    buffing_11_30.BackColor = Color.DarkSeaGreen;
                else
                    buffing_11_30.BackColor = Color.PaleVioletRed;

                if (buffing_2_30.Text == "")
                    buffing_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(buffing_2_30.Text) > 75)
                    buffing_2_30.BackColor = Color.DarkSeaGreen;
                else
                    buffing_2_30.BackColor = Color.PaleVioletRed;

                if (buffing_4_00.Text == "")
                    buffing_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(buffing_4_00.Text) > 100)
                    buffing_4_00.BackColor = Color.DarkSeaGreen;
                else
                    buffing_4_00.BackColor = Color.PaleVioletRed;

                //paint
                if (painting_9_30.Text == "")
                    painting_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(painting_9_30.Text) > 25)
                    painting_9_30.BackColor = Color.DarkSeaGreen;
                else
                    painting_9_30.BackColor = Color.PaleVioletRed;

                if (painting_11_30.Text == "")
                    painting_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(painting_11_30.Text) > 50)
                    painting_11_30.BackColor = Color.DarkSeaGreen;
                else
                    painting_11_30.BackColor = Color.PaleVioletRed;

                if (painting_2_30.Text == "")
                    painting_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(painting_2_30.Text) > 75)
                    painting_2_30.BackColor = Color.DarkSeaGreen;
                else
                    painting_2_30.BackColor = Color.PaleVioletRed;

                if (painting_4_00.Text == "")
                    painting_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(painting_4_00.Text) > 100)
                    painting_4_00.BackColor = Color.DarkSeaGreen;
                else
                    painting_4_00.BackColor = Color.PaleVioletRed;

                //pack
                if (packing_9_30.Text == "")
                    packing_9_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(packing_9_30.Text) > 25)
                    packing_9_30.BackColor = Color.DarkSeaGreen;
                else
                    packing_9_30.BackColor = Color.PaleVioletRed;

                if (packing_11_30.Text == "")
                    packing_11_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(packing_11_30.Text) > 50)
                    packing_11_30.BackColor = Color.DarkSeaGreen;
                else
                    packing_11_30.BackColor = Color.PaleVioletRed;

                if (packing_2_30.Text == "")
                    packing_2_30.BackColor = Color.Empty;
                else if (Convert.ToDouble(packing_2_30.Text) > 75)
                    packing_2_30.BackColor = Color.DarkSeaGreen;
                else
                    packing_2_30.BackColor = Color.PaleVioletRed;

                if (packing_4_00.Text == "")
                    packing_4_00.BackColor = Color.Empty;
                else if (Convert.ToDouble(packing_4_00.Text) > 100)
                    packing_4_00.BackColor = Color.DarkSeaGreen;
                else
                    packing_4_00.BackColor = Color.PaleVioletRed;

                //colours for the current boxesm
                int temp = 0;
                if (slimline_9_30.Text.Length > 0)
                {
                    if (slimline_11_30.Text.Length > 0)
                    {
                        if (slimline_2_30.Text.Length > 0)
                        {
                            if (slimline_4_00.Text.Length > 0)
                                temp = 100;
                            else
                                temp = 75;
                        }
                        else
                            temp = 50;
                    }
                    else
                        temp = 25;
                }

                if (Convert.ToDouble(txtSlimlinePercent.Text) > temp)
                    txtSlimlinePercent.BackColor = Color.DarkSeaGreen;
                else
                    txtSlimlinePercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtPunchPercent.Text) > temp)
                    txtPunchPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtPunchPercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtLaserPercent.Text) > temp)
                    txtLaserPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtLaserPercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtBendingPercent.Text) > temp)
                    txtBendingPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtBendingPercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtWeldPercent.Text) > temp)
                    txtWeldPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtWeldPercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtBuffPercent.Text) > temp)
                    txtBuffPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtBuffPercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtPaintPercent.Text) > temp)
                    txtPaintPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtPaintPercent.BackColor = Color.PaleVioletRed;

                if (Convert.ToDouble(txtPackPercent.Text) > temp)
                    txtPackPercent.BackColor = Color.DarkSeaGreen;
                else
                    txtPackPercent.BackColor = Color.PaleVioletRed;
            }
        }

        private void dgSlimline_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            dgSlimline.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgSlimline.Rows[e.RowIndex].Cells[0].Value), "Slimline", dteDateSelection.Value, "");
            frm.ShowDialog();
        }

        private void btnSlimlineDispatch_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;

            department_changed department_Changed = new department_changed();

            frmSelectStaff frmSS = new frmSelectStaff("SlimlineDispatch", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void btnSlimlineStores_Click(object sender, EventArgs e)
        {
            remove_absents();
            skipMessageBox = 2;

            department_changed department_Changed = new department_changed();
            frmSelectStaff frmSS = new frmSelectStaff("SlimlineStores", Convert.ToDateTime(dteDateSelection.Text));
            frmSS.ShowDialog();

            //instead of fill grid we're going to use refreshSelectedDepartments and only refresh the ones that need it
            // fillgrid();
            refreshSelectedDepartments();
            add_absents();
        }

        private void currentAvailable()
        {
            string sql = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    sql = "select round(punching,2),round(bending,2),round(welding,2),round(buffing,2),round(painting,2),round(packing,2) from dbo.power_planner_7_30_available WHERE date_plan = '" + dteDateSelection.Text + "'";
                    using (SqlCommand cmd = new SqlCommand("usp_power_planner_available_work", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@insert", SqlDbType.Int).Value = 0;

                        SqlDataAdapter reader = new SqlDataAdapter(cmd);
                        DataTable availableWork = new DataTable();
                        reader.Fill(availableWork);
                        txtAvailPunching.Text = availableWork.Rows[0][0].ToString();
                        txtAvailBending.Text = availableWork.Rows[0][1].ToString();
                        txtAvailWelding.Text = availableWork.Rows[0][2].ToString();
                        txtAvailBuffing.Text = availableWork.Rows[0][3].ToString();
                        txtAvailPainting.Text = availableWork.Rows[0][4].ToString();
                        txtAvailPacking.Text = availableWork.Rows[0][5].ToString();
                    }
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter reader = new SqlDataAdapter(cmd);
                        DataTable availableWork = new DataTable();
                        reader.Fill(availableWork);
                        txt730punching.Text = availableWork.Rows[0][0].ToString();
                        txt730Bending.Text = availableWork.Rows[0][1].ToString();
                        txt730Welding.Text = availableWork.Rows[0][2].ToString();
                        txt730Buffing.Text = availableWork.Rows[0][3].ToString();
                        txt730Painting.Text = availableWork.Rows[0][4].ToString();
                        txt730Packing.Text = availableWork.Rows[0][5].ToString();
                    }
                }
            }
            catch
            {
                txtAvailPunching.Text = "0";
                txtAvailBending.Text = "0";
                txtAvailWelding.Text = "0";
                txtAvailBuffing.Text = "0";
                txtAvailPainting.Text = "0";
                txtAvailPacking.Text = "0";
                txt730punching.Text = "0";
                txt730Bending.Text = "0";
                txt730Welding.Text = "0";
                txt730Buffing.Text = "0";
                txt730Painting.Text = "0";
                txt730Packing.Text = "0";
            }

            //while we are here also get the current available values!!!
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_power_planner_available_work", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@insert", SqlDbType.Int).Value = 2;

                    SqlDataAdapter reader = new SqlDataAdapter(cmd);
                    DataTable availableWork = new DataTable();
                    reader.Fill(availableWork);
                    txtPunchValue.Text = availableWork.Rows[0][0].ToString();
                    txtBendValue.Text = availableWork.Rows[0][1].ToString();
                    txtWeldValue.Text = availableWork.Rows[0][2].ToString();
                    txtBuffValue.Text = availableWork.Rows[0][3].ToString();
                    txtPaintValue.Text = availableWork.Rows[0][4].ToString();
                    txtPackValue.Text = availableWork.Rows[0][5].ToString();
                }
            }

        }

        private void MenuMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void kevinMessage()
        {

            //string person = login.userFullName;
            //person = person.Substring(0, person.IndexOf(" "));

            //if (person == "Kevin") //if its kevin take a different route and prompt for a message
            //{
            //    DialogResult result = MessageBox.Show("Would you like to open the message screen before opening the Power Planner?", "Power Planner", MessageBoxButtons.YesNo);
            //    if (result == DialogResult.Yes)
            //    {
            //        frmKevinMessage KH = new frmKevinMessage(-1);
            //        KH.ShowDialog();
            //    }

            //}
            //else if (person == "Other")
            //{

            //}
            //else
            //{
            //    string sql = "SELECT top 1  COALESCE(" + person + ",0) FROM dbo.kevinMessage where  " + person + " is null order by id desc";
            //    using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            //    {
            //        using (SqlCommand cmd = new SqlCommand(sql, conn))
            //        {
            //            conn.Open();
            //            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //            DataTable dt = new DataTable();
            //            da.Fill(dt);
            //            conn.Close();
            //            if (dt.Rows.Count > 0)
            //            {
            //                frmKevinMessage KM = new frmKevinMessage(0);
            //                KM.ShowDialog();
            //            }
            //            //otherwise skip
            //        }
            //    }
            //}
        }

        private void MenuMain_Shown(object sender, EventArgs e)
        {
            this.SuspendLayout();
            string sql = "";
            fillgrid();
            fillPunch();
            fillPaint();
            dgPunch.ClearSelection();
            dgPaint.ClearSelection();
            //btnRefresh.PerformClick();

            //currentAvailable();
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                sql = "select coalesce(press1UserID,0) FROM dbo.press_users ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    int getData = Convert.ToInt32(cmd.ExecuteScalar());
                    if (getData > 0)
                        lblPressPrompt.Visible = false;
                    else
                        lblPressPrompt.Visible = true;
                    conn.Close();
                }
            }
            if (login.userFullName == "Other Staff")
                btnOverTime.Visible = true;

            //check absents here 
            add_absents();

            dgCleaning.ClearSelection();
            dgvHSManagement.ClearSelection();

            this.ResumeLayout();

        }




        private void Listener()
        {


        }



        private void kevinNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string person = login.userFullName;
            person = person.Substring(0, person.IndexOf(" "));
            if (person == "Kevin")
            {
                frmKevinMessage frm = new frmKevinMessage(-1);
                frm.ShowDialog();
            }
            else if (person == "Other")
                return;
            else
            {
                string sql = "SELECT top 1  COALESCE(" + person + ",0) FROM dbo.kevinMessage where message_date = cast(getdate() as date) and " + person + " is null order by id desc";
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
                            frmKevinMessage frm = new frmKevinMessage(0);
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("There are no new messages currently.", "Please wait for any new messages");
                        }
                    }
                }
            }
        }

        private void absentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAbenstHolidayStaffDept frm = new frmAbenstHolidayStaffDept();
            frm.ShowDialog();

            //select between two dates and  view all absences/holidays
            
        }

        private void dgBend_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //need to work out the hours here to pass over
            string dropped_gained_hours = "";
            double hours = Convert.ToDouble(Convert.ToDecimal(dgBend.Rows[e.RowIndex].Cells[1].Value) + Convert.ToDecimal((Convert.ToDouble(dgBend.Rows[e.RowIndex].Cells[5].Value) * 0.8)));
            string staff_name = Convert.ToString(dgBend.Rows[e.RowIndex].Cells[0].Value);
            string start_date = "";
            double final_hours = 0;
            int staff_id = 0;
            staff_id = staff_name.IndexOf(" ", staff_name.IndexOf(" ") + 1); //staff id is being used as aa temp int var here
            staff_name = staff_name.Substring(0, staff_id);

            string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + staff_name + "'";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());


                sql = "SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] FROM dbo.door_part_completion_log WHERE staff_id = " +
                    staff_id.ToString() + " AND CAST(part_complete_date as DATE) = '" + dteDateSelection.Value.ToString("yyyyMMdd") + "' AND (part_status = 'Complete' or part_status = 'partial') AND op = 'Bending'  GROUP BY staff_id),0)";
                double worked = 0;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    worked = Convert.ToDouble(cmd.ExecuteScalar());

                final_hours = Math.Round(hours - worked,2);

                if (final_hours < 0)
                {
                    final_hours = final_hours * -1;
                    dropped_gained_hours = "Gained - " + final_hours;
                }
                else
                    dropped_gained_hours = "Dropped - " + final_hours;
                conn.Close();
            }

            dgBend.ClearSelection();
            frmChronological frm = new frmChronological(Convert.ToString(dgBend.Rows[e.RowIndex].Cells[0].Value), "Bending", dteDateSelection.Value, dropped_gained_hours);
            frm.ShowDialog();
        }

        private void ryucxd_Click(object sender, EventArgs e)
        {
            //frmSubDeptMultiple frm = new frmSubDeptMultiple();
            //frm.Show();
        }

        private void btnPress_Click(object sender, EventArgs e)
        {
            frmBendingPress frm = new frmBendingPress();
            frm.ShowDialog();
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                string sql = "select coalesce(press1UserID,0) FROM dbo.press_users ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    int getData = Convert.ToInt32(cmd.ExecuteScalar());
                    if (getData > 0)
                        lblPressPrompt.Visible = false;
                    else
                        lblPressPrompt.Visible = true;
                    conn.Close();
                }
            }
        }

        private void dgNotPlacementSL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgNotPlacementSL.Rows[e.RowIndex].Cells[1].Value.ToString().Contains("HOLIDAY"))
            {
                //if its a holiday show the form that displays when it was created
                //grab user ud
                int id = 0;
                string sql = "SELECT id FROM dbo.[user] WHERE forename + ' ' + surname = '" + dgNotPlacementSL.Rows[e.RowIndex].Cells[0].Value + "'";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }

                frmHolidayCreated frm = new frmHolidayCreated(id, Convert.ToDateTime(dteDateSelection.Text), dgNotPlacementSL.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.ShowDialog();
            }
        }

        private void departmentManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartmentManagement frm = new frmDepartmentManagement();
            frm.ShowDialog();
        }

        private void lOADWEEKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime startDate = dteDateSelection.Value;
            DateTime _tempDate = startDate;
            startDate = _tempDate;
            while (_tempDate.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                _tempDate = _tempDate.AddDays(-1);
            startDate = _tempDate;

            DialogResult result = MessageBox.Show("Are you sure you want to load defaults for this week?" + Environment.NewLine + "This will wipe ALL PLACEMENTS for this week. " + Environment.NewLine + " THIS ACTION CANNOT BE UNDONE.", "LOAD WEEK", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                for (int i = 0; i < 5; i++)
                {
                    //MessageBox.Show(startDate.DayOfWeek.ToString());
                    SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
                    using (SqlCommand cmd = new SqlCommand("usp_power_planner_load_defaults", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@placementDate", SqlDbType.Date).Value = startDate.ToString("yyyy-MM-dd");

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    startDate = startDate.AddDays(1);
                }
                fillgrid();
            }

        }

        private void paintingDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPaintingDetails frm = new frmPaintingDetails(dteDateSelection.Value);
            frm.ShowDialog();
        }

        private void oVERTIMESHEETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOvertimeSelection frm = new frmOvertimeSelection();
            frm.ShowDialog();
        }

        private void btnOverTime_Click(object sender, EventArgs e)
        {
            frmTim frm = new frmTim(dteDateSelection.Value);
            frm.ShowDialog();
        }

        private void weeklyPlacementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWeeklyDepartmentPlacements frm = new frmWeeklyDepartmentPlacements();
            frm.ShowDialog();
        }

        private void txtStockHours_Click(object sender, EventArgs e)
        {
            frmStockParts frm = new frmStockParts(dteDateSelection.Value);
            frm.ShowDialog();
        }

        private void departmentActivityTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartmentActivityTracker frm = new frmDepartmentActivityTracker();
            frm.ShowDialog();
        }
        private void add_absents()
        {
            string sql = "";
            foreach (DataGridViewRow row in dgNotPlaced.Rows)
            {
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    conn.Open();
                    if (row.Cells[1].Value.ToString() == "ABSENT" || row.Cells[1].Value.ToString() == "ABSENT TAKEN HOLIDAY")
                    {
                        //find this users default placement and put him in that grid
                        sql = "Select default_in_department from [user_info].dbo.[user] " +
                            "where forename + ' ' + surname = '" + row.Cells[0].Value.ToString() + "'";
                        string default_dept = "";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            default_dept = dt.Rows[0][0].ToString();
                        }
                        if (default_dept == "Punching")
                        {
                            //copy the punching and then add my row and copy back

                            DataTable dt = (DataTable)(dgPunch.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgPunch.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgPunch.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Bending")
                        {
                            //copy the bending and then add my row and copy back

                            DataTable dt = (DataTable)(dgBend.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgBend.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgBend.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Welding")
                        {
                            //copy the welding and then add my row and copy back

                            DataTable dt = (DataTable)(dgWeld.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgWeld.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgWeld.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Dressing" || default_dept == "Buffing")
                        {
                            //copy the dressing and then add my row and copy back

                            DataTable dt = (DataTable)(dgBuff.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgBuff.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgBuff.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Painting")
                        {
                            //copy the painting and then add my row and copy back

                            DataTable dt = (DataTable)(dgPaint.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgPaint.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgPaint.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Packing")
                        {
                            //copy the packin and then add my row and copy back

                            DataTable dt = (DataTable)(dgPack.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgPack.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgPack.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Laser")
                        {
                            //copy the welding and then add my row and copy back

                            DataTable dt = (DataTable)(dgLaser.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgLaser.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgLaser.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "HS" || default_dept == "Management")
                        {
                            //copy the welding and then add my row and copy back

                            DataTable dt = (DataTable)(dgvHSManagement.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgvHSManagement.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgvHSManagement.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (default_dept == "Cleaning")
                        {
                            //copy the welding and then add my row and copy back

                            DataTable dt = (DataTable)(dgCleaning.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgCleaning.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgCleaning.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                    }
                    conn.Close();
                }
            }

            foreach (DataGridViewRow row in dgNotPlacementSL.Rows)
            {
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    conn.Open();
                    if (row.Cells[1].Value.ToString() == "ABSENT" || row.Cells[1].Value.ToString() == "ABSENT TAKEN HOLIDAY")
                    {
                        //find this users default placement and put him in that grid
                        sql = "Select default_in_department from [user_info].dbo.[user] " +
                            "where forename + ' ' + surname = '" + row.Cells[0].Value.ToString() + "'";
                        string default_dept = "";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            default_dept = dt.Rows[0][0].ToString();
                        }
                        if (default_dept == "Slimline")
                        {
                            //copy the welding and then add my row and copy back

                            DataTable dt = (DataTable)(dgSlimline.DataSource);

                            DataRow dataRow;
                            dataRow = dt.NewRow();
                            dataRow[0] = row.Cells[0].Value.ToString() + Environment.NewLine + " ABSENT";
                            // dataRow[2] = "ABSENT";
                            dt.Rows.Add(dataRow);

                            dgSlimline.DataSource = dt;

                            foreach (DataGridViewRow dgvRow in dgSlimline.Rows)
                                if (dgvRow.Cells[0].Value.ToString().Contains("ABSENT"))
                                    dgvRow.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                    }
                    conn.Close();
                }
            }
        }

        private void remove_absents()
        {

            //remove absents from the dgvs
            if ("Punch" == "Punch")
            {
                DataTable dt = (DataTable)(dgPunch.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgPunch.DataSource = dt;
            }
            if ("Bend" == "Bend")
            {
                DataTable dt = (DataTable)(dgBend.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgBend.DataSource = dt;
            }
            if ("weld" == "weld")
            {
                DataTable dt = (DataTable)(dgWeld.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgWeld.DataSource = dt;
            }
            if ("Buff" == "Buff")
            {
                DataTable dt = (DataTable)(dgBuff.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgBuff.DataSource = dt;
            }
            if ("Paint" == "Paint")
            {
                DataTable dt = (DataTable)(dgPaint.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgPaint.DataSource = dt;
            }
            if ("Pack" == "Pack")
            {
                DataTable dt = (DataTable)(dgPack.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgPack.DataSource = dt;
            }

            if ("Slimline" == "Slimline")
            {
                DataTable dt = (DataTable)(dgSlimline.DataSource);

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dt.Rows[i][0].ToString().Contains("ABSENT"))
                    {
                        dt.Rows[i].Delete();
                        count--;
                        i--;
                    }
                    //MessageBox.Show(dt.Rows[i][0].ToString());
                }

                dgSlimline.DataSource = dt;
            }
        }

        private void lastCalendarUser_Click(object sender, EventArgs e)
        {
            string sql = "select forename + ' ' + surname FROM last_calendar_user l " +
                         "left join [user_info].dbo.[user] u on l.staff_id = u.id";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string calendarUser = cmd.ExecuteScalar().ToString();

                    frmLastCalendarUser frm = new frmLastCalendarUser(calendarUser);
                    frm.ShowDialog();
                }

                conn.Close();
            }
        }

        private void btnWeldingDoorTypes_Click(object sender, EventArgs e)
        {
            frmWeldDoorType frm = new frmWeldDoorType();
            frm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            //prompt for a passcode here...

            frmWarningPasscode frm = new frmWarningPasscode();
            frm.ShowDialog();
        }

        private void btnBuffingDiscs_Click(object sender, EventArgs e)
        {
            frmBuffingDiscs frm = new frmBuffingDiscs(dteDateSelection.Value);
            frm.ShowDialog();
        }

        private void btnShowIntervals_Click(object sender, EventArgs e)
        {
            frmPowerPlanStaffView frm = new frmPowerPlanStaffView(dteDateSelection.Value);
            frm.ShowDialog();
        }
    }
}
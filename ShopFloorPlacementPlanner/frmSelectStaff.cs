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
using Microsoft.VisualBasic;

namespace ShopFloorPlacementPlanner
{
    public partial class frmSelectStaff : Form

    {
        public string _department { get; set; }
        public DateTime _selectedDate { get; set; }
        public int skipPassword { get; set; }
        public double _standardHours { get; set; }

        public frmSelectStaff(string department, DateTime selectedDate)
        {

            InitializeComponent();
            skipPassword = 0;
            dgSelected.CellClick += dataGridViewSoftware_CellClick;
            _department = department;
            _selectedDate = selectedDate;

            ensureDateTableEntry();
            //getStandardHours();
            fillGrid();


            checkExistingSelections();
            getOvertime();
            getAD();




            this.Text = "Select Staff: " + _department;
            this.lblMessage.Text = "Staff selection for " + _department + " Department ";
            this.lblMessage2.Text = _selectedDate.ToShortDateString();

        }

        private void ensureDateTableEntry()
        {

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmdDate = new SqlCommand("usp_update_planner_clear", conn);
            cmdDate.CommandType = CommandType.StoredProcedure;
            cmdDate.Parameters.AddWithValue("@plannerDate", SqlDbType.Date).Value = _selectedDate;
            cmdDate.Parameters.AddWithValue("@department", SqlDbType.NVarChar).Value = _department;
            cmdDate.ExecuteNonQuery();
        }

        private void getOvertime()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            string sql = "";


            switch (_department)
            {
                case "Slimline":
                    sql = "SELECT slimline_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "SlimlineDispatch":
                    sql = "SELECT slimlineDispatch_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "SlimlineStores":
                    sql = "SELECT slimlineStores_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Laser":
                    sql = "SELECT laser_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Punching":
                    sql = "SELECT punching_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Bending":
                    sql = "SELECT bending_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Welding":
                    sql = "SELECT welding_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Dressing":
                    sql = "SELECT buffing_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Painting":
                    sql = "SELECT painting_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Packing":
                    sql = "SELECT packing_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Stores":
                    sql = "SELECT stores_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Dispatch":
                    sql = "SELECT dispatch_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "toolroom":
                    sql = "SELECT toolroom_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Cleaning":
                    sql = "SELECT cleaning_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Management":
                    sql = "SELECT management_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "HS":
                    sql = "SELECT hs_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;

            }





            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {

                conn.Open();
                Overtime o = new Overtime();
                o.getDateID(_selectedDate);


                cmd.Parameters.AddWithValue("@dateID", o._dateID);

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtOvertime.Text = rdr["FieldName"].ToString();
                }
                else
                {
                    txtOvertime.Text = 0.ToString();
                }
            }


        }

        private void getAD()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            string sql = "";


            switch (_department)
            {
                case "Slimline":
                    sql = "SELECT slimline_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "SlimlineDispatch":
                    sql = "SELECT slimlineDispatch_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "SlimlineStores":
                    sql = "SELECT slimlineDispatch_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Laser":
                    sql = "SELECT laser_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Punching":
                    sql = "SELECT punching_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Bending":
                    sql = "SELECT bending_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Welding":
                    sql = "SELECT welding_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Dressing":
                    sql = "SELECT buffing_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Painting":
                    sql = "SELECT painting_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Packing":
                    sql = "SELECT packing_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Stores":
                    sql = "SELECT stores_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Dispatch":
                    sql = "SELECT dispatch_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "toolroom":
                    sql = "SELECT toolroom_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Cleaning":
                    sql = "SELECT cleaning_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;
                case "Management":
                    sql = "SELECT management_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;

                case "HS":
                    sql = "SELECT hs_AD as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
                    break;


            }





            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {

                conn.Open();
                AdditionsDeductions ad = new AdditionsDeductions();
                ad.getDateID(_selectedDate);


                cmd.Parameters.AddWithValue("@dateID", ad._dateID);

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtAD.Text = rdr["FieldName"].ToString();
                }
                else
                {
                    txtAD.Text = 0.ToString();
                }
            }










        }

        private void frmSelectStaff_Load(object sender, EventArgs e)
        {

        }

        private void paintGrid()
        {
            int placementID = 0;


            foreach (DataGridViewRow row in dgSelected.Rows)
            {
                if (row.Cells[3].Value.ToString() == "Shift")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }

            foreach (DataGridViewRow row in dgSelected.Rows)
            {
                if (row.Cells[3].Value.ToString() == "Manual")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                }
            }

            foreach (DataGridViewRow row in dgSelected.Rows)
            {
                if (row.Cells[3].Value.ToString() == "Half Day")
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                }
            }


            foreach (DataGridViewRow row in dgSelected.Rows)
            {
                placementID = Convert.ToInt32(row.Cells[0].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }




            dgSelected.ClearSelection();
            dgSelected.DefaultCellStyle.SelectionBackColor = dgSelected.DefaultCellStyle.BackColor;
            dgSelected.DefaultCellStyle.SelectionForeColor = dgSelected.DefaultCellStyle.ForeColor;

        }

        private void dataGridViewSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int selectedrowindex = dgSelected.SelectedCells[0].RowIndex;

            DataGridViewRow selectedRow = dgSelected.Rows[selectedrowindex];

            int placementID = Convert.ToInt32(selectedRow.Cells["PlacementID"].Value);
            int staffID = Convert.ToInt32(selectedRow.Cells["Staff Id"].Value);
            //for my department swap - ryucxd 27/11/19
            string PT = Convert.ToString(selectedRow.Cells["placement type"].Value);
            double hour = Convert.ToDouble(selectedRow.Cells["Hours"].Value);

            //MessageBox.Show(_selectedDate.ToString());
            getStandardHours(staffID);


            //REMOVE BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Remove"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
                // the below code is only commented out because it would remove the user and we cant have that ^-^

                using (SqlCommand cmd = new SqlCommand("DELETE  FROM DBO.power_plan_staff where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("@placementID", placementID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();
                    department_changed dc = new department_changed();
                    dc.setDepartment(_department);
                }

                //if this user is in painting then this needs to be removed from dbo.power_plan_paint_sub_dept_test_temp_2
                if (_department == "Painting")
                {
                    using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.power_plan_paint_sub_dept_test_temp_2 WHERE placement_ID = " + placementID, connection))
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }


                //ask if the user wants to move this person to another location ---
                DialogResult result = MessageBox.Show("Would you like to move this user to another department?", "Move user?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    //find out where they are heading
                    //form with combobox maybe?
                    int isSlimline = 0;
                    if (_department.Contains("Slimline") == true)
                        isSlimline = -1;
                    frmMoveDept md = new frmMoveDept(staffID, _selectedDate, PT, hour,isSlimline);
                    md.ShowDialog();
                }
            }


            //MANUAL BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Manual"].Index)
            {

                if (_department == "Slimline")
                {
                    if (skipPassword != -1)
                    {
                        string passcode = "design";

                        string input = Interaction.InputBox("ENTER THE PASSWORD", "PASSWORD");
                        if (input == passcode)
                        {
                            skipPassword = -1;

                        }
                        else
                        {
                            MessageBox.Show("Wrong password!", "!!", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
                frmManualHours mh = new frmManualHours();
                mh.ShowDialog();





                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.power_plan_staff set placement_type = 'Manual' , hours = @hours where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("placementID", placementID);
                    cmd.Parameters.AddWithValue("@hours", mh._manualHours);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();

                    //dept change
                    department_changed dc = new department_changed();
                    dc.setDepartment(_department);

                }
            }




            //FULL BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Full"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.power_plan_staff set placement_type = 'Full Day' , hours = @hours where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("placementID", placementID);
                    cmd.Parameters.AddWithValue("@hours", _standardHours);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();
                    department_changed dc = new department_changed();
                    dc.setDepartment(_department);
                }
            }

            //shift BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Shift"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);


                if (_department == "Slimline")
                {
                    if (skipPassword != -1)
                    {
                        string passcode = "design";

                        string input = Interaction.InputBox("ENTER THE PASSWORD", "PASSWORD");
                        if (input == passcode)
                        {
                            skipPassword = -1;
                        }
                        else
                        {
                            MessageBox.Show("Wrong password!", "!!", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }


                int index = dgSelected.Columns["Full Name"].Index;

                Staff s = new Staff(dgSelected.Rows[e.RowIndex].Cells[index].Value.ToString());

                //prompt for more days

                DialogResult weekly = MessageBox.Show("Would you like to assign '" + s._fullname + "' more days in " + _department + " this week?", "Weekly Placement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (weekly == DialogResult.Yes)
                {
                    string subDept = "";
                    //im pretty sure at this point the entry has been made so from here we should start adding placements for the sub-dept IF it is painting
                    if (_department == "Painting")
                    {
                        //quickly grab the max placement type
                        int MAXplacementID = 0;
                        using (SqlConnection conn2 = new SqlConnection(connectionStrings.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", conn))
                            {
                                conn2.Open();
                                MAXplacementID = Convert.ToInt32(cmd.ExecuteScalar());
                                conn2.Close();
                            }
                        }
                        //now prompt the user to select which area they want the user in
                        frmSubDeptMultiple frmSDM = new frmSubDeptMultiple();
                        frmSDM.ShowDialog();
                        subDept = frmSDM.location;
                        //SubDeptClass add = new SubDeptClass();
                        //add.checkPlacement(MAXplacementID);
                        //add.add_placement(MAXplacementID, subDept);
                    }
                    //run procedure to populate all the dates for this week --
                    dateInsert di = new dateInsert();
                    di.check_date(_selectedDate);
                    if (subDept.Length < 1)
                        subDept = "ERROR";
                    //open form
                    frmWeeklyInsert frm = new frmWeeklyInsert(s._staffID, s._fullname, _selectedDate, _department, subDept);
                    frm.ShowDialog();
                    department_changed dc = new department_changed();
                    dc.setDepartment(_department);

                }
                else
                {

                    frmShiftHours sh = new frmShiftHours(staffID, _selectedDate, _department);
                    sh.ShowDialog();


                    using (SqlCommand cmd = new SqlCommand("UPDATE dbo.power_plan_staff set placement_type = 'Shift' , hours = @hours where ID = @placementID", conn))
                    {
                        cmd.Parameters.AddWithValue("placementID", placementID);
                        cmd.Parameters.AddWithValue("@hours", sh._shiftHours);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        checkExistingSelections();
                        department_changed dc = new department_changed();
                        dc.setDepartment(_department);
                    }
                }
            }


            //Half BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Half"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.power_plan_staff set placement_type = 'Half Day' , hours = @hours where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("placementID", placementID);
                    cmd.Parameters.AddWithValue("@hours", _standardHours / 2);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();
                    department_changed dc = new department_changed();
                    dc.setDepartment(_department);
                }
            }

            //Note BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Note"].Index)
            {
                PlacementNote pn = new PlacementNote(placementID);
                pn.ShowDialog();
                checkExistingSelections();
                department_changed dc = new department_changed();
                dc.setDepartment(_department);
            }

            //Sub Dept BUTTON
            if (_department == "Painting")
            {
                if (e.ColumnIndex == dgSelected.Columns["Sub Dept"].Index)
                {

                    //MessageBox.Show("Sub Dept");
                    frmSubDept sd = new frmSubDept(placementID);
                    sd.ShowDialog();
                    //PlacementNote pn = new PlacementNote(placementID);
                    //pn.ShowDialog();
                    //checkExistingSelections();
                    department_changed dc = new department_changed();
                    dc.setDepartment(_department);
                }
            }


        }

        private void checkExistingSelections()
        {
            //CHECKS TO SEE IF THERE ARE EXISTING SELECTIONS, IF THERE ARE THEN THE DATAGRID BINDING IS BLANK

            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.view_planner_punch_staff where date_plan = @selectedDate and department = @dept", conn))
            {

                cmd.Parameters.AddWithValue("@selectedDate", _selectedDate);
                cmd.Parameters.AddWithValue("@dept", _department);

                conn.Open();


                SqlDataReader rdr = cmd.ExecuteReader();


                // dgSelected.DataSource = null;
                dgSelected.Columns.Clear();
                populateExisting();


                DataGridViewButtonColumn fullDayButton = new DataGridViewButtonColumn();
                fullDayButton.Name = "Full";
                fullDayButton.Text = "Full";
                fullDayButton.UseColumnTextForButtonValue = true;
                int columnIndex = 5;
                if (dgSelected.Columns["full_column"] == null)
                {
                    dgSelected.Columns.Insert(columnIndex, fullDayButton);
                }


                DataGridViewButtonColumn halfDayButton = new DataGridViewButtonColumn();
                halfDayButton.Name = "Half";
                halfDayButton.Text = "Half";
                halfDayButton.UseColumnTextForButtonValue = true;
                columnIndex = 6;
                if (dgSelected.Columns["half_column"] == null)
                {
                    dgSelected.Columns.Insert(columnIndex, halfDayButton);
                }

                DataGridViewButtonColumn shiftButton = new DataGridViewButtonColumn();
                shiftButton.Name = "Shift";
                shiftButton.Text = "Shift";
                shiftButton.UseColumnTextForButtonValue = true;
                columnIndex = 7;
                if (dgSelected.Columns["shift_column"] == null)
                {
                    dgSelected.Columns.Insert(columnIndex, shiftButton);
                }


                DataGridViewButtonColumn manualButton = new DataGridViewButtonColumn();
                manualButton.Name = "Manual";
                manualButton.Text = "Manual";
                manualButton.UseColumnTextForButtonValue = true;
                columnIndex = 8;
                if (dgSelected.Columns["manual_column"] == null)
                {
                    dgSelected.Columns.Insert(columnIndex, manualButton);
                }


                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                deleteButton.Name = "Remove";
                deleteButton.Text = "Remove";
                deleteButton.UseColumnTextForButtonValue = true;
                columnIndex = 9;
                if (dgSelected.Columns["uninstall_column"] == null)
                {
                    dgSelected.Columns.Insert(columnIndex, deleteButton);
                }
                DataGridViewButtonColumn noteButton = new DataGridViewButtonColumn();
                noteButton.Name = "Note";
                noteButton.Text = "Note";
                noteButton.UseColumnTextForButtonValue = true;
                columnIndex = 10;
                if (dgSelected.Columns["note_column"] == null)
                {
                    dgSelected.Columns.Insert(columnIndex, noteButton);
                }

                //sub dept button
                DataGridViewButtonColumn subDeptButton = new DataGridViewButtonColumn();
                subDeptButton.Name = "Sub Dept";
                subDeptButton.Text = "Sub Dept";
                subDeptButton.UseColumnTextForButtonValue = true;
                columnIndex = 11;
                if (dgSelected.Columns["Sub Dept_column"] == null && _department == "Painting")
                {
                    dgSelected.Columns.Insert(columnIndex, subDeptButton);
                }





                conn.Close();



            }
        }
        private void populateExisting()
        {


            //Adds the existing selections back into the selection window
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString); //this is where the DGV is selected 


            using (SqlCommand cmd = new SqlCommand("SELECT PlacementID,staff_id as 'Staff ID',[Staff Name] as 'Full Name',Placement as 'Placement Type',hours as 'Hours' from dbo.view_planner_punch_staff where date_plan = @selectedDate and department = @dept order by placementID ", conn))
            {
                cmd.Parameters.AddWithValue("@selectedDate", _selectedDate); 
                    cmd.Parameters.AddWithValue("@dept", _department);

                conn.Open();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgSelected.DataSource = dt;
                conn.Close();

            }
        }

        private void fillGrid()
        {
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionStringUser);
            string sqlDepartment;

            
          
            //STAFF DEPARTMENTS WORK DIFFERENTLY IF SLIMLINE
            if (_department == "Slimline" || _department == "SlimlineStores" || _department == "SlimlineDispatch")
            {
                sqlDepartment = "select id, forename + ' ' + surname as fullname from dbo.[user] where slimline_staff = -1 and ShopFloor = -1 and [current] = 1 order by fullname";
            }
            else
            {
                sqlDepartment = "Select id, forename + ' ' + surname as fullname from dbo.[user] where " +
                "([actual_department] = @department or [allocation_dept_2] = @department or [allocation_dept_3] = @department or " +
                "[allocation_dept_4] = @department or [allocation_dept_5] = @department or [allocation_dept_6] = @department) and [current]=1 order by fullname";
            }



            using (SqlCommand cmd = new SqlCommand(sqlDepartment, conn))
            {
                cmd.Parameters.AddWithValue("@department", _department);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstStaff.Items.Add(reader["fullname"].ToString());
                    }
                    reader.Close();
                }
                conn.Close();
            }
        }

        private void getStandardHours(int staffID)
        {
            string dayOfWeek;

            dayOfWeek = _selectedDate.DayOfWeek.ToString();

            //Differing standard hours for certain users


            if (dayOfWeek == "Friday")
            {
                switch (staffID)
                {
                    case 63:
                        _standardHours = 3.6;
                        break;
                    case 68:
                        _standardHours = 3.6;
                        break;
                    case 165:
                        _standardHours = 11.2;
                        break;
                    default:
                        _standardHours = 5.6;
                        break;

                }

            }
            else if (dayOfWeek == "Saturday" || dayOfWeek == "Sunday")
            {
                _standardHours = 0.0001;
            }
            else
            {
                switch (staffID)
                {
                    case 63:
                        _standardHours = 4.4;
                        break;
                    case 68:
                        _standardHours = 4.4;
                        break;
                    case 165:
                        _standardHours = 12.8;
                        break;
                    default:
                        _standardHours = 6.4;
                        break;

                }
            }
        }

        private void lstStaff_DoubleClick(object sender, EventArgs e)
        {
            double remainingHours;
            //string remainingPlacementType;

            Staff s = new Staff(lstStaff.SelectedItem.ToString());

            getStandardHours(s._staffID);

            Placement p = new Placement(_selectedDate, s._staffID, _department, "Full Day", _standardHours);

            p.notPresent();
            p.checkPlacement();

            p.getWeldTeamUserID();
            p.checkWeldTeamAbsence();


            //CHECKS IF STAFF MEMBERS IN WELD TEAM 2 ARE CURRENTLY ON HOLIDAY OR ABSENT
            if (s._staffID == 165)
            {
                if (p._weldTeamMembersPresent == 1)
                {
                    p._hours = _standardHours / 2;
                    MessageBox.Show("One member of this weld team is either absent or on holiday. Adding half the hours", "Half Placement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (p._weldTeamMembersPresent == 2)
                {
                    p._hours = _standardHours * 0;
                    MessageBox.Show("Both members of this team are either absent or on holiday", "Zero Placement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }


            if (p._notPresentType == 5 || p._notPresentType == 2)
            {
                MessageBox.Show("This staff member is either absent today or has a full day holiday!", "Cannot Place", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (p._notPresentType == 3)
            {
                MessageBox.Show("This staff member has half day holiday so can only be placed for half day", "Half Day Placement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                remainingHours = _standardHours / 2;
                Placement p3 = new Placement(_selectedDate, s._staffID, _department, "Half Day", remainingHours);
                p3.addPlacment();
            }
            else
            {
                if (p._alreadyPlaced == true)
                {
                    if (p._existingPlacementHours == _standardHours)
                    {
                        DialogResult noHourPlacement = MessageBox.Show("Staff member already has a full day placement for this day, would you like to place them for 0 hours?", "Already Placed", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (noHourPlacement == DialogResult.Yes)
                        {
                            Placement p69 = new Placement(_selectedDate, s._staffID, _department, "Manual", 0);
                            p69.addPlacment();
                        }
                    }
                    else
                    {
                        if (p._existingPlacementHours != _standardHours && p._existingPlacementHours > 0)
                        {
                            remainingHours = _standardHours - p._existingPlacementHours;
                            MessageBox.Show("Staff member already placed for " + p._existingPlacementHours.ToString(), "Staff member part placed", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            Placement p2 = new Placement(_selectedDate, s._staffID, _department, p._existingPlacementType, remainingHours);
                            p2.addPlacment();
                        }
                        else
                        {

                            DialogResult weekly = MessageBox.Show("Would you like to assign '" + s._fullname + "' more days in " + _department + " this week?", "Weekly Placement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (weekly == DialogResult.Yes)
                            {
                                string subDept = "";
                                //im pretty sure at this point the entry has been made so from here we should start adding placements for the sub-dept IF it is painting
                                if (_department == "Painting")
                                {
                                    //quickly grab the max placement type
                                    int MAXplacementID = 0;
                                    using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", conn))
                                        {
                                            conn.Open();
                                            MAXplacementID = Convert.ToInt32(cmd.ExecuteScalar());
                                            conn.Close();
                                        }
                                    }
                                    //now prompt the user to select which area they want the user in
                                    frmSubDeptMultiple frmSDM = new frmSubDeptMultiple();
                                    frmSDM.ShowDialog();
                                    subDept = frmSDM.location;
                                    //SubDeptClass add = new SubDeptClass();
                                    //add.checkPlacement(MAXplacementID);
                                    //add.add_placement(MAXplacementID, subDept);
                                }
                                //run procedure to populate all the dates for this week --
                                dateInsert di = new dateInsert();
                                di.check_date(_selectedDate);
                                if (subDept.Length < 1)
                                    subDept = "ERROR";
                                //open form
                                frmWeeklyInsert frm = new frmWeeklyInsert(s._staffID, s._fullname, _selectedDate, _department, subDept);
                                frm.ShowDialog();
                            }
                            else
                            {

                                p.addPlacment(); // add placement is here so there is no way it should be the reason for changing the placements  on the next form close
                                                 //im pretty sure at this point the entry has been made so from here we should start adding placements for the sub-dept IF it is painting
                                if (_department == "Painting")
                                {
                                    //quickly grab the max placement type
                                    int MAXplacementID = 0;
                                    using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", conn))
                                        {
                                            conn.Open();
                                            MAXplacementID = Convert.ToInt32(cmd.ExecuteScalar());
                                            conn.Close();
                                        }
                                    }
                                    //now prompt the user to select which area they want the user in
                                    frmSubDeptMultiple frmSDM = new frmSubDeptMultiple();
                                    frmSDM.ShowDialog();
                                    string subDept = frmSDM.location;
                                    SubDeptClass add = new SubDeptClass();
                                    add.checkPlacement(MAXplacementID);
                                    add.add_placement(MAXplacementID, subDept);
                                }
                            }




                        }
                    }
                }
            }





            department_changed dc = new department_changed();
            dc.setDepartment(_department);
            checkExistingSelections();

        }

        private void lstStaff_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgSelected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            //CLEARS ALL EXISTING SELECTION FROM THE TABLE FOR THIS DEPARTMENT
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();
            SqlCommand cmdDate = new SqlCommand("usp_update_planner_clear", conn);
            cmdDate.CommandType = CommandType.StoredProcedure;
            cmdDate.Parameters.AddWithValue("@plannerDate", SqlDbType.Date).Value = _selectedDate;
            cmdDate.Parameters.AddWithValue("@department", SqlDbType.NVarChar).Value = _department;
            cmdDate.ExecuteNonQuery();

            foreach (DataGridViewRow row in dgSelected.Rows)
            {

                SqlCommand cmdStaff = new SqlCommand("usp_update_planner", conn);
                cmdStaff.CommandType = CommandType.StoredProcedure;
                cmdStaff.Parameters.AddWithValue("@plannerDate", SqlDbType.Date).Value = _selectedDate;
                cmdStaff.Parameters.AddWithValue("@staffID", SqlDbType.Int).Value = row.Cells[0].Value;
                cmdStaff.Parameters.AddWithValue("@department", SqlDbType.NVarChar).Value = _department;
                cmdStaff.Parameters.AddWithValue("@placementType", SqlDbType.NVarChar).Value = row.Cells[2].Value;
                cmdStaff.Parameters.AddWithValue("@hours", SqlDbType.Decimal).Value = row.Cells[3].Value;

                cmdStaff.ExecuteNonQuery();

            }

            this.Close();

        }


        private void submitAD()
        {
            double ADAmount;

            try
            {
                ADAmount = Convert.ToDouble(txtAD.Text);
            }
            catch
            {
                ADAmount = 0;
            }



            AdditionsDeductions ad = new AdditionsDeductions();
            ad.updateAD(_selectedDate, _department, ADAmount);

        }

        private void submitOT()
        {
            double overtimeAmount;

            try
            {
                overtimeAmount = Convert.ToDouble(txtOvertime.Text);
            }
            catch
            {
                overtimeAmount = 0;
            }

            Overtime o = new Overtime();

            o.updateOT(_selectedDate, _department, overtimeAmount);
        }


        private void frmSelectStaff_Leave(object sender, EventArgs e)
        {

        }

        private void frmSelectStaff_FormClosed(object sender, FormClosedEventArgs e)
        {
            submitOT();
            submitAD();
        }

        private void btn_overtime_Click(object sender, EventArgs e)
        {
            //open form and pass over current date
            //run procedure to populate all the dates for this week --
            dateInsert di = new dateInsert();
            di.check_date(_selectedDate);

            ////test start
            //frmWeeklyOverTime frm = new frmWeeklyOverTime(_selectedDate, _department);
            //frm.ShowDialog();
            //txtOvertime.Text = frm.totalOvertime.ToString();
            ////test end


            frmWeeklyOverTime frm = new frmWeeklyOverTime(_selectedDate, _department);
            frm.ShowDialog();
            txtOvertime.Text = frm.totalOvertime.ToString();
            //frmWeeklyOT OT = new frmWeeklyOT(_selectedDate, _department);
            //OT.ShowDialog();
            //need to pass the NEW overtime value into the upper right textbox to stop the value over-writing it
            //txtOvertime.Text = OT.overtimeForSD.ToString();
            this.Close();
        }

        private void Btn_additions_Click(object sender, EventArgs e)
        {
            //open form and pass over current date
            //run procedure to populate all the dates for this week --
            dateInsert di = new dateInsert();
            di.check_date(_selectedDate);
            frmWeeklyAD AD = new frmWeeklyAD(_selectedDate, _department);
            AD.ShowDialog();
            //need to pass the NEW overtime value into the upper right textbox to stop the value over-writing it
            txtAD.Text = AD.additionForSD.ToString();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmWeeklyOverTime frm = new frmWeeklyOverTime(_selectedDate, _department);
            frm.ShowDialog();
            txtOvertime.Text = frm.totalOvertime.ToString();
        }

        private void frmSelectStaff_Shown(object sender, EventArgs e)
        {

        }

        private void dgSelected_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int placementID = 0;
            int temp = dgSelected.Columns["Placement Type"].Index;
            int tempID = dgSelected.Columns["PlacementID"].Index;
            for (int i = 0; i < dgSelected.Rows.Count; i++)
            {
                if (dgSelected.Rows[i].Cells[temp].Value.ToString() == "Shift") //same as below but this is with shift instead
                {
                    dgSelected.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }


                if (dgSelected.Rows[i].Cells[temp].Value.ToString() == "Half Day") //because it looks for [half day] it should only pick up actual half days and not 3.2
                {
                    dgSelected.Rows[i].DefaultCellStyle.BackColor = Color.MediumPurple;
                }

                placementID = Convert.ToInt32(dgSelected.Rows[i].Cells[tempID].Value.ToString());
                PlacementNoteClass pnc = new PlacementNoteClass(placementID);
                pnc.getNote();

                if (pnc._hasNote == true)
                {
                    dgSelected.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }

                //if (i == 2)
                //{
                //    dgSelected.Rows[i].DefaultCellStyle.BackColor = Color.MediumPurple; // testing if ithis actually applies a colour to the correct location
                //}
            }
            dgSelected.ClearSelection();
            dgSelected.DefaultCellStyle.SelectionBackColor = dgSelected.DefaultCellStyle.BackColor;
            dgSelected.DefaultCellStyle.SelectionForeColor = dgSelected.DefaultCellStyle.ForeColor;

        }
    }
}

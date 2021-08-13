using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmWeeklyInsert : Form
    {
        public int _staff_id { get; set; }
        public string _staff_fullname { get; set; }
        public DateTime _selectedDate { get; set; }
        public DateTime monday { get; set; }
        public DateTime sunday { get; set; }
        public DateTime passed_date { get; set; }
        public string _dept { get; set; }
        public int skipPassword { get; set; }
        public double _standardHours { get; set; }
        public bool alreadyPlaced { get; set; }
        public string _subDept { get; set; }

        public frmWeeklyInsert(int staff_id, string staff_fullname, DateTime searchDate, string department, string subDept)
        {
            InitializeComponent();
            // add all the variables into new props

            _staff_id = staff_id;
            skipPassword = 0;
            _staff_fullname = staff_fullname;
            _selectedDate = searchDate;
            _dept = department;
            _subDept = subDept;
            //now get date range
            getDates();
            DGV();
            buttons();
        }

        private void buttons()
        {
            // f u l l   shift
            DataGridViewButtonColumn fullDayButton = new DataGridViewButtonColumn();
            fullDayButton.Name = "Full";
            fullDayButton.Text = "Full";
            fullDayButton.UseColumnTextForButtonValue = true;
            int columnIndex = 4;
            if (dataGridView1.Columns["full_column"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, fullDayButton);
            }

            // h a l f   shft
            DataGridViewButtonColumn halfDayButton = new DataGridViewButtonColumn();
            halfDayButton.Name = "Half";
            halfDayButton.Text = "Half";
            halfDayButton.UseColumnTextForButtonValue = true;
            columnIndex = 5;
            if (dataGridView1.Columns["half_column"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, halfDayButton);
            }

            //s h i f t  shift
            DataGridViewButtonColumn shiftButton = new DataGridViewButtonColumn();
            shiftButton.Name = "Shift";
            shiftButton.Text = "Shift";
            shiftButton.UseColumnTextForButtonValue = true;
            columnIndex = 6;
            if (dataGridView1.Columns["shift_column"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, shiftButton);
            }

            // m a n u a l shift
            DataGridViewButtonColumn manualButton = new DataGridViewButtonColumn();
            manualButton.Name = "Manual";
            manualButton.Text = "Manual";
            manualButton.UseColumnTextForButtonValue = true;
            columnIndex = 7;
            if (dataGridView1.Columns["manual_column"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, manualButton);
            }
        }

        private void getDates()
        {
            passed_date = _selectedDate;
            while (_selectedDate.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                _selectedDate = _selectedDate.AddDays(-1);
            monday = _selectedDate;
            //MessageBox.Show("monday = " + monday.ToString());
            //get end of week
            sunday = _selectedDate.AddDays(4);
            // MessageBox.Show("sunday = " + sunday.ToString());
        }

        private void DGV()
        {
            string sql = "";
            using (SqlConnection CONNECT = new SqlConnection(connectionStrings.ConnectionString))
            {
                if (_dept == "Dressing")
                {
                    sql = "Select a.id,CAST(a.date_plan as date)" +   //dressing onLY --
                             " FROM dbo.power_plan_date a " +
                             "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                             "WHERE date_plan >= '" + monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
                }
                else//
                {
                    sql = "Select a.id,CAST(a.date_plan as date)" +
                              " FROM dbo.power_plan_date a " +
                              "LEFT JOIN dbo.power_plan_overtime b on b.date_id = a.id " +
                              "WHERE date_plan >= '" + monday.ToString("yyyyMMdd") + "' AND date_plan <= '" + sunday.ToString("yyyyMMdd") + "' ORDER BY date_plan ASC";
                }
                using (SqlCommand COMMAND = new SqlCommand(sql, CONNECT))
                {
                    CONNECT.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(COMMAND);
                    da.Fill(dt);
                    //add time and placement columns
                    dt.Columns.Add("Placement Type", typeof(System.String));
                    dt.Columns.Add("Time", typeof(System.Double));
                    dataGridView1.DataSource = dt;
                    //here we are going to check if there are any placements for that day(s)

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        sql = "SELECT placement_type,[hours] from dbo.power_plan_staff LEFT JOIN dbo.power_plan_date on power_plan_staff.date_id = power_plan_date.id " +
                                 " WHERE date_plan = '" + Convert.ToDateTime(row.Cells[1].Value).ToString("yyyyMMdd") + "'  AND staff_id = " + _staff_id + " AND department = '" + _dept + "' ORDER BY date_plan ASC ";

                        using (SqlCommand cmd = new SqlCommand(sql, CONNECT))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                row.Cells[2].Value = reader["placement_type"].ToString();
                                row.Cells[3].Value = reader["hours"].ToString();
                            }
                            reader.Close();
                        }
                        //quickly alter colours too
                        if (row.Cells[2].Value.ToString() == "Full Day")
                            row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                        if (row.Cells[2].Value.ToString() == "Half Day")
                            row.DefaultCellStyle.BackColor = Color.MediumPurple;
                        if (row.Cells[2].Value.ToString() == "Shift")
                            row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                        if (row.Cells[2].Value.ToString() == "Manual")
                            row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    }
                    CONNECT.Close();
                }

                //format
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Calibri", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
                }
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle.Font = new Font("Calibri", 13F, FontStyle.Regular, GraphicsUnit.Pixel);
                //------------

                dataGridView1.Columns[1].HeaderText = "Date ";

                //set the CURRENT dates checkbox to true
                DateTime dgv; //date in the DGV
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    //row.Cells[2].Value = CheckState.Unchecked;
                //    dgv = Convert.ToDateTime(dataGridView1.Rows[row.Index].Cells[1].Value);
                //    if (dgv == passed_date)
                //    {                                                 //TOM WANTED THIS REMOVED 23/01/2020
                //        //row.Cells[2].Value = CheckState.Checked;
                //        dataGridView1.Rows.RemoveAt(row.Index);
                //    }

                //}

                dataGridView1.Refresh();
            }
        }

        private void frmWeeklyInsert_Load(object sender, EventArgs e)
        {
            lbl_title.Text = "Select Which days you want " + _staff_fullname + " in " + _dept;
            int columnIndex = 0;
            columnIndex = dataGridView1.Columns["Placement Type"].Index;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //quickly alter colours too
                if (row.Cells[columnIndex].Value.ToString() == "Full Day")
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                if (row.Cells[columnIndex].Value.ToString() == "Half Day")
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
                if (row.Cells[columnIndex].Value.ToString() == "Shift")
                    row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.LightSeaGreen)
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
            //else
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSeaGreen;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //loop through every single colour
            //if its green THEN run through absent-already placed-placement to make sure there are no doubles in this code.
            //declare variables
            DateTime dgvDate;

            double remainingHours;
            int placement_id;
            string sql = "";
            string note = "Placement errors:";
            int validationID = 0;
            //get standard hours
            int _dateID;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                //everything inside here is already within a connection string
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //loop for colour
                    alreadyPlaced = false;
                    placement_id = 0;
                    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor != Color.Empty)
                    { //the correct colour
                      //get variables

                        dgvDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[5].Value);
                        getStandardHours(_staff_id, dgvDate);
                        Placement p = new Placement(dgvDate, _staff_id, _dept, "Full Day", _standardHours); //initate the class

                        p._alreadyPlaced = false;
                        //p.checkPlacement();
                        //time to make my own  checkplacement()

                        sql = "Select id from dbo.power_plan_date where date_plan = '" + dgvDate.ToString("yyyy - MM - dd") + "'";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            // cmd.ExecuteNonQuery();
                            object toNullOrNotToNull = cmd.ExecuteScalar();
                            if (toNullOrNotToNull != null)
                                validationID = Convert.ToInt32(cmd.ExecuteScalar());
                            conn.Close();
                        }
                        //now we have the DATE id we can get into the validation
                        if (validationID != 0)
                        {
                            //if the row is shift then allow for the user to be placed in TWO places (i check for maximum hours in shift anyway so they **probably** wont go over
                            //if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.PaleVioletRed)
                            //    sql = "SELECT id from dbo.power_plan_staff WHERE date_id = " + validationID.ToString() + " AND staff_id = " + _staff_id + " AND department = '" + _dept + "'";
                            //else // t
                            sql = "SELECT id from dbo.power_plan_staff WHERE date_id = " + validationID.ToString() + " AND staff_id = " + _staff_id;
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                conn.Open();
                                object nullification = cmd.ExecuteScalar();
                                if (nullification != null)
                                    alreadyPlaced = true;
                                else
                                    alreadyPlaced = false;
                                conn.Close();
                            }
                        }
                        //get the current DATEID
                        // if they are already placed that day then move them

                        /////////////////////////   REWRITING THIS BIT FOR MULTIPLE PLACEMENTS VVVVVV ///////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (alreadyPlaced == true)
                        {
                            conn.Open();
                            double hoursCurrentlyAssigned = 0;
                            double hoursAssigned = 0;
                            int timeIndex = 0;
                            //before getting the sum hours we need to check if they are places in THAT department and remove it first
                            sql = "DELETE  FROM dbo.power_plan_staff where staff_id = " + _staff_id.ToString() + " AND date_id = " + p._dateID + " AND department = '" + _dept + "'";
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                                cmd.ExecuteNonQuery();
                            //i guess also remove any overtime thats been assigned too?
                            sql = "DELETE  FROM dbo.power_plan_overtime_remake where staff_id = " + _staff_id.ToString() + " AND date_id = " + p._dateID + " AND department = '" + _dept + "'";
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                                cmd.ExecuteNonQuery();

                            sql = "Select sum(hours) FROM dbo.power_plan_staff where staff_id = " + _staff_id.ToString() + " AND date_id = " + p._dateID; //get all the placement ids for this person
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                hoursCurrentlyAssigned = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                            }
                            timeIndex = dataGridView1.Columns["Time"].Index;
                            hoursAssigned = Convert.ToDouble(dataGridView1.Rows[i].Cells[timeIndex].Value.ToString());
                            if (hoursCurrentlyAssigned + hoursAssigned > _standardHours)
                            {
                                //inform them whats the max hours they can add
                                double maxHours = Math.Round(_standardHours - hoursCurrentlyAssigned, 2);
                                MessageBox.Show("Placement for " + dgvDate.ToString("dd/MM/yyyy") + " exceeds the limit. The maximum hours " + _staff_fullname + " can be assigned in " + _dept + " is " + maxHours.ToString() + ".");
                                dataGridView1.Rows[i].Cells[timeIndex].Value = maxHours;
                                if (maxHours == _standardHours / 2)
                                {
                                    dataGridView1.Rows[i].Cells[timeIndex - 1].Value = "Half Day";
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.MediumPurple;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[timeIndex - 1].Value = "Manual";
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSeaGreen;

                                }
                                return;
                            }


                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            ////////////// old code vvvvv///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //get the placement id using date_id and staff_id
                            //this is the same as the above -- if the user is going into shift it needs to look at CURRENT dept and not every dept :}
                            //if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                            //    sql = "Select id FROM dbo.power_plan_staff where staff_id = " + _staff_id.ToString() + " AND date_id = " + p._dateID + " AND department = '" + _dept + "'";
                            //else
                            ////////////////////////////////sql = "Select id FROM dbo.power_plan_staff where staff_id = " + _staff_id.ToString() + " AND date_id = " + p._dateID;
                            //////////////////////////////////MessageBox.Show(sql);
                            ////////////////////////////////using (SqlCommand cmd = new SqlCommand(sql, conn))
                            ////////////////////////////////{
                            ////////////////////////////////    // conn.Open();
                            ////////////////////////////////    cmd.ExecuteNonQuery();
                            ////////////////////////////////    object isItNull = cmd.ExecuteScalar();
                            ////////////////////////////////    if (isItNull != null)
                            ////////////////////////////////        placement_id = (Int32)cmd.ExecuteScalar(); //get placement ID for removing a person from a placement already
                            ////////////////////////////////    else
                            ////////////////////////////////    {
                            ////////////////////////////////        conn.Close();
                            ////////////////////////////////        continue; // pop an error message because there is no date returned
                            ////////////////////////////////    }
                            ////////////////////////////////    conn.Close();
                            ////////////////////////////////    //  MessageBox.Show(test.ToString());
                            ////////////////////////////////}
                            //////////////////////////////////they are already placed so just remove them  so they can be added down the line
                            ////////////////////////////////using (SqlCommand cmd = new SqlCommand("DELETE  FROM DBO.power_plan_staff where date_id = @date_id AND staff_id = @staff_id", conn))
                            ////////////////////////////////{
                            ////////////////////////////////    cmd.Parameters.AddWithValue("@date_id", p._dateID);
                            ////////////////////////////////    cmd.Parameters.AddWithValue("@staff_id", _staff_id);
                            ////////////////////////////////    conn.Open();
                            ////////////////////////////////    cmd.ExecuteNonQuery();
                            ////////////////////////////////    conn.Close();
                            ////////////////////////////////}
                            //////////////////////////////////if its painting also delete it from here (as it will likely already have a placement??
                            ////////////////////////////////if (_dept == "Painting")
                            ////////////////////////////////{
                            ////////////////////////////////    using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.power_plan_paint_sub_dept_test_temp_2 WHERE placement_id = " + placement_id, conn))
                            ////////////////////////////////    {
                            ////////////////////////////////        conn.Open();
                            ////////////////////////////////        cmd.ExecuteNonQuery();
                            ////////////////////////////////        conn.Close();
                            ////////////////////////////////    }
                            ////////////////////////////////}
                            conn.Close();
                        }
                        /////////////////////////   REWRITING THIS BIT FOR MULTIPLE PLACEMENTS VVVVVV ///////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        p.notPresent(); //check for attendance
                        string placement_type = dataGridView1.Rows[i].Cells[6].Value.ToString();
                        double h_o_u_r_s = Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        if (p._notPresentType == 5 || p._notPresentType == 2) // find out what 5 and 2
                        {
                            //add the messagebox note here
                            note = "\nUnable to place on " + dgvDate + ".";
                            note = note.Substring(0, note.Length - 8);
                            continue; // they have full holiday so they cannot be placed   maybe have a running msgbox  - monday 11th cant be placed because of holiday etc
                        }
                        else if (p._notPresentType == 3) // has half a day booked
                        {  //max placement on friday through half day is 5.6 / 2
                           //max placement on a normal day with half day is 6.4 / 2
                            if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "Manual")
                            {
                                //unique case of when the user has manually inputted more hours than the user can have (because of half day)
                                //if its above the max hous then reset it to max hours otherwise carry on as normal
                                remainingHours = _standardHours / 2; // max hours
                                double ManualHours = Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
                                if (ManualHours > remainingHours)
                                {
                                    //reduce it back to half and add note
                                    Placement p3 = new Placement(_selectedDate, _staff_id, _dept, "Manual", remainingHours);
                                    p3.addPlacment();
                                    note = note + "\n Manual Hours reduced because placement has half day on " + dgvDate + "";
                                    note = note.Substring(0, note.Length - 8);
                                }
                                else
                                {
                                    Placement p3 = new Placement(_selectedDate, _staff_id, _dept, "Manual", remainingHours);
                                    p3.addPlacment();
                                }
                                //if i add painting here it should make a difference (either way if its manual it will fall into this else and then run thhis block of code
                                if (_dept == "Painting")
                                {
                                    //get max id here maybe????
                                    int MAXplacementID = 0;
                                    using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", connection))
                                        {
                                            connection.Open();
                                            MAXplacementID = Convert.ToInt32(cmd.ExecuteScalar());
                                            connection.Close();
                                        }
                                    }
                                    SubDeptClass place = new SubDeptClass();
                                    place.checkPlacement(placement_id);
                                    place.add_placement(placement_id, _subDept);
                                }
                            }
                            else //
                            {//should be the same as normal
                                remainingHours = _standardHours / 2;
                                Placement p3 = new Placement(_selectedDate, _staff_id, _dept, "Half Day", remainingHours); // adds them in but its for /half/ the time
                                p3.addPlacment(); // a new instance of adding placement
                                note = note + "\nHalf day placement on " + dgvDate + "";
                                note = note.Substring(0, note.Length - 8);

                                //another iteration for painting to be added
                                if (_dept == "Painting")
                                {
                                    //get max id here maybe????
                                    int MAXplacementID = 0;
                                    using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", connection))
                                        {
                                            connection.Open();
                                            MAXplacementID = Convert.ToInt32(cmd.ExecuteScalar());
                                            connection.Close();
                                        }
                                    }
                                    SubDeptClass place = new SubDeptClass();
                                    place.checkPlacement(placement_id);
                                    place.add_placement(placement_id, _subDept);
                                }
                            }
                        }
                        else
                        {//  they are present and they dont have time off == they also aren't placed in another dept
                         //p.addPlacment();
                         //no obscure data to consider, just plaster it in
                            using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                            {
                                //placement type and hours assign
                                using (SqlCommand cmd = new SqlCommand("insert into dbo.power_plan_staff(date_id, staff_id, department, placement_type, hours) VALUES(@dateID, @staffID, @department, @placementType, @hours)", conn))
                                {
                                    cmd.Parameters.AddWithValue("@dateID", validationID);
                                    cmd.Parameters.AddWithValue("@staffID", _staff_id);
                                    cmd.Parameters.AddWithValue("@department", _dept);
                                    cmd.Parameters.AddWithValue("@placementType", placement_type);
                                    cmd.Parameters.AddWithValue("@hours", h_o_u_r_s);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                            //this one is a bit weird but as far as my above commet says lets do the same and just /plaster/ it in
                            if (_dept == "Painting")
                            {
                                //get max id here maybe????
                                int MAXplacementID = 0;
                                using (SqlConnection connection = new SqlConnection(connectionStrings.ConnectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT MAX(placementID)  from dbo.view_planner_punch_staff", connection))
                                    {
                                        connection.Open();
                                        MAXplacementID = Convert.ToInt32(cmd.ExecuteScalar());
                                        connection.Close();
                                    }
                                }
                                SubDeptClass place = new SubDeptClass();
                                place.checkPlacement(MAXplacementID);
                                place.add_placement(MAXplacementID, _subDept);
                            }
                        }
                    }
                }//end of if back colour = green
            } //end of for loop
            MessageBox.Show("Placements updated!");
            if (note != "Placement errors:")
                MessageBox.Show(note);
            this.Close();
        }

        private void getStandardHours(int staffID, DateTime dgvDate) //call this to get the time for each day of the week
        {
            string dayOfWeek;

            dayOfWeek = dgvDate.DayOfWeek.ToString();

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get standard hours for that day
            getStandardHours(_staff_id, Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value));
            if (e.ColumnIndex == dataGridView1.Columns["Full"].Index)
            {
                //6.40 hours - full Day
                dataGridView1.Rows[e.RowIndex].Cells[7].Value = _standardHours;
                dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Full Day";
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSeaGreen;
            }
            if (e.ColumnIndex == dataGridView1.Columns["Half"].Index)
            {//3.2
                dataGridView1.Rows[e.RowIndex].Cells[7].Value = _standardHours / 2;
                dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Half Day";
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MediumPurple;
            }
            if (e.ColumnIndex == dataGridView1.Columns["Shift"].Index)
            {//this needs to be manual input now
                if (_dept == "Slimline")
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

                int index = 5;
                if (dataGridView1.Columns.Contains("Date ") == true)
                    index = dataGridView1.Columns["Date "].Index;
                frmWeeklyShift frm = new frmWeeklyShift(Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[index].Value.ToString()), _staff_id, _dept);
                frm.ShowDialog();

                //if (shiftHours.validation == 0)
                //{
                //    MessageBox.Show("Shift Cancelled");
                //    return;
                //}

                dataGridView1.Rows[e.RowIndex].Cells[7].Value = shiftHours._hours;
                dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Shift";
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PaleVioletRed;
            }
            if (e.ColumnIndex == dataGridView1.Columns["Manual"].Index)
            {
                if (_dept == "Slimline")
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
                //grab time from user input
                //open form
                frmManualHours mh = new frmManualHours();
                mh.ShowDialog();
                dataGridView1.Rows[e.RowIndex].Cells[7].Value = mh._manualHours;
                dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Manual";
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSeaGreen;
            }
        }
    }
}
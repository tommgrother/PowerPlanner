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

namespace ShopFloorPlacementPlanner
{
    public partial class frmSelectStaff : Form

    {
        public string _department { get; set; }
        public DateTime _selectedDate { get; set; }
        public double _standardHours { get; set; }

        public frmSelectStaff(string department, DateTime selectedDate)
        {

            InitializeComponent();
          
            dgSelected.CellClick += dataGridViewSoftware_CellClick;
            _department = department;
            _selectedDate = selectedDate;

            ensureDateTableEntry();
            //getStandardHours();
            fillGrid();


            checkExistingSelections();
            getOvertime();



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

            string sql="";


            switch (_department)
            {
                case "Slimline":
                    sql = "SELECT slimline_OT as 'FieldName' from dbo.power_plan_overtime where date_id=@dateID";
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

        private void frmSelectStaff_Load(object sender, EventArgs e)
        {
        
        }

        private void paintGrid()
        {
            foreach (DataGridViewRow row in dgSelected.Rows)
                if (row.Cells[3].Value.ToString() == "Shift")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            foreach (DataGridViewRow row in dgSelected.Rows)
                if (row.Cells[3].Value.ToString() == "Half Day")
                {
                    row.DefaultCellStyle.BackColor = Color.MediumPurple;
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


            getStandardHours(staffID);


            //REMOVE BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Remove"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("DELETE  FROM DBO.power_plan_staff where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("@placementID", placementID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();
                }
            }


            //MANUAL BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Manual"].Index)
            {

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
                }
            }

            //FULL BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Shift"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.power_plan_staff set placement_type = 'Shift' , hours = @hours where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("placementID", placementID);
                    cmd.Parameters.AddWithValue("@hours", _standardHours);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();
                }
            }


            //Half BUTTON
            if (e.ColumnIndex == dgSelected.Columns["Half"].Index)
            {
                SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.power_plan_staff set placement_type = 'Half Day' , hours = @hours where ID = @placementID", conn))
                {
                    cmd.Parameters.AddWithValue("placementID", placementID);
                    cmd.Parameters.AddWithValue("@hours", _standardHours / 2 );
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    checkExistingSelections();
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


                paintGrid();



                conn.Close();



            }
        }
        private void populateExisting()
        {


            //Adds the existing selections back into the selection window
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT PlacementID,staff_id as 'Staff ID',[Staff Name] as 'Full Name',Placement as 'Placement Type',hours as 'Hours' from dbo.view_planner_punch_staff where date_plan = @selectedDate and department = @dept order by placementID", conn))
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
            if (_department == "Slimline")
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

            if (dayOfWeek == "Friday")
            {
                switch (staffID)
                {
                    case 63:
                        _standardHours = 3.6;
                        break;
                    case 165:
                        _standardHours = 11.2;
                        break;
                    default:
                        _standardHours = 5.6;
                        break;

                }

            }else if(dayOfWeek == "Saturday" || dayOfWeek == "Sunday")
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

           

            if (p._notPresentType == 5 || p._notPresentType == 2)
            {
                MessageBox.Show("This staff member is either absent today or has a full day holiday!", "Cannot Place", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(p._notPresentType == 3)
            {
                MessageBox.Show("This staff member has half day holiday so can only be placed for half day", "Half Day Placement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                remainingHours = _standardHours / 2;
                Placement p3 = new Placement(_selectedDate, s._staffID, _department,"Half Day", remainingHours);
                p3.addPlacment();
            }
            else
            {
                if (p._alreadyPlaced == true)
                {
                    if (p._existingPlacementHours == _standardHours)
                    {

                        MessageBox.Show("Staff member already has a full day placement for this day!", "Already Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                            p.addPlacment();
                        }
                    }
                }
            }


            


           
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
            
            o.updateOT(_selectedDate,_department, overtimeAmount);
        }


        private void frmSelectStaff_Leave(object sender, EventArgs e)
        {

         




        }

        private void frmSelectStaff_FormClosed(object sender, FormClosedEventArgs e)
        {
            submitOT();
        }
    }
}

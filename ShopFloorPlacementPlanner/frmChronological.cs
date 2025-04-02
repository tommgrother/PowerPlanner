﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    public partial class frmChronological : Form
    {
        public int user_id { get; set; }
        public int actionIndex { get; set; }
        public string _staff { get; set; }
        public int _staff_id { get; set; }
        public string _dept { get; set; }
        public int _status_index { get; set; }
        public int _door_id_index { get; set; }
        public int _door_type_index { get; set; }
        public int _time_index { get; set; }
        public int _action_index { get; set; }
        public int _note_index { get; set; }
        public int _part_index { get; set; }
        public int _time_for_part_index { get; set; }
        public int _time_for_part_minute_index { get; set; }
        public string _hours { get; set; }

        public int _allocation_staff_id { get; set; }
        public string _allocation_staff_name { get; set; }

        public frmChronological(string staff, string dept, DateTime plannerDate, string hours)
        {
            InitializeComponent();
            dteAction.Value = plannerDate;
            dteActionEnd.Value = plannerDate;
            _staff = staff;
            _allocation_staff_name = staff;
            _dept = dept;
            _hours = hours;
            getData(staff, dept);
        }


        private void getStaffDropped()
        {
            //work out hours for label
            double hours = 0;
            string sql_hours = "SELECT " +
                         "cast(coalesce(sum(hours),0) as float) + coalesce(sum(cast(coalesce(o.overtime ,0)as float) * 0.8),0) " +
                         "FROM dbo.power_plan_staff a " +
                         "LEFT JOIN dbo.power_plan_date b on a.date_id = b.id " +
                         "left join dbo.power_plan_staff_percent_log p on b.date_plan = p.log_date AND a.staff_id = p.staff_id AND a.department = p.department " +
                         "left join dbo.power_plan_overtime_remake o on a.date_id = o.date_id AND a.department = o.department AND a.staff_id = o.staff_id " +
                         "WHERE " +
                         "a.staff_id = " + _allocation_staff_id + " AND " +
                         "CAST(b.date_plan as DATE)>= '" + dteAction.Value.ToString("yyyyMMdd") + "' AND " +
                         "CAST(b.date_plan as DATE)<= '" + dteActionEnd.Value.ToString("yyyyMMdd") + "' AND " +
                         "a.department = '" + _dept + "' ";


            double worked = 0;
            string sql_worked = "SELECT COALESCE((SELECT ROUND((SUM(time_for_part) / 60),2) as [time_for_part] " +
                   "FROM dbo.door_part_completion_log " +
                   "WHERE staff_id = " + _allocation_staff_id + " AND " +
                   "CAST(part_complete_date as DATE)>= '" + dteAction.Value.ToString("yyyyMMdd") + "' AND " +
                   "CAST(part_complete_date as DATE)<= '" + dteActionEnd.Value.ToString("yyyyMMdd") + "' AND " +
                   "(part_status = 'Complete' or part_status = 'Partial')  AND op = '" + _dept + "' GROUP BY staff_id),0)";


            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql_hours, conn))
                    hours = Convert.ToDouble(cmd.ExecuteScalar().ToString());

                using (SqlCommand cmd = new SqlCommand(sql_worked, conn))
                    worked = Convert.ToDouble(cmd.ExecuteScalar().ToString());


                double fuga = hours - worked;
                string a = "";
                if (fuga < 0)
                {
                    
                    fuga = fuga * -1;
                }

                label1.Text = _allocation_staff_name + " - " + fuga;

                if (hours > worked)  //gained / dropped
                {
                    double temp = hours - worked;
                    if (temp < 0)
                        temp = temp * -1;
                    label1.BackColor = Color.PaleVioletRed;
                    label1.Text = "Dropped " + Math.Round(temp, 2).ToString() + " Hours";
                }
                else
                {
                    double temp = hours - worked;
                    if (temp < 0)
                        temp = temp * -1;

                    label1.Text = "Gained " + Math.Round(temp, 2).ToString() + " Hours";
                    label1.BackColor = Color.DarkSeaGreen;
                }


                conn.Close();
            }

        }



        private void getData(string staff, string dept)
        {
            int staff_id = 0;
            //MessageBox.Show(staff);
            staff_id = staff.IndexOf(" ", staff.IndexOf(" ") + 1); //staff id is a temp int var here
            _staff_id = staff_id;
            staff = staff.Substring(0, staff_id);
            //MessageBox.Show(staff);
            label1.Text = staff + " " + _hours + " Hours";
            //
            
            //

            if (label1.Text.Contains("Dropped"))
                label1.BackColor = Color.PaleVioletRed;
            else if (label1.Text.Contains("Gained"))
                label1.BackColor = Color.YellowGreen;
            //grab the staff name
            using (SqlConnection conn2 = new SqlConnection(connectionStrings.ConnectionStringUser))
            {
                conn2.Open();
                string sql = "SELECT id FROM dbo.[user] WHERE forename + ' ' + surname = '" + staff + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn2))
                {

                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());
                    _allocation_staff_id = staff_id;

                }
                //start date too
                sql = "SELECT cast([start_date] as date) FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + staff + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn2))
                {
                    var temp = cmd.ExecuteScalar();
                    if (temp != null)
                        lblStartDate.Text = "Start Date: " + Convert.ToDateTime(cmd.ExecuteScalar().ToString()).ToString("dd/MM/yyyy");
                    else
                        lblStartDate.Text = "No Start Date";
                }
                conn2.Close();
            }
            getStaffDropped();
            //usp_power_planner_chronological_shop_actions_simline
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                if (dept == "Slimline")
                {
                    using (SqlCommand cmd = new SqlCommand("usp_power_planner_chronological_shop_actions_slimline", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action_time", SqlDbType.Date).Value = dteAction.Value;
                        cmd.Parameters.AddWithValue("@action_time_end", SqlDbType.Date).Value = dteActionEnd.Value;
                        cmd.Parameters.AddWithValue("@user_id", SqlDbType.Date).Value = staff_id;

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Status");// dataGridView1.Columns.Add("Status", "Status");
                        dt.Columns.Add("Time");
                        da.Fill(dt);
                        conn.Close();
                        dataGridView1.DataSource = dt;

                        //format
                        REarrange();
                        dataGridView1.Refresh();
                    }

                }
                else if (dept == "Bending")
                {
                    using (SqlCommand cmd = new SqlCommand("usp_power_planner_chronological_shop_actions_bending", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action_time", SqlDbType.Date).Value = dteAction.Value;
                        cmd.Parameters.AddWithValue("@action_time_end", SqlDbType.Date).Value = dteActionEnd.Value;
                        cmd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = dept;
                        cmd.Parameters.AddWithValue("@user_id", SqlDbType.Date).Value = staff_id;

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        //dt.Columns.Add("Status");// dataGridView1.Columns.Add("Status", "Status");
                        //dt.Columns.Add("Time");
                        da.Fill(dt);
                        conn.Close();
                        dataGridView1.DataSource = dt;

                        //format
                        REarrange();
                        dataGridView1.Refresh();
                    }
                }
                else //usp_power_planner_chronological_shop_actions
                {
                    using (SqlCommand cmd = new SqlCommand("usp_power_planner_chronological_shop_actions", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action_time", SqlDbType.Date).Value = dteAction.Value;
                        cmd.Parameters.AddWithValue("@action_time_end", SqlDbType.Date).Value = dteActionEnd.Value;
                        cmd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = dept;
                        cmd.Parameters.AddWithValue("@user_id", SqlDbType.Date).Value = staff_id;

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Status");// dataGridView1.Columns.Add("Status", "Status");
                        dt.Columns.Add("Time");
                        da.Fill(dt);
                        conn.Close();
                        dataGridView1.DataSource = dt;

                        //format
                        REarrange();
                        dataGridView1.Refresh();
                    }
                }
            }
        }

        private void REarrange()
        {
            //ok one of the harder things to do here is to get the columns to merge like they do in the report :{
            //first step lets grab all of the indexes of each column we have
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            int part_time_minutes, status, action, part, part_time, action_time, action_date, fullname, door_id, sort_order, department, department_time, predicted_end, note, door_type, time;
            if (_dept == "Bending")
            {
                //status = dataGridView1.Columns["status"].Index;
                //_status_index = status;
                action = dataGridView1.Columns["action"].Index;
                _action_index = action;
                actionIndex = action;
                part = dataGridView1.Columns["part"].Index;
                _part_index = part;
                part_time = dataGridView1.Columns["part_time"].Index;
                _time_for_part_index = part_time;

                part_time_minutes = dataGridView1.Columns["part_time_minutes"].Index;
                _time_for_part_minute_index = part_time_minutes;
                //action_time = dataGridView1.Columns["action_time"].Index;
                action_date = dataGridView1.Columns["action_date"].Index;
                fullname = dataGridView1.Columns["fullname"].Index;
                door_id = dataGridView1.Columns["door_id"].Index;
                _door_id_index = door_id;
                //sort_order = dataGridView1.Columns["sort_order"].Index;
                //department = dataGridView1.Columns["department"].Index;
                //department_time = dataGridView1.Columns["department_time"].Index;
                //predicted_end = dataGridView1.Columns["predicted_end"].Index;
                note = dataGridView1.Columns["note"].Index;
                _note_index = note;
                door_type = dataGridView1.Columns["door_type"].Index;
                _door_type_index = door_type;
                time = dataGridView1.Columns["Time"].Index;
                _time_index = time;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //start messing with the columns :}

                //first column :- =IIf([Action]="Door Start",[Action] & " - Duration: " & Round(([department_time]/60),2) & " >>",IIf([Action]<>"Finish Part",[Action] & " >>",""))
                //gonna need to loop through all the records for this one



                //formating
                //hide the columns we do not need
                //dataGridView1.Columns[action_date].Visible = false;
                //dataGridView1.Columns[action_time].Visible = false;
                dataGridView1.Columns[fullname].Visible = false;
                //dataGridView1.Columns[sort_order].Visible = false;
                //dataGridView1.Columns[department].Visible = false;
                //dataGridView1.Columns[department_time].Visible = false;
                //dataGridView1.Columns[predicted_end].Visible = false;
                dataGridView1.Columns[note].Visible = false;

                //dataGridView1.Columns[time].DisplayIndex = 3;
                //dataGridView1.Columns[door_id].DisplayIndex = status + 1; //1st column
                //dataGridView1.Columns[door_type].DisplayIndex = status + 2; //2nd etc...
                //dataGridView1.Columns[action].DisplayIndex = status + 3;
                //dataGridView1.Columns[part].DisplayIndex = status + 4;
                //dataGridView1.Columns[part_time].DisplayIndex = status + 5;

                //sizeeeees
                //dataGridView1.Columns[status].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[door_type].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[door_id].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[action].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[action_date].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[part].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[part_time].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[part_time_minutes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //dataGridView1.Columns[time].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //headertext
                //dataGridView1.Columns[status].HeaderText = "Status";
                dataGridView1.Columns[door_type].HeaderText = "Door Type";
                dataGridView1.Columns[door_id].HeaderText = "Door ID";
                dataGridView1.Columns[action].HeaderText = "Action";
                dataGridView1.Columns[part].HeaderText = "Part";
                dataGridView1.Columns[action_date].HeaderText = "Action Date";
                dataGridView1.Columns[part_time].HeaderText = "Time For Part";
                //dataGridView1.Columns[time].HeaderText = "Time";

                //messing with the colours
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Finish")) //mark complete jobs as green
                //        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                //}
            }
            else
            {
                status = dataGridView1.Columns["status"].Index;
                _status_index = status;
                action = dataGridView1.Columns["action"].Index;
                _action_index = action;
                actionIndex = action;
                part = dataGridView1.Columns["part"].Index;
                _part_index = part;
                part_time = dataGridView1.Columns["part_time"].Index;
                _time_for_part_index = part_time;

                part_time_minutes = dataGridView1.Columns["part_time_minutes"].Index;
                _time_for_part_minute_index = part_time_minutes;


                action_time = dataGridView1.Columns["action_time"].Index;
                action_date = dataGridView1.Columns["action_date"].Index;
                fullname = dataGridView1.Columns["fullname"].Index;
                door_id = dataGridView1.Columns["door_id"].Index;
                _door_id_index = door_id;
                sort_order = dataGridView1.Columns["sort_order"].Index;
                department = dataGridView1.Columns["department"].Index;
                department_time = dataGridView1.Columns["department_time"].Index;
                predicted_end = dataGridView1.Columns["predicted_end"].Index;
                note = dataGridView1.Columns["note"].Index;
                _note_index = note;
                door_type = dataGridView1.Columns["door_type"].Index;
                _door_type_index = door_type;
                time = dataGridView1.Columns["Time"].Index;
                _time_index = time;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //start messing with the columns :}

                //first column :- =IIf([Action]="Door Start",[Action] & " - Duration: " & Round(([department_time]/60),2) & " >>",IIf([Action]<>"Finish Part",[Action] & " >>",""))
                //gonna need to loop through all the records for this one

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[action].Value.ToString() == "Door Start")
                    {
                        dataGridView1.Rows[i].Cells[status].Value = dataGridView1.Rows[i].Cells[action].Value.ToString() + " - Duration: " +
                            Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells[department_time].Value) / 60, 2)) + " " +
                            " ( " + Convert.ToString(Math.Round(Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells[department_time].Value) / 60, 2) * 60, 0)) +
                            " mins) >>";
                    }
                    else if (dataGridView1.Rows[i].Cells[action].Value.ToString().Contains("Live") == true)
                    {
                        dataGridView1.Rows[i].Cells[status].Value = dataGridView1.Rows[i].Cells[action].Value.ToString() + " - Duration: " +
                            Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells[department_time].Value) / 60, 2)) +
                            " ( " + Convert.ToString(Math.Round(Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells[department_time].Value) / 60, 2) * 60, 0)) +
                            " mins) >>";
                    }
                    else if (dataGridView1.Rows[i].Cells[action].Value.ToString() != "Finish Part")
                    {
                        dataGridView1.Rows[i].Cells[status].Value = dataGridView1.Rows[i].Cells[action].Value.ToString() + " >> ";
                    }

                    //else if (dataGridView1.Rows[i].Cells[action].Value.ToString().Contains("Door Allocated"))
                    //{
                    //    dataGridView1.Rows[i].Cells[actionIndex].Value = dataGridView1.Rows[i].Cells[action].Value = "Door Allocated";
                    //}
                    //while we are here also remove the date from the actiontime
                    DateTime tempDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[action_time].Value);
                    dataGridView1.Rows[i].Cells[time].Value = tempDate.ToString("dd/MM/yyyy HH:mm");
                }

                //formating
                //hide the columns we do not need
                dataGridView1.Columns[action_date].Visible = false;
                dataGridView1.Columns[action_time].Visible = false;
                dataGridView1.Columns[fullname].Visible = false;
                dataGridView1.Columns[sort_order].Visible = false;
                dataGridView1.Columns[department].Visible = false;
                dataGridView1.Columns[department_time].Visible = false;
                dataGridView1.Columns[predicted_end].Visible = false;
                dataGridView1.Columns[note].Visible = false;

                dataGridView1.Columns[time].DisplayIndex = 3;
                dataGridView1.Columns[door_id].DisplayIndex = status + 1; //1st column
                dataGridView1.Columns[door_type].DisplayIndex = status + 2; //2nd etc...
                dataGridView1.Columns[action].DisplayIndex = status + 3;
                dataGridView1.Columns[part].DisplayIndex = status + 4;
                dataGridView1.Columns[part_time].DisplayIndex = status + 5;
                dataGridView1.Columns[part_time_minutes].DisplayIndex = status + 6;

                //sizeeeees
                dataGridView1.Columns[status].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[door_type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[door_id].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[action].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[part].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[part_time].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[part_time_minutes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[time].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //headertext
                dataGridView1.Columns[status].HeaderText = "Status";
                dataGridView1.Columns[door_type].HeaderText = "Door Type";
                dataGridView1.Columns[door_id].HeaderText = "Door ID";
                dataGridView1.Columns[action].HeaderText = "Action";
                dataGridView1.Columns[part].HeaderText = "Part";
                dataGridView1.Columns[part_time].HeaderText = "Time For Part";
                dataGridView1.Columns[part_time_minutes].HeaderText = "Time For Part (mins)";
                dataGridView1.Columns[time].HeaderText = "Time";

                //messing with the colours
                List<string> door_list = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Finish")) //mark complete jobs as green
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Complete")) //mark complete doors as green
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        //grab the door id for this too
                        door_list.Add(dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString());

                    }
                    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Start")) //mark started doors as green
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.CornflowerBlue;

                    if (dataGridView1.Rows[i].Cells["Status"].Value.ToString().Contains("Door Complete >>"))
                    {

                        //while we are here stick the value in this cell
                        string value = "select '£' + format(line_total_no_install,'n0') FROM dbo.view_door_value where id = " + dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString();
                        using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(value, conn))
                                value = cmd.ExecuteScalar().ToString();

                            dataGridView1.Rows[i].Cells["Status"].Value = dataGridView1.Rows[i].Cells["Status"].Value.ToString() + " Value: " + value;
                            conn.Close();
                        }
                    }
                }
                string sql = "";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sql = "select * from dbo.door_stoppages right join(select MAX(id) as id,MAX(door_id) as door_id from dbo.door_stoppages group by door_id,department) a on a.id = door_stoppages.id " +
                        "where [action] = 'Paused' AND department = '" + _dept + "' AND dbo.door_stoppages.door_id = " + dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            var temp = cmd.ExecuteScalar();
                            if (temp != null)
                            {
                                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Start"))
                                {
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                                }
                            }
                            conn.Close();
                        }
                    }
                }
                //now loop through it again - checking each door_id for a match and make it green
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (door_list.Contains(dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString()))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                    //note = yellow is the final colour change to make
                    if (/*dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Paused") &&*/ dataGridView1.Rows[i].Cells[_note_index].Value.ToString().Length > 0)
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            dataGridView1.ClearSelection();
        }

        private void frmChronological_Shown(object sender, EventArgs e)
        {
            List<string> door_list = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Finish")) //mark complete jobs as green
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Complete")) //mark complete doors as green
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    //grab the door id for this too
                    door_list.Add(dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString());
                }
                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Start")) //mark started doors as green
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.CornflowerBlue;
                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Complete")) //mark started doors as green
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.PaleVioletRed;


                }
            }
            string sql = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sql = "select * from dbo.door_stoppages right join(select MAX(id) as id,MAX(door_id) as door_id from dbo.door_stoppages group by door_id,department) a on a.id = door_stoppages.id " +
                    "where [action] = 'Paused' AND department = '" + _dept + "' AND dbo.door_stoppages.door_id = " + dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString();
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var temp = cmd.ExecuteScalar();
                        if (temp != null)
                        {
                            if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Start"))
                            {
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                            }
                        }
                        conn.Close();
                    }
                }
            }

            //now loop through it again - checking each door_id for a match and make it green
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (door_list.Contains(dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString()))
                {
                    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.CornflowerBlue)
                    { }
                    else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                    { }
                    else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.PaleVioletRed)
                    { }
                    else
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                }
                //note = yellow is the final colour change to make
                if (/*dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Paused") &&*/ dataGridView1.Rows[i].Cells[_note_index].Value.ToString().Length > 0)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            }
            dataGridView1.ClearSelection();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Rectangle bounds = this.Bounds;
            //using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            //{
            //    using (Graphics g = Graphics.FromImage(bitmap))
            //    {
            //        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            //    }
            //    bitmap.Save(@"C:\Temp\temp.jpg", ImageFormat.Jpeg);
            //    printImage();
            //}

            //^^ old way to print

            print_excel_new();
        }

        private void print_excel_new()
        {
            _status_index = dataGridView1.Columns["status"].Index;

            _action_index = dataGridView1.Columns["action"].Index;


            _part_index = dataGridView1.Columns["part"].Index;

            _time_for_part_index = dataGridView1.Columns["part_time"].Index;

            _time_for_part_minute_index = dataGridView1.Columns["part_time_minutes"].Index;

            _door_id_index = dataGridView1.Columns["door_id"].Index;



            _note_index = dataGridView1.Columns["note"].Index;

            _door_type_index = dataGridView1.Columns["door_type"].Index;

            _time_index = dataGridView1.Columns["Time"].Index;




            int current_excel_row = 1;
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\Chronological.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");


            int day_counter = 0;



            int sheet_counter = 1;
            int loop_counter = 0;
            int max_loop_counter = dataGridView1.Rows.Count;

            while (sheet_counter < 9999)
            {
                current_excel_row = 1;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Worksheets.Add
                    (System.Reflection.Missing.Value, xlWorkbook.Worksheets[xlWorkbook.Worksheets.Count],
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xlWorksheet = xlWorkbook.Sheets[sheet_counter];

                xlWorkSheet.Name = "Sheet " + sheet_counter.ToString();
                //add the title
                xlWorksheet.Cells[1][current_excel_row].Value2 = label1.Text;

                //
                if (label1.BackColor != Color.Empty)
                    xlWorksheet.Range["A" + current_excel_row.ToString() + ":H" + current_excel_row.ToString()].Interior.Color = label1.BackColor;

                xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 7]].Merge();
                xlWorkSheet.Range["A1:H2"].Cells.Font.Size = 20;

                current_excel_row++;

                //column headers
                current_excel_row++;

                if (dataGridView1.Rows.Count <= 0)
                {
                    loop_counter = 99999999;
                }
                else
                {
                    DateTime current_date = Convert.ToDateTime(dataGridView1.Rows[0].Cells["Time"].Value).Date;

                    //DateTime current_date = Convert.ToDateTime(Convert.ToDateTime(dataGridView1.Rows[0].Cells[_time_index].Value).Date.ToString("yyyyMMdd"));

                    //vvv we need to loop through dgv 
                    for (int i = loop_counter; i < max_loop_counter; i++)
                    {
                        if (current_date.Date < Convert.ToDateTime(dataGridView1.Rows[i].Cells[_time_index].Value).Date)
                        {
                            day_counter++;
                            current_date = Convert.ToDateTime(dataGridView1.Rows[i].Cells[_time_index].Value).Date;
                            if (day_counter == 5)
                            {
                                day_counter = 0;
                                break;
                            }
                        }

                        xlWorksheet.Cells[1][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_status_index].Value.ToString();
                        xlWorksheet.Cells[1][current_excel_row].Cells.Font.Size = 20;
                        xlWorksheet.Cells[2][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString();
                        xlWorksheet.Cells[2][current_excel_row].Cells.Font.Size = 20;
                        xlWorksheet.Cells[3][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_door_type_index].Value.ToString();
                        xlWorksheet.Cells[3][current_excel_row].Cells.Font.Size = 20;
                        xlWorksheet.Cells[4][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[actionIndex].Value.ToString();
                        xlWorksheet.Cells[4][current_excel_row].Cells.Font.Size = 20;
                        xlWorksheet.Cells[5][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_part_index].Value.ToString();
                        xlWorksheet.Cells[5][current_excel_row].Cells.Font.Size = 20;
                        if (btnHideTimes.Text == "Hide Times")
                        {
                            xlWorksheet.Cells[6][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_time_for_part_index].Value.ToString();
                            xlWorksheet.Cells[6][current_excel_row].Cells.Font.Size = 20;
                            xlWorksheet.Cells[7][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_time_for_part_minute_index].Value.ToString();
                            xlWorksheet.Cells[7][current_excel_row].Cells.Font.Size = 20;
                        }
                        xlWorksheet.Cells[8][current_excel_row].Value2 = dataGridView1.Rows[i].Cells[_time_index].Value.ToString();
                        xlWorksheet.Cells[8][current_excel_row].Cells.Font.Size = 20;
                        //paint the row based on what the dgv is

                        if (dataGridView1.Rows[i].DefaultCellStyle.BackColor != Color.Empty)
                            xlWorksheet.Range["A" + current_excel_row.ToString() + ":H" + current_excel_row.ToString()].Interior.Color = dataGridView1.Rows[i].DefaultCellStyle.BackColor;

                        //xlWorkSheet.Range["A" + current_excel_row.ToString() + ":H" + current_excel_row.ToString()].Cells.Font.Size = 20;

                        current_excel_row++;
                        loop_counter++;
                    }
                }

                //border
                xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row - 1, 8]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                xlWorksheet.Columns.AutoFit();

                //xlWorksheet.Rows.AutoFit();

                Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
                xlPageSetUp.Zoom = false;
                xlPageSetUp.FitToPagesWide = 1;
                xlPageSetUp.Orientation = Excel.XlPageOrientation.xlLandscape;



                xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                sheet_counter++;
                if (loop_counter >= max_loop_counter)
                    sheet_counter = 99999999;
            }


            //xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
            }
        }

        private void print_excel()
        {

            int current_excel_row = 1;
            // Store the Excel processes before opening.
            Process[] processesBefore = Process.GetProcessesByName("excel");
            // Open the file in Excel.
            string temp = @"\\designsvr1\public\Kevin Power Planner\Chronological.xlsx";
            var xlApp = new Excel.Application();
            var xlWorkbooks = xlApp.Workbooks;
            var xlWorkbook = xlWorkbooks.Open(temp);
            var xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            // Get Excel processes after opening the file.
            Process[] processesAfter = Process.GetProcessesByName("excel");


            //add the title
            xlWorksheet.Cells[1][current_excel_row].Value2 = label1.Text;
            //
            if (label1.BackColor != Color.Empty)
                xlWorksheet.Range["A" + current_excel_row.ToString() + ":G" + current_excel_row.ToString()].Interior.Color = label1.BackColor;
            current_excel_row++;

            //column headers
            current_excel_row++;

            //vvv we need to loop through dgv 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                xlWorksheet.Cells[1][current_excel_row].Value2 = row.Cells[_status_index].Value.ToString();
                xlWorksheet.Cells[2][current_excel_row].Value2 = row.Cells[_door_id_index].Value.ToString();
                xlWorksheet.Cells[3][current_excel_row].Value2 = row.Cells[_door_type_index].Value.ToString();
                xlWorksheet.Cells[4][current_excel_row].Value2 = row.Cells[actionIndex].Value.ToString();
                xlWorksheet.Cells[5][current_excel_row].Value2 = row.Cells[_part_index].Value.ToString();
                if (btnHideTimes.Text == "Hide Times")
                    xlWorksheet.Cells[6][current_excel_row].Value2 = row.Cells[_time_for_part_index].Value.ToString();
                xlWorksheet.Cells[7][current_excel_row].Value2 = row.Cells[_time_index].Value.ToString();
                //paint the row based on what the dgv is
                if (row.DefaultCellStyle.BackColor != Color.Empty)
                    xlWorksheet.Range["A" + current_excel_row.ToString() + ":G" + current_excel_row.ToString()].Interior.Color = row.DefaultCellStyle.BackColor;
                current_excel_row++;
            }

            //border
            xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[current_excel_row - 1, 7]].Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


            xlWorksheet.Columns.AutoFit();
            xlWorksheet.Rows.AutoFit();

            Excel.PageSetup xlPageSetUp = xlWorksheet.PageSetup;
            xlPageSetUp.Zoom = false;
            xlPageSetUp.FitToPagesWide = 1;
            xlPageSetUp.Orientation = Excel.XlPageOrientation.xlPortrait;

            xlWorksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);




            //xlWorkbook.SaveAs(@"c:\temp\test.xlsx");  // or book.Save();

            xlWorkbook.Close(false); //close the excel sheet without saving
                                     // xlApp.Quit();


            // Manual disposal because of COM
            xlApp.Quit();

            // Now find the process id that was created, and store it.
            int processID = 0;
            foreach (Process process in processesAfter)
            {
                if (!processesBefore.Select(p => p.Id).Contains(process.Id))
                    processID = process.Id;

            }

            // And now kill the process.
            if (processID != 0)
            {
                Process process = Process.GetProcessById(processID);
                process.Kill();
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

        private void dteAction_CloseUp(object sender, EventArgs e)
        {
            getData(_staff, _dept);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            //insert into temptable

            //using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            //{
            //    conn.Open();
            //    string sql = "DELETE FROM dbo.chronological_temp_table";
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        cmd.ExecuteNonQuery();
            //    }
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {

            //        sql = "INSERT INTO dbo.chronological_temp_table (status,door_id,door_type,time,action,part,time_for_part) VALUES ('" + row.Cells[_status_index].Value.ToString() + "','" + row.Cells[_door_id_index].Value.ToString() + "','" +
            //             row.Cells[_door_type_index].Value.ToString() + "','" + row.Cells[_time_index].Value.ToString() + "','" + row.Cells[_action_index].Value.ToString() + "','" + row.Cells[_part_index].Value.ToString() + "','" + row.Cells[_time_for_part_index].Value.ToString() + "') ";
            //        using (SqlCommand cmd = new SqlCommand(sql, conn))
            //        {
            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //    conn.Close();
            //}

            //frmProductivityEmail frm = new frmProductivityEmail(label1.Text);
            //frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_time_for_part_minute_index == e.ColumnIndex)
            {

                int door_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[_door_id_index].Value.ToString());
                string dept = _dept;
                double time_for_part = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[_time_for_part_minute_index].Value.ToString());
                int staff_id = _allocation_staff_id;
                string part_name = dataGridView1.Rows[e.RowIndex].Cells[_part_index].Value.ToString();
                string date = dataGridView1.Rows[e.RowIndex].Cells[_time_index].Value.ToString();

                frmMoveTime frm = new frmMoveTime(door_id, dept, time_for_part, staff_id, part_name, date);
                frm.ShowDialog();

                getData(_staff, _dept);
            }
            else if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Yellow)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[_note_index].Value.ToString());
            }

        }

        private void btnSort_Click(object sender, EventArgs e)
        {

            if (btnSort.Text == "Sort by Doors")
            {
                btnSort.Text = "Sort by Time";
                this.dataGridView1.Sort(this.dataGridView1.Columns[_door_id_index], ListSortDirection.Ascending);
                List<string> door_list = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Finish")) //mark complete jobs as green
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Complete")) //mark complete doors as green
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        //grab the door id for this too
                        door_list.Add(dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString());
                    }
                    if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Start")) //mark started doors as green
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    //if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Complete")) //mark started doors as green
                    //    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                }
                string sql = "";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sql = "select * from dbo.door_stoppages right join(select MAX(id) as id,MAX(door_id) as door_id from dbo.door_stoppages group by door_id,department) a on a.id = door_stoppages.id " +
                        "where [action] = 'Paused' AND department = '" + _dept + "' AND dbo.door_stoppages.door_id = " + dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            var temp = cmd.ExecuteScalar();
                            if (temp != null)
                            {
                                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Door Start"))
                                {
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                                }
                            }
                            conn.Close();
                        }
                    }
                }

                //now loop through it again - checking each door_id for a match and make it green
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (door_list.Contains(dataGridView1.Rows[i].Cells[_door_id_index].Value.ToString()))
                    {
                        if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.CornflowerBlue)
                        { }
                        else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                        { }
                        else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.PaleVioletRed)
                        { }
                        else
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                    //note = yellow is the final colour change to make
                    if (/*dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Paused") &&*/ dataGridView1.Rows[i].Cells[_note_index].Value.ToString().Length > 0)
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                dataGridView1.ClearSelection();
            }
            else
            {
                btnSort.Text = "Sort by Doors";
                getData(_staff, _dept);
            }

        }

        private void btnHideTimes_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns[_time_for_part_index].Visible == false)
            {
                dataGridView1.Columns[_time_for_part_index].Visible = true;
                dataGridView1.Columns[_time_for_part_minute_index].Visible = true;
                btnHideTimes.Text = "Hide Times";
            }
            else
            {
                dataGridView1.Columns[_time_for_part_index].Visible = false;
                dataGridView1.Columns[_time_for_part_minute_index].Visible = false;
                btnHideTimes.Text = "Show Times";
            }
        }

        private void dteActionEnd_CloseUp(object sender, EventArgs e)
        {
            getData(_staff, _dept);
        }

        private void btnAllocation_Click(object sender, EventArgs e)
        {
            frmAllocation frm = new frmAllocation(_allocation_staff_id, _dept, _allocation_staff_name.Trim() + " Time in Motion Doors Complete");
            frm.ShowDialog();
        }

        private void btnTimeInMotion_Click(object sender, EventArgs e)
        {
            frmTimeInMotion frm = new frmTimeInMotion(dteAction.Value, dteActionEnd.Value, _allocation_staff_id, _dept, _allocation_staff_name);
            frm.ShowDialog();
        }
    }
}
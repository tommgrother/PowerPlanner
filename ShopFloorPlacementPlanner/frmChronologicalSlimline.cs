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

namespace SlimlinePowerPlanner
{
    public partial class frmChronologicalSlimline : Form
    {

        public int _staff_id { get; set; }
        public int _department { get; set; }

        public frmChronologicalSlimline(int staff_id,int department,int date_id)
        {
            InitializeComponent();
            SqlStatements s = new SqlStatements();
            _staff_id = staff_id;
            _department = department;
            dteAction.Value = s.GetDateFromDateID(date_id);
            dteActionEnd.Value = dteAction.Value;

            label1.Text = s.returnStaffName(staff_id);

        }


        public void load_dgv ()
        {
            SqlStatements s = new SqlStatements();
            DataTable dt = s.LoadChronological(dteAction.Value, dteActionEnd.Value, _staff_id, _department);

            dgvChronological.DataSource = dt;
            REarrange();
        }

        private void REarrange()
        {
            //ok one of the harder things to do here is to get the columns to merge like they do in the report :{
            //first step lets grab all of the indexes of each column we have
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            int part_time_minutes, status, action, part, part_time, action_time, action_date, fullname, door_id, sort_order, department, department_time, predicted_end, note, door_type, time;
                status = dgvChronological.Columns["status"].Index;
                action = dgvChronological.Columns["action"].Index;
                part = dgvChronological.Columns["part"].Index;
                part_time = dgvChronological.Columns["part_time"].Index;

                if (dgvChronological.Columns["part_time_minutes"] != null)
                {
                    part_time_minutes = dgvChronological.Columns["part_time_minutes"].Index;
                }
                else
                {
                    part_time_minutes = 0;
                }

                action_time = dgvChronological.Columns["action_time"].Index;
                action_date = dgvChronological.Columns["action_date"].Index;
                fullname = dgvChronological.Columns["fullname"].Index;
                door_id = dgvChronological.Columns["door_id"].Index;
                sort_order = dgvChronological.Columns["sort_order"].Index;
                department = dgvChronological.Columns["department"].Index;
                department_time = dgvChronological.Columns["department_time"].Index;
                predicted_end = dgvChronological.Columns["predicted_end"].Index;
                note = dgvChronological.Columns["note"].Index;
                door_type = dgvChronological.Columns["door_type"].Index;
                time = dgvChronological.Columns["Time"].Index;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //start messing with the columns :}

                //first column :- =IIf([Action]="Door Start",[Action] & " - Duration: " & Round(([department_time]/60),2) & " >>",IIf([Action]<>"Finish Part",[Action] & " >>",""))
                //gonna need to loop through all the records for this one

                for (int i = 0; i < dgvChronological.Rows.Count; i++)
                {
                    if (dgvChronological.Rows[i].Cells[action].Value.ToString() == "Door Start")
                    {
                        dgvChronological.Rows[i].Cells[status].Value = dgvChronological.Rows[i].Cells[action].Value.ToString() + " - Duration: " +
                            Convert.ToString(Math.Round(Convert.ToDecimal(dgvChronological.Rows[i].Cells[department_time].Value) / 60, 2)) + " " +
                            " ( " + Convert.ToString(Math.Round(Math.Round(Convert.ToDecimal(dgvChronological.Rows[i].Cells[department_time].Value) / 60, 2) * 60, 0)) +
                            " mins) >>";
                    }
                    else if (dgvChronological.Rows[i].Cells[action].Value.ToString().Contains("Live") == true)
                    {
                        dgvChronological.Rows[i].Cells[status].Value = dgvChronological.Rows[i].Cells[action].Value.ToString() + " - Duration: " +
                            Convert.ToString(Math.Round(Convert.ToDecimal(dgvChronological.Rows[i].Cells[department_time].Value) / 60, 2)) +
                            " ( " + Convert.ToString(Math.Round(Math.Round(Convert.ToDecimal(dgvChronological.Rows[i].Cells[department_time].Value) / 60, 2) * 60, 0)) +
                            " mins) >>";
                    }
                    else if (dgvChronological.Rows[i].Cells[action].Value.ToString() != "Finish Part")
                    {
                        dgvChronological.Rows[i].Cells[status].Value = dgvChronological.Rows[i].Cells[action].Value.ToString() + " >> ";
                    }

                    //else if (dgvChronological.Rows[i].Cells[action].Value.ToString().Contains("Door Allocated"))
                    //{
                    //    dgvChronological.Rows[i].Cells[actionIndex].Value = dgvChronological.Rows[i].Cells[action].Value = "Door Allocated";
                    //}
                    //while we are here also remove the date from the actiontime
                    DateTime tempDate = Convert.ToDateTime(dgvChronological.Rows[i].Cells[action_time].Value);
                    dgvChronological.Rows[i].Cells[time].Value = tempDate.ToString("dd/MM/yyyy HH:mm");
                }

                //formating
                //hide the columns we do not need
                dgvChronological.Columns[action_date].Visible = false;
                dgvChronological.Columns[action_time].Visible = false;
                dgvChronological.Columns[fullname].Visible = false;
                dgvChronological.Columns[sort_order].Visible = false;
                dgvChronological.Columns[department].Visible = false;
                dgvChronological.Columns[department_time].Visible = false;
                dgvChronological.Columns[predicted_end].Visible = false;
                dgvChronological.Columns[note].Visible = false;

                dgvChronological.Columns[time].DisplayIndex = 3;
                dgvChronological.Columns[door_id].DisplayIndex = status + 1; //1st column
                dgvChronological.Columns[door_type].DisplayIndex = status + 2; //2nd etc...
                dgvChronological.Columns[action].DisplayIndex = status + 3;
                dgvChronological.Columns[part].DisplayIndex = status + 4;
                dgvChronological.Columns[part_time].DisplayIndex = status + 5;
                if (dgvChronological.Columns["part_time_minutes"] != null)
                    dgvChronological.Columns[part_time_minutes].DisplayIndex = status + 6;

                //sizeeeees
                dgvChronological.Columns[status].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[door_type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[door_id].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[action].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[part].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[part_time].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[part_time_minutes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvChronological.Columns[time].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //headertext
                dgvChronological.Columns[status].HeaderText = "Status";
                dgvChronological.Columns[door_type].HeaderText = "Door Type";
                dgvChronological.Columns[door_id].HeaderText = "Door ID";
                dgvChronological.Columns[action].HeaderText = "Action";
                dgvChronological.Columns[part].HeaderText = "Part";
                dgvChronological.Columns[part_time].HeaderText = "Time For Part";
                dgvChronological.Columns[part_time_minutes].HeaderText = "Time For Part (mins)";
                dgvChronological.Columns[time].HeaderText = "Time";

                //messing with the colours
                List<string> door_list = new List<string>();
                for (int i = 0; i < dgvChronological.Rows.Count; i++)
                {
                    if (dgvChronological.Rows[i].Cells[action].Value.ToString().Contains("Finish")) //mark complete jobs as green
                        dgvChronological.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    if (dgvChronological.Rows[i].Cells[action].Value.ToString().Contains("Door Complete")) //mark complete doors as green
                    {
                        dgvChronological.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        //grab the door id for this too
                        door_list.Add(dgvChronological.Rows[i].Cells[door_id].Value.ToString());

                    }
                    if (dgvChronological.Rows[i].Cells[action].Value.ToString().Contains("Door Start")) //mark started doors as green
                        dgvChronological.Rows[i].DefaultCellStyle.BackColor = Color.CornflowerBlue;

                    if (dgvChronological.Rows[i].Cells["Status"].Value.ToString().Contains("Door Complete >>"))
                    {

                        //while we are here stick the value in this cell
                        string value = "select '£' + format(line_total_no_install,'n0') FROM dbo.view_door_value where id = " + dgvChronological.Rows[i].Cells[door_id].Value.ToString();
                        using (SqlConnection conn = new SqlConnection(SqlStatements.ConnectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(value, conn))
                                value = cmd.ExecuteScalar().ToString();

                            dgvChronological.Rows[i].Cells["Status"].Value = dgvChronological.Rows[i].Cells["Status"].Value.ToString() + " Value: " + value;
                            conn.Close();
                        }
                    }
                }
                string sql = "";
                for (int i = 0; i < dgvChronological.Rows.Count; i++)
                {
                    sql = "select * from dbo.door_stoppages right join(select MAX(id) as id,MAX(door_id) as door_id from dbo.door_stoppages group by door_id,department) a on a.id = door_stoppages.id " +
                        "where [action] = 'Paused' AND department = '" + _department + "' AND dbo.door_stoppages.door_id = " + dgvChronological.Rows[i].Cells[door_id].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(SqlStatements.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            var temp = cmd.ExecuteScalar();
                            if (temp != null)
                            {
                                if (dgvChronological.Rows[i].Cells[action].Value.ToString().Contains("Door Start"))
                                {
                                    dgvChronological.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                                }
                            }
                            conn.Close();
                        }
                    }
                }
                //now loop through it again - checking each door_id for a match and make it green
                for (int i = 0; i < dgvChronological.Rows.Count; i++)
                {
                    if (door_list.Contains(dgvChronological.Rows[i].Cells[door_id].Value.ToString()))
                    {
                        dgvChronological.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                    //note = yellow is the final colour change to make
                    if (/*dgvChronological.Rows[i].Cells[actionIndex].Value.ToString().Contains("Paused") &&*/ dgvChronological.Rows[i].Cells[note].Value.ToString().Length > 0)
                        dgvChronological.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            
            dgvChronological.ClearSelection();
        }

        private void frmChronological_Shown(object sender, EventArgs e)
        {
            load_dgv();
        }

        private void dgvChronological_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChronological.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Yellow)
            {
                MessageBox.Show(dgvChronological.Rows[e.RowIndex].Cells["note"].Value.ToString());
            }
        }

        private void dteActionEnd_CloseUp(object sender, EventArgs e)
        {
            load_dgv();
        }

        private void dteAction_CloseUp(object sender, EventArgs e)
        {
            load_dgv();
        }
    }
}

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
    public partial class frmChronological : Form
    {

        public int actionIndex { get; set; }
        public frmChronological(string staff, string dept, DateTime plannerDate)
        {
            InitializeComponent();

            int staff_id = 0;
            //MessageBox.Show(staff);
            staff_id = staff.IndexOf(" ", staff.IndexOf(" ") + 1); //staff id is a temp int var here
            staff = staff.Substring(0, staff_id);
            //MessageBox.Show(staff);
            label1.Text = staff;
            //grab the staff name
            using (SqlConnection conn2 = new SqlConnection(connectionStrings.ConnectionStringUser))
            {
                string sql = "SELECT id FROM dbo.[user] WHERE forename + ' ' + surname = '" + staff + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn2))
                {
                    conn2.Open();
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());
                    conn2.Close();
                }
            }

            //usp_power_planner_chronological_shop_actions
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_power_planner_chronological_shop_actions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_time", SqlDbType.Date).Value = plannerDate;
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

        private void REarrange()
        {


            //ok one of the harder things to do here is to get the columns to merge like they do in the report :{
            //first step lets grab all of the indexes of each column we have 
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            int status, action, part, part_time, action_time, action_date, fullname, door_id, sort_order, department, department_time, predicted_end, note, door_type, time;
            status = dataGridView1.Columns["status"].Index;
            action = dataGridView1.Columns["action"].Index;
            actionIndex = action;
            part = dataGridView1.Columns["part"].Index;
            part_time = dataGridView1.Columns["part_time"].Index;
            action_time = dataGridView1.Columns["action_time"].Index;
            action_date = dataGridView1.Columns["action_date"].Index;
            fullname = dataGridView1.Columns["fullname"].Index;
            door_id = dataGridView1.Columns["door_id"].Index;
            sort_order = dataGridView1.Columns["sort_order"].Index;
            department = dataGridView1.Columns["department"].Index;
            department_time = dataGridView1.Columns["department_time"].Index;
            predicted_end = dataGridView1.Columns["predicted_end"].Index;
            note = dataGridView1.Columns["note"].Index;
            door_type = dataGridView1.Columns["door_type"].Index;
            time = dataGridView1.Columns["Time"].Index;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //start messing with the columns :}


            //first column :- =IIf([Action]="Door Start",[Action] & " - Duration: " & Round(([department_time]/60),2) & " >>",IIf([Action]<>"Finish Part",[Action] & " >>",""))
            //gonna need to loop through all the records for this one

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[action].Value.ToString() == "Door Start")
                {
                    dataGridView1.Rows[i].Cells[status].Value = dataGridView1.Rows[i].Cells[action].Value.ToString() + " - Duration: " +
                        Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView1.Rows[i].Cells[department_time].Value) / 60, 2)) + " >>";
                }
                else if (dataGridView1.Rows[i].Cells[action].Value.ToString() != "Finish Part")
                {
                    dataGridView1.Rows[i].Cells[status].Value = dataGridView1.Rows[i].Cells[action].Value.ToString() + " >> ";
                }
                //while we are here also remove the date from the actiontime
                DateTime tempDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[action_time].Value);
                dataGridView1.Rows[i].Cells[time].Value = tempDate.ToString("HH:mm");
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

            //sizeeeees
            dataGridView1.Columns[status].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[door_type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[door_id].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[action].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[part].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[part_time].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[time].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //headertext
            dataGridView1.Columns[status].HeaderText = "Status";
            dataGridView1.Columns[door_type].HeaderText = "Door Type";
            dataGridView1.Columns[door_id].HeaderText = "Door ID";
            dataGridView1.Columns[action].HeaderText = "Action";
            dataGridView1.Columns[part].HeaderText = "Part";
            dataGridView1.Columns[part_time].HeaderText = "Time For Part";
            dataGridView1.Columns[time].HeaderText = "Time";

            //messing with the colours
        }

        private void frmChronological_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[actionIndex].Value.ToString().Contains("Finish"))
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            }
            dataGridView1.ClearSelection();
        }
    }
}

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

    public partial class frmCovidInput : Form
    {
        public int allowClose { get; set; }
        public int dateid { get; set; }
        public int Shopfloor { get; set; }
        public DateTime placementDate   { get; set; }
        public DateTime lastDate { get; set; }
        public frmCovidInput(DateTime _placementDate, int _Shopfloor)
        {
            InitializeComponent();
            string sql = "test";
            allowClose = -1;
            Shopfloor = _Shopfloor;
            dateTimePicker1.Value = _placementDate;
            lastDate = _placementDate; //incase they swap then we still need to save the data
            loadData();
        }

        private void loadData()
        {
            string sql = "test";
            placementDate = dateTimePicker1.Value;
            SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString);
            conn.Open();

            if (Shopfloor == -1)
            {
                sql = "usp_power_planner_covid_load_staff";
                SqlCommand cmdDate = new SqlCommand("usp_power_planner_covid_load_staff", conn);
                cmdDate.CommandType = CommandType.StoredProcedure;
                cmdDate.Parameters.AddWithValue("@date", SqlDbType.Date).Value = placementDate;
                cmdDate.Parameters.AddWithValue("@shopfloor", SqlDbType.Date).Value = Shopfloor; //identifies if its shopfloor or office 

                SqlDataAdapter da = new SqlDataAdapter(cmdDate); //shopfloor is filtered by sortOrder which is the extra column in #covidtemps or whatever its called 
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                // if we're here then its because its the office 
                label25.Text = "Office";
                sql = "usp_power_planner_covid_load_staff";
                SqlCommand cmdDate = new SqlCommand("usp_power_planner_covid_load_staff", conn);
                cmdDate.CommandType = CommandType.StoredProcedure;
                cmdDate.Parameters.AddWithValue("@date", SqlDbType.Date).Value = placementDate;
                cmdDate.Parameters.AddWithValue("@shopfloor", SqlDbType.Date).Value = Shopfloor;

                SqlDataAdapter da = new SqlDataAdapter(cmdDate);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            sql = "SELECT id from dbo.power_plan_date where date_plan = '" + placementDate.ToString("yyyy-MM-dd") + "'";
            using (SqlCommand cmd2 = new SqlCommand(sql, conn))
            {
                dateid = Convert.ToInt32(cmd2.ExecuteScalar());
            }
            //after getting each person we  need to loop through them and see if any of them already have temps stored
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sql = "SELECT temp_morning FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid + "";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    row.Cells[2].Value = Convert.ToString(cmd.ExecuteScalar());

                }
                //sql = "SELECT temp_afternoon FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid + "";
                //using (SqlCommand cmd = new SqlCommand(sql, conn))
                //{

                //    row.Cells[5].Value = Convert.ToString(cmd.ExecuteScalar());

                //}
                sql = "SELECT mask  FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid + "";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    row.Cells[6].Value = Convert.ToString(cmd.ExecuteScalar());

                }
                sql = "SELECT ear_protection FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid + "";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    row.Cells[7].Value = Convert.ToString(cmd.ExecuteScalar());
                }
                sql = "SELECT DISTINCT vax_date_1  FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string temp = Convert.ToString(cmd.ExecuteScalar());
                    if (string.IsNullOrWhiteSpace(temp))
                    {
                        sql = "SELECT DISTINCT vax_date_1 FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                        using (SqlCommand cmd2 = new SqlCommand(sql, conn))
                            temp = Convert.ToString(cmd.ExecuteScalar());
                        if (string.IsNullOrWhiteSpace(temp))
                            temp = "";
                    }
                    if (temp.Length > 10)
                        temp = temp.Substring(0, 10);
                    row.Cells[8].Value = temp;
                }
                sql = "SELECT DISTINCT vax_date_2  FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string temp = Convert.ToString(cmd.ExecuteScalar());
                    if (string.IsNullOrWhiteSpace(temp))
                    {
                        sql = "SELECT DISTINCT vax_date_2  FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                        using (SqlCommand cmd2 = new SqlCommand(sql, conn))
                            temp = Convert.ToString(cmd.ExecuteScalar());
                        if (string.IsNullOrWhiteSpace(temp))
                            temp = "";
                    }
                    if (temp.Length > 10)
                        temp = temp.Substring(0, 10);
                    row.Cells[9].Value = temp;
                }
                //note
                sql = "SELECT note FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid + "";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    row.Cells[10].Value = Convert.ToString(cmd.ExecuteScalar());
                }

                colour();
            }



            conn.Close();

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            allowClose = 0;
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 2) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
        }
        //also need this void to prevent letters etc
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //"power_planner_covid_temps"
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int hasRecord = 0;  // check if there is an entry to update or insert
                    using (SqlCommand cmdRecord = new SqlCommand("SELECT id FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid, conn))
                    {
                        var getData = cmdRecord.ExecuteScalar();
                        if (getData != null)
                            hasRecord = -1;
                        else
                            hasRecord = 0;
                    }
                    if (row.Cells[2].Value.ToString() != "")
                    {
                        string sql = "";
                        if (hasRecord == -1)
                            sql = "UPDATE dbo.power_plan_covid_temps SET temp_morning = " + row.Cells[2].Value.ToString() + " WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                        {
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,temp_morning) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", " + row.Cells[2].Value.ToString() + ")";
                            hasRecord = -1;
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    //same again but for afternoon temp
                    //if (row.Cells[5].Value.ToString() != "")
                    //{
                    //    string sql = "";
                    //    if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                    //        sql = "UPDATE dbo.power_plan_covid_temps SET temp_afternoon = " + row.Cells[5].Value.ToString() + " WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                    //    else
                    //        sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,temp_afternoon) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", " + row.Cells[5].Value.ToString() + ")";
                    //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    //    {
                    //        cmd.ExecuteNonQuery();
                    //        row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    //    }
                    //}

                    //masks/ear protection
                    if (row.Cells[6].Value.ToString() != "")
                    {
                        string sql = "";
                        if (hasRecord == -1)
                            sql = "UPDATE dbo.power_plan_covid_temps SET mask = '" + row.Cells[6].Value.ToString() + "' WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                        {
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,mask) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", '" + row.Cells[6].Value.ToString() + "')";
                            hasRecord = -1;
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    if (row.Cells[7].Value.ToString() != "")
                    {
                        string sql = "";
                        if (hasRecord == -1)
                            sql = "UPDATE dbo.power_plan_covid_temps SET ear_protection = '" + row.Cells[7].Value.ToString() + "' WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                        {
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,ear_protection) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", '" + row.Cells[7].Value.ToString() + "')";
                            hasRecord = -1;
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    //check if they exist in the new vax table
                    if (row.Cells[8].Value.ToString() != "")
                    {
                        string sql = "";
                        string temp = "SELECT staff_id FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                        using (SqlCommand cmd = new SqlCommand(temp, conn))
                            temp = Convert.ToString(cmd.ExecuteScalar());
                        if (string.IsNullOrWhiteSpace(temp) == false)
                            sql = "UPDATE dbo.power_plan_covid_vax_dates SET vax_date_1 = '" + row.Cells[8].Value.ToString() + "' WHERE  staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_vax_dates (staff_id,vax_date_1) VALUES(" + row.Cells[3].Value.ToString() + ",'" + row.Cells[8].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }

                    //check if they exist in the new vax table
                    if (row.Cells[9].Value.ToString() != "")
                    {
                       string sql = "";
                        string temp = "SELECT staff_id FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                        using (SqlCommand cmd = new SqlCommand(temp, conn))
                            temp = Convert.ToString(cmd.ExecuteScalar());
                        if (string.IsNullOrWhiteSpace(temp) == false)
                            sql = "UPDATE dbo.power_plan_covid_vax_dates SET vax_date_2 = '" + row.Cells[9].Value.ToString() + "' WHERE  staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_vax_dates (staff_id,vax_date_2) VALUES(" + row.Cells[3].Value.ToString() + ",'" + row.Cells[9].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }

                    }
                    if (row.Cells[10].Value.ToString() != "")
                    {
                        string sql = "";
                        if (hasRecord == -1)
                            sql = "UPDATE dbo.power_plan_covid_temps SET note = '" + row.Cells[10].Value.ToString() + "' WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                        {
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,note) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", '" + row.Cells[10].Value.ToString() + "')";
                            hasRecord = -1;
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.Cells[10].Style.BackColor = Color.Empty;
                        }
                    }

                }
                conn.Close();
            }
            allowClose = -1;
            this.Close();
        }

        private void frmCovidInput_Shown(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString().Length > 0)
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                if (row.Cells[5].Value.ToString().Length > 0)
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                if (row.Cells[6].Value.ToString().Contains("y") || row.Cells[6].Value.ToString().Contains("Y")) //because theres no 'offical' way to do this so far, do a like on y and if it matches then green else assume its a no and make it red¬ :D
                    row.Cells[6].Style.BackColor = Color.DarkSeaGreen;
                else
                    row.Cells[6].Style.BackColor = Color.PaleVioletRed;

                if (row.Cells[7].Value.ToString().Contains("y") || row.Cells[7].Value.ToString().Contains("Y")) //same as above but for ear protections not masks 
                    row.Cells[7].Style.BackColor = Color.DarkSeaGreen;
                else
                    row.Cells[7].Style.BackColor = Color.PaleVioletRed;

                row.Cells[10].Style.BackColor = Color.Empty;
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //y/n looks better in the middle  
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ClearSelection();
        }

        private void colour()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString().Length > 0)
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                if (row.Cells[5].Value.ToString().Length > 0)
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                if (row.Cells[6].Value.ToString().Contains("y") || row.Cells[6].Value.ToString().Contains("Y")) //because theres no 'offical' way to do this so far, do a like on y and if it matches then green else assume its a no and make it red¬ :D
                    row.Cells[6].Style.BackColor = Color.DarkSeaGreen;
                else
                    row.Cells[6].Style.BackColor = Color.PaleVioletRed;

                if (row.Cells[7].Value.ToString().Contains("y") || row.Cells[7].Value.ToString().Contains("Y")) //same as above but for ear protections not masks 
                    row.Cells[7].Style.BackColor = Color.DarkSeaGreen;
                else
                    row.Cells[7].Style.BackColor = Color.PaleVioletRed;

                row.Cells[10].Style.BackColor = Color.Empty;
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //y/n looks better in the middle  
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ClearSelection();
        }

        private void frmCovidInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowClose == 0)
            {
                DialogResult result = MessageBox.Show("You have not uploaded your changes, are you sure you want to exit?", "Unsaved Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    allowClose = -1;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //before  we load the data we need to commit to the database
            //basically the same as normal upload but we change the dateid around
            //"power_planner_covid_temps"
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                string sql = "SELECT id from dbo.power_plan_date where date_plan = '" + lastDate.ToString("yyyy-MM-dd") + "'";
                using (SqlCommand cmd2 = new SqlCommand(sql, conn))
                {
                    dateid = Convert.ToInt32(cmd2.ExecuteScalar());
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[2].Value.ToString() != "")
                    {
                         sql = "";
                        if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                            sql = "UPDATE dbo.power_plan_covid_temps SET temp_morning = " + row.Cells[2].Value.ToString() + " WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,temp_morning) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", " + row.Cells[2].Value.ToString() + ")";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    //same again but for afternoon temp
                    //if (row.Cells[5].Value.ToString() != "")
                    //{
                    //    string sql = "";
                    //    if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                    //        sql = "UPDATE dbo.power_plan_covid_temps SET temp_afternoon = " + row.Cells[5].Value.ToString() + " WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                    //    else
                    //        sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,temp_afternoon) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", " + row.Cells[5].Value.ToString() + ")";
                    //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    //    {
                    //        cmd.ExecuteNonQuery();
                    //        row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    //    }
                    //}

                    //masks/ear protection
                    if (row.Cells[6].Value.ToString() != "")
                    {
                         sql = "";
                        if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                            sql = "UPDATE dbo.power_plan_covid_temps SET mask = '" + row.Cells[6].Value.ToString() + "' WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,mask) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", '" + row.Cells[6].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    if (row.Cells[7].Value.ToString() != "")
                    {
                         sql = "";
                        if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                            sql = "UPDATE dbo.power_plan_covid_temps SET ear_protection = '" + row.Cells[7].Value.ToString() + "' WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,ear_protection) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", '" + row.Cells[7].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    //check if they exist in the new vax table
                    if (row.Cells[8].Value.ToString() != "")
                    {
                         sql = "";
                        string temp = "SELECT staff_id FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                        using (SqlCommand cmd = new SqlCommand(temp, conn))
                            temp = Convert.ToString(cmd.ExecuteScalar());
                        if (string.IsNullOrWhiteSpace(temp) == false)
                            sql = "UPDATE dbo.power_plan_covid_vax_dates SET vax_date_1 = '" + row.Cells[8].Value.ToString() + "' WHERE  staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_vax_dates (staff_id,vax_date_1) VALUES(" + row.Cells[3].Value.ToString() + ",'" + row.Cells[8].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }

                    //check if they exist in the new vax table
                    if (row.Cells[9].Value.ToString() != "")
                    {
                         sql = "";
                        string temp = "SELECT staff_id FROM dbo.power_plan_covid_vax_dates WHERE staff_id = " + row.Cells[3].Value.ToString();
                        using (SqlCommand cmd = new SqlCommand(temp, conn))
                            temp = Convert.ToString(cmd.ExecuteScalar());
                        if (string.IsNullOrWhiteSpace(temp) == false)
                            sql = "UPDATE dbo.power_plan_covid_vax_dates SET vax_date_2 = '" + row.Cells[9].Value.ToString() + "' WHERE  staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_vax_dates (staff_id,vax_date_2) VALUES(" + row.Cells[3].Value.ToString() + ",'" + row.Cells[9].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }

                    }
                    if (row.Cells[10].Value.ToString() != "")
                    {
                         sql = "";
                        if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                            sql = "UPDATE dbo.power_plan_covid_temps SET note = '" + row.Cells[10].Value.ToString() + "' WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,note) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", '" + row.Cells[10].Value.ToString() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.Cells[10].Style.BackColor = Color.Empty;
                        }
                    }

                }
                conn.Close();
            }
            lastDate = dateTimePicker1.Value;
            loadData();
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[6].Value = "y";
                row.Cells[7].Value = "y";
                allowClose = 0;
            }
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[6].Value = "n";
                row.Cells[7].Value = "n";
                allowClose = 0;
            }
        }
    }
}


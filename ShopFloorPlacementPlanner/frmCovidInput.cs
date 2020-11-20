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
        public frmCovidInput(DateTime placementDate, int Shopfloor)
        {
            InitializeComponent();
            string sql = "test";
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
                sql = "SELECT temp_afternoon FROM dbo.power_plan_covid_temps WHERE staff_id = " + row.Cells[3].Value.ToString() + " AND date_id = " + dateid + "";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    row.Cells[5].Value = Convert.ToString(cmd.ExecuteScalar());

                }
            }



            conn.Close();

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
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
                    if (row.Cells[2].Value.ToString() != "")
                    {
                        string sql = "";
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
                    if (row.Cells[5].Value.ToString() != "")
                    {
                        string sql = "";
                        if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                            sql = "UPDATE dbo.power_plan_covid_temps SET temp_afternoon = " + row.Cells[5].Value.ToString() + " WHERE date_id = " + dateid + " AND staff_id = " + row.Cells[3].Value.ToString();
                        else
                            sql = "INSERT INTO dbo.power_plan_covid_temps (staff_id,date_id,temp_afternoon) VALUES(" + row.Cells[3].Value.ToString() + "," + dateid + ", " + row.Cells[5].Value.ToString() + ")";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                }
                conn.Close();
            }
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
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.ClearSelection();
        }

        private void frmCovidInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowClose == 0)
            {
                allowClose = -1;
                btnGo.PerformClick();
            }
        }
    }
}


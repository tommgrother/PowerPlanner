using DocumentFormat.OpenXml.Wordprocessing;
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

namespace ShopFloorPlacementPlanner
{
    public partial class frmTimeInMotion : Form
    {
        public int staff_id { get; set; }
        public string department { get; set; }
        public frmTimeInMotion(DateTime startDate, DateTime endDate, int staff_id, string dept, string staff_name)
        {
            InitializeComponent();

            department = dept;
            dteStartDate.Value = startDate;
            dteEndDate.Value = endDate;
            this.staff_id = staff_id;
            chkTimed.Checked = true;
            load_data();
            lblTitle.Text = staff_name;

        }

        private void load_data()
        {
            //--[dbo].[WorkTime] @StartDate DATETIME, @FinishDate DATETIME
            //--weld_tm,buff_tm,pack_tm
            string short_dept = "";
            switch (department)
            {
                case "Buffing":
                    short_dept = "buff";
                    break;
                case "Welding":
                    short_dept = "weld";
                    break;
                case "Packing":
                    short_dept = "pack";
                    break;
            }



            string sql = "select d.id,dt.door_type_description,started_" + short_dept + ",date_" + short_dept + "_complete," +
                "round(cast(dbo.WorkTime(started_" + short_dept + ",date_" + short_dept + "_complete) as float)  / 60,2) as time_in_motion," +
                "round(CAST(time_" + short_dept + " as float) / 60,2) as time_allowed," +
                "CASE WHEN dt." + short_dept + "_tm = -1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS timed " +
                "FROM dbo.door d " +
                "left join dbo.door_type dt on d.door_type_id = dt.id " +
                "left join dbo.door_allocation da on d.id = da.door_id " +
                "where department = '" + department.Replace("Buffing","Dressing") + "' and da.staff_id = " + staff_id + " and " +
                "started_" + short_dept + " > '" + dteStartDate.Value.ToString("yyyyMMdd") + "' " +
                "and date_" + short_dept + "_complete < '" + dteEndDate.Value.ToString("yyyyMMdd") + "' and " +
                "complete_" + short_dept + " = 1 and " +
                "d.id not in (select door_id FROM door_stoppages where door_id = d.id and department = '" + department.Replace("Buffing", "Dressing") + "') ";

            if (chkNotTimed.Checked == true && chkTimed.Checked != true)
                sql += "and (dt." + short_dept + "_tm = 0 or dt." + short_dept + "_tm is null) ";
            else if (chkNotTimed.Checked != true && chkTimed.Checked == true)
                sql += "and (dt." + short_dept + "_tm = -1) ";

            sql += "ORDER BY started_" + short_dept + " asc";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

                conn.Close();
            }
            format();
            paint();
            dataGridView1.ClearSelection();
        }

        private void paint()
        {
            for (int i = 0; i <  dataGridView1.Rows.Count; i++)
            {
                double time_taken = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
                double time_allowed = Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                
                if (time_taken > time_allowed)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.PaleVioletRed;
                else
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightSeaGreen;


            }
        }

        private void format()
        {
            dataGridView1.Columns[0].HeaderText = "Door ID";
            dataGridView1.Columns[1].HeaderText = "Door Type";
            dataGridView1.Columns[2].HeaderText = "Operation Started";
            dataGridView1.Columns[3].HeaderText = "Operation Finished";
            dataGridView1.Columns[4].HeaderText = "Time in Motion";
            dataGridView1.Columns[5].HeaderText = "Time Allowed";
            dataGridView1.Columns[6].HeaderText = "Timed";

            this.dataGridView1.Columns[4].DefaultCellStyle.Format = "0.00##";
            this.dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns[5].DefaultCellStyle.Format = "0.00##";
            this.dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells ;
            }

            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dteStartDate_CloseUp(object sender, EventArgs e)
        {
            load_data();
        }

        private void dteEndDate_CloseUp(object sender, EventArgs e)
        {
            load_data();
        }

        private void frmTimeInMotion_Shown(object sender, EventArgs e)
        {
            paint();
        }

        private void chkTimed_CheckedChanged(object sender, EventArgs e)
        {
            load_data();
        }

        private void chkNotTimed_CheckedChanged(object sender, EventArgs e)
        {
            load_data();
        }
    }
}

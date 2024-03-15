
using DocumentFormat.OpenXml.Office.Word;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class frmAllocatedSetWorked : Form
    {
        public string dept_short { get; set; }
        public frmAllocatedSetWorked()
        {
            InitializeComponent();

            load_grid();
        }

        private void load_grid()
        {

            switch (cmbDepartment.Text)
            {
                case "Bending":
                    dept_short = "bend";
                    break;
                case "Welding":
                    dept_short = "welding";
                    break;
                case "Buffing":
                    dept_short = "buff";
                    break;
                case "Packing":
                    dept_short = "Pack";
                    break;
            }


            if (dataGridView1.Columns.Contains("Allocated") == true)
            {
                dataGridView1.Columns.Remove("Allocated");
            }
            if (dataGridView1.Columns.Contains("Worked") == true)
            {
                dataGridView1.Columns.Remove("Worked");
            }



            string sql = "";
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                sql = "select u.id,forename + ' ' + surname as [Staff Name], s.hours as [Set Hours]  FROM dbo.power_plan_staff s " +
                "left join dbo.power_plan_date d on s.date_id = d.id " +
                "left join[user_info].dbo.[user] u on s.staff_id = u.id " +
                "where s.department = '" + cmbDepartment.Text + "' and date_plan = '" + dteDate.Value.ToString("yyyyMMdd") + "'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

                //get the allocated work
                dataGridView1.Columns.Add("Allocated", "Allocated");
                dataGridView1.Columns.Add("Worked", "Worked");

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string staff_id = dataGridView1.Rows[i].Cells[0].Value.ToString();

                    //allocated
                    sql = "SELECT sum(allocated) as allocated FROM (select round(cast(sum(time_remaining_pack * quantity_same) as float) /60,2) as allocated " +
                        "from dbo.door_allocation da left join dbo.door d on da.door_id = d.id " +
                        "where (complete_" + dept_short + " = 0 or complete_" + dept_short + " is null) " +
                        "AND da.department = '" + cmbDepartment.Text + "' and " +
                        "(status_id = 1 or status_id = 2) and time_remaining_pack > 0 " +
                        "and staff_id  = " + staff_id + " " +
                        "group by staff_id,da.door_id) as a ";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                        dataGridView1.Rows[i].Cells[3].Value = (cmd.ExecuteScalar().ToString()) ;


                    //worked
                    sql = "SELECT ROUND((SUM(time_for_part) / 60),2) as worked " +
                        "FROM dbo.view_worked_hours " +
                        "WHERE staff_id = " + staff_id.ToString() + " AND " +
                        "CAST(part_complete_date as DATE) = '" + dteDate.Value.ToString("yyyyMMdd") + "' " +
                        "AND part_status = 'Complete' AND " +
                        "op = '" + cmbDepartment.Text + "' GROUP BY staff_id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                            dataGridView1.Rows[i].Cells[4].Value = Convert.ToDecimal(cmd.ExecuteScalar().ToString());
                        else
                            dataGridView1.Rows[i].Cells[4].Value = 0.0;
                    }
                }






                conn.Close();
            }
        }

        private void dteDate_CloseUp(object sender, EventArgs e)
        {
            load_grid();
        }

        //SqlCommand cmdryucxd = new SqlCommand("usp_power_planner_worked_hours", conn);
        //cmdryucxd.CommandType = CommandType.StoredProcedure;
        //cmdryucxd.Parameters.AddWithValue("@department", SqlDbType.Date).Value = "Packing";
        //cmdryucxd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = dteDateSelection.Text;

        //var dataReader = cmdryucxd.ExecuteReader();
        //// SqlDataAdapter da2 = new SqlDataAdapter(cmdryucxd);
        //DataTable workedHours = new DataTable();
        //workedHours.Load(dataReader);
        ////da2.Fill(workedHours);

        //overtime -- usp_power_planner_overtime_hours
        //string allocated = "";
        //try
        //{
        //    sql = "SELECT sum(hours) FROM (select round(cast(sum(time_remaining_pack * quantity_same) as float) /60,2) as hours from dbo.door_allocation da " +
        //        "left join dbo.door d on da.door_id = d.id where (complete_pack = 0 or complete_pack is null) AND da.department = 'packing' and (status_id = 1 or status_id = 2) and time_remaining_pack > 0 and staff_id  = " + dtStaffID.Rows[i][0].ToString() +
        //        "group by staff_id,da.door_id) as a";
        //    using (SqlCommand cmdAllocated = new SqlCommand(sql, conn))
        //    {
        //        allocated = (string)cmdAllocated.ExecuteScalar().ToString();
        //        if (allocated == "")
        //            allocated = "0";
        //    }
        //}
        //catch
        //{
        //    allocated = "0";
        //}
        //double overtimeTemp = Convert.ToDouble(dgPack.Rows[i].Cells[6].Value) * 0.8;
        //hours = Convert.ToString(Convert.ToDecimal(dgPack.Rows[i].Cells[1].Value) + Convert.ToDecimal(overtimeTemp));   // dgPack.Rows[i].Cells[1].Value.ToString();
        //       // worked = dgPack.Rows[i].Cells[3].Value.ToString();
        //        dgPack[4, i].Value = hours /*+ " / " + worked */ + Environment.NewLine + "£" + packValue.Rows[0][i].ToString();// + " " + Environment.NewLine + "" + allocated + " Allo";

    }
}

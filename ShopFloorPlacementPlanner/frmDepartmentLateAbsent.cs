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
    public partial class frmDepartmentLateAbsent : Form
    {
        public int staff_id { get; set; }
        public frmDepartmentLateAbsent()
        {
            InitializeComponent();

            string sql = "SELECT forename + ' ' +surname as staff from [user_info].dbo.[user] WHERE [current] =  1 and (non_user = 0 or non_user is null) order by forename "; //test aaa

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        cmbStaff.Items.Add(row[0].ToString());
                    }
                }
                conn.Close();
            }
            //get the start and end of current month
            dteStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dteEnd.Value = (dteStart.Value.AddMonths(1).AddDays(-1));
            apply_filter();

        }

        private void apply_filter()
        {



            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                staff_id = 0;
                string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + cmbStaff.Text + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());


                //absent total
                sql = "select  coalesce(sum(1),0) as [Total Absent] from dbo.absent_holidays " +
                         "left join[user_info].dbo.[user] u on u.id = staff_id " +
                         "where(absent_type = 5 or absent_type = 8) AND " +
                         "staff_id = " + staff_id.ToString() +
                         " AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    lblAbsent.Text = "Total Absent Days: " + cmd.ExecuteScalar().ToString();
                }

                //late total
                sql = "select  coalesce(sum(1),0) as [ Total Late] from dbo.absent_holidays " +
                        "left join[user_info].dbo.[user] u on u.id = staff_id " +
                        "where(absent_type = 7) AND " +
                        "staff_id = " + staff_id.ToString() +
                        "AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    lblLate.Text = "Total Late Days: " + cmd.ExecuteScalar().ToString();

                //absences
                sql = "select  Convert(char,date_absent,103)  as [Absent Date],datename(WEEKDAY,date_absent) as [Day of Week],sum(1) [Absent] from dbo.absent_holidays " +
                    "left join[user_info].dbo.[user] u on u.id = staff_id " +
                    "where(absent_type = 5 or absent_type = 8) AND " +
                    "staff_id =  " + staff_id.ToString() +
                    " AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                    "group by date_absent";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAbsent.DataSource = dt;
                }
                //lates 
                sql = "select  Convert(char,date_absent,103)  as [Late Date],datename(WEEKDAY,date_absent) as [Day of Week],sum(1) [Late] from dbo.absent_holidays " +
                    "left join[user_info].dbo.[user] u on u.id = staff_id " +
                    "where(absent_type = 7) AND " +
                    "staff_id =  " + staff_id.ToString() +
                    "AND date_absent >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND date_absent <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                    "group by date_absent";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvLate.DataSource = dt;
                }

                //performance data
                //look up shopfloor or office
                //sql = "select coalesce(ShopFloor,0),coalesce(isEngineer,0),coalesce(isEstimator,0),coalesce(slimline,0) from [user_info].dbo.[user] where id = " + staff_id.ToString();
                //using (SqlCommand cmd = new SqlCommand(sql, conn))
                //{
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    //dgvLate.DataSource = dt;

                //    try
                //    {
                //        shopfloor = Convert.ToInt32(dt.Rows[0][0].ToString());
                //        engineer = Convert.ToInt32(dt.Rows[0][1].ToString());
                //        estimator = Convert.ToInt32(dt.Rows[0][2].ToString());
                //        slimline = Convert.ToInt32(dt.Rows[0][3].ToString());
                //    }
                //    catch
                //    {
                //        shopfloor = 0;
                //        engineer = 0;
                //        estimator = 0;
                //    }
                //}
                //if (shopfloor == -1)
                //{
                //    sql = "select cast(d.date_plan as date) as date_plan,datename(WEEKDAY,date_plan) as day_of_week,round(cast([hours] as float) + coalesce((ot.overtime * 0.8),0),2) as [set_hours]," +
                //        "COALESCE(worked.worked_hours,0) as worked_hours from dbo.power_plan_staff s " +
                //        "left join dbo.power_plan_date d on s.date_id = d.id " +
                //        "left join dbo.power_plan_overtime_remake ot on s.date_id = ot.date_id AND s.staff_id = ot.staff_id " +
                //        "left join (SELECT CAST(part_complete_date as date) as [date],ROUND((SUM(time_for_part) / 60), 2) as [worked_hours],staff_id FROM dbo.door_part_completion_log  " +
                //        "WHERE staff_id = " + staff_id.ToString() + " AND CAST(part_complete_date as DATE) >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND CAST(part_complete_date as DATE) <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                //        "AND part_status = 'Complete'   GROUP BY CAST(part_complete_date as date),staff_id) as worked on d.date_plan = worked.date " +
                //        "where  s.staff_id = " + staff_id.ToString() + " and d.date_plan >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' and d.date_plan <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' AND d.date_plan <= CAST(GETDATE() as date) " +
                //        "order by d.date_plan";
                //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                //    {
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataTable dt = new DataTable();
                //        da.Fill(dt);
                //        dgvPerformance.DataSource = dt;
                //    }
                //}
                //else if (engineer == -1)
                //{
                //    using (SqlCommand cmd = new SqlCommand("usp_staff_checker_programming", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.AddWithValue("@startDate", SqlDbType.DateTime).Value = dteStart.Value;
                //        cmd.Parameters.AddWithValue("@endDate", SqlDbType.DateTime).Value = dteEnd.Value;
                //        cmd.Parameters.AddWithValue("@staffID", SqlDbType.Int).Value = staff_id;

                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataTable dt = new DataTable();

                //        da.Fill(dt);
                //        dgvPerformance.DataSource = dt;
                //    }
                //}
                //else if (estimator == -1)
                //{
                //    string estSwitch;
                //    estSwitch = cmbStaff.Text;

                //    if (estSwitch == "Nicholas Thomas")
                //    {
                //        estSwitch = "Nick Thomas";
                //    }




                //    sql = "select date_output,day_of_week,coalesce(1* 90,0) as goal,coalesce(items,0) from " +
                //          "(select cast(date_output as date) as date_output, datename(WEEKDAY, cast(date_output as date)) as day_of_week,sum(item_count) as items " +
                //          "from dbo.solidworks_quotation_log l where quoted_by = '" + estSwitch + "' group by cast(date_output as date)) as solidworks " +
                //          "left join (select cast(placement_date as date) as placement_date,count(string) as allocated_estimators " +
                //          "from [order_database].dbo.view_department_reverse_concat_office where department = 'Estimating' " +
                //          " " +
                //          "group by placement_date) placement on solidworks.date_output = placement.placement_date " +
                //          "where cast(date_output as date) >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' and cast(date_output as date) <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "'";
                //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                //    {
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataTable dt = new DataTable();
                //        da.Fill(dt);
                //        dgvPerformance.DataSource = dt;
                //    }
                //}
                //else if (slimline == -1)
                //{
                //    sql = "select cast(quote_date as date) as quote_date,datename(WEEKDAY, cast(quote_date as date)) as day_of_week,62500,sum(coalesce(price,0)) as price " +
                //          "from [price_master].dbo.sl_quotation " +
                //          "left join [user_info].dbo.[user] u on sl_quotation.created_by_id = u.id " +
                //          "where highest_issue = -1 AND u.forename + ' ' + u.surname = '" + cmbStaff.Text + "' AND " +
                //          "cast(quote_date as date) >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' and cast(quote_date as date) <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                //          "group by quote_date ";
                //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                //    {
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataTable dt = new DataTable();
                //        da.Fill(dt);
                //        dgvPerformance.DataSource = dt;
                //    }
                //}
                //else if (slimline == -1 && shopfloor == -1)
                //{
                //    sql = "select cast(d.date_plan as date) as date_plan,datename(WEEKDAY,date_plan) as day_of_week,round(cast([hours] as float) + coalesce((ot.overtime * 0.8),0),2) as [set_hours]," +
                //          "COALESCE(worked.worked_hours,0) as worked_hours from dbo.power_plan_staff s " +
                //          "left  merge join dbo.power_plan_date d on s.date_id = d.id " +
                //          "left  merge join dbo.power_plan_overtime_remake ot on s.date_id = ot.date_id AND s.staff_id = ot.staff_id " +
                //          "left merge join (SELECT CAST(part_complete_date as date) as [date],ROUND((SUM(time_for_part) / 60), 2) as [worked_hours],staff_id FROM dbo.door_part_completion_log  " +
                //          "WHERE part_percent_complete is not null AND staff_id = " + staff_id.ToString() + " AND CAST(part_complete_date as DATE) >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' AND CAST(part_complete_date as DATE) <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
                //          "AND part_status = 'Complete'   GROUP BY CAST(part_complete_date as date),staff_id) as worked on d.date_plan = worked.date " +
                //          "where  s.staff_id = " + staff_id.ToString() + " and d.date_plan >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "' and d.date_plan <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' AND d.date_plan <= CAST(GETDATE() as date) " +
                //          "order by d.date_plan";
                //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                //    {
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataTable dt = new DataTable();
                //        da.Fill(dt);
                //        dgvPerformance.DataSource = dt;
                //    }

                //}
                //else
                //    dgvPerformance.DataSource = null;



                ////remake/repaint
                //sql = "SELECT [date] as [Work Date],datename(WEEKDAY,[date]) as [Day of Week],sum(repaint_count) as [Repaint Count],sum(remake_count) as [Remake Count],sum(cost) as [Cost] FROM ( " +
                //         "select cast(date_logged as date) as [date],count(dept.department_name) as repaint_count,0 as remake_count,sum(COALESCE(round(r.repaint_cost, 2), 0)) as [cost] from dbo.door d " +
                //         "right join dbo.repaints r on r.door_id = d.id " +
                //         "left join [user_info].dbo.[user] u_fault on u_fault.id = r.painter_name " +
                //         "left merge join dbo.SALES_LEDGER s on s.ACCOUNT_REF = d.customer_acc_ref " +
                //         "left merge join[dsl_kpi].dbo.department dept on dept.id = r.department " +
                //         "left join[user_info].dbo.[user] u_logged on u_logged.id = r.logged_by_id " +
                //         "WHERE date_logged >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "'  AND date_logged<= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "'  and u_fault.id = " + staff_id.ToString() + " " +
                //         "group by cast(date_logged as date) union all " +
                //         "select cast([date] as date) as [date],0 as repaint_count,count(dbo.remake.id) as remake_count,sum(coalesce(cost, 0)) as Cost from dbo.remake left join dbo.door on dbo.door.id = dbo.remake.door_id " +
                //         "left merge join dbo.SALES_LEDGER on dbo.SALES_LEDGER.ACCOUNT_REF = dbo.door.customer_acc_ref left " +
                //         "join [user_info].dbo.[user] as u on u.id = dbo.remake.persons_responsible " +
                //         "left merge join dsl_kpi.dbo.department as d1 on d1.id = dbo.remake.dept_responsible left  merge " +
                //         "join dsl_kpi.dbo.department as d2 on d2.id = dbo.remake.dept_noticed " +
                //         "where [date] >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "'  AND[date] <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "'  and remake.persons_responsible = " + staff_id.ToString() + " " +
                //         "group by[date]) as a group by[date] order by[date] ";

                //using (SqlCommand cmd = new SqlCommand(sql, conn))
                //{
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    dgvRemakeRepaint.DataSource = dt;
                //}

                conn.Close();
            }

            format();
        }

        private void format()
        {
            foreach (DataGridViewColumn col in dgvAbsent.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataGridViewColumn col in dgvLate.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            //foreach (DataGridViewColumn col in dgvPerformance.Columns)
            //{
            //    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}
            //foreach (DataGridViewColumn col in dgvRemakeRepaint.Columns)
            //{
            //    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}

            ////performance stuffs -- if shopfloor
            //if (shopfloor == -1)
            //{
            //    dgvPerformance.Columns[0].HeaderText = "Work Date";
            //    dgvPerformance.Columns[1].HeaderText = "Day of Week";
            //    dgvPerformance.Columns[2].HeaderText = "Set Hours";
            //    dgvPerformance.Columns[3].HeaderText = "Worked Hours";
            //}
            //else if (engineer == -1)
            //{
            //    dgvPerformance.Columns[0].HeaderText = "Work Date";
            //    dgvPerformance.Columns[1].HeaderText = "Day of Week";
            //    dgvPerformance.Columns[2].HeaderText = "Goal Points";
            //    dgvPerformance.Columns[3].HeaderText = "Actual Points";
            //}
            //else if (estimator == -1)
            //{
            //    dgvPerformance.Columns[0].HeaderText = "Work Date";
            //    dgvPerformance.Columns[1].HeaderText = "Day of Week";
            //    dgvPerformance.Columns[2].HeaderText = "Target";
            //    dgvPerformance.Columns[3].HeaderText = "Items Quoted";
            //}
            //else if (slimline == -1)
            //{
            //    dgvPerformance.Columns[0].HeaderText = "Work Date";
            //    dgvPerformance.Columns[1].HeaderText = "Day of Week";
            //    dgvPerformance.Columns[2].HeaderText = "Target";
            //    dgvPerformance.Columns[3].HeaderText = "Value Quoted";
            //}

            ////colours
            //double green = 0;
            //double red = 0;
            //foreach (DataGridViewRow row in dgvPerformance.Rows)
            //{

            //    if (Convert.ToDouble(row.Cells[2].Value.ToString()) <= Convert.ToDouble(row.Cells[3].Value.ToString()))
            //    {
            //        dgvPerformance.Rows[row.Index].Cells[2].Style.BackColor = Color.LightSeaGreen;
            //        dgvPerformance.Rows[row.Index].Cells[3].Style.BackColor = Color.LightSeaGreen;
            //        green++;
            //    }
            //    else
            //    {
            //        dgvPerformance.Rows[row.Index].Cells[2].Style.BackColor = Color.PaleVioletRed;
            //        dgvPerformance.Rows[row.Index].Cells[3].Style.BackColor = Color.PaleVioletRed;
            //        red++;
            //    }
            //}

            //total cost of remake/repaint
            double total_cost = 0;
            //for (int i = 0; i < dgvRemakeRepaint.Rows.Count; i++)
            //    total_cost = total_cost + Convert.ToDouble(dgvRemakeRepaint.Rows[i].Cells[4].Value.ToString());

            //get the values for remake/repaint seperately 
            //string total_cost_string = "";
            //using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            //{
            //    conn.Open();

            //    //remake
            //    string sql = "select COALESCE(sum(coalesce(cost, 0)),0) as Cost " +
            //        "from dbo.remake left join dbo.door on dbo.door.id = dbo.remake.door_id " +
            //        "left merge join dbo.SALES_LEDGER on dbo.SALES_LEDGER.ACCOUNT_REF = dbo.door.customer_acc_ref " +
            //        "left join [user_info].dbo.[user] as u on u.id = dbo.remake.persons_responsible " +
            //        "left merge join dsl_kpi.dbo.department as d1 on d1.id = dbo.remake.dept_responsible " +
            //        "left merge join dsl_kpi.dbo.department as d2 on d2.id = dbo.remake.dept_noticed " +
            //        "where [date] >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "'  AND[date] <= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
            //        "and remake.persons_responsible = " + staff_id.ToString();
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {

            //        var getData = cmd.ExecuteScalar();
            //        if (getData != null)
            //        {
            //            lblRemake.Text = getData.ToString();

            //            total_cost_string = Convert.ToDouble(cmd.ExecuteScalar().ToString()).ToString("#,##0.00");
            //            //ammend stuff like colour and having £-100
            //            total_cost_string = "£" + total_cost_string;
            //            //first up is the - number

            //            if (total_cost_string.Contains("-"))
            //            {
            //                total_cost_string = total_cost_string.Replace("-", "");
            //                total_cost_string = total_cost_string.Insert(0, "-");
            //            }
            //            lblRemake.Text = total_cost_string;
            //        }
            //        else
            //            lblRemake.Text = "£0";

            //    }

            //    //repaint
            //    sql = "select coalesce(sum(COALESCE(round(r.repaint_cost, 2), 0)),0) as [cost] " +
            //        "from dbo.door d " +
            //        "right join dbo.repaints r on r.door_id = d.id " +
            //        "left  join [user_info].dbo.[user] u_fault on u_fault.id = r.painter_name " +
            //        "left merge join dbo.SALES_LEDGER s on s.ACCOUNT_REF = d.customer_acc_ref " +
            //        "left merge join[dsl_kpi].dbo.department dept on dept.id = r.department " +
            //        "left  join[user_info].dbo.[user] u_logged on u_logged.id = r.logged_by_id " +
            //        "WHERE date_logged >= '" + dteStart.Value.ToString("yyyy-MM-dd") + "'  AND date_logged<= '" + dteEnd.Value.ToString("yyyy-MM-dd") + "' " +
            //        "and u_fault.id = " + staff_id.ToString();

            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {

            //        var getData = cmd.ExecuteScalar();
            //        if (getData != null)
            //        {
            //            lblRepaint.Text = getData.ToString();

            //            total_cost_string = Convert.ToDouble(cmd.ExecuteScalar().ToString()).ToString("#,##0.00");
            //            //ammend stuff like colour and having £-100
            //            total_cost_string = "£" + total_cost_string;
            //            //first up is the - number

            //            if (total_cost_string.Contains("-"))
            //            {
            //                total_cost_string = total_cost_string.Replace("-", "");
            //                total_cost_string = total_cost_string.Insert(0, "-");
            //            }
            //            lblRepaint.Text = total_cost_string;
            //        }
            //        else
            //            lblRepaint.Text = "£0";

            //    }

            //    conn.Close();
            //}


            //    string total_cost_string = total_cost.ToString("#,##0.00");
            ////ammend stuff like colour and having £-100
            //total_cost_string = "£" + total_cost_string;
            ////first up is the - number

            //if (total_cost_string.Contains("-"))
            //{
            //    total_cost_string = total_cost_string.Replace("-", "");
            //    total_cost_string = total_cost_string.Insert(0, "-");
            //}


            //  lblRemakeRepaint.Text = "Remake/Repaint Cost: " + total_cost_string;

            //work out percentage
            //    if (green == 0 && red == 0)
            //    {
            //        lblPerformance.Text = "0% Under Target";
            //    }//nothing
            //    else
            //    {
            //        if (green == 0)
            //            lblPerformance.Text = "100% Under Target";
            //        else if (red == 0)
            //            lblPerformance.Text = "0% Under Target";
            //        else
            //        {
            //            double test = 0;
            //            test = Math.Round((red / (green + red) * 100), 2);
            //            //Math.Round((green / red) * 100,2);
            //            lblPerformance.Text = test.ToString() + "% Under Target";
            //        }
            //    }
            //}
        }

        private void dteStart_CloseUp(object sender, EventArgs e)
        {
            apply_filter();
        }

        private void dteEnd_CloseUp(object sender, EventArgs e)
        {
            apply_filter();
        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            apply_filter();
        }
    }
}

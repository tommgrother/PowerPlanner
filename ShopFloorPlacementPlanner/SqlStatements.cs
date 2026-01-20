using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimlinePowerPlanner
{
    internal class SqlStatements
    {
        //接続文字列
        public const string ConnectionString = "user id=sa;" +
        "password=Dodid1;Network Address=192.168.0.150\\sqlexpress;" +
        "Trusted_Connection=no;" +
        "database=order_database; " +
        "connection timeout=30";

        //勤務時間に関する定数
        public const double FULL_DAY_HOURS = 6.4;
        public const double FULL_DAY_FRIDAY_HOURS = 5.6;
        public const double HALF_DAY_HOURS = 3.2;
        public const double HALF_DAY_FRIDAY_HOURS = 2.8;


        public const string DepartmentSqlString = "select id,department FROM dbo.power_plan_slimline_department";


        //SQL　文字列をここに実行する
        public void RunSQL(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        //SQL 文字列の結果は datatable に読み込む
        public DataTable ReturnSqlDatatable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                conn.Close();
                return dt;
            }

        }
        // SQL 文の実行結果を文字列として返す
        public string ReturnSqlString(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return cmd.ExecuteScalar().ToString();
                }

            }
        }

        public Boolean ReturnSqlBoolean(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0) //何かがいる
                        return true;
                    else
                        return false;
                }

            }
        }


        //部門名前を取得する
        public string GetDepartment(int department)
        {
            string sql = "SELECT department FROM dbo.power_plan_slimline_department WHERE id =" + department;

            if (ReturnSqlBoolean(sql))
                return ReturnSqlString(sql);
            else
                return "";

        }

        //部門のidを取得する
        public int GetDepartment(string department)
        {
            string sql = "SELECT id FROM dbo.power_plan_slimline_department WHERE department = '" + department + "'";
            return Convert.ToInt32(ReturnSqlString(sql));
        }

        //date_id　を取得する
        public int GetDateId(DateTime date_plan)
        {
            string sql = "SELECT id FROM dbo.power_plan_date where date_plan = '" + date_plan.ToString("yyyyMMdd") + "'";

            //date_idがいない、SQL文字列を実行する
            if (ReturnSqlBoolean(sql) == false)
            {
                //INSERT　文字列を実行する
                RunSQL("INSERT INTO dbo.power_plan_date (date_plan) VALUES ('" + date_plan.ToString("yyyyMMdd") + "')");
            }
            return Convert.ToInt32(ReturnSqlString(sql));
        }
        public DateTime GetDateFromDateID(int date_id)
        {
            string sql = "SELECT date_plan FROM dbo.power_plan_date where id = " + date_id + "";
            //date_id　を再取得する
            return Convert.ToDateTime(ReturnSqlString(sql));

        }

        //SQL文字列の結果をBooleanで返す
        public Boolean CheckExistingPlacement(int staff_id, int department, int date_id)
        {
            string sql = "select id FROM dbo.power_plan_slimline_staff WHERE date_id = " + date_id + " AND staff_id = " + staff_id + " AND department = " + department;
            return ReturnSqlBoolean(sql);
        }

        //金曜日かどうかを確認する
        public Boolean isFriday(int date_id)
        {
            if (GetDateFromDateID(date_id).DayOfWeek == DayOfWeek.Friday)
                return true;
            else
                return false;
        }

        //スタッフの合計勤務時間を取得する
        public double GetDefaultHours(int staff_id, int department, int date_id)
        {
            double defaultHours = 0;
            //date_id の値が金曜日の場合、デフォルトの勤務時間を1時間減らす
            if (isFriday(date_id))
                defaultHours = FULL_DAY_FRIDAY_HOURS; //金曜日のデフォルトの勤務時間
            else
                defaultHours = FULL_DAY_HOURS; //デフォルトの勤務時間

            string sql = "SELECT SUM([hours]) FROM dbo.power_plan_slimline_staff " +
                             "WHERE staff_id = " + staff_id + " AND date_id = " + date_id;
            string setHours = ReturnSqlString(sql);

            if (!string.IsNullOrEmpty(setHours))
                defaultHours -= Convert.ToDouble(setHours);

            //勤務時間が0以下の場合、0を返す
            if (defaultHours < 0)
                defaultHours = 0;

            return Math.Round(defaultHours, 2);
        }

        public double GetAllocatedHours(int staff_id, int department, int date_id)
        {
            string sql = "SELECT SUM([hours]) FROM dbo.power_plan_slimline_staff " +
                             "WHERE staff_id = " + staff_id + " AND date_id = " + date_id;

            string result = ReturnSqlString(sql);

            if (string.IsNullOrEmpty(sql))
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(result);
            }
        }

        //自動配置タイプを取得する
        public int GetAutomaticPlacementType(double hours, int date_id)
        {
            if (GetDateFromDateID(date_id).DayOfWeek == DayOfWeek.Friday)
            {
                if (hours == FULL_DAY_FRIDAY_HOURS)
                    return 1; //Full Day
                else if (hours == HALF_DAY_FRIDAY_HOURS)
                    return 2; //Half Day
                else
                    return 3; // Manual
            }
            else
            {
                if (hours == FULL_DAY_HOURS)
                    return 1; //Full Day
                else if (hours == HALF_DAY_HOURS)
                    return 2; //Half Day
                else
                    return 3; // Manual
            }
        }

        //アプデート勤務時間と配置タイプ
        public void UpdateHoursAndPlacementType(int staff_id, int department, int date_id, double hours, int placement_type)
        {
            string sql = "UPDATE dbo.power_plan_slimline_staff " +
                         "SET hours = " + hours + ",placement_type = " + placement_type + " " +
                         "WHERE date_id = " + date_id + " AND staff_id = " + staff_id + " AND department = " + department;
            RunSQL(sql);
        }


        public void UpdateOverTime(int placement_id, double am_hours, double pm_hours)
        {
            string sql = "";

            //placement_id が dbo.power_plan_slimline_overtime に存在する場合、SQL UPDATE を実行する
            sql = "SELECT * FROM dbo.power_plan_slimline_overtime WHERE placement_id = " + placement_id;

            if (ReturnSqlBoolean(sql))
                sql = "UPDATE dbo.power_plan_slimline_overtime SET am = " + am_hours + ", pm = " + pm_hours + " WHERE placement_id = " + placement_id;
            else
                sql = "INSERT INTO dbo.power_plan_slimline_overtime (placement_id,am,pm)" +
                      "VALUES (" + placement_id + "," + am_hours + "," + pm_hours + ")";

            RunSQL(sql);
        }

        public void UpdateDepartment(int placement_id, int department)
        {
            string sql = "UPDATE dbo.power_plan_slimline_staff SET department = " + department + " WHERE id = " + placement_id;
            RunSQL(sql);

        }

        public void RemovePlacement(int placement_id)
        {
            string sql = "DELETE FROM dbo.power_plan_slimline_staff WHERE id = " + placement_id;
            RunSQL(sql);
            sql = "DELETE FROM dbo.power_plan_slimline_overtime WHERE placement_id = " + placement_id;
            RunSQL(sql);
        }

        public void ClearPlan(int date_id)
        {
            //do this all through a stored procedure
            string sql = "EXEC [dbo].[usp_power_plan_slimline_clear_day] " +
                         "@date_id = @_date_id ";


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@_date_id", date_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }

        public string returnStaffName(int staff_id)
        {
            string sql = "SELECT forename + ' ' + surname as staff_name FROM [user_info].dbo.[user] WHERE id = " + staff_id;
            return ReturnSqlString(sql);
        }

        public void UpdateAutomaticPreset(int staff_id, int enabled, int default_dept, double hours)
        {
            string sql = "UPDATE [user_info].dbo.[user] " +
                             "SET slimline_pp_enabled = " + enabled + "," +
                             "slimline_pp_default_dept = " + default_dept + "," +
                             "slimline_pp_default_hours = " + hours + " " +
                             "WHERE id = " + staff_id;

            RunSQL(sql);
        }

        public void InsertPlacment(int date_id, int staff_id, int department, int placementType, double hours)
        {
            //SQL の INSERT 文字列を作成して実行する
            string sql = "INSERT INTO dbo.power_plan_slimline_staff " +
                            "(date_id,staff_id,department,placement_type,hours) " +
                         "VALUES " +
                            "(" + date_id + "," + staff_id + "," + department + "," + placementType + "," + hours + ")";
            RunSQL(sql);
        }

        public int ReturnStaffHolidayAbsence(int staff_id, int date_id)
        {
            string sql = "SELECT a.absent_type FROM dbo.absent_holidays a " +
             "WHERE a.date_absent = '" + GetDateFromDateID(date_id).ToString("yyyyMMdd") + "' AND staff_id = " + staff_id;
            if (ReturnSqlBoolean(sql))
                return Convert.ToInt32(ReturnSqlString(sql));
            else
                return 0;
        }

        public string ReturnStaffHolidayAbsenceString(int staff_id, int date_id)
        {

            string sql = "SELECT t.absent_type FROM dbo.absent_holidays a " +
                         "LEFT JOIN dbo.absent_holidays_type t on a.absent_type = t.absent_number " +
                         "WHERE a.date_absent = '" + GetDateFromDateID(date_id).ToString("yyyyMMdd") + "' AND staff_id = " + staff_id;
            if (ReturnSqlBoolean(sql))
                return ReturnSqlString(sql);
            else
                return "";

        }

        public void LoadDefaultPlacements(int date_id)
        {
            //まずは、現在のdate_idの各スタッフPlacementはクリアする
            ClearPlan(date_id);
            //次に、各スタッフのデフォルトPlacementを実行する
            string sql = "select u.id as staff_id,slimline_pp_default_dept ,slimline_pp_default_hours " +
                         "FROM [user_info].dbo.[user] u where slimline_pp_enabled = -1";

            DataTable dt = ReturnSqlDatatable(sql);

            foreach (DataRow row in dt.Rows)
            {
                int staff_id = Convert.ToInt32(row[0].ToString());
                int department = Convert.ToInt32(row[1].ToString());
                double default_hours = Convert.ToDouble(row[2].ToString());

                //スタッフの休憩日と不在日を確認する

                if (GetDateFromDateID(date_id).DayOfWeek == DayOfWeek.Friday)
                {
                    default_hours = (default_hours - 1) * 0.8; //金曜日の場合、勤務時間を1時間減らしてから80％にする
                }
                else default_hours = default_hours * 0.8; //金曜日以外の場合、勤務時間を80％にする

                default_hours = Math.Round(default_hours, 2);

                int holiday = ReturnStaffHolidayAbsence(staff_id, date_id);
                if (holiday == 3)
                    default_hours /= 2; //半分勤務時間
                else if (holiday > 0)
                    continue; //休憩または不在日のためスキップする

                    int placementType = GetAutomaticPlacementType(default_hours, date_id);

                InsertPlacment(
                    date_id,
                    staff_id,
                    department,
                    placementType,
                    default_hours
                    );
            }


        }

        public string ReturnPartCompletionLogDepartment(int department)
        {
            string sql = "SELECT part_completion_log_dept FROM dbo.power_plan_slimline_department WHERE id = " + department;
            return ReturnSqlString(sql);
        }


        public DataTable LoadChronological(DateTime start, DateTime end, int staff_id,int department)
        {
            string department_link = ReturnPartCompletionLogDepartment(department);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("usp_power_planner_chronological_shop_actions_slimline",conn ))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_time", SqlDbType.Date).Value = start;
                    cmd.Parameters.AddWithValue("@action_time_end", SqlDbType.Date).Value = end;
                    cmd.Parameters.AddWithValue("@user_id", SqlDbType.Int).Value = staff_id;
                    cmd.Parameters.AddWithValue("@department_link", SqlDbType.NVarChar).Value = department_link;

                    //dt.Columns.Add("Status");// dataGridView1.Columns.Add("Status", "Status");
                    //dt.Columns.Add("Time");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;



                }
                conn.Close();
            }
        }


        public string GetExcelColumnLetter(int columnIndex)
        {
            //columnIndex++; // Excel is 1-based

            string columnName = "";
            while (columnIndex > 0)
            {
                int remainder = (columnIndex - 1) % 26;
                columnName = (char)(remainder + 'A') + columnName;
                columnIndex = (columnIndex - 1) / 26;
            }

            return columnName;
        }

        public void loadCombobox(string sql, ComboBox cmb)
        {
            //cmb.BindComboBox(ComboBoxLookup.LoadComboBox(sql));
        }

    }
}

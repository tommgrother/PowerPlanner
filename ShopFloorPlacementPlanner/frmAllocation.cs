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
    public partial class frmAllocation : Form
    {
        public int staff_id { get; set; }
        public string department_long { get; set; }
        public string deptartment_short { get; set; }
        public frmAllocation(int _staff_id, string _department, string staff_name)
        {
            InitializeComponent();
            department_long = _department;
            staff_id = _staff_id;

            switch (department_long)
            {
                case "Punching":
                    deptartment_short = "punch";
                    break;
                case "Bending":
                    deptartment_short = "bend";
                    break;
                case "Welding":
                    deptartment_short = "weld";
                    break;
                case "Buffing":
                    deptartment_short = "buff";
                    break;
                case "Painting":
                    deptartment_short = "paint";
                    break;
                case "Packing":
                    deptartment_short = "pack";
                    break;

            }
            string sql = "SELECT forename + ' ' + surname FROM [user_info].dbo.[user] WHERE id = " + staff_id;
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    staff_name = cmd.ExecuteScalar().ToString();
                }


                    conn.Close();
            }
            lblTitle.Text = staff_name + " Door Allocation";

            load_grid();
        }

        private void load_grid()
        {
            string sql = "SELECT dbo.door.id as [Door ID], " +
                "RTRIM(s.NAME) as Customer, " +
                //"date_" + deptartment_short + "_complete as end_dept, " +
                "round((cast([time_" + deptartment_short + "] as float)*[quantity_same])/60,2) as [" + department_long + " Time], " +
                "round(([time_remaining_" + deptartment_short + "]*[quantity_same])/60,2) as  [" + department_long + " Time Remaining] ," +
                "operation_date as [Date Allocated], " +
                "started_" + deptartment_short + " as [" + department_long + " Started],  " +
                "date_" + deptartment_short + "_complete as [" + department_long + " Finished], " +
                "CASE WHEN priority_status_" + deptartment_short.Replace("bend","") + " = -1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS [" + department_long + " Priority] ," +
                "dbo.door." + department_long + "_note as [" + department_long + " Note] " +
                "FROM dbo.door_allocation " +
                "LEFT JOIN dbo.door ON dbo.door_allocation.door_id = dbo.door.id " +
                "LEFT JOIN dbo.SALES_LEDGER s on customer_acc_ref = s.ACCOUNT_REF " +
                "WHERE (dbo.door_allocation.staff_id = " + staff_id + ") AND " +
                "(dbo.door_allocation.department = '" + department_long.Replace("Buffing","Dressing") + "') AND " +
                "time_remaining_" + deptartment_short + " >= 0 AND " +
                "((complete_" + deptartment_short + " is null or complete_" + deptartment_short + " = 0) or date_" + deptartment_short + "_complete >= cast(getdate() as date)) AND " +
                "(dbo.door.id > 100000 or (dbo.door.id > 64690 AND dbo.door.id < 64699) OR (dbo.door.id > 66200 AND dbo.door.id < 66209)) " +
                "order by date_" + deptartment_short + "_complete";

        
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

                conn.Close();
            }

            format();
        }

        private void format()
        {
            foreach (DataGridViewColumn col  in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value.ToString() == "0")
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                if (Convert.ToBoolean(row.Cells[7].Value.ToString()) == true)
                    row.Cells[7].Style.BackColor = Color.MediumPurple;
            }

        }

        private void frmAllocation_Load(object sender, EventArgs e)
        {
            format();
        }
    }
}

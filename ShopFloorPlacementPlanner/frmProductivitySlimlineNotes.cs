using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmProductivitySlimlineNotes : Form
    {
        public string staff_name { get; set; }
        public string date { get; set; }
        public frmProductivitySlimlineNotes(string _staff_name, string _date,DateTime date_string)
        {
            InitializeComponent();

            staff_name = _staff_name;
            date = _date;

            fill_grid();

            label1.Text = _staff_name + " - " + date_string.ToString("dd/MM/yyyy");
        }

        private void fill_grid()
        {
            int staff_id = 0;
            string sql = "SELECT id FROM [user_info].dbo.[user] WHERE forename + ' ' + surname = '" + staff_name + "'";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }

                sql = "select d.id,part_complete_date,COALESCE(sl_stores_note,'') as sl_stores_note, " +
                      "case when cut_staff_allocation = '" + staff_name + "' then COALESCE(cutting_note,'') else '' end as cutting_note, " +
                      "case when prep_staff_allocation = '" + staff_name + "' then COALESCE(prepping_note,'') else '' end as prepping_note, " +
                      "case when assembly_staff_allocation = '" + staff_name + "' then COALESCE(assembly_note,'') else '' end as assembly_note, " +
                      "case when SL_buff_staff_allocation = '" + staff_name + "' then COALESCE(sl_buff_note,'') else '' end as sl_buff_note, " +
                      "case when pack_staff_allocation = '" + staff_name + "' then COALESCE(packing_note,'') else '' end as packing_note " +
                      "FROM dbo.door d " +
                      "left join dbo.door_part_completion_log p on d.id = p.door_id " +
                      "where (cut_staff_allocation = '" + staff_name + "' or prep_staff_allocation = '" + staff_name + "' or " +
                      "assembly_staff_allocation = '" + staff_name + "' or SL_buff_staff_allocation = '" + staff_name + "' or " +
                      "pack_staff_allocation = '" + staff_name + "') AND staff_id = " + staff_id + " AND " +
                      "CAST(part_complete_date as DATE) = '" + date + "' ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

                format();
                conn.Close();
            }
        }

        private void format()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dataGridView1.Columns[0].HeaderText = "Door ID";
            dataGridView1.Columns[1].HeaderText = "Operation Complete Date";
            dataGridView1.Columns[2].HeaderText = "Stores Note";
            dataGridView1.Columns[3].HeaderText = "Cutting Note";
            dataGridView1.Columns[4].HeaderText = "Prepping Note";
            dataGridView1.Columns[5].HeaderText = "Assembly Note";
            dataGridView1.Columns[6].HeaderText = "Buffing Note";
            dataGridView1.Columns[7].HeaderText = "Packing Note";

        }

        private void frmProductivitySlimlineNotes_Load(object sender, EventArgs e)
        {
            format();
        }

        private void frmProductivitySlimlineNotes_Shown(object sender, EventArgs e)
        {
            format();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == false)
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}

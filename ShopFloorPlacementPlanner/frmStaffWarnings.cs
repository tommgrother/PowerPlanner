using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class frmStaffWarnings : Form
    {
        public int cmb_index_skip { get; set; }
        public frmStaffWarnings()
        {
            InitializeComponent();
            load_grid();
            load_cmb();
        }

        private void load_grid()
        {
            string temp_name = cmbStaff.Text;
            string sql = "select u.id as staff_id,forename + ' ' + surname as Staff,warning_number as [Warning Number],warning_note as [Warning Note],warning_department as [Department],warning_date as [Warning Date],warning_given_by as [Warning given By] FROM dbo.staff_warnings s " +
                "left join [user_info].dbo.[user] u on s.staff_id = u.id " +
                "WHERE [current] = 1 ";

            if (cmbStaff.Text.Length > 0)
                sql = sql + " AND forename + ' ' + surname = '" + cmbStaff.Text + "' ";

            sql = sql + "order by forename + ' ' + surname asc,warning_number asc";

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


        private void load_cmb()
        {
            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();
                //get the data for the combobox
                string sql = "select distinct forename + ' ' + surname as staff FROM dbo.staff_warnings s " +
                    "left join[user_info].dbo.[user] u on s.staff_id = u.id " +
                    "WHERE[current] = 1 order by forename + ' ' + surname asc";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtStaff = new DataTable();
                    da.Fill(dtStaff);
                    cmbStaff.Items.Clear();
                    foreach (DataRow dr in dtStaff.Rows)
                    {
                        cmbStaff.Items.Add(dr[0].ToString());
                    }
                }
                conn.Close();
            }
        }

        private void format()
        {
            dataGridView1.Columns[0].Visible = false;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //2 needs to be fill
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //autosize height
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; ;

        }

        private void btnNewWarning_Click(object sender, EventArgs e)
        {
            frmNewWarning frm = new frmNewWarning();
            frm.ShowDialog();
            load_cmb();
            load_grid();

        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_grid();
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbStaff.SelectedIndex = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            frmViewWarning frm = new frmViewWarning(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()),
                                                    Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
            frm.ShowDialog();
        }
    }
}

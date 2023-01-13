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
    public partial class frmStockParts : Form
    {
        public DateTime date { get; set; }
        public frmStockParts(DateTime _date)
        {
            InitializeComponent();
            date = _date;
            loadGrid();
            fillCombo();
            lblTitle.Text= "Stock Parts for " + _date.ToString("dd/MM/yyyy");
        }

        private void loadGrid()
        {
            string sql = "select staff_string,part_string,date_started,date_complete,total_items_complete,total_time " +
                         "from dbo.view_bending_session where CAST(date_started as date) = '" + date.ToString("yyyyMMdd") + "' AND staff_string like '%" + cmbStaff.Text + "%' ";

            using (SqlConnection connection= new SqlConnection(connectionStrings.ConnectionString)) 
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                connection.Close();
            }
            format();
        }

        private void format()
        {
            dataGridView1.Columns[0].HeaderText = "Staff";
            dataGridView1.Columns[1].HeaderText = "Stock Part";
            dataGridView1.Columns[2].HeaderText = "Date Started";
            dataGridView1.Columns[3].HeaderText = "Date Complete";
            dataGridView1.Columns[4].HeaderText = "Total Items";
            dataGridView1.Columns[5].HeaderText = "Total Time";

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void fillCombo()
        {

            cmbStaff.Items.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (cmbStaff.Items.Contains(row.Cells[0].Value.ToString()))
                { } //nothing
                else
                    cmbStaff.Items.Add(row.Cells[0].Value.ToString());
            }


        }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}

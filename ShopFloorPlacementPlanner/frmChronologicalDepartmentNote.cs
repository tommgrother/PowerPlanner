using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.CodeDom.Compiler;
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
    public partial class frmChronologicalDepartmentNote : Form
    {
        public string dept { get; set; }
        public DateTime date { get; set; }

        public frmChronologicalDepartmentNote(string dept,DateTime date)
        {
            InitializeComponent();

            this.dept = dept; 
            this.date = date;

        }

        private void load_grid(string dept,DateTime date)
        {
            string note = "";

            using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
            {
                conn.Open();

                string sql = "select goal_notes_" + dept + " FROM dbo.daily_department_goal where date_goal = '" + date.ToString("yyyyMMdd") + "'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    note = cmd.ExecuteScalar().ToString();
                }

                conn.Close();
            }

            note = "<h2>" + dept + "ing Note - " + date.ToString("dd/MM/yyyy") + "</h2>" + note;

            this.Invoke((MethodInvoker)(() => {
                webBrowser1.DocumentText = note;
            }));
            
        }

        private void frmChronologicalDepartmentNote_Shown(object sender, EventArgs e)
        {
            load_grid(dept, date);
        }
    }
}

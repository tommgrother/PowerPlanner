using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SlimlinePowerPlanner
{
    public partial class frmNote : Form
    {
        public int _placement_id { get; set; }
        public frmNote(int placement_id)
        {
            InitializeComponent();

            _placement_id = placement_id;
            SqlStatements s = new SqlStatements();
            string sql = "SELECT placement_note FROM dbo.power_plan_slimline_staff WHERE id = " + placement_id;
            txtNote.Text = s.ReturnSqlString(sql);
            txtNote.SelectionStart = txtNote.Text.Length;
            txtNote.SelectionLength = 0;

        }

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE dbo.power_plan_slimline_staff " +
                         "SET placement_note = '" + txtNote.Text.Replace("'","") + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + Environment.NewLine + "' " +
                         "WHERE id = " + _placement_id;

            SqlStatements s = new SqlStatements();
            s.RunSQL(sql);
            this.Close();
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmKevinMessage : Form
    {
        public int _keivn { get; set; }

        // Sets the window to be foreground
        [DllImport("User32")]
        private static extern int SetForegroundWindow(IntPtr hwnd);
        // Activate or minimize a window
        [DllImportAttribute("User32.DLL")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 5;

        private void ActivateApplication(string briefAppName)
        {
            Process[] procList = Process.GetProcessesByName(briefAppName);

            if (procList.Length > 0)
            {
                ShowWindow(procList[0].MainWindowHandle, SW_RESTORE);
                SetForegroundWindow(procList[0].MainWindowHandle);

            }
        }

        public frmKevinMessage(int kevin)
        {
            InitializeComponent();
            login.formIsOpen = -1;

            ActivateApplication(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            _keivn = kevin;

            fillDGV();
        }

        private void fillDGV()
        {
            //remove any potential buttons
            try
            {
                dataGridView1.Columns.Remove("Acknowledged");
            }
            catch
            {
            }


            //unique based on whos logged in
            string sql = "";
            string person = login.userFullName;
            person = person.Substring(0, person.IndexOf(" "));

            if (_keivn == -1)
            {
                sql = "SELECT [message] as [Message],  case when simon = -1 then 'Acknowledged - ' + CAST(simon_date as nvarchar(max)) WHEN simon = -2 then 'N/A' else 'Not Acknowledged' end as [Simon]," +
                    "case when damian = -1 then 'Acknowledged - ' + CAST(damian_date as nvarchar(max)) WHEN damian = -2 then 'N/A' else 'Not Acknowledged' end as [Damian]," +
                    "case when richard = -1 then 'Acknowledged - ' + CAST(richard_date as nvarchar(max)) WHEN richard = -2 then 'N/A' else 'Not Acknowledged' end as [Richard]  FROM dbo.kevinMessage WHERE message_date >= CAST(dateadd(day,-7,GETDATE()) as date) order by id desc";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        conn.Close();
                        dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        btnAdd.Visible = true;
                        dataGridView1.ClearSelection();
                    }
                }
            }
            else if (person == "other")
            { }
            else
            {
                sql = "SELECT message as Message  FROM dbo.kevinMessage WHERE  " + person + " is null order by id asc";
                using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        conn.Close();

                        DataGridViewButtonColumn ackButton = new DataGridViewButtonColumn();
                        ackButton.Name = "Acknowledged";
                        ackButton.Text = "Acknowledged";
                        ackButton.UseColumnTextForButtonValue = true;
                        int columnIndex = (dataGridView1.Columns.Count);
                        if (dataGridView1.Columns["Acknowledged"] == null)
                        {
                            dataGridView1.Columns.Insert(columnIndex, ackButton);
                        }
                        dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        dataGridView1.ClearSelection();
                    }
                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.DarkSeaGreen)
            {
                string person = login.userFullName;
                person = person.Substring(0, person.IndexOf(" "));

                var senderGrid = (DataGridView)sender;
                int index = -1;
                int Messageindex = -1;
                if (dataGridView1.Columns.Contains("Acknowledged"))
                    index = dataGridView1.Columns["Acknowledged"].Index;
                Messageindex = dataGridView1.Columns["Message"].Index;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && index >= 0)
                {
                    //mark this one as acknowledged
                    string sql = "UPDATE dbo.kevinMessage set " + person + " = -1 , " + person + "_date = getdate() WHERE message = '" + dataGridView1.Rows[e.RowIndex].Cells[Messageindex].Value.ToString() + "' ";
                    using (SqlConnection conn = new SqlConnection(connectionStrings.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddMessage AM = new frmAddMessage();
            AM.ShowDialog();
            dataGridView1.DataSource = null;
            fillDGV();
            string person = login.userFullName;
            person = person.Substring(0, person.IndexOf(" "));
            if (person == "Kevin")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (row.Cells[i].Value.ToString() == "Not Acknowledged")
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.PaleVioletRed;
                            row.Cells[i].Style = style;
                        }
                        if (row.Cells[i].Value.ToString().Contains("Acknowledged -"))
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.DarkSeaGreen;
                            row.Cells[i].Style = style;
                        }
                        if (row.Cells[i].Value.ToString() == "N/A")
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.DarkSeaGreen;
                            row.Cells[i].Style = style;
                        }
                    }
                }
            }
        }

        private void frmKevinMessage_Shown(object sender, EventArgs e)
        {
            string person = login.userFullName;
            person = person.Substring(0, person.IndexOf(" "));
            if (person == "Kevin")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (row.Cells[i].Value.ToString() == "Not Acknowledged")
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.PaleVioletRed;
                            row.Cells[i].Style = style;
                        }
                        if (row.Cells[i].Value.ToString().Contains("Acknowledged -"))
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.DarkSeaGreen;
                            row.Cells[i].Style = style;
                        }
                        if (row.Cells[i].Value.ToString() == "N/A")
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.DarkSeaGreen;
                            row.Cells[i].Style = style;
                        }
                    }
                }
            }

            dataGridView1.ClearSelection();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //maybe a "you cant leave until here you have acknowledged"
            string person = login.userFullName;
            person = person.Substring(0, person.IndexOf(" "));
            if (person != "Kevin")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.DefaultCellStyle.BackColor != Color.DarkSeaGreen)
                    {
                        MessageBox.Show("Please acknowledge all messages before closing this screen", "Awaiting acknowledgement");
                        return;
                    }

                }
            }
            this.Close();
            login.formIsOpen = 0;
        }

        private void frmKevinMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                string person = login.userFullName;
                person = person.Substring(0, person.IndexOf(" "));
                if (person != "Kevin")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.DefaultCellStyle.BackColor != Color.DarkSeaGreen)
                        {
                            MessageBox.Show("Please acknowledge all messages before closing this screen", "Awaiting acknowledgement");
                            e.Cancel = true;
                            return;
                        }

                    }
                }
                //if (MessageBox.Show("Are you sure you want to exit?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                //    Environment.Exit(1);
                //else
                //    e.Cancel = true;
            }
        }
    }
}

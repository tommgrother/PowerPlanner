using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimlinePowerPlanner
{
    public partial class frmUpdateStaff : Form
    {
        public int _department { get; set; }
        public int _date_id { get; set; }
        public frmUpdateStaff(int _date_id)
        {
            InitializeComponent();
            SqlStatements s = new SqlStatements();
            this._department = _department;

            //フォームのタイトルラベルに部門名を取得して表示する
            //lblTitle.Text = s.GetDepartment(_department);
            //lblCurrentStaff.Text = "Current " + lblTitle.Text + " Staff";
            this._date_id = _date_id;


        }

        private void load_both_grids()
        {
            load_current_staff();
            //load_selectable_staff();

        }

        private void load_selectable_staff()
        {
            SqlStatements s = new SqlStatements();
            //選択可能なスタッフをDataGridViewに読み込む
            string sql = "select id,Fullname as [Staff] FROM [user_info].dbo.c_view_slimline_staff order by Fullname";
            DataTable dt = s.ReturnSqlDatatable(sql);
            if (dt.Rows.Count > 0)
            {
          
            }

        }


        private void load_current_staff()
        {
            SqlStatements s = new SqlStatements();
            //現在のスターフをDataGridViewに読み込む
            string sql = "select s.placement_id,staff_id,[Staff Name],[Department],[Placement Type],Hours,[Over Time],[Placement Note] FROM view_power_plan_slimline s " +
                         "left join dbo.power_plan_slimline_overtime o on s.placement_id = o.placement_id " +
                         "where " +
                         "date_id = " + _date_id + " " +
                         "order by department_id asc,[Staff Name] asc";

            DataTable dt = s.ReturnSqlDatatable(sql);

            if (dt.Rows.Count > 0)
            {
                dgvCurrentStaff.DataSource = dt;


                //PlacementTypeボタン
                if (dgvCurrentStaff.Columns.Contains("Full Day") == true)
                    dgvCurrentStaff.Columns.Remove("Full Day");

                if (dgvCurrentStaff.Columns.Contains("Half Day") == true)
                    dgvCurrentStaff.Columns.Remove("Half Day");

                if (dgvCurrentStaff.Columns.Contains("Manual") == true)
                    dgvCurrentStaff.Columns.Remove("Manual");

                if (dgvCurrentStaff.Columns.Contains("Note") == true)
                    dgvCurrentStaff.Columns.Remove("Note");

                if (dgvCurrentStaff.Columns.Contains("X") == true)
                    dgvCurrentStaff.Columns.Remove("X");

                int column_index = dgvCurrentStaff.Columns["Over Time"].Index + 1;
                //DataGridViewButtonColumn FullDayButton = new DataGridViewButtonColumn();
                //FullDayButton.Name = "Full Day";
                //FullDayButton.Text = "Full Day";
                //FullDayButton.UseColumnTextForButtonValue = true;
                //if (dgvCurrentStaff.Columns["Full Day"] == null)
                //{
                //    dgvCurrentStaff.Columns.Insert(column_index, FullDayButton);
                //}
                //column_index += 1;
                //DataGridViewButtonColumn HalfDayButton = new DataGridViewButtonColumn();
                //HalfDayButton.Name = "Half Day";
                //HalfDayButton.Text = "Half Day";
                //HalfDayButton.UseColumnTextForButtonValue = true;
                //if (dgvCurrentStaff.Columns["Half Day"] == null)
                //{
                //    dgvCurrentStaff.Columns.Insert(column_index, HalfDayButton);
                //}
                //column_index += 1;
                //DataGridViewButtonColumn ManualButton = new DataGridViewButtonColumn();
                //ManualButton.Name = "Manual";
                //ManualButton.Text = "Manual";
                //ManualButton.UseColumnTextForButtonValue = true;
                //if (dgvCurrentStaff.Columns["Manual"] == null)
                //{
                //    dgvCurrentStaff.Columns.Insert(column_index, ManualButton);
                //}
                //column_index += 1;
                DataGridViewButtonColumn NoteButton = new DataGridViewButtonColumn();
                NoteButton.Name = "Note";
                NoteButton.Text = "Note";
                NoteButton.UseColumnTextForButtonValue = true;
                if (dgvCurrentStaff.Columns["Note"] == null)
                {
                    dgvCurrentStaff.Columns.Insert(column_index, NoteButton);
                }
                //column_index += 1;
                //DataGridViewButtonColumn RemoveButton = new DataGridViewButtonColumn();
                //RemoveButton.Name = "X";
                //RemoveButton.Text = "X";
                //RemoveButton.UseColumnTextForButtonValue = true;
                //if (dgvCurrentStaff.Columns["X"] == null)
                //{
                //    dgvCurrentStaff.Columns.Insert(column_index, RemoveButton);
                //}


                //placement_id　列を非表示する
                dgvCurrentStaff.Columns["placement_id"].Visible = false;
                //Staff_id　列を非表示する
                dgvCurrentStaff.Columns["staff_id"].Visible = false;
                dgvCurrentStaff.Columns["Placement Note"].Visible = false;

                foreach (DataGridViewRow row in dgvCurrentStaff.Rows)
                {
                    if (row.Cells["Placement Note"].Value.ToString().Length > 0)
                        row.DefaultCellStyle.BackColor = Color.Gold;
                }

                //DataGridViewの列幅を自動調整する
                foreach (DataGridViewColumn col in dgvCurrentStaff.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
                }
                //「Staff　Name」列の列幅を fill (自動伸縮) に設定する
                dgvCurrentStaff.Columns["Staff Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //DataGridViewのフォーカスを外す
                dgvCurrentStaff.ClearSelection();
            }
            else
            {
                dgvCurrentStaff.Columns.Clear();
                dgvCurrentStaff.DataSource = null;
            }

        }

        private void frmUpdateStaff_Shown(object sender, EventArgs e)
        {
            load_both_grids();
        }

        private void dgvSelectable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //SqlStatements s = new SqlStatements();
            //DataGridViewRow row = dgvSelectable.Rows[e.RowIndex];
            //int staff_id = Convert.ToInt32(row.Cells["id"].Value.ToString());

            ////このスタッフはまだ選択されていないことを確認する
            //if (s.CheckExistingPlacement(staff_id, _department, _date_id) == false)
            //{
            //    double hours = 0;
            //    int placementType = 0;
            //    //スタッフの勤務時間を取得する
            //    hours = s.GetDefaultHours(staff_id, _department, _date_id);


            //    int holiday = s.ReturnStaffHolidayAbsence(staff_id, _date_id);
            //    if (holiday == 3)
            //    {
            //        hours /= 2; //半分勤務時間
            //        MessageBox.Show(s.returnStaffName(staff_id) + " has a " + s.ReturnStaffHolidayAbsenceString(staff_id, _date_id) + " and can only be placed for half the normal hours.", "Half Day", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else if (holiday > 0) //休憩または不在日のためスキップする
            //    {
            //        MessageBox.Show(s.returnStaffName(staff_id) + " has a " + s.ReturnStaffHolidayAbsenceString(staff_id, _date_id) + " and cannot be placed.", "Absence/Holiday", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    ;

            //    placementType = s.GetAutomaticPlacementType(hours, _date_id);

            //    ////SQL の INSERT 文字列を作成して実行する
            //    //string sql = "INSERT INTO dbo.power_plan_slimline_staff " +
            //    //                "(date_id,staff_id,department,placement_type,hours) " +
            //    //             "VALUES " +
            //    //                "(" + _date_id + "," + staff_id + "," + _department + "," + placementType + "," + hours + ")";

            //    s.InsertPlacment(_date_id, staff_id, _department, placementType, hours);
            //    load_both_grids();
            //}
            //else
            //{
            //    MessageBox.Show(row.Cells["Staff"].Value.ToString() + " has already been placed in this department and date!", "Staff Already Been Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void dgvCurrentStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlStatements s = new SqlStatements();
            DataGridViewRow row = dgvCurrentStaff.Rows[e.RowIndex];
            int placement_id = Convert.ToInt32(row.Cells["placement_id"].Value.ToString());
            int staff_id = Convert.ToInt32(row.Cells["staff_id"].Value.ToString());
            string staff_name = row.Cells["Staff Name"].Value.ToString();
            double hours = 0;
            double allocated_hours = s.GetAllocatedHours(staff_id, _department, _date_id);
            double current_hours = Convert.ToDouble(row.Cells["hours"].Value.ToString());
            Boolean is_friday = s.isFriday(_date_id);

            if (e.ColumnIndex == row.Cells["Note"].ColumnIndex)
            {
                frmNote frm = new frmNote(placement_id);
                frm.ShowDialog();
                //DataGridViewを再読み込み
                load_both_grids();
            }




        }

        //private void btnOverTime_Click(object sender, EventArgs e)
        //{
        //    frmWeeklyOverTime frm = new frmWeeklyOverTime(_department, _date_id);
        //    frm.ShowDialog();
        //    load_current_staff();
        //}
    }
}

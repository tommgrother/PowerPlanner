using System;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmBatchPlacement : Form
    {
        private DateTime rangeStart;
        private DateTime rangeEnd;

        public frmBatchPlacement()
        {
            InitializeComponent();
        }

        private void MCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            rangeStart = Convert.ToDateTime(mCalendar.SelectionRange.Start);
            rangeEnd = Convert.ToDateTime(mCalendar.SelectionRange.End);

            lblStart.Text = "From:  " + rangeStart.ToShortDateString();
            lblEnd.Text = "To:     " + rangeEnd.ToShortDateString();
        }

        private void TabPlaceStaff_Click(object sender, EventArgs e)
        {
        }

        private void FrmBatchPlacement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'user_infoDataSet.c_view_current_shop_floor_staff' table. You can move, or remove it, as needed.
            this.c_view_current_shop_floor_staffTableAdapter.Fill(this.user_infoDataSet.c_view_current_shop_floor_staff);
        }

        private void FillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.c_view_current_shop_floor_staffTableAdapter.FillBy(this.user_infoDataSet.c_view_current_shop_floor_staff);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void FillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.c_view_current_shop_floor_staffTableAdapter.FillBy1(this.user_infoDataSet.c_view_current_shop_floor_staff);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
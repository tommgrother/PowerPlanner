namespace ShopFloorPlacementPlanner
{
    partial class frmBatchPlacement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchPlacement));
            this.mCalendar = new System.Windows.Forms.MonthCalendar();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.tabBatch = new System.Windows.Forms.TabControl();
            this.tabPlaceStaff = new System.Windows.Forms.TabPage();
            this.btnUpdatePlacements = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPlacementType = new System.Windows.Forms.ComboBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.cviewcurrentshopfloorstaffBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.user_infoDataSet = new ShopFloorPlacementPlanner.user_infoDataSet();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.c_view_current_shop_floor_staffTableAdapter = new ShopFloorPlacementPlanner.user_infoDataSetTableAdapters.c_view_current_shop_floor_staffTableAdapter();
            this.tabBatch.SuspendLayout();
            this.tabPlaceStaff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cviewcurrentshopfloorstaffBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_infoDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // mCalendar
            // 
            this.mCalendar.Location = new System.Drawing.Point(13, 13);
            this.mCalendar.Name = "mCalendar";
            this.mCalendar.TabIndex = 0;
            this.mCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MCalendar_DateChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(18, 188);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(0, 13);
            this.lblStart.TabIndex = 1;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(18, 214);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(0, 13);
            this.lblEnd.TabIndex = 2;
            // 
            // tabBatch
            // 
            this.tabBatch.Controls.Add(this.tabPlaceStaff);
            this.tabBatch.Controls.Add(this.tabPage2);
            this.tabBatch.Location = new System.Drawing.Point(253, 13);
            this.tabBatch.Name = "tabBatch";
            this.tabBatch.SelectedIndex = 0;
            this.tabBatch.Size = new System.Drawing.Size(340, 199);
            this.tabBatch.TabIndex = 3;
            // 
            // tabPlaceStaff
            // 
            this.tabPlaceStaff.Controls.Add(this.btnUpdatePlacements);
            this.tabPlaceStaff.Controls.Add(this.label3);
            this.tabPlaceStaff.Controls.Add(this.label2);
            this.tabPlaceStaff.Controls.Add(this.label1);
            this.tabPlaceStaff.Controls.Add(this.cmbPlacementType);
            this.tabPlaceStaff.Controls.Add(this.cmbDepartment);
            this.tabPlaceStaff.Controls.Add(this.cmbStaff);
            this.tabPlaceStaff.Location = new System.Drawing.Point(4, 22);
            this.tabPlaceStaff.Name = "tabPlaceStaff";
            this.tabPlaceStaff.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlaceStaff.Size = new System.Drawing.Size(332, 173);
            this.tabPlaceStaff.TabIndex = 0;
            this.tabPlaceStaff.Text = "Place Staff";
            this.tabPlaceStaff.UseVisualStyleBackColor = true;
            this.tabPlaceStaff.Click += new System.EventHandler(this.TabPlaceStaff_Click);
            // 
            // btnUpdatePlacements
            // 
            this.btnUpdatePlacements.Location = new System.Drawing.Point(221, 133);
            this.btnUpdatePlacements.Name = "btnUpdatePlacements";
            this.btnUpdatePlacements.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePlacements.TabIndex = 6;
            this.btnUpdatePlacements.Text = "Update";
            this.btnUpdatePlacements.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Placement Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Department:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Staff Name:";
            // 
            // cmbPlacementType
            // 
            this.cmbPlacementType.FormattingEnabled = true;
            this.cmbPlacementType.Items.AddRange(new object[] {
            "Full Day",
            "Half Day",
            "Shift",
            "Manual"});
            this.cmbPlacementType.Location = new System.Drawing.Point(99, 80);
            this.cmbPlacementType.Name = "cmbPlacementType";
            this.cmbPlacementType.Size = new System.Drawing.Size(197, 21);
            this.cmbPlacementType.TabIndex = 2;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Items.AddRange(new object[] {
            "Stores",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "Dispatch",
            "Slimline",
            "Toolroom",
            "Cleaning",
            "Management",
            "HS"});
            this.cmbDepartment.Location = new System.Drawing.Point(99, 53);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(197, 21);
            this.cmbDepartment.TabIndex = 1;
            // 
            // cmbStaff
            // 
            this.cmbStaff.DataSource = this.cviewcurrentshopfloorstaffBindingSource;
            this.cmbStaff.DisplayMember = "fullname";
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(99, 26);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(197, 21);
            this.cmbStaff.TabIndex = 0;
            this.cmbStaff.ValueMember = "id";
            // 
            // cviewcurrentshopfloorstaffBindingSource
            // 
            this.cviewcurrentshopfloorstaffBindingSource.DataMember = "c_view_current_shop_floor_staff";
            this.cviewcurrentshopfloorstaffBindingSource.DataSource = this.user_infoDataSet;
            // 
            // user_infoDataSet
            // 
            this.user_infoDataSet.DataSetName = "user_infoDataSet";
            this.user_infoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(332, 162);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // c_view_current_shop_floor_staffTableAdapter
            // 
            this.c_view_current_shop_floor_staffTableAdapter.ClearBeforeFill = true;
            // 
            // frmBatchPlacement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 492);
            this.Controls.Add(this.tabBatch);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.mCalendar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBatchPlacement";
            this.Text = "Batch Placements";
            this.Load += new System.EventHandler(this.FrmBatchPlacement_Load);
            this.tabBatch.ResumeLayout(false);
            this.tabPlaceStaff.ResumeLayout(false);
            this.tabPlaceStaff.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cviewcurrentshopfloorstaffBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_infoDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mCalendar;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.TabControl tabBatch;
        private System.Windows.Forms.TabPage tabPlaceStaff;
        private System.Windows.Forms.ComboBox cmbPlacementType;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnUpdatePlacements;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private user_infoDataSet user_infoDataSet;
        private System.Windows.Forms.BindingSource cviewcurrentshopfloorstaffBindingSource;
        private user_infoDataSetTableAdapters.c_view_current_shop_floor_staffTableAdapter c_view_current_shop_floor_staffTableAdapter;
    }
}
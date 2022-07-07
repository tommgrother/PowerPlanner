namespace ShopFloorPlacementPlanner
{
    partial class frmDepartmentManagement
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
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.lblStaff = new System.Windows.Forms.Label();
            this.cmbFirst = new System.Windows.Forms.ComboBox();
            this.cmbSecond = new System.Windows.Forms.ComboBox();
            this.cmbThird = new System.Windows.Forms.ComboBox();
            this.cmbFourth = new System.Windows.Forms.ComboBox();
            this.cmbFifth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSlimline = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbDefault = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbStaff
            // 
            this.cmbStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(15, 63);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(134, 21);
            this.cmbStaff.TabIndex = 0;
            this.cmbStaff.SelectedIndexChanged += new System.EventHandler(this.cmbStaff_SelectedIndexChanged);
            // 
            // lblStaff
            // 
            this.lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(12, 37);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(137, 23);
            this.lblStaff.TabIndex = 1;
            this.lblStaff.Text = "STAFF";
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbFirst
            // 
            this.cmbFirst.Enabled = false;
            this.cmbFirst.FormattingEnabled = true;
            this.cmbFirst.Items.AddRange(new object[] {
            "Cutting",
            "Prepping",
            "Assembly",
            "SL Buff",
            "SlimlineStores",
            "SlimlineDispatch",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "Dispatch",
            "Management",
            "HS",
            "ToolRoom",
            "Cleaning"});
            this.cmbFirst.Location = new System.Drawing.Point(15, 113);
            this.cmbFirst.Name = "cmbFirst";
            this.cmbFirst.Size = new System.Drawing.Size(134, 21);
            this.cmbFirst.TabIndex = 2;
            // 
            // cmbSecond
            // 
            this.cmbSecond.Enabled = false;
            this.cmbSecond.FormattingEnabled = true;
            this.cmbSecond.Items.AddRange(new object[] {
            "Cutting",
            "Prepping",
            "Assembly",
            "SL Buff",
            "SL Dispatch",
            "sl_stores",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "Dispatch",
            "Management",
            "HS",
            "ToolRoom",
            "Cleaning"});
            this.cmbSecond.Location = new System.Drawing.Point(155, 113);
            this.cmbSecond.Name = "cmbSecond";
            this.cmbSecond.Size = new System.Drawing.Size(134, 21);
            this.cmbSecond.TabIndex = 3;
            // 
            // cmbThird
            // 
            this.cmbThird.Enabled = false;
            this.cmbThird.FormattingEnabled = true;
            this.cmbThird.Items.AddRange(new object[] {
            "Cutting",
            "Prepping",
            "Assembly",
            "SL Buff",
            "SL Dispatch",
            "sl_stores",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "Dispatch",
            "Management",
            "HS",
            "ToolRoom",
            "Cleaning"});
            this.cmbThird.Location = new System.Drawing.Point(15, 163);
            this.cmbThird.Name = "cmbThird";
            this.cmbThird.Size = new System.Drawing.Size(134, 21);
            this.cmbThird.TabIndex = 4;
            // 
            // cmbFourth
            // 
            this.cmbFourth.Enabled = false;
            this.cmbFourth.FormattingEnabled = true;
            this.cmbFourth.Items.AddRange(new object[] {
            "Cutting",
            "Prepping",
            "Assembly",
            "SL Buff",
            "SL Dispatch",
            "sl_stores",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "Dispatch",
            "Management",
            "HS",
            "ToolRoom",
            "Cleaning"});
            this.cmbFourth.Location = new System.Drawing.Point(155, 163);
            this.cmbFourth.Name = "cmbFourth";
            this.cmbFourth.Size = new System.Drawing.Size(134, 21);
            this.cmbFourth.TabIndex = 5;
            // 
            // cmbFifth
            // 
            this.cmbFifth.Enabled = false;
            this.cmbFifth.FormattingEnabled = true;
            this.cmbFifth.Items.AddRange(new object[] {
            "Cutting",
            "Prepping",
            "Assembly",
            "SL Buff",
            "SL Dispatch",
            "sl_stores",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "Dispatch",
            "Management",
            "HS",
            "ToolRoom",
            "Cleaning"});
            this.cmbFifth.Location = new System.Drawing.Point(15, 214);
            this.cmbFifth.Name = "cmbFifth";
            this.cmbFifth.Size = new System.Drawing.Size(134, 21);
            this.cmbFifth.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "First Department";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(155, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Secondary Department";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Third Department";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(152, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 23);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fourth Department";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fifth Department";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // chkSlimline
            // 
            this.chkSlimline.AutoSize = true;
            this.chkSlimline.Enabled = false;
            this.chkSlimline.Location = new System.Drawing.Point(215, 217);
            this.chkSlimline.Name = "chkSlimline";
            this.chkSlimline.Size = new System.Drawing.Size(15, 14);
            this.chkSlimline.TabIndex = 12;
            this.chkSlimline.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(184, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Slimline";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(-25, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(370, 34);
            this.label7.TabIndex = 14;
            this.label7.Text = "Department Management";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(158, 61);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save Departments";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbDefault
            // 
            this.cmbDefault.Enabled = false;
            this.cmbDefault.FormattingEnabled = true;
            this.cmbDefault.Items.AddRange(new object[] {
            "Cutting",
            "Prepping",
            "Assembly",
            "SL Buff",
            "Laser",
            "Punching",
            "Bending",
            "Welding",
            "Dressing",
            "Painting",
            "Packing",
            "HS",
            "Management",
            "ToolRoom",
            "Stores",
            "Dispatch"});
            this.cmbDefault.Location = new System.Drawing.Point(155, 254);
            this.cmbDefault.Name = "cmbDefault";
            this.cmbDefault.Size = new System.Drawing.Size(134, 21);
            this.cmbDefault.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 21);
            this.label8.TabIndex = 17;
            this.label8.Text = "DEFAULT DEPARTMENT";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // frmDepartmentManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 310);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbDefault);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkSlimline);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFifth);
            this.Controls.Add(this.cmbFourth);
            this.Controls.Add(this.cmbThird);
            this.Controls.Add(this.cmbSecond);
            this.Controls.Add(this.cmbFirst);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.cmbStaff);
            this.Name = "frmDepartmentManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Department Management";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.ComboBox cmbFirst;
        private System.Windows.Forms.ComboBox cmbSecond;
        private System.Windows.Forms.ComboBox cmbThird;
        private System.Windows.Forms.ComboBox cmbFourth;
        private System.Windows.Forms.ComboBox cmbFifth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkSlimline;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbDefault;
        private System.Windows.Forms.Label label8;
    }
}
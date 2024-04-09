namespace ShopFloorPlacementPlanner
{
    partial class frmViewWarning
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewWarning));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.txtWarning = new System.Windows.Forms.RichTextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtDateGiven = new System.Windows.Forms.TextBox();
            this.txtDateLogged = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWarningGivenBy = new System.Windows.Forms.TextBox();
            this.lblLate = new System.Windows.Forms.Label();
            this.lblAbsent = new System.Windows.Forms.Label();
            this.dgvLate = new System.Windows.Forms.DataGridView();
            this.dgvAbsent = new System.Windows.Forms.DataGridView();
            this.dteStart = new System.Windows.Forms.DateTimePicker();
            this.dteEnd = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsent)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabControl.Location = new System.Drawing.Point(12, 95);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(594, 30);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.label1.Location = new System.Drawing.Point(216, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Staff";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbStaff
            // 
            this.cmbStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(216, 43);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(186, 28);
            this.cmbStaff.TabIndex = 5;
            this.cmbStaff.SelectedIndexChanged += new System.EventHandler(this.cmbStaff_SelectedIndexChanged);
            // 
            // txtWarning
            // 
            this.txtWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtWarning.Location = new System.Drawing.Point(12, 122);
            this.txtWarning.Name = "txtWarning";
            this.txtWarning.Size = new System.Drawing.Size(596, 453);
            this.txtWarning.TabIndex = 7;
            this.txtWarning.Text = "";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtDepartment.Location = new System.Drawing.Point(107, 601);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(186, 26);
            this.txtDepartment.TabIndex = 8;
            // 
            // txtDateGiven
            // 
            this.txtDateGiven.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtDateGiven.Location = new System.Drawing.Point(326, 601);
            this.txtDateGiven.Name = "txtDateGiven";
            this.txtDateGiven.Size = new System.Drawing.Size(186, 26);
            this.txtDateGiven.TabIndex = 9;
            // 
            // txtDateLogged
            // 
            this.txtDateLogged.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtDateLogged.Location = new System.Drawing.Point(326, 674);
            this.txtDateLogged.Name = "txtDateLogged";
            this.txtDateLogged.Size = new System.Drawing.Size(186, 26);
            this.txtDateLogged.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(107, 578);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Department";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(326, 578);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Date Given";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.Location = new System.Drawing.Point(326, 651);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 23);
            this.label4.TabIndex = 13;
            this.label4.Text = "Date Logged";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label5.Location = new System.Drawing.Point(107, 651);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Warning Given By";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWarningGivenBy
            // 
            this.txtWarningGivenBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtWarningGivenBy.Location = new System.Drawing.Point(107, 674);
            this.txtWarningGivenBy.Name = "txtWarningGivenBy";
            this.txtWarningGivenBy.Size = new System.Drawing.Size(186, 26);
            this.txtWarningGivenBy.TabIndex = 14;
            // 
            // lblLate
            // 
            this.lblLate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLate.ForeColor = System.Drawing.Color.Red;
            this.lblLate.Location = new System.Drawing.Point(1034, 64);
            this.lblLate.Name = "lblLate";
            this.lblLate.Size = new System.Drawing.Size(391, 28);
            this.lblLate.TabIndex = 19;
            this.lblLate.Text = "Total Late Days: 0";
            this.lblLate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblAbsent
            // 
            this.lblAbsent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsent.ForeColor = System.Drawing.Color.Red;
            this.lblAbsent.Location = new System.Drawing.Point(614, 64);
            this.lblAbsent.Name = "lblAbsent";
            this.lblAbsent.Size = new System.Drawing.Size(392, 28);
            this.lblAbsent.TabIndex = 18;
            this.lblAbsent.Text = "Total Absent Days: 0";
            this.lblAbsent.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dgvLate
            // 
            this.dgvLate.AllowUserToAddRows = false;
            this.dgvLate.AllowUserToDeleteRows = false;
            this.dgvLate.AllowUserToResizeColumns = false;
            this.dgvLate.AllowUserToResizeRows = false;
            this.dgvLate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLate.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLate.Location = new System.Drawing.Point(1039, 95);
            this.dgvLate.Name = "dgvLate";
            this.dgvLate.ReadOnly = true;
            this.dgvLate.RowHeadersVisible = false;
            this.dgvLate.Size = new System.Drawing.Size(392, 605);
            this.dgvLate.TabIndex = 17;
            // 
            // dgvAbsent
            // 
            this.dgvAbsent.AllowUserToAddRows = false;
            this.dgvAbsent.AllowUserToDeleteRows = false;
            this.dgvAbsent.AllowUserToResizeColumns = false;
            this.dgvAbsent.AllowUserToResizeRows = false;
            this.dgvAbsent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAbsent.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAbsent.Location = new System.Drawing.Point(619, 95);
            this.dgvAbsent.Name = "dgvAbsent";
            this.dgvAbsent.ReadOnly = true;
            this.dgvAbsent.RowHeadersVisible = false;
            this.dgvAbsent.Size = new System.Drawing.Size(392, 605);
            this.dgvAbsent.TabIndex = 16;
            // 
            // dteStart
            // 
            this.dteStart.Location = new System.Drawing.Point(770, 20);
            this.dteStart.Name = "dteStart";
            this.dteStart.Size = new System.Drawing.Size(139, 20);
            this.dteStart.TabIndex = 20;
            this.dteStart.Visible = false;
            // 
            // dteEnd
            // 
            this.dteEnd.Location = new System.Drawing.Point(915, 20);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Size = new System.Drawing.Size(139, 20);
            this.dteEnd.TabIndex = 21;
            this.dteEnd.Visible = false;
            // 
            // frmViewWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 710);
            this.Controls.Add(this.dteEnd);
            this.Controls.Add(this.dteStart);
            this.Controls.Add(this.lblLate);
            this.Controls.Add(this.lblAbsent);
            this.Controls.Add(this.dgvLate);
            this.Controls.Add(this.dgvAbsent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWarningGivenBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateLogged);
            this.Controls.Add(this.txtDateGiven);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.txtWarning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStaff);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewWarning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staff Warning";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.RichTextBox txtWarning;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtDateGiven;
        private System.Windows.Forms.TextBox txtDateLogged;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWarningGivenBy;
        private System.Windows.Forms.Label lblLate;
        private System.Windows.Forms.Label lblAbsent;
        private System.Windows.Forms.DataGridView dgvLate;
        private System.Windows.Forms.DataGridView dgvAbsent;
        private System.Windows.Forms.DateTimePicker dteStart;
        private System.Windows.Forms.DateTimePicker dteEnd;
    }
}
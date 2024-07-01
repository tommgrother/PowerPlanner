namespace ShopFloorPlacementPlanner
{
    partial class frmDepartmentLateAbsent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblLate = new System.Windows.Forms.Label();
            this.lblAbsent = new System.Windows.Forms.Label();
            this.dgvLate = new System.Windows.Forms.DataGridView();
            this.dgvAbsent = new System.Windows.Forms.DataGridView();
            this.lblDate = new System.Windows.Forms.Label();
            this.dteEnd = new System.Windows.Forms.DateTimePicker();
            this.dteStart = new System.Windows.Forms.DateTimePicker();
            this.lblStaff = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsent)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLate
            // 
            this.lblLate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLate.ForeColor = System.Drawing.Color.Red;
            this.lblLate.Location = new System.Drawing.Point(447, 74);
            this.lblLate.Name = "lblLate";
            this.lblLate.Size = new System.Drawing.Size(391, 28);
            this.lblLate.TabIndex = 13;
            this.lblLate.Text = "Total Late Days: 0";
            this.lblLate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblAbsent
            // 
            this.lblAbsent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsent.ForeColor = System.Drawing.Color.Red;
            this.lblAbsent.Location = new System.Drawing.Point(27, 74);
            this.lblAbsent.Name = "lblAbsent";
            this.lblAbsent.Size = new System.Drawing.Size(392, 28);
            this.lblAbsent.TabIndex = 12;
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLate.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLate.Location = new System.Drawing.Point(447, 105);
            this.dgvLate.Name = "dgvLate";
            this.dgvLate.ReadOnly = true;
            this.dgvLate.RowHeadersVisible = false;
            this.dgvLate.Size = new System.Drawing.Size(392, 717);
            this.dgvLate.TabIndex = 11;
            // 
            // dgvAbsent
            // 
            this.dgvAbsent.AllowUserToAddRows = false;
            this.dgvAbsent.AllowUserToDeleteRows = false;
            this.dgvAbsent.AllowUserToResizeColumns = false;
            this.dgvAbsent.AllowUserToResizeRows = false;
            this.dgvAbsent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAbsent.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAbsent.Location = new System.Drawing.Point(27, 105);
            this.dgvAbsent.Name = "dgvAbsent";
            this.dgvAbsent.ReadOnly = true;
            this.dgvAbsent.RowHeadersVisible = false;
            this.dgvAbsent.Size = new System.Drawing.Size(392, 717);
            this.dgvAbsent.TabIndex = 10;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(333, -5);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(436, 37);
            this.lblDate.TabIndex = 19;
            this.lblDate.Text = "Date Range";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dteEnd
            // 
            this.dteEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteEnd.Location = new System.Drawing.Point(578, 35);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Size = new System.Drawing.Size(191, 29);
            this.dteEnd.TabIndex = 17;
            this.dteEnd.CloseUp += new System.EventHandler(this.dteEnd_CloseUp);
            // 
            // dteStart
            // 
            this.dteStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteStart.Location = new System.Drawing.Point(333, 35);
            this.dteStart.Name = "dteStart";
            this.dteStart.Size = new System.Drawing.Size(208, 29);
            this.dteStart.TabIndex = 16;
            this.dteStart.CloseUp += new System.EventHandler(this.dteStart_CloseUp);
            // 
            // lblStaff
            // 
            this.lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(98, 8);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(216, 24);
            this.lblStaff.TabIndex = 15;
            this.lblStaff.Text = "Staff";
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(547, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 27);
            this.label1.TabIndex = 18;
            this.label1.Text = "to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbStaff
            // 
            this.cmbStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(98, 35);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(216, 32);
            this.cmbStaff.TabIndex = 20;
            this.cmbStaff.SelectedIndexChanged += new System.EventHandler(this.cmbStaff_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(779, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 21;
            this.button1.Text = "PRINT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmDepartmentLateAbsent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 834);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbStaff);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dteEnd);
            this.Controls.Add(this.dteStart);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLate);
            this.Controls.Add(this.lblAbsent);
            this.Controls.Add(this.dgvLate);
            this.Controls.Add(this.dgvAbsent);
            this.Name = "frmDepartmentLateAbsent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDepartmentLateAbsent";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLate;
        private System.Windows.Forms.Label lblAbsent;
        private System.Windows.Forms.DataGridView dgvLate;
        private System.Windows.Forms.DataGridView dgvAbsent;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dteEnd;
        private System.Windows.Forms.DateTimePicker dteStart;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.Button button1;
    }
}
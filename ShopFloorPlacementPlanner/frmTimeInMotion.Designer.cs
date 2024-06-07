namespace ShopFloorPlacementPlanner
{
    partial class frmTimeInMotion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimeInMotion));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dteStartDate = new System.Windows.Forms.DateTimePicker();
            this.dteEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkTimed = new System.Windows.Forms.CheckBox();
            this.chkNotTimed = new System.Windows.Forms.CheckBox();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(638, 774);
            this.dataGridView1.TabIndex = 0;
            // 
            // dteStartDate
            // 
            this.dteStartDate.Location = new System.Drawing.Point(185, 47);
            this.dteStartDate.Name = "dteStartDate";
            this.dteStartDate.Size = new System.Drawing.Size(140, 20);
            this.dteStartDate.TabIndex = 1;
            this.dteStartDate.CloseUp += new System.EventHandler(this.dteStartDate_CloseUp);
            // 
            // dteEndDate
            // 
            this.dteEndDate.Location = new System.Drawing.Point(340, 47);
            this.dteEndDate.Name = "dteEndDate";
            this.dteEndDate.Size = new System.Drawing.Size(140, 20);
            this.dteEndDate.TabIndex = 2;
            this.dteEndDate.CloseUp += new System.EventHandler(this.dteEndDate_CloseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.lblTitle.Location = new System.Drawing.Point(12, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(638, 23);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "time in motion";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkTimed
            // 
            this.chkTimed.AutoSize = true;
            this.chkTimed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.chkTimed.Location = new System.Drawing.Point(516, 19);
            this.chkTimed.Name = "chkTimed";
            this.chkTimed.Size = new System.Drawing.Size(108, 21);
            this.chkTimed.TabIndex = 4;
            this.chkTimed.Text = "Doors Timed";
            this.chkTimed.UseVisualStyleBackColor = true;
            this.chkTimed.CheckedChanged += new System.EventHandler(this.chkTimed_CheckedChanged);
            // 
            // chkNotTimed
            // 
            this.chkNotTimed.AutoSize = true;
            this.chkNotTimed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.chkNotTimed.Location = new System.Drawing.Point(516, 46);
            this.chkNotTimed.Name = "chkNotTimed";
            this.chkNotTimed.Size = new System.Drawing.Size(134, 21);
            this.chkNotTimed.TabIndex = 5;
            this.chkNotTimed.Text = "Doors Not Timed";
            this.chkNotTimed.UseVisualStyleBackColor = true;
            this.chkNotTimed.CheckedChanged += new System.EventHandler(this.chkNotTimed_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(76, 45);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmTimeInMotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 859);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chkNotTimed);
            this.Controls.Add(this.chkTimed);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dteEndDate);
            this.Controls.Add(this.dteStartDate);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTimeInMotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Time in Motion";
            this.Shown += new System.EventHandler(this.frmTimeInMotion_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dteStartDate;
        private System.Windows.Forms.DateTimePicker dteEndDate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkTimed;
        private System.Windows.Forms.CheckBox chkNotTimed;
        private System.Windows.Forms.Button btnPrint;
    }
}
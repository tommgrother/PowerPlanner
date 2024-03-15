namespace ShopFloorPlacementPlanner
{
    partial class frmAllocatedSetWorked
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dteDate = new System.Windows.Forms.DateTimePicker();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 120);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1013, 489);
            this.dataGridView1.TabIndex = 0;
            // 
            // dteDate
            // 
            this.dteDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteDate.Location = new System.Drawing.Point(434, 57);
            this.dteDate.Name = "dteDate";
            this.dteDate.Size = new System.Drawing.Size(200, 24);
            this.dteDate.TabIndex = 1;
            this.dteDate.CloseUp += new System.EventHandler(this.dteDate_CloseUp);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Items.AddRange(new object[] {
            "Bending",
            "Welding",
            "Buffing",
            "Packing"});
            this.cmbDepartment.Location = new System.Drawing.Point(214, 55);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(194, 26);
            this.cmbDepartment.TabIndex = 2;
            this.cmbDepartment.Text = "Packing";
            // 
            // frmAllocatedSetWorked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 621);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.dteDate);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmAllocatedSetWorked";
            this.Text = "frmAllocatedSetWorked";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dteDate;
        private System.Windows.Forms.ComboBox cmbDepartment;
    }
}
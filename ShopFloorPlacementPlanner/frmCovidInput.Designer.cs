namespace ShopFloorPlacementPlanner
{
    partial class frmCovidInput
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
            this.label25 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnY = new System.Windows.Forms.Button();
            this.btnN = new System.Windows.Forms.Button();
            this.lblButon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1223, 741);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(12, 18);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1223, 26);
            this.label25.TabIndex = 57;
            this.label25.Text = "Shop Floor ";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(1160, 794);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 58;
            this.btnGo.Text = "Upload!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(1064, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(171, 20);
            this.dateTimePicker1.TabIndex = 59;
            this.dateTimePicker1.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1064, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 20);
            this.label1.TabIndex = 60;
            this.label1.Text = "DATE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnY
            // 
            this.btnY.Location = new System.Drawing.Point(320, 22);
            this.btnY.Name = "btnY";
            this.btnY.Size = new System.Drawing.Size(45, 23);
            this.btnY.TabIndex = 61;
            this.btnY.Text = "Y";
            this.btnY.UseVisualStyleBackColor = true;
            this.btnY.Click += new System.EventHandler(this.btnY_Click);
            // 
            // btnN
            // 
            this.btnN.Location = new System.Drawing.Point(371, 22);
            this.btnN.Name = "btnN";
            this.btnN.Size = new System.Drawing.Size(50, 23);
            this.btnN.TabIndex = 62;
            this.btnN.Text = "N";
            this.btnN.UseVisualStyleBackColor = true;
            this.btnN.Click += new System.EventHandler(this.btnN_Click);
            // 
            // lblButon
            // 
            this.lblButon.AutoSize = true;
            this.lblButon.Location = new System.Drawing.Point(292, 8);
            this.lblButon.Name = "lblButon";
            this.lblButon.Size = new System.Drawing.Size(159, 13);
            this.lblButon.TabIndex = 63;
            this.lblButon.Text = "Mark all Mask/Ear Protection as";
            // 
            // frmCovidInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 829);
            this.Controls.Add(this.lblButon);
            this.Controls.Add(this.btnN);
            this.Controls.Add(this.btnY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCovidInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Covid Temp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCovidInput_FormClosing);
            this.Shown += new System.EventHandler(this.frmCovidInput_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnY;
        private System.Windows.Forms.Button btnN;
        private System.Windows.Forms.Label lblButon;
    }
}
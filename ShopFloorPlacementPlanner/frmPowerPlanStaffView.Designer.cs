namespace ShopFloorPlacementPlanner
{
    partial class frmPowerPlanStaffView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgPack = new System.Windows.Forms.DataGridView();
            this.dgBuff = new System.Windows.Forms.DataGridView();
            this.dgWeld = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dteDateSelection = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvBend = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgPack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWeld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBend)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPack
            // 
            this.dgPack.AllowUserToAddRows = false;
            this.dgPack.AllowUserToDeleteRows = false;
            this.dgPack.AllowUserToResizeColumns = false;
            this.dgPack.AllowUserToResizeRows = false;
            this.dgPack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgPack.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPack.Location = new System.Drawing.Point(1176, 91);
            this.dgPack.Name = "dgPack";
            this.dgPack.ReadOnly = true;
            this.dgPack.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPack.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPack.RowTemplate.Height = 50;
            this.dgPack.Size = new System.Drawing.Size(536, 652);
            this.dgPack.TabIndex = 8;
            this.dgPack.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPack_CellClick);
            // 
            // dgBuff
            // 
            this.dgBuff.AllowUserToAddRows = false;
            this.dgBuff.AllowUserToDeleteRows = false;
            this.dgBuff.AllowUserToResizeColumns = false;
            this.dgBuff.AllowUserToResizeRows = false;
            this.dgBuff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgBuff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgBuff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBuff.Location = new System.Drawing.Point(579, 487);
            this.dgBuff.Name = "dgBuff";
            this.dgBuff.ReadOnly = true;
            this.dgBuff.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgBuff.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgBuff.RowTemplate.Height = 50;
            this.dgBuff.Size = new System.Drawing.Size(536, 256);
            this.dgBuff.TabIndex = 7;
            this.dgBuff.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBuff_CellClick);
            // 
            // dgWeld
            // 
            this.dgWeld.AllowUserToAddRows = false;
            this.dgWeld.AllowUserToDeleteRows = false;
            this.dgWeld.AllowUserToResizeColumns = false;
            this.dgWeld.AllowUserToResizeRows = false;
            this.dgWeld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgWeld.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgWeld.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWeld.Location = new System.Drawing.Point(579, 91);
            this.dgWeld.Name = "dgWeld";
            this.dgWeld.ReadOnly = true;
            this.dgWeld.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgWeld.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgWeld.RowTemplate.Height = 50;
            this.dgWeld.Size = new System.Drawing.Size(536, 353);
            this.dgWeld.TabIndex = 6;
            this.dgWeld.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgWeld_CellClick);
            this.dgWeld.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgWeld_CellDoubleClick);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1177, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(536, 26);
            this.label6.TabIndex = 21;
            this.label6.Text = "Packing";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(579, 458);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(536, 26);
            this.label4.TabIndex = 20;
            this.label4.Text = "Buffing";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(579, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(536, 26);
            this.label3.TabIndex = 19;
            this.label3.Text = "Welding";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dteDateSelection
            // 
            this.dteDateSelection.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.dteDateSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.dteDateSelection.Location = new System.Drawing.Point(821, 12);
            this.dteDateSelection.Name = "dteDateSelection";
            this.dteDateSelection.Size = new System.Drawing.Size(154, 23);
            this.dteDateSelection.TabIndex = 22;
            this.dteDateSelection.CloseUp += new System.EventHandler(this.dteDateSelection_CloseUp);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(750, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 26);
            this.label1.TabIndex = 23;
            this.label1.Text = "Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btnPrint.Location = new System.Drawing.Point(981, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 24;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(536, 26);
            this.label5.TabIndex = 27;
            this.label5.Text = "Bending";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvBend
            // 
            this.dgvBend.AllowUserToAddRows = false;
            this.dgvBend.AllowUserToDeleteRows = false;
            this.dgvBend.AllowUserToResizeColumns = false;
            this.dgvBend.AllowUserToResizeRows = false;
            this.dgvBend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvBend.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBend.Location = new System.Drawing.Point(12, 91);
            this.dgvBend.Name = "dgvBend";
            this.dgvBend.ReadOnly = true;
            this.dgvBend.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBend.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBend.RowTemplate.Height = 50;
            this.dgvBend.Size = new System.Drawing.Size(536, 652);
            this.dgvBend.TabIndex = 25;
            // 
            // frmPowerPlanStaffView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1725, 755);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvBend);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dteDateSelection);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgPack);
            this.Controls.Add(this.dgBuff);
            this.Controls.Add(this.dgWeld);
            this.Name = "frmPowerPlanStaffView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPowerPlanStaffView";
            this.Shown += new System.EventHandler(this.frmPowerPlanStaffView_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgPack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWeld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPack;
        private System.Windows.Forms.DataGridView dgBuff;
        private System.Windows.Forms.DataGridView dgWeld;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dteDateSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvBend;
    }
}
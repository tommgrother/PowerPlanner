namespace ShopFloorPlacementPlanner
{
    partial class frmCopyWeek
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
            this.btnCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dteWeekCopyFrom = new System.Windows.Forms.DateTimePicker();
            this.dteWeekCopyTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCopy
            // 
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Location = new System.Drawing.Point(151, 117);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(113, 27);
            this.btnCopy.TabIndex = 0;
            this.btnCopy.Text = "Copy Week";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Copy Week From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dteWeekCopyFrom
            // 
            this.dteWeekCopyFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteWeekCopyFrom.Location = new System.Drawing.Point(16, 34);
            this.dteWeekCopyFrom.Name = "dteWeekCopyFrom";
            this.dteWeekCopyFrom.Size = new System.Drawing.Size(248, 26);
            this.dteWeekCopyFrom.TabIndex = 5;
            this.dteWeekCopyFrom.ValueChanged += new System.EventHandler(this.dteWeekCopyFrom_ValueChanged);
            // 
            // dteWeekCopyTo
            // 
            this.dteWeekCopyTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteWeekCopyTo.Location = new System.Drawing.Point(16, 85);
            this.dteWeekCopyTo.Name = "dteWeekCopyTo";
            this.dteWeekCopyTo.Size = new System.Drawing.Size(248, 26);
            this.dteWeekCopyTo.TabIndex = 7;
            this.dteWeekCopyTo.ValueChanged += new System.EventHandler(this.dteWeekCopyTo_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "TO:";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(16, 117);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(113, 27);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmCopyWeek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 166);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dteWeekCopyTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dteWeekCopyFrom);
            this.Controls.Add(this.btnCopy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCopyWeek";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Weekly Placements";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dteWeekCopyFrom;
        private System.Windows.Forms.DateTimePicker dteWeekCopyTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
    }
}
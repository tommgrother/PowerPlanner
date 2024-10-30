namespace ShopFloorPlacementPlanner
{
    partial class frmBendAvailable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBendAvailable));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBendingAvailable = new System.Windows.Forms.TextBox();
            this.txtBendingAllocated = new System.Windows.Forms.TextBox();
            this.txtWorkShakenOut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 79);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bending Available:\r\n(Complete in punch and not allocated)\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 79);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bending Allocated:\r\n(Complete in punch and allocated)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label3.Location = new System.Drawing.Point(12, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(380, 79);
            this.label3.TabIndex = 2;
            this.label3.Text = "Work Shaken Out:\r\n(Work marked as shaken out today)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBendingAvailable
            // 
            this.txtBendingAvailable.Enabled = false;
            this.txtBendingAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtBendingAvailable.Location = new System.Drawing.Point(398, 23);
            this.txtBendingAvailable.Name = "txtBendingAvailable";
            this.txtBendingAvailable.ReadOnly = true;
            this.txtBendingAvailable.Size = new System.Drawing.Size(109, 29);
            this.txtBendingAvailable.TabIndex = 3;
            // 
            // txtBendingAllocated
            // 
            this.txtBendingAllocated.Enabled = false;
            this.txtBendingAllocated.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtBendingAllocated.Location = new System.Drawing.Point(398, 122);
            this.txtBendingAllocated.Name = "txtBendingAllocated";
            this.txtBendingAllocated.ReadOnly = true;
            this.txtBendingAllocated.Size = new System.Drawing.Size(109, 29);
            this.txtBendingAllocated.TabIndex = 4;
            // 
            // txtWorkShakenOut
            // 
            this.txtWorkShakenOut.Enabled = false;
            this.txtWorkShakenOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtWorkShakenOut.Location = new System.Drawing.Point(398, 210);
            this.txtWorkShakenOut.Name = "txtWorkShakenOut";
            this.txtWorkShakenOut.ReadOnly = true;
            this.txtWorkShakenOut.Size = new System.Drawing.Size(109, 29);
            this.txtWorkShakenOut.TabIndex = 5;
            // 
            // frmBendAvailable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 292);
            this.Controls.Add(this.txtWorkShakenOut);
            this.Controls.Add(this.txtBendingAllocated);
            this.Controls.Add(this.txtBendingAvailable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBendAvailable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bending";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBendingAvailable;
        private System.Windows.Forms.TextBox txtBendingAllocated;
        private System.Windows.Forms.TextBox txtWorkShakenOut;
    }
}
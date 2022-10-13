namespace ShopFloorPlacementPlanner
{
    partial class frmTim
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
            this.lblPunch = new System.Windows.Forms.Label();
            this.lblBend = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPunch
            // 
            this.lblPunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.lblPunch.Location = new System.Drawing.Point(12, 21);
            this.lblPunch.Name = "lblPunch";
            this.lblPunch.Size = new System.Drawing.Size(165, 31);
            this.lblPunch.TabIndex = 0;
            this.lblPunch.Text = "Punching: 0.8";
            // 
            // lblBend
            // 
            this.lblBend.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.lblBend.Location = new System.Drawing.Point(12, 63);
            this.lblBend.Name = "lblBend";
            this.lblBend.Size = new System.Drawing.Size(165, 30);
            this.lblBend.TabIndex = 1;
            this.lblBend.Text = "Bending: 0.8";
            // 
            // frmTim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 109);
            this.Controls.Add(this.lblBend);
            this.Controls.Add(this.lblPunch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tim";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPunch;
        private System.Windows.Forms.Label lblBend;
    }
}
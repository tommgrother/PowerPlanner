namespace ShopFloorPlacementPlanner
{
    partial class frmBendingPress
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
            this.cmbPress1 = new System.Windows.Forms.ComboBox();
            this.cmbPress2 = new System.Windows.Forms.ComboBox();
            this.cmbPress3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbPress1
            // 
            this.cmbPress1.FormattingEnabled = true;
            this.cmbPress1.Location = new System.Drawing.Point(25, 38);
            this.cmbPress1.Name = "cmbPress1";
            this.cmbPress1.Size = new System.Drawing.Size(158, 21);
            this.cmbPress1.TabIndex = 0;
            this.cmbPress1.SelectedIndexChanged += new System.EventHandler(this.cmbPress1_SelectedIndexChanged);
            // 
            // cmbPress2
            // 
            this.cmbPress2.FormattingEnabled = true;
            this.cmbPress2.Location = new System.Drawing.Point(25, 79);
            this.cmbPress2.Name = "cmbPress2";
            this.cmbPress2.Size = new System.Drawing.Size(158, 21);
            this.cmbPress2.TabIndex = 1;
            this.cmbPress2.SelectedIndexChanged += new System.EventHandler(this.cmbPress2_SelectedIndexChanged);
            // 
            // cmbPress3
            // 
            this.cmbPress3.FormattingEnabled = true;
            this.cmbPress3.Location = new System.Drawing.Point(25, 120);
            this.cmbPress3.Name = "cmbPress3";
            this.cmbPress3.Size = new System.Drawing.Size(158, 21);
            this.cmbPress3.TabIndex = 2;
            this.cmbPress3.SelectedIndexChanged += new System.EventHandler(this.cmbPress3_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Press 1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(25, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Press 2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(25, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Press 3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(48, 147);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Update Press Users";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmBendingPress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 184);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPress3);
            this.Controls.Add(this.cmbPress2);
            this.Controls.Add(this.cmbPress1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBendingPress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asign Press Users";
            this.Shown += new System.EventHandler(this.frmBendingPress_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPress1;
        private System.Windows.Forms.ComboBox cmbPress2;
        private System.Windows.Forms.ComboBox cmbPress3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
    }
}
namespace ShopFloorPlacementPlanner
{
    partial class frmProductivityEmail
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
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.btnEmail = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkRichard = new System.Windows.Forms.CheckBox();
            this.chkDamian = new System.Windows.Forms.CheckBox();
            this.chkSimon = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 70);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(271, 142);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "";
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(150, 218);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(91, 23);
            this.btnEmail.TabIndex = 2;
            this.btnEmail.Text = "Send Email";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(53, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkRichard
            // 
            this.chkRichard.AutoSize = true;
            this.chkRichard.Location = new System.Drawing.Point(138, 26);
            this.chkRichard.Name = "chkRichard";
            this.chkRichard.Size = new System.Drawing.Size(63, 17);
            this.chkRichard.TabIndex = 11;
            this.chkRichard.Text = "Richard";
            this.chkRichard.UseVisualStyleBackColor = true;
            this.chkRichard.CheckedChanged += new System.EventHandler(this.chkRichard_CheckedChanged);
            // 
            // chkDamian
            // 
            this.chkDamian.AutoSize = true;
            this.chkDamian.Location = new System.Drawing.Point(215, 26);
            this.chkDamian.Name = "chkDamian";
            this.chkDamian.Size = new System.Drawing.Size(62, 17);
            this.chkDamian.TabIndex = 10;
            this.chkDamian.Text = "Damian";
            this.chkDamian.UseVisualStyleBackColor = true;
            this.chkDamian.CheckedChanged += new System.EventHandler(this.chkDamian_CheckedChanged);
            // 
            // chkSimon
            // 
            this.chkSimon.AutoSize = true;
            this.chkSimon.Location = new System.Drawing.Point(69, 26);
            this.chkSimon.Name = "chkSimon";
            this.chkSimon.Size = new System.Drawing.Size(55, 17);
            this.chkSimon.TabIndex = 9;
            this.chkSimon.Text = "Simon";
            this.chkSimon.UseVisualStyleBackColor = true;
            this.chkSimon.CheckedChanged += new System.EventHandler(this.chkSimon_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(18, 26);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(37, 17);
            this.chkAll.TabIndex = 8;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // frmProductivityEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 250);
            this.Controls.Add(this.chkRichard);
            this.Controls.Add(this.chkDamian);
            this.Controls.Add(this.chkSimon);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.txtMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductivityEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Productivity Email";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkRichard;
        private System.Windows.Forms.CheckBox chkDamian;
        private System.Windows.Forms.CheckBox chkSimon;
        private System.Windows.Forms.CheckBox chkAll;
    }
}
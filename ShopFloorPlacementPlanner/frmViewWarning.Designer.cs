namespace ShopFloorPlacementPlanner
{
    partial class frmViewWarning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewWarning));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.txtWarning = new System.Windows.Forms.RichTextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtDateGiven = new System.Windows.Forms.TextBox();
            this.txtDateLogged = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWarningGivenBy = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabControl.Location = new System.Drawing.Point(12, 95);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(594, 30);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.label1.Location = new System.Drawing.Point(216, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Staff";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbStaff
            // 
            this.cmbStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(216, 43);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(186, 28);
            this.cmbStaff.TabIndex = 5;
            this.cmbStaff.SelectedIndexChanged += new System.EventHandler(this.cmbStaff_SelectedIndexChanged);
            // 
            // txtWarning
            // 
            this.txtWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtWarning.Location = new System.Drawing.Point(12, 122);
            this.txtWarning.Name = "txtWarning";
            this.txtWarning.Size = new System.Drawing.Size(596, 453);
            this.txtWarning.TabIndex = 7;
            this.txtWarning.Text = "";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtDepartment.Location = new System.Drawing.Point(107, 601);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(186, 26);
            this.txtDepartment.TabIndex = 8;
            // 
            // txtDateGiven
            // 
            this.txtDateGiven.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtDateGiven.Location = new System.Drawing.Point(326, 601);
            this.txtDateGiven.Name = "txtDateGiven";
            this.txtDateGiven.Size = new System.Drawing.Size(186, 26);
            this.txtDateGiven.TabIndex = 9;
            // 
            // txtDateLogged
            // 
            this.txtDateLogged.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtDateLogged.Location = new System.Drawing.Point(326, 674);
            this.txtDateLogged.Name = "txtDateLogged";
            this.txtDateLogged.Size = new System.Drawing.Size(186, 26);
            this.txtDateLogged.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(107, 578);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Department";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(326, 578);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Date Given";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.Location = new System.Drawing.Point(326, 651);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 23);
            this.label4.TabIndex = 13;
            this.label4.Text = "Date Logged";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label5.Location = new System.Drawing.Point(107, 651);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Warning Given By";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWarningGivenBy
            // 
            this.txtWarningGivenBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtWarningGivenBy.Location = new System.Drawing.Point(107, 674);
            this.txtWarningGivenBy.Name = "txtWarningGivenBy";
            this.txtWarningGivenBy.Size = new System.Drawing.Size(186, 26);
            this.txtWarningGivenBy.TabIndex = 14;
            // 
            // frmViewWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 710);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWarningGivenBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateLogged);
            this.Controls.Add(this.txtDateGiven);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.txtWarning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStaff);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewWarning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staff Warning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.RichTextBox txtWarning;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtDateGiven;
        private System.Windows.Forms.TextBox txtDateLogged;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWarningGivenBy;
    }
}
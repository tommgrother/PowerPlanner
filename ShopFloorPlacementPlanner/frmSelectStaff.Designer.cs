﻿namespace ShopFloorPlacementPlanner
{
    partial class frmSelectStaff
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectStaff));
            this.lstStaff = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgSelected = new System.Windows.Forms.DataGridView();
            this.StaffID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StaffFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlacementType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOvertime = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // lstStaff
            // 
            this.lstStaff.FormattingEnabled = true;
            this.lstStaff.Location = new System.Drawing.Point(12, 92);
            this.lstStaff.Name = "lstStaff";
            this.lstStaff.Size = new System.Drawing.Size(170, 394);
            this.lstStaff.TabIndex = 0;
            this.lstStaff.SelectedIndexChanged += new System.EventHandler(this.lstStaff_SelectedIndexChanged);
            this.lstStaff.DoubleClick += new System.EventHandler(this.lstStaff_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select from staff:";
            // 
            // dgSelected
            // 
            this.dgSelected.AllowUserToAddRows = false;
            this.dgSelected.AllowUserToResizeColumns = false;
            this.dgSelected.AllowUserToResizeRows = false;
            this.dgSelected.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSelected.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StaffID,
            this.StaffFullName,
            this.PlacementType,
            this.Hours});
            this.dgSelected.Location = new System.Drawing.Point(189, 69);
            this.dgSelected.Name = "dgSelected";
            this.dgSelected.RowHeadersVisible = false;
            this.dgSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSelected.Size = new System.Drawing.Size(777, 417);
            this.dgSelected.TabIndex = 2;
            this.dgSelected.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSelected_CellContentClick);
            // 
            // StaffID
            // 
            this.StaffID.HeaderText = "Staff ID";
            this.StaffID.Name = "StaffID";
            // 
            // StaffFullName
            // 
            this.StaffFullName.HeaderText = "Full Name";
            this.StaffFullName.Name = "StaffFullName";
            // 
            // PlacementType
            // 
            this.PlacementType.HeaderText = "Placement Type";
            this.PlacementType.Items.AddRange(new object[] {
            "Full Day",
            "Half Day",
            "Shift"});
            this.PlacementType.Name = "PlacementType";
            // 
            // Hours
            // 
            this.Hours.HeaderText = "Hours";
            this.Hours.Name = "Hours";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 13);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(51, 20);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "label2";
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage2.Location = new System.Drawing.Point(12, 33);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(46, 17);
            this.lblMessage2.TabIndex = 5;
            this.lblMessage2.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(747, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Section Overtime:";
            // 
            // txtOvertime
            // 
            this.txtOvertime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOvertime.Location = new System.Drawing.Point(900, 27);
            this.txtOvertime.Name = "txtOvertime";
            this.txtOvertime.Size = new System.Drawing.Size(66, 26);
            this.txtOvertime.TabIndex = 7;
            this.txtOvertime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmSelectStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 530);
            this.Controls.Add(this.txtOvertime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMessage2);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dgSelected);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstStaff);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectStaff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Staff";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSelectStaff_FormClosed);
            this.Load += new System.EventHandler(this.frmSelectStaff_Load);
            this.Leave += new System.EventHandler(this.frmSelectStaff_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.dgSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstStaff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgSelected;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaffID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaffFullName;
        private System.Windows.Forms.DataGridViewComboBoxColumn PlacementType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOvertime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
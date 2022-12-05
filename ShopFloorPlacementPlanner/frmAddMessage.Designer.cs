namespace ShopFloorPlacementPlanner
{
    partial class frmAddMessage
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
            this.txtNote = new System.Windows.Forms.RichTextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkSimon = new System.Windows.Forms.CheckBox();
            this.chkDamian = new System.Windows.Forms.CheckBox();
            this.chkRichard = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(12, 31);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(430, 189);
            this.txtNote.TabIndex = 0;
            this.txtNote.Text = "";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 19);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Please Enter Your Note";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(230, 249);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add Note";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(149, 249);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel Note";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(98, 226);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(37, 17);
            this.chkAll.TabIndex = 4;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkSimon
            // 
            this.chkSimon.AutoSize = true;
            this.chkSimon.Location = new System.Drawing.Point(149, 226);
            this.chkSimon.Name = "chkSimon";
            this.chkSimon.Size = new System.Drawing.Size(55, 17);
            this.chkSimon.TabIndex = 5;
            this.chkSimon.Text = "Simon";
            this.chkSimon.UseVisualStyleBackColor = true;
            this.chkSimon.CheckedChanged += new System.EventHandler(this.chkSimon_CheckedChanged);
            // 
            // chkDamian
            // 
            this.chkDamian.AutoSize = true;
            this.chkDamian.Location = new System.Drawing.Point(295, 226);
            this.chkDamian.Name = "chkDamian";
            this.chkDamian.Size = new System.Drawing.Size(62, 17);
            this.chkDamian.TabIndex = 6;
            this.chkDamian.Text = "Damian";
            this.chkDamian.UseVisualStyleBackColor = true;
            this.chkDamian.CheckedChanged += new System.EventHandler(this.chkDamian_CheckedChanged);
            // 
            // chkRichard
            // 
            this.chkRichard.AutoSize = true;
            this.chkRichard.Location = new System.Drawing.Point(218, 226);
            this.chkRichard.Name = "chkRichard";
            this.chkRichard.Size = new System.Drawing.Size(63, 17);
            this.chkRichard.TabIndex = 7;
            this.chkRichard.Text = "Richard";
            this.chkRichard.UseVisualStyleBackColor = true;
            this.chkRichard.CheckedChanged += new System.EventHandler(this.chkRichard_CheckedChanged);
            // 
            // frmAddMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 283);
            this.Controls.Add(this.chkRichard);
            this.Controls.Add(this.chkDamian);
            this.Controls.Add(this.chkSimon);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtNote);
            this.Name = "frmAddMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtNote;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkSimon;
        private System.Windows.Forms.CheckBox chkDamian;
        private System.Windows.Forms.CheckBox chkRichard;
    }
}
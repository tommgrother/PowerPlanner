namespace ShopFloorPlacementPlanner
{
    partial class frmDepartmentActivityTracker
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
            this.dteStart = new System.Windows.Forms.DateTimePicker();
            this.dteEnd = new System.Windows.Forms.DateTimePicker();
            this.txtPunching = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBending = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWelding = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBuffing = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPainting = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPacking = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dteStart
            // 
            this.dteStart.Location = new System.Drawing.Point(267, 11);
            this.dteStart.Name = "dteStart";
            this.dteStart.Size = new System.Drawing.Size(119, 20);
            this.dteStart.TabIndex = 0;
            this.dteStart.CloseUp += new System.EventHandler(this.dteStart_CloseUp);
            // 
            // dteEnd
            // 
            this.dteEnd.Location = new System.Drawing.Point(267, 37);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Size = new System.Drawing.Size(119, 20);
            this.dteEnd.TabIndex = 1;
            this.dteEnd.CloseUp += new System.EventHandler(this.dteEnd_CloseUp);
            // 
            // txtPunching
            // 
            this.txtPunching.Location = new System.Drawing.Point(12, 89);
            this.txtPunching.Name = "txtPunching";
            this.txtPunching.Size = new System.Drawing.Size(100, 20);
            this.txtPunching.TabIndex = 2;
            this.txtPunching.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPunching.Click += new System.EventHandler(this.txtPunching_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Punching (%)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(118, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bending (%)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtBending
            // 
            this.txtBending.Location = new System.Drawing.Point(118, 89);
            this.txtBending.Name = "txtBending";
            this.txtBending.Size = new System.Drawing.Size(100, 20);
            this.txtBending.TabIndex = 4;
            this.txtBending.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBending.Click += new System.EventHandler(this.txtBending_Click);
            this.txtBending.TextChanged += new System.EventHandler(this.txtBending_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(224, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Welding (%)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtWelding
            // 
            this.txtWelding.Location = new System.Drawing.Point(224, 89);
            this.txtWelding.Name = "txtWelding";
            this.txtWelding.Size = new System.Drawing.Size(100, 20);
            this.txtWelding.TabIndex = 6;
            this.txtWelding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWelding.Click += new System.EventHandler(this.txtWelding_Click);
            this.txtWelding.TextChanged += new System.EventHandler(this.txtWelding_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(330, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Buffing (%)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtBuffing
            // 
            this.txtBuffing.Location = new System.Drawing.Point(330, 89);
            this.txtBuffing.Name = "txtBuffing";
            this.txtBuffing.Size = new System.Drawing.Size(100, 20);
            this.txtBuffing.TabIndex = 8;
            this.txtBuffing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuffing.Click += new System.EventHandler(this.txtBuffing_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label5.Location = new System.Drawing.Point(436, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Painting (%)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtPainting
            // 
            this.txtPainting.Location = new System.Drawing.Point(436, 89);
            this.txtPainting.Name = "txtPainting";
            this.txtPainting.Size = new System.Drawing.Size(100, 20);
            this.txtPainting.TabIndex = 10;
            this.txtPainting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPainting.Click += new System.EventHandler(this.txtPainting_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label6.Location = new System.Drawing.Point(542, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Packing (%)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtPacking
            // 
            this.txtPacking.Location = new System.Drawing.Point(542, 89);
            this.txtPacking.Name = "txtPacking";
            this.txtPacking.Size = new System.Drawing.Size(100, 20);
            this.txtPacking.TabIndex = 12;
            this.txtPacking.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPacking.Click += new System.EventHandler(this.txtPacking_Click);
            // 
            // frmDepartmentActivityTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 121);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPacking);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPainting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBuffing);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWelding);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBending);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPunching);
            this.Controls.Add(this.dteEnd);
            this.Controls.Add(this.dteStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDepartmentActivityTracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Department Activity Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dteStart;
        private System.Windows.Forms.DateTimePicker dteEnd;
        private System.Windows.Forms.TextBox txtPunching;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBending;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWelding;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBuffing;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPainting;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPacking;
    }
}
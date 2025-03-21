namespace ShopFloorPlacementPlanner
{
    partial class frmMoveTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMoveTime));
            this.lblDoor = new System.Windows.Forms.Label();
            this.txtTimeToMove = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDoor
            // 
            this.lblDoor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lblDoor.Location = new System.Drawing.Point(12, 9);
            this.lblDoor.Name = "lblDoor";
            this.lblDoor.Size = new System.Drawing.Size(290, 23);
            this.lblDoor.TabIndex = 0;
            this.lblDoor.Text = "Door ID: ";
            this.lblDoor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimeToMove
            // 
            this.txtTimeToMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtTimeToMove.Location = new System.Drawing.Point(79, 109);
            this.txtTimeToMove.Name = "txtTimeToMove";
            this.txtTimeToMove.Size = new System.Drawing.Size(156, 23);
            this.txtTimeToMove.TabIndex = 1;
            this.txtTimeToMove.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeToMove_KeyPress);
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lblTime.Location = new System.Drawing.Point(12, 44);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(290, 23);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Current Time: (minutes)";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(301, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Enter the number of minutes you want to move";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(79, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(160, 143);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 5;
            this.btnMove.Text = "MOVE TIME";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // frmMoveTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 178);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtTimeToMove);
            this.Controls.Add(this.lblDoor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMoveTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Move Time";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDoor;
        private System.Windows.Forms.TextBox txtTimeToMove;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMove;
    }
}
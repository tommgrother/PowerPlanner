namespace ShopFloorPlacementPlanner
{
    partial class MenuMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuMain));
            this.dgPunch = new System.Windows.Forms.DataGridView();
            this.dgBend = new System.Windows.Forms.DataGridView();
            this.dgWeld = new System.Windows.Forms.DataGridView();
            this.dgBuff = new System.Windows.Forms.DataGridView();
            this.dgPaint = new System.Windows.Forms.DataGridView();
            this.dgPack = new System.Windows.Forms.DataGridView();
            this.dteDateSelection = new System.Windows.Forms.DateTimePicker();
            this.btnAddPunch = new System.Windows.Forms.Button();
            this.btnAddBend = new System.Windows.Forms.Button();
            this.btnAddWeld = new System.Windows.Forms.Button();
            this.btnAddBuff = new System.Windows.Forms.Button();
            this.btnAddPaint = new System.Windows.Forms.Button();
            this.btnAddPack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgNotPlaced = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddLaser = new System.Windows.Forms.Button();
            this.dgLaser = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgPunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWeld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotPlaced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLaser)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPunch
            // 
            this.dgPunch.AllowUserToAddRows = false;
            this.dgPunch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgPunch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPunch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPunch.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgPunch.Enabled = false;
            this.dgPunch.Location = new System.Drawing.Point(13, 97);
            this.dgPunch.Name = "dgPunch";
            this.dgPunch.RowHeadersVisible = false;
            this.dgPunch.RowTemplate.Height = 40;
            this.dgPunch.Size = new System.Drawing.Size(200, 409);
            this.dgPunch.TabIndex = 0;
            // 
            // dgBend
            // 
            this.dgBend.AllowUserToAddRows = false;
            this.dgBend.AllowUserToDeleteRows = false;
            this.dgBend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgBend.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgBend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBend.Enabled = false;
            this.dgBend.Location = new System.Drawing.Point(426, 97);
            this.dgBend.Name = "dgBend";
            this.dgBend.RowHeadersVisible = false;
            this.dgBend.RowTemplate.Height = 40;
            this.dgBend.Size = new System.Drawing.Size(200, 409);
            this.dgBend.TabIndex = 1;
            // 
            // dgWeld
            // 
            this.dgWeld.AllowUserToAddRows = false;
            this.dgWeld.AllowUserToDeleteRows = false;
            this.dgWeld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgWeld.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgWeld.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWeld.Enabled = false;
            this.dgWeld.Location = new System.Drawing.Point(632, 97);
            this.dgWeld.Name = "dgWeld";
            this.dgWeld.RowHeadersVisible = false;
            this.dgWeld.RowTemplate.Height = 40;
            this.dgWeld.Size = new System.Drawing.Size(200, 409);
            this.dgWeld.TabIndex = 2;
            // 
            // dgBuff
            // 
            this.dgBuff.AllowUserToAddRows = false;
            this.dgBuff.AllowUserToDeleteRows = false;
            this.dgBuff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgBuff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgBuff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBuff.Enabled = false;
            this.dgBuff.Location = new System.Drawing.Point(838, 97);
            this.dgBuff.Name = "dgBuff";
            this.dgBuff.RowHeadersVisible = false;
            this.dgBuff.RowTemplate.Height = 40;
            this.dgBuff.Size = new System.Drawing.Size(200, 409);
            this.dgBuff.TabIndex = 3;
            // 
            // dgPaint
            // 
            this.dgPaint.AllowUserToAddRows = false;
            this.dgPaint.AllowUserToDeleteRows = false;
            this.dgPaint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgPaint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPaint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPaint.Enabled = false;
            this.dgPaint.Location = new System.Drawing.Point(1044, 97);
            this.dgPaint.Name = "dgPaint";
            this.dgPaint.RowHeadersVisible = false;
            this.dgPaint.RowTemplate.Height = 40;
            this.dgPaint.Size = new System.Drawing.Size(225, 409);
            this.dgPaint.TabIndex = 4;
            // 
            // dgPack
            // 
            this.dgPack.AllowUserToAddRows = false;
            this.dgPack.AllowUserToDeleteRows = false;
            this.dgPack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgPack.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPack.Enabled = false;
            this.dgPack.Location = new System.Drawing.Point(1282, 97);
            this.dgPack.Name = "dgPack";
            this.dgPack.RowHeadersVisible = false;
            this.dgPack.RowTemplate.Height = 40;
            this.dgPack.Size = new System.Drawing.Size(200, 409);
            this.dgPack.TabIndex = 5;
            // 
            // dteDateSelection
            // 
            this.dteDateSelection.Location = new System.Drawing.Point(1667, 12);
            this.dteDateSelection.Name = "dteDateSelection";
            this.dteDateSelection.Size = new System.Drawing.Size(154, 20);
            this.dteDateSelection.TabIndex = 6;
            this.dteDateSelection.ValueChanged += new System.EventHandler(this.dteDateSelection_ValueChanged);
            // 
            // btnAddPunch
            // 
            this.btnAddPunch.Location = new System.Drawing.Point(156, 68);
            this.btnAddPunch.Name = "btnAddPunch";
            this.btnAddPunch.Size = new System.Drawing.Size(57, 23);
            this.btnAddPunch.TabIndex = 7;
            this.btnAddPunch.Text = "Update";
            this.btnAddPunch.UseVisualStyleBackColor = true;
            this.btnAddPunch.Click += new System.EventHandler(this.btnAddPunch_Click);
            // 
            // btnAddBend
            // 
            this.btnAddBend.Location = new System.Drawing.Point(569, 68);
            this.btnAddBend.Name = "btnAddBend";
            this.btnAddBend.Size = new System.Drawing.Size(57, 23);
            this.btnAddBend.TabIndex = 8;
            this.btnAddBend.Text = "Update";
            this.btnAddBend.UseVisualStyleBackColor = true;
            this.btnAddBend.Click += new System.EventHandler(this.btnAddBend_Click);
            // 
            // btnAddWeld
            // 
            this.btnAddWeld.Location = new System.Drawing.Point(775, 68);
            this.btnAddWeld.Name = "btnAddWeld";
            this.btnAddWeld.Size = new System.Drawing.Size(57, 23);
            this.btnAddWeld.TabIndex = 9;
            this.btnAddWeld.Text = "Update";
            this.btnAddWeld.UseVisualStyleBackColor = true;
            this.btnAddWeld.Click += new System.EventHandler(this.btnAddWeld_Click);
            // 
            // btnAddBuff
            // 
            this.btnAddBuff.Location = new System.Drawing.Point(981, 68);
            this.btnAddBuff.Name = "btnAddBuff";
            this.btnAddBuff.Size = new System.Drawing.Size(57, 23);
            this.btnAddBuff.TabIndex = 10;
            this.btnAddBuff.Text = "Update";
            this.btnAddBuff.UseVisualStyleBackColor = true;
            this.btnAddBuff.Click += new System.EventHandler(this.btnAddBuff_Click);
            // 
            // btnAddPaint
            // 
            this.btnAddPaint.Location = new System.Drawing.Point(1212, 68);
            this.btnAddPaint.Name = "btnAddPaint";
            this.btnAddPaint.Size = new System.Drawing.Size(57, 23);
            this.btnAddPaint.TabIndex = 11;
            this.btnAddPaint.Text = "Update";
            this.btnAddPaint.UseVisualStyleBackColor = true;
            this.btnAddPaint.Click += new System.EventHandler(this.btnAddPaint_Click);
            // 
            // btnAddPack
            // 
            this.btnAddPack.Location = new System.Drawing.Point(1425, 68);
            this.btnAddPack.Name = "btnAddPack";
            this.btnAddPack.Size = new System.Drawing.Size(57, 23);
            this.btnAddPack.TabIndex = 12;
            this.btnAddPack.Text = "Update";
            this.btnAddPack.UseVisualStyleBackColor = true;
            this.btnAddPack.Click += new System.EventHandler(this.btnAddPack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 26);
            this.label1.TabIndex = 13;
            this.label1.Text = "Punching";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(471, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 26);
            this.label2.TabIndex = 14;
            this.label2.Text = "Bending";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(678, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 26);
            this.label3.TabIndex = 15;
            this.label3.Text = "Welding";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(895, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 26);
            this.label4.TabIndex = 16;
            this.label4.Text = "Buffing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1115, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 26);
            this.label5.TabIndex = 17;
            this.label5.Text = "Painting";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1329, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 26);
            this.label6.TabIndex = 18;
            this.label6.Text = "Packing";
            // 
            // dgNotPlaced
            // 
            this.dgNotPlaced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgNotPlaced.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgNotPlaced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotPlaced.Location = new System.Drawing.Point(1566, 97);
            this.dgNotPlaced.Name = "dgNotPlaced";
            this.dgNotPlaced.RowHeadersVisible = false;
            this.dgNotPlaced.Size = new System.Drawing.Size(255, 409);
            this.dgNotPlaced.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(1639, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 26);
            this.label7.TabIndex = 20;
            this.label7.Text = "Not Placed";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 568);
            this.splitter1.TabIndex = 21;
            this.splitter1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(290, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 26);
            this.label8.TabIndex = 24;
            this.label8.Text = "Laser";
            // 
            // btnAddLaser
            // 
            this.btnAddLaser.Location = new System.Drawing.Point(362, 68);
            this.btnAddLaser.Name = "btnAddLaser";
            this.btnAddLaser.Size = new System.Drawing.Size(57, 23);
            this.btnAddLaser.TabIndex = 23;
            this.btnAddLaser.Text = "Update";
            this.btnAddLaser.UseVisualStyleBackColor = true;
            this.btnAddLaser.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgLaser
            // 
            this.dgLaser.AllowUserToAddRows = false;
            this.dgLaser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgLaser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgLaser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLaser.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgLaser.Enabled = false;
            this.dgLaser.Location = new System.Drawing.Point(219, 97);
            this.dgLaser.Name = "dgLaser";
            this.dgLaser.RowHeadersVisible = false;
            this.dgLaser.RowTemplate.Height = 40;
            this.dgLaser.Size = new System.Drawing.Size(200, 409);
            this.dgLaser.TabIndex = 22;
            // 
            // MenuMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1832, 568);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnAddLaser);
            this.Controls.Add(this.dgLaser);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgNotPlaced);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddPack);
            this.Controls.Add(this.btnAddPaint);
            this.Controls.Add(this.btnAddBuff);
            this.Controls.Add(this.btnAddWeld);
            this.Controls.Add(this.btnAddBend);
            this.Controls.Add(this.btnAddPunch);
            this.Controls.Add(this.dteDateSelection);
            this.Controls.Add(this.dgPack);
            this.Controls.Add(this.dgPaint);
            this.Controls.Add(this.dgBuff);
            this.Controls.Add(this.dgWeld);
            this.Controls.Add(this.dgBend);
            this.Controls.Add(this.dgPunch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuMain";
            this.Text = "PowerPlanner";
            this.Load += new System.EventHandler(this.MenuMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPunch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWeld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotPlaced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLaser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPunch;
        private System.Windows.Forms.DataGridView dgBend;
        private System.Windows.Forms.DataGridView dgWeld;
        private System.Windows.Forms.DataGridView dgBuff;
        private System.Windows.Forms.DataGridView dgPaint;
        private System.Windows.Forms.DataGridView dgPack;
        private System.Windows.Forms.DateTimePicker dteDateSelection;
        private System.Windows.Forms.Button btnAddPunch;
        private System.Windows.Forms.Button btnAddBend;
        private System.Windows.Forms.Button btnAddWeld;
        private System.Windows.Forms.Button btnAddBuff;
        private System.Windows.Forms.Button btnAddPaint;
        private System.Windows.Forms.Button btnAddPack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgNotPlaced;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddLaser;
        private System.Windows.Forms.DataGridView dgLaser;
    }
}


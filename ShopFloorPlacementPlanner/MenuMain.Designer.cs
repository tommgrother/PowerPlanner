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
            this.txtPunchMen = new System.Windows.Forms.TextBox();
            this.txtPunchHours = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLaserHours = new System.Windows.Forms.TextBox();
            this.txtLaserMen = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBendHours = new System.Windows.Forms.TextBox();
            this.txtBendMen = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtWeldHours = new System.Windows.Forms.TextBox();
            this.txtWeldMen = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBuffHours = new System.Windows.Forms.TextBox();
            this.txtBuffMen = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPaintHours = new System.Windows.Forms.TextBox();
            this.txtPaintMen = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPackHours = new System.Windows.Forms.TextBox();
            this.txtPackMen = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.copyPlacementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgPunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWeld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotPlaced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLaser)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.dgNotPlaced.Location = new System.Drawing.Point(1621, 93);
            this.dgNotPlaced.Name = "dgNotPlaced";
            this.dgNotPlaced.RowHeadersVisible = false;
            this.dgNotPlaced.Size = new System.Drawing.Size(200, 409);
            this.dgNotPlaced.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(1662, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 26);
            this.label7.TabIndex = 20;
            this.label7.Text = "Not Placed";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 544);
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
            // txtPunchMen
            // 
            this.txtPunchMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPunchMen.Location = new System.Drawing.Point(49, 512);
            this.txtPunchMen.Name = "txtPunchMen";
            this.txtPunchMen.Size = new System.Drawing.Size(60, 20);
            this.txtPunchMen.TabIndex = 25;
            // 
            // txtPunchHours
            // 
            this.txtPunchHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPunchHours.Location = new System.Drawing.Point(153, 512);
            this.txtPunchHours.Name = "txtPunchHours";
            this.txtPunchHours.Size = new System.Drawing.Size(60, 20);
            this.txtPunchHours.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 515);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Men:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(115, 515);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Hours:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(322, 515);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Hours:";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(219, 515);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Men:";
            // 
            // txtLaserHours
            // 
            this.txtLaserHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLaserHours.Location = new System.Drawing.Point(360, 512);
            this.txtLaserHours.Name = "txtLaserHours";
            this.txtLaserHours.Size = new System.Drawing.Size(60, 20);
            this.txtLaserHours.TabIndex = 30;
            // 
            // txtLaserMen
            // 
            this.txtLaserMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLaserMen.Location = new System.Drawing.Point(256, 512);
            this.txtLaserMen.Name = "txtLaserMen";
            this.txtLaserMen.Size = new System.Drawing.Size(60, 20);
            this.txtLaserMen.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(527, 515);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Hours:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(424, 515);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 35;
            this.label14.Text = "Men:";
            // 
            // txtBendHours
            // 
            this.txtBendHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBendHours.Location = new System.Drawing.Point(565, 512);
            this.txtBendHours.Name = "txtBendHours";
            this.txtBendHours.Size = new System.Drawing.Size(60, 20);
            this.txtBendHours.TabIndex = 34;
            // 
            // txtBendMen
            // 
            this.txtBendMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBendMen.Location = new System.Drawing.Point(461, 512);
            this.txtBendMen.Name = "txtBendMen";
            this.txtBendMen.Size = new System.Drawing.Size(60, 20);
            this.txtBendMen.TabIndex = 33;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(734, 515);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Hours:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(631, 515);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 39;
            this.label16.Text = "Men:";
            // 
            // txtWeldHours
            // 
            this.txtWeldHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWeldHours.Location = new System.Drawing.Point(772, 512);
            this.txtWeldHours.Name = "txtWeldHours";
            this.txtWeldHours.Size = new System.Drawing.Size(60, 20);
            this.txtWeldHours.TabIndex = 38;
            // 
            // txtWeldMen
            // 
            this.txtWeldMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWeldMen.Location = new System.Drawing.Point(668, 512);
            this.txtWeldMen.Name = "txtWeldMen";
            this.txtWeldMen.Size = new System.Drawing.Size(60, 20);
            this.txtWeldMen.TabIndex = 37;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(941, 515);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Hours:";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(838, 515);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 43;
            this.label18.Text = "Men:";
            // 
            // txtBuffHours
            // 
            this.txtBuffHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBuffHours.Location = new System.Drawing.Point(979, 512);
            this.txtBuffHours.Name = "txtBuffHours";
            this.txtBuffHours.Size = new System.Drawing.Size(60, 20);
            this.txtBuffHours.TabIndex = 42;
            // 
            // txtBuffMen
            // 
            this.txtBuffMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBuffMen.Location = new System.Drawing.Point(875, 512);
            this.txtBuffMen.Name = "txtBuffMen";
            this.txtBuffMen.Size = new System.Drawing.Size(60, 20);
            this.txtBuffMen.TabIndex = 41;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1168, 515);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 48;
            this.label19.Text = "Hours:";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1065, 515);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 47;
            this.label20.Text = "Men:";
            // 
            // txtPaintHours
            // 
            this.txtPaintHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPaintHours.Location = new System.Drawing.Point(1206, 512);
            this.txtPaintHours.Name = "txtPaintHours";
            this.txtPaintHours.Size = new System.Drawing.Size(60, 20);
            this.txtPaintHours.TabIndex = 46;
            // 
            // txtPaintMen
            // 
            this.txtPaintMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPaintMen.Location = new System.Drawing.Point(1102, 512);
            this.txtPaintMen.Name = "txtPaintMen";
            this.txtPaintMen.Size = new System.Drawing.Size(60, 20);
            this.txtPaintMen.TabIndex = 45;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1384, 515);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 13);
            this.label21.TabIndex = 52;
            this.label21.Text = "Hours:";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(1281, 515);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 51;
            this.label22.Text = "Men:";
            // 
            // txtPackHours
            // 
            this.txtPackHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPackHours.Location = new System.Drawing.Point(1422, 512);
            this.txtPackHours.Name = "txtPackHours";
            this.txtPackHours.Size = new System.Drawing.Size(60, 20);
            this.txtPackHours.TabIndex = 50;
            // 
            // txtPackMen
            // 
            this.txtPackMen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPackMen.Location = new System.Drawing.Point(1318, 512);
            this.txtPackMen.Name = "txtPackMen";
            this.txtPackMen.Size = new System.Drawing.Size(60, 20);
            this.txtPackMen.TabIndex = 49;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPlacementsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1832, 24);
            this.menuStrip1.TabIndex = 53;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // copyPlacementsToolStripMenuItem
            // 
            this.copyPlacementsToolStripMenuItem.Name = "copyPlacementsToolStripMenuItem";
            this.copyPlacementsToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.copyPlacementsToolStripMenuItem.Text = "Copy Placements";
            this.copyPlacementsToolStripMenuItem.Click += new System.EventHandler(this.copyPlacementsToolStripMenuItem_Click);
            // 
            // MenuMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1832, 568);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtPackHours);
            this.Controls.Add(this.txtPackMen);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtPaintHours);
            this.Controls.Add(this.txtPaintMen);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtBuffHours);
            this.Controls.Add(this.txtBuffMen);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtWeldHours);
            this.Controls.Add(this.txtWeldMen);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtBendHours);
            this.Controls.Add(this.txtBendMen);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtLaserHours);
            this.Controls.Add(this.txtLaserMen);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPunchHours);
            this.Controls.Add(this.txtPunchMen);
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
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtPunchMen;
        private System.Windows.Forms.TextBox txtPunchHours;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLaserHours;
        private System.Windows.Forms.TextBox txtLaserMen;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBendHours;
        private System.Windows.Forms.TextBox txtBendMen;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtWeldHours;
        private System.Windows.Forms.TextBox txtWeldMen;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBuffHours;
        private System.Windows.Forms.TextBox txtBuffMen;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPaintHours;
        private System.Windows.Forms.TextBox txtPaintMen;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtPackHours;
        private System.Windows.Forms.TextBox txtPackMen;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyPlacementsToolStripMenuItem;
    }
}


﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorPlacementPlanner
{
    public partial class frmManualHours : Form
    {


        public double _manualHours { get; set; }



        public frmManualHours()
        {
            InitializeComponent();
        }

        private void frmManualHours_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _manualHours = Convert.ToDouble(txtManual.Text);
            this.Close();
        }
    }
}

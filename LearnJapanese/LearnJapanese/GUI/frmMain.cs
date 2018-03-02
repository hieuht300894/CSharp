using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LearnJapanese
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            Load -= FrmMain_Load;
            Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsData.GetData();
        }
    }
}

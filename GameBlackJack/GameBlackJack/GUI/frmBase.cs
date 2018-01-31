using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBlackJack
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();

            Load -= FrmBase_Load;
            Load += FrmBase_Load;
            FormClosing -= FrmBase_FormClosing;
            FormClosing += FrmBase_FormClosing;
        }

        protected virtual void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        protected virtual void FrmBase_Load(object sender, EventArgs e)
        {
        }
    }
}

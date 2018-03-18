using HostKVM.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostKVM.GUI
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
        }

        protected virtual void frmBase_Load(object sender, EventArgs e)
        {
        }
        protected virtual void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

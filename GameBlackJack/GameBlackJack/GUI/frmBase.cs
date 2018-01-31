using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBlackJack.GUI
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();

            Load -= FrmBase_Load;
            Load += FrmBase_Load;
        }

        protected virtual void FrmBase_Load(object sender, EventArgs e)
        {
        }
    }
}

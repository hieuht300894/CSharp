using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientKVM.GUI
{
    public partial class frmMain : frmBase
    {
        public frmMain()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadIPLocal();
        }

        void LoadIPLocal()
        {
            //frmConfig frm = new frmConfig();
            //frm.IPServer = "127.0.0.1";
            //frm.IPClient = "127.0.0.1";
            //frm.PortServer = 9999;
            //frm.PortClient = 4000;
            //frm.SetControlValue(ServerAddress: $"{frm.IPServer}:{frm.PortServer}", ClientAddress: $"{frm.IPClient}:{frm.PortClient}");
            //frm.ShowDialog(this);
        }
    }
}

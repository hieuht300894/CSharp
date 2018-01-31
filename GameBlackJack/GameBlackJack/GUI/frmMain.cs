using GameBlackJack.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameBlackJack
{
    public partial class frmMain : frmBase
    {
        public frmMain()
        {
            InitializeComponent();
        }
        protected override void FrmBase_Load(object sender, EventArgs e)
        {
            base.FrmBase_Load(sender, e);

            StartServer();
            StartClient();
        }
        protected override void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.FrmBase_FormClosing(sender, e);
        }

        void StartServer()
        {
            using (ServerApp.frmMain frm = new ServerApp.frmMain())
            {
                frm.Show();
            }
        }
        void StartClient()
        {
            using (ClientApp.frmMain frm = new ClientApp.frmMain())
            {
                frm.Show();
            }
        }
    }
}

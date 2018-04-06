using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace gamecaro
{
    public partial class frmServerConfig : frmBase
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errorIP = new ErrorProvider();
        ErrorProvider errorPort = new ErrorProvider();

        public frmServerConfig()
        {
            InitializeComponent();
        }
        protected override void FrmBase_Load(object sender, EventArgs e)
        {
            base.FrmBase_Load(sender, e);

            LoadConfig();
            CustomForm();
        }
        protected override void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.FrmBase_FormClosing(sender, e);
        }

        void LoadConfig()
        {
            txtIP.Text = clsGeneral.ServerConfig.IPServer.IsEmpty() ? clsGeneral.DefaultIP : clsGeneral.ServerConfig.IPServer;
            numPort.Value = clsGeneral.ServerConfig.PortServer > 0 ? clsGeneral.ServerConfig.PortServer : clsGeneral.DefaultServerPort;
        }
        bool ValidData()
        {
            bool chk = true;

            if (txtIP.Text.IsEmpty())
            {
                txtIP.SetError(errorIP, "IP is required.");
                chk = false;
            }
            else if (!txtIP.Text.CheckFormat(clsGeneral.regexIPAddress))
            {
                txtIP.SetError(errorIP, "IP is not correct format.");
                chk = false;
            }
            else
            {
                txtIP.SetError(errorIP, string.Empty);
            }

            if (numPort.Value == 0)
            {
                numPort.SetError(errorPort, "Port is required (greater than 0.)");
                chk = false;
            }
            else
            {
                numPort.SetError(errorPort, string.Empty);
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData())
                return false;

            if (!clsGeneral.ServerConfig.IPServer.Equals(txtIP.Text.Trim()) || clsGeneral.ServerConfig.PortServer != (int)numPort.Value)
            {
                //clsServer.StatusChanged -= clsGeneral.mainServer_StatusChanged;
                //clsGeneral.MainServer?.CloseListening();

                clsGeneral.ServerConfig.IPServer = txtIP.Text.Trim();
                clsGeneral.ServerConfig.PortServer = (int)numPort.Value;

                ReloadData?.Invoke(true);
            }

            return true;
        }
        void CustomForm()
        {
            txtIP.Enabled = false;

            btnOK.Click -= BtnOK_Click;
            btnOK.Click += BtnOK_Click;
            numPort.PreviewKeyDown -= NumPort_PreviewKeyDown;
            numPort.PreviewKeyDown += NumPort_PreviewKeyDown;
        }

        void NumPort_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
        }
        void BtnOK_Click(object sender, EventArgs e)
        {
            if (SaveData())
                DialogResult = DialogResult.OK;
        }
    }
}

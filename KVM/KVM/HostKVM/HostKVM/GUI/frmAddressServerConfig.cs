using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace HostKVM.GUI
{
    public partial class frmAddressServerConfig : frmBase
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errorIP = new ErrorProvider();
        ErrorProvider errorPort = new ErrorProvider();

        public frmAddressServerConfig()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadConfig();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);
        }

        void LoadConfig()
        {
            txtIP.Text = clsGeneral.Config.IPServer.IsEmpty() ? clsGeneral.DefaultIP : clsGeneral.Config.IPServer;
            numPort.Value = clsGeneral.Config.PortServer > 0 ? clsGeneral.Config.PortServer : clsGeneral.DefaultServerPort;

            chkIMEIRequired.Checked = clsGeneral.Config.IMEIRequired == 1;
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

            clsGeneral.Config.IMEIRequired = chkIMEIRequired.Checked ? 1 : 0;

            if (!clsGeneral.Config.IPServer.Equals(txtIP.Text.Trim()) || clsGeneral.Config.PortServer != (int)numPort.Value)
            {
                clsServer.StatusChanged -= clsGeneral.mainServer_StatusChanged;
                clsGeneral.MainServer?.CloseListening();

                clsGeneral.Config.IPServer = txtIP.Text.Trim();
                clsGeneral.Config.PortServer = (int)numPort.Value;
                clsGeneral.AddressServer = clsGeneral.Config.AddressServer.ParseAddress();
                clsGeneral.Config.Clients.Clear();

                SaveLog();
                ReloadData?.Invoke(true);
            }

            return true;
        }
        void SaveLog()
        {
            clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, string.Format(@"{0},{1},{2},{3},{4},{5},{6}#.", clsGeneral.AppVersion, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), clsGeneral.fKey.HOST.ToString(), string.Empty, string.Empty, clsGeneral.Config.IPServer, clsGeneral.Config.PortServer));
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

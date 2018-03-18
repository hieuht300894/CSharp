using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ConfigAutomation
{
    public partial class frmAddressServerConfig : Form
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errorIP = new ErrorProvider();
        ErrorProvider errorPort = new ErrorProvider();

        public frmAddressServerConfig()
        {
            InitializeComponent();

            Load -= frmAddressServerConfig_Load;
            Load += frmAddressServerConfig_Load;
        }
        void frmAddressServerConfig_Load(object sender, EventArgs e)
        {
            LoadSaveFile();
            CustomForm();
        }

        void LoadSaveFile()
        {
            txtIP.Text = clsGeneral.Config.IPServer;
            numPort.Value = clsGeneral.Config.PortServer > 0 ? clsGeneral.Config.PortServer : clsGeneral.DefaultServerPort;
        }
        bool ValidData()
        {
            bool chk = true;

            if (txtIP.Text.IsEmpty())
            {
                txtIP.SetError(errorIP, "IP is required.");
                chk = false;
            }
            else
            {
                txtIP.SetError(errorIP, "");
            }

            if (numPort.Value == 0)
            {
                numPort.SetError(errorPort, "Port is required (greater than 0.)");
                chk = false;
            }
            else
            {
                numPort.SetError(errorPort, "");
            }

            if (chk)
            {
                if (clsGeneral.Config.PortClient == (int)numPort.Value)
                {
                    numPort.SetError(errorPort, string.Format("Port {0} is used by another client.", numPort.Value));
                    chk = false;
                }
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData())
                return false;

            if (!clsGeneral.Config.IPServer.Equals(txtIP.Text) || clsGeneral.Config.PortServer != (int)numPort.Value)
            {
                clsGeneral.Config.IPServer = txtIP.Text.Trim();
                clsGeneral.Config.PortServer = (int)numPort.Value;
                clsGeneral.ServerHost = clsGeneral.Config.AddressServer.ParseAddress();

                ReloadData?.Invoke(true);
            }

            return true;
        }
        void CustomForm()
        {
            btnOK.Click -= BtnOK_Click;
            btnOK.Click += BtnOK_Click;
            txtIP.PreviewKeyDown -= TxtIP_PreviewKeyDown;
            txtIP.PreviewKeyDown += TxtIP_PreviewKeyDown;
            numPort.PreviewKeyDown -= NumPort_PreviewKeyDown;
            numPort.PreviewKeyDown += NumPort_PreviewKeyDown;
        }

        void NumPort_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
        }
        void TxtIP_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

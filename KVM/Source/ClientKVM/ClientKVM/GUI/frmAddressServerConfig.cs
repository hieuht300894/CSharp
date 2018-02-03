using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClientKVM.GUI
{
    public partial class frmAddressServerConfig : frmBase
    {
        public delegate void LoadData();
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

            LoadSaveFile();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);
        }

        void LoadSaveFile()
        {
            txtIP.Text = clsGeneral.Config.IPServer.IsEmpty() ? LoadIP() : clsGeneral.Config.IPServer;
            numPort.Value = clsGeneral.Config.PortServer;
        }
        string LoadIP()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                return string.Empty;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }
        bool ValidData()
        {
            bool chk = true;

            if (txtIP.Text.IsEmpty())
            {
                txtIP.SetError(errorIP, "IP is not blank");
                chk = false;
            }
            else
            {
                txtIP.SetError(errorIP, "");
            }

            if (numPort.Value == 0)
            {
                numPort.SetError(errorPort, "Port is not less than one");
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
                    numPort.SetError(errorPort, "The port exists port client");
                    chk = false;
                }
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData()) return false;

            clsGeneral.Config.IPServer = txtIP.Text.Trim();
            clsGeneral.Config.PortServer = (int)numPort.Value;
            clsGeneral.Config.AddressServer = $"{clsGeneral.Config.IPServer}:{clsGeneral.Config.PortServer}";
            clsGeneral.ServerHost = clsGeneral.Config.AddressServer.ParseAddress();

            ReloadData?.Invoke();
            return true;
        }
        void CustomForm()
        {
            btnOK.Click -= BtnOK_Click;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (SaveData())
                DialogResult = DialogResult.OK;
        }
    }
}

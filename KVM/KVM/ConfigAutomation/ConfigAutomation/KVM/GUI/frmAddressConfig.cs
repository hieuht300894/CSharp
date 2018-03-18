using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigAutomation
{
    public partial class frmAddressConfig : Form
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errIPClient = new ErrorProvider();
        ErrorProvider errPortClient = new ErrorProvider();
        ErrorProvider errIPServer = new ErrorProvider();
        ErrorProvider errPortServer = new ErrorProvider();

        public frmAddressConfig()
        {
            InitializeComponent();

            Load -= frmAddressClientConfig_Load;
            Load += frmAddressClientConfig_Load;
        }

        void frmAddressClientConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
            CustomForm();
        }

        void LoadConfig()
        {
            txtIPClient.Text = clsGeneral.Config.IPClient.IsEmpty() ? clsGeneral.DefaultIP : clsGeneral.Config.IPClient;
            numPortClient.Value = clsGeneral.Config.PortClient > 0 ? clsGeneral.Config.PortClient : clsGeneral.DefaultClientPort;

            txtIPServer.Text = clsGeneral.Config.IPServer;
            numPortServer.Value = clsGeneral.Config.PortServer > 0 ? clsGeneral.Config.PortServer : clsGeneral.DefaultServerPort;
        }
        bool ValidData()
        {
            bool chk = true;

            if (txtIPClient.Text.IsEmpty())
            {
                txtIPClient.SetError(errIPClient, "IP is required.");
                chk = false;
            }
            else if (!txtIPClient.Text.CheckFormat(clsGeneral.regexIPAddress))
            {
                txtIPClient.SetError(errPortClient, "IP is not correct format.");
                chk = false;
            }
            else
            {
                txtIPClient.SetError(errIPClient, "");
            }

            if (numPortClient.Value == 0)
            {
                numPortClient.SetError(errPortClient, "Port is required (greater than 0.)");
                chk = false;
            }
            else
            {
                numPortClient.SetError(errPortClient, "");
            }

            if (txtIPServer.Text.IsEmpty())
            {
                txtIPServer.SetError(errIPServer, "IP is required.");
                chk = false;
            }
            else if (!txtIPServer.Text.CheckFormat(clsGeneral.regexIPAddress))
            {
                txtIPServer.SetError(errIPServer, "IP is not correct format.");
                chk = false;
            }
            else
            {
                txtIPServer.SetError(errIPServer, "");
            }

            if (numPortServer.Value == 0)
            {
                numPortServer.SetError(errPortServer, "Port is required (greater than 0.)");
                chk = false;
            }
            else
            {
                numPortServer.SetError(errPortServer, "");
            }

            if (chk)
            {
                if ((int)numPortClient.Value == clsGeneral.Config.PortServer)
                {
                    numPortClient.SetError(errPortClient, string.Format("Port {0} is used by host.", numPortClient.Value));
                    chk = false;
                }

                if ((int)numPortServer.Value == clsGeneral.Config.PortClient)
                {
                    numPortServer.SetError(errPortServer, string.Format("Port {0} is used by client.", numPortServer.Value));
                    chk = false;
                }
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData())
                return false;

            if (!txtIPClient.Text.Equals(clsGeneral.Config.IPClient) || !txtIPServer.Text.Equals(clsGeneral.Config.IPServer) || (int)numPortClient.Value != clsGeneral.Config.PortClient || (int)numPortServer.Value != clsGeneral.Config.PortServer)
            {
                clsGeneral.Config.IPClient = txtIPClient.Text;
                clsGeneral.Config.PortClient = (int)numPortClient.Value;
                clsGeneral.ClientHost = clsGeneral.Config.AddressClient.ParseAddress();

                clsGeneral.Config.IPServer = txtIPServer.Text.Trim();
                clsGeneral.Config.PortServer = (int)numPortServer.Value;
                clsGeneral.ServerHost = clsGeneral.Config.AddressServer.ParseAddress();

                SaveLog();
                ReloadData?.Invoke(true);
            }

            return true;
        }
        void SaveLog()
        {
        }
        void CustomForm()
        {
            txtIPClient.Enabled = false;

            btnOK.Click -= BtnOK_Click;
            btnOK.Click += BtnOK_Click;
            numPortClient.PreviewKeyDown -= NumPort_PreviewKeyDown;
            numPortClient.PreviewKeyDown += NumPort_PreviewKeyDown;
            numPortServer.PreviewKeyDown -= NumPort_PreviewKeyDown;
            numPortServer.PreviewKeyDown += NumPort_PreviewKeyDown;
            txtIPClient.PreviewKeyDown -= txtIP_PreviewKeyDown;
            txtIPClient.PreviewKeyDown += txtIP_PreviewKeyDown;
            txtIPServer.PreviewKeyDown -= txtIP_PreviewKeyDown;
            txtIPServer.PreviewKeyDown += txtIP_PreviewKeyDown;
        }

        void txtIP_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
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

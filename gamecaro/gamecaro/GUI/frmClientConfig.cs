using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gamecaro
{
    public partial class frmClientConfig : Form
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errIPClient = new ErrorProvider();
        ErrorProvider errPortClient = new ErrorProvider();
        ErrorProvider errIPServer = new ErrorProvider();
        ErrorProvider errPortServer = new ErrorProvider();

        public frmClientConfig()
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
            txtIPClient.Text = clsGeneral.ClientConfig.IPClient.IsEmpty() ? clsGeneral.DefaultIP : clsGeneral.ClientConfig.IPClient;
            numPortClient.Value = clsGeneral.ClientConfig.PortClient > 0 ? clsGeneral.ClientConfig.PortClient : clsGeneral.DefaultClientPort;

            //txtIPServer.Text = clsGeneral.ClientConfig.IPServer;
            txtIPServer.Text = clsGeneral.ClientConfig.IPServer.IsEmpty() ? clsGeneral.DefaultIP : clsGeneral.ClientConfig.IPServer;
            numPortServer.Value = clsGeneral.ClientConfig.PortServer > 0 ? clsGeneral.ClientConfig.PortServer : clsGeneral.DefaultServerPort;
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
                if ((int)numPortClient.Value == clsGeneral.ClientConfig.PortServer)
                {
                    numPortClient.SetError(errPortClient, string.Format("Port {0} is used by server.", numPortClient.Value));
                    chk = false;
                }

                if ((int)numPortServer.Value == clsGeneral.ClientConfig.PortClient)
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

            if (!txtIPClient.Text.Equals(clsGeneral.ClientConfig.IPClient) || !txtIPServer.Text.Equals(clsGeneral.ClientConfig.IPServer) || (int)numPortClient.Value != clsGeneral.ClientConfig.PortClient || (int)numPortServer.Value != clsGeneral.ClientConfig.PortServer)
            {
                clsGeneral.ClientConfig.IPClient = txtIPClient.Text;
                clsGeneral.ClientConfig.PortClient = (int)numPortClient.Value;

                clsGeneral.ClientConfig.IPServer = txtIPServer.Text.Trim();
                clsGeneral.ClientConfig.PortServer = (int)numPortServer.Value;

                ReloadData?.Invoke(true);
            }

            return true;
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

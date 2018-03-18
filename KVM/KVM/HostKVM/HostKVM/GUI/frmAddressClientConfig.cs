using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostKVM.GUI
{
    public partial class frmAddressClientConfig : frmBase
    {
        public delegate void LoadData(ClientInfo client);
        public LoadData ReloadData;
        ErrorProvider errorIP = new ErrorProvider();
        ErrorProvider errorPort = new ErrorProvider();
        ClientInfo client;

        public frmAddressClientConfig()
        {
            InitializeComponent();
        }
        public frmAddressClientConfig(ClientInfo client)
        {
            InitializeComponent();

            this.client = client;
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
            txtIP.Text = client.IPClient;
            numPort.Value = client.PortClient;
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

            if (chk)
            {
                if (clsGeneral.Config.PortServer == (int)numPort.Value)
                {
                    numPort.SetError(errorPort, string.Format("Port {0} is used by host.", numPort.Value));
                    chk = false;
                }
                else if (clsGeneral.Config.Clients.Any(x => !x.ControlName.Equals(client.ControlName) && x.IPClient.Equals(txtIP.Text.Trim()) && x.PortClient.Equals(Convert.ToInt32(numPort.Value))))
                {
                    foreach (Client _client in clsGeneral.Config.Clients)
                    {
                        if (!_client.ControlName.Equals(client.ControlName))
                        {
                            if (_client.IPClient.Equals(txtIP.Text.Trim()))
                            {
                                txtIP.SetError(errorIP, string.Format("IP {0} is used by another client.", txtIP.Text.Trim()));
                                chk = false;
                            }
                            if (_client.PortClient.Equals(Convert.ToInt32(numPort.Value)))
                            {
                                numPort.SetError(errorPort, string.Format("Port {0} is used by another client.", numPort.Value));
                                chk = false;
                            }

                            if (!chk)
                                break;
                        }
                    }
                }
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData())
                return false;

            if (!client.IPClient.Equals(txtIP.Text) || client.PortClient != (int)numPort.Value)
            {
                clsServer.RemoveClient(client.AddressClient);

                client.IPClient = txtIP.Text;
                client.PortClient = (int)numPort.Value;

                client.Client.IPClient = client.IPClient;
                client.Client.PortClient = client.PortClient;
                client.ClientHost = client.AddressClient.ParseAddress();

                client.Control.SetHeaderStatus(IPAddress: client.IPClient, Port: client.PortClient, Status: client.ConnectionStatus);
                SaveLog();
            }

            return true;
        }
        void SaveLog()
        {
            clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, string.Format(@"{0},{1},{2},{3},{4},{5},{6}#.", clsGeneral.AppVersion, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), clsGeneral.fKey.CLIENT.ToString(), client.ColumnID, client.RowID, client.IPClient, client.PortClient));
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
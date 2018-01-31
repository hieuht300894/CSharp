using GameBlackJack.BUS;
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

namespace GameBlackJack.GUI
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

        protected override void FrmBase_Load(object sender, EventArgs e)
        {
            base.FrmBase_Load(sender, e);

            LoadSaveFile();
            CustomForm();
        }
        protected override void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.FrmBase_FormClosing(sender, e);
        }

        void LoadSaveFile()
        {
            txtIP.Text = client.IPClient.IsEmpty() ? LoadIP() : client.IPClient;
            numPort.Value = client.PortClient;
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
                txtIP.SetError(errorIP, "IP isn't blank");
                chk = false;
            }
            else
            {
                txtIP.SetError(errorIP, "");
            }

            if (numPort.Value == 0)
            {
                numPort.SetError(errorPort, "Port isn't less than 1");
                chk = false;
            }
            else
            {
                numPort.SetError(errorPort, "");
            }

            if (chk)
            {
                if (clsGeneral.Config.Clients.Any(x => !x.ControlName.Equals(client.ControlName) && x.PortClient.Equals(Convert.ToInt32(numPort.Value))))
                {
                    numPort.SetError(errorPort, "The port exists");
                    chk = false;
                }
                else if (clsGeneral.Config.Port == (int)numPort.Value)
                {
                    numPort.SetError(errorPort, "The port exists port server");
                    chk = false;
                }
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData()) return false;

            clsServer.RemoveClient(client.AddressClient);

            client.IPClient = txtIP.Text;
            client.PortClient = (int)numPort.Value;
            client.AddressClient = string.Format("{0}:{1}", client.IPClient, client.PortClient);

            client.Client.IPClient = client.IPClient;
            client.Client.PortClient = client.PortClient;
            client.Client.AddressClient = client.AddressClient;
            client.ClientHost = client.AddressClient.ParseAddress();

     

            ReloadData?.Invoke(client);
            SaveLog();
            return true;
        }
        void SaveLog()
        {
            clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, client.ColumnID, client.RowID, client.IPClient, client.PortClient);
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
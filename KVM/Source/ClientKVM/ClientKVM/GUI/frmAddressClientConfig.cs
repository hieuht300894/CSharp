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

namespace ClientKVM.GUI
{
    public partial class frmAddressClientConfig : frmBase
    {
        public delegate void LoadData();
        public LoadData ReloadData;
        ErrorProvider errorIP = new ErrorProvider();
        ErrorProvider errorPort = new ErrorProvider();

        public frmAddressClientConfig()
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
            txtIP.Text = clsGeneral.Config.IPClient.IsEmpty() ? LoadIP() : clsGeneral.Config.IPClient;
            numPort.Value = clsGeneral.Config.PortClient;
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
                if (clsGeneral.Config.PortServer == (int)numPort.Value)
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

            clsGeneral.Config.IPClient = txtIP.Text;
            clsGeneral.Config.PortClient = (int)numPort.Value;
            clsGeneral.Config.AddressClient = $"{  clsGeneral.Config.IPClient}:{  clsGeneral.Config.PortClient}";
            clsGeneral.ClientHost = clsGeneral.Config.AddressClient.ParseAddress();

            ReloadData?.Invoke();
            SaveLog();
            return true;
        }
        void SaveLog()
        {
            //clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, cInfo.ColumnID, cInfo.RowID, cInfo.IPClient, cInfo.PortClient);
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
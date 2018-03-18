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

namespace ConfigAutomation
{
    public partial class frmAddressClientConfig : Form
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errorIP = new ErrorProvider();
        ErrorProvider errorPort = new ErrorProvider();

        public frmAddressClientConfig()
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
            txtIP.Text = clsGeneral.Config.IPClient.IsEmpty() ? clsGeneral.DefaultIP : clsGeneral.Config.IPClient;
            numPort.Value = clsGeneral.Config.PortClient > 0 ? clsGeneral.Config.PortClient : clsGeneral.DefaultClientPort;
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
                if (clsGeneral.Config.PortServer == (int)numPort.Value)
                {
                    numPort.SetError(errorPort, string.Format("Port {0} is used by host.", numPort.Value));
                    chk = false;
                }
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData())
                return false;

            if (!clsGeneral.Config.IPClient.Equals(txtIP.Text) || clsGeneral.Config.PortClient != (int)numPort.Value)
            {
                clsGeneral.Config.IPClient = txtIP.Text;
                clsGeneral.Config.PortClient = (int)numPort.Value;
                clsGeneral.ClientHost = clsGeneral.Config.AddressClient.ParseAddress();

                SaveLog();
                ReloadData?.Invoke(true);
            }
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
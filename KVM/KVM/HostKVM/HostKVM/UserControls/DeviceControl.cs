using System;
using System.Drawing;
using System.Windows.Forms;

namespace HostKVM.UserControls
{
    public partial class DeviceControl : UserControl
    {
        public ErrorProvider errFSN { get; set; } = new ErrorProvider();
        public ErrorProvider errIMEI { get; set; } = new ErrorProvider();
        public ErrorProvider errStatus { get; set; } = new ErrorProvider();


        //Color bclrSuccess = Color.Green;
        //Color bclrFail = Color.Red;
        //Color bclrProcess = Color.Yellow;


        //Color fclrSuccess = Color.White;
        //Color fclrFail = Color.White;
        //Color fclrProcess = SystemColors.ControlText;

        Color bclrGeneral = SystemColors.Control;
        Color bclrNotConfigured = Color.Gray;
        Color bclrConnected = Color.Blue;
        Color bclrDisconnected = Color.Gray;
        Color bclrPass = Color.Green;
        Color bclrFail = Color.Red;
        Color bclrProcessing = Color.Yellow;

        Color fclrGeneral = SystemColors.ControlText;
        Color fclrNotConfigured = Color.White;
        Color fclrConnected = Color.White;
        Color fclrDisconnected = Color.White;
        Color fclrPass = Color.White;
        Color fclrFail = Color.White;
        Color fclrProcessing = SystemColors.ControlText;

        public DeviceControl()
        {
            InitializeComponent();

            Load -= DeviceControl_Load;
            Load += DeviceControl_Load;
        }

        void DeviceControl_Load(object sender, EventArgs e)
        {
            CustomForm();
        }

        public void SetHeaderTitle(string ClientName = "")
        {
            string _ClientName = ClientName ?? string.Empty;
            lbClientName.Text = _ClientName;
        }

        public void SetHeaderStatus(string IPAddress = "", int Port = 0, clsGeneral.fKey Status = clsGeneral.fKey.OFF)
        {
            if (IPAddress.IsEmpty() || Port == 0)
                lbInfo.Text = "Not configured";
            else
                lbInfo.Text = string.Format("{0}:{1}", IPAddress, Port);

            switch (Status)
            {
                case clsGeneral.fKey.ON:
                    lbInfo.BackColor = bclrConnected;
                    lbInfo.ForeColor = fclrConnected;
                    break;
                case clsGeneral.fKey.OFF:
                    lbInfo.BackColor = bclrDisconnected;
                    lbInfo.ForeColor = fclrDisconnected;
                    break;
            }
        }

        public void SetFooterStatus(string FSN = "", clsGeneral.fKey Status = clsGeneral.fKey.WAITING)
        {
            if (FSN.IsEmpty())
                lbStatus.Text = Status.ToString();

            switch (Status)
            {
                case clsGeneral.fKey.PASS:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString());
                    lbStatus.BackColor = bclrPass;
                    lbStatus.ForeColor = fclrPass;
                    lbSKUNumber.BackColor = bclrPass;
                    lbSKUNumber.ForeColor = fclrPass;
                    lbReturnCode.BackColor = bclrPass;
                    lbReturnCode.ForeColor = fclrPass;
                    break;
                case clsGeneral.fKey.FAILED:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString());
                    lbStatus.BackColor = bclrFail;
                    lbStatus.ForeColor = fclrFail;
                    lbSKUNumber.BackColor = bclrFail;
                    lbSKUNumber.ForeColor = fclrFail;
                    lbReturnCode.BackColor = bclrFail;
                    lbReturnCode.ForeColor = fclrFail;
                    break;
                case clsGeneral.fKey.PROCESSING:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString());
                    lbStatus.BackColor = bclrProcessing;
                    lbStatus.ForeColor = fclrProcessing;
                    lbSKUNumber.BackColor = bclrProcessing;
                    lbSKUNumber.ForeColor = fclrProcessing;
                    lbReturnCode.BackColor = bclrProcessing;
                    lbReturnCode.ForeColor = fclrProcessing;
                    break;
                case clsGeneral.fKey.NO_CLIENT_RESULT:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString().Replace('_', ' '));
                    lbStatus.BackColor = bclrFail;
                    lbStatus.ForeColor = fclrFail;
                    lbSKUNumber.BackColor = bclrFail;
                    lbSKUNumber.ForeColor = fclrFail;
                    lbReturnCode.BackColor = bclrFail;
                    lbReturnCode.ForeColor = fclrFail;
                    break;
                default:
                    lbStatus.Text = string.Format("{0}", Status.ToString());
                    lbStatus.BackColor = bclrGeneral;
                    lbStatus.ForeColor = fclrGeneral;
                    lbSKUNumber.BackColor = bclrGeneral;
                    lbSKUNumber.ForeColor = fclrGeneral;
                    lbReturnCode.BackColor = bclrGeneral;
                    lbReturnCode.ForeColor = fclrGeneral;
                    break;
            }
        }

        public void SetFooterSKUNumber(string SKUNumber = "")
        {
            string _SKUNumber = SKUNumber ?? string.Empty;
            lbSKUNumber.Text = string.Format("{0}", _SKUNumber);
        }

        public void SetFooterReturnCode(string ReturnCode = "")
        {
            string _ReturnCode = ReturnCode ?? string.Empty;
            lbReturnCode.Text = string.Format("{0}", _ReturnCode);
        }

        public void SetFSN(string FSN = "")
        {
            if (!FSN.IsEmpty())
                txtFSN.Text = FSN.Trim();
        }

        public void SetIMEI(string IMEI = "", bool IMEIRequired = true)
        {
            if (!IMEI.IsEmpty())
                txtIMEI.Text = IMEI.Trim();

            txtIMEI.Enabled = IMEIRequired;
        }

        public void SetErrorIMEI(string msg)
        {
            txtIMEI.SetError(errIMEI, msg);
        }

        public void SetErrorFSN(string msg)
        {
            txtFSN.SetError(errFSN, msg);
        }

        public void SetErrorStatus(string msg)
        {
            if (msg.IsEmpty())
                tpMain.SetColumnSpan(lbStatus, 3);
            else
                tpMain.SetColumnSpan(lbStatus, 2);

            lbStatus.SetError(errStatus, msg);
        }

        public void LockControls()
        {
            txtFSN.SetError(errFSN, "");
            txtIMEI.SetError(errIMEI, "");
            lbStatus.SetError(errStatus, "");

            txtFSN.Enabled = false;
            txtIMEI.Enabled = false;
            btnConfig.Enabled = false;

            tpMain.SetColumnSpan(lbStatus, 3);
        }

        public void LockTextBox()
        {
            txtFSN.SetError(errFSN, "");
            txtIMEI.SetError(errIMEI, "");
            lbStatus.SetError(errStatus, "");

            txtFSN.Enabled = false;
            txtIMEI.Enabled = false;

            tpMain.SetColumnSpan(lbStatus, 3);
        }

        public void UnlockControls()
        {
            txtFSN.Enabled = true;
            txtIMEI.Enabled = clsGeneral.Config.IMEIRequired == 1;
            btnConfig.Enabled = true;
        }

        public void UnLockTextBox()
        {
            txtFSN.SetError(errFSN, "");
            txtIMEI.SetError(errIMEI, "");
            lbStatus.SetError(errStatus, "");

            txtFSN.Enabled = true;
            txtIMEI.Enabled = true;

            tpMain.SetColumnSpan(lbStatus, 3);
        }

        void CustomForm()
        {
        }
    }
}

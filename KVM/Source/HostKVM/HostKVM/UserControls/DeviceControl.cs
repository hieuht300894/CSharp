using System;
using System.Drawing;
using System.Windows.Forms;

namespace HostKVM.UserControls
{
    public partial class DeviceControl : UserControl
    {
        Color bclrGeneral = SystemColors.Control;
        Color bclrSuccess = Color.Green;
        Color bclrFail = Color.Red;

        Color fclrGeneral = SystemColors.ControlText;
        Color fclrSuccess = Color.White;
        Color fclrFail = Color.White;

        Size dSize = new Size();

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

        public void SetInfo(string ClientName = "", string Address = "", clsGeneral.fKey Status = clsGeneral.fKey.OFF)
        {
            string _ClientName = ClientName ?? string.Empty;
            string _Address = Address.IsEmpty() ? "Not configured" : Address;

            lbInfo.Text = string.Join(" - ", _ClientName, _Address, Status.ToString());

            switch (Status)
            {
                case clsGeneral.fKey.ON:
                    lbInfo.BackColor = bclrSuccess;
                    lbInfo.ForeColor = fclrSuccess;
                    break;
                case clsGeneral.fKey.OFF:
                    lbInfo.BackColor = bclrFail;
                    lbInfo.ForeColor = fclrFail;
                    break;
            }
        }

        public void SetStatus(string FSN = "", clsGeneral.fKey Status = clsGeneral.fKey.WAITING)
        {
            if (FSN.IsEmpty())
                lbStatus.Text = Status.ToString();

            switch (Status)
            {
                case clsGeneral.fKey.PASS:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString());
                    lbStatus.BackColor = bclrSuccess;
                    lbStatus.ForeColor = fclrSuccess;
                    break;
                case clsGeneral.fKey.FAILED:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString());
                    lbStatus.BackColor = bclrFail;
                    lbStatus.ForeColor = fclrFail;
                    break;
                case clsGeneral.fKey.PROCESSING:
                    lbStatus.Text = string.Format("{0} ...", Status.ToString());
                    lbStatus.BackColor = bclrGeneral;
                    lbStatus.ForeColor = fclrGeneral;
                    break;
                case clsGeneral.fKey.NO_CLIENT_RESULT:
                    lbStatus.Text = string.Format("{0} - {1}", FSN, Status.ToString().Replace('_', ' '));
                    lbStatus.BackColor = bclrFail;
                    lbStatus.ForeColor = fclrFail;
                    break;
                default:
                    lbStatus.Text = string.Format("{0}", Status.ToString());
                    lbStatus.BackColor = bclrGeneral;
                    lbStatus.ForeColor = fclrGeneral;
                    break;
            }
        }

        public void SetFSN(string FSN = "")
        {
            if (!FSN.IsEmpty())
                txtFSN.Text = FSN.Trim();
        }

        public void SetIMEI(string IMEI = "")
        {
            if (!IMEI.IsEmpty())
                txtIMEI.Text = IMEI.Trim();
        }

        void CustomForm()
        {
            dSize = new Size(Size.Width, Size.Height);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServerApp
{
    public partial class DeviceControl : UserControl
    {
        Color bclrGeneral = SystemColors.Control;
        Color bclrSuccess = Color.Green;
        Color bclrFail = Color.Red;

        Color fclrGeneral = SystemColors.ControlText;
        Color fclrSuccess = Color.White;
        Color fclrFail = Color.White;

        public DeviceControl()
        {
            InitializeComponent();

            Load -= DeviceControl_Load;
            Load += DeviceControl_Load;
        }

        void DeviceControl_Load(object sender, EventArgs e)
        {
        }
    }
}

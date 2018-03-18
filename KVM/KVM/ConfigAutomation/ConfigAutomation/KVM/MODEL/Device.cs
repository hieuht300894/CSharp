using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigAutomation
{
    public class Device
    {

        public string FSN { get; set; }
        public string IMEI { get; set; }
        public TextBox txtFSN { get; set; }
        public TextBox txtIMEI { get; set; }
        public TextBox txtConfigFolder { get; set; }
        public TextBox txtConfigExe { get; set; }
        public TextBox txtConfigFile { get; set; }
        public TextBox txtConfigLog { get; set; }
        public bool IsScanIMEI { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostKVM
{
    public class ClientSetting
    {
        public string IPServer { get; set; } = string.Empty;
        public string IPClient { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public int PortClient { get; set; } = 0;
        public string AddressServer { get; set; } = string.Empty;
        public string AddressClient { get; set; } = string.Empty;
        public string FSN { get; set; } = string.Empty;
        public string IMEI { get; set; } = string.Empty;
        public string ConfigFolder { get; set; } = string.Empty;
        public string ConfigExe { get; set; } = string.Empty;
        public string ConfigFile { get; set; } = string.Empty;
        public string ConfigLog { get; set; } = string.Empty;
        public bool Checked { get; set; } = false;
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
        public string TimeCheck { get { return "0s"; } }
        public string Percent { get { return "0%"; } }
    }
}

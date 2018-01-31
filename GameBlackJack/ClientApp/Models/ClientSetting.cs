using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class ClientSetting
    {
        public string IPServer { get; set; } = string.Empty;
        public string IPClient { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public int PortClient { get; set; } = 0;
        public string AddressServer { get; set; } = string.Empty;
        public string AddressClient { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
    }
}

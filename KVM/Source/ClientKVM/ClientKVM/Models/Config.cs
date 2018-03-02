using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientKVM
{
    public class Config
    {
        public string IPServer { get; set; } = string.Empty;
        public string IPClient { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public int PortClient { get; set; } = 0;
        public string AddressServer { get; set; } = string.Empty;
        public string AddressClient { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ConfigFolder { get; set; } = string.Empty;
        public string ConfigExe { get; set; } = string.Empty;
        public string ConfigFile { get; set; } = string.Empty;
        public string ConfigLog { get; set; } = string.Empty;
    }
}

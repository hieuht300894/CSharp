using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class ClientConfig
    {
        public string IPClient { get; set; } = string.Empty;
        public int PortClient { get; set; } = clsGeneral.DefaultClientPort;
        public string AddressClient { get { return string.Format("{0}:{1}", IPClient, PortClient); } }
        public string IPServer { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public string AddressServer { get { return string.Format("{0}:{1}", IPServer, PortServer); } }
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class ServerConfig
    {
        public string IPServer { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public string AddressServer { get { return string.Format("{0}:{1}", IPServer, PortServer); } }
    }
}

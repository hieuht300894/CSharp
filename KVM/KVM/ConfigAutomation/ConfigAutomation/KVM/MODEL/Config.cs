using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigAutomation
{
    public class Config
    {
        /// <summary>
        /// 127.0.0.1
        /// </summary>
        public string IPServer { get; set; } = string.Empty;
        /// <summary>
        /// 127.0.0.1
        /// </summary>
        public string IPClient { get; set; } = string.Empty;
        /// <summary>
        /// 1 - 65536
        /// </summary>
        public int PortServer { get; set; } = 0;
        /// <summary>
        /// 1 - 65536
        /// </summary>
        public int PortClient { get; set; } = 0;
        /// <summary>
        /// 127.0.0.1:0000
        /// </summary>
        public string AddressServer { get { return string.Format("{0}:{1}", IPServer, PortServer); } }
        /// <summary>
        /// 127.0.0.1:0000
        /// </summary>
        public string AddressClient { get { return string.Format("{0}:{1}", IPClient, PortClient); } }
    }
}

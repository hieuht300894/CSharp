using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostKVM
{
    public class Config
    {
        public int ColumnNumber { get; set; } = 0;
        public int RowNumber { get; set; } = 0;
        public int TotalNumber { get { return ColumnNumber * RowNumber; } }
        public int IMEIRequired { get; set; } = 1;

        public string IPServer { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public string AddressServer { get { return string.Format("{0}:{1}", IPServer, PortServer); } }

        public List<Client> Clients = new List<Client>();
    }

    public class Client
    {
        public int ColumnID { get; set; }
        public int RowID { get; set; }
        public string IPClient { get; set; } = string.Empty;
        public int PortClient { get; set; } = clsGeneral.DefaultClientPort;
        public string AddressClient { get { return string.Format("{0}:{1}", IPClient, PortClient); } }
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
    }
}

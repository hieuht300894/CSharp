using HostKVM.UserControls;
using System.Net;

namespace HostKVM
{
    public class ClientInfo
    {
        public IPEndPoint ServerHost { get; set; }
        public IPEndPoint ClientHost { get; set; }
        public DeviceControl Control { get; set; }
        public Client Client { get; set; }
        public int Position { get; set; }
        public int ColumnID { get; set; }
        public int RowID { get; set; }
        public string IPServer { get; set; } = clsGeneral.DefaultIP;
        public string IPClient { get; set; } = string.Empty;
        public int PortServer { get; set; } = clsGeneral.DefaultServerPort;
        public int PortClient { get; set; } = clsGeneral.DefaultClientPort;
        public string AddressServer { get { return string.Format("{0}:{1}", IPServer, PortServer); } }
        public string AddressClient { get { return string.Format("{0}:{1}", IPClient, PortClient); } }
        public string FSN { get; set; } = string.Empty;
        public string IMEI { get; set; } = string.Empty;
        public string SKUNumber { get; set; } = string.Empty;
        public string ReturnCode { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
        public clsGeneral.fKey ConnectionStatus { get; set; } = clsGeneral.fKey.OFF;
        public clsGeneral.fKey ClientStatus { get; set; } = clsGeneral.fKey.WAITING;
        public clsGeneral.fKey ConnectionMessage { get; set; } = clsGeneral.fKey.EMPTY;
        public clsGeneral.fKey ClientMessage { get; set; } = clsGeneral.fKey.EMPTY;
    }
}

using HostKVM.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HostKVM.clsGeneral;

namespace HostKVM
{
    public class ClientInfo
    {
        public IPEndPoint ServerHost { get; set; }
        public IPEndPoint ClientHost { get; set; }
        public DeviceControl Control { get; set; }
        public Client Client { get; set; }
        public ErrorProvider errorFSN { get; set; } = new ErrorProvider();
        public ErrorProvider errorIMEI { get; set; } = new ErrorProvider();
        public int ColumnID { get; set; }
        public int RowID { get; set; }
        public string IPServer { get; set; } = string.Empty;
        public string IPClient { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public int PortClient { get; set; } = 0;
        public string AddressServer { get; set; } = string.Empty;
        public string AddressClient { get; set; } = string.Empty;
        public string FSN { get; set; } = string.Empty;
        public string IMEI { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
        public fKey ConnectionStatus { get; set; } = fKey.OFF;
        public fKey ClientStatus { get; set; } = fKey.WAITING;
        public fKey ConnectionMessage { get; set; } = fKey.EMPTY;
        public fKey ClientMessage { get; set; } = fKey.EMPTY;
    }
}

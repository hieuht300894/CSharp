﻿using GameBlackJack.MODEL;
using HostKVM.UserControls;
using System.Net;
using System.Windows.Forms;

namespace GameBlackJack.BUS
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
        public clsGeneral.fKey ConnectionStatus { get; set; } = clsGeneral.fKey.OFF;
        public clsGeneral.fKey ClientStatus { get; set; } = clsGeneral.fKey.WAITING;
        public clsGeneral.fKey ConnectionMessage { get; set; } = clsGeneral.fKey.EMPTY;
        public clsGeneral.fKey ClientMessage { get; set; } = clsGeneral.fKey.EMPTY;
    }
}

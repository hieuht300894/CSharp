using GameBlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlackJack.MODEL
{
    public class Config
    {
        public int ColumnNumber { get; set; } = clsGeneral.DefaultNumOfColumns;
        public int RowNumber { get; set; } = clsGeneral.DefaultNumOfRows;
        public int TotalNumber { get { return ColumnNumber * RowNumber; } }

        public string IP { get; set; } = string.Empty;
        public int Port { get; set; } = clsGeneral.DefaultPort;
        public string Address { get; set; } = string.Empty;

        public Banker Banker { get; set; } = new Banker();
        public List<Client> Clients = new List<Client>();
    }

    public class Client
    {
        public int ColumnID { get; set; }
        public int RowID { get; set; }
        public string IPServer { get; set; } = string.Empty;
        public string IPClient { get; set; } = string.Empty;
        public int PortServer { get; set; } = 0;
        public int PortClient { get; set; } = 0;
        public string AddressServer { get; set; } = string.Empty;
        public string AddressClient { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ControlName { get; set; } = string.Empty;
    }

    public class Banker
    {
        public int ColumnID { get; set; }
        public int RowID { get; set; }
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

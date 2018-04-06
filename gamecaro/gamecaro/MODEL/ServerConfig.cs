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

        public delegate void UpdateStatusCallback(string address, string msg);
        public UpdateStatusCallback updateStatus;
        public clsServer MainServer { get; set; }
        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            updateStatus?.Invoke(e.EventAddress, e.EventMessage);
        }
        public bool IsStart { get; set; }
    }
}

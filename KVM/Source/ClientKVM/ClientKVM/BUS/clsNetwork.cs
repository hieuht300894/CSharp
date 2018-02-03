using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ClientKVM
{
    public static class clsNetwork
    {
        public static bool PingNetwork(IPEndPoint address)
        {
            if (address == null) return false;

            Ping ping = new Ping();
            PingReply reply = ping.Send(address.Address, 1000, new byte[32], new PingOptions(128, true));
            if (reply == null)
                return false;
            if (reply.Status == IPStatus.Success)
                return true;
            return false;
        }

        public static TcpState CheckAvailable(this IPEndPoint address)
        {
            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == address.Port)
                    return tcpi.State;
            }
            return TcpState.Unknown;
        }
        public static TcpState CheckListening(this IPEndPoint address)
        {
            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] listeners = ipGlobalProperties.GetActiveTcpListeners();

            foreach (IPEndPoint listener in listeners)
            {
                if (listener.ToString().Equals(address.ToString()))
                    return TcpState.Listen;
            }
            return TcpState.Unknown;
        }
    }
}

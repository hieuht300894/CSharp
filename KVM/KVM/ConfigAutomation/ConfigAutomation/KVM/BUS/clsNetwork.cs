using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConfigAutomation
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
            return TcpState.Listen;

            //// Evaluate current system tcp connections. This is the same information provided
            //// by the netstat command line application, just in .Net strongly-typed object
            //// form.  We will look through the list, and if our port we would like to use
            //// in our TcpClient is occupied, we will set isAvailable to false.
            //IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            //IPEndPoint[] listeners = ipGlobalProperties.GetActiveTcpListeners();

            //foreach (IPEndPoint listener in listeners)
            //{
            //    if (listener.ToString().Equals(address.ToString()))
            //        return TcpState.Listen;
            //}
            //return TcpState.Unknown;
        }
        public static string LoadIPv4()
        {
            string query = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'";
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(query);
            ManagementObjectCollection moCollection = moSearch.Get();

            // Every record in this collection is a network interface
            foreach (ManagementObject mo in moCollection)
            {
                // IPAddresses, probably have more than one value
                string[] addresses = (string[])mo["IPAddress"];
                foreach (string ipaddress in addresses)
                {
                    if (string.Format("{0}:{1}", ipaddress, "0000").CheckFormat(clsGeneral.regexAddress))
                        return ipaddress;
                }
            }
            return string.Empty;

            //bool isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
            //if (!isNetworkAvailable)
            //{
            //    clsIO.SaveLog("Logs", "log", "txt", "NetworkInterface.GetIsNetworkAvailable: " + isNetworkAvailable);
            //    return string.Empty;
            //}

            //string hostName = Dns.GetHostName();
            //IPHostEntry host = Dns.GetHostEntry(hostName);
            //clsIO.SaveLog("Logs", "log", "txt", "NetworkInterface.GetIsNetworkAvailable: " + isNetworkAvailable);
            //clsIO.SaveLog("Logs", "log", "txt", "Dns.GetHostName: " + hostName);
            //foreach (var ip in host.AddressList)
            //{
            //    clsIO.SaveLog("Logs", "log", "txt", "Address: " + ip.ToString());
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //        return ip.ToString();
            //}
            //return string.Empty;

            //string hostName = Dns.GetHostName();
            //var host = Dns.GetHostEntry(hostName);
            //clsIO.SaveLog("Logs", "log", "txt", "NetworkInterface.GetIsNetworkAvailable: " + isNetworkAvailable);
            //clsIO.SaveLog("Logs", "log", "txt", "Dns.GetHostName: " + hostName);
            //foreach (var ip in host.AddressList)
            //{
            //    clsIO.SaveLog("Logs", "log", "txt", "Address: " + ip.ToString());
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //        return ip.ToString();
            //}
            //return string.Empty;
        }
    }
}

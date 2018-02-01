using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace GameThirteen
{
    public static class clsGeneral
    {
        public static string DefaultIPAddress
        {
            get
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    return string.Empty;

                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        return ip.ToString();
                }
                return string.Empty;
            }
        }
        public static int DefaultPort
        {
            get
            {
                return 5000;
            }
        }
        public static int DefaultNumOfColumns
        {
            get
            {
                return 1;
            }
        }
        public static int DefaultNumOfRows
        {
            get
            {
                return 1;
            }
        }

        public enum fKey
        {
            EMPTY = 0,
            OK = 1,
            ACCEPT = 2,
            DENY = 3,
            WAITING = 4,
            CONNECTED = 5,
            DISCONNECTED = 6,
            PASS = 7,
            FAILED = 8,
            RESULT = 10,
            START = 11,
            FSN = 12,
            IMEI = 13,
            REGISTER = 18,
            TESTING = 20,
            STOP = 21,
            BEGIN = 22,
            USERNAME = 24,
            QUESTION = 25,
            CONNECTING = 26,
            CLIENT = 27,
            SETUP = 28,
            FINISHED = 31,
            STARTING = 32,
            ON = 33,
            OFF = 34,
            PROCESSING = 35,
            NO_CLIENT_RESULT = 36,
            SERVER = 37
        }
        public static Regex regexAddress { get { return new Regex(@"(?<IP>(\d{3}|\d{2}|\d{1}).(\d{3}|\d{2}|\d{1}).(\d{3}|\d{2}|\d{1}).(\d{3}|\d{2}|\d{1})):(?<Port>(\d{5}|\d{4}))"); } }
        public static Regex regexCommand { get { return new Regex($@"([[](?<Key>\w+)[]])([=])([[]]|([[](?<Value>[a-zA-z0-9 -:.\\\/]+)[]]))"); } }
        public static string DirConfig { get { return "Configs"; } }
        public static string FileConfig { get { return "ServerConfig"; } }
        public static string ExtConfig { get { return "xml"; } }
        public static string DirLog { get { return "Logs"; } }
        public static string FileLog { get { return DateTime.Now.ToString("yyyyMMdd"); } }
        public static string ExtLog { get { return "txt"; } }

        public static IPEndPoint AddressServer { get; set; }
        public static Config Config { get; set; } = new Config();
        public static List<ClientInfo> Clients { get; set; } = new List<ClientInfo>();
        public static List<ClientInfo> Playings { get; set; } = new List<ClientInfo>();
    }
}

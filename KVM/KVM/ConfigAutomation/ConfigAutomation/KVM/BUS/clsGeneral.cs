using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ConfigAutomation
{
    public static class clsGeneral
    {
        public static string DefaultIP { get; private set; } = clsNetwork.LoadIPv4();
        public static int DefaultServerPort { get; private set; } = 5000;
        public static int DefaultClientPort { get; private set; } = 5001;

        public static void showMessage(this Form frmMain, string Title = "Information", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        public static void showWarming(this Form frmMain, string Title = "Information", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        public static void showError(this Form frmMain, string Title = "Information", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public static bool showConfirm(this Form frmMain, string Title = "Information", string Caption = "") { return MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }
        public enum fKey : Int32
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
            RESULT = 9,
            START = 10,
            FSN = 11,
            IMEI = 12,
            REGISTER = 13,
            TESTING = 14,
            STOP = 15,
            BEGIN = 16,
            USERNAME = 17,
            QUESTION = 18,
            CONNECTING = 19,
            CLIENT = 20,
            SETUP = 21,
            FINISHED = 22,
            STARTING = 23,
            ON = 24,
            OFF = 25,
            PROCESSING = 26,
            ERROR = 27,
            NORMAL = 28,
            NO_CONFIGURATION = 29,
            HOST = 30,
            IS_SCAN_IMEI = 31,
            SKU_NUMBER = 32,
            RETURN_CODE = 33
        }
        public static Regex regexIPAddress { get { return new Regex(@"(\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})"); } }
        public static Regex regexAddress { get { return new Regex(@"(?<IP>(\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1}))[:](?<Port>(\d{5}|\d{4}))"); } }
        public static Regex regexCommand { get { return new Regex("([[](?<Key>\\w+)[]])([=])([[]]|([[](?<Value>[a-zA-Z0-9 ~`!@#$%^&*()_+\\-=\\[\\]{};':\"\\|,.<>\\/?]+)[]]))"); } }
        public static string DirConfig { get { return "Configs"; } }
        public static string FileConfig { get { return "Config"; } }
        public static string ExtConfig { get { return "xml"; } }
        public static string DirLog { get { return "Logs"; } }
        public static string FileLog { get { return $"{DateTime.Now.ToString("yyyyMMdd")}"; } }
        public static string ExtLog { get { return "txt"; } }
        public static IPEndPoint ServerHost { get; set; }
        public static IPEndPoint ClientHost { get; set; }
        public static Config Config { get; set; } = new Config();
        public static int ClientMode { get; set; } = 0;
        /// <summary>
        /// Port of client is on or off
        /// </summary>
        public static fKey PortStatus { get; set; } = fKey.OFF;
        /// <summary>
        /// Connection is connected or disconnected
        /// </summary>
        public static fKey ConnectionStatus { get; set; } = fKey.DISCONNECTED;
        /// <summary>
        /// Delegate change title of application
        /// </summary>
        public static string _ApplicationTitle { get; set; }
        public static string _TesterName { get; set; }
        public static string _PCName { get; private set; } = Environment.MachineName;
        public static Form frmMain { get; set; }
        public delegate void SetTitle();
        public static SetTitle _SetTitle { get; private set; } = new SetTitle(new Action(() =>
        {
            frmMain.BeginInvokeExt(() =>
            {
                if (ClientMode == 0)
                {
                    frmMain.Text = string.Format("{0} - {1} - {2}",
                        _ApplicationTitle, _TesterName,
                        fKey.NORMAL.ToString());
                }
                else
                {
                    if (ClientHost == null && ServerHost == null)
                    {
                        frmMain.Text = string.Format("{0} - {1} - {2}: ({3}) - {4}: ({5})",
                            _ApplicationTitle, _PCName,
                            fKey.CLIENT, fKey.NO_CONFIGURATION.ToString().Replace('_', ' '),
                            fKey.HOST, fKey.NO_CONFIGURATION.ToString().Replace('_', ' '));
                    }
                    else if (ClientHost != null && ServerHost == null)
                    {
                        frmMain.Text = string.Format("{0} - {1} - {2}: {3} ({4}) - {5}: ({6})",
                            _ApplicationTitle, _PCName,
                            fKey.CLIENT, Config.AddressClient, PortStatus.ToString(),
                            fKey.HOST, fKey.NO_CONFIGURATION.ToString().Replace('_', ' '));
                    }
                    else if (ClientHost == null && ServerHost != null)
                    {
                        frmMain.Text = string.Format("{0} - {1} - {2}: ({3}) - {4}: {5} ({6})",
                            _ApplicationTitle, _PCName,
                            fKey.CLIENT, fKey.NO_CONFIGURATION.ToString().Replace('_', ' '),
                            fKey.HOST, Config.AddressServer, ConnectionStatus.ToString());
                    }
                    else
                    {
                        frmMain.Text = string.Format("{0} - {1} - {2}: {3} ({4}) - {5}: {6} ({7})",
                            _ApplicationTitle, _PCName,
                            fKey.CLIENT, Config.AddressClient, PortStatus.ToString(),
                            fKey.HOST, Config.AddressServer, ConnectionStatus.ToString());
                    }
                }
            });
        }));

        public static Device Device { get; private set; } = new Device();
    }
}

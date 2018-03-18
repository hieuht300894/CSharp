using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostKVM
{
    public static class clsGeneral
    {
        public static string DefaultIP { get; private set; } = clsNetwork.LoadIPv4();
        public static int DefaultServerPort { get; private set; } = 5000;
        public static int DefaultClientPort { get; private set; } = 5001;
        public static int DefaultNumOfColumns { get; private set; } = 1;
        public static int DefaultNumOfRows { get; private set; } = 1;

        public static void showMessage(this Form frmMain, string Title = "Message", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        public static void showWarming(this Form frmMain, string Title = "Warming", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        public static void showError(this Form frmMain, string Title = "Error", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public static bool showConfirm(this Form frmMain, string Title = "Confirm", string Caption = "") { return MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }
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
            NO_CLIENT_RESULT = 27,
            ERROR = 28,
            NO_CONFIGURATION = 29,
            HOST = 30,
            IS_SCAN_IMEI = 31,
            SKU_NUMBER = 32,
            RETURN_CODE = 33
        }
        public static Regex regexIPAddress { get { return new Regex(@"(\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})"); } }
        public static Regex regexAddress { get { return new Regex(@"(?<IP>(\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1}))[:](?<Port>(\d{5}|\d{4}))"); } }
        public static Regex regexCommand { get { return new Regex(string.Format("{0}{1}{2}", @"([[](?<Key>\w+)[]])([=])([[]]|([[](?<Value>[a-zA-Z0-9 ~`!@#$%^&*()_+\-=\[\]{};':", "\"", @"\\|,.<>\/?]+)[]]))")); } }
        public static string DirConfig { get { return "Configs"; } }
        public static string FileConfig { get { return "Config"; } }
        public static string ExtConfig { get { return "xml"; } }
        public static string DirLog { get { return "Logs"; } }
        public static string FileLog { get { return DateTime.Now.ToString("yyyyMMdd"); } }
        public static string ExtLog { get { return "txt"; } }

        public static IPEndPoint AddressServer { get; set; }
        /// <summary>
        /// Port server is ON or OFF
        /// </summary>
        public static fKey PortStatus { get; set; } = fKey.OFF;
        /// <summary>
        /// Delegate change title of application
        /// </summary>
        public static Form frmMain { get; set; }
        public delegate void SetTitle();
        public static SetTitle _SetTitle { get; private set; } = new SetTitle(new Action(() =>
        {
            frmMain?.BeginInvokeExt(() =>
            {
                if (PortStatus == fKey.NO_CONFIGURATION)
                {
                    frmMain.Text = string.Format("{0} - {1} - {2} - {3})",
                        AppTitle, AppVersion, PCName, PortStatus.ToString().Replace('_', ' '));
                }
                else if (PortStatus == fKey.ON)
                {
                    frmMain.Text = string.Format("{0} - {1} - {2} - {3} ({4})",
                        AppTitle, AppVersion, PCName,
                        Config.AddressServer, PortStatus.ToString());
                }
                else if (PortStatus == fKey.OFF)
                {
                    frmMain.Text = string.Format("{0} - {1} - {2} - {3} ({4})",
                        AppTitle, AppVersion, PCName,
                        Config.AddressServer, PortStatus.ToString());
                }
            });
        }));
        public static Config Config { get; set; } = new Config();
        public static string AppTitle { get; private set; } = Application.ProductName;
        public static string AppVersion { get; private set; } = Application.ProductVersion;
        public static string PCName { get; private set; } = Environment.MachineName;

        public delegate void UpdateStatusCallback(string address, string msg);
        public static UpdateStatusCallback updateStatus;
        public static clsServer MainServer { get; set; }
        public static void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            updateStatus?.Invoke(e.EventAddress, e.EventMessage);
        }
    }
}

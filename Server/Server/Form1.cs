using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtServerIP.Text = GetLocalIPAddress();
        }
        //---------------------------------------------Server---------------------------------------------
        #region Variable Server
        bool isJoin;
        #endregion

        #region Method Server
        public string GetLocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                return string.Empty;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        public bool IsJoin
        {
            get { return isJoin; }
            set { isJoin = value; }
        }
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, long nMaxCount);
        private string GetClassNameOfWindow(IntPtr hwnd)
        {
            StringBuilder className = new StringBuilder(100);
            long nret = GetClassName(hwnd, className, className.Capacity);
            return className.ToString();
        }
        private delegate void UpdateStatusCallback(string strMessage);
        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }
        private void UpdateStatus(string strMessage)
        {
            // Updates the log with the message
            txtLog.AppendText(strMessage + "\r\n");

            if (strMessage.Equals("Connected"))
                IsJoin = true;
            else if (IsJoin)
            {
                string[] str = strMessage.Split('-');
                if (str.Length == 2)
                {
                    LoadProcess(str[0], str[1]);
                }
                else
                {
                    if (string.IsNullOrEmpty(strMessage))
                        lstAppName.Items.Clear();
                    lstAppName.Items.Add(strMessage);
                }
            }
        }
        private void LoadProcess(string appName, string keyCode)
        {
            foreach (Process p in Process.GetProcesses().OrderBy(x => x.ProcessName))
            {
                if (p.ProcessName.Equals(appName))
                {
                    string classname = GetClassNameOfWindow(p.MainWindowHandle);
                    IntPtr handle = FindWindow(classname, p.MainWindowTitle);
                    if (handle == IntPtr.Zero)
                        return;
                    SetForegroundWindow(handle);
                    SendKeys.SendWait(keyCode);
                }
            }
        }
        #endregion

        #region Events Server
        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the server's IP address out of the TextBox
                IPAddress ipAddr = IPAddress.Parse(txtServerIP.Text);
                // Create a new instance of the ChatServer object
                ChatServer mainServer = new ChatServer(ipAddr);
                // Hook the StatusChanged event handler to mainServer_StatusChanged
                ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
                // Start listening for connections
                mainServer.StartListening(Convert.ToInt32(txtPort.Text));
                // Show that we started to listen for connections
                txtLog.AppendText("Monitoring for connections...\r\n");
            }
            catch { IsJoin = false; }
        }
        #endregion

        //---------------------------------------------Client---------------------------------------------
        #region Variable Client
        // Will hold the user name
        private string UserName = "Unknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
        // Needed to update the form with messages from another thread
        private delegate void UpdateLogCallback(string strMessage);
        // Needed to set the form to a "disconnected" state from another thread
        private delegate void CloseConnectionCallback(string strReason);
        private Thread thrMessaging;
        private IPAddress ipAddr;
        private bool Connected;
        #endregion

        #region Method Client
        private void InitializeConnection()
        {
            // Parse the IP address from the TextBox into an IPAddress object
            ipAddr = IPAddress.Parse(txtClientIP.Text);
            // Start a new TCP connections to the chat server
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddr, Convert.ToInt32(txtServerPort.Text));

            // Helps us track whether we're connected or not
            Connected = true;
            // Prepare the form
            UserName = txtClientName.Text;

            // Disable and enable the appropriate fields
            txtClientIP.Enabled = false;
            txtClientName.Enabled = false;
            txtClientMessage.Enabled = true;
            btnSend.Enabled = true;
            btnClientConnect.Text = "Disconnect";

            // Send the desired username to the server
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(txtClientName.Text);
            swSender.Flush();

            // Start the thread for receiving messages and further communication
            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();
        }
        private void ReceiveMessages()
        {
            // Receive the response from the server
            srReceiver = new StreamReader(tcpServer.GetStream());
            // If the first character of the response is 1, connection was successful
            string ConResponse = srReceiver.ReadLine();
            // If the first character is a 1, connection was successful
            if (ConResponse[0] == '1')
            {
                // Update the form to tell it we are now connected
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else // If the first character is not a 1 (probably a 0), the connection was unsuccessful
            {
                string Reason = "Not Connected: ";
                // Extract the reason out of the response message. The reason starts at the 3rd character
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                // Update the form with the reason why we couldn't connect
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                // Exit the method
                return;
            }
            // While we are successfully connected, read incoming lines from the server
            while (Connected)
            {
                // Show the messages in the log TextBox
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }
        // This method is called from a different thread in order to update the log TextBox
        private void UpdateLog(string strMessage)
        {
            // Append text also scrolls the TextBox to the bottom each time
            //txtLog.AppendText(strMessage + "\r\n");
        }
        // Sends the message typed in to the server
        private void SendMessage()
        {
            if (txtClientMessage.Lines.Length >= 1)
            {
                swSender.WriteLine(txtClientMessage.Text);
                swSender.Flush();
                txtClientMessage.Lines = null;
            }
            txtClientMessage.Text = "";
        }
        // Closes a current connection
        private void CloseConnection(string Reason)
        {
            // Show the reason why the connection is ending
            //txtLog.AppendText(Reason + "\r\n");
            // Enable and disable the appropriate controls on the form
            txtClientIP.Enabled = true;
            txtClientName.Enabled = true;
            txtClientMessage.Enabled = false;
            btnSend.Enabled = false;
            btnClientConnect.Text = "Connect";

            // Close the objects
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }
        #endregion

        #region Events Client
        private void btnClientConnect_Click(object sender, EventArgs e)
        {
            // If we are not currently connected but awaiting to connect
            if (Connected == false)
            {
                // Initialize the connection
                InitializeConnection();
            }
            else // We are connected, thus disconnect
            {
                CloseConnection("Disconnected at user's request.");
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
        private void txtClientMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the key is Enter
            if (e.KeyChar == (char)13)
            {
                SendMessage();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chkSendPro.Checked)
            {
                foreach (Process p in Process.GetProcesses().OrderBy(x => x.ProcessName))
                {
                    txtClientMessage.Text = p.ProcessName;
                    SendMessage();
                }
            }
        }
        #endregion
    }
}

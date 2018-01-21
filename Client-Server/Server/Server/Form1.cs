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

namespace Server
{
    public partial class Form1 : Form
    {
        private delegate void UpdateStatusCallback(string strMessage);
        ChatServer mainServer;
        bool start = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                // Parse the server's IP address out of the TextBox
                IPAddress ipAddr = IPAddress.Parse(txtIp.Text);
                // Create a new instance of the ChatServer object
                mainServer = new ChatServer(ipAddr, Convert.ToInt32(txtPort.Text));
                // Hook the StatusChanged event handler to mainServer_StatusChanged
                ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
                // Start listening for connections
                mainServer.StartListening();
                // Show that we started to listen for connections
                txtLog.AppendText("Monitoring for connections...\r\n");
            }
            else
            {
                mainServer.CloseListening();
                txtLog.AppendText("Close connections...\r\n");
            }
            start = !start;
            btnListen.Text = start ? "Stop listening" : "Start listening";
        }

        #region Method
        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }
        private void UpdateStatus(string strMessage)
        {
            // Updates the log with the message
            txtLog.AppendText(strMessage + "\r\n");
        }
        private string GetLocalIPAddress()
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
            return string.Empty;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIp.Text = GetLocalIPAddress();
            txtPort.Text = "1111";
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (start)
                mainServer.CloseListening();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ArduinoSerialRead
{
    public partial class Form1 : Form
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, long nMaxCount);

        private delegate void LineReceivedEvent(string line);
        SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = "COM9";
                serialPort.BaudRate = 115200;
                serialPort.DtrEnable = true;
                serialPort.DataReceived += serialPort_DataReceived;
                serialPort.Open();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string line = serialPort.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
        }

        private void LineReceived(string line)
        {
            //What to do with the received line here
            label1.Text = line;

            try
            {
                int id = Convert.ToInt32(line);
                SetForegroundWindow(handle);
                if (id==30)
                    SendKeys.SendWait("a"); 
                if (id == 31)
                    SendKeys.SendWait("b");
            }
            catch { }
        }

        IntPtr handle = IntPtr.Zero;
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses().Where(x => !string.IsNullOrEmpty(x.MainWindowTitle)).OrderBy(x => x.ProcessName))
            {
                if (p.MainWindowTitle.Contains("Virtual Piano"))
                {
                    string classname = GetClassNameOfWindow(p.MainWindowHandle);
                     handle = FindWindow(classname, p.MainWindowTitle);
                    if (handle == IntPtr.Zero)
                    {
                        this.Text = "Not running.";
                        return;
                    }
                    SetForegroundWindow(handle);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetForegroundWindow(handle);
            SendKeys.SendWait("a");
        }

        private string GetClassNameOfWindow(IntPtr hwnd)
        {
            StringBuilder className = new StringBuilder(100);
            long nret = GetClassName(hwnd, className, className.Capacity);
            return className.ToString();
        }
    }
}

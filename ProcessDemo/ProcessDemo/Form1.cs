using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Windows;

namespace ProcessDemo
{
    public partial class Form1 : Form
    {

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        #region Method
        private void LoadProcess()
        {
           // ConsoleKeyInfo keyInfo = Console.ReadKey();
           //if (keyInfo.KeyChar==(char)65)
           //{
           //    listBox1.Items.Add(keyInfo.KeyChar.ToString());
           //}
           //else
           //{
           //}
        }
        #endregion


        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadProcess();

            //foreach (Process p in Process.GetProcesses())
            //{
            //    if (p.ProcessName.Equals("CutImage"))
            //    {
            //        IntPtr calculatorHandle = FindWindow("CalcFrame", p.MainWindowTitle);

            //        // Verify that Calculator is a running process.
            //        if (calculatorHandle == IntPtr.Zero)
            //        {
            //            this.Text = "Application not running";
            //            return;
            //        }

            //        // Make Calculator the foreground application and send it 
            //        // a set of calculations.
            //        SetForegroundWindow(calculatorHandle);
            //        SendKeys.SendWait("1");
            //        //SendKeys.SendWait("*");
            //        //SendKeys.SendWait("11");
            //        //SendKeys.SendWait("=");
            //    }
            //    if (p.ProcessName.Equals("calc"))
            //    {
            //        IntPtr calculatorHandle = FindWindow("CalcFrame", p.MainWindowTitle);

            //        // Verify that Calculator is a running process.
            //        if (calculatorHandle == IntPtr.Zero)
            //        {
            //            this.Text = "Application not running";
            //            return;
            //        }

            //        // Make Calculator the foreground application and send it 
            //        // a set of calculations.
            //        SetForegroundWindow(calculatorHandle);
            //        SendKeys.SendWait("1");
            //        //SendKeys.SendWait("*");
            //        //SendKeys.SendWait("11");
            //        //SendKeys.SendWait("=");
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}

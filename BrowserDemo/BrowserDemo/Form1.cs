using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 800 || this.Height < 400)
            {
                this.Width = 800;
                this.Height = 400;
            }
            else
            {
                btHome.Location = new Point(panel2.Width - 483, 0);
                btBack.Location = new Point(panel2.Width - 402, 0);
                btForward.Location = new Point(panel2.Width - 321, 0);
                btRefresh.Location = new Point(panel2.Width - 240, 0);
                btStop.Location = new Point(panel2.Width - 159, 0);
                btGo.Location = new Point(panel2.Width - 78, 0);
            }
        }

        private void btHome_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void btForward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void btGo_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text.Trim());
        }
    }
}

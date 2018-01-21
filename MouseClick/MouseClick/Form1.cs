using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MouseClick
{
    public partial class Form1 : Form
    {
        private int id = 0;
        private Point point;
        private KeyHandler K;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (id)
            {
                case 1:
                    lbPoint.Text = Cursor.Position.ToString(); break;
                case 2:
                    break;
                case 3:
                    MouseHandle.LeftMouseClick(point);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            K = new KeyHandler(Keys.Escape, this);
            K.Register();
        }

        private void _TurnTimer()
        {
            //Turn on/off time to get mouse position
            timer1.Enabled = !timer1.Enabled;
            id = 1;
        }

        private void _StartClick()
        {
            timer1.Enabled = !timer1.Enabled;
            id = 3;
        }

        private void _SavePoint()
        {
            lbSetPoint.ResetText();
            point = new Point();
            point = Cursor.Position;
            lbSetPoint.Text = point.ToString();
            id = 2;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D1)
                _TurnTimer();
            if (keyData == Keys.D2)
                _SavePoint();
            if (keyData == Keys.D3)
                _StartClick();
            if (keyData == Keys.Escape)
            {
                id = 0;
                timer1.Enabled = false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                timer1.Enabled = false;
            base.WndProc(ref m);
        }
    }
}

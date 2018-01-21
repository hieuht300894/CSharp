using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SetBackground
{
    public partial class frmMain : Form
    {
        #region Variable
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        const int SPIF_SENDCHANGE = 0x2;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public enum Style : int
        {
            Fill,
            Fit,
            Stretch,
            Tile,
            Center,
            Span
        }
        List<string> lstTimes;
        #endregion

        #region Form
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void btSetting_Click(object sender, EventArgs e)
        {

        }
        private void frmMain_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Method
        #endregion
    }
}

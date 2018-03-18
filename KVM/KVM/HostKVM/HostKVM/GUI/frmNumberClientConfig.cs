using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostKVM.GUI
{
    public partial class frmNumberClientConfig : frmBase
    {
        public delegate void LoadData(bool bRe);
        public LoadData ReloadData;
        ErrorProvider errorColumn = new ErrorProvider();
        ErrorProvider errorRow = new ErrorProvider();

        public frmNumberClientConfig()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadConfig();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);
        }

        void LoadConfig()
        {
            numColumn.Value = clsGeneral.Config.ColumnNumber > 0 ? clsGeneral.Config.ColumnNumber : clsGeneral.DefaultNumOfColumns;
            numRow.Value = clsGeneral.Config.RowNumber > 0 ? clsGeneral.Config.RowNumber : clsGeneral.DefaultNumOfRows;
        }
        bool ValidData()
        {
            bool chk = true;

            if (numColumn.Value == 0)
            {
                numColumn.SetError(errorColumn, "Number is not less than one");
                chk = false;
            }
            else
            {
                numColumn.SetError(errorColumn, "");
            }

            if (numRow.Value == 0)
            {
                numRow.SetError(errorRow, "Number is not less than one");
                chk = false;
            }
            else
            {
                numRow.SetError(errorRow, "");
            }

            return chk;
        }
        bool SaveData()
        {
            if (!ValidData())
                return false;

            if (clsGeneral.Config.ColumnNumber != (int)numColumn.Value || clsGeneral.Config.RowNumber != (int)numRow.Value)
            {
                clsServer.StatusChanged -= clsGeneral.mainServer_StatusChanged;
                clsGeneral.MainServer?.CloseListening();

                clsGeneral.Config.ColumnNumber = (int)numColumn.Value;
                clsGeneral.Config.RowNumber = (int)numRow.Value;
                clsGeneral.Config.Clients.Clear();

                SaveLog();
                ReloadData?.Invoke(true);
            }

            return true;
        }
        void SaveLog()
        {
            clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, string.Format(@"{0},{1},{2},{3},{4},{5},{6}.", clsGeneral.AppVersion, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), clsGeneral.fKey.HOST.ToString(), clsGeneral.Config.RowNumber, clsGeneral.Config.ColumnNumber, string.Empty, string.Empty));
        }
        void CustomForm()
        {
            btnOK.Click -= BtnOK_Click;
            btnOK.Click += BtnOK_Click;
            numColumn.PreviewKeyDown -= NumColumn_PreviewKeyDown;
            numColumn.PreviewKeyDown += NumColumn_PreviewKeyDown;
            numRow.PreviewKeyDown -= NumRow_PreviewKeyDown;
            numRow.PreviewKeyDown += NumRow_PreviewKeyDown;
        }

        void NumRow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
        }
        void NumColumn_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
        }
        void BtnOK_Click(object sender, EventArgs e)
        {
            if (SaveData())
                DialogResult = DialogResult.OK;
        }
    }
}

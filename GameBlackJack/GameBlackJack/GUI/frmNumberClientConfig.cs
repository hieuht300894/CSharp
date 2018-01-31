using GameBlackJack.BUS;
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

namespace GameBlackJack.GUI
{
    public partial class frmNumberClientConfig : frmBase
    {
        public delegate void LoadData();
        public LoadData ReloadData;
        ErrorProvider errorColumn = new ErrorProvider();
        ErrorProvider errorRow = new ErrorProvider();

        public frmNumberClientConfig()
        {
            InitializeComponent();
        }
        protected override void FrmBase_Load(object sender, EventArgs e)
        {
            base.FrmBase_Load(sender, e);

            LoadSaveFile();
            CustomForm();
        }
        protected override void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.FrmBase_FormClosing(sender, e);
        }

        void LoadSaveFile()
        {
            numColumn.Value = clsGeneral.Config.ColumnNumber;
            numRow.Value = clsGeneral.Config.RowNumber;
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

            clsGeneral.Config.ColumnNumber = (int)numColumn.Value;
            clsGeneral.Config.RowNumber = (int)numRow.Value;

            ReloadData?.Invoke();
            return true;
        }
        void CustomForm()
        {
            btnOK.Click -= BtnOK_Click;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (SaveData())
                DialogResult = DialogResult.OK;
        }
    }
}

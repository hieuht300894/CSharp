using Microsoft.SqlServer.Management.Smo;
using SQL_Tools.General;
using SQL_Tools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Tools.GUI
{
    public partial class frmMain : Form
    {
        #region Variables
        List<Info> lstDatabase = new List<Info>();
        List<Info> lstTable = new List<Info>();
        #endregion

        #region Forms
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            loadDefault();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            kiemTraKetNoi();
        }

        private void cbbAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            txtUsername.Enabled = txtPassword.Enabled = cbb.SelectedIndex == 1;
        }

        private void lstbxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            layDanhSachTable();
            lbDatabase.Text = lbxDatabase.Text;
        }

        private void lbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTable.Text = lbxTable.Text;
        }

        private void btnGenerateScripts_Click(object sender, EventArgs e)
        {
            GenerateScripts();
        }
        #endregion

        #region Methods
        void loadDefault()
        {
            cbbAuth.SelectedIndex = 0;

            cbbServerName.Items.Clear();
            cbbServerName.Items.Add(Environment.MachineName + @"\SQLEXPRESS");
            cbbServerName.SelectedIndex = 0;
        }
        void kiemTraKetNoi()
        {
            HamKetNoi.LuuChuoiKetNoi(
                cbbServerName.Text.Trim(),
                cbbAuth.SelectedIndex == 0,
                txtUsername.Text.Trim(),
                txtPassword.Text.Trim(),
                10000,
                "master");

            SqlConnection conn = HamKetNoi.KetNoi();
            try { conn.Open(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }

            layDanhSachDB();
        }
        void layDanhSachDB()
        {
            lbxDatabase.SelectedIndexChanged -= lstbxDatabase_SelectedIndexChanged;
            lbxDatabase.Items.Clear();
            lbDatabase.ResetText();
            lstDatabase = new List<Info>(DanhSachDoiTuong.LayDanhSachDB());
            lbxDatabase.Items.AddRange(lstDatabase.Select(x => x.Name).ToArray());
            lbxDatabase.Text = lbxDatabase.Text;
            lbxDatabase.SelectedIndexChanged += lstbxDatabase_SelectedIndexChanged;
        }
        void layDanhSachTable()
        {
            HamKetNoi.LuuChuoiKetNoi(
              cbbServerName.Text.Trim(),
              cbbAuth.SelectedIndex == 0,
              txtUsername.Text.Trim(),
              txtPassword.Text.Trim(),
              10000,
              lbxDatabase.Text);

            lbxTable.Items.Clear();
            lbTable.ResetText();
            lbxTable.SelectedIndexChanged -= lbxTable_SelectedIndexChanged;
            lstTable = new List<Info>(DanhSachDoiTuong.LayDanhSachTable());
            lbxTable.Items.AddRange(lstTable.Select(x => x.Name).ToArray());
            lbTable.Text = lbxTable.Text;
            lbxTable.SelectedIndexChanged += lbxTable_SelectedIndexChanged;
        }
        void GenerateScripts()
        {
            if (!string.IsNullOrEmpty(lbDatabase.Text) && !string.IsNullOrEmpty(lbTable.Text))
            {
                frmGenScript frm = new frmGenScript();
                frm.rtbScript.ResetText();
                frm.rtbScript.AppendText(DanhSachDoiTuong.ScriptsInsert(lbTable.Text));
                frm.ShowDialog();
            }
        }
        #endregion
    }
}

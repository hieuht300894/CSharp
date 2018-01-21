using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BUS;

namespace QLKTXSPKT
{
    public partial class UserControl_DinhMuc : UserControl
    {
       
        public UserControl_DinhMuc()
        {
            InitializeComponent();
        }
        public frmKTX MainForm
        {
            get
            {
                var parent = Parent;
                while (parent != null && (parent as frmKTX) == null)
                {
                    parent = parent.Parent;
                }
                return parent as frmKTX;
            }
        }

        private void LoadDS()
        {
            DataTable dtb = null;
            if (cbx_maDinhMuc.SelectedIndex.ToString() == "0")
                dtb = BUS_DIENNUOC.DSDinhMuc("D");
            else
                dtb = BUS_DIENNUOC.DSDinhMuc("N");
            dataGridView_thongtin.DataSource = dtb;
        }
        private void cbx_maDinhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDS();
        }

        int row;
        private void dataGridView_thongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = dataGridView_thongtin.CurrentCell.RowIndex;
        }
        private void Mo()
        {
            textBox_SoTien.Enabled = true;
        }
        private void Dong()
        {
            textBox_MaDM.Enabled = false;
            textBox_SoTien.Enabled = false;
        }
        private void Xoa()
        {
            textBox_MaDM.ResetText();
            textBox_SoTien.ResetText();
            textBox_ghichu.ResetText();
          
        }
        private void button_Sua_Click(object sender, EventArgs e)
        {
            if (row != (dataGridView_thongtin.Rows.Count - 1))
            {
                textBox_MaDM.Text = dataGridView_thongtin.Rows[row].Cells[0].Value.ToString();
                textBox_SoTien.Text = dataGridView_thongtin.Rows[row].Cells[1].Value.ToString();
                textBox_ghichu.Text = dataGridView_thongtin.Rows[row].Cells[2].Value.ToString();
            }
        }
        private void button_huy_Click(object sender, EventArgs e)
        {
            button_hoanThanh.Enabled = false;
            Dong();
            Xoa();
        }

        private void button_hoanThanh_Click(object sender, EventArgs e)
        {
            BUS_DIENNUOC.SuaGiaDienNuoc(textBox_MaDM.Text, int.Parse(textBox_SoTien.Text));
            LoadDS();
            Xoa();
        }

        private void textBox_SoTien_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_SoTien.Text) == true)
                button_hoanThanh.Enabled = true;
            else
                button_hoanThanh.Enabled = false;
        }
    }
}

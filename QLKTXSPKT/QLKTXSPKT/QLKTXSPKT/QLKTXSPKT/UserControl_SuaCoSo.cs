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
    public partial class UserControl_SuaCoSo : UserControl
    {
        public UserControl_SuaCoSo()
        {
            InitializeComponent();
            LoadDS();
        }
        private void LoadDS()
        {
            DataTable dtb = BUS_CSHT.DSCoSo();
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
        }
        private void button_TaiDS_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            textBox_MaCoSo.Text = dataGridView1.Rows[row].Cells["MaCoSo"].Value.ToString();
            textBox_DiaChi.Text = dataGridView1.Rows[row].Cells["DiaDiem"].Value.ToString();
        }

        private void button_HoanThanhCoSo_Click(object sender, EventArgs e)
        {
            BUS_CSHT.SuaCoSo(textBox_MaCoSo.Text, textBox_DiaChi.Text);
            LoadDS();
            Xoa();
        }
        private void Xoa()
        {
            textBox_DiaChi.ResetText();
            textBox_MaCoSo.ResetText();
        }

        private void button_HuyCoSo_Click(object sender, EventArgs e)
        {
            Xoa();
        }
    }
}

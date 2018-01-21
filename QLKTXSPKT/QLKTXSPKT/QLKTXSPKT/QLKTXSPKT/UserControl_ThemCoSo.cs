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
    public partial class UserControl_ThemCoSo : UserControl
    {
        public UserControl_ThemCoSo()
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

        private void button_HoanThanhCoSo_Click(object sender, EventArgs e)
        {
            BUS_CSHT.ThemCoSo(textBox_MaCoSo.Text, textBox_DiaChi.Text);
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

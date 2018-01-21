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
    public partial class UserControl_ThemTang : UserControl
    {
        public UserControl_ThemTang()
        {
            InitializeComponent();
            LoadDSCoSo();
        }

        private void LoadDSCoSo()
        {
            DataTable dtb=BUS_CSHT.DSCoSo();
            comboBox_MaCoSo.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    comboBox_MaCoSo.Items.Add(dtb.Rows[i]["MaCoSo"].ToString());
        }
        private void LoadDSTang()
        {
            DataTable dtb = BUS_CSHT.DSTang(comboBox_MaCoSo.SelectedItem.ToString());
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
        }
        private void button_TaiDS_Click(object sender, EventArgs e)
        {
        }

        private void comboBox_MaCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSTang();
        }

        private void button_HoanThanhTang_Click(object sender, EventArgs e)
        {
            string gioitinh = "";
            if (radioButton_nam.Checked)
                gioitinh = "Nam";
            if (radioButton_nu.Checked)
                gioitinh = "Nữ";
            int truongtang = -1;
            if (textBox_TruongTang.Text != "")
                truongtang = int.Parse(textBox_TruongTang.Text);
            BUS_CSHT.ThemTang(
                textBox_MaTang.Text,
                gioitinh,
                truongtang,
                comboBox_MaCoSo.SelectedItem.ToString());
            LoadDSTang();
            Xoa();
        }
        private void Xoa()
        {
            textBox_MaTang.ResetText();
            textBox_TruongTang.ResetText();
        }

        private void button_HuyTang_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

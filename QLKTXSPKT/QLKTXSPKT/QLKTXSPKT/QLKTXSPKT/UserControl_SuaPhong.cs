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
    public partial class UserControl_SuaPhong : UserControl
    {
        public UserControl_SuaPhong()
        {
            InitializeComponent();
            LoadDSCoSo();
        }
        private void LoadDSCoSo()
        {
            DataTable dtb = BUS_CSHT.DSCoSo();
            comboBox_MaCoSo.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    comboBox_MaCoSo.Items.Add(dtb.Rows[i]["MaCoSo"].ToString());
        }
        private void LoadDSTang()
        {
            DataTable dtb = BUS_CSHT.DSTang(comboBox_MaCoSo.SelectedItem.ToString());
            comboBox_MaTang.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    comboBox_MaTang.Items.Add(dtb.Rows[i]["MaTang"].ToString());
        }
        private void LoadDSPhong()
        {
            DataTable dtb = BUS_CSHT.DSPhong(comboBox_MaTang.SelectedItem.ToString(), 2);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
        }
        private void button_TaiDS_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            textBox_MaPhong.Text = dataGridView1.Rows[r].Cells["MaPhong"].Value.ToString();
            textBox_SLMax.Text = dataGridView1.Rows[r].Cells["SoLuongMax"].Value.ToString();
            textBox_TruongPhong.Text = dataGridView1.Rows[r].Cells["TruongPhong"].Value.ToString();
        }

        private void button_HoanThanhPhong_Click(object sender, EventArgs e)
        {
            int truongphong = -1;
            if (textBox_TruongPhong.Text != "")
                truongphong = int.Parse(textBox_TruongPhong.Text);
            BUS_CSHT.SuaPhong(textBox_MaPhong.Text, truongphong, int.Parse(textBox_SLMax.Text));
            LoadDSPhong();
            Xoa();
        }
        private void Xoa()
        {
            textBox_MaPhong.ResetText();
            textBox_SLMax.ResetText();
            textBox_TruongPhong.ResetText();
        }

        private void button_HuyPhong_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void comboBox_MaCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSTang();
        }

        private void comboBox_MaTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSPhong();
        }

        private void textBox_TruongPhong_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_TruongPhong.Text) == true)
                button_HoanThanhPhong.Enabled = true;
            else
                button_HoanThanhPhong.Enabled = false;
        }

        private void textBox_SLMax_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_SLMax.Text) == true)
                button_HoanThanhPhong.Enabled = true;
            else
                button_HoanThanhPhong.Enabled = false;
        }
    }
}

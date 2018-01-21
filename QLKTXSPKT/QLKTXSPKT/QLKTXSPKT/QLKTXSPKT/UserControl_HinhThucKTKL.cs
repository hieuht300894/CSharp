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
    public partial class UserControl_HinhThucKTKL : UserControl
    {
        public UserControl_HinhThucKTKL()
        {
            InitializeComponent();
        }

        private void LoadDSHT(int chon)
        {
            DataTable dtb = BUS_HINHTHUC.DSHinhThuc(chon);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
        }
        private void button_taiDS_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                groupBox1.Visible = true;
                LoadDSHT(comboBox1.SelectedIndex);
                ChucNang(true, false, false);
            }
        }
        int chucnang = -1;
        int r = -1;
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            r = dataGridView1.CurrentCell.RowIndex;
            if (r != dataGridView1.Rows.Count - 1)
            {
                switch (chucnang)
                {
                    case 0://Sửa
                        textBox_tenht.Text = dataGridView1.Rows[r].Cells["TenHT"].Value.ToString();
                        textBox_mota.Text = dataGridView1.Rows[r].Cells["MoTa"].Value.ToString();
                        if (dataGridView1.Rows[r].Cells["KieuHT"].Value.ToString() == "KT")
                            radioButton_kt.Checked = true;
                        else
                            radioButton_kl.Checked = true;
                        break;
                    case 1://Xóa
                        textBox_tenht.Text = dataGridView1.Rows[r].Cells["TenHT"].Value.ToString();
                        textBox_mota.Text = dataGridView1.Rows[r].Cells["MoTa"].Value.ToString();
                        if (dataGridView1.Rows[r].Cells["KieuHT"].Value.ToString() == "KT")
                            radioButton_kt.Checked = true;
                        else
                            radioButton_kl.Checked = true;
                        break;
                }
            }
        }

        private void ChucNang(bool them, bool sua, bool xoa)
        {
            button_Sua.Enabled = sua;
            button_Them.Enabled = them;
            button_Xoa.Enabled = xoa;
        }
        private void button_Them_Click(object sender, EventArgs e)
        {
            chucnang = -1;
            ChucNang(false, true, true);
        }

        private void button_Sua_Click(object sender, EventArgs e)
        {
            chucnang = 0;
            ChucNang(true, false, true);
        }

        private void button_Xoa_Click(object sender, EventArgs e)
        {
            chucnang = 1;
            ChucNang(true, true, false);
        }
        private void Xoa()
        {
            textBox_mota.ResetText();
            textBox_tenht.ResetText();
        }
        private void button_xacnhan_Click(object sender, EventArgs e)
        {
            switch (chucnang)
            {
                case -1://Thêm
                    if (radioButton_kt.Checked == true)
                        BUS_HINHTHUC.HinhThuc(chucnang, chucnang, textBox_tenht.Text, textBox_mota.Text, "KT");
                    else
                        BUS_HINHTHUC.HinhThuc(chucnang, chucnang, textBox_tenht.Text, textBox_mota.Text, "KL");
                    break;
                case 0://Sửa
                    if (radioButton_kt.Checked == true)
                        BUS_HINHTHUC.HinhThuc(chucnang,int.Parse(dataGridView1.Rows[r].Cells["MaHT"].Value.ToString()), textBox_tenht.Text, textBox_mota.Text, "KT");
                    else
                        BUS_HINHTHUC.HinhThuc(chucnang, int.Parse(dataGridView1.Rows[r].Cells["MaHT"].Value.ToString()), textBox_tenht.Text, textBox_mota.Text, "KL");
                    break;
                case 1://Xóa 
                    BUS_HINHTHUC.HinhThuc(chucnang, int.Parse(dataGridView1.Rows[r].Cells["MaHT"].Value.ToString()), "", "", "");
                    break;
            }
            LoadDSHT(comboBox1.SelectedIndex);
            Xoa();
        }

        private void button_huy_Click(object sender, EventArgs e)
        {
            Xoa();
        }
    }
}

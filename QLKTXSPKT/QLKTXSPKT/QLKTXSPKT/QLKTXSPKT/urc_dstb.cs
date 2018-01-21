using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAO;
namespace QLKTXSPKT
{
    public partial class urc_dstb : UserControl
    {
        public urc_dstb()
        {
            InitializeComponent();
            Chia();
        }
        private void Chia()
        {
            if (DATA.chucnang4[0] == 1)
            {
                btn_them.Enabled = true;
                btn_sua.Enabled = true;
            }
            else
            {
                btn_them.Enabled = false;
                btn_sua.Enabled = false;
            }
        }
        string maphong;
        int flag;
        int MaTB;
        private void btn_them_Click(object sender, EventArgs e)
        {
            flag = 1;
            lbl_Suasinhvien.Text = "Thêm Thiết Bị Phòng";
            groupBox1.Visible = true;
        }
        private void btn_sua_Click(object sender, EventArgs e)
        {
            flag = 2;
            lbl_Suasinhvien.Text = "Thêm Thiết Bị Vào Phòng";
            groupBox1.Visible = true;
            comboBox_Phong2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            maphong = comboBox_MaPhong.Text;
            gridView1.Columns.Clear();
            gridControl1.DataSource = BUS_TB.DSTBPhong(maphong);
            btn_sua.Visible = true;
            btn_them.Visible = true;
            groupBox1.Visible = false;

        }
        public void resetText()
        {
            comboBox_TenTB.ResetText();
            textBox_SoLuong.ResetText();
            textBox_Mota.ResetText();
        }

        private void button_HoanThanh_Click(object sender, EventArgs e)
        {
            string MaPhong = comboBox_Phong2.Text;
            MaTB = int.Parse(comboBox_TenTB.SelectedValue.ToString());
            int soluong = int.Parse(textBox_SoLuong.Text);
            string MoTa = textBox_Mota.Text;
            switch (flag)
            {
                case 1:
                    BUS_TB.ThemTBPhong(MaTB, MaPhong, soluong, MoTa, DateTime.Now);
                    LoadPhong();
                    gridView1.Columns.Clear();
                    resetText();

                    break;
                case 2:
                    BUS_TB.ThemTBPhong(MaTB, maphong, soluong, MoTa, DateTime.Now);
                    BUS_TB.DSTBPhong(maphong);
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = BUS_TB.DSTBPhong(maphong);
                    resetText();
                    break;
            }
        }

        private void button_HuyThaoTac_Click(object sender, EventArgs e)
        {
            resetText();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            switch (flag)
            {
                case 2:

                    comboBox_Phong2.Text = gridView1.GetRowCellValue(e.RowHandle, "MaPhong").ToString();
                    break;
            }
        }
        private void LoadPhong()
        {
            DataTable dtb = BUS_TB.DSPhong();
            comboBox_MaPhong.Items.Clear();
            if (dtb.Rows.Count > 0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                    comboBox_MaPhong.Items.Add(dtb.Rows[i]["MaPhong"].ToString());
            }
        }
        private void LoadTenTB()
        {
            DataTable dtb = BUS_TB.DSTenThietBi();
            if (dtb.Rows.Count > 0)
            {
                comboBox_TenTB.DataSource = dtb;
                comboBox_TenTB.DisplayMember = "TenTB";
                comboBox_TenTB.ValueMember = "MaTB";
            }
        }
        private void LoadMaPhong()
        {
            DataTable dtb = BUS_TB.LayMaPhong();
            comboBox_Phong2.Items.Clear();
            if (dtb.Rows.Count > 0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                    comboBox_Phong2.Items.Add(dtb.Rows[i][0].ToString());
            }
        }

        private void urc_dstb_Load(object sender, EventArgs e)
        {
            LoadPhong();
            LoadTenTB();
            LoadMaPhong();
            btn_sua.Visible = false;
        }

        private void textBox_SoLuong_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_SoLuong.Text) == true)
                button_HoanThanh.Enabled = true;
            else
                button_HoanThanh.Enabled = false;
        }
    }
}

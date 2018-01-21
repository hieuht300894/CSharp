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
using DAO;

namespace QLKTXSPKT
{
    public partial class UserControl_TaiKhoan : UserControl
    {
        public UserControl_TaiKhoan()
        {
            InitializeComponent();
            Chia();
            LoadDS();
        }
        private void Chia()
        {
            if (DATA.chucnang7[1] == 1)
            {
                button_xacnhan.Enabled = true;

            }
            else
            {
                button_xacnhan.Enabled = false;
            }
            if (DATA.chucnang7[2] == 1)
            {
                button__mo.Enabled = true;
                button_khoa.Enabled = true;
                button_chuyenchucvu.Enabled = true;
            }
            else
            {
                button__mo.Enabled = false;
                button_khoa.Enabled = false;
                button_chuyenchucvu.Enabled = false;
            }
            if (DATA.chucnang7[3] == 1)
                button_xoa.Enabled = true;
            else
                button_xoa.Enabled = false;
        }
        private void button_xacnhan_Click(object sender, EventArgs e)
        {
            if (ErrorMessage.error == "")
            {
                if (textBox_pass.Text == textBox_repass.Text && comboBox_office.SelectedItem != null)
                {
                    BUS_CHUCVU.ThemTaiKhoan(textBox_user.Text, XuLyDuLieu.MD5(textBox_pass.Text), int.Parse(comboBox_office.SelectedValue.ToString()));
                    textBox_taikhoan.Text = textBox_user.Text;
                }
            }
        }
        //DataTable dtb;
        private void LoadDS()
        {
            DataTable dtb = BUS_CHUCVU.DSChucVu(1);

            comboBox_office.DataSource = dtb;
            comboBox_office.DisplayMember = "ChiTietChucVu";
            comboBox_office.ValueMember = "MaChucVu";

            comboBox_chucvu.DataSource = dtb;
            comboBox_chucvu.DisplayMember = "ChiTietChucVu";
            comboBox_chucvu.ValueMember = "MaChucVu";
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void LoadTaiKhoan()
        {
            DataTable dtb = BUS_CHUCVU.ChiTietTK(textBox_taikhoan.Text);
            if (dtb.Rows.Count == 0)
            {
                label_tinhtrang.Text = "Không tồn tại tài khoản này";
                label_chucvu.Text = "";
            }
            else
            {
                if (int.Parse(dtb.Rows[0]["TinhTrang"].ToString()) == 0)
                    label_tinhtrang.Text = "Khóa";
                else
                    label_tinhtrang.Text = "Hoạt động";
                label_chucvu.Text = dtb.Rows[0]["ChiTietChucVu"].ToString();
            }
        }
        private void textBox_taikhoan_TextChanged(object sender, EventArgs e)
        {
            LoadTaiKhoan();
        }

        private void button__mo_Click(object sender, EventArgs e)
        {
            BUS_CHUCVU.SuaTK(textBox_taikhoan.Text, 1);
            LoadTaiKhoan();
        }

        private void button_khoa_Click(object sender, EventArgs e)
        {
            BUS_CHUCVU.SuaTK(textBox_taikhoan.Text, 0);
            LoadTaiKhoan();
        }

        private void button_xoa_Click(object sender, EventArgs e)
        {
            BUS_CHUCVU.XoaTK(textBox_taikhoan.Text);
            LoadTaiKhoan();
        }

        private void button_chuyenchucvu_Click(object sender, EventArgs e)
        {
            BUS_CHUCVU.ChuyenChucVu(textBox_taikhoan.Text, int.Parse(comboBox_chucvu.SelectedValue.ToString()));
            LoadTaiKhoan();
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    LoadDS();
        //}

        //private void button_xacnhan_Click(object sender, EventArgs e)
        //{
        //    if (dtb.Rows[comboBox_office.SelectedIndex]["TinhTrang"].ToString() != "0")
        //    {
        //        textBox_user.Text = XuLyDuLieu.ChuyenDauThanhKhongDau(textBox_user.Text);
        //        if (textBox_pass.Text == textBox_repass.Text)
        //        {
        //            BUS_CHUCVU.ThemTaiKhoan(
        //                textBox_user.Text,
        //                XuLyDuLieu.ChuyenDauThanhKhongDau(textBox_pass.Text),
        //                int.Parse(comboBox_office.SelectedValue.ToString()));
        //            LoadDS();
        //            textBox_taikhoan.Text = textBox_user.Text;
        //            ChiTietTK();
        //        }
        //        else
        //        {
        //            textBox_pass.Focus();
        //            textBox_pass.ResetText();
        //            textBox_repass.ResetText();
        //        }
        //    }
        //    else
        //        MessageBox.Show("Chức vụ này bị khóa");
        //}

     
        //DataTable dtb_chitiet;

        //private void ChiTietTK()
        //{
        //    dtb_chitiet = BUS_CHUCVU.ChiTietTK(textBox_taikhoan.Text.Trim());
        //    if (dtb_chitiet.Rows.Count > 0)
        //    {
        //        switch (int.Parse(dtb_chitiet.Rows[0]["TinhTrang"].ToString()))
        //        {
        //            case 0:
        //                label_tinhtrang.Text = "Chưa kích hoạt";
        //                break;
        //            case 1:
        //                label_tinhtrang.Text = "Kích hoạt";
        //                break;
        //        }
        //        label_chucvu.Text = dtb_chitiet.Rows[0]["ChiTietChucVu"].ToString();
        //    }
        //    else
        //    {
        //        label_tinhtrang.Text = "Không có tài khoản này";
        //        label_chucvu.ResetText();
        //    }
        //}
        //private void button_xem_Click(object sender, EventArgs e)
        //{
        //    ChiTietTK();
        //}

        //private void button__mo_Click(object sender, EventArgs e)
        //{
        //    if (dtb_chitiet.Rows.Count > 0)
        //        BUS_CHUCVU.KichHoatTaiKhoan(
        //            dtb_chitiet.Rows[0]["TenTaiKhoan"].ToString().Trim(),
        //            dtb_chitiet.Rows[0]["TenChucVu"].ToString().Trim(),
        //            1);
        //    ChiTietTK();
        //}

        //private void button_khoa_Click(object sender, EventArgs e)
        //{
        //    if (dtb_chitiet.Rows.Count > 0)
        //        BUS_CHUCVU.KichHoatTaiKhoan(
        //            dtb_chitiet.Rows[0]["TenTaiKhoan"].ToString().Trim(),
        //            dtb_chitiet.Rows[0]["TenChucVu"].ToString().Trim(),
        //            0);
        //    ChiTietTK();
        //}

        //private void button_xoa_Click(object sender, EventArgs e)
        //{
        //    if (dtb_chitiet.Rows.Count > 0)
        //    {
        //        if (int.Parse(dtb_chitiet.Rows[0]["TinhTrang"].ToString()) == 0)
        //        {
        //            BUS_CHUCVU.KichHoatTaiKhoan(
        //                dtb_chitiet.Rows[0]["TenTaiKhoan"].ToString().Trim(),
        //                dtb_chitiet.Rows[0]["TenChucVu"].ToString().Trim(),
        //                -1);
        //            if (ErrorMessage.error == "")
        //                ChiTietTK();
        //            else
        //                label_tinhtrang.Text = ErrorMessage.error;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Phải khóa tài khoản rồi mới xóa");
        //        }
        //    }
        //}

        //private void textBox_user_TextChanged(object sender, EventArgs e)
        //{
        //    dtb_chitiet = BUS_CHUCVU.ChiTietTK(textBox_user.Text.Trim());
        //    if (dtb_chitiet.Rows.Count > 0 || textBox_user.Text.Trim() == "")
        //        button_thongbao.BackgroundImage = Properties.Resources.no_tick;
        //    else
        //        button_thongbao.BackgroundImage = Properties.Resources.tick;
        //}

    }
}

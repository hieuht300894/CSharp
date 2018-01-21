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
    public partial class urc_sinhvien_cs : UserControl
    {
        public urc_sinhvien_cs()
        {
            InitializeComponent();
            Chia();
        }


        public void Chia()
        {
            if (DATA.chucnang1[0] == 1)
            {
                btn_thongtinSV.Enabled = true;
                btn_TTTN.Enabled = true;
            }
            else
            {
                btn_thongtinSV.Enabled = false;
                btn_TTTN.Enabled = false;
            }
            if (DATA.chucnang1[1] == 1)
            {
                btn_them.Enabled = true;
                btn_ThemTN.Enabled = true;
            }
            else
            {
                btn_them.Enabled = false;
                btn_ThemTN.Enabled = false;
            }
            if (DATA.chucnang1[2] == 1)
            {
                btn_sua.Enabled = true;
                btn_chuyenphong.Enabled = true;
            }
            else
            {
                btn_sua.Enabled = false;
                btn_chuyenphong.Enabled = false;
            }
            if (DATA.chucnang1[3] == 1)
            {
                btn_xoa.Enabled = true;
            }
            else
            {
                btn_xoa.Enabled = false;
            }
        }
          
        int flag = 0;
        // load chi tiet sinh vien 
        public void LoadChiTietSV(int mssv)//dateTimePicker_NgaySinh.Value.ToString("yyyy/MM/dd")
        {
            DataTable dt = BUS_SV.ChiTietSV(mssv);
            if (dt.Rows.Count > 0)
            {
                textBox_MSSV.Text = mssv.ToString();
                textBox_hoten.Text = dt.Rows[0]["HoTen"].ToString();
                comboBox_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                textBox_CMND.Text = dt.Rows[0]["CMND"].ToString();
                dateTimePicker_NgaySinh.Text = dt.Rows[0]["NgaySinh"].ToString();
                textBox_NienKhoa.Text = dt.Rows[0]["NienKhoa"].ToString();
                textBox_HoKhau.Text = dt.Rows[0]["HoKhau"].ToString();
                textBox_SDT.Text = dt.Rows[0]["SDT"].ToString();
                textBox_DoUutien.Text = dt.Rows[0]["DienUuTien"].ToString();
                txt_ghichu.Text = dt.Rows[0]["GhiChu"].ToString();
            }

        }
        private void DongMoChucNang(bool[] A)
        {
            //btn_them.Enabled = A[0];
            //btn_sua.Enabled = A[1];
            //btn_xoa.Enabled = A[2];
            //btn_thongtinSV.Enabled = A[3];
            //btn_TTTN.Enabled = A[4];
            //btn_ThemTN.Enabled = A[5];
            //btn_chuyenphong.Enabled = A[6];
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            mo();
            groupBox1.Visible = true;
            resetText();
            lbl_Suasinhvien.Text = "THÊM SINH VIÊN";
            button_HoanThanh.Visible = true;
            button_HuyThaoTac.Visible = true;
            button_HoanThanh.Enabled = true;
            button_HuyThaoTac.Enabled = true;
            button_HoanThanh.Enabled = true;
            button_HuyThaoTac.Enabled = true;
            flag = 1;
            bool[] A = new bool[7] { false, true, true, true, true,true,true };
            DongMoChucNang(A);
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            lbl_Suasinhvien.Text = "SỬA THÔNG TIN SINH VIÊN";
            button_HoanThanh.Visible = true;
            button_HuyThaoTac.Visible = true;
            mo();
            button_HoanThanh.Enabled = true;
            button_HuyThaoTac.Enabled = true;
            textBox_MSSV.Enabled = false;
            flag = 2;
            bool[] A = new bool[7] { true, false, true, true, true, true, true };
            DongMoChucNang(A);
        }
        int MSSV = -1;
        private void button_xoa_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            flag = 3;
            bool[] A = new bool[7] { false, false, true, false, false, false, false };
            DongMoChucNang(A);
            BUS_SV.XoaSV(MSSV);
            gridView1.Columns.Clear();//xoa nhung cot da co san 
            gdc_sinhvien.DataSource = BUS_SV.LoadDSSinhVien(1);
            resetText();
        }

        private void btn_thongtinSV_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            lbl_Suasinhvien.Text = "THÔNG TIN SINH VIÊN";
            button_HoanThanh.Visible = false;
            button_HuyThaoTac.Visible = false;
            dong();
            flag = 4;
            bool[] A = new bool[7] { true, true, true, false, true, true, true };
            DongMoChucNang(A);
        }
        private void btn_TTTN_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            flag = 5;
            bool[] A = new bool[7] { true, true, true, true, false, true, true };
            DongMoChucNang(A);
        }

        private void btn_ThemTN_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            flag = 6;
            bool[] A = new bool[7] { true, true, true, true, true, false, true };
            DongMoChucNang(A);
        }
        private void btn_capphong_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            flag = 7;
            bool[] A = new bool[7] { true, true, true, true, true, true, false };
            DongMoChucNang(A);
           
        }

        public void dong()
        {
            textBox_MSSV.ReadOnly = true;
            textBox_hoten.ReadOnly = true;
            comboBox_GioiTinh.Enabled = false;
            textBox_CMND.ReadOnly = true;
            dateTimePicker_NgaySinh.Enabled = false;
            textBox_HoKhau.ReadOnly = true;
            textBox_SDT.ReadOnly = true;
            textBox_DoUutien.ReadOnly = true;
            dateTimePicker_NgaySinh.Enabled = false;
            txt_ghichu.ReadOnly = true;
        }
        public void mo()
        {
            textBox_MSSV.ReadOnly = false;
            textBox_hoten.ReadOnly = false;
            comboBox_GioiTinh.Enabled = true;
            textBox_CMND.ReadOnly = false;
            dateTimePicker_NgaySinh.Enabled = true;
            textBox_HoKhau.ReadOnly = false;
            textBox_SDT.ReadOnly = false;
            textBox_DoUutien.ReadOnly = false;
            dateTimePicker_NgaySinh.Enabled = true;
            txt_ghichu.ReadOnly = false;
            
        }
       
        
        private void resetText()
        {
            textBox_MSSV.ResetText();
            textBox_hoten.ResetText();
            comboBox_GioiTinh.ResetText();
            textBox_CMND.ResetText();
            dateTimePicker_NgaySinh.ResetText();
            textBox_NienKhoa.ResetText();
            textBox_HoKhau.ResetText();
            textBox_SDT.ResetText();
            textBox_DoUutien.ResetText();
        }


        private void KiemDK()
        {
            if (XuLyDuLieu.IsNumber(textBox_MSSV.Text) == true &&
                textBox_MSSV.Text.Length > 1 &&
                textBox_hoten.Text != "" &&
                comboBox_GioiTinh.SelectedItem != null &&
                XuLyDuLieu.IsNumber(textBox_CMND.Text) == true &&
                textBox_HoKhau.Text != "")
                button_HoanThanh.Enabled = true;
            else
                button_HoanThanh.Enabled = false;
        }

        private void button_HoanThanh_Click_1(object sender, EventArgs e)
        {

            int MSSV = int.Parse(textBox_MSSV.Text);

            string HoTen = textBox_hoten.Text;
            string gioitinh = "";
            if (comboBox_GioiTinh.SelectedItem == null)
                gioitinh = comboBox_GioiTinh.Text;
            else
                gioitinh = comboBox_GioiTinh.SelectedItem.ToString();
            string CMND = textBox_CMND.Text;
            string NgaySinh = dateTimePicker_NgaySinh.Value.ToString("yyyy/MM/dd");
            string NienKhoa = textBox_NienKhoa.Text;
            string HoKhau = textBox_HoKhau.Text;
            string SDT = textBox_SDT.Text;
            string DoUuTien = textBox_DoUutien.Text;
            int TinhTrang = 1;
            string GhiChu = " ";
            switch (flag)
            {
                case 1:
                    {
                        //thêm SV
                        BUS_SV.ThemSinhVien(MSSV, HoTen, gioitinh, CMND, NgaySinh, NienKhoa, HoKhau, SDT, DoUuTien, 1, GhiChu);
                        frm_CapPhong frmCapPhong = new frm_CapPhong();
                        frmCapPhong.textBox_mssv.Text = MSSV.ToString();
                        frmCapPhong.gioitinh = comboBox_GioiTinh.SelectedItem.ToString();
                        frmCapPhong.flag = 1;
                        frmCapPhong.Show();
                        gridView1.Columns.Clear();//xoa nhung cot da co san 
                        gdc_sinhvien.DataSource = BUS_SV.LoadDSSinhVien(1);
                        resetText();
                    }
                    break;
                case 2:
                    {
                        BUS_SV.SuathongtinSV(MSSV, HoTen, gioitinh, CMND, NgaySinh, NienKhoa, HoKhau, SDT, DoUuTien, TinhTrang, GhiChu);
                        gridView1.Columns.Clear();//xoa nhung cot da co san 
                        gdc_sinhvien.DataSource = BUS_SV.LoadDSSinhVien(1);
                        resetText();
                    }
                    break;
            }
        }

        private void button_HuyThaoTac_Click_1(object sender, EventArgs e)
        {
            resetText();
        }

        private void textBox_MSSV_TextChanged(object sender, EventArgs e)
        {
            if (textBox_MSSV.Text.Length >= 2)
            {
                int ma = int.Parse(textBox_MSSV.Text.Substring(0, 2));
                textBox_NienKhoa.Text = "20" + ma.ToString() + "-20" + (ma + 4).ToString();
            }
            else
                textBox_NienKhoa.ResetText();
            KiemDK();
        }

   
       

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            switch (flag)
            {
                case 2:
                    //Sua SV
                    LoadChiTietSV(int.Parse(gridView1.GetRowCellValue(e.RowHandle, "MSSV").ToString()));
                    mo();
                    break;
                case 3:
                    MSSV = int.Parse(gridView1.GetRowCellValue(e.RowHandle, "MSSV").ToString());
                    break;
                case 4:
                    LoadChiTietSV(int.Parse(gridView1.GetRowCellValue(e.RowHandle, "MSSV").ToString()));
                    dong();
                    //Thong tin SV
                    break;
                case 5:
                    //Thong tin than nhan
                    FormThanNhan frmTN = new FormThanNhan();
                    DataTable dtb = BUS_TN.ChiTietThanNhanSV(int.Parse(gridView1.GetRowCellValue(e.RowHandle, "MSSV").ToString()));
                    frmTN.gridControl1.DataSource = dtb;
                    frmTN.Show();
                    break;
                case 6:
                    //them than  nhan
                    frmThemThanNhan themTN = new frmThemThanNhan();
                    themTN.textBox_MSSV.Text = gridView1.GetRowCellValue(e.RowHandle, "MSSV").ToString();
                    themTN.Show();
                    break;
                case 7:
                    frm_CapPhong frmCapPhong = new frm_CapPhong();
                    frmCapPhong.textBox_mssv.Text = gridView1.GetRowCellValue(e.RowHandle, "MSSV").ToString();
                    frmCapPhong.gioitinh = gridView1.GetRowCellValue(e.RowHandle, "GioiTinh").ToString();
                    frmCapPhong.flag = 2;
                    frmCapPhong.Show();
                    break;

            }
        }

        private void textBox_hoten_TextChanged(object sender, EventArgs e)
        {
            KiemDK();
        }

        private void textBox_CMND_TextChanged(object sender, EventArgs e)
        {
            KiemDK();
        }

        private void textBox_HoKhau_TextChanged(object sender, EventArgs e)
        {
            KiemDK();
        }

      
      
    }
   
    
}

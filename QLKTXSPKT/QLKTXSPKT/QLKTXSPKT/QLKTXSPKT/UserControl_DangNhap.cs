using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO;
using BUS;

namespace QLKTXSPKT
{
    public partial class UserControl_DangNhap : UserControl
    {
        public UserControl_DangNhap()
        {
            InitializeComponent();

        }
        public frmKTX frm
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

        private void button_xacnhan_Click(object sender, EventArgs e)
        {
            KiemTraTaiKhoan();
        }
        private void KiemTraTaiKhoan()
        {
            DATA.UserName = "TruyCap";
            DATA.PassWord = "";
            if (DAO_CHECKLOGIN.KiemTraKetNoiTaiKhoan() == true)
            {
                DataTable dtb = BUS_CHUCVU.ChucVuCuaTaiKhoan(textBox_user.Text, XuLyDuLieu.MD5(textBox_pass.Text));
                if (dtb.Rows.Count == 0)
                    label_thongbao.Text = "Tài khoản hoặc mật khẩu không đúng";
                else
                {
                    if (dtb.Rows[0]["TinhTrang"].ToString() != "0")
                    {
                        ChiaChucNang(int.Parse(dtb.Rows[0]["MaChucVu"].ToString()));
                        DATA.UserName = dtb.Rows[0]["TenChucVu"].ToString();
                        DATA.PassWord = XuLyDuLieu.sDecrypt(dtb.Rows[0]["MatKhau"].ToString());
                        frm.label_thongbao.Visible = true;
                        frm.label_thongbao.Text = "Xin chào: " + textBox_user.Text;
                        UserControl_DoiMatKhau.Username = textBox_user.Text;
                        frm.grp_ThongTin.Controls.Clear();
                    }
                    else
                        label_thongbao.Text = "Tài khoản đang bị khóa";
                }
            }
            else
                label_thongbao.Text = "Kết nối máy chủ thất bại";
        }
        private void ChucNang(bool[] A)
        {
            frm.ribbonPage_SinhVien.Visible = A[0];
            frm.ribbonPage_CoSoHaTang.Visible = A[1];
            frm.ribbonPage_QLDienNuoc.Visible = A[2];
            frm.ribbonPage_QLThietBi.Visible = A[3];
            frm.ribbonPage_KT_KL.Visible = A[4];
            frm.ribbonPage_hopdong.Visible = A[5];
            frm.ribbonPage_admin.Visible = A[6];
        }
        private void ChiaChucNang(int MaChucVu)
        {
            DataTable dtb_phancong = DAO_CHUCVU.DSPhanCong(MaChucVu);
            if (dtb_phancong.Rows.Count > 0)
            {
                int[] A = new int[4];
                bool[] chucnang = new bool[7];
                if (dtb_phancong.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb_phancong.Rows.Count; i++)
                    {
                        A[0] = int.Parse(dtb_phancong.Rows[i]["ChiXem"].ToString());
                        A[1] = int.Parse(dtb_phancong.Rows[i]["Them"].ToString());
                        A[2] = int.Parse(dtb_phancong.Rows[i]["Sua"].ToString());
                        A[3] = int.Parse(dtb_phancong.Rows[i]["Xoa"].ToString());
                        chucnang[i] = IntSangBool(A);
                        switch (i)
                        {
                            case 0:
                                DATA.chucnang1 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang1[j] = A[j];
                                frm.Chia(i);
                                break;
                            case 1:
                                 DATA.chucnang2 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang2[j] = A[j];
                                frm.Chia(i);
                                break;
                            case 2:
                                  DATA.chucnang3 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang3[j] = A[j];
                                frm.Chia(i);
                                break;
                            case 3:
                                DATA.chucnang4 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang4[j] = A[j];
                                frm.Chia(i);
                                break;
                            case 4:
                                DATA.chucnang5 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang5[j] = A[j];
                                frm.Chia(i);
                                break;
                            case 5:
                                DATA.chucnang6 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang6[j] = A[j];
                                frm.Chia(i);
                                break;
                            case 6:
                                DATA.chucnang7 = new int[4];
                                for (int j = 0; j < A.Length; j++)
                                    DATA.chucnang7[j] = A[j];
                                frm.Chia(i);
                                break;
                        }
                    }
                }
                ChucNang(chucnang);
            }
        }
        private bool IntSangBool(int[] A)
        {
            bool kq = false;
            int i = 0;
            for (i = 0; i < A.Length; i++)
            {
                if (A[i] == 1)
                {
                    kq = true;
                    break;
                }
            }
            return kq;
        }

        private void textBox_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                KiemTraTaiKhoan();
            }
        }
    }
}

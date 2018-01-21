using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO;
using BUS;
using DevExpress.XtraGrid.Views.Grid;

namespace QLKTXSPKT
{
    public partial class frmKTX : Form
    {
        public frmKTX()
        {
            InitializeComponent();
            TatChucNang();
        }
        public void TatChucNang()
        {
            ribbonPage_CoSoHaTang.Visible = false;
            ribbonPage_hopdong.Visible = false;
            ribbonPage_KT_KL.Visible = false;
            ribbonPage_QLDienNuoc.Visible = false;
            ribbonPage_QLThietBi.Visible = false;
            ribbonPage_SinhVien.Visible = false;
            ribbonPage_admin.Visible = false;
        }

        private void barButtonItem_TTDinhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_DinhMuc dinhmuc = new UserControl_DinhMuc();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(dinhmuc);
        }

        private void barButtonItem_capnhatDN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_CapNhatDienNuoc CapNhatDienNuoc = new UserControl_CapNhatDienNuoc();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(CapNhatDienNuoc);
        }

        private void barButtonItem_DongTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_DongTienDienNuoc DongTienDienNuoc = new UserControl_DongTienDienNuoc();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(DongTienDienNuoc);
        }

        private void barButtonItem_themDN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_ThemDNLanDau ThemDNLanDau = new UserControl_ThemDNLanDau();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(ThemDNLanDau);
        }

        private void barButtonItem_DSCSHT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_CoSoHaTang CoSoHaTang = new UserControl_CoSoHaTang();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(CoSoHaTang);
        }

        private void barButtonItem_ThemCoSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_ThemCoSo ThemCoSo = new UserControl_ThemCoSo();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(ThemCoSo);
        }

        private void barButtonItem_ThemTang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_ThemTang ThemTang = new UserControl_ThemTang();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(ThemTang);
        }

        private void barButtonItem_ThemPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_ThemPhong ThemPhong = new UserControl_ThemPhong();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(ThemPhong);
        }

        private void barButtonItem_SuaTTCoSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_SuaCoSo SuaCoSo = new UserControl_SuaCoSo();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(SuaCoSo);
        }

        private void barButtonItem_SuaTTTang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_SuaTang SuaTang = new UserControl_SuaTang();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(SuaTang);
        }

        private void barButtonItem_SuaTTPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_SuaPhong SuaPhong = new UserControl_SuaPhong();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(SuaPhong);
        }

        private void barButtonItem_DangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_DangNhap DangNhap = new UserControl_DangNhap();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(DangNhap);

            barButtonItem_DangXuat.Enabled = true;
            barButtonItem_DangNhap.Enabled = false;
        }

        private void barButtonItem_TaoTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_TaiKhoan TaiKhoan = new UserControl_TaiKhoan();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(TaiKhoan);
        }

        private void barButtonItem_themCV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_ChucVu ChucVu = new UserControl_ChucVu();
            UserControl_TaiKhoan TaiKhoan = new UserControl_TaiKhoan();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(ChucVu);
        }

        private void barButtonItem_DangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonItem_DangXuat.Enabled = false;
            barButtonItem_DangNhap.Enabled = true;
            TatChucNang();
            DATA.UserName = "";
            DATA.PassWord = "";
            label_thongbao.Visible = false;
            label_thongbao.Text = "";
            grp_ThongTin.Controls.Clear();
        }

        private void barButtonItem_DSKTKL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_HinhThucKTKL HinhThucKTKL = new UserControl_HinhThucKTKL();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(HinhThucKTKL);
        }

        private void barButtonItem_DSSV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            urc_sinhvien_cs urc_sv = new urc_sinhvien_cs();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(urc_sv);
            urc_sv.gridView1.Columns.Clear();//xoa nhung cot da co san 
            urc_sv.gdc_sinhvien.DataSource = BUS_SV.LoadDSSinhVien(1);
        }

        private void barButtonItem_DSSVKoO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            urc_dsko ko = new urc_dsko();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(ko);
            ko.gridView1.Columns.Clear();
            ko.gridControl1.DataSource = BUS_SV.LoadDSSinhVien(0);
        }

        private void barButtonItem_DSTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControl_DSTieuThuDN DSTieuThuDN = new UserControl_DSTieuThuDN();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(DSTieuThuDN);
        }

        private void label_thongbao_Click(object sender, EventArgs e)
        {
            UserControl_DoiMatKhau DoiMatKhau = new UserControl_DoiMatKhau();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(DoiMatKhau);
        }

        private void barButtonItem_ThongTinTB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            urc_thietbi tb = new urc_thietbi();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(tb);
            tb.gridView1.Columns.Clear();
            tb.gridControl1.DataSource = BUS_TB.DSThietBi();
        }

        private void barButtonItem_DSTBPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            urc_dstb tb = new urc_dstb();
            grp_ThongTin.Controls.Clear();
            grp_ThongTin.Controls.Add(tb);
        }

        public void Chia(int danhmuc)
        {
            switch (danhmuc)
            {
                case 1:
                    if (DATA.chucnang2[0] == 1)
                    {
                        ribbonPageGroup_DanhSachCSHT.Enabled = true;
                    }
                    else
                    {
                        ribbonPageGroup_DanhSachCSHT.Enabled = false;
                    }
                    if (DATA.chucnang2[1] == 1)
                    {
                        barButtonItem_ThemPhong.Enabled = true;
                        barButtonItem_ThemCoSo.Enabled = true;
                        barButtonItem_ThemTang.Enabled = true;
                    }
                    else
                    {
                        barButtonItem_ThemPhong.Enabled = false;
                        barButtonItem_ThemCoSo.Enabled = false;
                        barButtonItem_ThemTang.Enabled = false;
                    }
                    if (DATA.chucnang2[2] == 1)
                    {
                        barButtonItem_SuaTTCoSo.Enabled = true;
                        barButtonItem_SuaTTPhong.Enabled = true;
                        barButtonItem_SuaTTTang.Enabled = true;
                    }
                    else
                    {
                        barButtonItem_SuaTTCoSo.Enabled = false;
                        barButtonItem_SuaTTPhong.Enabled = false;
                        barButtonItem_SuaTTTang.Enabled = false;
                    }
                    break;
                case 2:
                    if (DATA.chucnang3[0] == 1)
                    {
                        ribbonPageGroup_DSDienNuoc.Enabled = true;
                    }
                    else
                    {
                        ribbonPageGroup_DSDienNuoc.Enabled = false;
                    }
                    if (DATA.chucnang3[1] == 1)
                    {
                        barButtonItem_themDN.Enabled = true;
                    }
                    else
                    {

                        barButtonItem_themDN.Enabled = false;
                    }
                    if (DATA.chucnang3[2] == 1)
                    {
                        barButtonItem_capnhatDN.Enabled = true;
                        ribbonPageGroup_ThanhToan.Enabled = true;
                    }
                    else
                    {
                        ribbonPageGroup_ThanhToan.Enabled = false;
                        barButtonItem_capnhatDN.Enabled = false;
                    }
                    break;
                //case 3:
                //    if (DATA.chucnang4[0] == 1)
                //    {
                //        ribbonPageGroup_DSTB.Enabled = true;
                //    }
                //    else
                //    {
                //        ribbonPageGroup_DSTB.Enabled = false;
                //    }
                //    break;
            }
        }
    }
}

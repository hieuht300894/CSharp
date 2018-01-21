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
    public partial class UserControl_ChucVu : UserControl
    {
        public UserControl_ChucVu()
        {
            InitializeComponent();
            Chia();
            LoadDSChucVu();
        }
        private void Chia()
        {
            if (DATA.chucnang7[1] == 1)
            {
                button_themchucvu.Enabled = true;

            }
            else
            {
                button_themchucvu.Enabled = false;
            }
            if (DATA.chucnang7[2] == 1)
                button_xacnhan.Enabled = true;
            else
                button_xacnhan.Enabled = false;
        }

        private void LoadDSChucVu()
        {
            DataTable dtb = BUS_CHUCVU.DSChucVu(0);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns.Remove("MatKhau");
        }
        private void button_themchucvu_Click(object sender, EventArgs e)
        {
            if (textBox_chucvu.Text != "" && textBox_pass.Text == textBox_repass.Text)
                BUS_CHUCVU.TaoChucVu(
                    XuLyDuLieu.ChuyenDauThanhKhongDau(textBox_chucvu.Text),
                    XuLyDuLieu.ChuyenDauThanhKhongDau(textBox_pass.Text),
                    XuLyDuLieu.sEncrypt(XuLyDuLieu.ChuyenDauThanhKhongDau(textBox_pass.Text)),
                    textBox_chucvu.Text);
            LoadDSChucVu();
        }

        int r = -1;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            r = dataGridView1.CurrentCell.RowIndex;
            KiemTraTinhTrang();
            PhanCongViec();

        }
        private void KiemTraTinhTrang()
        {
            if (KiemTraChon() == true)
            {
                if (int.Parse(dataGridView1.Rows[r].Cells["TinhTrang"].Value.ToString()) == 1)
                    radioButton_bat.Checked = true;
                else
                    radioButton_khoa.Checked = true;
            }
        }

        private void KiemTraCheck(string DanhMuc, bool[] A)
        {
            switch (DanhMuc)
            {
                case "SV":
                    checkBox_sv_ko.Checked = A[0];
                    checkBox_sv_them.Checked = A[1];
                    checkBox_sv_sua.Checked = A[2];
                    checkBox_sv_xoa.Checked = A[3];
                    break;
                case "CSHT":
                    checkBox_csht_ko.Checked = A[0];
                    checkBox_csht_them.Checked = A[1];
                    checkBox_csht_sua.Checked = A[2];
                    checkBox_csht_xoa.Checked = A[3];
                    break;
                case "DN":
                    checkBox_dn_ko.Checked = A[0];
                    checkBox_dn_them.Checked = A[1];
                    checkBox_dn_sua.Checked = A[2];
                    checkBox_dn_xoa.Checked = A[3];
                    break;
                case "TB":
                    checkBox_tb_ko.Checked = A[0];
                    checkBox_tb_them.Checked = A[1];
                    checkBox_tb_sua.Checked = A[2];
                    checkBox_tb_xoa.Checked = A[3];
                    break;
                case "KTKL":
                    checkBox_klkt_ko.Checked = A[0];
                    checkBox_klkt_them.Checked = A[1];
                    checkBox_klkt_sua.Checked = A[2];
                    checkBox_klkt_xoa.Checked = A[3];
                    break;
                case "HD":
                    checkBox_hd_ko.Checked = A[0];
                    checkBox_hd_them.Checked = A[1];
                    checkBox_hd_sua.Checked = A[2];
                    checkBox_hd_xoa.Checked = A[3];
                    break;
                case "TK":
                    checkBox_tk_ko.Checked = A[0];
                    checkBox_tk_them.Checked = A[1];
                    checkBox_tk_sua.Checked = A[2];
                    checkBox_tk_xoa.Checked = A[3];
                    break;
            }
        }
        private void PhanCongViec()
        {
            if (KiemTraChon() == true)
            {
                bool[] chucnang = new bool[4];
                DataTable dtb_phancong = BUS_CHUCVU.DSPhanCong(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()));
                if (dtb_phancong.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb_phancong.Rows.Count; i++)
                    {
                        chucnang[0] = IntToBool(dtb_phancong.Rows[i]["ChiXem"].ToString());
                        chucnang[1] = IntToBool(dtb_phancong.Rows[i]["Them"].ToString());
                        chucnang[2] = IntToBool(dtb_phancong.Rows[i]["Sua"].ToString());
                        chucnang[3] = IntToBool(dtb_phancong.Rows[i]["Xoa"].ToString());
                        KiemTraCheck(dtb_phancong.Rows[i]["TenDanhMuc"].ToString(), chucnang);
                    }
                }
            }
        }
        private bool KiemTraChon()
        {
            bool kq = true;
            if (r == dataGridView1.Rows.Count - 1)
                kq = false;
            return kq;
        }
        private bool IntToBool(string str)
        {
            bool kq = true;
            int x = int.Parse(str);
            if (x == 0)
                kq = false;
            else
                kq = true;
            return kq;
        }
        private int[] BoolSangInt(bool[] bA)
        {
            int[] iA = new int[4];
            for (int i = 0; i < bA.Length; i++)
            {
                if (bA[i] == true)
                    iA[i] = 1;
                else
                    iA[i] = 0;
            }
            return iA;
        }
        private void LuuCongViec(string TenDanhMuc)
        {
            int TinhTrang = 0;
            if (radioButton_bat.Checked == true)
            {
                if (int.Parse(dataGridView1.Rows[r].Cells["TinhTrang"].Value.ToString()) == 0)
                {
                    BUS_CHUCVU.CapNguoiDung(dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString());
                    BUS_CHUCVU.CapTKNguoiDung(dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString());
                }
                TinhTrang = 1;
                int[] thaotac = new int[4];
                if (KiemTraChon() == true)
                {
                    if (Admin() == true)
                    {
                        bool[] Ad = new bool[] { checkBox_tk_ko.Checked, checkBox_tk_them.Checked, checkBox_tk_sua.Checked, checkBox_tk_xoa.Checked };
                        thaotac = BoolSangInt(Ad);
                        BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), "TK", thaotac, TinhTrang);
                    }
                    else
                    {
                        switch (TenDanhMuc)
                        {
                            case "SV":
                                bool[] SV = new bool[] { checkBox_sv_ko.Checked, checkBox_sv_them.Checked, checkBox_sv_sua.Checked, checkBox_sv_xoa.Checked };
                                thaotac = BoolSangInt(SV);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac);
                                break;
                            case "CSHT":
                                bool[] CSHT = new bool[] { checkBox_csht_ko.Checked, checkBox_csht_them.Checked, checkBox_csht_sua.Checked, checkBox_csht_xoa.Checked };
                                thaotac = BoolSangInt(CSHT);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac); break;
                            case "DN":
                                bool[] DN = new bool[] { checkBox_dn_ko.Checked, checkBox_dn_them.Checked, checkBox_dn_sua.Checked, checkBox_dn_xoa.Checked };
                                thaotac = BoolSangInt(DN);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac);
                                break;
                            case "TB":
                                bool[] TB = new bool[] { checkBox_tb_ko.Checked, checkBox_tb_them.Checked, checkBox_tb_sua.Checked, checkBox_tb_xoa.Checked };
                                thaotac = BoolSangInt(TB);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac);
                                break;
                            case "KTKL":
                                bool[] KTKL = new bool[] { checkBox_klkt_ko.Checked, checkBox_klkt_them.Checked, checkBox_klkt_sua.Checked, checkBox_klkt_xoa.Checked };
                                thaotac = BoolSangInt(KTKL);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac);
                                break;
                            case "HD":
                                bool[] HD = new bool[] { checkBox_hd_ko.Checked, checkBox_hd_them.Checked, checkBox_hd_sua.Checked, checkBox_hd_xoa.Checked };
                                thaotac = BoolSangInt(HD);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac);
                                break;
                            case "TK":
                                bool[] TK = new bool[] { checkBox_tk_ko.Checked, checkBox_tk_them.Checked, checkBox_tk_sua.Checked, checkBox_tk_xoa.Checked };
                                thaotac = BoolSangInt(TK);
                                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), TenDanhMuc, thaotac, TinhTrang);
                                CapLenh(TenDanhMuc, thaotac);
                                break;
                        }
                    }
                }
            }
            else
            {
                if (int.Parse(dataGridView1.Rows[r].Cells["TinhTrang"].Value.ToString()) == 1)
                {
                    BUS_CHUCVU.HuyNguoiDung(dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString());
                    BUS_CHUCVU.HuyTKNguoiDung(dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString());
                }
                TinhTrang = 0;
                int[] thaotac = new int[4];
                BUS_CHUCVU.PhanCongViec(int.Parse(dataGridView1.Rows[r].Cells["MaChucVu"].Value.ToString()), "TK", thaotac, TinhTrang);
            }
        }
        private void CapLenh(string danhmuc, int[] chucnang)
        {
            switch (danhmuc)
            {
                case "SV":
                    BUS_CHUCVU.CapQuyen("sp_chitietTN", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_DSSVMP", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spCapPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_loadchitietSV", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_loadSV", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("LoadDSMaPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);

                    BUS_CHUCVU.CapQuyen("sp_themTN", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyen("sp_themsinhvien", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);

                    BUS_CHUCVU.CapQuyen("sp_ChuyenPhongSV", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("SuaTTSinhVien", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);

                    BUS_CHUCVU.CapQuyen("sp_xoasinhvien", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[3]);
                    break;
                case "CSHT":
                    BUS_CHUCVU.CapQuyen("spDSCoSo", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSTang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSCoSo", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSTang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);

                    BUS_CHUCVU.CapQuyen("spThemCoSo", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyen("spThemPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyen("spThemTang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyen("spThemCoSo", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyen("spThemPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyen("spThemTang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);

                    BUS_CHUCVU.CapQuyen("spSuaCoSo", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spSuaPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spSuaTang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spSuaCoSo", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spSuaPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spSuaTang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    break;
                case "DN":
                    BUS_CHUCVU.CapQuyen("spDSDienNuoc", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSPhongChuaCapNhatDNTrongThang", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSPhongChuaDongTien", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spDSPhongChuaThemChiSoLanDau", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spXemTongTienTheoNgay", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spTongHopSoTienTheoNgay", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("spChiTietDNCuaPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);

                    BUS_CHUCVU.CapQuyen("spThemChiSoLanDau", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);

                    BUS_CHUCVU.CapQuyen("spCapNhatDienNuoc", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spDongTienDienNuoc", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyen("spSuaGiaDienNuoc", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    break;
                case "TB":

                    BUS_CHUCVU.CapQuyen("sp_danhsachTB", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_themTB", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_ChiTietTB", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_danhsachthietbi", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_MaPhongDaDcCap", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_LayMP", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyen("sp_DSTBPhong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);

                    BUS_CHUCVU.CapQuyen("sp_ThemThietBi", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);

                    BUS_CHUCVU.CapQuyen("sp_SuaTB", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    break;
                case "KTKL":
                    BUS_CHUCVU.CapQuyen("spDSHinhThuc", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);

                    BUS_CHUCVU.CapQuyen("spHinhThuc", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    break;
                case "HD":
                    break;
                case "TK":
                    BUS_CHUCVU.CapQuyenChucVu("spChiTietTK", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyenChucVu("spChucVuCuaTaiKhoan", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyenChucVu("spDSChucVu", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyenChucVu("spDSPhanCong", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);
                    BUS_CHUCVU.CapQuyenChucVu("spPhanCongViec", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[0]);

                    BUS_CHUCVU.CapQuyenChucVu("spTaoChucVu", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);
                    BUS_CHUCVU.CapQuyenChucVu("spThemTaiKhoan", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[1]);

                    BUS_CHUCVU.CapQuyenChucVu("spChuyenChucVu", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyenChucVu("spDoiMatKhau", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);
                    BUS_CHUCVU.CapQuyenChucVu("spSuaTK", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[2]);

                    BUS_CHUCVU.CapQuyenChucVu("spXoaTK", dataGridView1.Rows[r].Cells["TenChucVu"].Value.ToString(), chucnang[3]);
                    break;
            }
        }

        private bool Admin()
        {
            bool[] A = new bool[] { checkBox_tk_ko.Checked, checkBox_tk_them.Checked, checkBox_tk_sua.Checked, checkBox_tk_xoa.Checked };
            bool kq = true;
            int i = 0;
            for (i = 0; i < A.Length; i++)
                if (A[i] == false)
                {
                    kq = false;
                    break;
                }
            return kq;
        }
        private bool CheckOffice(bool[] A)
        {

            bool kq = false;
            int i = 0;
            for (i = 0; i < A.Length; i++)
                if (A[i] == true)
                {
                    kq = true;
                    break;
                }
            return kq;
        }

        private void button_xacnhan_Click(object sender, EventArgs e)
        {
            if (Admin() == true)
                LuuCongViec("TK");
            else
            {
                if (radioButton_bat.Checked == true)
                {
                    LuuCongViec("SV");
                    LuuCongViec("CSHT");
                    LuuCongViec("DN");
                    LuuCongViec("TB");
                    LuuCongViec("KTKL");
                    LuuCongViec("HD");
                    LuuCongViec("TK");
                }
                else
                    LuuCongViec("TK");
            }
            LoadDSChucVu();
            KiemTraTinhTrang();
            PhanCongViec();
        }
    }
}
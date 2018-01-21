using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using System.Data;
namespace BUS
{
    public class BUS_CHUCVU
    {
        public static void TaoChucVu(string loginame, string passwd, string MatKhauMaHoa, string ChiTietChucVu)
        {
            DAO_CHUCVU.TaoChucVu(loginame, passwd, MatKhauMaHoa, ChiTietChucVu);
        }
        public static void PhanCongViec(int MaChucVu, string TenDanhMuc, int[] thaotac, int TinhTrang)
        {
            DAO_CHUCVU.PhanCongViec(MaChucVu, TenDanhMuc, thaotac, TinhTrang);
        }
        public static DataTable DSPhanCong(int MaChucVu)
        {
            return DAO_CHUCVU.DSPhanCong(MaChucVu);
        }
        public static DataTable DSChucVu(int TinhTrang)
        {
            return DAO_CHUCVU.DSChucVu(TinhTrang);
        }
        public static DataTable ChucVuCuaTaiKhoan(string TenTaiKhoan, string MatKhau)
        {
            return DAO_CHUCVU.ChucVuCuaTaiKhoan(TenTaiKhoan, MatKhau);
        }
        public static DataTable ChiTietTK(string TenTaiKhoan)
        {
            return DAO_CHUCVU.ChiTietTK(TenTaiKhoan);
        }
        public static void CapQuyen(string LenhThucThi, string TenChucVu, int Chon)
        {
            DAO_CHUCVU.CapQuyen(LenhThucThi, TenChucVu, Chon);
        }
        public static void CapQuyenChucVu(string LenhThucThi, string TenChucVu, int Chon)
        {
            DAO_CHUCVU.CapQuyenChucVu(LenhThucThi, TenChucVu, Chon);
        }
        public static void CapNguoiDung(string loginame, string name_in_db)
        {
            DAO_CHUCVU.CapNguoiDung(loginame, name_in_db);
        }
        public static void HuyNguoiDung(string name_in_db)
        {
            DAO_CHUCVU.HuyNguoiDung(name_in_db); 
        }
        public static void ThemTaiKhoan(string loginame, string passwd, int MaChucVu)
        {
            DAO_CHUCVU.ThemTaiKhoan(loginame, passwd, MaChucVu);
        }
        public static void SuaTK(string TenTaiKhoan, int TinhTrang)
        {
            DAO_CHUCVU.SuaTK(TenTaiKhoan, TinhTrang);
        }
        public static void XoaTK(string TenTaiKhoan)
        {
            DAO_CHUCVU.XoaTK(TenTaiKhoan);
        }
        public static void ChuyenChucVu(string TenTaiKhoan, int MaChucVu)
        {
            DAO_CHUCVU.ChuyenChucVu(TenTaiKhoan, MaChucVu);
        }
        public static void CapTKNguoiDung(string loginame, string name_in_db)
        {
            DAO_CHUCVU.CapTKNguoiDung(loginame, name_in_db);
        }
        public static void HuyTKNguoiDung(string name_in_db)
        {
            DAO_CHUCVU.HuyTKNguoiDung(name_in_db);
        }
        public static void DoiMatKhau(string TenTaiKhoan, string MatKhau)
        {
            DAO_CHUCVU.DoiMatKhau(TenTaiKhoan, MatKhau);
        }
    }
}

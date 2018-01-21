using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAO;

namespace BUS
{
    public class BUS_SV
    {

        //laod danh sach sinh vien
        public static DataTable LoadDSSinhVien(int tinhtrang)
        {
            return DAO_SV.LoadDSSinhVien(tinhtrang);
        }

        public static DataTable LoasDSMaPhongTrong(string GioiTinh)
        {
            return DAO_SV.LoasDSMaPhongTrong(GioiTinh);
        }
        //them sinh vien
        public static void ThemSinhVien(int MSSV, string HoTen, string GioiTinh, string CMND, string NgaySinh, string NienKhoa, string HoKhau, string SDT, string DienUuTien, int TinhTrang, string GhiChu)
        {
            DAO_SV.ThemSV(MSSV, HoTen, GioiTinh, CMND, NgaySinh, NienKhoa, HoKhau, SDT, DienUuTien, TinhTrang, GhiChu);
        }
        //chi tiet sinh vien 

        public static DataTable ChiTietSV(int mssv)
        {
            return DAO_SV.ChiTietSV(mssv);
        }
        public static DataTable DSSVChuaCapPhong(int MSSV)
        {
            return DAO_SV.DSSVChuaCapPhong(MSSV);
        }

        // su doi thong tin sinh vien 
        public static void SuathongtinSV(int MSSV, string HoTen, string GioiTinh, string CMND, string NgaySinh, string NienKhoa, string HoKhau, string SDT, string DienUuTien, int TinhTrang, string GhiChu)
        {
            DAO_SV.SuaDoiThongTinSV(MSSV, HoTen, GioiTinh, CMND, NgaySinh, NienKhoa, HoKhau, SDT, TinhTrang, DienUuTien, GhiChu);
        }
        public static void ChuyenPhongSV(int MSSV, string MaPhong)
        {
            DAO_SV.ChuyenPhongSV(MSSV, MaPhong);
        }
        public static void CapPhongSV(int MSSV, string MaPhong)
        {
            DAO_SV.CapPhongSV(MSSV, MaPhong);
        }
        public static void XoaSV(int MSSV)
        {
            DAO_SV.XoaSV(MSSV);
        }
    }

}

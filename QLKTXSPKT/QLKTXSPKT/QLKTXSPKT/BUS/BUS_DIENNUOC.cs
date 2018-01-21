using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DAO;

namespace BUS
{
    public class BUS_DIENNUOC
    {
        public static DataTable DSDinhMuc(string MaDM)
        {
            return DAO_DIENNUOC.DSDinhMuc(MaDM);
        }
        public static DataTable DSPhongChuaCapNhatDNTrongThang(DateTime ngayghi)
        {
            return DAO_DIENNUOC.DSPhongChuaCapNhatDNTrongThang(ngayghi);
        }
        public static void CapNhatDienNuoc(string maphong, DateTime ngayghi, int sodiencuoi, int sonuoccuoi, int tinhtrang)
        {
            DAO_DIENNUOC.CapNhatDienNuoc(maphong, ngayghi, sodiencuoi, sonuoccuoi, tinhtrang);
        }
        public static DataTable DSPhongChuaDongTien(DateTime ngayghi)
        {
            return DAO_DIENNUOC.DSPhongChuaDongTien(ngayghi);
        }
        public static DataTable XemTongTienTheoNgay(DateTime ngaybatdau, DateTime ngayketthuc)
        {
            return DAO_DIENNUOC.XemTongTienTheoNgay(ngaybatdau, ngayketthuc);
        }
        public static DataTable TongHopSoTienTheoNgay(DateTime ngaybatdau, DateTime ngayketthuc)
        {
            return DAO_DIENNUOC.TongHopSoTienTheoNgay(ngaybatdau, ngayketthuc);
        }
        public static DataTable ChiTietDNCuaPhong(string MaPhong, DateTime ngaybatdau, DateTime ngayketthuc)
        {
            return DAO_DIENNUOC.ChiTietDNCuaPhong(MaPhong, ngaybatdau, ngayketthuc);
        }
        public static void DongTienDienNuoc(string MaPhong, DateTime ngayghi)
        {
            DAO_DIENNUOC.DongTienDienNuoc(MaPhong,ngayghi);
        }
        public static DataTable DSPhongChuaThemChiSoLanDau()
        {
            return DAO_DIENNUOC.DSPhongChuaThemChiSoLanDau();
        }
        public static void ThemChiSoLanDau(string maphong, DateTime ngayghi, int sodiendau, int sonuocdau)
        {
            DAO_DIENNUOC.ThemChiSoLanDau(maphong,  ngayghi, sodiendau, sonuocdau);
        }
        public static void SuaGiaDienNuoc(string MaDM, int sotien)
        {
            DAO_DIENNUOC.SuaGiaDienNuoc(MaDM, sotien);
        }
    }
}

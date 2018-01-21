using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
    public class BUS_CSHT
    {
        public static DataTable DSCoSo()
        {
            return DAO_CSHT.DSCoSo();
        }
        public static DataTable DSTang(string MaCoSo)
        {
            return DAO_CSHT.DSTang(MaCoSo);
        }
        public static DataTable DSPhong(string MaTang, int KhoaChon)
        {
            return DAO_CSHT.DSPhong(MaTang, KhoaChon);
        }

        public static void ThemCoSo(string MaCoSo, string DiaDiem)
        {
            DAO_CSHT.ThemCoSo(MaCoSo, DiaDiem);
        }
        public static void ThemTang(string MaTang, string LoaiTang, int TruongTang, string MaCoSo)
        {
            DAO_CSHT.ThemTang(MaTang, LoaiTang, TruongTang, MaCoSo);
        }
        public static void ThemPhong(string MaPhong, int TruongPhong, int SoLuongMax, string MaTang)
        {
            DAO_CSHT.ThemPhong(MaPhong, TruongPhong, SoLuongMax, MaTang);
        }

        public static void SuaCoSo(string MaCoSo, string DiaDiem)
        {
            DAO_CSHT.SuaCoSo(MaCoSo, DiaDiem);
        }
        public static void SuaTang(string MaTang, string LoaiTang, int TruongTang)
        {
            DAO_CSHT.SuaTang(MaTang, LoaiTang, TruongTang);
        }
        public static void SuaPhong(string MaPhong, int TruongPhong, int SoLuongMax)
        {
            DAO_CSHT.SuaPhong(MaPhong, TruongPhong, SoLuongMax);
        }
    }
}

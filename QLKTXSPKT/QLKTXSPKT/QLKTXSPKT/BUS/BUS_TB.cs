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
   public class BUS_TB
    {
       public static DataTable DSThietBi()
       {
           return DAO_TB.LoadDSThietBi();
       }
       public static void SuaThongTinTB(string TenTB, int DonGia)
       {
           DAO_TB.SuaTB(TenTB, DonGia);

       }
       public static void ThemThongTinTB(string TenTB, int DonGia)
       {
           DAO_TB.ThemTB(TenTB, DonGia);

       }
       public static DataTable ChiTietTB(string TenTB)
       {
           return DAO_TB.ChiTietTB(TenTB);
       }
       public static DataTable DSTBPhong(string MaPhong)
       {
           return DAO_TB.DSTBPhong(MaPhong);
       }
        public static DataTable DSPhong()
       {
           return DAO_TB.DSPhong();
       }
        public static DataTable DSTenThietBi()
        {
            return DAO_TB.DSThietBi();
        }
          public static void SuaTBPhong(int MaTB, string MaPhong, int SoLuong, string MoTa, DateTime nam)
        {
            DAO_TB.SuaTBPhong(MaTB, MaPhong, SoLuong, MoTa, nam);
        }
          public static void ThemTBPhong(int MaTB, string MaPhong, int SoLuong, string MoTa, DateTime nam)
          {
              DAO_TB.ThemTBPhong(MaTB, MaPhong, SoLuong, MoTa, nam);
          }
          public static DataTable ChiTietTBPhong(string MaPhong, string tenTB)
          {
              return DAO_TB.ChiTietTBPhong(MaPhong, tenTB);
          }
          public static DataTable LayMTB(string TenTB)
          {
              return DAO_TB.LayMTB(TenTB);
          }
          public static DataTable LayMaPhong()
          {
              return DAO_TB.LayMaPhong();
          }
    }
}

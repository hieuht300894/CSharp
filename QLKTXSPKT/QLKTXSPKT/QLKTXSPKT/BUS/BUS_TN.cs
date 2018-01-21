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
   public class BUS_TN
    {
       public static DataTable ChiTietThanNhanSV(int mssv)
       {
           return DAO_TN.ChiTietThanNhanSV(mssv);
       }
       public static void ThemThanNhan(string HoTenTN, string QuanHe, string NgheNghiep, string SDT, int MSSV)
       {
           DAO_TN.ThemThanNhan(HoTenTN, QuanHe, NgheNghiep, SDT, MSSV);

       }
       public static void SuaThanNhan(string HoTenTN, string QuanHe, string NgheNghiep, string SDT, int MSSV)
       {
           DAO_TN.SuaThanNhan(HoTenTN, QuanHe, NgheNghiep, SDT, MSSV);

       }
    }
}

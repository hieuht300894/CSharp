using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
  public   class DAO_CHECKLOGIN
    {
      public static bool KiemTraKetNoiTaiKhoan()
      {
          bool thanhcong = false;
          SqlConnection cn = HKN.HamKetNoiAccount();
          try
          {
              cn.Open();
              cn.Close();
              thanhcong = true;
          }
          catch (Exception) { thanhcong = false; }
          return thanhcong;
      }
      public static bool KiemTraKetNoi()
      {
          bool thanhcong = false;
          SqlConnection cn = HKN.HamKetNoi();
          try
          {
              cn.Open();
              cn.Close();
              thanhcong = true;
          }
          catch (Exception) { thanhcong = false; }
          return thanhcong;
      }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
   public class BUS_HINHTHUC
    {
       public static DataTable DSHinhThuc(int chon)
       {
           return DAO_HINHTHUC.DSHinhThuc(chon);
       }
       public static void HinhThuc(int Chon, int MaHT, string TenHT, string MoTa, string KieuHT)
       {
           DAO_HINHTHUC.HinhThuc(Chon,MaHT, TenHT, MoTa, KieuHT);
       }
    }
}

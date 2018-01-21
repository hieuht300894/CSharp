using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
   public class DAO_TN
    {
       // load thong tin chi tiet than nhan
       public static DataTable ChiTietThanNhanSV(int mssv)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_chitietTN", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           cmd.Parameters["@MSSV"].Value = mssv;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();
              
           }
           catch (Exception) {  }
           return dtb;
       }
       // them than nhan
       public static void ThemThanNhan(string HoTenTN, string QuanHe, string NgheNghiep, string SDT, int MSSV)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_themTN", cnn);
           cmd.CommandType = CommandType.StoredProcedure;


           cmd.Parameters.Add("@HoTenTN", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@QuanHe", SqlDbType.NVarChar, 10);
           cmd.Parameters.Add("@NgheNghiep", SqlDbType.NVarChar, 39);
           cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 11);
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);


           cmd.Parameters["@HoTenTN"].Value = HoTenTN;
           cmd.Parameters["@QuanHe"].Value = QuanHe;
           cmd.Parameters["@NgheNghiep"].Value = NgheNghiep;
           cmd.Parameters["@SDT"].Value = SDT;
           cmd.Parameters["@MSSV"].Value = MSSV;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();
           }
           catch (Exception) { }
       }
       // sua thong tin than nhan
       public static void SuaThanNhan(string HoTenTN, string QuanHe, string NgheNghiep, string SDT, int MSSV)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_suaTN", cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("@HoTenTN", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@QuanHe", SqlDbType.NVarChar, 10);
           cmd.Parameters.Add("@NgheNghiep", SqlDbType.NVarChar, 39);
           cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 11);
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);

           cmd.Parameters["@HoTenTN"].Value = HoTenTN;
           cmd.Parameters["@QuanHe"].Value = QuanHe;
           cmd.Parameters["@NgheNghiep"].Value = NgheNghiep;
           cmd.Parameters["@SDT"].Value = SDT;
           cmd.Parameters["@MSSV"].Value = MSSV;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();
               
           }
           catch (Exception) { }
       }

    }
}

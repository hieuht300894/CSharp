using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
   public class DAO_TB
    {
       public static DataTable LoadDSThietBi()
       {
           SqlConnection cn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_danhsachTB", cn);
           cmd.CommandType = CommandType.StoredProcedure;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cn.Close();
              
           }
           catch (Exception)
           {
            
           }
           return dtb;
       }
       public static void ThemTB(string TenTB, int DonGia)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_themTB", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@DonGia", SqlDbType.Int);

           cmd.Parameters["@TenTB"].Value = TenTB;
           cmd.Parameters["@DonGia"].Value = DonGia;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }
       }
       public static void SuaTB(string TenTB, int DonGia)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_SuaTB", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@DonGia", SqlDbType.Int);

           cmd.Parameters["@TenTB"].Value = TenTB;
           cmd.Parameters["@DonGia"].Value = DonGia;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }
       }
       public static DataTable ChiTietTB(string TenTB)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_ChiTietTB", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar, 30);
           
           cmd.Parameters["@TenTB"].Value = TenTB;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
       // load danh sach thiet bi phong
       public static DataTable DSTBPhong(string MaPhong)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_DSTBPhong", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MaPhong", SqlDbType.NVarChar, 30);

           cmd.Parameters["@MaPhong"].Value = MaPhong;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
       public static DataTable DSPhong()
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_MaPhongDaDcCap", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
       public static DataTable DSThietBi()
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_danhsachthietbi", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
       public static void ThemTBPhong(int MaTB, string MaPhong, int SoLuong, string MoTa, DateTime nam)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_ThemThietBi", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MaTB", SqlDbType.Int);
           cmd.Parameters.Add("@MaPhong", SqlDbType.Char,6);
           cmd.Parameters.Add("@SoLuong", SqlDbType.Int);
           cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar,30);
           cmd.Parameters.Add("@nam", SqlDbType.Date);

           cmd.Parameters["@MaTB"].Value = MaTB;
           cmd.Parameters["@MaPhong"].Value = MaPhong;
           cmd.Parameters["@SoLuong"].Value = SoLuong;
           cmd.Parameters["@MoTa"].Value = MoTa;
           cmd.Parameters["@nam"].Value = DateTime.Now;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }
       }
       public static void SuaTBPhong(int MaTB, string MaPhong, int SoLuong, string MoTa, DateTime nam)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_SuaThietBi", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MaTB", SqlDbType.Int);
           cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 6);
           cmd.Parameters.Add("@SoLuong", SqlDbType.Int);
           cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@nam", SqlDbType.Date);

           cmd.Parameters["@MaTB"].Value = MaTB;
           cmd.Parameters["@MaPhong"].Value = MaPhong;
           cmd.Parameters["@SoLuong"].Value = SoLuong;
           cmd.Parameters["@MoTa"].Value = MoTa;
           cmd.Parameters["@nam"].Value = DateTime.Now;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }
       }
       public static DataTable ChiTietTBPhong(string MaPhong, string tenTB)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_danhsachthietbi", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MaPhong", SqlDbType.Char,6);
           cmd.Parameters.Add("@tenTB", SqlDbType.NVarChar,30);
           cmd.Parameters["@MaPhong"].Value = MaPhong;
           cmd.Parameters["@tenTB"].Value = tenTB;

           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
       public static DataTable LayMTB(string TenTB)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_LayMTB", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@tenthietbi", SqlDbType.NVarChar, 30);

           cmd.Parameters["@tenthietbi"].Value = TenTB;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
       public static DataTable LayMaPhong()
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_LayMP", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cnn.Close();

           }
           catch (Exception) { }
           return dtb;
       }
    }
}

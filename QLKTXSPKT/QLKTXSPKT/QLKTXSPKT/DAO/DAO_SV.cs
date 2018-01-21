using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
   public class DAO_SV
    {
       
       public static DataTable LoadDSSinhVien(int tinhtrang)
       {
           SqlConnection cn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_loadSV", cn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@tinhtrang", SqlDbType.Int);
           //
           cmd.Parameters["@tinhtrang"].Value = tinhtrang;
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
       // load chi tiet sinh vien
       public static DataTable ChiTietSV(int mssv)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_loadchitietSV", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           //
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
       //them sinh vien
       public static void ThemSV(int MSSV, string HoTen, string GioiTinh, string CMND, string NgaySinh, string NienKhoa, string HoKhau, string SDT, string DienUuTien, int TinhTrang, string GhiChu)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_themsinhvien", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           
           cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 3);
           cmd.Parameters.Add("@CMND", SqlDbType.Int);
           cmd.Parameters.Add("@NgaySinh", SqlDbType.Date);
           cmd.Parameters.Add("@NienKhoa", SqlDbType.Char, 9);
           cmd.Parameters.Add("@HoKhau", SqlDbType.NVarChar, 80);
           cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 11);
           cmd.Parameters.Add("@DienUuTien", SqlDbType.NVarChar, 20);
           cmd.Parameters.Add("@TinhTrang", SqlDbType.Int);
           cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 50);

           cmd.Parameters["@MSSV"].Value = MSSV;
           
           cmd.Parameters["@HoTen"].Value = HoTen;
           cmd.Parameters["@GioiTinh"].Value = GioiTinh;
           cmd.Parameters["@CMND"].Value = CMND;
           cmd.Parameters["@NgaySinh"].Value = NgaySinh;
           cmd.Parameters["@NienKhoa"].Value = NienKhoa;
           cmd.Parameters["@HoKhau"].Value = HoKhau;
           cmd.Parameters["@SDT"].Value = SDT;
           cmd.Parameters["@DienUuTien"].Value = DienUuTien;
           cmd.Parameters["@TinhTrang"].Value = TinhTrang;
           cmd.Parameters["@GhiChu"].Value = GhiChu;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }

       }
       // sua thong tin sinh vien 
       public static void SuaDoiThongTinSV(int MSSV, string HoTen, string GioiTinh, string CMND, string NgaySinh,string NienKhoa, string HoKhau, string SDT, int TinhTrang, string DienUuTien, string GhiChu)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("SuaTTSinhVien", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 30);
           cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 3);
           cmd.Parameters.Add("@CMND", SqlDbType.Int);
           cmd.Parameters.Add("@NgaySinh", SqlDbType.Date);
           cmd.Parameters.Add("@NienKhoa", SqlDbType.Char, 9);
           cmd.Parameters.Add("@HoKhau", SqlDbType.NVarChar, 80);
           cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 11);
           cmd.Parameters.Add("@TinhTrang", SqlDbType.Int);
           cmd.Parameters.Add("@DienUuTien", SqlDbType.NVarChar, 20);
           cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 50);


           cmd.Parameters["@MSSV"].Value = MSSV;
           cmd.Parameters["@HoTen"].Value = HoTen;
           cmd.Parameters["@GioiTinh"].Value = GioiTinh;
           cmd.Parameters["@CMND"].Value = CMND;
           cmd.Parameters["@NgaySinh"].Value = DateTime.Parse(NgaySinh);
           cmd.Parameters["@NienKhoa"].Value = NienKhoa;
           cmd.Parameters["@HoKhau"].Value = HoKhau;
           cmd.Parameters["@SDT"].Value = SDT;
           cmd.Parameters["@TinhTrang"].Value = TinhTrang;
           cmd.Parameters["@DienUuTien"].Value = DienUuTien;
           cmd.Parameters["@GhiChu"].Value = GhiChu;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();
              
           }
           catch (Exception) {  }
       }
       
       // load danh sach ma phong
       public static DataTable LoasDSMaPhongTrong(string GioiTinh)
       {
           SqlConnection cn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("LoadDSMaPhong", cn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 3);
           cmd.Parameters["@GioiTinh"].Value = GioiTinh;
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dtb = new DataTable();
           try
           {
               cn.Open();
               cmd.ExecuteNonQuery();
               da.Fill(dtb);
               cn.Close();
               //ErrorMessage.Error = 0;
           }
           catch (Exception)
           {
               //ErrorMessage.Error = 1;
           }
           return dtb;
       }
       public static DataTable DSSVChuaCapPhong(int MSSV)
       {
           SqlConnection cn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_DSSVChuaCapPhong", cn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           cmd.Parameters["@MSSV"].Value = MSSV;
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
       public static void CapPhongSV(int MSSV, string MaPhong)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("spCapPhong", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 6);
           cmd.Parameters.Add("@NgayCap", SqlDbType.Date);

           cmd.Parameters["@MSSV"].Value = MSSV;
           cmd.Parameters["@MaPhong"].Value = MaPhong;
           cmd.Parameters["@NgayCap"].Value = DateTime.Now;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }
       }
       public static void ChuyenPhongSV(int MSSV, string MaPhong)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_ChuyenPhongSV", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
           cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 6);
           cmd.Parameters.Add("@NgayCap", SqlDbType.Date);

           cmd.Parameters["@MSSV"].Value = MSSV;
           cmd.Parameters["@MaPhong"].Value = MaPhong;
           cmd.Parameters["@NgayCap"].Value = DateTime.Now;
           try
           {
               cnn.Open();
               cmd.ExecuteNonQuery();
               cnn.Close();

           }
           catch (Exception) { }
       }
       public static void XoaSV(int MSSV)
       {
           SqlConnection cnn = HKN.HamKetNoi();
           SqlCommand cmd = new SqlCommand("sp_xoasinhvien", cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@MSSV", SqlDbType.Int);
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

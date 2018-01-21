using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_CSHT
    {
        public static DataTable DSCoSo()
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSCoSo", cn);
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
            catch (Exception) { }
            return dtb;
        }
        public static DataTable DSTang(string MaCoSo)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSTang", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaCoSo", SqlDbType.Char, 2);
            cmd.Parameters["@MaCoSo"].Value = MaCoSo;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                da.Fill(dtb);
                cn.Close();
            }
            catch (Exception) { }
            return dtb;
        }
        public static DataTable DSPhong(string MaTang, int KhoaChon)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSPhong", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaTang", SqlDbType.Char, 4);
            cmd.Parameters["@MaTang"].Value = MaTang;
            cmd.Parameters.Add("@KhoaChon", SqlDbType.Int);
            cmd.Parameters["@KhoaChon"].Value = KhoaChon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                da.Fill(dtb);
                cn.Close();
            }
            catch (Exception) { }
            return dtb;
        }
        //Thêm Thông tin
        public static void ThemCoSo(string MaCoSo, string DiaDiem)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spThemCoSo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaCoSo", SqlDbType.Char, 2);
            cmd.Parameters["@MaCoSo"].Value = MaCoSo;
            cmd.Parameters.Add("@DiaDiem", SqlDbType.NVarChar, 80);
            cmd.Parameters["@DiaDiem"].Value = DiaDiem;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void ThemTang(string MaTang, string LoaiTang,int TruongTang,string MaCoSo)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spThemTang", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaTang", SqlDbType.Char, 2);
            cmd.Parameters.Add("@LoaiTang", SqlDbType.NVarChar, 3);
            cmd.Parameters.Add("@TruongTang", SqlDbType.Int);
            cmd.Parameters.Add("@MaCoSo", SqlDbType.Char, 2);
            cmd.Parameters["@MaTang"].Value = MaTang;
            cmd.Parameters["@LoaiTang"].Value = LoaiTang;
            cmd.Parameters["@TruongTang"].Value = TruongTang;
            cmd.Parameters["@MaCoSo"].Value = MaCoSo;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                da.Fill(dtb);
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void ThemPhong(string MaPhong, int TruongPhong, int SoLuongMax, string MaTang)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spThemPhong", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 2);
            cmd.Parameters.Add("@TruongPhong", SqlDbType.Int);
            cmd.Parameters.Add("@SoLuongMax", SqlDbType.Int);
            cmd.Parameters.Add("@MaTang", SqlDbType.Char, 4);
            cmd.Parameters["@MaPhong"].Value = MaPhong;
            cmd.Parameters["@TruongPhong"].Value = TruongPhong;
            cmd.Parameters["@SoLuongMax"].Value = SoLuongMax;
            cmd.Parameters["@MaTang"].Value = MaTang;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        //Sửa thông tin
        public static void SuaCoSo(string MaCoSo, string DiaDiem)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spSuaCoSo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaCoSo", SqlDbType.Char, 2);
            cmd.Parameters["@MaCoSo"].Value = MaCoSo;
            cmd.Parameters.Add("@DiaDiem", SqlDbType.NVarChar, 80);
            cmd.Parameters["@DiaDiem"].Value = DiaDiem;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void SuaTang(string MaTang, string LoaiTang, int TruongTang)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spSuaTang", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaTang", SqlDbType.Char, 4);
            cmd.Parameters.Add("@LoaiTang", SqlDbType.NVarChar, 3);
            cmd.Parameters.Add("@TruongTang", SqlDbType.Int);
            cmd.Parameters["@MaTang"].Value = MaTang;
            cmd.Parameters["@LoaiTang"].Value = LoaiTang;
            cmd.Parameters["@TruongTang"].Value = TruongTang;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                da.Fill(dtb);
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void SuaPhong(string MaPhong, int TruongPhong, int SoLuongMax)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spSuaPhong", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 6);
            cmd.Parameters.Add("@TruongPhong", SqlDbType.Int);
            cmd.Parameters.Add("@SoLuongMax", SqlDbType.Int);
            cmd.Parameters["@MaPhong"].Value = MaPhong;
            cmd.Parameters["@TruongPhong"].Value = TruongPhong;
            cmd.Parameters["@SoLuongMax"].Value = SoLuongMax;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
    }
}

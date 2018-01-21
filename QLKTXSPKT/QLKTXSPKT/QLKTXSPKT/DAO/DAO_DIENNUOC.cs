using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_DIENNUOC
    {
        //Lấy DS định mức điện nước
        public static DataTable DSDinhMuc(string MaDM)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSDienNuoc", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDM", SqlDbType.Char,1);
            cmd.Parameters["@MaDM"].Value = MaDM;
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
        //Lấy DS các phòng chưa được cập nhật chỉ số điện nước
        public static DataTable DSPhongChuaCapNhatDNTrongThang(DateTime ngayghi)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSPhongChuaCapNhatDNTrongThang", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ngayghi", SqlDbType.Date);
            cmd.Parameters["@ngayghi"].Value = ngayghi;

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
        public static void CapNhatDienNuoc(string maphong, DateTime ngayghi, int sodiencuoi, int sonuoccuoi, int tinhtrang)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spCapNhatDienNuoc", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maphong", SqlDbType.Char, 6);
            cmd.Parameters.Add("@ngayghi", SqlDbType.Date);
            cmd.Parameters.Add("@sodiencuoi", SqlDbType.Int);
            cmd.Parameters.Add("@sonuoccuoi", SqlDbType.Int);
            cmd.Parameters.Add("@tinhtrang", SqlDbType.Int);

            cmd.Parameters["@maphong"].Value = maphong;
            cmd.Parameters["@ngayghi"].Value = ngayghi;
            cmd.Parameters["@sodiencuoi"].Value = sodiencuoi;
            cmd.Parameters["@sonuoccuoi"].Value = sonuoccuoi;
            cmd.Parameters["@tinhtrang"].Value = tinhtrang;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static DataTable DSPhongChuaDongTien(DateTime ngayghi)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSPhongChuaDongTien", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ngayghi", SqlDbType.Date);
            cmd.Parameters["@ngayghi"].Value = ngayghi;
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
        public static void DongTienDienNuoc(string MaPhong, DateTime ngayghi)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDongTienDienNuoc", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maphong", SqlDbType.Char, 6);
            cmd.Parameters.Add("@ngayghi", SqlDbType.Date);
            cmd.Parameters["@maphong"].Value = MaPhong;
            cmd.Parameters["@ngayghi"].Value = ngayghi;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static DataTable DSPhongChuaThemChiSoLanDau()
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSPhongChuaThemChiSoLanDau", cn);
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
        public static DataTable XemTongTienTheoNgay(DateTime ngaybatdau, DateTime ngayketthuc)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spXemTongTienTheoNgay", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ngaybatdau", SqlDbType.Date);
            cmd.Parameters["@ngaybatdau"].Value = ngaybatdau;
            cmd.Parameters.Add("@ngayketthuc", SqlDbType.Date);
            cmd.Parameters["@ngayketthuc"].Value = ngayketthuc;

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
        public static DataTable TongHopSoTienTheoNgay(DateTime ngaybatdau, DateTime ngayketthuc)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spTongHopSoTienTheoNgay", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ngaybatdau", SqlDbType.Date);
            cmd.Parameters["@ngaybatdau"].Value = ngaybatdau;
            cmd.Parameters.Add("@ngayketthuc", SqlDbType.Date);
            cmd.Parameters["@ngayketthuc"].Value = ngayketthuc;

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
        public static DataTable ChiTietDNCuaPhong(string MaPhong, DateTime ngaybatdau, DateTime ngayketthuc)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spChiTietDNCuaPhong", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 6);
            cmd.Parameters["@MaPhong"].Value = MaPhong;
            cmd.Parameters.Add("@ngaybatdau", SqlDbType.Date);
            cmd.Parameters["@ngaybatdau"].Value = ngaybatdau;
            cmd.Parameters.Add("@ngayketthuc", SqlDbType.Date);
            cmd.Parameters["@ngayketthuc"].Value = ngayketthuc;

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
        public static void ThemChiSoLanDau(string maphong, DateTime ngayghi, int sodiendau, int sonuocdau)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spThemChiSoLanDau", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 6);
            cmd.Parameters.Add("@ngayghi", SqlDbType.Date);
            cmd.Parameters.Add("@sodiendau", SqlDbType.Int);
            cmd.Parameters.Add("@sonuocdau", SqlDbType.Int);
            cmd.Parameters["@MaPhong"].Value = maphong;
            cmd.Parameters["@ngayghi"].Value = ngayghi;
            cmd.Parameters["@sodiendau"].Value = sodiendau;
            cmd.Parameters["@sonuocdau"].Value = sonuocdau;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void SuaGiaDienNuoc(string MaDM, int sotien)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spSuaGiaDienNuoc", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDM", SqlDbType.Char, 2);
            cmd.Parameters.Add("@sotien", SqlDbType.Int);
            cmd.Parameters["@MaDM"].Value = MaDM;
            cmd.Parameters["@sotien"].Value = sotien;
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

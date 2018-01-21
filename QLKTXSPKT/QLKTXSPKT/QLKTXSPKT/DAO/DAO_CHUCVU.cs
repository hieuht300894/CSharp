using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_CHUCVU
    {

        public static DataTable DSChucVu(int TinhTrang)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spDSChucVu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int);
            cmd.Parameters["@TinhTrang"].Value = TinhTrang;
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
        public static DataTable DSPhanCong(int MaChucVu)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spDSPhanCong", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaChucVu", SqlDbType.Int);
            cmd.Parameters["@MaChucVu"].Value = MaChucVu;
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
        public static DataTable ChucVuCuaTaiKhoan(string TenTaiKhoan, string MatKhau)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spChucVuCuaTaiKhoan", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 32);
            cmd.Parameters["@MatKhau"].Value = MatKhau;
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
        public static DataTable ChiTietTK(string TenTaiKhoan)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spChiTietTK", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
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
        public static void TaoChucVu(string loginame, string passwd, string MatKhauMaHoa, string ChiTietChucVu)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spTaoChucVu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@loginame", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@passwd", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@MatKhauMaHoa", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ChiTietChucVu", SqlDbType.NVarChar, 32);
            cmd.Parameters["@loginame"].Value = loginame;
            cmd.Parameters["@passwd"].Value = passwd;
            cmd.Parameters["@MatKhauMaHoa"].Value = MatKhauMaHoa;
            cmd.Parameters["@ChiTietChucVu"].Value = ChiTietChucVu;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void PhanCongViec(int MaChucVu, string TenDanhMuc, int[] thaotac, int TinhTrang)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spPhanCongViec", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaChucVu", SqlDbType.Int);
            cmd.Parameters.Add("@TenDanhMuc", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@ChiXem", SqlDbType.Int);
            cmd.Parameters.Add("@Them", SqlDbType.Int);
            cmd.Parameters.Add("@Sua", SqlDbType.Int);
            cmd.Parameters.Add("@Xoa", SqlDbType.Int);
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int);

            cmd.Parameters["@MaChucVu"].Value = MaChucVu;
            cmd.Parameters["@TenDanhMuc"].Value = TenDanhMuc;
            cmd.Parameters["@ChiXem"].Value = thaotac[0];
            cmd.Parameters["@Them"].Value = thaotac[1];
            cmd.Parameters["@Sua"].Value = thaotac[2];
            cmd.Parameters["@Xoa"].Value = thaotac[3];
            cmd.Parameters["@TinhTrang"].Value = TinhTrang;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void CapNguoiDung(string loginame, string name_in_db)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("sp_adduser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@loginame", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@name_in_db", SqlDbType.NVarChar, 32);
            cmd.Parameters["@loginame"].Value = loginame;
            cmd.Parameters["@name_in_db"].Value = name_in_db;
           
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void HuyNguoiDung(string name_in_db)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("sp_dropuser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@name_in_db", SqlDbType.NVarChar, 32);
            cmd.Parameters["@name_in_db"].Value = name_in_db;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void CapTKNguoiDung(string loginame, string name_in_db)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("sp_adduser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@loginame", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@name_in_db", SqlDbType.NVarChar, 32);
            cmd.Parameters["@loginame"].Value = loginame;
            cmd.Parameters["@name_in_db"].Value = name_in_db;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void HuyTKNguoiDung(string name_in_db)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("sp_dropuser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@name_in_db", SqlDbType.NVarChar, 32);
            cmd.Parameters["@name_in_db"].Value = name_in_db;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void CapQuyen(string LenhThucThi, string TenChucVu, int Chon)
        {
            SqlConnection cn = HKN.HamKetNoi();
            string kq = "";
            if (Chon == 1)
                kq = "grant exec on " + LenhThucThi + " to " + TenChucVu;
            else
                kq = "revoke exec on " + LenhThucThi + " from " + TenChucVu;
            SqlCommand cmd = new SqlCommand(kq, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void CapQuyenChucVu(string LenhThucThi, string TenChucVu, int Chon)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            string kq = "";
            if (Chon == 1)
                kq = "grant exec on " + LenhThucThi + " to " + TenChucVu;
            else
                kq = "revoke exec on " + LenhThucThi + " from " + TenChucVu;
            SqlCommand cmd = new SqlCommand(kq, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void ThemTaiKhoan(string TenTaiKhoan, string MatKhau, int MaChucVu)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spThemTaiKhoan", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@MaChucVu", SqlDbType.Int);

            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
            cmd.Parameters["@MatKhau"].Value = MatKhau;
            cmd.Parameters["@MaChucVu"].Value = MaChucVu;
           
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                ErrorMessage.error = "";
            }
            catch (Exception e) { ErrorMessage.error = e.Message; }
        }
        public static void SuaTK(string TenTaiKhoan, int TinhTrang)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spSuaTK", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int);
            cmd.Parameters["@TinhTrang"].Value = TinhTrang;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void XoaTK(string TenTaiKhoan)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spXoaTK", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void ChuyenChucVu(string TenTaiKhoan, int MaChucVu)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spChuyenChucVu", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
            cmd.Parameters.Add("@MaChucVu", SqlDbType.Int);
            cmd.Parameters["@MaChucVu"].Value = MaChucVu;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception) { }
        }
        public static void DoiMatKhau(string TenTaiKhoan, string MatKhau)
        {
            SqlConnection cn = HKN.HamKetNoiAccount();
            SqlCommand cmd = new SqlCommand("spDoiMatKhau", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 20);
            cmd.Parameters["@TenTaiKhoan"].Value = TenTaiKhoan;
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 32);
            cmd.Parameters["@MatKhau"].Value = MatKhau;
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

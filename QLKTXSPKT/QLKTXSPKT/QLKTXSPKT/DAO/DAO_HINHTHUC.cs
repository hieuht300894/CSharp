using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_HINHTHUC
    {
        public static DataTable DSHinhThuc(int chon)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spDSHinhThuc", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@chon", SqlDbType.Int);
            cmd.Parameters["@chon"].Value = chon;
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
        public static void HinhThuc(int Chon, int MaHT, string TenHT, string MoTa, string KieuHT)
        {
            SqlConnection cn = HKN.HamKetNoi();
            SqlCommand cmd = new SqlCommand("spHinhThuc", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Chon", SqlDbType.Int);
            cmd.Parameters["@Chon"].Value = Chon;
            cmd.Parameters.Add("@MaHT", SqlDbType.Int);
            cmd.Parameters["@MaHT"].Value = MaHT;
            cmd.Parameters.Add("@TenHT", SqlDbType.NVarChar,20);
            cmd.Parameters["@TenHT"].Value = TenHT;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar,100);
            cmd.Parameters["@MoTa"].Value = MoTa;
            cmd.Parameters.Add("@KieuHT", SqlDbType.Char,2);
            cmd.Parameters["@KieuHT"].Value = KieuHT;
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

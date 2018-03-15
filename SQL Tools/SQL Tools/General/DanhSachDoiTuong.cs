using SQL_Tools.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Tools.General
{
    public static class DanhSachDoiTuong
    {
        public static List<Info> LayDanhSachDB()
        {
            SqlConnection conn = HamKetNoi.KetNoi();
            try
            {
                List<Info> lstResult = new List<Info>();
                SqlCommand cmd = new SqlCommand("select dbid as N'ID', name as N'Name' from sysdatabases order by name", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                conn.Open();
                cmd.ExecuteNonQuery();
                da.Fill(table);
                conn.Close();

                foreach (DataRow dr in table.Rows)
                {
                    lstResult.Add(new Info() { ID = Convert.ToInt32(dr["ID"]), Name = dr["Name"].ToString() });
                }

                return lstResult;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                return new List<Info>();
            }
        }

        public static List<Info> LayDanhSachTable()
        {
            SqlConnection conn = HamKetNoi.KetNoi();
            try
            {
                List<Info> lstResult = new List<Info>();
                SqlCommand cmd = new SqlCommand("select object_id as N'ID', name as N'Name' from sys.tables order by name", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                conn.Open();
                cmd.ExecuteNonQuery();
                da.Fill(table);
                conn.Close();

                foreach (DataRow dr in table.Rows)
                {
                    lstResult.Add(new Info() { ID = Convert.ToInt32(dr["ID"]), Name = dr["Name"].ToString() });
                }

                return lstResult;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                return new List<Info>();
            }
        }

        public static string ScriptsInsert(string tableName)
        {
            SqlConnection conn = HamKetNoi.KetNoi();
            string result = string.Empty;

            conn.InfoMessage += new SqlInfoMessageEventHandler((sender, e) => conn_InfoMessage(sender, e, out result));
            try
            {
                StreamReader sr = new StreamReader(@"Script SQL\GenerateInsert.sql");
                string line;
                string val = string.Empty;
                while ((line = sr.ReadLine()) != null) { val += line + "\n"; }

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(val, tableName);

                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                result = string.Empty;
            }
            finally
            {
                conn.InfoMessage -= new SqlInfoMessageEventHandler((sender, e) => conn_InfoMessage(sender, e, out result));
                conn.Close();
            }
            return result;
        }

        private static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e,out string result)
        {
            result = e.Message;
        }
    }
}

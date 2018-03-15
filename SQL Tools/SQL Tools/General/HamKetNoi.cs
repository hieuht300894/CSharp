using SQL_Tools.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Tools.General
{
    public static class HamKetNoi
    {
        public static ChuoiKetNoi ckn = new ChuoiKetNoi() { ServerName = ".", Database = "master", IsAuth = true, ConnectionTimeOut = 10000 };
        public static void LuuChuoiKetNoi(string ServerName, bool IsAuth, string Username, string Password, int ConnectionTimeOut, string Database)
        {
            ckn = new ChuoiKetNoi()
            {
                ServerName = ServerName,
                IsAuth = IsAuth,
                Username = Username,
                Password = Password,
                ConnectionTimeOut = ConnectionTimeOut,
                Database = Database
            };
        }
        public static SqlConnection KetNoi()
        {
            string connStr = "";
            if (ckn.IsAuth)
                connStr = $"Data Source={ckn.ServerName};Initial Catalog={ckn.Database};Integrated Security={ckn.IsAuth};Connection Timeout={ckn.ConnectionTimeOut};";
            else
                connStr = $"Data Source={ckn.ServerName};Initial Catalog={ckn.Database};User ID={ckn.Username};Password={ckn.Password};Connection Timeout={ckn.ConnectionTimeOut};";
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }
    }
}

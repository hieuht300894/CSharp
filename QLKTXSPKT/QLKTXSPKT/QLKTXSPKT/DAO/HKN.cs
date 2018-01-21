using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class HKN
    {
        public static SqlConnection HamKetNoi()
        {
            SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=QLKTXSPKT3;User ID=" + DATA.UserName + ";Password=" + DATA.PassWord);
            //SqlConnection cn = new SqlConnection("Data Source=HIEU;Initial Catalog=QLKTXSPKT3;Integrated Security=True");
            return cn;
        }
        public static SqlConnection HamKetNoiAccount()
        {
            SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=ACCOUNT;User ID=" + DATA.UserName + ";Password=" + DATA.PassWord);
            //SqlConnection cn = new SqlConnection("Data Source=HIEU;Initial Catalog=ACCOUNT;Integrated Security=True");
            return cn;
        }
    }
}

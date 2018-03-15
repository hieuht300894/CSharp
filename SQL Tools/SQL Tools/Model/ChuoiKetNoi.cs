using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Tools.Model
{
    public class ChuoiKetNoi
    {
        public string ServerName { get; set; }
        public bool IsAuth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ConnectionTimeOut { get; set; }
        public string Database { get; set; }
    }
}

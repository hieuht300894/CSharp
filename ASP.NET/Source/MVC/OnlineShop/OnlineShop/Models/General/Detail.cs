using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Detail
    {
        public Int32 KeyID { get; set; } = AutoGenerateID.KeyID;
        public String GhiChu { get; set; } = string.Empty;
        public Int32 TrangThai { get; set; } = 1;
        public Boolean MacDinh { get; set; }
    }
}

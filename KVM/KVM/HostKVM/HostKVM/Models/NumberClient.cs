using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostKVM
{
    public class NumberClient
    {
        public int ColumnNumber { get; set; }
        public int RowNumber { get; set; }
        public int Total { get { return ColumnNumber * RowNumber; } }
    }
}

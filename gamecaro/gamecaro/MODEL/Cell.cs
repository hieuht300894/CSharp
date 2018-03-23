using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class Cell
    {
        public int PositionOfRow { get; set; }
        public int PositionOfColumn { get; set; }
        public clsGeneral.fKey TypeOfChess { get; set; } = clsGeneral.fKey.EMPTY;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class Board
    {
        /// <summary>
        /// X: Position Of Column
        /// Y: Position Of Row
        /// </summary>
        public Point Point { get; set; } = new Point();
        public clsGeneral.fKey TypeOfChess { get; set; } = clsGeneral.fKey.Empty;
    }
}

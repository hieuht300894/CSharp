using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class ChessPoint : ICloneable
    {
        public clsGeneral.fKey TypeOfChess { get; set; } = clsGeneral.fKey.EMPTY;
        public Point Location { get; set; } = new Point();
        public int PositionOfRow { get; set; }
        public int PositionOfColumn { get; set; }
        public Image Image { get; set; } = new Bitmap(clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height);
        public ChessPoint LastCheckPoint { get; set; }
        public long Score { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

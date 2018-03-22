using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class ChessBoard
    {
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        public int SizeOfCell { get { return 24; } }
        public int SizeOfCross { get { return 8; } }
        public int SizeOfCircle { get { return 8; } }
        public Color ColorOfCross { get { return Color.Red; } }
        public Color ColorOfCircle { get { return Color.Blue; } }
        public Size SizeOfBoard { get; set; }
        public List<Board> Boards { get; set; } = new List<Board>();
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class clsGeneral
    {
        public static string HowToPlay
        {
            get
            {
                return
                    "F1: How to play\n" +
                    "F2: Author\n" +
                    "F3: End game\n" +
                    "Play mode:\n" +
                    "   Key 1: Two players\n" +
                    "   Key 2: Player first\n" +
                    "   Key 3: Com first\n" +
                    "   Key 4: Com first\n" +
                    "Ctrl + Z: Undo\n" +
                    "Ctrl + X: Redo\n";
            }
        }
        public enum fKey
        {
            Empty = 0,
            OutLine = 1,
            UnChecked = 2,
            Checked = 3,
            X = 4,
            O = 5,
            Undo = 6,
            Board = 7,
            Line = 8
        }
        public static int[] LimitTime = new int[] { 10, 20, 30, 40, 50, 60 };

        public static ChessBoard ChessBoard { get; set; } = new ChessBoard();
    }
}

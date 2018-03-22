﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class ChessPoint
    {
        public clsGeneral.fKey TypeOfChess { get; set; } = clsGeneral.fKey.Empty;
        public Point Location { get; set; } = new Point();
        public Point SpotCell { get; set; } = new Point();
        public Bitmap Bitmap { get; set; } = new Bitmap(clsGeneral.SizeOfBoard.Width, clsGeneral.SizeOfBoard.Height);
    }
}
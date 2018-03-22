using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class clsChessBoard
    {
        public static bool CheckEmptyOfChess(clsGeneral.fKey typeOfChess, Point pointOfChess)
        {
            Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == pointOfChess.Y && x.Point.Y == pointOfChess.X);
            if (board.TypeOfChess == clsGeneral.fKey.Empty)
            {
                board.TypeOfChess = typeOfChess;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveTypeOfChess(Point pointOfChess)
        {
            Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == pointOfChess.Y && x.Point.Y == pointOfChess.X);
            if (board.TypeOfChess == clsGeneral.fKey.X || board.TypeOfChess == clsGeneral.fKey.O)
                board.TypeOfChess = clsGeneral.fKey.Empty;
        }

        public static void CreateEmptyBoard()
        {
            clsGeneral.ChessBoard.Boards = new List<Board>();

            clsGeneral.ChessBoard.Boards.Add(new Board() { Point = new Point(0, 0), TypeOfChess = clsGeneral.fKey.Empty });
            clsGeneral.ChessBoard.Boards.Add(new Board() { Point = new Point(0, clsGeneral.ChessBoard.NumberOfColumns), TypeOfChess = clsGeneral.fKey.Empty });
            clsGeneral.ChessBoard.Boards.Add(new Board() { Point = new Point(clsGeneral.ChessBoard.NumberOfRows, 0), TypeOfChess = clsGeneral.fKey.Empty });
            clsGeneral.ChessBoard.Boards.Add(new Board() { Point = new Point(clsGeneral.ChessBoard.NumberOfRows, clsGeneral.ChessBoard.NumberOfColumns), TypeOfChess = clsGeneral.fKey.Empty });

            for (int i = 1; i < clsGeneral.ChessBoard.NumberOfRows; i++)
            {
                for (int j = 1; j < clsGeneral.ChessBoard.NumberOfColumns; j++)
                {
                    clsGeneral.ChessBoard.Boards.Add(new Board()
                    {
                        Point = new Point(i, j),
                        TypeOfChess = clsGeneral.fKey.Empty
                    });
                }
            }
        }

        public static void ConvertPointOfCell(Point pCheck, ref Point pResult, ref clsGeneral.fKey Status)
        {
            int div_X = pCheck.X / clsGeneral.ChessBoard.SizeOfCell;
            int div_Y = pCheck.Y / clsGeneral.ChessBoard.SizeOfCell;
            int mod_x = pCheck.X % clsGeneral.ChessBoard.SizeOfCell;
            int mod_y = pCheck.Y % clsGeneral.ChessBoard.SizeOfCell;
            if (mod_x > (clsGeneral.ChessBoard.SizeOfCell / 2))
                div_X += 1;
            if (mod_y > (clsGeneral.ChessBoard.SizeOfCell / 2))
                div_Y += 1;

            if (div_X <= 0 || div_Y <= 0 || div_X >= clsGeneral.ChessBoard.NumberOfColumns || div_Y >= clsGeneral.ChessBoard.NumberOfRows)
            {
                /* Outline */
                Status = clsGeneral.fKey.OutLine;
            }
            else
            {
                /* Unchecked */
                Status = clsGeneral.fKey.UnChecked;
                pResult = new Point(div_X, div_Y);

                //if (banCo[div_Y][div_X] == 0)
                //{
                //    point.X = div_Y;
                //    point.Y = div_X;
                //}
                //else
                //{
                //    point.X = -2;
                //    point.Y = -2;
                //}
            }
        }
    }
}

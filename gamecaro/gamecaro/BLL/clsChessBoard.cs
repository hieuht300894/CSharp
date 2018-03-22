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
        public static bool CheckEmptyOfChess(clsGeneral.fKey typeOfChess, int PositionOfRow, int PositionOfColumn)
        {
            Cell board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == PositionOfColumn);
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

        public static void RemoveTypeOfChess(int PositionOfRow, int PositionOfColumn)
        {
            Cell board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == PositionOfColumn);
            if (board.TypeOfChess == clsGeneral.fKey.X || board.TypeOfChess == clsGeneral.fKey.O)
                board.TypeOfChess = clsGeneral.fKey.Empty;
        }

        public static void CreateEmptyBoard()
        {
            clsGeneral.ChessBoard.Boards = new List<Cell>();

            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = 0, PositionOfColumn = 0, TypeOfChess = clsGeneral.fKey.Empty });
            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = 0, PositionOfColumn = clsGeneral.ChessBoard.NumberOfColumns, TypeOfChess = clsGeneral.fKey.Empty });
            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = clsGeneral.ChessBoard.NumberOfRows, PositionOfColumn = 0, TypeOfChess = clsGeneral.fKey.Empty });
            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = clsGeneral.ChessBoard.NumberOfRows, PositionOfColumn = clsGeneral.ChessBoard.NumberOfColumns, TypeOfChess = clsGeneral.fKey.Empty });

            for (int i = 1; i < clsGeneral.ChessBoard.NumberOfRows; i++)
            {
                for (int j = 1; j < clsGeneral.ChessBoard.NumberOfColumns; j++)
                {
                    clsGeneral.ChessBoard.Boards.Add(new Cell()
                    {
                        PositionOfRow = i,
                        PositionOfColumn = j,
                        TypeOfChess = clsGeneral.fKey.Empty
                    });
                }
            }
        }

        public static void ConvertPointOfCell(Point pCheck, ref int PositionOfRow, ref int PositionOfColumn, ref clsGeneral.fKey Status)
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
                /* Empty */
                Status = clsGeneral.fKey.Empty;
                PositionOfRow = div_Y;
                PositionOfColumn = div_X;

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

        public static bool CheckWin(clsGeneral.fKey attack, clsGeneral.fKey block, int PositionOfRow, int PositionOfColumn, ref List<Cell> lstPoint)
        {
            int sumAttack = 0;
            int sumBlock = 0;
            lstPoint = new List<Cell>();

            /*Kiểm tra hàng ngang*/
            for (int d = 1; d < 6 && (PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns; d++)
            {
                Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn + d));
                if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O)
                    continue;

                if (cell.TypeOfChess == attack)
                {
                    sumAttack++;
                    lstPoint.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                }
                else
                {
                    if (cell.TypeOfChess == block)
                        sumBlock++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (PositionOfColumn - d) >= 1; d++)
            {
                Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn - d));
                if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O)
                    continue;

                if (cell.TypeOfChess == attack)
                {
                    sumAttack++;
                    lstPoint.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                }
                else
                {
                    if (cell.TypeOfChess == block)
                        sumBlock++;
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
                return true;

            ////Kiem tra hang doc
            //lstPoint.Clear();
            //sumAttack = 0;
            //sumBlock = 0;
            //for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1); d++)
            //{
            //    Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == point.Y && x.Point.Y == point.X);
            //    if (board.TypeOfChess != clsGeneral.fKey.X || board.TypeOfChess != clsGeneral.fKey.O)
            //        continue;

            //    if (banCo[point.X + d][point.Y] == attack)
            //    {
            //        sumAttack++;
            //        lstPoint.Add(new Point(point.X + d, point.Y));
            //    }
            //    else
            //    {
            //        if (banCo[point.X + d][point.Y] == block)
            //            sumBlock++;
            //        break;
            //    }
            //}
            //for (int d = 1; d < 6 && (point.X - d) >= 1; d++)
            //{
            //    Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == point.Y && x.Point.Y == point.X);
            //    if (board.TypeOfChess != clsGeneral.fKey.X || board.TypeOfChess != clsGeneral.fKey.O)
            //        continue;

            //    if (banCo[point.X - d][point.Y] == attack)
            //    {
            //        sumAttack++;
            //        lstPoint.Add(new Point(point.X - d, point.Y));
            //    }
            //    else
            //    {
            //        if (banCo[point.X - d][point.Y] == block)
            //            sumBlock++;
            //        break;
            //    }
            //}
            //if (sumAttack >= 4 && sumBlock < 2)
            //    return true;

            ////Kiem tra hang cheo xuong
            //lstPoint.Clear();
            //sumAttack = 0;
            //sumBlock = 0;
            //for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1) && (point.Y + d) < (bc.SoOCo - 1); d++)
            //{
            //    Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == point.Y && x.Point.Y == point.X);
            //    if (board.TypeOfChess != clsGeneral.fKey.X || board.TypeOfChess != clsGeneral.fKey.O)
            //        continue;

            //    if (banCo[point.X + d][point.Y + d] == attack)
            //    {
            //        sumAttack++;
            //        lstPoint.Add(new Point(point.X + d, point.Y + d));
            //    }
            //    else
            //    {
            //        if (banCo[point.X + d][point.Y + d] == block)
            //            sumBlock++;
            //        break;
            //    }
            //}
            //for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y - d) >= 1; d++)
            //{
            //    Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == point.Y && x.Point.Y == point.X);
            //    if (board.TypeOfChess != clsGeneral.fKey.X || board.TypeOfChess != clsGeneral.fKey.O)
            //        continue;

            //    if (banCo[point.X - d][point.Y - d] == attack)
            //    {
            //        sumAttack++;
            //        lstPoint.Add(new Point(point.X - d, point.Y - d));
            //    }
            //    else
            //    {
            //        if (banCo[point.X - d][point.Y - d] == block)
            //            sumBlock++;
            //        break;
            //    }
            //}
            //if (sumAttack >= 4 && sumBlock < 2)
            //    return true;


            ////Kiem tra hang cheo len
            //lstPoint.Clear();
            //sumAttack = 0;
            //sumBlock = 0;
            //for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y + d) < (bc.SoOCo - 1); d++)
            //{
            //    Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == point.Y && x.Point.Y == point.X);
            //    if (board.TypeOfChess != clsGeneral.fKey.X || board.TypeOfChess != clsGeneral.fKey.O)
            //        continue;

            //    if (banCo[point.X - d][point.Y + d] == attack)
            //    {
            //        sumAttack++;
            //        lstPoint.Add(new Point(point.X - d, point.Y + d));
            //    }
            //    else
            //    {
            //        if (banCo[point.X - d][point.Y + d] == block)
            //            sumBlock++;
            //        break;
            //    }
            //}
            //for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1) && (point.Y - d) >= 1; d++)
            //{
            //    Board board = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.Point.X == point.Y && x.Point.Y == point.X);
            //    if (board.TypeOfChess != clsGeneral.fKey.X || board.TypeOfChess != clsGeneral.fKey.O)
            //        continue;

            //    if (banCo[point.X + d][point.Y - d] == attack)
            //    {
            //        sumAttack++;
            //        lstPoint.Add(new Point(point.X + d, point.Y - d));
            //    }
            //    else
            //    {
            //        if (banCo[point.X + d][point.Y - d] == block)
            //            sumBlock++;
            //        break;
            //    }
            //}

            //if (sumAttack >= 4 && sumBlock < 2)
            //    return true;

            return false;
        }
    }
}

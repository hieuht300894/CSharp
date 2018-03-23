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
            ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == PositionOfColumn);
            if (chess.TypeOfChess == clsGeneral.fKey.EMPTY)
            {
                chess.TypeOfChess = typeOfChess;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveTypeOfChess(int PositionOfRow, int PositionOfColumn)
        {
            ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == PositionOfColumn);
            if (chess.TypeOfChess == clsGeneral.fKey.X || chess.TypeOfChess == clsGeneral.fKey.O)
                chess.TypeOfChess = clsGeneral.fKey.EMPTY;
        }

        public static void CreateEmptyBoard()
        {
            clsGeneral.ChessBoard.ListChesses.Clear();
            clsGeneral.ChessBoard.ListChessCheckeds.Clear();

            clsGeneral.ChessBoard.ListChesses.Add(new ChessPoint() { PositionOfRow = 0, PositionOfColumn = 0, TypeOfChess = clsGeneral.fKey.OUTLINE });
            clsGeneral.ChessBoard.ListChesses.Add(new ChessPoint() { PositionOfRow = 0, PositionOfColumn = clsGeneral.ChessBoard.NumberOfColumns, TypeOfChess = clsGeneral.fKey.OUTLINE });
            clsGeneral.ChessBoard.ListChesses.Add(new ChessPoint() { PositionOfRow = clsGeneral.ChessBoard.NumberOfRows, PositionOfColumn = 0, TypeOfChess = clsGeneral.fKey.OUTLINE });
            clsGeneral.ChessBoard.ListChesses.Add(new ChessPoint() { PositionOfRow = clsGeneral.ChessBoard.NumberOfRows, PositionOfColumn = clsGeneral.ChessBoard.NumberOfColumns, TypeOfChess = clsGeneral.fKey.OUTLINE });

            for (int i = 1; i < clsGeneral.ChessBoard.NumberOfRows; i++)
            {
                for (int j = 1; j < clsGeneral.ChessBoard.NumberOfColumns; j++)
                {
                    clsGeneral.ChessBoard.ListChesses.Add(new ChessPoint() { PositionOfRow = i, PositionOfColumn = j, TypeOfChess = clsGeneral.fKey.EMPTY });
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
                Status = clsGeneral.fKey.OUTLINE;
            }
            else
            {
                /* Empty */
                Status = clsGeneral.fKey.EMPTY;
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

        public static bool CheckWin(clsGeneral.fKey attack, clsGeneral.fKey block, int PositionOfRow, int PositionOfColumn)
        {
            int sumAttack = 0;
            int sumBlock = 0;
            clsGeneral.ChessBoard.ListChessWins.Clear();

            /*Horizontal*/
            sumAttack = 0;
            sumBlock = 0;
            clsGeneral.ChessBoard.ListChessWins.Clear();

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = PositionOfRow, PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfColumn - d) > 0)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn - d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = PositionOfRow, PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
            {
                clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortHorizontal();
                return true;
            }

            /*Vertical*/
            sumAttack = 0;
            sumBlock = 0;
            clsGeneral.ChessBoard.ListChessWins.Clear();

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == PositionOfColumn);
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = (PositionOfRow + d), PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow - d) > 0)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow - d) && x.PositionOfColumn == PositionOfColumn);
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = (PositionOfRow - d), PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
            {
                clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortVertical();
                return true;
            }

            /*Cross (top-left) - (bottom-right)*/
            sumAttack = 0;
            sumBlock = 0;
            clsGeneral.ChessBoard.ListChessWins.Clear();

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = (PositionOfRow + d), PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow - d) > 0 && (PositionOfColumn - d) > 0)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow - d) && x.PositionOfColumn == (PositionOfColumn - d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = (PositionOfRow - d), PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
            {
                clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortCross_A();
                return true;
            }

            /*Cross (top-right) - (bottom-left)*/
            sumAttack = 0;
            sumBlock = 0;
            clsGeneral.ChessBoard.ListChessWins.Clear();

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn - d) > 0)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn - d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = (PositionOfRow + d), PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow - d) > 0 && (PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow - d) && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == attack)
                    {
                        sumAttack++;
                        clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = (PositionOfRow - d), PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (chess.TypeOfChess == block)
                            sumBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
            {
                clsGeneral.ChessBoard.ListChessWins.Add(new ChessPoint() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortCross_B();
                return true;
            }

            clsGeneral.ChessBoard.ListChessWins.Clear();
            return false;
        }

        public static void SortHorizontal()
        {
            int n = clsGeneral.ChessBoard.ListChessWins.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn > clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn)
                    {
                        int t1 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow = clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow = t1;

                        int t2 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn = clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn = t2;
                    }
                }
            }
        }

        public static void SortVertical()
        {
            int n = clsGeneral.ChessBoard.ListChessWins.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow > clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow)
                    {
                        int t1 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow = clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow = t1;

                        int t2 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn = clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn = t2;
                    }
                }
            }
        }

        public static void SortCross_A()
        {
            int n = clsGeneral.ChessBoard.ListChessWins.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow > clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow && clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn > clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn)
                    {
                        int t1 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow = clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow = t1;

                        int t2 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn = clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn = t2;
                    }
                }
            }
        }

        public static void SortCross_B()
        {
            int n = clsGeneral.ChessBoard.ListChessWins.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow > clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow && clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn < clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn)
                    {
                        int t1 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfRow = clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfRow = t1;

                        int t2 = clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[i].PositionOfColumn = clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn;
                        clsGeneral.ChessBoard.ListChessWins[j].PositionOfColumn = t2;
                    }
                }
            }
        }
    }
}

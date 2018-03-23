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
            if (board.TypeOfChess == clsGeneral.fKey.EMPTY)
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
                board.TypeOfChess = clsGeneral.fKey.EMPTY;
        }

        public static void CreateEmptyBoard()
        {
            clsGeneral.ChessBoard.Boards = new List<Cell>();

            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = 0, PositionOfColumn = 0, TypeOfChess = clsGeneral.fKey.EMPTY });
            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = 0, PositionOfColumn = clsGeneral.ChessBoard.NumberOfColumns, TypeOfChess = clsGeneral.fKey.EMPTY });
            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = clsGeneral.ChessBoard.NumberOfRows, PositionOfColumn = 0, TypeOfChess = clsGeneral.fKey.EMPTY });
            clsGeneral.ChessBoard.Boards.Add(new Cell() { PositionOfRow = clsGeneral.ChessBoard.NumberOfRows, PositionOfColumn = clsGeneral.ChessBoard.NumberOfColumns, TypeOfChess = clsGeneral.fKey.EMPTY });

            for (int i = 1; i < clsGeneral.ChessBoard.NumberOfRows; i++)
            {
                for (int j = 1; j < clsGeneral.ChessBoard.NumberOfColumns; j++)
                {
                    clsGeneral.ChessBoard.Boards.Add(new Cell()
                    {
                        PositionOfRow = i,
                        PositionOfColumn = j,
                        TypeOfChess = clsGeneral.fKey.EMPTY
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

        public static bool CheckWin(clsGeneral.fKey attack, clsGeneral.fKey block, int PositionOfRow, int PositionOfColumn, ref List<Cell> lstCell)
        {
            int sumAttack = 0;
            int sumBlock = 0;
            lstCell.Clear();

            /*Horizontal*/
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn + d));
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn - d));
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                lstCell.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortHorizontal(ref lstCell);
                return true;
            }

            /*Vertical*/
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows)
                {
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == PositionOfColumn);
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = (PositionOfRow + d), PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow - d) && x.PositionOfColumn == PositionOfColumn);
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = (PositionOfRow - d), PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                lstCell.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortVertical(ref lstCell);
                return true;
            }

            /*Cross (top-left) - (bottom-right)*/
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn + d));
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = (PositionOfRow + d), PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow - d) && x.PositionOfColumn == (PositionOfColumn - d));
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = (PositionOfRow - d), PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                lstCell.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortCross_A(ref lstCell);
                return true;
            }

            /*Cross (top-right) - (bottom-left)*/
            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn - d) > 0)
                {
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn - d));
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = (PositionOfRow + d), PositionOfColumn = (PositionOfColumn - d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                    Cell cell = clsGeneral.ChessBoard.Boards.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow - d) && x.PositionOfColumn == (PositionOfColumn + d));
                    if (cell.TypeOfChess != clsGeneral.fKey.X && cell.TypeOfChess != clsGeneral.fKey.O && cell.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (cell.TypeOfChess == attack)
                    {
                        sumAttack++;
                        lstCell.Add(new Cell() { PositionOfRow = (PositionOfRow - d), PositionOfColumn = (PositionOfColumn + d), TypeOfChess = attack });
                    }
                    else
                    {
                        if (cell.TypeOfChess == block)
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
                lstCell.Add(new Cell() { PositionOfRow = PositionOfRow, PositionOfColumn = PositionOfColumn, TypeOfChess = attack });
                SortCross_B(ref lstCell);
                return true;
            }
        
            lstCell.Clear();
            return false;
        }

        public static void SortHorizontal(ref List<Cell> lstCell)
        {
            int n = lstCell.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (lstCell[i].PositionOfColumn > lstCell[j].PositionOfColumn)
                    {
                        int temp = lstCell[i].PositionOfColumn;
                        lstCell[i].PositionOfColumn = lstCell[j].PositionOfColumn;
                        lstCell[j].PositionOfColumn = temp;
                    }
                }
            }
        }

        public static void SortVertical(ref List<Cell> lstCell)
        {
            int n = lstCell.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (lstCell[i].PositionOfRow > lstCell[j].PositionOfRow)
                    {
                        int t1 = lstCell[i].PositionOfRow;
                        lstCell[i].PositionOfRow = lstCell[j].PositionOfRow;
                        lstCell[j].PositionOfRow = t1;
                    }
                }
            }
        }

        public static void SortCross_A(ref List<Cell> lstCell)
        {
            int n = lstCell.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (lstCell[i].PositionOfRow > lstCell[j].PositionOfRow && lstCell[i].PositionOfColumn > lstCell[j].PositionOfColumn)
                    {
                        int t1 = lstCell[i].PositionOfRow;
                        lstCell[i].PositionOfRow = lstCell[j].PositionOfRow;
                        lstCell[j].PositionOfRow = t1;

                        int t2 = lstCell[i].PositionOfColumn;
                        lstCell[i].PositionOfColumn = lstCell[j].PositionOfColumn;
                        lstCell[j].PositionOfColumn = t2;
                    }
                }
            }
        }

        public static void SortCross_B(ref List<Cell> lstCell)
        {
            int n = lstCell.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (lstCell[i].PositionOfRow > lstCell[j].PositionOfRow && lstCell[i].PositionOfColumn < lstCell[j].PositionOfColumn)
                    {
                        int t1 = lstCell[i].PositionOfRow;
                        lstCell[i].PositionOfRow = lstCell[j].PositionOfRow;
                        lstCell[j].PositionOfRow = t1;

                        int t2 = lstCell[i].PositionOfColumn;
                        lstCell[i].PositionOfColumn = lstCell[j].PositionOfColumn;
                        lstCell[j].PositionOfColumn = t2;
                    }
                }
            }
        }
    }
}

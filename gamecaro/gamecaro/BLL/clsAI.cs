using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecaro
{
    public class clsAI
    {
        //static int[] position_X = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 };
        //static int[] position_Y = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        static long[] AScore = new long[7] { 0, 6, 60, 600, 6000, 60000, 600000 };
        static long[] BScore = new long[7] { 0, 4, 40, 400, 4000, 40000, 400000 };
        static long Ratio = 5;

        public static bool MinMax(ref int PositionOfRow, ref int PositionOfColumn, clsGeneral.fKey Attack, clsGeneral.fKey Block)
        {
            bool IsEmpty = true;

            long Score = long.MinValue;

            foreach (ChessPoint chess in clsGeneral.ChessBoard.ListChesses)
            {
                /*Get chess is empty*/
                if (chess.TypeOfChess == clsGeneral.fKey.EMPTY)
                {
                    /*Set value*/
                    IsEmpty = false;
                    chess.Score = 0;

                    /*Get value*/
                    long ScoreAttack = CalculateAttackScore(chess.PositionOfRow, chess.PositionOfColumn, Attack, Block);
                    long ScoreBlock = CalculateBlockScore(chess.PositionOfRow, chess.PositionOfColumn, Block, Attack);

                    chess.Score = ScoreAttack > ScoreBlock ? ScoreAttack : ScoreBlock;
                    if (chess.Score > Score)
                    {
                        Score = chess.Score;
                        PositionOfRow = chess.PositionOfRow;
                        PositionOfColumn = chess.PositionOfColumn;
                    }
                }
            }

            return IsEmpty;
        }

        static long CalculateAttackScore(int PositionOfRow, int PositionOfColumn, clsGeneral.fKey Attack, clsGeneral.fKey Block)
        {
            long Score = 0;

            int NumberOfAttack = 0;
            int NumberOfBlock = 0;

            /*Horizontal*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock];


            /*Vertical*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == PositionOfColumn);
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock];

            /*Cross (top-left) - (bottom-right)*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock];

            /*Cross (top-right) - (bottom-left)*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn - d) > 0)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn - d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock];

            return Score;
        }

        static long CalculateBlockScore(int PositionOfRow, int PositionOfColumn, clsGeneral.fKey Attack, clsGeneral.fKey Block)
        {
            long Score = 0;

            int NumberOfAttack = 0;
            int NumberOfBlock = 0;

            /*Horizontal*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == PositionOfRow && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock] * Ratio;


            /*Vertical*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == PositionOfColumn);
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock] * Ratio;

            /*Cross (top-left) - (bottom-right)*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn + d) < clsGeneral.ChessBoard.NumberOfColumns)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn + d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock] * Ratio;

            /*Cross (top-right) - (bottom-left)*/
            NumberOfAttack = 0;
            NumberOfBlock = 0;

            for (int d = 1; d < 6; d++)
            {
                if ((PositionOfRow + d) < clsGeneral.ChessBoard.NumberOfRows && (PositionOfColumn - d) > 0)
                {
                    ChessPoint chess = clsGeneral.ChessBoard.ListChesses.FirstOrDefault(x => x.PositionOfRow == (PositionOfRow + d) && x.PositionOfColumn == (PositionOfColumn - d));
                    if (chess.TypeOfChess != clsGeneral.fKey.X && chess.TypeOfChess != clsGeneral.fKey.O && chess.TypeOfChess != clsGeneral.fKey.EMPTY)
                        continue;

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
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

                    if (chess.TypeOfChess == Attack)
                        NumberOfAttack++;
                    else
                    {
                        if (chess.TypeOfChess == Block)
                            NumberOfBlock++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (NumberOfBlock >= 2)
                Score += 0;
            else
                Score += AScore[NumberOfAttack] - BScore[NumberOfBlock] * Ratio;

            return Score;
        }

    }
}

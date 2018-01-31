using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game35Score
{
    class Program
    {
        static void Main(string[] args)
        {
            int col = 5;
            int row = 35;
            bool[] numbers = new bool[col];
            bool[][] array = new bool[row + col][];
            int i = 0;
            for (i = 0; i < (row + col); i++)
            {
                array[i] = new bool[col];
            }
            array[1][0] = true;
            array[3][2] = true;
            array[4][4] = true;
            array[5][3] = true;
            array[6][2] = true;
            array[7][0] = true;
            array[7][1] = true;
            array[8][0] = true;
            array[10][4] = true;
            array[11][3] = true;
            array[12][2] = true;
            array[12][4] = true;
            array[13][1] = true;
            array[13][2] = true;
            array[14][0] = true;
            array[14][3] = true;
            array[16][2] = true;
            array[17][4] = true;
            array[18][3] = true;
            array[19][2] = true;
            array[20][0] = true;
            array[20][1] = true;
            array[21][0] = true;
            array[23][4] = true;
            array[24][3] = true;
            array[25][2] = true;
            array[25][4] = true;
            array[26][1] = true;
            array[26][2] = true;
            array[27][0] = true;
            array[27][3] = true;
            array[29][2] = true;
            array[30][4] = true;
            array[31][3] = true;
            array[32][2] = true;
            array[33][0] = true;
            array[33][1] = true;
            array[34][0] = true;

            StartGame(col, row, numbers, array);
        }

        static void StartGame(int col, int row, bool[] numbers, bool[][] array)
        {
            //PlayerVSComputer(col, row, numbers, array);
            ComputerVSPlayer(col, row, numbers, array);
        }

        static void PlayerVSComputer(int col, int row, bool[] numbers, bool[][] array)
        {
            int score = 0;
            int scoreMax = 35;
            int winner = -1;//0: Player, 1:Computer

            while (score <= scoreMax)
            {
                score += Human(col, numbers);
                Console.WriteLine("Score: {0}", score);
                if (score > scoreMax)
                {
                    winner = 1;
                    break;
                }

                score += Computer(col, row, score, numbers, array);
                Console.WriteLine("Score: {0}", score);
                if (score > scoreMax)
                {
                    winner = 0;
                    break;
                }
            }

            if (winner == 0)
                Console.WriteLine("Player win");
            else if (winner == 1)
                Console.WriteLine("Computer win");
            Console.ReadLine();
        }
        static void ComputerVSPlayer(int col, int row, bool[] numbers, bool[][] array)
        {
            int score = 0;
            int scoreMax = 35;
            int winner = -1;//0: Player, 1:Computer

            while (score <= scoreMax)
            {
                score += Computer(col, row, score, numbers, array);
                Console.WriteLine("Score: {0}", score);
                if (score > scoreMax)
                {
                    winner = 0;
                    break;
                }

                score += Human(col, numbers);
                Console.WriteLine("Score: {0}", score);
                if (score > scoreMax)
                {
                    winner = 1;
                    break;
                }

            }

            if (winner == 0)
                Console.WriteLine("Player win");
            else if (winner == 1)
                Console.WriteLine("Computer win");
            Console.ReadLine();
        }

        static int Human(int col, bool[] numbers)
        {
            Console.Write("Player: ");
            int index = Convert.ToInt32(Console.ReadLine());
            while (numbers[index - 1])
            {
                Console.Write("Player input again: ");
                index = Convert.ToInt32(Console.ReadLine());
            }
            for (int i = 0; i < col; i++)
            {
                numbers[i] = false;
            }
            numbers[index - 1] = true;
            return index;
        }

        static int Computer(int col, int row, int score, bool[] numbers, bool[][] array)
        {
            int index = -1;

            for (int i = score; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (array[i][j])
                    {
                        index = j;
                        break;
                    }
                }
                if (index != -1)
                    break;
            }

            if (index == -1)
            {
                for (int i = 0; i < col; i++)
                {
                    if (!numbers[i])
                    {
                        index = i;
                        break;
                    }
                }
            }
            for (int i = 0; i < col; i++)
            {
                numbers[i] = false;
            }
            numbers[index] = true;
            Console.WriteLine("Computer: {0}", index + 1);
            return index + 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trucxanh
{
    class XulyGame
    {
        public bool checkDigit(String s)
        {
            bool t=true;
            if (s == "")
                t = false;
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsDigit(s[i]) == false)
                    {
                        t = false;
                        break;
                    }
                }
                if (t == true)
                {
                    if (int.Parse(s) > 25 || int.Parse(s) < 1 || int.Parse(s) % 2 == 1)
                        t = false;
                }
            }
            return t;
        }
        public int[][] TaoMangNgauNhien(int n, int m)
        {
            int[][] A = null;
            A = new int[n][];
            int i = 0;

            for (i = 0; i < n; i++)
                A[i] = new int[n];

            Random rand = new Random();
            for (i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i > j)
                    {
                        int t = rand.Next(0, m);
                        A[i][j] = t;
                        A[j][i] = t;
                    }
                }
            
            int k=n*10;
            while (k >= 0)
            {
                int r = rand.Next(0, n);
                int c = rand.Next(0, n);
                int h = rand.Next(0, n);
                int temp = A[r][h];
                A[r][h] = A[h][c];
                A[h][c] = temp;
                k--;
            }
            return A;
        }
        public bool KiemTraFull(int[][] A, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (A[i][j] == -1)
                        return false;
            return true;
        }
        public bool KiemTraGiongNhau(int[][] A, int[][] tbIndex, int x, int y)
        {
            int temp1 = -1;
            int temp2 = -1;
            int n = A.Length;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (tbIndex[i][j] == x)
                        temp1 = A[i][j];
                    if (tbIndex[i][j] == y)
                        temp2 = A[i][j];
                }
            if (temp1 == temp2 && x != y)
                return true;
            return false;
        }
        public bool KiemTraKetThucGame(int[][] A, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (A[i][j] != -1)
                        return false;
            return true;
        }
        public bool isEmpty(string s)
        {
            if (s == "")
                return true;
            return false;
        }
    }
}
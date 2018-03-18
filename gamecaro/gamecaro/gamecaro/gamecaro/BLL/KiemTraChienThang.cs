using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gamecaro
{
    public class KiemTraChienThang
    {
        private int[][] banCo;
        public KiemTraChienThang(int[][] _banco)
        {
            this.banCo = _banco;
        }
        public KiemTraChienThang() { }

        public List<Point> KiemTra(Point point, int attack, int block)
        {
            BanCoCaRo bc = new BanCoCaRo();
            List<Point> lst = new List<Point>();
            //Point p_temp = new Point();
            int sumAttack = 0;
            int sumBlock = 0;

            //Kiem tra hang ngang
            for (int d = 1; d < 6 && (point.Y + d) < (bc.SoOCo-1); d++)
            {
                if (banCo[point.X][point.Y + d] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X, point.Y + d));
                }
                else
                {
                    if (banCo[point.X][point.Y + d] == block)
                        sumBlock++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X][point.Y - d] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X, point.Y - d));
                }
                else
                {
                    if (banCo[point.X][point.Y - d] == block)
                        sumBlock++;
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
                return lst;

            //Kiem tra hang doc
            lst.Clear();
            sumAttack = 0;
            sumBlock = 0;
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo-1); d++)
            {
                if (banCo[point.X + d][point.Y] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X + d, point.Y));
                }
                else
                {
                    if (banCo[point.X + d][point.Y] == block)
                        sumBlock++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X - d) >= 1; d++)
            {
                if (banCo[point.X - d][point.Y] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X - d, point.Y));
                }
                else
                {
                    if (banCo[point.X - d][point.Y] == block)
                        sumBlock++;
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
                return lst;

            //Kiem tra hang cheo xuong
            lst.Clear();
            sumAttack = 0;
            sumBlock = 0;
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo-1) && (point.Y + d) < (bc.SoOCo-1); d++)
            {
                if (banCo[point.X + d][point.Y + d] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X + d, point.Y + d));
                }
                else
                {
                    if (banCo[point.X + d][point.Y + d] == block)
                        sumBlock++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X - d][point.Y - d] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X - d, point.Y - d));
                }
                else
                {
                    if (banCo[point.X - d][point.Y - d] == block)
                        sumBlock++;
                    break;
                }
            }
            if (sumAttack >= 4 && sumBlock < 2)
                return lst;


            //Kiem tra hang cheo len
            lst.Clear();
            sumAttack = 0;
            sumBlock = 0;
            for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y + d) < (bc.SoOCo-1); d++)
            {
                if (banCo[point.X - d][point.Y + d] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X - d, point.Y + d));
                }
                else
                {
                    if (banCo[point.X - d][point.Y + d] == block)
                        sumBlock++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo-1) && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X + d][point.Y - d] == attack)
                {
                    sumAttack++;
                    lst.Add(new Point(point.X + d, point.Y - d));
                }
                else
                {
                    if (banCo[point.X + d][point.Y - d] == block)
                        sumBlock++;
                    break;
                }
            }

            if (sumAttack >= 4 && sumBlock < 2)
                return lst;

            lst.Clear();
            return lst;
        }

        public void SapXep(List<Point> lst)
        {
            int n = lst.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (lst[i].X > lst[j].X)
                    {
                        Point p_temp = lst[i];
                        lst[i] = lst[j];
                        lst[j] = p_temp;
                    }
                }
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (lst[i].X == lst[j].X)
                        if (lst[i].Y > lst[j].Y)
                        {
                            Point p_temp = lst[i];
                            lst[i] = lst[j];
                            lst[j] = p_temp;
                        }
                }
        }
    }
}
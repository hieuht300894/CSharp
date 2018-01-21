using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gamecaro
{
    public class ToaDo
    {
        BanCoCaRo bc = new BanCoCaRo();
        int[][] banCo;
        public ToaDo(int[][] _banco)
        {
            banCo = _banco;
        }
        public Point ChuyenToaDo(int x, int y)
        {
            Point point = new Point();
            int div_x = x / bc.KichThuoc;
            int div_y = y / bc.KichThuoc;
            int mod_x = x % bc.KichThuoc;
            int mod_y = y % bc.KichThuoc;
            if (mod_x > (bc.KichThuoc * 2 / 3))
                div_x += 1;
            if (mod_y > (bc.KichThuoc * 2 / 3))
                div_y += 1;

            if (div_x <= 0 || div_y <= 0 || div_x >= bc.SoOCo - 1 || div_y >= bc.SoOCo - 1)
            {
                point.X = -1;
                point.Y = -1;
            }
            else
            {
                if (banCo[div_y][div_x] == 0)
                {
                    point.X = div_y;
                    point.Y = div_x;
                }
                else
                {
                    point.X = -2;
                    point.Y = -2;
                }
            }
            return point;
        }
    }
}
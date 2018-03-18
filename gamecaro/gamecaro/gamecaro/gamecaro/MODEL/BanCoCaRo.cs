using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gamecaro
{
    public class BanCoCaRo
    {
        private const int soOCo = 25;
        private const int kichthuoc = 23;

        public int SoOCo
        {
            get { return soOCo; }
        }
        public int KichThuoc
        {
            get { return kichthuoc; }
        }

        int[][] banco;
        public BanCoCaRo()
        {
            banco = new int[SoOCo][];
            int i;
            for (i = 0; i < SoOCo; i++)
                banco[i] = new int[SoOCo];
        }
        public int[][] BanCo()
        {
            return banco;
        }
        public void DatViTri(Point point, int player)
        {
            banco[point.X][point.Y] = player;
        }

        private int nguoichienthang = 0;

        public int NguoiChienThang
        {
            get { return nguoichienthang; }
            set { nguoichienthang = value; }
        }

        public bool ConOTrong()
        {
            for (int i = 1; i < soOCo - 1; i++)
                for (int j = 1; j < soOCo - 1; j++)
                    if (banco[i][j] == 0)
                        return true;
            return false;
        }
    }
}
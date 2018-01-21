using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gamecaro
{
    public class AI1
    {
        private struct NuocDi
        {
            public Point point;
            public long score;
            public int player;
            public NuocDi(Point _point, long _score, int _player)
            {
                this.point = _point;
                this.score = _score;
                this.player = _player;
            }
        }
        private int[][] banCo;
        private int[] vitri_x = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 };
        private int[] vitri_y = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        public AI1() { }
        public Point KhoiTao(int x, int y)
        {
            Random r = new Random();
            BanCoCaRo bc = new BanCoCaRo();
            Point p = new Point(bc.SoOCo / 2, bc.SoOCo / 2);
            while (banCo[p.X][p.Y] != 0)
            {
                p = new Point(x + vitri_x[r.Next(0, 8)], y + vitri_y[r.Next(0, 8)]);
            }
            return p;
        }
        public AI1(int[][] _banCo)
        {
            banCo = _banCo;
        }
        private long[] AScore = new long[7] { 0, 6, 60, 600, 6000, 60000, 600000 };
        private long[] BScore = new long[7] { 0, 4, 40, 400, 4000, 40000, 400000 };
        private long DiemTanCong(Point point, int attack, int block)
        {
            BanCoCaRo bc = new BanCoCaRo();
            long kq = 0;
            int S_block = 0;
            int S_attack = 0;
            // Kiểm tra hàng ngang 
            for (int d = 1; d < 6 && (point.Y + d) < bc.SoOCo - 1; d++)
            {
                if (banCo[point.X][point.Y + d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X][point.Y + d] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X][point.Y - d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X][point.Y - d] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block];

            // Kiểm tra hàng dọc 
            S_block = 0;
            S_attack = 0;
            for (int d = 1; d < 6 && (point.X + d) < bc.SoOCo - 1; d++)
            {
                if (banCo[point.X + d][point.Y] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X + d][point.Y] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X - d) >= 1; d++)
            {
                if (banCo[point.X - d][point.Y] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X - d][point.Y] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block];

            // Kiểm tra hàng chéo xuống 
            S_block = 0;
            S_attack = 0;
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1) && (point.Y + d) < (bc.SoOCo - 1); d++)
            {
                if (banCo[point.X + d][point.Y + d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X + d][point.Y + d] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X - d][point.Y - d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X - d][point.Y - d] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block];

            // Kiểm tra hàng chéo lên 
            S_block = 0;
            S_attack = 0;
            for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y + d) < (bc.SoOCo - 1); d++)
            {
                if (banCo[point.X - d][point.Y + d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X - d][point.Y + d] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1) && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X + d][point.Y - d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X + d][point.Y - d] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block];
            return kq;
        }
        private long DiemPhongThu(Point point, int attack, int block)
        {
            BanCoCaRo bc = new BanCoCaRo();
            int tile = 20;
            long kq = 0;
            int S_block = 0;
            int S_attack = 0;
            // Kiểm tra hàng ngang 
            for (int d = 1; d < 6 && (point.Y + d) < (bc.SoOCo - 1); d++)
            {
                if (banCo[point.X][point.Y + d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X][point.Y + d] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X][point.Y - d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X][point.Y - d] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block] * tile;

            // Kiểm tra hàng dọc 
            S_block = 0;
            S_attack = 0;
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1); d++)
            {
                if (banCo[point.X + d][point.Y] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X + d][point.Y] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X - d) >= 1; d++)
            {
                if (banCo[point.X - d][point.Y] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X - d][point.Y] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block] * tile;

            // Kiểm tra hàng chéo xuống 
            S_block = 0;
            S_attack = 0;
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1) && (point.Y + d) < (bc.SoOCo - 1); d++)
            {
                if (banCo[point.X + d][point.Y + d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X + d][point.Y + d] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X - d][point.Y - d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X - d][point.Y - d] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block] * tile;

            // Kiểm tra hàng chéo lên 
            S_block = 0;
            S_attack = 0;
            for (int d = 1; d < 6 && (point.X - d) >= 1 && (point.Y + d) < (bc.SoOCo - 1); d++)
            {
                if (banCo[point.X - d][point.Y + d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X - d][point.Y + d] == block)
                        S_block++;
                    break;
                }
            }
            for (int d = 1; d < 6 && (point.X + d) < (bc.SoOCo - 1) && (point.Y - d) >= 1; d++)
            {
                if (banCo[point.X + d][point.Y - d] == attack)
                    S_attack++;
                else
                {
                    if (banCo[point.X + d][point.Y - d] == block)
                        S_block++;
                    break;
                }
            }
            if (S_block >= 2)
                kq += 0;
            else
                kq += AScore[S_attack] - BScore[S_block] * tile;
            return kq;
        }
        private void SapXepNuocDi(List<NuocDi> lst)
        {
            int n = lst.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (lst[i].score < lst[j].score)
                    {
                        NuocDi temp = lst[i];
                        lst[i] = lst[j];
                        lst[j] = temp;
                    }
                }
        }

        private List<NuocDi> mayTanCong()
        {
            BanCoCaRo bc = new BanCoCaRo();
            List<NuocDi> lst = new List<NuocDi>();
            //long diemtoiuu = 0;
            for (int i = 1; i < bc.SoOCo - 1; i++)
                for (int j = 1; j < bc.SoOCo - 1; j++)
                    // Quét các ô trống 
                    if (banCo[i][j] == 0)
                    {
                        //Gồm điểm tấn công là đánh
                        // điểm phòng thủ là chặn
                        // 2 nước đi của máy
                        // 1 nước đi của người
                        long diemtancong = DiemTanCong(new Point(i, j), 2, 1);
                        long diemphongthu = DiemPhongThu(new Point(i, j), 1, 2);
                        long diemxet = diemtancong > diemphongthu ? diemtancong : diemphongthu;
                        int ngchoi = diemtancong > diemphongthu ? 2 : 1;
                        Point p = new Point();
                        p.X = i;
                        p.Y = j;
                        NuocDi nd;
                        nd.point = p;
                        nd.score = diemxet;
                        nd.player = ngchoi;
                        lst.Add(nd);
                    }
            SapXepNuocDi(lst);
            return lst;
        }
        private NuocDi nguoiTanCong()
        {
            BanCoCaRo bc = new BanCoCaRo();
            List<NuocDi> lst = new List<NuocDi>();
            //long diemtoiuu = 0;
            for (int i = 1; i < bc.SoOCo - 1; i++)
                for (int j = 1; j < bc.SoOCo - 1; j++)
                    // Quét các ô trống 
                    if (banCo[i][j] == 0)
                    {
                        //Gồm điểm tấn công là đánh
                        // điểm phòng thủ là chặn
                        // 2 nước đi của máy
                        // 1 nước đi của người
                        long diemtancong = DiemTanCong(new Point(i, j), 1, 2);
                        long diemphongthu = DiemPhongThu(new Point(i, j), 2, 1);
                        long diemxet = diemtancong > diemphongthu ? diemtancong : diemphongthu;
                        int ngchoi = diemtancong > diemphongthu ? 2 : 1;
                        Point p = new Point();
                        p.X = i;
                        p.Y = j;
                        NuocDi nd;
                        nd.point = p;
                        nd.score = diemxet;
                        nd.player = ngchoi;
                        lst.Add(nd);
                    }
            SapXepNuocDi(lst);
            return lst[0];
        }

        public Point Duyet2Cap()
        {
            int soduongmaydi = 0;
            int soduongnguoidi = 0;

            List<NuocDi> lst_maydi = mayTanCong();
            Point best_point = lst_maydi[0].point;
            long best_score = lst_maydi[0].score;

            for (int i = 0; i < 2; i++)
            {

                banCo[lst_maydi[i].point.X][lst_maydi[i].point.Y] = 2;
                if (lst_maydi[i].player == 1)
                    soduongnguoidi++;
                else
                    soduongmaydi++;

                NuocDi nguoidi = nguoiTanCong();
                banCo[nguoidi.point.X][nguoidi.point.Y] = 1;
                if (nguoidi.player == 1)
                    soduongnguoidi++;
                else
                    soduongmaydi++;

                NuocDi nuocdimaycap1 = mayTanCong()[0];
                if (nuocdimaycap1.player == 1)
                    soduongnguoidi++;
                else
                    soduongmaydi++;

                NuocDi nuocdinguoicap1 = nguoiTanCong();
                if (nuocdinguoicap1.player == 1)
                    soduongnguoidi++;
                else
                    soduongmaydi++;

                banCo[lst_maydi[i].point.X][lst_maydi[i].point.Y] = 0;
                banCo[nguoidi.point.X][nguoidi.point.Y] = 0;
                if (soduongmaydi > soduongnguoidi && best_score < lst_maydi[i].score)
                    best_point = lst_maydi[i].point;
            }
            return best_point;
        }
    }
}
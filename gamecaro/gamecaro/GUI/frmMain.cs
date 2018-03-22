using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using gamecaro.USERCONTROL;

namespace gamecaro
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            //lb_luatchoi.Text = thongbao;
            //lb_time.Text = "0:" +  ThoiGianChoi(); 

            //string[] thoigian = new string[] {"10","20", "30" };
            //toolStripCb_time.Items.AddRange(thoigian);
            //toolStripCb_time.SelectionStart = 0;
        }
        private struct NuocDi
        {
            public Point point;
            public int nguoichoi;
            public NuocDi(Point _point, int _nguoichoi)
            {
                this.point = _point;
                this.nguoichoi = _nguoichoi;
            }
        }
        Graphics g;
        BanCoCaRo bc = null;
        bool start = false;//Khoi dong game
        bool khoitaonuocdi = false;
        int chedochoi = -1;//0:NgvsNg , 1:NgvsM , 2:MvsNg
        int luotchoi = 1;
        List<NuocDi> lst_undo;
        List<NuocDi> lst_redo;
        int second = 10;
        int thoigian = 10;
        bool tamdung = false;

        private void VeBanCo()
        {
            Pen p = new Pen(Color.Black);
            Graphics g = pnBoard.CreateGraphics();
            pnBoard.Size = new System.Drawing.Size((bc.SoOCo - 1) * bc.KichThuoc + 3, (bc.SoOCo - 1) * bc.KichThuoc + 3);
            for (int i = 0; i < bc.SoOCo; i++)
            {
                g.DrawLine(p, 0, bc.KichThuoc * i, (bc.SoOCo - 1) * bc.KichThuoc, bc.KichThuoc * i);//ngang
                g.DrawLine(p, bc.KichThuoc * i, 0, bc.KichThuoc * i, (bc.SoOCo - 1) * bc.KichThuoc);//doc          
            }

            p.Dispose();
            g.Dispose();
        }

        private void Ve_X(Point point, Color color)
        {
            Pen p = new Pen(color, 2);
            int kt = bc.KichThuoc / 3;
            // Vẽ chéo thuận
            g.DrawLine(p, point.Y * bc.KichThuoc - kt, point.X * bc.KichThuoc - kt, point.Y * bc.KichThuoc + kt, point.X * bc.KichThuoc + kt);
            // Vẽ chéo nghịch
            g.DrawLine(p, point.Y * bc.KichThuoc - kt, point.X * bc.KichThuoc + kt, point.Y * bc.KichThuoc + kt, point.X * bc.KichThuoc - kt);
        }

        private void Ve_O(Point point, Color color)
        {
            Pen p = new Pen(color, 2);
            int kt = bc.KichThuoc / 3;
            g.DrawEllipse(p, point.Y * bc.KichThuoc - kt, point.X * bc.KichThuoc - kt, 2 * kt, 2 * kt);
        }

        private void VeDuongCheo(Point point1, Point point2, Color color)
        {
            Pen p = new Pen(color, 2);
            // Vẽ chéo thuận
            g.DrawLine(p, point1.Y * bc.KichThuoc, point1.X * bc.KichThuoc, point2.Y * bc.KichThuoc, point2.X * bc.KichThuoc);
        }

        private int ThoiGianChoi()
        {
            if (toolStripCb_time.SelectedItem == null)
                return 10;
            return int.Parse(toolStripCb_time.SelectedItem.ToString());
        }

        private void BatDau()
        {

            if (start)
            {
                Timer1.Enabled = false;
                bc = null;
                g.Clear(Color.White);
                lst_undo = null;
                lst_redo = null;
                khoitaonuocdi = false;
            }

            bc = new BanCoCaRo();
            g = pnBoard.CreateGraphics();
            lst_undo = new List<NuocDi>();
            lst_redo = new List<NuocDi>();
            VeBanCo();
            start = true;
            Timer1.Enabled = true;

            thoigian = ThoiGianChoi();
            second = thoigian;

            switch (chedochoi)
            {
                case 0:
                    luotchoi = 1;
                    //lb_chedochoi.Text = "Play mode: two players";
                    break;
                case 1:
                    //lb_chedochoi.Text = "Play mode: Player vs Com";
                    break;
                case 2:
                    //lb_chedochoi.Text = "Play mode: Com vs Player";
                    if (!khoitaonuocdi)
                        KhoiTaoNuocDi(new Point(bc.SoOCo / 2, bc.SoOCo / 2));
                    break;
            }

        }

        private void KetThuc()
        {
            if (start)
            {
                //lb_chedochoi.Text = "Play mode: ";
                Timer1.Enabled = false;
                start = false;
                bc = null;
                g.Clear(Color.White);
                lst_undo = null;
                lst_redo = null;
                khoitaonuocdi = false;
                chedochoi = -1;//0:NgvsNg , 1:NgvsM , 2:MvsNg
                luotchoi = 1;
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (start)
            {
                if (!tamdung)
                {
                    if (bc.NguoiChienThang == 0)
                    {
                        ToaDo td = new ToaDo(bc.BanCo());
                        Point point = td.ChuyenToaDo(e.X, e.Y);
                        if (point.X != -1 && point.Y != -1)
                        {
                            if (point.X != -2 && point.Y != -2)
                            {
                                if (chedochoi == 1 || chedochoi == 2)
                                    NguoiDanh(point);
                                else
                                {
                                    if (chedochoi == 0)
                                    {
                                        switch (luotchoi)
                                        {
                                            case 1:
                                                NguoiDanh(point);
                                                luotchoi = 2;
                                                second = thoigian;
                                                break;
                                            case 2:
                                                MayDanh(point);
                                                luotchoi = 1;
                                                second = thoigian;
                                                break;
                                        }
                                        if (!bc.ConOTrong())
                                        {
                                            Timer1.Enabled = false;
                                            MessageBox.Show("Out of chess,Even!", "Message");
                                            bc.NguoiChienThang = -1;
                                        }
                                    }
                                }
                            }
                            else
                                MessageBox.Show("This chess has been chosen !", "Message");
                        }
                        else
                            MessageBox.Show("Cannot choose this place !", "Message");
                    }
                    else
                    {
                        string thongbao = "";
                        if (bc.NguoiChienThang == 1 || bc.NguoiChienThang == 2)
                            thongbao = "Player " + bc.NguoiChienThang + " win";
                        else
                            thongbao = "Even";
                        MessageBox.Show(thongbao, "Message");
                    }
                }
                else
                {
                    MessageBox.Show("Pausing !", "Message");
                }
            }
            else
                MessageBox.Show("Press NewGame !", "Message");
        }

        private void NguoiDanh(Point p)
        {
            if (bc.NguoiChienThang == 0)
            {
                Timer1.Enabled = true;
                second = thoigian;

                NuocDi nd;
                nd.point = p;
                nd.nguoichoi = 1;
                lst_undo.Add(nd);
                lst_redo.Clear();

                Ve_X(p, Color.Blue);
                bc.DatViTri(p, 1);

                KiemTraChienThang kq = new KiemTraChienThang(bc.BanCo());
                List<Point> lst = kq.KiemTra(p, 1, 2);
                if (lst.Count >= 4)
                {
                    Timer1.Enabled = false;
                    lst.Add(p);
                    kq.SapXep(lst);
                    VeDuongCheo(lst[0], lst[lst.Count - 1], Color.Yellow);
                    MessageBox.Show("Player 1 win", "Message");
                    bc.NguoiChienThang = 1;
                }
                if (chedochoi == 1 || chedochoi == 2)
                {
                    if (khoitaonuocdi)
                    {
                        LayNuocDiChoMay();
                    }
                    else
                        KhoiTaoNuocDi(p);
                }
            }
        }

        private void MayDanh(Point p)
        {
            if (bc.NguoiChienThang == 0)
            {
                NuocDi nd;
                nd.point = p;
                nd.nguoichoi = 2;
                lst_undo.Add(nd);
                lst_redo.Clear();

                Ve_O(p, Color.Red);
                bc.DatViTri(p, 2);
                KiemTraChienThang kq = new KiemTraChienThang(bc.BanCo());
                List<Point> lst = kq.KiemTra(p, 2, 1);
                if (lst.Count >= 4)
                {
                    Timer1.Enabled = false;
                    lst.Add(p);
                    kq.SapXep(lst);
                    VeDuongCheo(lst[0], lst[lst.Count - 1], Color.Yellow);
                    if (chedochoi == 0)
                        MessageBox.Show("Player 2 win", "Message");
                    else
                        MessageBox.Show("Com win", "Message");
                    bc.NguoiChienThang = 2;
                }
            }
        }

        private void KhoiTaoNuocDi(Point p)
        {
            if (!khoitaonuocdi)
            {
                if (chedochoi == 1)
                {
                    AI ai = new AI(bc.BanCo());
                    MayDanh(ai.KhoiTao(bc.SoOCo / 2, bc.SoOCo / 2));
                }
                else if (chedochoi == 2)
                {
                    AI ai = new AI(bc.BanCo());
                    MayDanh(ai.KhoiTao(p.X, p.Y));
                }
                khoitaonuocdi = true;
            }
        }

        private void LayNuocDiChoMay()
        {
            bool kiemtraOTrong = bc.ConOTrong();
            if (kiemtraOTrong)
            {
                //AI ai = new AI(bc.BanCo());
                //Point point = ai.mayTanCong();

                AI1 ai = new AI1(bc.BanCo());
                Point point = ai.Duyet2Cap();
                MayDanh(point);
                if (!bc.ConOTrong())
                {
                    Timer1.Enabled = false;
                    MessageBox.Show("Out of chess,Even!", "Message");
                    bc.NguoiChienThang = -1;
                }
            }
            else
            {
                Timer1.Enabled = false;
                MessageBox.Show("Out of chess,Even!", "Message");
                bc.NguoiChienThang = -1;
            }
        }

        private void XoaNuocDi(Point point, int ngchoi)
        {
            switch (ngchoi)
            {
                case 1:
                    Ve_X(point, Color.WhiteSmoke);
                    break;
                case 2:
                    Ve_O(point, Color.WhiteSmoke);
                    break;
            }
            Pen p = new Pen(Color.Black);
            int kt = bc.KichThuoc / 3;
            kt += 2;
            g.DrawLine(p, point.Y * bc.KichThuoc, point.X * bc.KichThuoc - kt, point.Y * bc.KichThuoc, point.X * bc.KichThuoc + kt);
            g.DrawLine(p, point.Y * bc.KichThuoc - kt, point.X * bc.KichThuoc, point.Y * bc.KichThuoc + kt, point.X * bc.KichThuoc);
            bc.DatViTri(point, 0);
        }

        private void PhucHoiNuocDi(Point point, int ngchoi)
        {
            switch (ngchoi)
            {
                case 1:
                    Ve_X(point, Color.Blue);
                    break;
                case 2:
                    Ve_O(point, Color.Red);
                    break;
            }
            bc.DatViTri(point, ngchoi);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (second < 0)
            {
                Timer1.Enabled = false;
                //lb_time.Text = "Time's up";
                //lb_pause.Text = "You lost :(";
                bc.NguoiChienThang = 2;
            }
            else
            {
                //lb_pause.Text = "";
                //lb_time.Text = "0:" + second ;
                second--;
            }
        }

        private void humanVsHumanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chedochoi = 0;
            BatDau();
        }

        private void humanVsComToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chedochoi = 1;
            BatDau();
        }

        private void comVsHumanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chedochoi = 2;
            BatDau();
        }

        private void redo()
        {
            if (lst_redo != null)
            {
                int n = lst_redo.Count;
                if (n > 0)
                {
                    if (chedochoi == 1)
                    {
                        if (n >= 4)
                        {
                            PhucHoiNuocDi(lst_redo[n - 1].point, 1);
                            PhucHoiNuocDi(lst_redo[n - 2].point, 2);
                        }
                        else
                        {
                            PhucHoiNuocDi(lst_redo[n - 1].point, 1);
                            PhucHoiNuocDi(lst_redo[n - 2].point, 2);
                            khoitaonuocdi = true;
                        }
                        lst_undo.Add(lst_redo[n - 1]);
                        lst_redo.RemoveAt(n - 1);
                        lst_undo.Add(lst_redo[n - 2]);
                        lst_redo.RemoveAt(n - 2);
                    }
                    else if (chedochoi == 2)
                    {
                        if (n >= 2)
                        {
                            PhucHoiNuocDi(lst_redo[n - 1].point, 1);
                            PhucHoiNuocDi(lst_redo[n - 2].point, 2);
                            lst_undo.Add(lst_redo[n - 1]);
                            lst_redo.RemoveAt(n - 1);
                            lst_undo.Add(lst_redo[n - 2]);
                            lst_redo.RemoveAt(n - 2);
                        }
                    }
                    else if (chedochoi == 0)
                    {
                        PhucHoiNuocDi(lst_redo[n - 1].point, luotchoi);
                        luotchoi = luotchoi == 1 ? 2 : 1;
                        lst_undo.Add(lst_redo[n - 1]);
                        lst_redo.RemoveAt(n - 1);
                    }
                }
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void undo()
        {
            if (lst_undo != null)
            {
                int n = lst_undo.Count;
                if (n > 0)
                {
                    if (chedochoi == 1)
                    {
                        if (n >= 4)
                        {
                            XoaNuocDi(lst_undo[n - 1].point, 2);
                            XoaNuocDi(lst_undo[n - 2].point, 1);
                        }
                        else
                        {
                            XoaNuocDi(lst_undo[n - 1].point, 2);
                            XoaNuocDi(lst_undo[n - 2].point, 1);
                            khoitaonuocdi = false;
                        }
                        lst_redo.Add(lst_undo[n - 1]);
                        lst_undo.RemoveAt(n - 1);
                        lst_redo.Add(lst_undo[n - 2]);
                        lst_undo.RemoveAt(n - 2);
                    }
                    else if (chedochoi == 2)
                    {
                        if (n >= 3)
                        {
                            XoaNuocDi(lst_undo[n - 1].point, 2);
                            XoaNuocDi(lst_undo[n - 2].point, 1);
                            lst_redo.Add(lst_undo[n - 1]);
                            lst_undo.RemoveAt(n - 1);
                            lst_redo.Add(lst_undo[n - 2]);
                            lst_undo.RemoveAt(n - 2);
                        }
                    }
                    else if (chedochoi == 0)
                    {
                        XoaNuocDi(lst_undo[n - 1].point, luotchoi == 1 ? 2 : 1);
                        luotchoi = luotchoi == 1 ? 2 : 1;
                        lst_redo.Add(lst_undo[n - 1]);
                        lst_undo.RemoveAt(n - 1);
                    }
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Giupdo();
        }

        private void Giupdo()
        {
            string sLuatChoi =
                 "- Take turn to choose the chess .\n" +
                 "- If you have 5 chesses first without block 2 ways, you'll win.\n" +
                 "- Out of chess,Even.\n";
            MessageBox.Show(sLuatChoi, "How to play");
        }

        private void resartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KetThuc();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    chedochoi = 0;
                    BatDau();
                    break;
                case Keys.D2:
                    chedochoi = 1;
                    BatDau();
                    break;
                case Keys.D3:
                    chedochoi = 2;
                    BatDau();
                    break;
                case Keys.F3:
                    KetThuc();
                    break;
                case Keys.Z:
                    if (e.Control)
                        undo();
                    break;
                case Keys.X:
                    if (e.Control)
                        redo();
                    break;
                case Keys.F1:
                    //Xử lý sự kiện trợ giúp
                    Giupdo();
                    break;
                case Keys.F2:
                    //Xử lý sự kiện tác giả
                    TacGia();
                    break;
                case Keys.Space:
                    pauseGame();
                    break;
                case Keys.S:
                    if (e.Control)
                        saveGame();
                    break;
                case Keys.O:
                    if (e.Control)
                        openGame();
                    break;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TacGia();
        }

        private void TacGia()
        {
            string thongbao =
              "Hoàng Trọng Hiếu - 12110054\n" +
              "Lương Triều Vũ - 12110243";
            MessageBox.Show(thongbao, "Author");
        }

        private void pauseGame()
        {
            if (start)
            {
                Timer1.Enabled = tamdung;
                //lb_time.Text = "0:" + second;
                //lb_pause.Text = "Pause";
                tamdung = tamdung ? false : true;
            }
        }

        private void saveGame()
        {
            pauseGame();

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = "caro";
            savefile.Filter = ".caro|*.caro";
            savefile.Title = "Save game caro";
            if (savefile.ShowDialog() == DialogResult.OK && start)
            {
                FileStream fs;
                fs = new FileStream(savefile.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(khoitaonuocdi.ToString());
                sw.WriteLine(chedochoi);
                sw.WriteLine(luotchoi);
                sw.WriteLine(second);
                sw.WriteLine(thoigian);
                sw.WriteLine(tamdung);
                for (int i = 0; i < lst_undo.Count; i++)
                {
                    sw.WriteLine(lst_undo[i].nguoichoi + "," + lst_undo[i].point.X + "," + lst_undo[i].point.Y);
                }
                sw.Flush();
                sw.Close();
            }
            savefile.Dispose();
            savefile = null;
            pauseGame();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveGame();
        }

        private void openGame()
        {
            pauseGame();

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = ".caro|*.caro";
            open.ShowDialog();
            if (open.FileName != "")
            {
                StreamReader sr = File.OpenText(open.FileName);
                if (start)
                {
                    Timer1.Enabled = false;
                    bc = null;
                    g.Clear(Color.WhiteSmoke);
                    lst_undo = null;
                    lst_redo = null;
                    khoitaonuocdi = false;
                }

                bc = new BanCoCaRo();
                g = pnBoard.CreateGraphics();
                lst_undo = new List<NuocDi>();
                lst_redo = new List<NuocDi>();
                VeBanCo();
                start = true;
                Timer1.Enabled = true;

                khoitaonuocdi = bool.Parse(sr.ReadLine());
                chedochoi = int.Parse(sr.ReadLine());
                switch (chedochoi)
                {
                    case 0:
                        luotchoi = 1;
                        //lb_chedochoi.Text = "Play mode: two players";
                        break;
                    case 1:
                        //lb_chedochoi.Text = "Play mode: Player vs Com";
                        break;
                    case 2:
                        //lb_chedochoi.Text = "Play mode: Com vs Player";
                        break;
                }
                luotchoi = int.Parse(sr.ReadLine());
                second = int.Parse(sr.ReadLine());
                thoigian = int.Parse(sr.ReadLine());
                toolStripCb_time.SelectedItem = thoigian.ToString();
                tamdung = !bool.Parse(sr.ReadLine());
                pauseGame();

                string temp = "";
                while ((temp = sr.ReadLine()) != null)
                {
                    string[] arrNuocdi = temp.Split(',');
                    Point point = new Point(int.Parse(arrNuocdi[1]), int.Parse(arrNuocdi[2]));
                    int ngchoi = int.Parse(arrNuocdi[0]);
                    bc.DatViTri(point, ngchoi);
                    if (ngchoi == 1)
                        Ve_X(point, Color.Blue);
                    else
                        Ve_O(point, Color.Red);
                }
                sr.Close();
            }
            else
                pauseGame();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bc = new BanCoCaRo();
            BoardPlayerAndPlayer board = new BoardPlayerAndPlayer();
            board.Dock = DockStyle.Fill;
            board._SetPicture = new BoardPlayerAndPlayer.SetPicture(new Action<Image>((img) =>
            {
                pictureBox1.Image = img;
            }));
            tpBoard.Controls.Add(board, 0, 0);
        }
    }
}

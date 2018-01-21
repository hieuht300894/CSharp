using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Trucxanh
{
    public partial class fm_game : Form
    {
        XulyGame xl = new XulyGame();
        Button[][] ov = null;
        int[][] tabInd = null;
        int[][] A = null;
        Bitmap bmButton = null;
        public fm_game()
        {
            InitializeComponent();
        }
        int X = 1;
        int Y = 1;
        bool begin = false;
        public void TaoOVuong(int n)
        {
            int dai = (X-1) / n;
            int rong = (Y-120) / n;
            ov = new Button[n][];
            tabInd = new int[n][];
            for (int i = 0; i < n; i++)
            {
                ov[i] = new Button[n];
                tabInd[i] = new int[n];
            }
            int k = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    ov[i][j] = new Button();
                    ov[i][j].Size = new Size(dai, rong);
                    ov[i][j].BackColor = Color.SkyBlue;
                    ov[i][j].Location = new Point(dai * j, rong * (i+1));
                    ov[i][j].TabIndex= k;
                    tabInd[i][j] = k;
                    k++;
                    ov[i][j].MouseClick += new MouseEventHandler(buttonClick);
                    this.Controls.Add(ov[i][j]);
                }
        }
        int firstChoose = -1;
        int secondChoose = -1;
        int lan = 1;
        public void buttonClick(object sender, MouseEventArgs e)
        {
            Button bt = (Button)sender;
            if (lan == 1)
            {
                MoOVuong(bt.TabIndex);
                firstChoose = bt.TabIndex;
                timer2.Enabled = false;
            }
            else 
            {
                if (lan == 2)
                {
                    MoOVuong(bt.TabIndex);
                    secondChoose = bt.TabIndex;
                    timer2.Enabled = true;
                }
                else
                {
                    DongOVuong(bt.TabIndex);
                    time += 5;
                }
            }
            lan++;
        }
        private void bt_play_Click(object sender, EventArgs e)
        {
            if (begin == true)
                Play();
            else
                MessageBox.Show("Xin vui lòng chọn ảnh");
        }
        public void Play()
        {
            String s = tb_play.Text;
            if (xl.checkDigit(s) == true)
            {
                timer1.Enabled = true;
                bt_play.Hide();
                lb_play.Hide();
                tb_play.Hide();
                bt_end.Show();
                bt_end.Enabled = true;
                TaoOVuong(int.Parse(s));
                A = xl.TaoMangNgauNhien(int.Parse(s),S.Length);
            }
            else
            {
                MessageBox.Show("Nhập số chẵn 2->24");
                tb_play.Focus();
                tb_play.ResetText();
            }
        }
        private void bt_end_Click(object sender, EventArgs e)
        {
            End();
            bt_end.Enabled = false;
        }
        public void End()
        {
            bt_play.Show();
            lb_play.Show();
            tb_play.Show();
            int n = int.Parse(tb_play.Text);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    XoaOVuong(ov[i][j].TabIndex);
            time = 0;
            timer1.Enabled = false;
        }
        public void XoaOVuong(int index)
        {
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A.Length; j++)
                    if (tabInd[i][j] == index)
                    {
                        A[i][j] = -1;
                        this.Controls.Remove(ov[i][j]);
                        break;
                    }
        }

        public void MoOVuong(int index)
        {
            int n = int.Parse(tb_play.Text);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (tabInd[i][j] == index)
                    {
                        //bmButton = new Bitmap(S[A[i][j]]);
                        //BitmapRegion.CreateRegion(ov[i][j], bmButton);
                        ov[i][j].BackgroundImage = Image.FromFile(S[A[i][j]]);
                        ov[i][j].BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    }
        }
        public void DongOVuong(int index)
        {
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A.Length; j++)
                    if (tabInd[i][j] == index)
                    {
                        ov[i][j].BackgroundImage = null;
                        break;
                    }
        }

        private void fm_game_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            X = control.Size.Width;
            Y = control.Size.Height;
            Sizechange(X, Y);
        }
        public void Sizechange(int x, int y)
        {
            if (xl.isEmpty(tb_play.Text))
                tb_play.Focus();
            else
            {
                int n = int.Parse(tb_play.Text);
                int dai = (x - 3) / n;
                int rong = (y - 140) / n;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        ov[i][j].Size = new Size(dai, rong);
                        ov[i][j].Location = new Point(dai * j, rong * (i+1));
                    }
            }
        }
        private void fm_game_Load(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            X = control.Size.Width;
            Y = control.Size.Height;
            Sizechange(X, Y);
            bt_end.Enabled = false;
            bt_play.Enabled = false;
        }

        int s = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (s == 8)
            {
                if (xl.KiemTraGiongNhau(A, tabInd, firstChoose, secondChoose))
                {
                    XoaOVuong(firstChoose);
                    XoaOVuong(secondChoose);
                }
                else
                {
                    DongOVuong(firstChoose);
                    DongOVuong(secondChoose);
                }
                if (xl.KiemTraKetThucGame(A, int.Parse(tb_play.Text)))
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    MessageBox.Show(time.ToString() + "s");
                    End();
                }
                s = 0;
                lan = 1;
            }
            s++;
        }
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            lb_time.Text = time.ToString();
        }

        string[] S = null;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = ".png|*.png|.jpg|*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                S = open.FileNames;
                MessageBox.Show("Đã tải " + S.Length.ToString() + " ảnh");
                begin = true;
                bt_play.Enabled = true;
            }
            if (S == null)
                MessageBox.Show("Lỗi chọn ảnh");
        }
    }
}

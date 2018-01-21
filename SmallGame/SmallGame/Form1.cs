using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallGame
{
    public partial class Form1 : Form
    {
        int[][] picStatus;
        int num = 4;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0, j = 0;
            string[] files = new string[num * num];
            string str = @"C:\Users\Hieu\Desktop\file\IMG";
            picStatus = new int[num][];
            for (i = 0; i < num; i++)
            {
                picStatus[i] = new int[num];
            }

            for (i = 0; i < num * num; i++)
            {
                files[i] = str + (i + 1) + ".png";
            }

            i = 0;
            j = 0;
            foreach (Control ctr in panel1.Controls)
            {
                if (ctr.Name.Contains("pictureBox"))
                {
                    int index = -1;
                    try
                    {
                        index = Convert.ToInt32(ctr.Name.Substring(10));
                    }
                    catch { index = -1; }
                    if (index > 0)
                    {
                        ctr.BackgroundImage = Image.FromFile(files[index - 1]);

                        i = index % num;
                        j = index / num;
                        i = i == 0 ? j : index - j * num;
                        j--;
                        i--;
                        if (i >= 0 && j >= 0 && i < num && j < num)
                            picStatus[i][j] = index;
                    }
                    ctr.KeyPress += ctr_KeyPress;
                }
            }
        }

        void ctr_KeyPress(object sender, KeyPressEventArgs e)
        {
            PictureBox picBox = sender as PictureBox;
            switch (e.KeyChar)
            {
                case (char)Keys.Left:
                    break;
                case (char)Keys.Right:
                    break;
                case (char)Keys.Up:
                    break;
                case (char)Keys.Down:
                    break;
            }
        }
    }
}

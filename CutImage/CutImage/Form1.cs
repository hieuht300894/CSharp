using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CutImage
{
    public partial class Form1 : Form
    {
        #region Variable
        private Rectangle mHoverRectangle = Rectangle.Empty;
        private const int HOVER_RECTANGLE_SIZE = 20;
        private bool isLoadPicture;
        Graphics grImage;
        Bitmap bmImage;
        int index = 0;
        int indexImg = 0;
        int count = 0;
        public bool IsLoadPicture
        {
            get { return isLoadPicture; }
            set { isLoadPicture = value; }
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 10;
        }

        #region Event
        private void btUpload_Click(object sender, EventArgs e)
        {
            IsLoadPicture = false;
            lstFiles.Items.Clear();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png";
            open.Multiselect = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string filePath = open.FileName;
                //picCrop.Image = Image.FromFile(filePath);
                //IsLoadPicture = true;
                //numX.Maximum = numY.Maximum = numWidth.Maximum = numHeight.Maximum =
                //    picCrop.Image.Size.Width > picCrop.Image.Size.Height ? picCrop.Image.Size.Width : picCrop.Image.Size.Height;

                foreach (string filename in open.FileNames)
                {
                    string[] item = new string[3];
                    item[0] = Path.GetFileName(filename);
                    item[1] = "Wait";
                    item[2] = filename;
                    lstFiles.Items.Add(new ListViewItem(item));
                    Image img = Image.FromFile(filename);
                    if (img.Size.Width > (int)numWidth.Maximum)
                        numX.Maximum = numWidth.Maximum = img.Size.Width;
                    if (img.Size.Height > (int)numHeight.Maximum)
                        numY.Maximum = numHeight.Maximum = img.Size.Height;
                }

                foreach (ListViewItem item in lstPoint.Items)
                {
                    item.SubItems[4].Text = "Wait";
                }
            }
            IsLoadPicture = true;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void picCrop_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsLoadPicture && picCrop.Image != null)
            {
                numX.Value = e.X;
                numY.Value = e.Y;
                ResizeHightLight();
            }
        }

        private void picCrop_MouseLeave(object sender, EventArgs e)
        {
            //mHoverRectangle = Rectangle.Empty;
        }

        private void picCrop_Paint(object sender, PaintEventArgs e)
        {
            if (mHoverRectangle != Rectangle.Empty)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(222, Color.Transparent)))
                {
                    e.Graphics.FillRectangle(b, mHoverRectangle);
                }
            }
        }

        private void picCrop_DoubleClick(object sender, EventArgs e)
        {
            //lstPoint.Items.Add(((MouseEventArgs)e).Location);
            InsertItem();
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            ResizeHightLight();
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            ResizeHightLight();
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            ResizeHightLight();
        }

        private void numHeight_ValueChanged(object sender, EventArgs e)
        {
            ResizeHightLight();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            InsertItem();
        }

        private void lstPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstPoint.SelectedItems)
            {

            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                btStart.Text = "Start";
            }
            else
            {
                count = 0;
                indexImg = 0;
                index = 0;
                btStart.Text = "Stop";
            }
            timer1.Enabled = !timer1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (indexImg >= lstFiles.Items.Count)
            {
                btStart.Text = "Start";
                timer1.Enabled = false;
            }
            else
            {
                ListViewItem itemIMG = lstFiles.Items[indexImg];
                if (picCrop.Image == null)
                {
                    picCrop.Image = Image.FromFile(itemIMG.SubItems[2].Text);
                }

                if (index >= lstPoint.Items.Count)
                {
                    lstFiles.Items[indexImg].SubItems[1].Text = "OK";
                    index = 0;
                    indexImg++;
                    picCrop.Image = null;
                    foreach (ListViewItem item in lstPoint.Items)
                    {
                        item.SubItems[4].Text = "Wait";
                    }
                }
                else
                {
                    ListViewItem item = lstPoint.Items[index];
                    CropImage(new Rectangle(
                         Convert.ToInt32(item.SubItems[0].Text),
                         Convert.ToInt32(item.SubItems[1].Text),
                         Convert.ToInt32(item.SubItems[2].Text),
                         Convert.ToInt32(item.SubItems[3].Text)));
                    if (bmImage != null)
                    {
                        lstPoint.Items[index].SubItems[4].Text = SaveImage() ? "OK" : "Error";
                    }
                    else
                    {
                        btStart.Text = "Start";
                        lstPoint.Items[index].SubItems[4].Text = "Error";
                        timer1.Enabled = false;
                    }
                    index++;
                }
            }
        }

        private void btSaveAuto_Click(object sender, EventArgs e)
        {
            InsertItems();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstFiles.Items)
            {
                item.SubItems[1].Text = "Wait";
            }
            lstPoint.Items.Clear();
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstFiles.SelectedItems)
            {
                picCrop.Image = null;
                picCrop.Image = Image.FromFile(item.SubItems[2].Text);
            }
        }
        #endregion

        #region Method
        private bool SaveImage()
        {
            count++;
            try
            {
                string filename = "IMG" + count + ".png";
                bmImage.Save(filename, ImageFormat.Png);
                bmImage.Dispose();
                grImage.Dispose();
                return true;
            }
            catch { return false; }
        }

        private void ResizeHightLight()
        {
            if (IsLoadPicture)
            {
                lbCurPoint.Text = "{" + numX.Value + "," + numY.Value + "}";
                mHoverRectangle = new Rectangle(
                Convert.ToInt32(numX.Value),
                Convert.ToInt32(numY.Value),
                Convert.ToInt32(numWidth.Value),
                Convert.ToInt32(numHeight.Value));
                picCrop.Invalidate();
            }
        }

        private void InsertItem()
        {
            string[] item = new string[] 
            {
                numX.Value.ToString(),
                numY.Value.ToString(),
                numWidth.Value.ToString(),
                numHeight.Value.ToString(),
                "Wait"
            };
            lstPoint.Items.Add(new ListViewItem(item));

            //CropImage(new Rectangle(
            //     Convert.ToInt32(item[0]),
            //     Convert.ToInt32(item[1]),
            //     Convert.ToInt32(item[2]),
            //     Convert.ToInt32(item[3])));
            //if (bmImage != null)
            //{
            //}
            //bmImage.Dispose();
            //grImage.Dispose();
        }

        private void InsertItems()
        {
            if (picCrop.Image == null)
            {
                MessageBox.Show("Please choose a image", "Message");
                return;
            }
            int wPart = (int)(picCrop.Image.Width / Convert.ToInt32(numWidth.Value));
            int hPart = (int)(picCrop.Image.Height / Convert.ToInt32(numHeight.Value));
            int wPart_Du = (int)(picCrop.Image.Width % Convert.ToInt32(numWidth.Value));
            int hPart_Du = (int)(picCrop.Image.Height % Convert.ToInt32(numHeight.Value));

            string[] item = new string[5];

            for (int i = 0; i <= wPart; i++)
            {
                for (int j = 0; j <= hPart; j++)
                {
                    item[0] = (i * Convert.ToInt32(numWidth.Value)).ToString();
                    item[1] = (j * Convert.ToInt32(numHeight.Value)).ToString();
                    item[4] = "Wait";
                    if (i == wPart || j == hPart)
                    {
                        if (wPart_Du > 0 && hPart_Du > 0)
                        {
                            item[2] = wPart_Du.ToString();
                            item[3] = hPart_Du.ToString();
                        }
                    }
                    else
                    {
                        item[2] = numWidth.Value.ToString();
                        item[3] = numHeight.Value.ToString();
                    }
                    lstPoint.Items.Add(new ListViewItem(item));
                }
            }
        }

        private void CropImage(Rectangle section)
        {
            try
            {
                bmImage = new Bitmap(section.Width, section.Height, PixelFormat.Format32bppArgb);
                bmImage.SetResolution(72, 72);
                grImage = Graphics.FromImage(bmImage);
                grImage.SmoothingMode = SmoothingMode.AntiAlias;
                grImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grImage.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grImage.DrawImage(picCrop.Image, new Rectangle(0, 0, section.Width, section.Height), section.X, section.Y, section.Width, section.Height, GraphicsUnit.Pixel);
            }
            catch { bmImage = null; }
        }
        #endregion
    }
}

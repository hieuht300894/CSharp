using System;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Xml;

namespace SetBackground
{
    public partial class Form1 : Form
    {
        #region Variable
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        const int SPIF_SENDCHANGE = 0x2;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public enum Style : int
        {
            Fill,
            Fit,
            Stretch,
            Tile,
            Center,
            Span
        }
        List<string> lstTimes;
        #endregion

        #region Form
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            lstTimes = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                string value = "";
                if (i < 10)
                    value = "0" + i;
                else
                    value = i.ToString();
                lstTimes.Add(value + ":00");
                lstTimes.Add(value + ":15");
                lstTimes.Add(value + ":30");
                lstTimes.Add(value + ":45");
            }
            cbxTime.DataSource = lstTimes;
            //timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (lstIMG.Count > 0)
            //{
            //    int index = new Random().Next(0, lstIMG.Count - 1);
            //    Set(lstIMG[index], Style.Span);
            //}
        }
        private void btUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void btLoad_Click(object sender, EventArgs e)
        {
            LoadFile();
        }
        private void btRemove_Click(object sender, EventArgs e)
        {
            RemoveItem();
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }
        private void lstDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDetailFile();
        }
        #endregion

        #region Method
        private void Set(string url, Style style)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            //Fill 10
            //Fit 6
            //stretch 2
            //Tile 0
            //Center 0
            //Span 22
            switch (style)
            {
                case Style.Center:
                    key.SetValue(@"WallpaperStyle", 0, RegistryValueKind.String);
                    key.SetValue(@"TileWallpaper", 0, RegistryValueKind.String);
                    break;
                case Style.Fill:
                    key.SetValue(@"WallpaperStyle", 10, RegistryValueKind.String);
                    key.SetValue(@"TileWallpaper", 0, RegistryValueKind.String);
                    break;
                case Style.Fit:
                    key.SetValue(@"WallpaperStyle", 6, RegistryValueKind.String);
                    key.SetValue(@"TileWallpaper", 0, RegistryValueKind.String);
                    break;
                case Style.Span:
                    key.SetValue(@"WallpaperStyle", 22, RegistryValueKind.String);
                    key.SetValue(@"TileWallpaper", 0, RegistryValueKind.String);
                    break;
                case Style.Stretch:
                    key.SetValue(@"WallpaperStyle", 2, RegistryValueKind.String);
                    key.SetValue(@"TileWallpaper", 0, RegistryValueKind.String);
                    break;
                case Style.Tile:
                    key.SetValue(@"WallpaperStyle", 0, RegistryValueKind.String);
                    key.SetValue(@"TileWallpaper", 1, RegistryValueKind.String);
                    break;
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, url, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        private void UploadFile()
        {
            groupBox1.Enabled = false;
            lstDetail.Items.Clear();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png";
            open.Multiselect = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string filename in open.FileNames)
                {
                    string[] item = new string[4];
                    item[0] = Path.GetFileName(filename);
                    item[1] = filename;
                    item[2] = "00:00";
                    item[3] = "Not set";
                    lstDetail.Items.Add(new ListViewItem(item));
                }
                groupBox1.Enabled = true;
            }
        }
        private void SaveFile()
        {
            try
            {
                string GridLayoutPath = @"Save";
                if (!System.IO.Directory.Exists(GridLayoutPath))
                    System.IO.Directory.CreateDirectory(GridLayoutPath);

                string path;
                path = GridLayoutPath + @"\wallpaper" + ".xml";
                if (System.IO.File.Exists(path))
                {
                    //Đóng và xóa file
                    File.Create(path).Close();
                    File.Delete(path);
                }
                //Tạo file
                XmlTextWriter boGhi = new XmlTextWriter(path, Encoding.UTF8);
                boGhi.Formatting = Formatting.Indented;
                boGhi.WriteStartDocument();
                boGhi.WriteStartElement("HinhAnh");
                boGhi.WriteString("\n");
                for (int i = 0; i < lstDetail.Items.Count; i++)
                {
                    boGhi.WriteStartElement("Hinh");
                    boGhi.WriteAttributeString("Filename", lstDetail.Items[i].SubItems[0].Text.ToString());
                    boGhi.WriteAttributeString("Filepath", lstDetail.Items[i].SubItems[1].Text.ToString());
                    boGhi.WriteAttributeString("Start", lstDetail.Items[i].SubItems[2].Text.ToString());
                    boGhi.WriteAttributeString("Status", lstDetail.Items[i].SubItems[3].Text.ToString());
                    boGhi.WriteEndElement();
                    boGhi.WriteString("\n");
                }
                boGhi.WriteEndElement();
                boGhi.WriteEndDocument();
                boGhi.Flush();
                boGhi.Close();

                MessageBox.Show("Start success.");
            }
            catch { MessageBox.Show("Start error."); }
        }
        private void LoadFile()
        {
            try
            {
                lstDetail.Items.Clear();
                string GridLayoutPath = @"Save";
                string path = GridLayoutPath + @"\wallpaper" + ".xml";
                if (File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open);
                    XmlTextReader xtr = new XmlTextReader(fs);
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.Element)
                        {
                            string[] item = new string[4];
                            bool isLoad = false;
                            for (int i = 0; i < xtr.AttributeCount; i++)
                            {
                                item[i] = xtr[i].ToString();
                                isLoad = true;
                            }
                            if (isLoad)
                                lstDetail.Items.Add(new ListViewItem(item));
                        }
                    }
                }
                MessageBox.Show("Load success.");
            }
            catch { MessageBox.Show("Load fail."); }
        }
        private void RemoveItem()
        {
            try
            {
                int index = Convert.ToInt32(lbIndex.Text);
                lstDetail.Items.RemoveAt(index);
            }
            catch { }
            ClearDetail();
        }
        private void UpdateItem()
        {
            try
            {
                int index = Convert.ToInt32(lbIndex.Text);
                lstDetail.Items[index].SubItems[2].Text = cbxTime.SelectedItem.ToString();
            }
            catch { }
            ClearDetail();
        }
        private void LoadDetailFile()
        {
            groupBox1.Enabled = false;
            foreach (ListViewItem item in lstDetail.SelectedItems)
            {
                groupBox1.Enabled = true;
                lbIndex.Text = item.Index.ToString();
                picCur.BackgroundImage = null;
                picCur.BackgroundImage = Image.FromFile(item.SubItems[1].Text);
                picCur.BackgroundImageLayout = ImageLayout.Zoom;

                string[] value = item.SubItems[2].Text.Split(':');
                if (value.Length == 2)
                {
                    //numHour.Value = Convert.ToDecimal(value[0]);
                    //numMinute.Value = Convert.ToDecimal(value[1]);
                }
                int index = lstTimes.FindIndex(x => x.Equals(item.SubItems[2].Text));
                if (index != -1)
                    cbxTime.SelectedIndex = index;
            }
        }
        private void ClearDetail()
        {
            lbIndex.ResetText();
            cbxTime.SelectedIndex = 0;
        }
        #endregion
    }
}

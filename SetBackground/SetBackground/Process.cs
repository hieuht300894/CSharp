using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace SetBackground
{
    class Process
    {
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

        public static void Set(string url, Style style)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
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

        public static void LoadFile()
        {

            try
            {
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
            }
            catch { }
        }
    }
}

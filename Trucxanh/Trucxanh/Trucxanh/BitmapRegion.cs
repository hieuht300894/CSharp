﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Trucxanh
{
    public class BitmapRegion
    {
        public BitmapRegion() { }
        public static void CreateRegion(Control ct, Bitmap bm)
        {
            if (ct == null || bm == null)
            {
                return;
            }
            // Thiết lập kích thước của control bằng với
            // kích thước của bitmap
            ct.Width = bm.Width;
            ct.Height = bm.Height;
            // kiểm tra xem ct có phải là Form
            if (ct is System.Windows.Forms.Form)
            {
                Form fm = (Form)ct;
                // Tăng kích thước form để dành chổ cho đường viền (nếu có)
                fm.Width += 15;
                fm.Height += 35;
                // thiết lập form trở thành không có border
                fm.FormBorderStyle = FormBorderStyle.None;
                // đưa bitmap trở thành background image
                fm.BackgroundImage = bm;
                // tính toán graphics path dựa trên bitmap
                GraphicsPath path = CalculateGraphicsPath(bm);
                // thay đổi thuộc tính Region của form
                fm.Region = new Region(path);
                fm.ControlBox = true;
            }
            else
                if (ct is System.Windows.Forms.Button)
                {
                    Button bt = (Button)ct;
                    bt.Text = ""; // hiển thị label của button
                    bt.Cursor = Cursors.Hand;
                    bt.BackColor = Color.Transparent;
                    // thiết lập background image cho button
                    bt.BackgroundImage = bm;
                    GraphicsPath path = CalculateGraphicsPath(bm);
                    bt.Region = new Region(path);
                }
        }

        private static GraphicsPath CalculateGraphicsPath(Bitmap bm)
        {
            GraphicsPath path = new GraphicsPath();
            // dùng màu của góc trên trái của bm là màu transparent
            Color transparentColor = bm.GetPixel(0, 0);
            // duyệt bm theo cột
            for (int row = 0; row < bm.Height; row++)
            {
                // duyệt bm theo hàng
                for (int col = 0; col < bm.Width; col++)
                {
                    if (bm.GetPixel(col, row) != transparentColor)
                    {
                        int i = col;
                        // đếm số pixel trong hàng khác màu với transparentColor
                        for (i = col + 1; i < bm.Width; i++)
                        {
                            if (bm.GetPixel(i, row) == transparentColor)
                                break;
                        }
                        path.AddRectangle(new Rectangle(col, row, i - col, 1));
                        col = i;
                    }
                }
            }
            return path;
        }
    }
}

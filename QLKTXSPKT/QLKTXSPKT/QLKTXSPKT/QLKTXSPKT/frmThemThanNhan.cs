using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using System.Data.SqlClient;
using DAO;

namespace QLKTXSPKT
{
    public partial class frmThemThanNhan : Form
    {
        public frmThemThanNhan()
        {
            InitializeComponent();
        }
     
        private void textBox_MSSV_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_MSSV.Text) == true)
                button_HoanThanh.Enabled = true;
            else
                button_HoanThanh.Enabled = false;
        }

        private void button_HoanThanh_Click(object sender, EventArgs e)
        {
           int MSSV =int.Parse( textBox_MSSV.Text);
           string hoten= textBox_HoTenTN.Text;
           string quanhe= textBox_QuanHe.Text;
           string nghenghiep= textBox_NgheNghiep.Text;
            string sdt=textBox_SDT.Text;

            BUS_TN.ThemThanNhan(hoten, quanhe, nghenghiep, sdt, MSSV);
            resetText();

        }

        private void textBox_SDT_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_MSSV.Text) == true)
                button_HoanThanh.Enabled = true;
            else
                button_HoanThanh.Enabled = false;
        }

     

        private void button_Huy_Click(object sender, EventArgs e)
        {
            resetText();
        }

        private void resetText()
        {
            textBox_MSSV.ResetText();
            textBox_HoTenTN.ResetText();
            textBox_QuanHe.ResetText();
            textBox_NgheNghiep.ResetText();
            textBox_SDT.ResetText();
            
        }
    }
}

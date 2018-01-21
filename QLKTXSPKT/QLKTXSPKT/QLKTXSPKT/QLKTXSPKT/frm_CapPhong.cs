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

namespace QLKTXSPKT
{
    public partial class frm_CapPhong : Form
    {
        public frm_CapPhong()
        {
            InitializeComponent();
        }
        public int flag { get; set; }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Kiemtra();
        }
        private void Kiemtra()
        {
            DataTable dtb = BUS_SV.DSSVChuaCapPhong(int.Parse(textBox_mssv.Text));
            if (dtb.Rows.Count > 0)
            {
                label_phongcu.Text = dtb.Rows[0]["MaPhong"].ToString();
                label_phongmoi.Text = dtb.Rows[0]["MaPhong"].ToString();
                phongcu = dtb.Rows[0]["MaPhong"].ToString();
            }
            else
            {
                label_phongcu.Text = "Chưa có thông tin phòng";
                label_phongmoi.Text = "Chưa có thông tin phòng";
            }
        }
        string phongcu = "";
        private void btn_xannhan_Click(object sender, EventArgs e)
        {
            switch (flag)
            {
                case 1:
                    BUS_SV.CapPhongSV(int.Parse(textBox_mssv.Text), comboBox_MaPhong.SelectedItem.ToString());
                    Kiemtra();
                    break;
                case 2:
                    BUS_SV.ChuyenPhongSV(int.Parse(textBox_mssv.Text), comboBox_MaPhong.SelectedItem.ToString());
                    DataTable dtb = BUS_SV.DSSVChuaCapPhong(int.Parse(textBox_mssv.Text));
                    if (dtb.Rows.Count > 0)
                    {
                        label_phongcu.Text = phongcu;
                        label_phongmoi.Text = dtb.Rows[0]["MaPhong"].ToString();
                    }
                    break;
            }
        }
        public string gioitinh = "";
        private void LoadPhong()
        {
            DataTable dtb = BUS_SV.LoasDSMaPhongTrong(gioitinh);
            comboBox_MaPhong.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    comboBox_MaPhong.Items.Add(dtb.Rows[i]["MaPhong"].ToString());
        }
        private void frm_CapPhong_Load(object sender, EventArgs e)
        {
            LoadPhong();
        }
    }
}

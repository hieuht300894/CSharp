using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BUS;

namespace QLKTXSPKT
{
    public partial class UserControl_ThemDNLanDau : UserControl
    {
        public UserControl_ThemDNLanDau()
        {
            InitializeComponent();
        }
        private void button_tailai_Click(object sender, EventArgs e)
        {
            LoadDS();
        }

        private void LoadDS()
        {
            DataTable dtb = BUS_DIENNUOC.DSPhongChuaThemChiSoLanDau();
            cbx_MaPhong.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    cbx_MaPhong.Items.Add(dtb.Rows[i]["MaPhong"].ToString());
            label_solgphong.Text = dtb.Rows.Count.ToString() + " phòng";
        }

        private void button_hoanThanhdn_Click(object sender, EventArgs e)
        {

            if (cbx_MaPhong.SelectedItem != null)
            {
                BUS_DIENNUOC.ThemChiSoLanDau(
                    cbx_MaPhong.SelectedItem.ToString(),
                    dateTimePicker1.Value,
                    int.Parse(textBox_SDCuoi.Text),
                    int.Parse(textBox_SNCuoi.Text));
                LoadDS();
                Xoa();
            }
        }
        private void Xoa()
        {
            textBox_SDCuoi.ResetText();
            textBox_SNCuoi.ResetText();
        }

        private void button_huyThaoTac_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void textBox_SDCuoi_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_SDCuoi.Text) == true)
                button_hoanThanhdn.Enabled = true;
            else
                button_hoanThanhdn.Enabled = false;
        }

        private void textBox_SNCuoi_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(textBox_SNCuoi.Text) == true)
                button_hoanThanhdn.Enabled = true;
            else
                button_hoanThanhdn.Enabled = false;
        }
    }
}

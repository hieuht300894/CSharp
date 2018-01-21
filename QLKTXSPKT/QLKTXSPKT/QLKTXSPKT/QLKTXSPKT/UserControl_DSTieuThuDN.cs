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
    public partial class UserControl_DSTieuThuDN : UserControl
    {
        public UserControl_DSTieuThuDN()
        {
            InitializeComponent();
        }

        private void button_xem_Click(object sender, EventArgs e)
        {
            DataTable dtb = BUS_DIENNUOC.TongHopSoTienTheoNgay(dateTimePicker1.Value, dateTimePicker2.Value);
            if (dtb.Rows.Count == 0)
                label_SoTien.Text = "Không có dữ liệu";
            else
            {
                label_SoTien.Text = dtb.Rows[0]["SoTien"].ToString() + ".00 đồng";
                label_sodien.Text = dtb.Rows[0]["SoDien"].ToString() + " kW";
                label_sonuoc.Text = dtb.Rows[0]["SoNuoc"].ToString() + " m3";
            }

        }

        private void label_SoTien_Click(object sender, EventArgs e)
        {
            LoadTongTien();
        }

        private void LoadTongTien()
        {
            DataTable dtb = BUS_DIENNUOC.XemTongTienTheoNgay(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
        }
        bool khoa = false;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            if (khoa == false)
            {
                DataTable dtb = BUS_DIENNUOC.ChiTietDNCuaPhong(
                    dataGridView1.Rows[r].Cells["MaPhong"].Value.ToString(),
                    dateTimePicker1.Value,
                    dateTimePicker2.Value);
                dataGridView1.DataSource = dtb;
                dataGridView1.AutoResizeColumns();
                khoa = true;
            }
        }

    

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                khoa = false;
                LoadTongTien();
            }
        }
    }
}

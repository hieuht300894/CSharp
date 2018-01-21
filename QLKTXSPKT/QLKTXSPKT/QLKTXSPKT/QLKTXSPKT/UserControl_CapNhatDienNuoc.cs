using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using System.Data.SqlClient;

namespace QLKTXSPKT
{
    public partial class UserControl_CapNhatDienNuoc : UserControl
    {
        public UserControl_CapNhatDienNuoc()
        {
            InitializeComponent();
        }
       
        public frmKTX MainForm
        {
            get
            {
                var parent = Parent;
                while (parent != null && (parent as frmKTX) == null)
                {
                    parent = parent.Parent;
                }
                return parent as frmKTX;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadDS();
        }

        private void button_hoanThanhdn_Click(object sender, EventArgs e)
        {
            if (cbx_MaPhong.SelectedItem != null)
            {

                int tinhtrang = 0;
                if (radioButton_chuadong.Checked)
                    tinhtrang = 0;
                else
                    tinhtrang = 1;
                BUS_DIENNUOC.CapNhatDienNuoc(
                cbx_MaPhong.SelectedItem.ToString(),
                dateTimePicker1.Value,
                int.Parse(textBox_SDCuoi.Text),
                int.Parse(textBox_SNCuoi.Text), tinhtrang);
                LoadDS();
                Xoa();
            }
        }

        private void LoadDS()
        {
            DataTable dtb = BUS_DIENNUOC.DSPhongChuaCapNhatDNTrongThang(dateTimePicker1.Value);
            cbx_MaPhong.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    cbx_MaPhong.Items.Add(dtb.Rows[i]["MaPhong"].ToString());
            dataGridView1.DataSource = dtb;
        }
        private void button_tailai_Click(object sender, EventArgs e)
        {
            LoadDS();
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

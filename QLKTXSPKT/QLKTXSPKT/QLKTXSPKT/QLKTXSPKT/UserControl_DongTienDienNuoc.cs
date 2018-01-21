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

namespace QLKTXSPKT
{
    public partial class UserControl_DongTienDienNuoc : UserControl
    {
        public UserControl_DongTienDienNuoc()
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
        private void LoadDS()
        {
            DataTable dtb = BUS_DIENNUOC.DSPhongChuaDongTien(dateTimePicker1.Value);
            cbx_MaPhong.Items.Clear();
            if (dtb.Rows.Count > 0)
                for (int i = 0; i < dtb.Rows.Count; i++)
                    cbx_MaPhong.Items.Add(dtb.Rows[i]["MaPhong"].ToString());
            dataGridView1.DataSource = dtb;
        }

        private void button_xacnhan_Click(object sender, EventArgs e)
        {
            if (cbx_MaPhong.SelectedItem != null)
                BUS_DIENNUOC.DongTienDienNuoc(
                    cbx_MaPhong.SelectedItem.ToString(),
                    dateTimePicker1.Value);
            LoadDS();
        }

        private void cbx_MaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                if (cbx_MaPhong.SelectedItem.ToString() == dataGridView1.Rows[i].Cells["MaPhong"].Value.ToString())
                {
                    label_thanhtien.Text = dataGridView1.Rows[i].Cells["TongTien"].Value.ToString();
                    break;
                }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadDS();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDS();
        }
    }
}

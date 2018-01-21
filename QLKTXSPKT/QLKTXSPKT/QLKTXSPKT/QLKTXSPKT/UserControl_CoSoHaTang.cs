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
    public partial class UserControl_CoSoHaTang : UserControl
    {
        public UserControl_CoSoHaTang()
        {
            InitializeComponent();
        }
        private void LoadDSCoSo()
        {
            DataTable dtb = BUS_CSHT.DSCoSo();
            dataGridView_coso.DataSource = dtb;
            dataGridView_coso.AutoResizeColumns();
        }

        private void button_DSCoso_Click(object sender, EventArgs e)
        {
            LoadDSCoSo();
        }

        private void dataGridView_coso_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtb = BUS_CSHT.DSTang(dataGridView_coso.Rows[dataGridView_coso.CurrentCell.RowIndex].Cells["MaCoSo"].Value.ToString());
            dataGridView_tang.DataSource = dtb;
            dataGridView_tang.AutoResizeColumns();
        }

        int rowID = -1;
        private void dataGridView_tang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowID = dataGridView_tang.CurrentCell.RowIndex;
            if (radioButton_phongtrong.Checked)
                LoadDSPhong(0);
            if (radioButton_phongconguoi.Checked)
                LoadDSPhong(1);
            if (radioButton_tatca.Checked)
                LoadDSPhong(2);
        }

        private void UserControl_CoSoHaTang_Load(object sender, EventArgs e)
        {
        }

        private void LoadDSPhong(int ID)
        {
            if (rowID != -1)
            {
                DataTable dtb = BUS_CSHT.DSPhong(dataGridView_tang.Rows[rowID].Cells["MaTang"].Value.ToString(), ID);
                dataGridView_phong.DataSource = dtb;
                dataGridView_phong.AutoResizeColumns();
            }
        }
        private void radioButton_phongtrong_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_phongtrong.Checked)
                LoadDSPhong(0);
        }

        private void radioButton_phongconguoi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_phongconguoi.Checked)
                LoadDSPhong(1);
        }

        private void radioButton_tatca_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_tatca.Checked)
                LoadDSPhong(2);
        }
    }
}
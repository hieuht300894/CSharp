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
using DAO;

namespace QLKTXSPKT
{
    public partial class urc_thietbi : UserControl
    {
        public urc_thietbi()
        {
            InitializeComponent();
            Chia();
        }
        private void Chia()
        {
            if (DATA.chucnang4[1] == 1)
            {
                btn_them.Enabled=true;
            }
            else
            {
                btn_them.Enabled = false;
            }
            if (DATA.chucnang4[2] == 1)
            {
                btn_sua.Enabled = true;
            }
            else
            {
                btn_sua.Enabled = false;
            }
        }

        int flag = 0;
        string tenTB;
        private void DongMoChucNang(bool[] A)
        {
            btn_them.Enabled = A[0];
            btn_sua.Enabled = A[1];
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            resetText();
            flag = 1;
            groupBox_thietbi.Visible = true;
            lbl_Tieude.Text = "Thêm Thiết Bị";
            bool[] A = new bool[2] { false, true };
            mo();
            DongMoChucNang(A);
        }
        private void btn_sua_Click(object sender, EventArgs e)
        {
            resetText();
            flag = 2;
            groupBox_thietbi.Visible = true;
            mo();
            lbl_Tieude.Text = "Sửa Thiết Bị";
            bool[] A = new bool[2] { true, false };
            DongMoChucNang(A);
            txb_tenthietbi.Enabled = false;
            gridView1.RowCellClick += gridView1_RowCellClick;
        }
        private void btn_huy_Click(object sender, EventArgs e)
        {
            resetText();
        }
        private void btn_xacnhan_Click(object sender, EventArgs e)
        {
            string tentb = txb_tenthietbi.Text;
            int dongia = int.Parse(txb_dongia.Text);
            switch (flag)
            {
                case 1:
                    BUS_TB.ThemThongTinTB(tentb, dongia);

                    gridView1.Columns.Clear();//xoa nhung cot da co san 
                    gridControl1.DataSource = BUS_TB.DSThietBi();
                    resetText();
                    break;
                case 2:
                    BUS_TB.SuaThongTinTB(tentb, dongia);
                    gridView1.Columns.Clear();//xoa nhung cot da co san 
                    gridControl1.DataSource = BUS_TB.DSThietBi();
                    resetText();
                    break;
                default:
                    break;
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            tenTB = gridView1.GetRowCellValue(e.RowHandle, "TenTB").ToString();
            DataTable dt = BUS_TB.ChiTietTB(tenTB);
            if (e.Clicks == 1)
            {
                txb_tenthietbi.Text = tenTB;
                txb_dongia.Text=dt.Rows[0][1].ToString();
            }
           
        }
        public void resetText()
        {
            txb_dongia.ResetText();
            txb_tenthietbi.ResetText();
        }
        public void mo()
        {
            txb_tenthietbi.Enabled = true;
            txb_dongia.Enabled = true;
            btn_huy.Enabled = true;
            btn_xacnhan.Enabled = true;
        }
        public void dong()
        {
            txb_dongia.Enabled = false;
            txb_tenthietbi.Enabled = false;
            btn_huy.Enabled = false;
            btn_xacnhan.Enabled = false;
        }

        private void txb_dongia_TextChanged(object sender, EventArgs e)
        {
            if (XuLyDuLieu.IsNumber(txb_dongia.Text) == true)
                btn_xacnhan.Enabled = true;
            else
                btn_xacnhan.Enabled = false;
        }    
    }
}

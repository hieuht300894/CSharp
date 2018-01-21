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
    public partial class UserControl_DoiMatKhau : UserControl
    {
        public UserControl_DoiMatKhau()
        {
            InitializeComponent();
        }
        public frmKTX frm
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
        public static string Username = "";
        private void button_hoanthanh_Click(object sender, EventArgs e)
        {
            if (textBox_pass.Text == textBox_repass.Text)
            {
                BUS_CHUCVU.DoiMatKhau(Username, XuLyDuLieu.MD5(textBox_pass.Text));
                textBox_pass.ResetText();
                textBox_repass.ResetText();
                frm.label_thongbao.Visible = false;
                frm.barButtonItem_DangNhap.Enabled = true;
                frm.barButtonItem_DangXuat.Enabled = false;
                frm.TatChucNang();
                frm.grp_ThongTin.Controls.Clear();
            }
            else
                label_thongbao.Text = "Mật khẩu không trùng khớp";
        }
    }
}

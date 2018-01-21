using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lookcreen
{
    public partial class frmMain : Form
    {
        #region Variable
        private bool isLoginSuccess = false;
        private Point startPoint, endPoint;

        #endregion

        #region Form
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            startPoint = new Point(this.Location.X, this.Location.Y);
            endPoint = new Point(this.Location.X + this.Width, this.Location.Y + this.Height);
            notifyIcon1.Text = "Unlock";
            notifyIcon1.Visible = false;
            timer1.Enabled = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    Login();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lock();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            HideForm();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (e.CloseReason == CloseReason.UserClosing && !isLoginSuccess);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isLoginSuccess)
            {
                Point curPoint = Cursor.Position;
                if (curPoint.X < startPoint.X || curPoint.Y < startPoint.Y || curPoint.X > endPoint.X || curPoint.Y > endPoint.Y)
                {
                    Cursor.Position = startPoint;
                }
            }
        }
        #endregion

        #region Methods
        private void HideForm()
        {
            //this.Hide();
            //notifyIcon1.Visible = true;
            //isLoginSuccess = false;
            //timer1.Enabled = true;
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            txtPassword.ResetText();
            isLoginSuccess = false;
            lbMessage.Text = "Please enter your password";
            notifyIcon1.Visible = false;
        }

        private void Login()
        {
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                isLoginSuccess = false;
                lbMessage.Text = "Password is not blank";
            }
            else
            {
                if (txtPassword.Text.Equals(Properties.Settings.Default.Password))
                {
                    lbMessage.Text = "Password is correct";
                    isLoginSuccess = true;
                    Application.Exit();
                }
                else
                {
                    isLoginSuccess = false;
                    lbMessage.Text = "Password is incorrect";
                }
            }
        }

        private void Logout()
        {
            isLoginSuccess = false;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void ChangePassword()
        {
            using (frmChangePassword frm = new frmChangePassword())
            {
                frm.OldPassword = isLoginSuccess ? txtPassword.Text : string.Empty;
                frm.ShowDialog();
            }
        }

        private void Lock()
        {
            //HideForm();

            isLoginSuccess = false;
            timer1.Enabled = true;
        }
        #endregion

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}

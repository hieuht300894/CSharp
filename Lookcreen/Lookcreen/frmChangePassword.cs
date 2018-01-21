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
    public partial class frmChangePassword : Form
    {

        #region Variable
        private string oldPassword, newPassword, renewPassword;

        public string RenewPassword
        {
            get { return renewPassword; }
            set { renewPassword = value; }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; }
        }

        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value; }
        }
        #endregion

        #region Form
        public frmChangePassword()
        {
            InitializeComponent();
        }
        #endregion  

        #region Events
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtOld.ReadOnly = !string.IsNullOrEmpty(OldPassword);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btChange_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region Methods
        private void SaveData()
        {
            bool chk = true;
            if (string.IsNullOrEmpty(txtOld.Text) || string.IsNullOrEmpty(txtNew.Text) || string.IsNullOrEmpty(txtReNew.Text))
            {
                chk = false;
                MessageBox.Show("Password is not blank", "Message");
                return;
            }

            if (!Properties.Settings.Default.Password.Equals(txtOld.Text))
            {
                chk = false;
                MessageBox.Show("Old password incorrect", "Message");
                return;
            }
            if (!txtNew.Text.Equals(txtReNew.Text))
            {
                chk = false;
                MessageBox.Show("New password and confirm incorrect", "Message");
                return;
            }

            if (chk)
            {
                Properties.Settings.Default.Password = txtNew.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("Password has changed", "Message");
                this.Close();
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RenameFiles
{
    public partial class Form1 : Form
    {
        List<string> lstTargets;//Folder
        List<string> lstfilesFilterExt;//File filter extension
        List<int> lstIndexCheck;

        int iTimerStart = 0;
        int iTimerEnd = 0;
        int action = 0;//0:Load, 1:Rename, 2:Delete

        public Form1()
        {
            InitializeComponent();

            lstfilesFilterExt = new List<string>();
            lstIndexCheck = new List<int>();
            lstTargets = new List<string>();
        }

        private void ProcessDirectory()
        {
            try
            {
                lstIndexCheck.Clear();
                checkedListBox1.Items.Clear();
                richTextBox1.ResetText();
                chkCheckAll.Checked = false;
                iTimerStart = 0;
                action = 0;
                timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Path not exist.", "Message");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case 0:
                    LoadFiles();
                    break;
                case 1:
                    RenameFiles();
                    break;
                case 2:
                    DeleteFiles();
                    break;
            }
        }

        private void ProcessFile(string pathFile)
        {
            foreach (string s in Directory.GetFiles(pathFile))
            {
                if (lstfilesFilterExt.Count > 0)
                {
                    string filename = Path.GetExtension(s);
                    if (lstfilesFilterExt.Any(x => x.Trim().Equals(filename)))
                        checkedListBox1.Items.Add(s);
                }
                else
                    checkedListBox1.Items.Add(s);
            }

            nStart.Maximum = checkedListBox1.Items.Count;
            nEnd.Maximum = checkedListBox1.Items.Count;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fl = new FolderBrowserDialog();
            fl.SelectedPath = "C:\\";//đường dẫn mặc định thư mục lúc mở ra
            fl.ShowNewFolderButton = false;
            if (fl.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fl.SelectedPath))//gọi thư mục
            {
                txtPath.Text = fl.SelectedPath;

                lstTargets.Clear();
                lstTargets.Add(txtPath.Text);

                lstfilesFilterExt.Clear();
                foreach (string s in txtFileExt.Text.Split(','))
                {
                    if (!string.IsNullOrEmpty(s))
                        lstfilesFilterExt.Add("." + s.Trim());
                }

                ProcessDirectory();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            lstTargets.Clear();
            lstTargets.Add(txtPath.Text);

            lstfilesFilterExt.Clear();
            foreach (string s in txtFileExt.Text.Split(','))
            {
                if (!string.IsNullOrEmpty(s))
                    lstfilesFilterExt.Add("." + s.Trim());
            }

            if (!string.IsNullOrEmpty(txtPath.Text))
                ProcessDirectory();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            action = 1;
            if (ValidationForm())
            {
                int start = Convert.ToInt32(nStart.Value) - 1;
                int end = Convert.ToInt32(nEnd.Value) - 1;

                if (start < 0 || end < 0)
                    return;

                string pathCopy = txtPath.Text + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                Directory.CreateDirectory(pathCopy);

                int k = 1;
                for (int i = start; i <= end; i++)
                {

                    bool chk = CopyFile(checkedListBox1.Items[lstIndexCheck[i]].ToString(), pathCopy, k);
                    if (chk)
                        checkedListBox1.Items[lstIndexCheck[i]] += " - Done";
                    else
                    {
                        checkedListBox1.Items[lstIndexCheck[i]] += " - Error. Line " + lstIndexCheck[i];
                        break;
                    }
                    k++;
                }
            }
        }

        private bool ValidationForm()
        {
            if (Convert.ToInt32(nStart.Value) > Convert.ToInt32(nEnd.Value))
                return false;
            return true;
        }

        private bool CopyFile(string fileSource, string pathDes, int index)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtExt.Text))
                    File.Copy(fileSource, pathDes + @"\" + txtName.Text + index + "." + txtExt.Text);

                if (!string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtExt.Text))
                    File.Copy(fileSource, pathDes + @"\" + txtName.Text + index + Path.GetExtension(fileSource));

                if (string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtExt.Text))
                    File.Copy(fileSource, pathDes + @"\" + Path.GetFileNameWithoutExtension(fileSource) + "." + txtExt.Text);

                if (string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtExt.Text))
                    File.Copy(fileSource, pathDes + @"\" + Path.GetFileName(fileSource));

                return true;
            }
            catch { return false; }

        }

        private void nStart_ValueChanged(object sender, EventArgs e)
        {
        }

        private void nEnd_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(nEnd.Value) > lstIndexCheck.Count)
                nEnd.Value = lstIndexCheck.Count;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                lstIndexCheck.Add(e.Index);
            else
                lstIndexCheck.Remove(e.Index);
            UpdateIndex();
        }

        private void UpdateIndex()
        {
            richTextBox1.ResetText();
            foreach (int i in lstIndexCheck)
            {
                richTextBox1.Text += "\"" + Path.GetFileName(checkedListBox1.Items[i].ToString()) + "\" ";
            }
            nEnd.Value = lstIndexCheck.Count;
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (chkCheckAll.Checked)
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                else
                    checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            action = 2;
            int start = Convert.ToInt32(nStart.Value) - 1;
            int end = Convert.ToInt32(nEnd.Value) - 1;

            if (start < 0 || end < 0)
            {
                iTimerStart = 0;
                iTimerEnd = 0;
                timer1.Enabled = false;
            }
            else
            {
                iTimerStart = start;
                iTimerEnd = end;
                timer1.Enabled = true;
            }
        }

        private void DeleteFiles()
        {
            if (iTimerStart > iTimerEnd)
            {
                timer1.Enabled = false;
                return;
            }

            bool chk = false;
            try
            {
                File.Delete(checkedListBox1.Items[lstIndexCheck[iTimerStart]].ToString());
                chk = true;
            }
            catch
            {
                chk = false;
            }

            if (chk)
                checkedListBox1.Items[lstIndexCheck[iTimerStart]] += " - Done";
            else
            {
                checkedListBox1.Items[lstIndexCheck[iTimerStart]] += " - Error. Line " + lstIndexCheck[iTimerStart];
                timer1.Enabled = false;
            }
            iTimerStart++;
        }

        private void RenameFiles()
        {
            
        }

        private void LoadFiles()
        {
            if (iTimerStart >= lstTargets.Count)
            {
                btnClick.BackColor = Color.Transparent;
                timer1.Enabled = false;
            }
            else
            {
                btnClick.BackColor = Color.Green;
                lstTargets.AddRange(Directory.GetDirectories(lstTargets[iTimerStart]));
                ProcessFile(lstTargets[iTimerStart]);
            }
            iTimerStart++;
        }
    }
}

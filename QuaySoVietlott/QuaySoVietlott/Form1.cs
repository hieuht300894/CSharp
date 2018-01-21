using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuaySoVietlott
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 45; i++)
            {
                listView1.Items.Add(new ListViewItem(new string[2] { i.ToString(), "0" }));
            }
        }

        private void LoadData()
        {
            string str = textBox2.Text.Replace("\r\n", " ");
            string[] value = str.Split(' ');
            MessageBox.Show(value.Length.ToString());

            foreach (string s in value)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (Convert.ToInt32(s) == Convert.ToInt32(item.SubItems[0].Text))
                    {
                        int curCount = Convert.ToInt32(item.SubItems[1].Text);
                        item.SubItems[1].Text = (curCount + 1).ToString();
                    }
                }
            }

            List<Number> result = new List<Number>();
            foreach (ListViewItem item in listView1.Items)
            {
                result.Add(new Number
                {
                    KeyID = Convert.ToInt32(item.SubItems[0].Text),
                    Value = Convert.ToInt32(item.SubItems[1].Text)
                });
            }
            List<Number> resultOrderBy = new List<Number>();
            resultOrderBy = result.OrderByDescending(x => x.Value).ToList();

            for (int i = 0; i < 6; i++)
            {
                textBox1.Text += resultOrderBy[i].KeyID + " ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
    class Number
    {
        public int KeyID { get; set; }
        public int Value { get; set; }
    }
}


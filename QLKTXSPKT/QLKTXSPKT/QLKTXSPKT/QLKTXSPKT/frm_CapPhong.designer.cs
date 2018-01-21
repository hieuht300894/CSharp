namespace QLKTXSPKT
{
    partial class frm_CapPhong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_xannhan = new System.Windows.Forms.Button();
            this.comboBox_MaPhong = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_mssv = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_phongmoi = new System.Windows.Forms.Label();
            this.label_phongcu = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_phongcu);
            this.panel1.Controls.Add(this.label_phongmoi);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btn_xannhan);
            this.panel1.Controls.Add(this.comboBox_MaPhong);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_mssv);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 247);
            this.panel1.TabIndex = 1;
            // 
            // btn_xannhan
            // 
            this.btn_xannhan.Location = new System.Drawing.Point(301, 108);
            this.btn_xannhan.Name = "btn_xannhan";
            this.btn_xannhan.Size = new System.Drawing.Size(125, 32);
            this.btn_xannhan.TabIndex = 5;
            this.btn_xannhan.Text = "Hoàn thành";
            this.btn_xannhan.UseVisualStyleBackColor = true;
            this.btn_xannhan.Click += new System.EventHandler(this.btn_xannhan_Click);
            // 
            // comboBox_MaPhong
            // 
            this.comboBox_MaPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MaPhong.FormattingEnabled = true;
            this.comboBox_MaPhong.Location = new System.Drawing.Point(122, 67);
            this.comboBox_MaPhong.Name = "comboBox_MaPhong";
            this.comboBox_MaPhong.Size = new System.Drawing.Size(304, 24);
            this.comboBox_MaPhong.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Phòng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "MSSV:";
            // 
            // textBox_mssv
            // 
            this.textBox_mssv.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_mssv.Location = new System.Drawing.Point(122, 26);
            this.textBox_mssv.Name = "textBox_mssv";
            this.textBox_mssv.ReadOnly = true;
            this.textBox_mssv.Size = new System.Drawing.Size(304, 23);
            this.textBox_mssv.TabIndex = 0;
            this.textBox_mssv.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Phòng cũ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Phòng mới";
            // 
            // label_phongmoi
            // 
            this.label_phongmoi.AutoSize = true;
            this.label_phongmoi.Location = new System.Drawing.Point(156, 193);
            this.label_phongmoi.Name = "label_phongmoi";
            this.label_phongmoi.Size = new System.Drawing.Size(46, 17);
            this.label_phongmoi.TabIndex = 6;
            this.label_phongmoi.Text = "label3";
            // 
            // label_phongcu
            // 
            this.label_phongcu.AutoSize = true;
            this.label_phongcu.Location = new System.Drawing.Point(156, 154);
            this.label_phongcu.Name = "label_phongcu";
            this.label_phongcu.Size = new System.Drawing.Size(46, 17);
            this.label_phongcu.TabIndex = 6;
            this.label_phongcu.Text = "label3";
            // 
            // frm_CapPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 275);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CapPhong";
            this.Text = "Cấp phòng";
            this.Load += new System.EventHandler(this.frm_CapPhong_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_xannhan;
        public System.Windows.Forms.TextBox textBox_mssv;
        public System.Windows.Forms.ComboBox comboBox_MaPhong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label_phongcu;
        public System.Windows.Forms.Label label_phongmoi;

    }
}
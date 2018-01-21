namespace QLKTXSPKT
{
    partial class urc_dstb
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_Phong2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_thongbao = new System.Windows.Forms.Label();
            this.lbl_Suasinhvien = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_HuyThaoTac = new System.Windows.Forms.Button();
            this.comboBox_TenTB = new System.Windows.Forms.ComboBox();
            this.button_HoanThanh = new System.Windows.Forms.Button();
            this.textBox_Mota = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_SoLuong = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_MaPhong = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Location = new System.Drawing.Point(21, 112);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(655, 244);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // btn_sua
            // 
            this.btn_sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sua.Location = new System.Drawing.Point(530, 63);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(122, 28);
            this.btn_sua.TabIndex = 5;
            this.btn_sua.Text = "Thêm TB Phòng";
            this.btn_sua.UseVisualStyleBackColor = true;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_them
            // 
            this.btn_them.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_them.Location = new System.Drawing.Point(550, 16);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(102, 31);
            this.btn_them.TabIndex = 4;
            this.btn_them.Text = "Thêm ";
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_Phong2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl_thongbao);
            this.groupBox1.Controls.Add(this.lbl_Suasinhvien);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button_HuyThaoTac);
            this.groupBox1.Controls.Add(this.comboBox_TenTB);
            this.groupBox1.Controls.Add(this.button_HoanThanh);
            this.groupBox1.Controls.Add(this.textBox_Mota);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox_SoLuong);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(705, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 279);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // comboBox_Phong2
            // 
            this.comboBox_Phong2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Phong2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Phong2.FormattingEnabled = true;
            this.comboBox_Phong2.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.comboBox_Phong2.Location = new System.Drawing.Point(108, 59);
            this.comboBox_Phong2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_Phong2.Name = "comboBox_Phong2";
            this.comboBox_Phong2.Size = new System.Drawing.Size(246, 23);
            this.comboBox_Phong2.TabIndex = 87;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(19, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 86;
            this.label1.Text = "Phòng:";
            // 
            // lbl_thongbao
            // 
            this.lbl_thongbao.AutoSize = true;
            this.lbl_thongbao.ForeColor = System.Drawing.Color.Red;
            this.lbl_thongbao.Location = new System.Drawing.Point(113, 186);
            this.lbl_thongbao.Name = "lbl_thongbao";
            this.lbl_thongbao.Size = new System.Drawing.Size(0, 13);
            this.lbl_thongbao.TabIndex = 85;
            // 
            // lbl_Suasinhvien
            // 
            this.lbl_Suasinhvien.AutoSize = true;
            this.lbl_Suasinhvien.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Suasinhvien.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_Suasinhvien.Location = new System.Drawing.Point(128, 16);
            this.lbl_Suasinhvien.Name = "lbl_Suasinhvien";
            this.lbl_Suasinhvien.Size = new System.Drawing.Size(0, 13);
            this.lbl_Suasinhvien.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 69;
            this.label4.Tag = "";
            this.label4.Text = "Tên Thiết Bị:";
            // 
            // button_HuyThaoTac
            // 
            this.button_HuyThaoTac.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_HuyThaoTac.Location = new System.Drawing.Point(154, 205);
            this.button_HuyThaoTac.Margin = new System.Windows.Forms.Padding(2);
            this.button_HuyThaoTac.Name = "button_HuyThaoTac";
            this.button_HuyThaoTac.Size = new System.Drawing.Size(76, 32);
            this.button_HuyThaoTac.TabIndex = 84;
            this.button_HuyThaoTac.Text = "Hủy";
            this.button_HuyThaoTac.UseVisualStyleBackColor = true;
            this.button_HuyThaoTac.Click += new System.EventHandler(this.button_HuyThaoTac_Click);
            // 
            // comboBox_TenTB
            // 
            this.comboBox_TenTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TenTB.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_TenTB.FormattingEnabled = true;
            this.comboBox_TenTB.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.comboBox_TenTB.Location = new System.Drawing.Point(108, 90);
            this.comboBox_TenTB.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_TenTB.Name = "comboBox_TenTB";
            this.comboBox_TenTB.Size = new System.Drawing.Size(246, 23);
            this.comboBox_TenTB.TabIndex = 70;
            // 
            // button_HoanThanh
            // 
            this.button_HoanThanh.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_HoanThanh.Location = new System.Drawing.Point(245, 205);
            this.button_HoanThanh.Margin = new System.Windows.Forms.Padding(2);
            this.button_HoanThanh.Name = "button_HoanThanh";
            this.button_HoanThanh.Size = new System.Drawing.Size(109, 32);
            this.button_HoanThanh.TabIndex = 83;
            this.button_HoanThanh.Text = "Hoàn Thành";
            this.button_HoanThanh.UseVisualStyleBackColor = true;
            this.button_HoanThanh.Click += new System.EventHandler(this.button_HoanThanh_Click);
            // 
            // textBox_Mota
            // 
            this.textBox_Mota.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Mota.Location = new System.Drawing.Point(108, 150);
            this.textBox_Mota.MaxLength = 11;
            this.textBox_Mota.Name = "textBox_Mota";
            this.textBox_Mota.Size = new System.Drawing.Size(246, 22);
            this.textBox_Mota.TabIndex = 80;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 79;
            this.label9.Text = "Mô Tả:";
            // 
            // textBox_SoLuong
            // 
            this.textBox_SoLuong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SoLuong.Location = new System.Drawing.Point(108, 122);
            this.textBox_SoLuong.Name = "textBox_SoLuong";
            this.textBox_SoLuong.Size = new System.Drawing.Size(246, 22);
            this.textBox_SoLuong.TabIndex = 78;
            this.textBox_SoLuong.TextChanged += new System.EventHandler(this.textBox_SoLuong_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 15);
            this.label8.TabIndex = 77;
            this.label8.Text = "Số Lượng: ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_MaPhong);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_them);
            this.panel1.Controls.Add(this.btn_sua);
            this.panel1.Location = new System.Drawing.Point(21, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 91);
            this.panel1.TabIndex = 92;
            // 
            // comboBox_MaPhong
            // 
            this.comboBox_MaPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MaPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_MaPhong.FormattingEnabled = true;
            this.comboBox_MaPhong.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.comboBox_MaPhong.Location = new System.Drawing.Point(91, 18);
            this.comboBox_MaPhong.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_MaPhong.Name = "comboBox_MaPhong";
            this.comboBox_MaPhong.Size = new System.Drawing.Size(273, 27);
            this.comboBox_MaPhong.TabIndex = 71;
            this.comboBox_MaPhong.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(21, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Phòng:";
            // 
            // urc_dstb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridControl1);
            this.Name = "urc_dstb";
            this.Size = new System.Drawing.Size(1100, 380);
            this.Load += new System.EventHandler(this.urc_dstb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_Suasinhvien;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button button_HuyThaoTac;
        public System.Windows.Forms.ComboBox comboBox_TenTB;
        public System.Windows.Forms.Button button_HoanThanh;
        public System.Windows.Forms.TextBox textBox_Mota;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBox_SoLuong;
        public System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_MaPhong;
        private System.Windows.Forms.Label lbl_thongbao;
        public System.Windows.Forms.ComboBox comboBox_Phong2;
        private System.Windows.Forms.Label label1;
    }
}

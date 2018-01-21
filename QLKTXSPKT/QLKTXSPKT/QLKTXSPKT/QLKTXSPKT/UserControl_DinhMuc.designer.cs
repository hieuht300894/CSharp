namespace QLKTXSPKT
{
    partial class UserControl_DinhMuc
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_maDinhMuc = new System.Windows.Forms.ComboBox();
            this.label_TieuDe = new System.Windows.Forms.Label();
            this.dataGridView_thongtin = new System.Windows.Forms.DataGridView();
            this.button_huy = new System.Windows.Forms.Button();
            this.button_hoanThanh = new System.Windows.Forms.Button();
            this.textBox_SoTien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Sua = new System.Windows.Forms.Button();
            this.textBox_MaDM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ghichu = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_thongtin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn định mức";
            // 
            // cbx_maDinhMuc
            // 
            this.cbx_maDinhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_maDinhMuc.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_maDinhMuc.FormattingEnabled = true;
            this.cbx_maDinhMuc.Items.AddRange(new object[] {
            "Điện",
            "Nước"});
            this.cbx_maDinhMuc.Location = new System.Drawing.Point(110, 62);
            this.cbx_maDinhMuc.Name = "cbx_maDinhMuc";
            this.cbx_maDinhMuc.Size = new System.Drawing.Size(121, 23);
            this.cbx_maDinhMuc.TabIndex = 1;
            this.cbx_maDinhMuc.SelectedIndexChanged += new System.EventHandler(this.cbx_maDinhMuc_SelectedIndexChanged);
            // 
            // label_TieuDe
            // 
            this.label_TieuDe.AutoSize = true;
            this.label_TieuDe.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TieuDe.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_TieuDe.Location = new System.Drawing.Point(13, 16);
            this.label_TieuDe.Name = "label_TieuDe";
            this.label_TieuDe.Size = new System.Drawing.Size(346, 31);
            this.label_TieuDe.TabIndex = 8;
            this.label_TieuDe.Text = "Thông tin định mức điện nước";
            // 
            // dataGridView_thongtin
            // 
            this.dataGridView_thongtin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_thongtin.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_thongtin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_thongtin.Location = new System.Drawing.Point(19, 102);
            this.dataGridView_thongtin.Name = "dataGridView_thongtin";
            this.dataGridView_thongtin.ReadOnly = true;
            this.dataGridView_thongtin.Size = new System.Drawing.Size(447, 262);
            this.dataGridView_thongtin.TabIndex = 9;
            this.dataGridView_thongtin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_thongtin_CellClick);
            // 
            // button_huy
            // 
            this.button_huy.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_huy.Location = new System.Drawing.Point(796, 211);
            this.button_huy.Name = "button_huy";
            this.button_huy.Size = new System.Drawing.Size(70, 35);
            this.button_huy.TabIndex = 15;
            this.button_huy.Text = "Hủy bỏ";
            this.button_huy.UseVisualStyleBackColor = true;
            this.button_huy.Click += new System.EventHandler(this.button_huy_Click);
            // 
            // button_hoanThanh
            // 
            this.button_hoanThanh.Enabled = false;
            this.button_hoanThanh.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_hoanThanh.Location = new System.Drawing.Point(872, 211);
            this.button_hoanThanh.Name = "button_hoanThanh";
            this.button_hoanThanh.Size = new System.Drawing.Size(91, 35);
            this.button_hoanThanh.TabIndex = 14;
            this.button_hoanThanh.Text = "Hoàn Thành ";
            this.button_hoanThanh.UseVisualStyleBackColor = true;
            this.button_hoanThanh.Click += new System.EventHandler(this.button_hoanThanh_Click);
            // 
            // textBox_SoTien
            // 
            this.textBox_SoTien.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_SoTien.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SoTien.Location = new System.Drawing.Point(699, 134);
            this.textBox_SoTien.Name = "textBox_SoTien";
            this.textBox_SoTien.Size = new System.Drawing.Size(264, 22);
            this.textBox_SoTien.TabIndex = 13;
            this.textBox_SoTien.TextChanged += new System.EventHandler(this.textBox_SoTien_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(607, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Số Tiền";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(607, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Mã Định Mức";
            // 
            // button_Sua
            // 
            this.button_Sua.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Sua.Location = new System.Drawing.Point(472, 105);
            this.button_Sua.Name = "button_Sua";
            this.button_Sua.Size = new System.Drawing.Size(91, 35);
            this.button_Sua.TabIndex = 14;
            this.button_Sua.Text = "Sửa";
            this.button_Sua.UseVisualStyleBackColor = true;
            this.button_Sua.Click += new System.EventHandler(this.button_Sua_Click);
            // 
            // textBox_MaDM
            // 
            this.textBox_MaDM.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_MaDM.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MaDM.Location = new System.Drawing.Point(699, 102);
            this.textBox_MaDM.Name = "textBox_MaDM";
            this.textBox_MaDM.ReadOnly = true;
            this.textBox_MaDM.Size = new System.Drawing.Size(264, 22);
            this.textBox_MaDM.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(607, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ghi chú";
            // 
            // textBox_ghichu
            // 
            this.textBox_ghichu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_ghichu.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ghichu.Location = new System.Drawing.Point(699, 167);
            this.textBox_ghichu.Name = "textBox_ghichu";
            this.textBox_ghichu.ReadOnly = true;
            this.textBox_ghichu.Size = new System.Drawing.Size(264, 22);
            this.textBox_ghichu.TabIndex = 13;
            // 
            // UserControl_DinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.button_huy);
            this.Controls.Add(this.button_Sua);
            this.Controls.Add(this.button_hoanThanh);
            this.Controls.Add(this.textBox_ghichu);
            this.Controls.Add(this.textBox_MaDM);
            this.Controls.Add(this.textBox_SoTien);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView_thongtin);
            this.Controls.Add(this.label_TieuDe);
            this.Controls.Add(this.cbx_maDinhMuc);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControl_DinhMuc";
            this.Size = new System.Drawing.Size(1100, 380);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_thongtin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbx_maDinhMuc;
        public System.Windows.Forms.Label label_TieuDe;
        private System.Windows.Forms.DataGridView dataGridView_thongtin;
        private System.Windows.Forms.Button button_huy;
        private System.Windows.Forms.Button button_hoanThanh;
        private System.Windows.Forms.TextBox textBox_SoTien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Sua;
        private System.Windows.Forms.TextBox textBox_MaDM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ghichu;
    }
}

namespace QLKTXSPKT
{
    partial class UserControl_SuaTang
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_TruongTang = new System.Windows.Forms.TextBox();
            this.comboBox_MaCoSo = new System.Windows.Forms.ComboBox();
            this.radioButton_nu = new System.Windows.Forms.RadioButton();
            this.radioButton_nam = new System.Windows.Forms.RadioButton();
            this.button_HuyTang = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_HoanThanhTang = new System.Windows.Forms.Button();
            this.textBox_MaTang = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(370, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(716, 311);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_TruongTang);
            this.groupBox2.Controls.Add(this.comboBox_MaCoSo);
            this.groupBox2.Controls.Add(this.radioButton_nu);
            this.groupBox2.Controls.Add(this.radioButton_nam);
            this.groupBox2.Controls.Add(this.button_HuyTang);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button_HoanThanhTang);
            this.groupBox2.Controls.Add(this.textBox_MaTang);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 323);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sửa thông tin tầng";
            // 
            // textBox_TruongTang
            // 
            this.textBox_TruongTang.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_TruongTang.Location = new System.Drawing.Point(86, 198);
            this.textBox_TruongTang.Name = "textBox_TruongTang";
            this.textBox_TruongTang.Size = new System.Drawing.Size(240, 22);
            this.textBox_TruongTang.TabIndex = 10;
            this.textBox_TruongTang.TextChanged += new System.EventHandler(this.textBox_TruongTang_TextChanged);
            // 
            // comboBox_MaCoSo
            // 
            this.comboBox_MaCoSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MaCoSo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_MaCoSo.FormattingEnabled = true;
            this.comboBox_MaCoSo.Location = new System.Drawing.Point(86, 53);
            this.comboBox_MaCoSo.Name = "comboBox_MaCoSo";
            this.comboBox_MaCoSo.Size = new System.Drawing.Size(240, 23);
            this.comboBox_MaCoSo.TabIndex = 9;
            this.comboBox_MaCoSo.SelectedIndexChanged += new System.EventHandler(this.comboBox_MaCoSo_SelectedIndexChanged);
            // 
            // radioButton_nu
            // 
            this.radioButton_nu.AutoSize = true;
            this.radioButton_nu.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_nu.Location = new System.Drawing.Point(137, 147);
            this.radioButton_nu.Name = "radioButton_nu";
            this.radioButton_nu.Size = new System.Drawing.Size(41, 19);
            this.radioButton_nu.TabIndex = 8;
            this.radioButton_nu.Text = "Nữ";
            this.radioButton_nu.UseVisualStyleBackColor = true;
            // 
            // radioButton_nam
            // 
            this.radioButton_nam.AutoSize = true;
            this.radioButton_nam.Checked = true;
            this.radioButton_nam.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_nam.Location = new System.Drawing.Point(86, 147);
            this.radioButton_nam.Name = "radioButton_nam";
            this.radioButton_nam.Size = new System.Drawing.Size(49, 19);
            this.radioButton_nam.TabIndex = 8;
            this.radioButton_nam.TabStop = true;
            this.radioButton_nam.Text = "Nam";
            this.radioButton_nam.UseVisualStyleBackColor = true;
            // 
            // button_HuyTang
            // 
            this.button_HuyTang.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_HuyTang.Location = new System.Drawing.Point(86, 256);
            this.button_HuyTang.Name = "button_HuyTang";
            this.button_HuyTang.Size = new System.Drawing.Size(108, 37);
            this.button_HuyTang.TabIndex = 7;
            this.button_HuyTang.Text = "Hủy";
            this.button_HuyTang.UseVisualStyleBackColor = true;
            this.button_HuyTang.Click += new System.EventHandler(this.button_HuyTang_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Cơ sở";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Trưởng tầng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Loại tầng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tầng";
            // 
            // button_HoanThanhTang
            // 
            this.button_HoanThanhTang.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_HoanThanhTang.Location = new System.Drawing.Point(211, 256);
            this.button_HoanThanhTang.Name = "button_HoanThanhTang";
            this.button_HoanThanhTang.Size = new System.Drawing.Size(115, 37);
            this.button_HoanThanhTang.TabIndex = 5;
            this.button_HoanThanhTang.Text = "Hoàn Thành";
            this.button_HoanThanhTang.UseVisualStyleBackColor = true;
            this.button_HoanThanhTang.Click += new System.EventHandler(this.button_HoanThanhTang_Click);
            // 
            // textBox_MaTang
            // 
            this.textBox_MaTang.Enabled = false;
            this.textBox_MaTang.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MaTang.Location = new System.Drawing.Point(86, 102);
            this.textBox_MaTang.Name = "textBox_MaTang";
            this.textBox_MaTang.Size = new System.Drawing.Size(240, 22);
            this.textBox_MaTang.TabIndex = 3;
            // 
            // UserControl_SuaTang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Name = "UserControl_SuaTang";
            this.Size = new System.Drawing.Size(1100, 380);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_TruongTang;
        private System.Windows.Forms.ComboBox comboBox_MaCoSo;
        private System.Windows.Forms.RadioButton radioButton_nu;
        private System.Windows.Forms.RadioButton radioButton_nam;
        private System.Windows.Forms.Button button_HuyTang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_HoanThanhTang;
        private System.Windows.Forms.TextBox textBox_MaTang;
    }
}

namespace QLKTXSPKT
{
    partial class UserControl_DoiMatKhau
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_repass = new System.Windows.Forms.TextBox();
            this.button_hoanthanh = new System.Windows.Forms.Button();
            this.label_thongbao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(282, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mật khẩu mới";
            // 
            // textBox_pass
            // 
            this.textBox_pass.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_pass.Location = new System.Drawing.Point(454, 80);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.Size = new System.Drawing.Size(349, 29);
            this.textBox_pass.TabIndex = 1;
            this.textBox_pass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(282, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nhập lại mật khẩu";
            // 
            // textBox_repass
            // 
            this.textBox_repass.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_repass.Location = new System.Drawing.Point(454, 138);
            this.textBox_repass.Name = "textBox_repass";
            this.textBox_repass.Size = new System.Drawing.Size(349, 29);
            this.textBox_repass.TabIndex = 1;
            this.textBox_repass.UseSystemPasswordChar = true;
            // 
            // button_hoanthanh
            // 
            this.button_hoanthanh.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_hoanthanh.Location = new System.Drawing.Point(645, 229);
            this.button_hoanthanh.Name = "button_hoanthanh";
            this.button_hoanthanh.Size = new System.Drawing.Size(158, 49);
            this.button_hoanthanh.TabIndex = 2;
            this.button_hoanthanh.Text = "Xác nhận";
            this.button_hoanthanh.UseVisualStyleBackColor = true;
            this.button_hoanthanh.Click += new System.EventHandler(this.button_hoanthanh_Click);
            // 
            // label_thongbao
            // 
            this.label_thongbao.AutoSize = true;
            this.label_thongbao.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_thongbao.Location = new System.Drawing.Point(450, 190);
            this.label_thongbao.Name = "label_thongbao";
            this.label_thongbao.Size = new System.Drawing.Size(0, 21);
            this.label_thongbao.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Đổi mật khẩu";
            // 
            // UserControl_DoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_hoanthanh);
            this.Controls.Add(this.textBox_repass);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.label_thongbao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "UserControl_DoiMatKhau";
            this.Size = new System.Drawing.Size(1100, 390);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_repass;
        private System.Windows.Forms.Button button_hoanthanh;
        private System.Windows.Forms.Label label_thongbao;
        private System.Windows.Forms.Label label1;
    }
}

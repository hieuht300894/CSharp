namespace SQL_Tools.GUI
{
    partial class frmMain
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
            this.cbbServerName = new System.Windows.Forms.ComboBox();
            this.cbbAuth = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lbxDatabase = new System.Windows.Forms.ListBox();
            this.lbxTable = new System.Windows.Forms.ListBox();
            this.btnGenerateScripts = new System.Windows.Forms.Button();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.lbTable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbbServerName
            // 
            this.cbbServerName.FormattingEnabled = true;
            this.cbbServerName.Location = new System.Drawing.Point(97, 12);
            this.cbbServerName.Name = "cbbServerName";
            this.cbbServerName.Size = new System.Drawing.Size(200, 21);
            this.cbbServerName.TabIndex = 0;
            // 
            // cbbAuth
            // 
            this.cbbAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAuth.FormattingEnabled = true;
            this.cbbAuth.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cbbAuth.Location = new System.Drawing.Point(97, 40);
            this.cbbAuth.Name = "cbbAuth";
            this.cbbAuth.Size = new System.Drawing.Size(200, 21);
            this.cbbAuth.TabIndex = 1;
            this.cbbAuth.SelectedIndexChanged += new System.EventHandler(this.cbbAuth_SelectedIndexChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(97, 96);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Authentication:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password:";
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.Location = new System.Drawing.Point(222, 122);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnKetNoi.TabIndex = 5;
            this.btnKetNoi.Text = "Kết nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            this.btnKetNoi.Click += new System.EventHandler(this.btnKetNoi_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(97, 68);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 6;
            // 
            // lbxDatabase
            // 
            this.lbxDatabase.FormattingEnabled = true;
            this.lbxDatabase.HorizontalScrollbar = true;
            this.lbxDatabase.Location = new System.Drawing.Point(15, 151);
            this.lbxDatabase.Name = "lbxDatabase";
            this.lbxDatabase.Size = new System.Drawing.Size(130, 225);
            this.lbxDatabase.TabIndex = 7;
            // 
            // lbxTable
            // 
            this.lbxTable.FormattingEnabled = true;
            this.lbxTable.HorizontalScrollbar = true;
            this.lbxTable.Location = new System.Drawing.Point(151, 151);
            this.lbxTable.Name = "lbxTable";
            this.lbxTable.Size = new System.Drawing.Size(146, 225);
            this.lbxTable.TabIndex = 8;
            // 
            // btnGenerateScripts
            // 
            this.btnGenerateScripts.Location = new System.Drawing.Point(315, 12);
            this.btnGenerateScripts.Name = "btnGenerateScripts";
            this.btnGenerateScripts.Size = new System.Drawing.Size(100, 49);
            this.btnGenerateScripts.TabIndex = 9;
            this.btnGenerateScripts.Text = "Generate Scripts";
            this.btnGenerateScripts.UseVisualStyleBackColor = true;
            this.btnGenerateScripts.Click += new System.EventHandler(this.btnGenerateScripts_Click);
            // 
            // lbDatabase
            // 
            this.lbDatabase.AutoSize = true;
            this.lbDatabase.Location = new System.Drawing.Point(12, 389);
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(35, 13);
            this.lbDatabase.TabIndex = 10;
            this.lbDatabase.Text = "label5";
            // 
            // lbTable
            // 
            this.lbTable.AutoSize = true;
            this.lbTable.Location = new System.Drawing.Point(148, 389);
            this.lbTable.Name = "lbTable";
            this.lbTable.Size = new System.Drawing.Size(35, 13);
            this.lbTable.TabIndex = 10;
            this.lbTable.Text = "label5";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.lbTable);
            this.Controls.Add(this.lbDatabase);
            this.Controls.Add(this.btnGenerateScripts);
            this.Controls.Add(this.lbxTable);
            this.Controls.Add(this.lbxDatabase);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnKetNoi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cbbAuth);
            this.Controls.Add(this.cbbServerName);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbServerName;
        private System.Windows.Forms.ComboBox cbbAuth;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnKetNoi;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ListBox lbxDatabase;
        private System.Windows.Forms.ListBox lbxTable;
        private System.Windows.Forms.Button btnGenerateScripts;
        private System.Windows.Forms.Label lbDatabase;
        private System.Windows.Forms.Label lbTable;
    }
}
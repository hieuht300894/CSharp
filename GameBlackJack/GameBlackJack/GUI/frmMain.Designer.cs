namespace GameBlackJack
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
            this.mnMain = new System.Windows.Forms.MenuStrip();
            this.mnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnClientConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnServerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tpClient = new System.Windows.Forms.TableLayoutPanel();
            this.tpBanker = new System.Windows.Forms.TableLayoutPanel();
            this.mnMain.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnMain
            // 
            this.mnMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSetting});
            this.mnMain.Location = new System.Drawing.Point(0, 0);
            this.mnMain.Name = "mnMain";
            this.mnMain.Size = new System.Drawing.Size(884, 24);
            this.mnMain.TabIndex = 0;
            this.mnMain.Text = "menuStrip1";
            // 
            // mnSetting
            // 
            this.mnSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnClientConfig,
            this.mnServerConfig});
            this.mnSetting.Name = "mnSetting";
            this.mnSetting.Size = new System.Drawing.Size(56, 20);
            this.mnSetting.Text = "Setting";
            // 
            // mnClientConfig
            // 
            this.mnClientConfig.Name = "mnClientConfig";
            this.mnClientConfig.Size = new System.Drawing.Size(181, 22);
            this.mnClientConfig.Text = "Client configuration";
            // 
            // mnServerConfig
            // 
            this.mnServerConfig.Name = "mnServerConfig";
            this.mnServerConfig.Size = new System.Drawing.Size(181, 22);
            this.mnServerConfig.Text = "Server configuration";
            // 
            // tpMain
            // 
            this.tpMain.ColumnCount = 3;
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpMain.Controls.Add(this.tpClient, 1, 2);
            this.tpMain.Controls.Add(this.tpBanker, 1, 1);
            this.tpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMain.Location = new System.Drawing.Point(0, 24);
            this.tpMain.Name = "tpMain";
            this.tpMain.RowCount = 4;
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpMain.Size = new System.Drawing.Size(884, 387);
            this.tpMain.TabIndex = 1;
            // 
            // tpClient
            // 
            this.tpClient.AutoSize = true;
            this.tpClient.ColumnCount = 1;
            this.tpClient.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tpClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpClient.Location = new System.Drawing.Point(23, 196);
            this.tpClient.Name = "tpClient";
            this.tpClient.RowCount = 1;
            this.tpClient.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpClient.Size = new System.Drawing.Size(838, 167);
            this.tpClient.TabIndex = 0;
            // 
            // tpBanker
            // 
            this.tpBanker.AutoSize = true;
            this.tpBanker.ColumnCount = 1;
            this.tpBanker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpBanker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpBanker.Location = new System.Drawing.Point(23, 23);
            this.tpBanker.Name = "tpBanker";
            this.tpBanker.RowCount = 1;
            this.tpBanker.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpBanker.Size = new System.Drawing.Size(838, 167);
            this.tpBanker.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.tpMain);
            this.Controls.Add(this.mnMain);
            this.MainMenuStrip = this.mnMain;
            this.Name = "frmMain";
            this.Text = "Blackjack";
            this.mnMain.ResumeLayout(false);
            this.mnMain.PerformLayout();
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnMain;
        private System.Windows.Forms.ToolStripMenuItem mnSetting;
        private System.Windows.Forms.ToolStripMenuItem mnClientConfig;
        private System.Windows.Forms.ToolStripMenuItem mnServerConfig;
        private System.Windows.Forms.TableLayoutPanel tpMain;
        private System.Windows.Forms.TableLayoutPanel tpClient;
        private System.Windows.Forms.TableLayoutPanel tpBanker;
    }
}
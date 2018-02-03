namespace HostKVM.GUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnNumberClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnAddressServer = new System.Windows.Forms.ToolStripMenuItem();
            this.tpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbIPNetwork = new System.Windows.Forms.Label();
            this.tpClient = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnFile
            // 
            this.mnFile.AutoSize = false;
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnNumberClient,
            this.mnAddressServer});
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(75, 20);
            this.mnFile.Text = "Settings";
            // 
            // mnNumberClient
            // 
            this.mnNumberClient.AutoSize = false;
            this.mnNumberClient.Name = "mnNumberClient";
            this.mnNumberClient.Size = new System.Drawing.Size(152, 22);
            this.mnNumberClient.Text = "Client Configuration";
            // 
            // mnAddressServer
            // 
            this.mnAddressServer.AutoSize = false;
            this.mnAddressServer.Name = "mnAddressServer";
            this.mnAddressServer.Size = new System.Drawing.Size(152, 22);
            this.mnAddressServer.Text = "Host Configuration";
            // 
            // tpMain
            // 
            this.tpMain.ColumnCount = 2;
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tpMain.Controls.Add(this.btnStart, 1, 1);
            this.tpMain.Controls.Add(this.lbIPNetwork, 0, 1);
            this.tpMain.Controls.Add(this.tpClient, 0, 0);
            this.tpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMain.Location = new System.Drawing.Point(0, 24);
            this.tpMain.Name = "tpMain";
            this.tpMain.RowCount = 2;
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.Size = new System.Drawing.Size(884, 387);
            this.tpMain.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(781, 349);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 35);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // lbIPNetwork
            // 
            this.lbIPNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIPNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIPNetwork.Location = new System.Drawing.Point(3, 346);
            this.lbIPNetwork.Name = "lbIPNetwork";
            this.lbIPNetwork.Size = new System.Drawing.Size(772, 41);
            this.lbIPNetwork.TabIndex = 2;
            this.lbIPNetwork.Text = "IP Network";
            this.lbIPNetwork.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpClient
            // 
            this.tpClient.ColumnCount = 1;
            this.tpMain.SetColumnSpan(this.tpClient, 2);
            this.tpClient.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpClient.Location = new System.Drawing.Point(3, 3);
            this.tpClient.Name = "tpClient";
            this.tpClient.RowCount = 1;
            this.tpClient.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpClient.Size = new System.Drawing.Size(878, 340);
            this.tpClient.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.tpMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnFile;
        private System.Windows.Forms.TableLayoutPanel tpMain;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbIPNetwork;
        private System.Windows.Forms.ToolStripMenuItem mnNumberClient;
        private System.Windows.Forms.ToolStripMenuItem mnAddressServer;
        private System.Windows.Forms.TableLayoutPanel tpClient;
    }
}
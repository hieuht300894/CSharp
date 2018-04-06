namespace gamecaro
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
            this.components = new System.ComponentModel.Container();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.humanVsHumanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humanVsComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comVsHumanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCb_time = new System.Windows.Forms.ToolStripComboBox();
            this.mnEndGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOption = new System.Windows.Forms.ToolStripMenuItem();
            this.mnServerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnClientConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnHowToPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tpBoard = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.mnEdit,
            this.mnHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 30;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnNewGame,
            this.mnEndGame,
            this.mnSave,
            this.mnOpen,
            this.mnOption,
            this.mnExit});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // mnNewGame
            // 
            this.mnNewGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.humanVsHumanToolStripMenuItem,
            this.humanVsComToolStripMenuItem,
            this.comVsHumanToolStripMenuItem,
            this.toolStripCb_time});
            this.mnNewGame.Name = "mnNewGame";
            this.mnNewGame.Size = new System.Drawing.Size(150, 22);
            this.mnNewGame.Text = "New Game";
            // 
            // humanVsHumanToolStripMenuItem
            // 
            this.humanVsHumanToolStripMenuItem.Name = "humanVsHumanToolStripMenuItem";
            this.humanVsHumanToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.humanVsHumanToolStripMenuItem.Text = "Player vs Player (Key 1)";
            // 
            // humanVsComToolStripMenuItem
            // 
            this.humanVsComToolStripMenuItem.Name = "humanVsComToolStripMenuItem";
            this.humanVsComToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.humanVsComToolStripMenuItem.Text = "Player vs Com (Key 2)";
            // 
            // comVsHumanToolStripMenuItem
            // 
            this.comVsHumanToolStripMenuItem.Name = "comVsHumanToolStripMenuItem";
            this.comVsHumanToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.comVsHumanToolStripMenuItem.Text = "Com vs Player (Key 3)";
            // 
            // toolStripCb_time
            // 
            this.toolStripCb_time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripCb_time.MergeIndex = 1;
            this.toolStripCb_time.Name = "toolStripCb_time";
            this.toolStripCb_time.Size = new System.Drawing.Size(121, 23);
            this.toolStripCb_time.ToolTipText = "Time to play";
            // 
            // mnEndGame
            // 
            this.mnEndGame.Name = "mnEndGame";
            this.mnEndGame.Size = new System.Drawing.Size(150, 22);
            this.mnEndGame.Text = "End (F3)";
            // 
            // mnSave
            // 
            this.mnSave.Name = "mnSave";
            this.mnSave.Size = new System.Drawing.Size(150, 22);
            this.mnSave.Text = "Save (Ctrl + S)";
            // 
            // mnOpen
            // 
            this.mnOpen.Name = "mnOpen";
            this.mnOpen.Size = new System.Drawing.Size(150, 22);
            this.mnOpen.Text = "Open (Ctrl+O)";
            // 
            // mnOption
            // 
            this.mnOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnServerConfig,
            this.mnClientConfig});
            this.mnOption.Name = "mnOption";
            this.mnOption.Size = new System.Drawing.Size(150, 22);
            this.mnOption.Text = "Options";
            // 
            // mnServerConfig
            // 
            this.mnServerConfig.Name = "mnServerConfig";
            this.mnServerConfig.Size = new System.Drawing.Size(106, 22);
            this.mnServerConfig.Text = "Server";
            // 
            // mnClientConfig
            // 
            this.mnClientConfig.Name = "mnClientConfig";
            this.mnClientConfig.Size = new System.Drawing.Size(106, 22);
            this.mnClientConfig.Text = "Client";
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.Size = new System.Drawing.Size(150, 22);
            this.mnExit.Text = "Exit";
            // 
            // mnEdit
            // 
            this.mnEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnRedo,
            this.mnUndo});
            this.mnEdit.Name = "mnEdit";
            this.mnEdit.Size = new System.Drawing.Size(39, 20);
            this.mnEdit.Text = "Edit";
            // 
            // mnRedo
            // 
            this.mnRedo.Name = "mnRedo";
            this.mnRedo.Size = new System.Drawing.Size(154, 22);
            this.mnRedo.Text = "Redo (Ctrl + X)";
            // 
            // mnUndo
            // 
            this.mnUndo.Name = "mnUndo";
            this.mnUndo.Size = new System.Drawing.Size(154, 22);
            this.mnUndo.Text = "Undo (Ctrl + Z)";
            // 
            // mnHelp
            // 
            this.mnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnHowToPlay,
            this.mnAbout});
            this.mnHelp.Name = "mnHelp";
            this.mnHelp.Size = new System.Drawing.Size(44, 20);
            this.mnHelp.Text = "Help";
            // 
            // mnHowToPlay
            // 
            this.mnHowToPlay.Name = "mnHowToPlay";
            this.mnHowToPlay.Size = new System.Drawing.Size(161, 22);
            this.mnHowToPlay.Text = "How to play (F1)";
            // 
            // mnAbout
            // 
            this.mnAbout.Name = "mnAbout";
            this.mnAbout.Size = new System.Drawing.Size(161, 22);
            this.mnAbout.Text = "About (F2)";
            // 
            // tpMain
            // 
            this.tpMain.ColumnCount = 1;
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpMain.Controls.Add(this.tpBoard, 0, 0);
            this.tpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMain.Location = new System.Drawing.Point(0, 24);
            this.tpMain.Name = "tpMain";
            this.tpMain.RowCount = 3;
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tpMain.Size = new System.Drawing.Size(984, 437);
            this.tpMain.TabIndex = 31;
            // 
            // tpBoard
            // 
            this.tpBoard.ColumnCount = 1;
            this.tpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpBoard.Location = new System.Drawing.Point(3, 3);
            this.tpBoard.Name = "tpBoard";
            this.tpBoard.RowCount = 1;
            this.tpMain.SetRowSpan(this.tpBoard, 3);
            this.tpBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpBoard.Size = new System.Drawing.Size(978, 431);
            this.tpBoard.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.tpMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Tic Tac Toe Game";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnNewGame;
        private System.Windows.Forms.ToolStripMenuItem humanVsHumanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem humanVsComToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comVsHumanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.ToolStripMenuItem mnEdit;
        private System.Windows.Forms.ToolStripMenuItem mnRedo;
        private System.Windows.Forms.ToolStripMenuItem mnUndo;
        private System.Windows.Forms.ToolStripMenuItem mnHelp;
        private System.Windows.Forms.ToolStripMenuItem mnHowToPlay;
        private System.Windows.Forms.ToolStripMenuItem mnAbout;
        private System.Windows.Forms.ToolStripMenuItem mnEndGame;
        private System.Windows.Forms.ToolStripComboBox toolStripCb_time;
        private System.Windows.Forms.ToolStripMenuItem mnSave;
        private System.Windows.Forms.ToolStripMenuItem mnOpen;
        private System.Windows.Forms.TableLayoutPanel tpMain;
        private System.Windows.Forms.TableLayoutPanel tpBoard;
        private System.Windows.Forms.ToolStripMenuItem mnOption;
        private System.Windows.Forms.ToolStripMenuItem mnServerConfig;
        private System.Windows.Forms.ToolStripMenuItem mnClientConfig;
    }
}


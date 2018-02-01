namespace ServerApp
{
    partial class DeviceControl
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
            this.tpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lvCards = new System.Windows.Forms.ListView();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lbClientName = new System.Windows.Forms.Label();
            this.tpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tpMain
            // 
            this.tpMain.ColumnCount = 2;
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tpMain.Controls.Add(this.lvCards, 0, 0);
            this.tpMain.Controls.Add(this.picLogo, 0, 1);
            this.tpMain.Controls.Add(this.lbClientName, 0, 2);
            this.tpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMain.Location = new System.Drawing.Point(0, 0);
            this.tpMain.Name = "tpMain";
            this.tpMain.RowCount = 3;
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.Size = new System.Drawing.Size(200, 120);
            this.tpMain.TabIndex = 0;
            // 
            // lvCards
            // 
            this.tpMain.SetColumnSpan(this.lvCards, 2);
            this.lvCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCards.Location = new System.Drawing.Point(3, 3);
            this.lvCards.Name = "lvCards";
            this.lvCards.Size = new System.Drawing.Size(194, 43);
            this.lvCards.TabIndex = 0;
            this.lvCards.UseCompatibleStateImageBehavior = false;
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Location = new System.Drawing.Point(3, 52);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(64, 43);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lbClientName
            // 
            this.lbClientName.AutoSize = true;
            this.tpMain.SetColumnSpan(this.lbClientName, 2);
            this.lbClientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClientName.Location = new System.Drawing.Point(3, 101);
            this.lbClientName.Margin = new System.Windows.Forms.Padding(3);
            this.lbClientName.Name = "lbClientName";
            this.lbClientName.Size = new System.Drawing.Size(194, 16);
            this.lbClientName.TabIndex = 2;
            this.lbClientName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeviceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tpMain);
            this.Name = "DeviceControl";
            this.Size = new System.Drawing.Size(200, 120);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tpMain;
        public System.Windows.Forms.ListView lvCards;
        public System.Windows.Forms.PictureBox picLogo;
        public System.Windows.Forms.Label lbClientName;
    }
}

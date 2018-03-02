namespace HostKVM.UserControls
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
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFSN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIMEI = new System.Windows.Forms.TextBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpMain
            // 
            this.tpMain.AutoSize = true;
            this.tpMain.ColumnCount = 3;
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tpMain.Controls.Add(this.lbInfo, 0, 0);
            this.tpMain.Controls.Add(this.btnConfig, 2, 0);
            this.tpMain.Controls.Add(this.label2, 0, 1);
            this.tpMain.Controls.Add(this.txtFSN, 1, 1);
            this.tpMain.Controls.Add(this.label3, 0, 2);
            this.tpMain.Controls.Add(this.txtIMEI, 1, 2);
            this.tpMain.Controls.Add(this.lbStatus, 0, 3);
            this.tpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpMain.Location = new System.Drawing.Point(0, 0);
            this.tpMain.Name = "tpMain";
            this.tpMain.RowCount = 4;
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.Size = new System.Drawing.Size(300, 110);
            this.tpMain.TabIndex = 0;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.tpMain.SetColumnSpan(this.lbInfo, 2);
            this.lbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInfo.Location = new System.Drawing.Point(3, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(258, 26);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "Client - Not configured - OFF";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConfig
            // 
            this.btnConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfig.Location = new System.Drawing.Point(267, 3);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(30, 20);
            this.btnConfig.TabIndex = 1;
            this.btnConfig.TabStop = false;
            this.btnConfig.Text = "...";
            this.btnConfig.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "FSN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFSN
            // 
            this.txtFSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFSN.Location = new System.Drawing.Point(41, 29);
            this.txtFSN.Name = "txtFSN";
            this.txtFSN.Size = new System.Drawing.Size(220, 21);
            this.txtFSN.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "IMEI";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIMEI
            // 
            this.txtIMEI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIMEI.Location = new System.Drawing.Point(41, 56);
            this.txtIMEI.Name = "txtIMEI";
            this.txtIMEI.Size = new System.Drawing.Size(220, 21);
            this.txtIMEI.TabIndex = 5;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.tpMain.SetColumnSpan(this.lbStatus, 3);
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Location = new System.Drawing.Point(0, 80);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(300, 30);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "WATTING";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeviceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tpMain);
            this.Margin = new System.Windows.Forms.Padding(10, 50, 10, 0);
            this.Name = "DeviceControl";
            this.Size = new System.Drawing.Size(300, 110);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TableLayoutPanel tpMain;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbStatus;
        public System.Windows.Forms.Button btnConfig;
        public System.Windows.Forms.TextBox txtFSN;
        public System.Windows.Forms.TextBox txtIMEI;
    }
}

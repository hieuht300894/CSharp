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
            this.lbFSN = new System.Windows.Forms.Label();
            this.txtFSN = new System.Windows.Forms.TextBox();
            this.lbIMEI = new System.Windows.Forms.Label();
            this.txtIMEI = new System.Windows.Forms.TextBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.lbSKUNumber = new System.Windows.Forms.Label();
            this.lbReturnCode = new System.Windows.Forms.Label();
            this.lbClientName = new System.Windows.Forms.Label();
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
            this.tpMain.Controls.Add(this.lbFSN, 0, 2);
            this.tpMain.Controls.Add(this.txtFSN, 1, 2);
            this.tpMain.Controls.Add(this.lbIMEI, 0, 3);
            this.tpMain.Controls.Add(this.txtIMEI, 1, 3);
            this.tpMain.Controls.Add(this.lbStatus, 0, 5);
            this.tpMain.Controls.Add(this.lbInfo, 0, 1);
            this.tpMain.Controls.Add(this.btnConfig, 2, 1);
            this.tpMain.Controls.Add(this.lbSKUNumber, 0, 4);
            this.tpMain.Controls.Add(this.lbReturnCode, 0, 6);
            this.tpMain.Controls.Add(this.lbClientName, 0, 0);
            this.tpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpMain.Location = new System.Drawing.Point(0, 0);
            this.tpMain.Name = "tpMain";
            this.tpMain.RowCount = 7;
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tpMain.Size = new System.Drawing.Size(300, 190);
            this.tpMain.TabIndex = 0;
            // 
            // lbFSN
            // 
            this.lbFSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFSN.Location = new System.Drawing.Point(3, 57);
            this.lbFSN.Margin = new System.Windows.Forms.Padding(3);
            this.lbFSN.Name = "lbFSN";
            this.lbFSN.Size = new System.Drawing.Size(32, 21);
            this.lbFSN.TabIndex = 2;
            this.lbFSN.Text = "FSN";
            this.lbFSN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFSN
            // 
            this.txtFSN.Location = new System.Drawing.Point(41, 57);
            this.txtFSN.Name = "txtFSN";
            this.txtFSN.Size = new System.Drawing.Size(220, 21);
            this.txtFSN.TabIndex = 3;
            // 
            // lbIMEI
            // 
            this.lbIMEI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIMEI.Location = new System.Drawing.Point(3, 84);
            this.lbIMEI.Margin = new System.Windows.Forms.Padding(3);
            this.lbIMEI.Name = "lbIMEI";
            this.lbIMEI.Size = new System.Drawing.Size(32, 21);
            this.lbIMEI.TabIndex = 4;
            this.lbIMEI.Text = "IMEI";
            this.lbIMEI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIMEI
            // 
            this.txtIMEI.Location = new System.Drawing.Point(41, 84);
            this.txtIMEI.Name = "txtIMEI";
            this.txtIMEI.Size = new System.Drawing.Size(220, 21);
            this.txtIMEI.TabIndex = 5;
            // 
            // lbStatus
            // 
            this.tpMain.SetColumnSpan(this.lbStatus, 3);
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(3, 138);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Padding = new System.Windows.Forms.Padding(3);
            this.lbStatus.Size = new System.Drawing.Size(294, 21);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "WATTING";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInfo
            // 
            this.tpMain.SetColumnSpan(this.lbInfo, 2);
            this.lbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInfo.Location = new System.Drawing.Point(3, 30);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(3);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Padding = new System.Windows.Forms.Padding(3);
            this.lbInfo.Size = new System.Drawing.Size(258, 21);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "Not configured";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConfig
            // 
            this.btnConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfig.Location = new System.Drawing.Point(267, 30);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(30, 20);
            this.btnConfig.TabIndex = 1;
            this.btnConfig.TabStop = false;
            this.btnConfig.Text = "...";
            this.btnConfig.UseVisualStyleBackColor = true;
            // 
            // lbSKUNumber
            // 
            this.tpMain.SetColumnSpan(this.lbSKUNumber, 3);
            this.lbSKUNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSKUNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSKUNumber.Location = new System.Drawing.Point(3, 111);
            this.lbSKUNumber.Margin = new System.Windows.Forms.Padding(3);
            this.lbSKUNumber.Name = "lbSKUNumber";
            this.lbSKUNumber.Padding = new System.Windows.Forms.Padding(3);
            this.lbSKUNumber.Size = new System.Drawing.Size(294, 21);
            this.lbSKUNumber.TabIndex = 6;
            this.lbSKUNumber.Text = "SKU NUMBER:";
            this.lbSKUNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReturnCode
            // 
            this.tpMain.SetColumnSpan(this.lbReturnCode, 3);
            this.lbReturnCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReturnCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReturnCode.Location = new System.Drawing.Point(3, 165);
            this.lbReturnCode.Margin = new System.Windows.Forms.Padding(3);
            this.lbReturnCode.Name = "lbReturnCode";
            this.lbReturnCode.Padding = new System.Windows.Forms.Padding(3);
            this.lbReturnCode.Size = new System.Drawing.Size(294, 22);
            this.lbReturnCode.TabIndex = 6;
            this.lbReturnCode.Text = "RETURN CODE:";
            this.lbReturnCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbClientName
            // 
            this.lbClientName.BackColor = System.Drawing.Color.LimeGreen;
            this.tpMain.SetColumnSpan(this.lbClientName, 3);
            this.lbClientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClientName.ForeColor = System.Drawing.Color.White;
            this.lbClientName.Location = new System.Drawing.Point(3, 3);
            this.lbClientName.Margin = new System.Windows.Forms.Padding(3);
            this.lbClientName.Name = "lbClientName";
            this.lbClientName.Padding = new System.Windows.Forms.Padding(3);
            this.lbClientName.Size = new System.Drawing.Size(294, 21);
            this.lbClientName.TabIndex = 6;
            this.lbClientName.Text = "CLIENT NAME";
            this.lbClientName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeviceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tpMain);
            this.Margin = new System.Windows.Forms.Padding(10, 50, 10, 0);
            this.Name = "DeviceControl";
            this.Size = new System.Drawing.Size(300, 190);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TableLayoutPanel tpMain;
        private System.Windows.Forms.Label lbInfo;
        public System.Windows.Forms.Button btnConfig;
        public System.Windows.Forms.TextBox txtFSN;
        public System.Windows.Forms.TextBox txtIMEI;
        public System.Windows.Forms.Label lbStatus;
        public System.Windows.Forms.Label lbSKUNumber;
        public System.Windows.Forms.Label lbReturnCode;
        public System.Windows.Forms.Label lbClientName;
        public System.Windows.Forms.Label lbFSN;
        public System.Windows.Forms.Label lbIMEI;
    }
}

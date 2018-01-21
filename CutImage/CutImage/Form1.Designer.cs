namespace CutImage
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btUpload = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picCrop = new System.Windows.Forms.PictureBox();
            this.lbCurPoint = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstPoint = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btSave = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btStart = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btSaveAuto = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCrop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // btUpload
            // 
            this.btUpload.BackColor = System.Drawing.SystemColors.Control;
            this.btUpload.Location = new System.Drawing.Point(1059, 12);
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(279, 30);
            this.btUpload.TabIndex = 0;
            this.btUpload.Text = "Upload";
            this.btUpload.UseVisualStyleBackColor = false;
            this.btUpload.Click += new System.EventHandler(this.btUpload_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.picCrop);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1038, 668);
            this.panel1.TabIndex = 2;
            // 
            // picCrop
            // 
            this.picCrop.BackColor = System.Drawing.Color.White;
            this.picCrop.Location = new System.Drawing.Point(3, 6);
            this.picCrop.Name = "picCrop";
            this.picCrop.Size = new System.Drawing.Size(1032, 657);
            this.picCrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCrop.TabIndex = 0;
            this.picCrop.TabStop = false;
            this.picCrop.Paint += new System.Windows.Forms.PaintEventHandler(this.picCrop_Paint);
            this.picCrop.DoubleClick += new System.EventHandler(this.picCrop_DoubleClick);
            this.picCrop.MouseLeave += new System.EventHandler(this.picCrop_MouseLeave);
            this.picCrop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCrop_MouseMove);
            // 
            // lbCurPoint
            // 
            this.lbCurPoint.AutoSize = true;
            this.lbCurPoint.BackColor = System.Drawing.Color.White;
            this.lbCurPoint.Location = new System.Drawing.Point(1303, 669);
            this.lbCurPoint.Name = "lbCurPoint";
            this.lbCurPoint.Size = new System.Drawing.Size(35, 13);
            this.lbCurPoint.TabIndex = 4;
            this.lbCurPoint.Text = "label1";
            // 
            // numX
            // 
            this.numX.BackColor = System.Drawing.Color.White;
            this.numX.Location = new System.Drawing.Point(1101, 48);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(235, 20);
            this.numX.TabIndex = 1;
            this.numX.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numX.ValueChanged += new System.EventHandler(this.numX_ValueChanged);
            // 
            // numY
            // 
            this.numY.BackColor = System.Drawing.Color.White;
            this.numY.Location = new System.Drawing.Point(1101, 74);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(235, 20);
            this.numY.TabIndex = 2;
            this.numY.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numY.ValueChanged += new System.EventHandler(this.numY_ValueChanged);
            // 
            // numWidth
            // 
            this.numWidth.BackColor = System.Drawing.Color.White;
            this.numWidth.Location = new System.Drawing.Point(1103, 100);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(235, 20);
            this.numWidth.TabIndex = 3;
            this.numWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numWidth.ValueChanged += new System.EventHandler(this.numWidth_ValueChanged);
            // 
            // numHeight
            // 
            this.numHeight.BackColor = System.Drawing.Color.White;
            this.numHeight.Location = new System.Drawing.Point(1103, 126);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(235, 20);
            this.numHeight.TabIndex = 4;
            this.numHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numHeight.ValueChanged += new System.EventHandler(this.numHeight_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1056, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1056, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1056, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Width:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1056, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Height:";
            // 
            // lstPoint
            // 
            this.lstPoint.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstPoint.Location = new System.Drawing.Point(1059, 407);
            this.lstPoint.Name = "lstPoint";
            this.lstPoint.Size = new System.Drawing.Size(279, 213);
            this.lstPoint.TabIndex = 7;
            this.lstPoint.UseCompatibleStateImageBehavior = false;
            this.lstPoint.View = System.Windows.Forms.View.Details;
            this.lstPoint.SelectedIndexChanged += new System.EventHandler(this.lstPoint_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "X";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Y";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Width";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Height";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(1256, 152);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(82, 30);
            this.btSave.TabIndex = 5;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(1204, 627);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(134, 39);
            this.btStart.TabIndex = 7;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(1059, 627);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(134, 39);
            this.btClear.TabIndex = 8;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btSaveAuto
            // 
            this.btSaveAuto.Location = new System.Drawing.Point(1168, 152);
            this.btSaveAuto.Name = "btSaveAuto";
            this.btSaveAuto.Size = new System.Drawing.Size(82, 30);
            this.btSaveAuto.TabIndex = 6;
            this.btSaveAuto.Text = "Save Auto";
            this.btSaveAuto.UseVisualStyleBackColor = true;
            this.btSaveAuto.Click += new System.EventHandler(this.btSaveAuto_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader6});
            this.lstFiles.Location = new System.Drawing.Point(1059, 188);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(279, 213);
            this.lstFiles.TabIndex = 7;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Filename";
            this.columnHeader10.Width = 200;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Status";
            this.columnHeader11.Width = 75;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1350, 691);
            this.Controls.Add(this.btSaveAuto);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.lstPoint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.lbCurPoint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btUpload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cut Images Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCrop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btUpload;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picCrop;
        private System.Windows.Forms.Label lbCurPoint;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lstPoint;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btSaveAuto;
        private System.Windows.Forms.ListView lstFiles;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}


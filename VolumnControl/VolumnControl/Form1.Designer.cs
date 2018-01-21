namespace VolumnControl
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
            this.btVolUp = new System.Windows.Forms.Button();
            this.btVolDown = new System.Windows.Forms.Button();
            this.btMute = new System.Windows.Forms.Button();
            this.btnSoundDevice = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btVolUp
            // 
            this.btVolUp.Location = new System.Drawing.Point(13, 13);
            this.btVolUp.Name = "btVolUp";
            this.btVolUp.Size = new System.Drawing.Size(75, 23);
            this.btVolUp.TabIndex = 0;
            this.btVolUp.Text = "Vol +";
            this.btVolUp.UseVisualStyleBackColor = true;
            this.btVolUp.Click += new System.EventHandler(this.btVolUp_Click);
            // 
            // btVolDown
            // 
            this.btVolDown.Location = new System.Drawing.Point(94, 13);
            this.btVolDown.Name = "btVolDown";
            this.btVolDown.Size = new System.Drawing.Size(75, 23);
            this.btVolDown.TabIndex = 1;
            this.btVolDown.Text = "Vol -";
            this.btVolDown.UseVisualStyleBackColor = true;
            this.btVolDown.Click += new System.EventHandler(this.btVolDown_Click);
            // 
            // btMute
            // 
            this.btMute.Location = new System.Drawing.Point(175, 12);
            this.btMute.Name = "btMute";
            this.btMute.Size = new System.Drawing.Size(75, 23);
            this.btMute.TabIndex = 2;
            this.btMute.Text = "Mute";
            this.btMute.UseVisualStyleBackColor = true;
            this.btMute.Click += new System.EventHandler(this.btMute_Click);
            // 
            // btnSoundDevice
            // 
            this.btnSoundDevice.Location = new System.Drawing.Point(256, 13);
            this.btnSoundDevice.Name = "btnSoundDevice";
            this.btnSoundDevice.Size = new System.Drawing.Size(94, 23);
            this.btnSoundDevice.TabIndex = 4;
            this.btnSoundDevice.Text = "Sound Devices";
            this.btnSoundDevice.UseVisualStyleBackColor = true;
            this.btnSoundDevice.Click += new System.EventHandler(this.btnSoundDevice_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 400);
            this.Controls.Add(this.btnSoundDevice);
            this.Controls.Add(this.btMute);
            this.Controls.Add(this.btVolDown);
            this.Controls.Add(this.btVolUp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btVolUp;
        private System.Windows.Forms.Button btVolDown;
        private System.Windows.Forms.Button btMute;
        private System.Windows.Forms.Button btnSoundDevice;
        private System.Windows.Forms.Timer timer1;
    }
}


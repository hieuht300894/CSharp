﻿using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace VolumnControl
{
    public partial class Form1 : Form
    {

        #region Variable
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        bool oldValue = false;
        bool newValue = false;
        bool isMute = false;
        bool curMute = false;
        #endregion

        #region Form
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Event
        private void Form1_Load(object sender, EventArgs e)
        {
            var enumerator = new MMDeviceEnumerator();
            newValue = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Count == 1;
            if (newValue)
            {
                oldValue = newValue;
                isMute = newValue;
            }
            else
            {
                oldValue = !newValue;
                isMute = newValue;
            }
            timer1.Enabled = true;
        }

        private void btVolUp_Click(object sender, EventArgs e)
        {
            VolUp();
        }

        private void btVolDown_Click(object sender, EventArgs e)
        {
            VolDown();
        }

        private void btMute_Click(object sender, EventArgs e)
        {
            Mute();
        }

        private void btnSoundDevice_Click(object sender, EventArgs e)
        {
            GetSoundDevices();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetSoundDevices();
        }
        #endregion

        #region Method
        private void Mute()
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        private void VolDown()
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        private void VolUp()
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
        }

        private void GetSoundDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            int count = 0;
            foreach (var endpoint in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                if (endpoint.FriendlyName.Contains("Speakers"))
                    curMute = endpoint.AudioEndpointVolume.Mute;
                count++;
            }
            newValue = count == 1;
            SpeakerStateChange();
        }

        private void SpeakerStateChange()
        {
            if (newValue && !oldValue && !isMute)
            {
                if (!curMute)
                    Mute();
                isMute = true;
                oldValue = newValue;
                return;
            }
            if (!newValue && oldValue)
            {
                isMute = false;
                oldValue = newValue;
                return;
            }
        }
        #endregion
    }
}

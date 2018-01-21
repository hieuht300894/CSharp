using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sound
{
    public partial class Form1 : Form
    {
        bool headphoneState = false;
        MMDeviceEnumerator deviceEnumerator;
        AudioEndpointVolume audioEndpointVolume;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            deviceEnumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            timer1.Enabled = true;
        }
        private void VolumnMute()
        {
            audioEndpointVolume.Mute = !audioEndpointVolume.Mute;
        }
        private void VolumnUp()
        {
            audioEndpointVolume.VolumeStepUp();
        }
        private void VolumnDown()
        {
            audioEndpointVolume.VolumeStepDown();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (deviceEnumerator != null && timer1.Enabled)
            {
                // Allows you to enumerate rendering devices in certain states
                var endpoints = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active | DeviceState.Active);
                foreach (var endpoint in endpoints)
                {
                    audioEndpointVolume = endpoint.AudioEndpointVolume;
                    if (endpoint.FriendlyName.Contains("Headphones"))
                    {
                        if (endpoint.State == DeviceState.Unplugged && !headphoneState)
                        {
                            VolumnMute();
                            headphoneState = !headphoneState;
                        }
                    }
                }
            }
        }
        private void btUp_Click(object sender, EventArgs e)
        {
            VolumnUp();
        }
        private void btDown_Click(object sender, EventArgs e)
        {
            VolumnDown();
        }
        private void btMute_Click(object sender, EventArgs e)
        {
            VolumnMute();
        }
    }
}
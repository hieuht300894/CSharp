using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toub.Sound.Midi;

namespace PianoDemo
{
    public partial class Form1 : Form
    {
        private bool isClick;
        private List<Keys> lstKeys;

        public bool IsClick
        {
            get { return isClick; }
            set { isClick = value; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MidiPlayer.OpenMidi();
            lstKeys = new List<Keys>();
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void button1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (lstKeys.Any(x => x == e.KeyCode))
                return;

            lstKeys.Add(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.A:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "C" + trbNote.Value, 100));
                    break;
                case Keys.S:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "D" + trbNote.Value, 100));
                    break;
                case Keys.D:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "E" + trbNote.Value, 100));
                    break;
                case Keys.F:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "F" + trbNote.Value, 100));
                    break;
                case Keys.G:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "G" + trbNote.Value, 100));
                    break;
                case Keys.H:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "A" + trbNote.Value, 100));
                    break;
                case Keys.J:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "B" + trbNote.Value, 100));
                    break;
                case Keys.W:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "C#" + trbNote.Value, 100));
                    break;
                case Keys.E:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "D#" + trbNote.Value, 100));
                    break;
                case Keys.T:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "F#" + trbNote.Value, 100));
                    break;
                case Keys.Y:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "G#" + trbNote.Value, 100));
                    break;
                case Keys.U:
                    MidiPlayer.Play(new NoteOn(0, Convert.ToByte(trbChannel.Value), "A#" + trbNote.Value, 100));
                    break;
            }
            label3.Text = lstKeys.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClick = false;
        }

        private void button1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            isClick = false;
            lstKeys.Remove(e.KeyCode);
            label3.Text = lstKeys.Count.ToString();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MidiPlayer.OpenMidi();

        //    MidiPlayer.Play(new NoteOn(0, 1, "C4", 127));

        //}
    }
}

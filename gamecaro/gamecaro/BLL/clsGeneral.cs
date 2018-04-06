using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gamecaro
{
    public static class clsGeneral
    {
        public static string HowToPlay
        {
            get
            {
                return
                    "F1: How to play\n" +
                    "F2: Author\n" +
                    "F3: End game\n" +
                    "Play mode:\n" +
                    "   Key 1: Two players\n" +
                    "   Key 2: Player first\n" +
                    "   Key 3: Com first\n" +
                    "   Key 4: Com first\n" +
                    "Ctrl + Z: Undo\n" +
                    "Ctrl + X: Redo\n";
            }
        }
        public enum fKey
        {
            EMPTY = 0,
            OUTLINE = 1,
            X = 2,
            O = 3,
            UNDO = 4,
            BOARD = 5,
            LINE = 6,
            ACCEPT = 7,
            DENY = 8,
            REGISTER = 9,
            //OK = 7,
            //WAITING = 10,
            //CONNECTED = 5,
            //DISCONNECTED = 6,
            //PASS = 7,
            //FAILED = 8,
            RESULT = 9,
            //START = 10,
            //STOP = 15,
            //BEGIN = 16,
            USERNAME = 17,
            //QUESTION = 18,
            CONNECTING = 19,
            //CLIENT = 20,
            //SETUP = 21,
            //FINISHED = 22,
            //STARTING = 23,
            //ON = 24,
            //OFF = 25,
            //PROCESSING = 26,
            //NO_CLIENT_RESULT = 27,
            ERROR = 28,
            //NO_CONFIGURATION = 29,
            //HOST = 30,
        }
        public static string DefaultIP { get; private set; } = clsNetwork.LoadIPv4();
        public static int DefaultServerPort { get; private set; } = 5000;
        public static int DefaultClientPort { get; private set; } = 5001;

        public static void showMessage(this Form frmMain, string Title = "Message", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        public static void showWarming(this Form frmMain, string Title = "Warming", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        public static void showError(this Form frmMain, string Title = "Error", string Caption = "") { MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public static bool showConfirm(this Form frmMain, string Title = "Confirm", string Caption = "") { return MessageBox.Show(frmMain, Caption, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }

        public static Regex regexIPAddress { get { return new Regex(@"(\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})"); } }
        public static Regex regexAddress { get { return new Regex(@"(?<IP>(\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1})[.](\d{3}|\d{2}|\d{1}))[:](?<Port>(\d{5}|\d{4}))"); } }
        public static Regex regexCommand { get { return new Regex(string.Format("{0}{1}{2}", @"([[](?<Key>\w+)[]])([=])([[]]|([[](?<Value>[a-zA-Z0-9 ~`!@#$%^&*()_+\-=\[\]{};':", "\"", @"\\|,.<>\/?]+)[]]))")); } }

        public static ChessBoard ChessBoard { get; set; } = new ChessBoard();
        public static ServerConfig ServerConfig { get; set; } = new ServerConfig();
        public static ClientConfig ClientConfig { get; set; } = new ClientConfig();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace gamecaro.USERCONTROL
{
    public partial class BoardPlayerAndComputer : UserControl
    {
        public delegate void SetPicture(Image img);
        public SetPicture _SetPicture;

        FormWindowState prevWindowState = FormWindowState.Normal;
        Rectangle mHoverRectangle = Rectangle.Empty;

        public BoardPlayerAndComputer()
        {
            InitializeComponent();
        }

        private void BoardPlayerAndComputer_Load(object sender, EventArgs e)
        {
            prevWindowState = ParentForm.WindowState;

            SizeChanged -= BoardPlayerAndComputer_SizeChanged;
            SizeChanged += BoardPlayerAndComputer_SizeChanged;
        }
        private void BoardPlayerAndComputer_SizeChanged(object sender, EventArgs e)
        {
            if (prevWindowState != ParentForm.WindowState)
            {
                if (prevWindowState == FormWindowState.Minimized && (ParentForm.WindowState == FormWindowState.Normal || ParentForm.WindowState == FormWindowState.Maximized)) { }
                else if ((prevWindowState == FormWindowState.Normal || prevWindowState == FormWindowState.Maximized) && ParentForm.WindowState == FormWindowState.Minimized) { }
                else
                {
                    CalculateBoard();
                    DrawBoard();
                }
                prevWindowState = ParentForm.WindowState;
            }
            else
            {
                CalculateBoard();
                DrawBoard();
            }
        }
        private void BoardPlayerAndComputer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                Undo();
            }
        }
        private void pbMain_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            int PositionOfRow = 0;
            int PositionOfColumn = 0;
            clsGeneral.fKey Status = clsGeneral.fKey.OUTLINE;

            clsChessBoard.ConvertPointOfCell(mouse.Location, ref PositionOfRow, ref PositionOfColumn, ref Status);

            if (Status == clsGeneral.fKey.EMPTY)
            {
                switch (mouse.Button)
                {
                    case MouseButtons.Left:
                        DrawCross(PositionOfRow, PositionOfColumn);
                        break;
                    case MouseButtons.Right:
                        DrawCircle(PositionOfRow, PositionOfColumn);
                        break;
                }
            }
        }

        void CalculateBoard()
        {
            clsGeneral.ChessBoard.NumberOfRows = pbMain.Height / clsGeneral.ChessBoard.SizeOfCell;
            clsGeneral.ChessBoard.NumberOfColumns = pbMain.Width / clsGeneral.ChessBoard.SizeOfCell;
            clsGeneral.ChessBoard.SizeOfBoard = new Size(clsGeneral.ChessBoard.NumberOfColumns * clsGeneral.ChessBoard.SizeOfCell + 1, clsGeneral.ChessBoard.NumberOfRows * clsGeneral.ChessBoard.SizeOfCell + 1);

            pbMain.Size = new Size(clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height);
            pbMain.Image = new Bitmap(clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height, PixelFormat.Format32bppArgb);

            clsChessBoard.CreateEmptyBoard();
        }
        void DrawBoard()
        {
            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.BOARD;
            clsGeneral.ChessBoard.ListChessCheckeds.Insert(0, chessPoint);

            Pen pen = new Pen(Color.Black);
            Graphics graphics = Graphics.FromImage((Bitmap)pbMain.Image);

            for (int i = 0; i <= clsGeneral.ChessBoard.NumberOfRows; i++)
            {
                graphics.DrawLine(pen, 0, clsGeneral.ChessBoard.SizeOfCell * i, clsGeneral.ChessBoard.NumberOfColumns * clsGeneral.ChessBoard.SizeOfCell, clsGeneral.ChessBoard.SizeOfCell * i);
            }

            for (int i = 0; i <= clsGeneral.ChessBoard.NumberOfColumns; i++)
            {
                graphics.DrawLine(pen, clsGeneral.ChessBoard.SizeOfCell * i, 0, clsGeneral.ChessBoard.SizeOfCell * i, clsGeneral.ChessBoard.NumberOfRows * clsGeneral.ChessBoard.SizeOfCell);
            }

            SaveImage(chessPoint);
            EndGame();
            StartGame();
            TestGame();
        }
        bool DrawCross(int PositionOfRow, int PositionOfColumn)
        {
            if (!clsChessBoard.CheckEmptyOfChess(clsGeneral.fKey.X, PositionOfRow, PositionOfColumn)) return true;

            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.X;
            chessPoint.PositionOfRow = PositionOfRow;
            chessPoint.PositionOfColumn = PositionOfColumn;
            chessPoint.Location = new Point(PositionOfColumn * clsGeneral.ChessBoard.SizeOfCell, PositionOfRow * clsGeneral.ChessBoard.SizeOfCell);
            chessPoint.LastCheckPoint = clsGeneral.ChessBoard.ListChessCheckeds[0];
            clsGeneral.ChessBoard.ListChessCheckeds.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ChessBoard.ColorOfCross, 2);
            Graphics graphics = Graphics.FromImage((Bitmap)pbMain.Image);

            /* Vẽ chéo thuận (\) */
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X - clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y - clsGeneral.ChessBoard.SizeOfCross);
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X + clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y + clsGeneral.ChessBoard.SizeOfCross);

            /* Vẽ chéo nghịch (/) */
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X - clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y + clsGeneral.ChessBoard.SizeOfCross);
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X + clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y - clsGeneral.ChessBoard.SizeOfCross);

            SaveImage(chessPoint);

            if (clsChessBoard.CheckWin(clsGeneral.fKey.X, clsGeneral.fKey.O, PositionOfRow, PositionOfColumn))
            {
                EndGame();
                return true;
            }

            return false;
        }
        bool DrawCircle(int PositionOfRow, int PositionOfColumn)
        {
            if (!clsChessBoard.CheckEmptyOfChess(clsGeneral.fKey.O, PositionOfRow, PositionOfColumn)) return true;

            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.O;
            chessPoint.PositionOfRow = PositionOfRow;
            chessPoint.PositionOfColumn = PositionOfColumn;
            chessPoint.Location = new Point(PositionOfColumn * clsGeneral.ChessBoard.SizeOfCell, PositionOfRow * clsGeneral.ChessBoard.SizeOfCell);
            chessPoint.LastCheckPoint = clsGeneral.ChessBoard.ListChessCheckeds[0];
            clsGeneral.ChessBoard.ListChessCheckeds.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ChessBoard.ColorOfCircle, 2);
            Graphics graphics = Graphics.FromImage((Bitmap)pbMain.Image);

            graphics.DrawEllipse(pen, chessPoint.Location.X - clsGeneral.ChessBoard.SizeOfCircle, chessPoint.Location.Y - clsGeneral.ChessBoard.SizeOfCircle, clsGeneral.ChessBoard.SizeOfCircle * 2, clsGeneral.ChessBoard.SizeOfCircle * 2);

            SaveImage(chessPoint);

            if (clsChessBoard.CheckWin(clsGeneral.fKey.O, clsGeneral.fKey.X, PositionOfRow, PositionOfColumn))
            {
                EndGame();
                return true;
            }

            return false;
        }
        void DrawLine()
        {
            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.LINE;
            chessPoint.LastCheckPoint = clsGeneral.ChessBoard.ListChessCheckeds[0];
            clsGeneral.ChessBoard.ListChessCheckeds.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ChessBoard.ColorOfLine, 3f);
            Graphics graphics = Graphics.FromImage((Bitmap)pbMain.Image);

            ChessPoint cStart = clsGeneral.ChessBoard.ListChessWins.First();
            ChessPoint cEnd = clsGeneral.ChessBoard.ListChessWins.Last();

            graphics.DrawLine(pen, cStart.PositionOfColumn * clsGeneral.ChessBoard.SizeOfCell, cStart.PositionOfRow * clsGeneral.ChessBoard.SizeOfCell, cEnd.PositionOfColumn * clsGeneral.ChessBoard.SizeOfCell, cEnd.PositionOfRow * clsGeneral.ChessBoard.SizeOfCell);

            SaveImage(chessPoint);
        }

        void SaveImage(ChessPoint chessPoint)
        {
            pbMain.Invalidate();

            //using (Graphics graphics = Graphics.FromImage(chessPoint.Image))
            //{
            //    graphics.DrawImage(pbMain.Image, new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), GraphicsUnit.Pixel);
            //}
            //_SetPicture?.Invoke(chessPoint.Image);
        }
        void Undo()
        {
            if (clsGeneral.ChessBoard.ListChessCheckeds == null || clsGeneral.ChessBoard.ListChessCheckeds.Count == 0 || clsGeneral.ChessBoard.ListChessCheckeds.Count == 1) return;

            ChessPoint chessPoint = clsGeneral.ChessBoard.ListChessCheckeds[0];
            clsGeneral.ChessBoard.ListChessCheckeds.RemoveAt(0);

            Graphics graphics = Graphics.FromImage(pbMain.Image);
            graphics.Clear(pbMain.BackColor);
            graphics.DrawImage(chessPoint.LastCheckPoint.Image, new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), GraphicsUnit.Pixel);

            pbMain.Invalidate();

            clsChessBoard.RemoveTypeOfChess(chessPoint.PositionOfRow, chessPoint.PositionOfColumn);

            _SetPicture?.Invoke(chessPoint.LastCheckPoint.Image);
        }
        void StartGame()
        {
            pbMain.Click += pbMain_Click;
            PreviewKeyDown += BoardPlayerAndComputer_PreviewKeyDown;
        }
        void EndGame()
        {
            pbMain.Click -= pbMain_Click;
            PreviewKeyDown -= BoardPlayerAndComputer_PreviewKeyDown;

            if (clsGeneral.ChessBoard.ListChessWins.Count >= 5)
            {
                DrawLine();
            }
        }
        void TestGame()
        {
            clsGeneral.fKey Attack = clsGeneral.fKey.X;
            clsGeneral.fKey Block = clsGeneral.fKey.O;

            System.Timers.Timer timer = new System.Timers.Timer() { AutoReset = true, Interval = 50 };
            timer.Elapsed -= (s, e) => Timer_Elapsed(s, e, ref Attack, ref Block);
            timer.Elapsed += (s, e) => Timer_Elapsed(s, e, ref Attack, ref Block);
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e, ref clsGeneral.fKey Attack, ref clsGeneral.fKey Block)
        {
            System.Timers.Timer timer = (System.Timers.Timer)sender;
            timer.Stop();

            int PositionOfRow = 0;
            int PositionOfColumn = 0;

         

            if (!clsAI.MinMax(ref PositionOfRow, ref PositionOfColumn, Attack, Block))
                return;

            if (Attack == clsGeneral.fKey.X && Block == clsGeneral.fKey.O)
            {
                if (DrawCross(PositionOfRow, PositionOfColumn)) { return; }
                else
                {
                    Attack = clsGeneral.fKey.O;
                    Block = clsGeneral.fKey.X;
                }

            }
            else if (Attack == clsGeneral.fKey.O && Block == clsGeneral.fKey.X)
            {
                if (DrawCircle(PositionOfRow, PositionOfColumn)) { return; }
                else
                {
                    Attack = clsGeneral.fKey.X;
                    Block = clsGeneral.fKey.O;
                }
            }

            timer.Start();
        }
    }
}

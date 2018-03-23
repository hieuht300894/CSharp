using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace gamecaro.USERCONTROL
{
    public partial class BoardPlayerAndPlayer : UserControl
    {
        public delegate void SetPicture(Image img);
        public SetPicture _SetPicture;

        FormWindowState prevWindowState = FormWindowState.Normal;
        Rectangle mHoverRectangle = Rectangle.Empty;

        public BoardPlayerAndPlayer()
        {
            InitializeComponent();
        }

        private void BoardPlayerAndPlayer_Load(object sender, EventArgs e)
        {
            prevWindowState = ParentForm.WindowState;

            SizeChanged -= BoardPlayerAndPlayer_SizeChanged;
            SizeChanged += BoardPlayerAndPlayer_SizeChanged;
        }
        private void BoardPlayerAndPlayer_SizeChanged(object sender, EventArgs e)
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
        private void BoardPlayerAndPlayer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
            clsGeneral.ChessBoard.ListChesses.Insert(0, chessPoint);

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
        void DrawCross(int PositionOfRow, int PositionOfColumn)
        {
            if (!clsChessBoard.CheckEmptyOfChess(clsGeneral.fKey.X, PositionOfRow, PositionOfColumn)) return;

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
                EndGame();
        }
        void DrawCircle(int PositionOfRow, int PositionOfColumn)
        {
            if (!clsChessBoard.CheckEmptyOfChess(clsGeneral.fKey.O, PositionOfRow, PositionOfColumn)) return;

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
                EndGame();
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

            using (Graphics graphics = Graphics.FromImage(chessPoint.Image))
            {
                graphics.DrawImage(pbMain.Image, new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), GraphicsUnit.Pixel);
            }
            _SetPicture?.Invoke(chessPoint.Image);
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
            PreviewKeyDown += BoardPlayerAndPlayer_PreviewKeyDown;
        }
        void EndGame()
        {
            pbMain.Click -= pbMain_Click;
            PreviewKeyDown -= BoardPlayerAndPlayer_PreviewKeyDown;

            if (clsGeneral.ChessBoard.ListChessWins.Count >= 5)
            {
                DrawLine();
                clsGeneral.ChessBoard.ListChessWins.Clear();
            }
        }
        void TestGame()
        {
            //DrawCross(1, 1);
            //DrawCircle(1, 2);
            //DrawCircle(1, 3);
            //DrawCircle(1, 4);
            //DrawCircle(1, 5);
            //DrawCross(1, 8);
            //DrawCircle(1, 7);
            //DrawCircle(1, 6);

            //DrawCross(1, 2);
            //DrawCircle(2, 2);
            //DrawCircle(3, 2);
            //DrawCircle(4, 2);
            //DrawCircle(5, 2);
            //DrawCross(7, 2);
            //DrawCircle(6, 2);

            //DrawCross(1, 2);
            //DrawCircle(2, 3);
            //DrawCircle(3, 4);
            //DrawCircle(4, 5);
            //DrawCircle(5, 6);
            //DrawCross(8, 9);
            //DrawCircle(6, 7);

            //DrawCross(12, 4);
            //DrawCircle(11, 5);
            //DrawCircle(10, 6);
            //DrawCircle(9, 7);
            //DrawCircle(8, 8);
            //DrawCross(5, 11);
            //DrawCircle(7, 9);
        }
    }
}

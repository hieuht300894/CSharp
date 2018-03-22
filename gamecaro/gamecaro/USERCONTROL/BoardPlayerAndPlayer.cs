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

        List<ChessPoint> lstChessPoint = new List<ChessPoint>();
        FormWindowState prevWindowState = FormWindowState.Normal;
        Rectangle mHoverRectangle = Rectangle.Empty;

        public BoardPlayerAndPlayer()
        {
            InitializeComponent();
        }

        private void BoardPlayerAndPlayer_Load(object sender, EventArgs e)
        {
            prevWindowState = ParentForm.WindowState;

            pbMain.Click -= pbMain_Click;
            pbMain.Click += pbMain_Click;
            PreviewKeyDown -= BoardPlayerAndPlayer_PreviewKeyDown;
            PreviewKeyDown += BoardPlayerAndPlayer_PreviewKeyDown;
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
            clsGeneral.fKey Status = clsGeneral.fKey.OutLine;

            clsChessBoard.ConvertPointOfCell(mouse.Location, ref PositionOfRow, ref PositionOfColumn, ref Status);

            if (Status == clsGeneral.fKey.Empty)
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
            lstChessPoint.Clear();

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
            chessPoint.TypeOfChess = clsGeneral.fKey.Board;
            lstChessPoint.Insert(0, chessPoint);

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

            pbMain.Invalidate();

            SaveImage(chessPoint);
        }
        void DrawCross(int PositionOfRow, int PositionOfColumn)
        {
            if (!clsChessBoard.CheckEmptyOfChess(clsGeneral.fKey.X, PositionOfRow, PositionOfColumn)) return;

            List<Cell> lstCell = new List<Cell>();
            if (clsChessBoard.CheckWin(clsGeneral.fKey.X, clsGeneral.fKey.O, PositionOfRow, PositionOfColumn, ref lstCell)) return;

            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.X;
            chessPoint.PositionOfRow = PositionOfRow;
            chessPoint.PositionOfColumn = PositionOfColumn;
            chessPoint.Location = new Point(PositionOfColumn * clsGeneral.ChessBoard.SizeOfCell, PositionOfRow * clsGeneral.ChessBoard.SizeOfCell);
            chessPoint.LastCheckPoint = lstChessPoint[0];
            lstChessPoint.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ChessBoard.ColorOfCross, 2);
            Graphics graphics = Graphics.FromImage((Bitmap)pbMain.Image);

            /* Vẽ chéo thuận (\) */
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X - clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y - clsGeneral.ChessBoard.SizeOfCross);
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X + clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y + clsGeneral.ChessBoard.SizeOfCross);

            /* Vẽ chéo nghịch (/) */
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X - clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y + clsGeneral.ChessBoard.SizeOfCross);
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X + clsGeneral.ChessBoard.SizeOfCross, chessPoint.Location.Y - clsGeneral.ChessBoard.SizeOfCross);

            pbMain.Invalidate();

            SaveImage(chessPoint);
        }
        void DrawCircle(int PositionOfRow, int PositionOfColumn)
        {
            if (!clsChessBoard.CheckEmptyOfChess(clsGeneral.fKey.O, PositionOfRow, PositionOfColumn)) return;

            List<Cell> lstCell = new List<Cell>();
            if (clsChessBoard.CheckWin(clsGeneral.fKey.O, clsGeneral.fKey.X, PositionOfRow, PositionOfColumn, ref lstCell)) return;

            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.O;
            chessPoint.PositionOfRow = PositionOfRow;
            chessPoint.PositionOfColumn = PositionOfColumn;
            chessPoint.Location = new Point(PositionOfColumn * clsGeneral.ChessBoard.SizeOfCell, PositionOfRow * clsGeneral.ChessBoard.SizeOfCell);
            chessPoint.LastCheckPoint = lstChessPoint[0];
            lstChessPoint.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ChessBoard.ColorOfCircle, 2);
            Graphics graphics = Graphics.FromImage((Bitmap)pbMain.Image);

            graphics.DrawEllipse(pen, chessPoint.Location.X - clsGeneral.ChessBoard.SizeOfCircle, chessPoint.Location.Y - clsGeneral.ChessBoard.SizeOfCircle, clsGeneral.ChessBoard.SizeOfCircle * 2, clsGeneral.ChessBoard.SizeOfCircle * 2);

            pbMain.Invalidate();

            SaveImage(chessPoint);
        }

        void SaveImage(ChessPoint chessPoint)
        {
            using (Graphics graphics = Graphics.FromImage(chessPoint.Image))
            {
                graphics.DrawImage(pbMain.Image, new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), GraphicsUnit.Pixel);
            }
            _SetPicture?.Invoke(chessPoint.Image);
        }
        void Undo()
        {
            if (lstChessPoint == null || lstChessPoint.Count == 0 || lstChessPoint.Count == 1) return;

            ChessPoint chessPoint = lstChessPoint[0];
            lstChessPoint.RemoveAt(0);

            Graphics graphics = Graphics.FromImage(pbMain.Image);
            graphics.Clear(pbMain.BackColor);
            graphics.DrawImage(chessPoint.LastCheckPoint.Image, new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), new Rectangle(0, 0, clsGeneral.ChessBoard.SizeOfBoard.Width, clsGeneral.ChessBoard.SizeOfBoard.Height), GraphicsUnit.Pixel);

            pbMain.Invalidate();

            clsChessBoard.RemoveTypeOfChess(chessPoint.PositionOfRow, chessPoint.PositionOfColumn);

            _SetPicture?.Invoke(chessPoint.LastCheckPoint.Image);
        }
    }
}

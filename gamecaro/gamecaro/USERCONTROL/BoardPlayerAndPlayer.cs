using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gamecaro.USERCONTROL
{
    public partial class BoardPlayerAndPlayer : UserControl
    {
        public delegate void SetPicture(Image img);
        public SetPicture _SetPicture;

        List<ChessPoint> lstChessPoint = new List<ChessPoint>();
        FormWindowState prevWindowState = FormWindowState.Normal;

        public BoardPlayerAndPlayer()
        {
            InitializeComponent();
        }

        private void BoardPlayerAndPlayer_Load(object sender, EventArgs e)
        {
            prevWindowState = ParentForm.WindowState;

            pbMain.Paint -= pbMain_Paint;
            pbMain.Paint += pbMain_Paint;
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
            Point pResult = new Point();
            clsGeneral.fKey Status = clsGeneral.fKey.OutLine;

            ConvertPointOfCell(mouse.Location, ref pResult, ref Status);

            if (Status == clsGeneral.fKey.UnChecked)
            {
                switch (mouse.Button)
                {
                    case MouseButtons.Left:
                        DrawCross(pResult);
                        break;
                    case MouseButtons.Right:
                        DrawCircle(pResult);
                        break;
                }
            }
        }
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {

        }

        void CalculateBoard()
        {
            lstChessPoint.Clear();

            clsGeneral.NumberOfRows = pbMain.Height / clsGeneral.SizeOfCell;
            clsGeneral.NumberOfColumns = pbMain.Width / clsGeneral.SizeOfCell;
            clsGeneral.SizeOfBoard = new Size(clsGeneral.NumberOfColumns * clsGeneral.SizeOfCell + 1, clsGeneral.NumberOfRows * clsGeneral.SizeOfCell + 1);

            pbMain.Size = new Size(clsGeneral.SizeOfBoard.Width, clsGeneral.SizeOfBoard.Height);
            pbMain.Image = new Bitmap(clsGeneral.SizeOfBoard.Width, clsGeneral.SizeOfBoard.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        void DrawBoard()
        {
            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.Board;
            lstChessPoint.Insert(0, chessPoint);

            Pen pen = new Pen(Color.Black);
            Graphics graphics = Graphics.FromImage(pbMain.Image);

            for (int i = 0; i <= clsGeneral.NumberOfRows; i++)
            {
                graphics.DrawLine(pen, 0, clsGeneral.SizeOfCell * i, clsGeneral.NumberOfColumns * clsGeneral.SizeOfCell, clsGeneral.SizeOfCell * i);
            }

            for (int i = 0; i <= clsGeneral.NumberOfColumns; i++)
            {
                graphics.DrawLine(pen, clsGeneral.SizeOfCell * i, 0, clsGeneral.SizeOfCell * i, clsGeneral.NumberOfRows * clsGeneral.SizeOfCell);
            }

            pbMain.Invalidate();

            graphics.DrawImage(chessPoint.Image, 0, 0, chessPoint.Image.Width, chessPoint.Image.Height);
            _SetPicture?.Invoke(chessPoint.Image);
        }
        void DrawCross(Point point)
        {
            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.X;
            chessPoint.SpotCell = new Point(point.X, point.Y);
            chessPoint.Location = new Point(point.X * clsGeneral.SizeOfCell, point.Y * clsGeneral.SizeOfCell);
            lstChessPoint.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ColorOfCross, 2);
            Graphics graphics = Graphics.FromImage(pbMain.Image);

            /* Vẽ chéo thuận (\) */
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X - clsGeneral.SizeOfCross, chessPoint.Location.Y - clsGeneral.SizeOfCross);
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X + clsGeneral.SizeOfCross, chessPoint.Location.Y + clsGeneral.SizeOfCross);

            /* Vẽ chéo nghịch (/) */
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X - clsGeneral.SizeOfCross, chessPoint.Location.Y + clsGeneral.SizeOfCross);
            graphics.DrawLine(pen, chessPoint.Location.X, chessPoint.Location.Y, chessPoint.Location.X + clsGeneral.SizeOfCross, chessPoint.Location.Y - clsGeneral.SizeOfCross);

            pbMain.Invalidate();

            graphics.DrawImage(chessPoint.Image, 0, 0, chessPoint.Image.Width, chessPoint.Image.Height);
            _SetPicture?.Invoke(chessPoint.Image);
        }
        void DrawCircle(Point point)
        {
            ChessPoint chessPoint = new ChessPoint();
            chessPoint.TypeOfChess = clsGeneral.fKey.O;
            chessPoint.SpotCell = new Point(point.X, point.Y);
            chessPoint.Location = new Point(point.X * clsGeneral.SizeOfCell, point.Y * clsGeneral.SizeOfCell);
            lstChessPoint.Insert(0, chessPoint);

            Pen pen = new Pen(clsGeneral.ColorOfCircle, 2);
            Graphics graphics = Graphics.FromImage(pbMain.Image);

            graphics.DrawEllipse(pen, chessPoint.Location.X - clsGeneral.SizeOfCircle, chessPoint.Location.Y - clsGeneral.SizeOfCircle, clsGeneral.SizeOfCircle * 2, clsGeneral.SizeOfCircle * 2);

            pbMain.Invalidate();

            graphics.DrawImage(chessPoint.Image, 0, 0, chessPoint.Image.Width, chessPoint.Image.Height);
            _SetPicture?.Invoke(chessPoint.Image);
        }
        void ConvertPointOfCell(Point pCheck, ref Point pResult, ref clsGeneral.fKey Status)
        {
            int div_X = pCheck.X / clsGeneral.SizeOfCell;
            int div_Y = pCheck.Y / clsGeneral.SizeOfCell;
            int mod_x = pCheck.X % clsGeneral.SizeOfCell;
            int mod_y = pCheck.Y % clsGeneral.SizeOfCell;
            if (mod_x > (clsGeneral.SizeOfCell / 2))
                div_X += 1;
            if (mod_y > (clsGeneral.SizeOfCell / 2))
                div_Y += 1;

            if (div_X <= 0 || div_Y <= 0 || div_X >= clsGeneral.NumberOfColumns - 1 || div_Y >= clsGeneral.NumberOfRows - 1)
            {
                /* Outline */
                Status = clsGeneral.fKey.OutLine;
            }
            else
            {
                /* Unchecked */
                Status = clsGeneral.fKey.UnChecked;
                pResult = new Point(div_X, div_Y);

                //if (banCo[div_Y][div_X] == 0)
                //{
                //    point.X = div_Y;
                //    point.Y = div_X;
                //}
                //else
                //{
                //    point.X = -2;
                //    point.Y = -2;
                //}
            }
        }
        void Undo()
        {
            if (lstChessPoint == null || lstChessPoint.Count == 0) return;
            lstChessPoint.RemoveAt(0);

            if (lstChessPoint == null || lstChessPoint.Count == 0) return;
            ChessPoint chessPoint = lstChessPoint[0];

            Graphics graphics = Graphics.FromImage(chessPoint.Image);
            graphics.Clear(pbMain.BackColor);
            graphics.DrawImage(pbMain.Image, 0, 0);

            pbMain.Invalidate();

            _SetPicture?.Invoke(chessPoint.Image);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeEngine;

namespace TicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            GameBoard gameBoard = new GameBoard();
            gameBoard.Mark(2, 1);
            gameBoard.Mark(0, 0);
            BoardDrawer test = new BoardDrawer(gameBoard, form);
            test.Update();

            Application.Run(form);
        }
    }

    class GameBoard
    {
        char[,] moveMatrix;
        char currentTurn = 'X';
        int turnNumber = 0;

        public GameBoard(char[,] moveMatrix = null)
        {
            if (moveMatrix == null)
            {
                this.moveMatrix = GetEmptyMatrix();
            }
           else
           {
                this.moveMatrix = moveMatrix;
                int count = 0;
                foreach (char mark in moveMatrix)
                {
                    if (mark != 'N')
                    {
                        count += 1;
                    }
                }
                turnNumber = count;
           }
        }

        public char[,] MoveMatrix
        {
            get { return this.moveMatrix; }
        }

        public void Mark(int x, int y)
        {
            moveMatrix[y, x] = currentTurn;
            currentTurn = (currentTurn == 'X') ? 'O' : 'X';
        }

        public static char[,] GetEmptyMatrix()
        {
            return new char[,] { { 'N', 'N', 'N' },
                                 { 'N', 'N', 'N' },
                                 { 'N', 'N', 'N' } };
        }

        public char[,] GetMoveMatrix()
        {
            return this.moveMatrix;
        }

        public void Print()
        {
            foreach (char c in this.moveMatrix)
            {
                Debug.WriteLine(c);
            }
        }
    }

    class BoardDrawer
    {
        GameBoard gameBoard;
        PictureBox pictureBox;
        Stack<PictureBox> xMarkPool = new Stack<PictureBox>();
        Stack<PictureBox> oMarkPool = new Stack<PictureBox>();
        MainForm form;


        public BoardDrawer(GameBoard gameBoard, MainForm form)
        {
            this.gameBoard = gameBoard;
            this.form = form;

            this.pictureBox = form.BoardPicture;

            for (int i = 0; i < 4; i++)
            {
                xMarkPool.Push(CreateMark('X'));
                oMarkPool.Push(CreateMark('O'));     
            }
            xMarkPool.Push(CreateMark('X'));
        }

        private PictureBox CreateMark(char marking)
        {
            PictureBox mark = new PictureBox();
            mark.SizeMode = PictureBoxSizeMode.StretchImage;
            mark.Width = 100;
            mark.Height = 100;
            mark.Parent = pictureBox;
            mark.BackColor = System.Drawing.Color.Transparent;
            mark.Visible = false;
            if (marking == 'O')
            {
                mark.Image = TicTacToeEngine.Properties.Resources.TicTacToeO;
            }
            if (marking == 'X')
            {
                mark.Image = TicTacToeEngine.Properties.Resources.TicTacToeX;
            }

            form.Controls.Add(mark);
            return mark;
        }

        public void Update()
        {
            pictureBox.SendToBack();
            for (int i = 0; i < gameBoard.MoveMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.MoveMatrix.GetLength(1); j++)
                {
                    char mark = gameBoard.MoveMatrix[i, j];
                    PictureBox markImage = null;
                    switch (mark)
                    {
                        case 'X':
                            markImage = xMarkPool.Pop();
                            break;
                        case 'O':
                            markImage = oMarkPool.Pop();
                            break;
                        case 'N':
                            continue;
                    }

                    
                    int[] point = { j, i };
                    int[] convertedPoint = GetScreenPoint(point);
                    Debug.WriteLine(pictureBox.Left);
                    markImage.Left = pictureBox.Left + convertedPoint[0];
                    markImage.Top = pictureBox.Top + convertedPoint[1];
                    markImage.Visible = true;
                }
            }
        }

        private int[] GetScreenPoint(int[] point)
        {
            int unit = Math.Min(this.pictureBox.Width / 3, this.pictureBox.Height / 3);
            int offset = 40;

            int[] result = new int[2];
            result[0] = point[0] * unit + offset;
            result[1] = point[1] * unit + offset;
            return result;
        }
    }

   

}

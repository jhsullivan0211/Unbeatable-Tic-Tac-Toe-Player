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
        /// The main entry point for the application.  Creates the necessary game data and loads the form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            GameBoard gameBoard = new GameBoard();          
            BoardDrawer drawer = new BoardDrawer(gameBoard, form);
            InputReceiver inputReceiver = new InputReceiver(gameBoard, drawer);
            form.SetInputReceiver(inputReceiver);

            Application.Run(form);
        }
    }

    /// <summary>
    /// A class to receive click information from MainForm forms and mark the board if 
    /// the click is in the right place.
    /// </summary>
    public class InputReceiver
    {
        GameBoard board;
        PictureBox boardPicture;
        BoardDrawer drawer;

        /// <summary>
        /// Basic constructor for the InputReceiver class.
        /// </summary>
        /// <param name="board">The GameBoard to mark where clicked, if in a valid position.</param>    
        /// <param name="boardPicture">The PictureBox that displays the board.</param>
        /// <param name="drawer">The BoardDrawer </param>
        public InputReceiver(GameBoard board, BoardDrawer drawer)
        {
            this.board = board;
            this.boardPicture = drawer.BoardPicture;
            this.drawer = drawer;
        }

        /// <summary>
        /// Receives click information from the form and acts on the GameBoard, marking the appropriate
        /// place.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ProcessClick(int x, int y)
        {
            int unitX = boardPicture.Width / 3;
            int unitY = boardPicture.Height / 3;

            int convertedX = x / unitX;
            int convertedY = y / unitY;

            if (board.Mark(convertedX, convertedY))
            {
                char winner = board.TestBoard();
                if (winner != 'N')
                {
                    Debug.WriteLine(winner);
                }
                drawer.Update();
            }

        }
    }


    public class GameBoard
    {
        char[,] moveMatrix;
        char currentTurn = 'X';
        int turnNumber = 0;
        int[] lastMove;
                                       //X, O
        int[][] rowMarks = { new int[] { 0, 0 },        //row 0
                             new int[] { 0, 0 },        //row 1
                             new int[] { 0, 0 } };      //row 2

        int[][] columnMarks = { new int[] { 0, 0 },     //col 0
                                new int[] { 0, 0 },     //col 1
                                new int[] { 0, 0 } };   //col 2

        int[][] diagonalMarks = { new int[] { 0, 0 },   //up-left diag
                                  new int[] { 0, 0 }};  //down-right diag


        public GameBoard()
        {
            moveMatrix = GetEmptyMatrix();
        }

        public bool Mark(int x, int y)
        {
            if (moveMatrix[y, x] != 'N')
            {
                return false;
            }

            moveMatrix[y, x] = currentTurn;
            int markIndex = (currentTurn == 'X') ? 0 : 1;
            turnNumber++;
            rowMarks[y][markIndex]++;
            columnMarks[x][markIndex]++;

            if (y - x == 0)
            {
                diagonalMarks[0][markIndex]++;
            }
            if (y - x == 2 || y - x == -2 || (y - x == 0 && y == 1))
            {
                diagonalMarks[1][markIndex]++;
            }

            currentTurn = (currentTurn == 'X') ? 'O' : 'X';
            lastMove = new int[] { x, y };
            return true;
        }

        public char TestBoard()
        {
            int[][][] groups = { rowMarks, columnMarks, diagonalMarks };
            foreach (int[][] group in groups)
            {
                for (int i = 0; i < group.Length; i++)
                {
                    if (group[i][0] == 3)
                    {
                        return 'X';
                    }
                    if (group[i][1] == 3)
                    {
                        return 'O';
                    }
                }
            }
            if (turnNumber == 9)
            {
                return 'T';
            }
            else
            {
                return 'N';
            }
        }

        public static char[,] GetEmptyMatrix()
        {
            return new char[,] { { 'N', 'N', 'N' },
                                 { 'N', 'N', 'N' },
                                 { 'N', 'N', 'N' } };
        }


        public char[,] MoveMatrix
        {
            get { return this.moveMatrix; }
        }

        public int[] LastMove
        {
            get { return this.lastMove; }
        }

        public char CurrentTurn
        {
            get { return this.currentTurn; }
        }

    }


    public class BoardDrawer
    {
        GameBoard gameBoard;
        PictureBox boardPicture;
        MainForm form;

        public BoardDrawer(GameBoard gameBoard, MainForm form)
        {
            this.gameBoard = gameBoard;
            this.form = form;

            this.boardPicture = form.BoardPicture;
            boardPicture.SendToBack();
       
        }

        public PictureBox BoardPicture
        {
            get { return this.boardPicture; }
        }

        private PictureBox CreateMark(char marking, int x, int y)
        {
            PictureBox mark = new PictureBox();
            mark.SizeMode = PictureBoxSizeMode.StretchImage;
            mark.Width = 100;
            mark.Height = 100;
            mark.Parent = boardPicture;
            mark.BackColor = System.Drawing.Color.Transparent;
            mark.Left = x;
            mark.Top = y;

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
            char mark = gameBoard.CurrentTurn == 'X' ? 'O' : 'X';
            int[] point = GetScreenPoint(gameBoard.LastMove);
            PictureBox markPicture = CreateMark(mark, point[0], point[1]);
            markPicture.Parent = boardPicture;
        }

        private int[] GetScreenPoint(int[] point)
        {
            int unit = Math.Min(this.boardPicture.Width / 3, this.boardPicture.Height / 3);
            int offset = 40;
            int[] result = new int[2];
            result[0] = (point[0] * unit + offset);
            result[1] = (point[1] * unit + offset);
            return result;
        }
    }
}

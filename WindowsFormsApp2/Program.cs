using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeEngine;

namespace TicTacToe
{
    /// <summary>
    /// Entry point class that contains initialization, including launching the form.
    /// </summary>
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




            Application.Run(form);
        }
    }


    /// <summary>
    /// Stores the model of the tic tac toe data, namely which spaces are marked by
    /// which player and the number of marks each player has in each row, column, and
    /// diagonal.
    /// </summary>
    public class GameBoard
    {
        char[,] moveMatrix;
        char currentTurn = 'X';
        int turnNumber = 0;
        int[] lastMove;

        //These data structures hold the number of Xs and Os in each row, column and diagonal.
        //Within each data structure, the value at index 0 corresponds to the number of Xs in
        //that row/column/diagonal, and the value at index 1 corresponds to the number of Os.

        //  {Xs, Os}
        int[][] rowMarks = { new int[] { 0, 0 },        //row 0
                             new int[] { 0, 0 },        //row 1
                             new int[] { 0, 0 } };      //row 2

        int[][] columnMarks = { new int[] { 0, 0 },     //col 0
                                new int[] { 0, 0 },     //col 1
                                new int[] { 0, 0 } };   //col 2

        int[][] diagonalMarks = { new int[] { 0, 0 },   //up-left diag
                                  new int[] { 0, 0 }};  //down-right diag

        /// <summary>
        /// Basic constructor for this class.
        /// </summary>
        public GameBoard()
        {
            moveMatrix = GetEmptyMatrix();
        }

        /// <summary>
        /// Marks the current player's mark onto the board at the specified location, and switches the current
        /// turn to the next player.  Additionally, increments the number of Xs/Os in the marked row, column,
        /// and diagonal.
        /// 
        /// </summary>
        /// <param name="x">The column coordinate to mark, from 0 to 2 inclusive.</param>
        /// <param name="y">The row coordinate to mark, from 0 to 2 inclusive</param>
        /// <returns>Returns whether or not the mark was successful, i.e. a valid empty position was chosen.</returns>
        public bool Mark(int x, int y, char marking = 'z')
        {
            if (moveMatrix[y, x] != 'N')
            {
                return false;
            }

            if (marking != 'z' && marking != 'X' && marking != 'O')
            {
                throw new System.ArgumentException("The char provided for marking should be either 'X' or 'O'.");
            }
            if (marking == 'z')
            {
                marking = currentTurn;
            }

            moveMatrix[y, x] = marking;
            int markIndex = (marking == 'X') ? 0 : 1;
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

            currentTurn = (marking == 'X') ? 'O' : 'X';
            lastMove = new int[] { x, y };
            return true;
        }

        /// <summary>
        /// Tests the board for end conditions, whether 3-in-a-row by a player, a tie, or neither players wins, and returns
        /// the appropriate symbol describing this state.
        /// </summary>
        /// <returns>Returns the char describing the result of evalutating the board, either 'X' or 'O' for the winning player,
        /// 'T' for a tie, or 'N' for a non-finished game.</returns>
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

        /// <summary>
        /// Returns a new GameBoard identical to this one.
        /// </summary>
        /// <returns></returns>
        public GameBoard Duplicate()
        {
            GameBoard copy = new GameBoard();
            int count = 0;
            for (int i = 0; i < moveMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < moveMatrix.GetLength(1); j++)
                {
                    char mark = moveMatrix[i, j];
                    if (mark == 'X')
                    {
                        count += 1;
                    }
                    if (mark == 'O')
                    {
                        count -= 1;
                    }
                    copy.Mark(j, i, moveMatrix[i, j]);
                }
            }
            char turn = (count <= 0) ? 'X' : 'O';
            copy.CurrentTurn = turn;

            return copy;
        }

        /// <summary>
        /// Returns an empty move matrix.
        /// </summary>
        /// <returns>Returns an empty move matrix.</returns>
        public static char[,] GetEmptyMatrix()
        {
            return new char[,] { { 'N', 'N', 'N' },
                                 { 'N', 'N', 'N' },
                                 { 'N', 'N', 'N' } };
        }

        /// <summary>
        /// The MoveMatrix property represents the marks on the GameBoard.
        /// </summary>
        /// <value>The MoveMatrix property gets the value of the field moveMatrix.</value>

        public char[,] MoveMatrix
        {
            get { return this.moveMatrix; }
        }

        /// <summary>
        /// The LastMove property represents the (x,y) position of the last move.
        /// </summary>
        /// <value>The LastMove property gets the value of the field lastMove.</value>
        public int[] LastMove
        {
            get { return this.lastMove; }
        }

        /// <summary>
        /// The CurrentTurn property represents the char symbol of the current player ('X' or 'O').
        /// </summary>
        /// <value>The CurrentTurn property gets/sets the value of the field currentTurn.</value>
        public char CurrentTurn
        {
            get { return this.currentTurn; }
            set { this.currentTurn = value; }
        }
    }


    /// <summary>
    /// Displays the data of a GameBoard for the user to see.
    /// </summary>
    public class BoardDrawer
    {
        GameBoard gameBoard;
        PictureBox boardPicture;

        /// <summary>
        /// Basic constructor for the BoardDrawer class.
        /// </summary>
        /// <param name="gameBoard">The GameBoard to get the data from which to draw.</param>
        /// <param name="form">The Form containing the BoardPicture Control.</param>
        public BoardDrawer(GameBoard gameBoard, PictureBox boardPicture)
        {
            this.gameBoard = gameBoard;
            this.boardPicture = boardPicture;
            boardPicture.SendToBack();
        }

        /// <summary>
        /// The BoardPicture property represents the picture box containing the actual drawing of the Tic Tac Toe 
        /// board background without any marks.
        /// </summary>
        /// <value>The BoardPicture property gets the value of the field boardPicture.</value>
        public PictureBox BoardPicture
        {
            get { return this.boardPicture; }
        }

        /// <summary>
        /// Creates and returns a marking Control with a picture that corresponds to the 
        /// specified character ('X' or 'O'), positioned at the x and y coordinates.
        /// </summary>
        /// <param name="marking">The character ('X' or 'O') that defines the marking image.</param>
        /// <param name="x">The x-coordinate (screen coordinate) of the mark.</param>
        /// <param name="y">The y-coordinate (screen coordinate) of the mark.</param>
        /// <returns>Returns the created PictureBox containing the mark image at the specified location.</returns>
        private PictureBox CreateMark(char marking, int x, int y)
        {
            PictureBox mark = new PictureBox();
            double widthScalar = 0.65;
            double heightScalar = 0.65;

            mark.SizeMode = PictureBoxSizeMode.StretchImage;
            mark.Width = (int)((boardPicture.Width / 3) * widthScalar);
            mark.Height = (int)((boardPicture.Height / 3) * heightScalar);
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

            return mark;
        }

        /// <summary>
        /// To be called after any change to the GameBoard.  Draws (creates a visible Control at) the last mark that the 
        /// GameBoard received.
        /// </summary>
        public void Update()
        {
            char mark = gameBoard.CurrentTurn == 'X' ? 'O' : 'X';
            int[] point = GetScreenPoint(gameBoard.LastMove);
            PictureBox markPicture = CreateMark(mark, point[0], point[1]);
            markPicture.Parent = boardPicture;
        }

        /// <summary>
        /// Converts a tic tac toe point into a real point on the screen relative to
        /// the background picture of the game board.
        /// </summary>
        /// <param name="point">The point (x, y) to convert.</param>
        /// <returns></returns>
        private int[] GetScreenPoint(int[] point)
        {
            if (point.Length != 2)
            {
                throw new System.ArgumentException("Array argument must be of length 2.");
            }
            int unitX = boardPicture.Width / 3;
            int unitY = boardPicture.Height / 3;
            int offsetX = boardPicture.Width / 15;
            int offsetY = boardPicture.Height / 15;
            int[] result = new int[2];
            result[0] = (point[0] * unitX + offsetX);
            result[1] = (point[1] * unitY + offsetY);
            return result;
        }
    }

    /// <summary>
    /// The AI component of the computer player.
    /// </summary>
    public class AIEngine
    {
        public int[] GetNextMove(char[,] moveMatrix)
        {


            return null;
        }

        public GameBoard Minimax(GameBoard board, int alpha, int beta, bool isTurnX)
        {
            return null;
        }

        public List<GameBoard> GenerateChildren(GameBoard board)
        {
            return null;
        }
    }


    /// <summary>
    /// The Control that is put onto a Form which contains a GameBoard and BoardDrawer, drawing
    /// the information from the GameBoard onto the form and handling click events.
    ///
    /// </summary>
    public class TicTacToeBoard : PictureBox
    {
        GameBoard gameBoard;
        BoardDrawer drawer;
        bool isFinished = false;

        /// <summary>
        /// Basic constructor.  Initializes the GameBoard and BoardDrawer, and adds a click event listener
        /// to this Control.
        /// </summary>
        public TicTacToeBoard() : base()
        {
            this.gameBoard = new GameBoard();
            this.drawer = new BoardDrawer(this.gameBoard, this);
            this.Click += (sender, e) =>
            {
                MouseEventArgs click = (MouseEventArgs)e;
                ProcessClick(click.X, click.Y);
            };
        }

        /// <summary>
        /// Receives click information from the form and acts on the GameBoard, marking the appropriate
        /// place.
        /// </summary>
        /// <param name="x">The x coordinate of the click.</param>
        /// <param name="y">The y coordinate of the click.</param>
        public void ProcessClick(int x, int y)
        {
            if (isFinished) return;

            int unitX = this.Width / 3;
            int unitY = this.Height / 3;       
            int convertedX = x / unitX;
            int convertedY = y / unitY;

            if (gameBoard.Mark(convertedX, convertedY))
            {
                char winner = gameBoard.TestBoard();
                if (winner != 'N')
                {
                    isFinished = true;
                    Debug.WriteLine(winner);
                }
                drawer.Update();
            }
        }

        /// <summary>
        /// The GameBoard for which this Control shows the data.
        /// </summary>
        public GameBoard GameBoard
        {
            get { return this.gameBoard; }
            set { gameBoard = value; }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// The main entry point for the application.
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
        /// <param name="marking">The marking to mark with; defaults to 'z' which is converted within the method to the current
        /// player's mark.</param>
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

            Utility.ActOnMatrix((i, j) =>
            {
                char mark = moveMatrix[i, j];

                if (mark == 'X')
                {
                    count++;
                }
                if (mark == 'O')
                {
                    count--;
                }
                if (mark != 'N')
                {
                    copy.Mark(j, i, moveMatrix[i, j]);
                }             
            });
                                    
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

        /// <summary>
        /// The TurnNumber represents the number of marks that have been put on the board.  Thus 0 represents the start of the game, 1 the state
        /// after the 1st move, and so forth.
        /// </summary>
        /// <value>The TurnNumber property sets the value of the turnNumber field.</value>
        public int TurnNumber
        {
            get { return turnNumber; }
        }
    }


    /// <summary>
    /// The AI component of the computer player.
    /// </summary>
    public static class AIEngine
    {

        /// <summary>
        /// Returns the best possible move for the current player of a GameBoard to make.  Uses the minimax
        /// algorithm with alpha-beta pruning to make its choice.  To provide a more life-like response, uses
        /// a random number generator to select between the best possible moves.
        /// </summary>
        /// <param name="board">The GameBoard to evalutate.</param>
        /// <returns>Returns a point (x, y) that represents the best move for the current player on the board.</returns>
        public static int[] GetNextMove(GameBoard board)
        {

            List<GameBoard> children = GenerateChildren(board);
            List<GameBoard> winners = new List<GameBoard>();
            List<GameBoard> ties = new List<GameBoard>();
            int desiredOutcome = board.CurrentTurn== 'X' ? 1 : -1;
            foreach (GameBoard child in children)
            {
                if (child.TestBoard() == board.CurrentTurn)
                {
                    return child.LastMove;
                }

                int potential = Minimax(child, -2, 2, child.CurrentTurn == 'X');

                if (potential == desiredOutcome)
                {
                    winners.Add(child);
                }
                if (potential == 0)
                {
                    ties.Add(child);
                }
            }

            Random rnd = new Random();
            int index;
            if (winners.Count > 0)
            {
                index = rnd.Next(0, winners.Count);
                return winners[index].LastMove;
            }
            index = rnd.Next(0, ties.Count);
            return ties[index].LastMove;

        }

        /// <summary>
        /// The minimax algorithm, which predicts the outcome of a board, assuming optimal playing from both sides.
        /// Returns 1 for a predicted 'X' win, 0 for a predicted tie, and -1 for a predicted 'O' win. Uses alpha-beta
        /// pruning to speed up the algorithm.
        /// </summary>
        /// <param name="board">The GameBoard to evaluate.</param>
        /// <param name="alpha">The minimum score that player X is currently assured of.</param>
        /// <param name="beta">The maximum score that player O is currently assured of.</param>
        /// <param name="isTurnX"></param>
        /// <returns></returns>
        public static int Minimax(GameBoard board, int alpha, int beta, bool isTurnX)
        {
            char winner = board.TestBoard();
            if (winner == 'X') { return 1; }
            if (winner == 'O') { return -1; }
            if (winner == 'T') { return 0; }

            List<GameBoard> children = GenerateChildren(board);
            if (isTurnX)
            {
                int v = -2;
                foreach (GameBoard child in children)
                {
                    v = Math.Max(v, Minimax(child, alpha, beta, false));
                    alpha = Math.Max(alpha, v);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return v;
            }
            else
            {
                int v = 2;
                foreach (GameBoard child in children)
                {
                    v = Math.Min(v, Minimax(child, alpha, beta, true));
                    beta = Math.Min(beta, v);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return v;
            }

        }

        /// <summary>
        /// Gets all potential moves of the specified game board for the current player, in the form of a 
        /// List of GameBoards where each board has one of the moves performed.
        /// </summary>
        /// <param name="board">The GameBoard whose children should be generated.</param>
        /// <returns>Returns a List of GameBoards, each representing possible states that the specified GameBoard could reach.</returns>
        public static List<GameBoard> GenerateChildren(GameBoard board)
        {
            List<GameBoard> children = new List<GameBoard>();

            Utility.ActOnMatrix((i, j) =>
            {
                if (board.MoveMatrix[i, j] == 'N')
                {
                    GameBoard copy = board.Duplicate();
                    copy.Mark(j, i);
                    children.Add(copy);
                }
            });

            return children;
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
        bool isFinished = false;
        PictureBox[,] markMatrix;
        bool computerStart = false;
        bool playAsX = true;
        System.Drawing.Bitmap xImage = TicTacToeEngine.Properties.Resources.TicTacToeX;
        System.Drawing.Bitmap oImage = TicTacToeEngine.Properties.Resources.TicTacToeO;
        Label message;


        const double markWidthScalar = 0.65;
        const double markHeightScalar = 0.65;

        /// <summary>
        /// Basic constructor.  Initializes the GameBoard and BoardDrawer, adds a click event listener
        /// to this Control, and creates the mark picture boxes.
        /// </summary>
        public TicTacToeBoard() : base()
        {

            this.gameBoard = new GameBoard();
            
            this.Click += (sender, e) =>
            {
                MouseEventArgs click = (MouseEventArgs)e;
                ProcessClick(click.X, click.Y);          
            };

            this.Image = TicTacToeEngine.Properties.Resources.TicTaceToeBoard;
            markMatrix = new PictureBox[gameBoard.MoveMatrix.GetLength(0), gameBoard.MoveMatrix.GetLength(1)];
        }

        /// <summary>
        /// Method called after the control is created.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Utility.ActOnMatrix((i, j) => markMatrix[i, j] = CreateMark(j, i));
            Reset();
              
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

            convertedX = (convertedX > 2) ? 2 : convertedX;
            convertedY = (convertedY > 2) ? 2 : convertedY;

            Mark(convertedX, convertedY);
            
            if (!isFinished)
            {
                int[] move = AIEngine.GetNextMove(gameBoard);
                Mark(move[0], move[1]);
            }
        }

        /// <summary>
        /// Marks the GameBoard and updates graphics.
        /// </summary>
        /// <param name="x">The x-coordinate of the space to mark.</param>
        /// <param name="y"><The y-coordinate of the space to mark./param>

        public void Mark(int x, int y)
        {
            if (gameBoard.Mark(x, y))
            {
                char winner = gameBoard.TestBoard();
                if (winner != 'N')
                {
                    isFinished = true;
                    DisplayResult(winner);              
                }
                UpdateMarks();
            }
        }

        /// <summary>
        /// Creates and returns a marking Control with a picture that corresponds to the 
        /// specified character ('X' or 'O'), positioned at the x and y coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate (screen coordinate) of the mark.</param>
        /// <param name="y">The y-coordinate (screen coordinate) of the mark.</param>
        /// <returns>Returns the created PictureBox containing the mark image at the specified location.</returns>
        private PictureBox CreateMark(int x, int y)
        {
            PictureBox mark = new PictureBox();
            mark.BackColor = System.Drawing.Color.Transparent;
            mark.SizeMode = PictureBoxSizeMode.StretchImage;
            int[] point = GetScreenPoint(new int[] { x, y });
            mark.Width = (int)((this.Width / 3) * markWidthScalar);
            mark.Height = (int)((this.Height / 3) * markHeightScalar);
            mark.Left = point[0];
            mark.Top = point[1];


            mark.Visible = false;
            mark.Parent = this;
            return mark;
        }

        /// <summary>
        /// Updates the mark images to reflect the data of the gameBoard field.
        /// </summary>
        public void UpdateMarks()
        {
            Utility.ActOnMatrix((i, j) =>
            {
                char mark = gameBoard.MoveMatrix[i, j];
                System.Drawing.Bitmap primaryImage = playAsX ? xImage : oImage;
                System.Drawing.Bitmap secondaryImage = playAsX ? oImage : xImage;
                if (markMatrix[i, j] == null)
                {
                    return;
                }
                switch (mark)
                {
                    case ('N'):
                        markMatrix[i, j].Visible = false;
                        break;
                    case ('X'):
                        markMatrix[i, j].Image = primaryImage;
                        markMatrix[i, j].Visible = true;
                        break;
                    case ('O'):
                        markMatrix[i, j].Image = secondaryImage;
                        markMatrix[i, j].Visible = true;
                        break;
                }
            });                          
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
            int unitX = this.Width / 3;
            int unitY = this.Height / 3;
            int offsetX = this.Width / 15;
            int offsetY = this.Height / 15;
            int[] result = new int[2];
            result[0] = (point[0] * unitX + offsetX);
            result[1] = (point[1] * unitY + offsetY);
            return result;
        }

        /// <summary>
        /// Resets the game board.
        /// </summary>
        public void Reset(bool computerStart=false, bool playAsX=true)
        {
            isFinished = false;
            this.GameBoard = new GameBoard();
            this.playAsX = playAsX;
            this.ComputerStart = computerStart;
            message.Text = "";
            if (computerStart)
            {
                int[] move = AIEngine.GetNextMove(this.GameBoard);
                Mark(move[0], move[1]);
            }
        }

        public void DisplayResult(char result)
        {
            Debug.WriteLine(result);
            if (message == null)
            {
                return;
            }
            if (result == 'T')
            {
                message.Text = "It's a tie!";
            }
            else
            {
                if (!playAsX)
                {
                    result = result == 'X' ? 'O' : 'X';
                }
                message.Text = result + " wins!";
            }
        }

        public Label ResultLabel
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// The GameBoard property represents the data for which spaces are marked, which this control draws.
        /// </summary>
        ///<value>Sets/gets the value of the gameBoard field.  NOTE: when setting, calls the method to update
        ///mark graphics.</value> 

        public GameBoard GameBoard
        {
            get { return this.gameBoard; }
            set { gameBoard = value;
                UpdateMarks();
            }
        }

        /// <summary>
        /// The ComputerStart property represents whether the computer starts the game or not..
        /// </summary>
        ///<value>Sets/gets the value of the computerStart field.
        ///mark graphics.</value> 
        public bool ComputerStart
        {
            get { return this.computerStart; }
            set { this.computerStart = value; }
        }



    }

    /// <summary>
    /// A Utility class, which contains useful methods for all classes.
    /// </summary>
    public class Utility
    {

        public delegate void ActMethod(int i, int j);
        /// <summary>
        /// Performs the specified method (conforming to the ActMethod delegate) on each
        /// element of the matrix.  Essentially, this is shorthand for the nested loop through
        /// the matrix that I see repeatedly in this project.
        /// </summary>
        /// <param name="method">The method to apply.</param>
        /// <param name="width">The (optional) width of the matrix, defaulting to 3.</param>
        /// <param name="height">The (optional) height of the matrix, defaulting to 3.</param>
        public static void ActOnMatrix(ActMethod method, int width=3, int height=3)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    method(i, j);
                }
            }
        }
    }
}

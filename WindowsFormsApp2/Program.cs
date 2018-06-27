using System;
using System.Collections.Generic;
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
            Application.Run(new MainForm());

            GameBoard gameBoard = new GameBoard();

        }
    }

    class GameBoard
    {
        char[,] moveMatrix = GetEmptyMatrix();

        char currentTurn = 'X';

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
    }

    class BoardDrawer
    {
        public BoardDrawer(GameBoard gameBoard, PictureBox box)
        {
            PictureBox[] xMarkPool = new PictureBox[5];
            PictureBox[] oMarkPool = new PictureBox[4];

            for (int i = 0; i < xMarkPool.Length; i++)
            {
                xMarkPool[i] = CreateMark('X');
            }

            for (int j = 0; j < oMarkPool.Length; j++)
            {
                oMarkPool[j] = CreateMark('O');
            }
        }

        private static PictureBox CreateMark(char marking)
        {
            PictureBox mark = new PictureBox();
            mark.SizeMode = PictureBoxSizeMode.StretchImage;
            mark.Width = 50;
            mark.Height = 50;
            mark.Visible = false;
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

        public void update()
        {
            
        }
    }

}

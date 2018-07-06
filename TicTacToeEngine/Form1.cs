using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe;

namespace TicTacToeEngine
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            bool computerStart = computerStartBox.Checked;
            bool playChoice = playerChoiceBox.Checked;

            ticTacToeBoard1.Reset(computerStart, playChoice);
        }

        private void ticTacToeBoard1_Load(object sender, EventArgs e)
        {
            ((TicTacToeBoard)sender).ResultLabel = label1;
        }
    }
}

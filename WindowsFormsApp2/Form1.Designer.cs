namespace TicTacToeEngine
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TicTacToe.GameBoard gameBoard2 = new TicTacToe.GameBoard();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ticTacToeBoard1 = new TicTacToe.TicTacToeBoard();
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard1)).BeginInit();
            this.SuspendLayout();
            // 
            // ticTacToeBoard1
            // 
            gameBoard2.CurrentTurn = 'X';
            this.ticTacToeBoard1.GameBoard = gameBoard2;
            this.ticTacToeBoard1.Image = global::TicTacToeEngine.Properties.Resources.TicTaceToeBoard;
            this.ticTacToeBoard1.Location = new System.Drawing.Point(33, 28);
            this.ticTacToeBoard1.Name = "ticTacToeBoard1";
            this.ticTacToeBoard1.Size = new System.Drawing.Size(512, 512);
            this.ticTacToeBoard1.TabIndex = 0;
            this.ticTacToeBoard1.TabStop = false;
            this.ticTacToeBoard1.Click += new System.EventHandler(this.ticTacToeBoard1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 605);
            this.Controls.Add(this.ticTacToeBoard1);
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TicTacToe.TicTacToeBoard ticTacToeBoard1;
    }
}


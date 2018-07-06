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
            TicTacToe.GameBoard gameBoard1 = new TicTacToe.GameBoard();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ticTacToeBoard1 = new TicTacToe.TicTacToeBoard();
            this.resetButton = new System.Windows.Forms.Button();
            this.playerChoiceBox = new System.Windows.Forms.CheckBox();
            this.computerStartBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard1)).BeginInit();
            this.SuspendLayout();
            // 
            // ticTacToeBoard1
            // 
            this.ticTacToeBoard1.ComputerStart = false;
            gameBoard1.CurrentTurn = 'X';
            this.ticTacToeBoard1.GameBoard = gameBoard1;
            this.ticTacToeBoard1.Image = global::TicTacToeEngine.Properties.Resources.TicTaceToeBoard;
            this.ticTacToeBoard1.Location = new System.Drawing.Point(22, 27);
            this.ticTacToeBoard1.Name = "ticTacToeBoard1";
            this.ticTacToeBoard1.ResultLabel = this.label1;
            this.ticTacToeBoard1.Size = new System.Drawing.Size(512, 512);
            this.ticTacToeBoard1.TabIndex = 0;
            this.ticTacToeBoard1.TabStop = false;
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(641, 75);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(91, 37);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // playerChoiceBox
            // 
            this.playerChoiceBox.AutoSize = true;
            this.playerChoiceBox.Checked = true;
            this.playerChoiceBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.playerChoiceBox.Location = new System.Drawing.Point(641, 138);
            this.playerChoiceBox.Name = "playerChoiceBox";
            this.playerChoiceBox.Size = new System.Drawing.Size(70, 17);
            this.playerChoiceBox.TabIndex = 2;
            this.playerChoiceBox.Text = "Play as X";
            this.playerChoiceBox.UseVisualStyleBackColor = true;
            // 
            // computerStartBox
            // 
            this.computerStartBox.AutoSize = true;
            this.computerStartBox.Location = new System.Drawing.Point(641, 184);
            this.computerStartBox.Name = "computerStartBox";
            this.computerStartBox.Size = new System.Drawing.Size(107, 17);
            this.computerStartBox.TabIndex = 3;
            this.computerStartBox.Text = "Computer Starts?";
            this.computerStartBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(572, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 42);
            this.label1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 605);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.computerStartBox);
            this.Controls.Add(this.playerChoiceBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.ticTacToeBoard1);
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TicTacToe.TicTacToeBoard ticTacToeBoard1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.CheckBox playerChoiceBox;
        private System.Windows.Forms.CheckBox computerStartBox;
        private System.Windows.Forms.Label label1;
    }
}


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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ticTacToeBoard2 = new TicTacToe.TicTacToeBoard();
            this.ticTacToeBoard1 = new TicTacToe.TicTacToeBoard();
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard1)).BeginInit();
            this.SuspendLayout();
            // 
            // ticTacToeBoard2
            // 
            this.ticTacToeBoard2.Image = global::TicTacToeEngine.Properties.Resources.TicTaceToeBoard;
            this.ticTacToeBoard2.Location = new System.Drawing.Point(28, 129);
            this.ticTacToeBoard2.Name = "ticTacToeBoard2";
            this.ticTacToeBoard2.Size = new System.Drawing.Size(300, 300);
            this.ticTacToeBoard2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ticTacToeBoard2.TabIndex = 1;
            this.ticTacToeBoard2.TabStop = false;

            // 
            // ticTacToeBoard1
            // 
            this.ticTacToeBoard1.Image = global::TicTacToeEngine.Properties.Resources.TicTaceToeBoard;
            this.ticTacToeBoard1.Location = new System.Drawing.Point(360, 24);
            this.ticTacToeBoard1.Name = "ticTacToeBoard1";
            this.ticTacToeBoard1.Size = new System.Drawing.Size(512, 512);
            this.ticTacToeBoard1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ticTacToeBoard1.TabIndex = 0;
            this.ticTacToeBoard1.TabStop = false;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 605);
            this.Controls.Add(this.ticTacToeBoard2);
            this.Controls.Add(this.ticTacToeBoard1);
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ticTacToeBoard1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TicTacToe.TicTacToeBoard ticTacToeBoard2;
        private TicTacToe.TicTacToeBoard ticTacToeBoard1;
    }
}


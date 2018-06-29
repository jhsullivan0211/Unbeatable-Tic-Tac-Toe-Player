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
            this.BoardPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BoardPicture
            // 
            this.BoardPicture.BackColor = System.Drawing.Color.Transparent;
            this.BoardPicture.Image = global::TicTacToeEngine.Properties.Resources.TicTaceToeBoard;
            this.BoardPicture.Location = new System.Drawing.Point(50, 66);
            this.BoardPicture.Name = "BoardPicture";
            this.BoardPicture.Size = new System.Drawing.Size(512, 512);
            this.BoardPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BoardPicture.TabIndex = 0;
            this.BoardPicture.TabStop = false;
            this.BoardPicture.Click += new System.EventHandler(this.BoardPicture_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 605);
            this.Controls.Add(this.BoardPicture);
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox BoardPicture;
    }
}


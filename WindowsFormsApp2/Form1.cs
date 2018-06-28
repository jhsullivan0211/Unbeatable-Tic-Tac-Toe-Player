using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        TicTacToe.InputReceiver receiver;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BoardPicture_Click(object sender, EventArgs e)
        {
            MouseEventArgs click = (MouseEventArgs) e;

            if (receiver == null) return;
            receiver.ProcessClick(click.Location.X, click.Location.Y);
        }

        public void SetInputReceiver(InputReceiver receiver)
        {
            this.receiver = receiver;
        }
        
    }
}

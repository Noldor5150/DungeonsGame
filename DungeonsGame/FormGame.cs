using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonsGame
{
    public delegate void delClearSplash();
    public partial class FormGame : Form
    {
        MainBoard board;
        public FormGame()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
          board  = new MainBoard(panelGame, StartClearSplash);
        }

        private void aboutGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bledifull bledifull Game!!!");
        }

        private void aboutCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by C# Wizzards Guild");
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    board.MoveHero(Arrows.left);
                    break;
                case Keys.Right:
                    board.MoveHero(Arrows.right);
                    break;
                case Keys.Up:
                    board.MoveHero(Arrows.up);
                    break;
                case Keys.Down:
                    board.MoveHero(Arrows.down);
                    break;
                case Keys.Space:
                    board.CreateTrap();
                    break;
            }
        }

        private void timerSplashClear_Tick(object sender, EventArgs e)
        {
            board.ClearSplash();
            timerSplashClear.Enabled = false;

        }
        private void StartClearSplash()
        {
            timerSplashClear.Enabled = true;
        }
    }
}

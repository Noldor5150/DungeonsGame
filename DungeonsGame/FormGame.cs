using System;
using System.Windows.Forms;

namespace DungeonsGame
{
    public delegate void delClearSplash();
    public partial class FormGame : Form
    {
        MainBoard board;
        int level = 1;
        public FormGame()
        {
            InitializeComponent();
            NewGame();
        }
        private void NewGame()
        {
          board  = new MainBoard(panelGame, StartClearSplash, Score);
          ChangeLevel(level);
          timerGameOver.Enabled = true;
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
            if (timerGameOver.Enabled) {
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
        private void timerGameOver_Tick(object sender, EventArgs e)
        {
            if (board.GameOver())
            {
                timerGameOver.Enabled = false;
                DialogResult result = MessageBox.Show("Want some more?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if ( result == System.Windows.Forms.DialogResult.Yes)
                {
                    NewGame();
                }
            }
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void quitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChangeLevel(int levelStep)
        {
            level = levelStep;
            board.SetEnemyLevel(level);
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChangeLevel(1);
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChangeLevel(2);
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ChangeLevel(3);
        }
    }
}

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
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            MainBoard board = new MainBoard(panelGame);
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
    }
}

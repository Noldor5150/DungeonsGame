using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonsGame
{
    class Enemy
    {
        PictureBox enemy;
        Timer timer;
        public Enemy( PictureBox picEnemy)
        {
            enemy = picEnemy;
            CreateTimer();
            timer.Enabled = true;
        }

        private void CreateTimer()
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;    
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            enemy.Location = new Point(enemy.Location.X, enemy.Location.Y - 5);
        }
    }
}

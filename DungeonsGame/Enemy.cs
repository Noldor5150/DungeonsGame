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
        Point destination;
        Point enemyPlace;
        Movement movement;
        int step = 3;

        public Enemy( PictureBox picEnemy, PictureBox[,]mapPic, State[,] map)
        {
            enemy = picEnemy;
            movement = new Movement(picEnemy, mapPic, map);
            enemyPlace = movement.MyNowPoint();
            destination = new Point(15, 7);
            CreateTimer();
            timer.Enabled = true;
        }

        private void CreateTimer()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;    
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (enemyPlace == destination)
            
                return;
                MoveEnemy(destination);
            
        }

        private void MoveEnemy(Point newPlace)
        {
            int x = 0;
            int y = 0;
            if (enemyPlace.X < newPlace.X)
            {
                x = newPlace.X - enemyPlace.X > step ? step : newPlace.X - enemyPlace.X;
            }
            else
            {
                x = enemyPlace.X - newPlace.X < step ? newPlace.X - enemyPlace.X : -step;
            }
            if (enemyPlace.Y < newPlace.Y)
            {
                y = newPlace.Y - enemyPlace.Y > step ? step : newPlace.Y - enemyPlace.Y;
            }
            else
            {
                y = enemyPlace.Y - newPlace.Y < step ? newPlace.Y - enemyPlace.Y : -step;   
            }
            movement.MoveByStep(x, y);
            enemyPlace = movement.MyNowPoint();
        }
    }
}

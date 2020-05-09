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
        State[,] map;
        Movement movement;
        int step = 3;
        int[,] fmap;
        int paths;
        Point[] mileStone;
        int pathStep;
        static Random rand = new Random();


        public Enemy(PictureBox picEnemy, PictureBox[,] mapPic, State[,] map)
        {
            enemy = picEnemy;
            this.map = map;
            fmap = new int[map.GetLength(0) , map.GetLength(1)];
            mileStone = new Point[map.GetLength(0) * map.GetLength(1)];
            movement = new Movement(picEnemy, mapPic, map);
            enemyPlace = movement.MyNowPoint();
            destination = enemyPlace;
            CreateTimer();
            timer.Enabled = true;
        }

        private void CreateTimer()
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (enemyPlace == destination) GetNewPlace();
            if (mileStone[0].X == 0 & mileStone[0].Y == 0)
                if (!FindPath()) return;
            if (pathStep > paths) return;
            if (mileStone[pathStep] == enemyPlace) 
                pathStep++;
            else
                MoveEnemy(mileStone[pathStep]);
            
        }

        private void MoveEnemy(Point newPlace)
        {
            int x ;
            int y ;
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

        private bool FindPath()
        {

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    fmap[x, y] = 0;
                }
            }
            bool pathAdded;
            bool pathFound = false;
            fmap[enemyPlace.X, enemyPlace.Y] = 1;
            int pathNumber = 1;
            do
            {
                pathAdded = false;
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        if (fmap[x, y] == pathNumber)
                        {
                            MarkPath(x + 1, y, pathNumber + 1);
                            MarkPath(x - 1, y, pathNumber + 1);
                            MarkPath(x, y - 1, pathNumber + 1);
                            MarkPath(x, y + 1, pathNumber + 1);
                            pathAdded = true;
                        }
                    }
                }
                if (fmap[destination.X, destination.Y] > 0)
                {
                    pathFound = true;
                    break;
                }
                pathNumber++;
            } while (pathAdded);
            if (!pathFound)
            {
                return false;
            }
            int sx = destination.X;
            int sy = destination.Y;
            paths = pathNumber;
            while (pathNumber >= 0)
            {
                mileStone[pathNumber].X = sx;
                mileStone[pathNumber].Y = sy;

                if (IsItLegitPath(sx + 1, sy, pathNumber))
                {
                    sx++;
                }
                else if (IsItLegitPath(sx - 1, sy, pathNumber))
                {
                    sx--;
                }
                else if (IsItLegitPath(sx, sy + 1, pathNumber))
                {
                    sy++;
                }
                else if (IsItLegitPath(sx, sy - 1, pathNumber))
                {
                    sy--;
                }
                pathNumber--;
            }
            pathStep = 0;
            return true;
        }

        private void MarkPath(int x, int y, int pathNumber)
        {
            if (x < 0 || x >= map.GetLength(0))
            {
                return;
            }
            if (y < 0 || y >= map.GetLength(1))
            {
                return;
            }
            if (fmap[x, y] > 0)
            {
                return;
            }
            if (map[x,y] != State.empty)
            {
                return;
            }
            fmap[x, y] = pathNumber;
        }

        private bool IsItLegitPath(int x, int y, int pathNumber)
        {
            if (x < 0 || x >= map.GetLength(0))
            {
                return false;

            }
            if (y < 0 || y >= map.GetLength(1))
            {
                return false;
            }
          return fmap [x,y] == pathNumber;
        }

        private void GetNewPlace()
        {
            int loop = 0;
            do
            {
                destination.X = rand.Next(1, map.GetLength(0) - 1);
                destination.Y = rand.Next(1, map.GetLength(1) - 1);
            } while (!FindPath() && loop++ < 100);

            if(loop >= 100)
            {
                destination = enemyPlace;
            }
        }
    }
}

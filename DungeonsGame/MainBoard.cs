using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonsGame
{   
    public delegate void delSplash(Trap trap);
    enum State
    {
        empty,
        wall,
        barrel,
        iceball,
        splash,
        bonus
    }
    class MainBoard
    {
        Panel panelGame;
        PictureBox[,] mapPic;
        State[,] map;
        int sizeX = 17;
        int sizeY = 11;
        static Random rand = new Random();
        Hero hero;
        List <Enemy> enemies;
        public MainBoard(Panel panel)
        {
            panelGame = panel;
            enemies = new List<Enemy>();
            int boxSize;

            if ((panelGame.Width / sizeX) < (panelGame.Height / sizeY))
            {
                boxSize = panelGame.Width / sizeX;
            }
            else
            {
                boxSize = panelGame.Height / sizeY;
            }
            InitStartMap(boxSize);
            InitStartHero(boxSize);
            for (int i = 0; i < 6; i++)
            {
                InitStartEnemy(boxSize);
            }
            
        }

        private void InitStartMap(int boxSize)
        {
            mapPic = new PictureBox[sizeX, sizeY];
            panelGame.Controls.Clear();
            map = new State[sizeX, sizeY];

        
            for(int x = 0; x<sizeX; x++)
            {
                for (int y = 0; y< sizeY; y++)
                {
                    if(x == 0 || y == 0 || x==sizeX-1 || y == sizeY-1)
                    {
                        CreatePlace(new Point(x, y), boxSize, State.wall);
                    }
                    else if (x % 2 == 0 && y % 2 == 0)
                    {
                        CreatePlace(new Point(x, y), boxSize, State.wall);
                    }
                    else if (rand.Next(3) == 0)
                    {
                        CreatePlace(new Point(x, y), boxSize, State.barrel);
                    }
                    else
                    {
                        CreatePlace(new Point(x, y), boxSize, State.empty);
                    }
                }
            }
            ChangeState(new Point(1, 1), State.empty);
            ChangeState(new Point(1, 2), State.empty);
            ChangeState(new Point(2, 1), State.empty);
        }
        private void CreatePlace(Point point, int boxSize, State state)
        {
            PictureBox picture = new PictureBox();

            picture.Location = new Point(point.X*(boxSize -1), point.Y*(boxSize-1));
            picture.Size = new Size(boxSize, boxSize);
            /*picture.BorderStyle = BorderStyle.FixedSingle;*/
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            mapPic[point.X, point.Y] = picture;
            ChangeState(point, state);
            panelGame.Controls.Add(picture);

           /* picture.BackColor = Color.Azure;*/

        }
        private void ChangeState(Point point, State newState)
        {
            switch (newState)
            {
                
                case State.wall:
                    mapPic[point.X, point.Y].Image = Properties.Resources.wall2;
                    break;
                case State.barrel:
                    mapPic[point.X, point.Y].Image = Properties.Resources.barrel;
                    break;
                case State.iceball:
                    mapPic[point.X, point.Y].Image = Properties.Resources.iceball;
                    break;
                case State.splash:
                    mapPic[point.X, point.Y].Image = Properties.Resources.splash;
                    break;
                case State.bonus:
                    mapPic[point.X, point.Y].Image = Properties.Resources.ring;
                    break;
                default:
                    mapPic[point.X, point.Y].Image = Properties.Resources.ground;
                    break;
            }
            map[point.X, point.Y] = newState;
        }
        private void InitStartHero(int boxSize)
        {
            int x = 1;
            int y = 1;
            PictureBox picture = new PictureBox();
            picture.Location = new Point(x * (boxSize) + 7, y * (boxSize) + 3);
            picture.Size = new Size(boxSize - 14, boxSize - 6);
            picture.Image = Properties.Resources.Gandalf;
            picture.BackgroundImage = Properties.Resources.ground;
            picture.BackgroundImageLayout = ImageLayout.Stretch;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            panelGame.Controls.Add(picture);
            picture.BringToFront();
            hero = new Hero(picture, mapPic, map);

        }

        private void InitStartEnemy(int boxSize)
        {
            int x = 15;
            int y = 9;
            FindEmptyPlace(out x, out y);
            PictureBox picture = new PictureBox();
            picture.Location = new Point(x * (boxSize) - 8, y * (boxSize) - 6);
            picture.Size = new Size(boxSize - 14, boxSize - 5);
            picture.Image = Properties.Resources.dragonGreen;
            picture.BackgroundImage = Properties.Resources.ground;
            picture.BackgroundImageLayout = ImageLayout.Stretch;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            panelGame.Controls.Add(picture);
            picture.BringToFront();
            enemies.Add ( new Enemy(picture,mapPic,map));

        }
         
        private void FindEmptyPlace(out int x, out int y)
        {
            int loop = 0;
            do
            {
                x = rand.Next(map.GetLength(0)/2, map.GetLength(0));
                y = rand.Next(1, map.GetLength(1));

            } while (map[x,y] != State.empty && loop++ < 100);
        }


        public void MoveHero(Arrows  arrow)
        {
            if(hero == null)
            {
                return;
            }
            hero.MoveHero(arrow);
        }

        public void CreateTrap()
        {
            Point heroPoint = hero.MyNowPoint();
            if (map [heroPoint.X, heroPoint.Y] == State.iceball)       
            {
                return;
            }
            if (hero.CanCreateTrap(mapPic, Splash))
            {
                ChangeState(hero.MyNowPoint(), State.iceball);
            }
        }
         
        private void Splash(Trap trap)
        {
            ChangeState(trap.trapPlace, State.splash);
            RenderSplash(trap.trapPlace, Arrows.left);
            RenderSplash(trap.trapPlace, Arrows.right);
            RenderSplash(trap.trapPlace, Arrows.up);
            RenderSplash(trap.trapPlace, Arrows.down);
            hero.traps.Remove(trap);
            FreezeToDeath();

        }

        private void FreezeToDeath()
        {

            List<Enemy> deadEnemies = new List<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                Point enemyPoint = enemy.MyNowPoint();
                if (map[enemyPoint.X, enemyPoint.Y] == State.splash )
                {
                    deadEnemies.Add(enemy);
                }
            }
            for (int i = 0; i < deadEnemies.Count; i++)
            {
                enemies.Remove(deadEnemies[i]);
                panelGame.Controls.Remove(deadEnemies[i].enemy);
                deadEnemies[i] = null;
            }
        }

        private void RenderSplash(Point trapPlace, Arrows arrow)
        {
            int sx = 0;
            int sy = 0;

            switch (arrow)
            {
                case Arrows.left:
                    sx = -1;
                    break;
                case Arrows.right:
                    sx = 1;
                    break;
                case Arrows.up:
                    sy = -1;
                    break;
                case Arrows.down:
                    sy = 1;
                    break;
                default:
                    break;
            }

            bool isNotComplete = true;
            int x = 0;
            int y = 0;
            do
            {
                x += sx;
                y += sy;
                if (Math.Abs(x)  > hero.splashLength || Math.Abs(y) > hero.splashLength)
                {
                    break;
                }
                if (IsSplashActive(trapPlace, x, y))
                {
                    ChangeState(new Point(trapPlace.X + x, trapPlace.Y + y), State.splash);
                }
                else
                {
                    isNotComplete = false;
                }
            } while (isNotComplete);
        }

        private bool IsSplashActive(Point place, int x, int y)
        {
            switch (map[place.X + x, place.Y + y])
            {
                case State.empty:
                    return true;
                case State.wall:
                    return false;
                case State.barrel:
                    ChangeState(new Point(place.X + x, place.Y + y), State.splash);
                    return false;
                case State.iceball:
                    foreach (Trap trap in hero.traps)
                    {
                        if (trap.trapPlace == new Point(place.X + x, place.Y + y))
                        {
                            trap.TrapReaction();
                        }
                    }
                    return false;        
                default:
                    return true;
            }
        }
    }
}

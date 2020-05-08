using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonsGame
{
    enum Arrows
    {
        left,
        right,
        up,
        down
    }
    class Hero
    {

        PictureBox hero;
        int step;
        PictureBox[,] mapPic;
        State[,] map;
        public Hero(PictureBox hero, PictureBox[,] mapPic, State[,] map)
        {
            this.hero = hero;
            this.mapPic = mapPic;
            this.map = map;
            step = 3;
        }


        public void MoveHero(Arrows arrow)
        {
            switch (arrow)
            {
                case Arrows.left:
                    MoveByStep(-step, 0);
                    break;
                case Arrows.right:
                    MoveByStep(step, 0);
                    break;
                case Arrows.up:
                    MoveByStep(0, -step);
                    break;
                case Arrows.down:
                    MoveByStep(0, step);
                    break;
                default:
                    break;
            }
        }

        private void MoveByStep(int x, int y)
        {
            if (IsEmpty(ref x, ref y))
            {
                hero.Location = new Point(hero.Location.X + x, hero.Location.Y + y);
            }
           
        }

        private bool IsEmpty(ref int x, ref int y)
        {
            Point heroPoint = MyNowPoint();
            int heroRightSide = hero.Location.X + hero.Size.Width;
            int heroLeftSide = hero.Location.X;
            int heroBottomSide = hero.Location.Y + hero.Size.Height;
            int heroTopSide = hero.Location.Y;

            int rightObstacleLeftSide = mapPic[heroPoint.X + 1, heroPoint.Y].Location.X;
            int leftObstacleRightSide = mapPic[heroPoint.X - 1, heroPoint.Y].Location.X + mapPic[heroPoint.X - 1, heroPoint.Y].Size.Width;
            int bottomObstacleTopSide = mapPic[heroPoint.X, heroPoint.Y + 1].Location.Y;
            int topObstacleBottomSide = mapPic[heroPoint.X, heroPoint.Y - 1].Location.Y + mapPic[heroPoint.X, heroPoint.Y - 1].Size.Height;

            int rightTopObstacleBottomSide = mapPic[heroPoint.X + 1, heroPoint.Y - 1].Location.Y + mapPic[heroPoint.X + 1, heroPoint.Y - 1].Size.Height;
            int rightBottomObstacleTopSide = mapPic[heroPoint.X + 1, heroPoint.Y + 1].Location.Y;
            int leftTopObstacleBottomSide = mapPic[heroPoint.X - 1, heroPoint.Y - 1].Location.Y + mapPic[heroPoint.X - 1, heroPoint.Y - 1].Size.Height;
            int leftBottomObstacleTopSide = mapPic[heroPoint.X - 1, heroPoint.Y + 1].Location.Y;


            int rightTopObstacleLeftSide = mapPic[heroPoint.X + 1, heroPoint.Y - 1].Location.X;
            int leftTopObstacleRightSide = mapPic[heroPoint.X - 1, heroPoint.Y - 1].Location.X + mapPic[heroPoint.X - 1, heroPoint.Y - 1].Size.Width;
            int rightBottomObstacleLeftSide = mapPic[heroPoint.X + 1, heroPoint.Y + 1].Location.X;
            int leftBottomObstacleRightSide = mapPic[heroPoint.X - 1, heroPoint.Y + 1].Location.X + mapPic[heroPoint.X - 1, heroPoint.Y + 1].Size.Width;

            int offset = 3;

            if ( x > 0 && map[heroPoint.X + 1 , heroPoint.Y] == State.empty)
            {
                if(heroTopSide < rightTopObstacleBottomSide)
                {
                    if (rightTopObstacleBottomSide - heroTopSide > offset)
                    {
                        y = offset;
                    }
                    else
                    {
                        y = rightTopObstacleBottomSide - heroTopSide;
                    }
                }
                if (heroBottomSide > rightBottomObstacleTopSide)
                {
                    if (rightBottomObstacleTopSide - heroBottomSide < offset)
                    {
                        y = -offset;
                    }

                    else
                    {
                        y = rightBottomObstacleTopSide - heroBottomSide;
                    }
                }
                return true;
            }
            if (x < 0 && map[heroPoint.X - 1, heroPoint.Y] == State.empty)
            {
                if (heroTopSide < leftTopObstacleBottomSide)
                {
                    if (leftTopObstacleBottomSide - heroTopSide > offset)
                    {
                        y = offset;
                    }
                    else
                    {
                        y = leftTopObstacleBottomSide - heroTopSide;
                    }
                    
                }
                if (heroBottomSide > leftBottomObstacleTopSide)
                {
                    if (leftBottomObstacleTopSide - heroBottomSide < -offset)
                    {
                        y = -offset;
                    }
                    else
                    {
                        y = leftBottomObstacleTopSide - heroBottomSide;
                    }
                }
                return true;
            }
            if (y > 0 && map[heroPoint.X, heroPoint.Y + 1] == State.empty)
            {
                if (heroRightSide > rightBottomObstacleLeftSide)
                {
                    if (rightBottomObstacleLeftSide - heroRightSide < -offset)
                    {
                        x = -offset;
                    }
                    else
                    {
                        x = rightBottomObstacleLeftSide - heroRightSide;
                    }
                }
                if (heroLeftSide < leftBottomObstacleRightSide)
                {
                    if (leftBottomObstacleRightSide - heroLeftSide > offset)
                    {
                        x = offset;
                    }
                    else
                    {
                        x = leftBottomObstacleRightSide - heroLeftSide;
                    }
                }
                return true;
            }
            if (y < 0 && map[heroPoint.X, heroPoint.Y - 1] == State.empty)
            {
                if (heroRightSide > rightTopObstacleLeftSide)
                {
                    if (rightTopObstacleLeftSide-heroRightSide < -offset)
                    {
                        x = -offset;
                    }
                    else
                    {
                        x = rightTopObstacleLeftSide - heroRightSide;
                    }
                }
                if (heroLeftSide < leftTopObstacleRightSide)
                {
                    if (leftTopObstacleRightSide - heroLeftSide > offset)
                    {
                        x = offset;
                    }
                    else
                    {
                        x = leftTopObstacleRightSide - heroLeftSide;
                    }
                }
                return true;
            }
           

            if (x > 0 && heroRightSide + x > rightObstacleLeftSide)
            {
                x = rightObstacleLeftSide - heroRightSide;
            }
            if (x < 0 && heroLeftSide + x < leftObstacleRightSide)
            {
                x = leftObstacleRightSide - heroLeftSide;
            }
            if (y > 0 && heroBottomSide + y > bottomObstacleTopSide)
            {
                y = bottomObstacleTopSide - heroBottomSide;
            }
            if (y < 0 && heroTopSide + y < topObstacleBottomSide)
            {
                y = topObstacleBottomSide - heroTopSide;
            }


            return true;
        }
        private Point MyNowPoint()
        {
            Point point = new Point();
            {
                point.X = hero.Location.X + hero.Size.Width  / 2;
                point.Y = hero.Location.Y + hero.Size.Height / 2;
            }

            for (int x = 0; x < mapPic.GetLength(0); x++)
            {
                for (int y = 0; y < mapPic.GetLength(1); y++)
                {
                    if
                        (
                        mapPic[x, y].Location.X < point.X &&
                        mapPic[x, y].Location.Y < point.Y &&
                        mapPic[x, y].Location.X + mapPic[x, y].Size.Width > point.X &&
                        mapPic[x, y].Location.Y + mapPic[x, y].Size.Height > point.Y
                        )
                    {
                        return new Point(x, y); 
                    }
                        
                }
            } 
            return point;
        }
    }
}

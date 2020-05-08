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

            if ( x > 0 && map[heroPoint.X + 1 , heroPoint.Y] == State.empty)
            {
                return true;
            }
            if (x < 0 && map[heroPoint.X - 1, heroPoint.Y] == State.empty)
            {
                return true;
            }
            if (y > 0 && map[heroPoint.X, heroPoint.Y + 1] == State.empty)
            {
                return true;
            }
            if (y < 0 && map[heroPoint.X, heroPoint.Y - 1] == State.empty)
            {
                return true;
            }

            int heroRightSide = hero.Location.X + hero.Size.Width;
            int heroLeftSide = hero.Location.X;
            int heroBootomSide = hero.Location.Y + hero.Size.Height;
            int heroTopSide = hero.Location.Y;

            int rightObstacleLeftSide = mapPic[heroPoint.X + 1, heroPoint.Y].Location.X;
            int leftObstacleRightSide = mapPic[heroPoint.X - 1, heroPoint.Y].Location.X + mapPic[heroPoint.X - 1, heroPoint.Y].Size.Width;
            int bottomObstacleTopSide = mapPic[heroPoint.X, heroPoint.Y + 1].Location.Y;
            int topObstacleBottomSide = mapPic[heroPoint.X, heroPoint.Y - 1].Location.Y + mapPic[heroPoint.X, heroPoint.Y - 1].Size.Height;

            if (x > 0 && heroRightSide + x > rightObstacleLeftSide)
            {
                x = rightObstacleLeftSide - heroRightSide;
            }
            if (x < 0 && heroLeftSide + x < leftObstacleRightSide)
            {
                x = leftObstacleRightSide - heroLeftSide;
            }
            if (y > 0 && heroBootomSide + y > bottomObstacleTopSide)
            {
                y = bottomObstacleTopSide - heroBootomSide;
            }
            if (y < 0 && heroTopSide + y < topObstacleBottomSide)
            {
                x = topObstacleBottomSide - heroTopSide;
            }


            return true;
        }
        private Point MyNowPoint()
        {
            Point point = new Point();
            {
                point.X = hero.Location.X + hero.Size.Width / 2;
                point.Y = hero.Location.Y + hero.Size.Height / 2;
            }

            for (int x = 0; x < mapPic.GetLength(0); x++)
            {
                for (int y = 0; y < mapPic.GetLength(1); y++)
                {
                    if
                        (
                        mapPic[x, y].Location.X < point.X &&
                        mapPic[x, y].Location.Y < point.X &&
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

﻿using System.Drawing;
using System.Windows.Forms;

namespace DungeonsGame
{
    class Movement
    {
        PictureBox unit;
        PictureBox[,] mapPic;
        State[,] map;
        delAddBonus addBonus;

        public Movement(PictureBox unit, PictureBox[,] mapPic, State[,] map, delAddBonus addBonus)
        {
            this.unit = unit;
            this.mapPic = mapPic;
            this.map = map;
            this.addBonus = addBonus;
        }
        public void MoveByStep(int x, int y)
        {
            if (IsEmpty(ref x, ref y))
            {
                unit.Location = new Point(unit.Location.X + x, unit.Location.Y + y);
                Point myPlace = MyNowPoint();
                if (map [myPlace.X ,myPlace.Y] == State.bonus)
                {
                    addBonus(Bonus.GetBonus());
                    map[myPlace.X, myPlace.Y] = State.empty;
                    mapPic[myPlace.X, myPlace.Y].Image = Properties.Resources.ground;
                }
            }
        }
        private bool IsEmpty(ref int x, ref int y)
        {
            Point heroPoint = MyNowPoint();
            int heroRightSide = unit.Location.X + unit.Size.Width;
            int heroLeftSide = unit.Location.X;
            int heroBottomSide = unit.Location.Y + unit.Size.Height;
            int heroTopSide = unit.Location.Y;

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

            if (
                x > 0 && 
                (map[heroPoint.X + 1, heroPoint.Y] == State.empty ||
                map[heroPoint.X + 1, heroPoint.Y] == State.splash ||
                map[heroPoint.X + 1, heroPoint.Y] == State.bonus)
                )
            {
                if (heroTopSide < rightTopObstacleBottomSide)
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
            if (
                x < 0 &&
                (map[heroPoint.X - 1, heroPoint.Y] == State.empty || 
                map[heroPoint.X - 1, heroPoint.Y] == State.splash ||
                map[heroPoint.X - 1, heroPoint.Y] == State.bonus)  
                )
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
            if (
                y > 0 && 
                (map[heroPoint.X, heroPoint.Y + 1] == State.empty ||
                map[heroPoint.X, heroPoint.Y + 1] == State.splash||
                map[heroPoint.X, heroPoint.Y + 1] == State.bonus)
                )
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
            if (
                y < 0 &&
                (map[heroPoint.X, heroPoint.Y - 1] == State.empty ||  
                map[heroPoint.X, heroPoint.Y - 1] == State.splash ||
                map[heroPoint.X, heroPoint.Y - 1] == State.bonus)
                )
            {
                if (heroRightSide > rightTopObstacleLeftSide)
                {
                    if (rightTopObstacleLeftSide - heroRightSide < -offset)
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
        public Point MyNowPoint()
        {
            Point point = new Point();
            {
                point.X = unit.Location.X + unit.Size.Width / 2;
                point.Y = unit.Location.Y + unit.Size.Height / 2;
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

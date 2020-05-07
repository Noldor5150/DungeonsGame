using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonsGame
{
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
        public MainBoard(Panel panel)
        {
            panelGame = panel;
            InitStartMap();
        }

        private void InitStartMap()
        {
            mapPic = new PictureBox[sizeX, sizeY];
            panelGame.Controls.Clear();
            map = new State[sizeX, sizeY];

            int boxSize;

            if ((panelGame.Width / sizeX) < (panelGame.Height / sizeY))
            {

                boxSize = panelGame.Width / sizeX;
            }
            else
            {
                boxSize = panelGame.Height / sizeY;
            }
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
    }
}

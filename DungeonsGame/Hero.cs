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
        public Hero(PictureBox hero, PictureBox[,] mapPic)
        {
            this.hero = hero;
            this.mapPic = mapPic;
            step = 5;
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
            hero.Location = new Point(hero.Location.X + x, hero.Location.Y + y);
        }

      

    }
}

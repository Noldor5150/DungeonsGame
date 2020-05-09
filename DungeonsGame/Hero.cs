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
        
        Movement movement;
        public Hero(PictureBox hero, PictureBox[,] mapPic, State[,] map)
        {
            this.hero = hero;
           
            step = 3;
            movement = new Movement(hero, mapPic, map);
        }


        public void MoveHero(Arrows arrow)
        {
            switch (arrow)
            {
                case Arrows.left:
                   movement.MoveByStep(-step, 0);
                    break;
                case Arrows.right:
                    movement.MoveByStep(step, 0);
                    break;
                case Arrows.up:
                    movement.MoveByStep(0, -step);
                    break;
                case Arrows.down:
                    movement.MoveByStep(0, step);
                    break;
                default:
                    break;
            }
        }

        public Point MyNowPoint()
        {
            return movement.MyNowPoint();

        }
    }
}

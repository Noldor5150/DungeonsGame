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
       public List<Trap> traps { get; private set; }
        int quantityOfTraps;
        public int splashLength { get; private set; }


        public Hero(PictureBox hero, PictureBox[,] mapPic, State[,] map)
        {
            this.hero = hero;
            quantityOfTraps = 3;
            traps = new List<Trap>();
            splashLength = 3;

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

        public bool CanCreateTrap( PictureBox [,] mapPic, delSplash splash)
        {
            if (traps.Count >= quantityOfTraps)
            {
                return false;
            }
            Trap trap = new Trap(mapPic, MyNowPoint(),splash);
            traps.Add(trap);
            return true;
        }
    }
}

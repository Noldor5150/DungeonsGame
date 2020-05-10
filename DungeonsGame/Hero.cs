using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonsGame
{
    public delegate void delAddBonus(PrizeList prize);

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
       Label score;

        public Hero(PictureBox hero, PictureBox[,] mapPic, State[,] map, Label score)
        {
            this.score = score;
            this.hero = hero;
            quantityOfTraps = 3;
            traps = new List<Trap>();
            splashLength = 3;
            step = 3;
            movement = new Movement(hero, mapPic, map, AddBonus);
            ChangeScore();
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
        public void RemoveTrap(Trap trap)
        {
            traps.Remove(trap);
        }
        private void ChangeScore(string changes="")        {
            if (score == null)
            {
                return;
            }
            score.Text = "Speed: " + step + ", trap QNT: " + quantityOfTraps + ", trap Power: " + splashLength + " " +changes;
        }
        private void AddBonus(PrizeList prize)
        {
            switch (prize)
            {
                
                case PrizeList.trapQntIncrease:
                    quantityOfTraps++;
                 /*   ChangeScore("Traps added");*/
                    break;
                case PrizeList.trapQntDecrease:
                    quantityOfTraps = quantityOfTraps == 1 ? 1 : quantityOfTraps--;
                   /* ChangeScore("Traps removed");*/
                    break;
                case PrizeList.splashRangeIncrease:
                    splashLength++;
                    break;
                case PrizeList.splashRangeDecrease:
                    splashLength = splashLength == 1 ? 1 : splashLength--;
                    break;
                case PrizeList.speedIncrease:
                    step++;
                    break;
                case PrizeList.speedDecrease:
                    step = step <= 3 ? 3 : step--;
                    break;
                default:
                    break;
            }
            ChangeScore(prize.ToString());
        }
    }
}

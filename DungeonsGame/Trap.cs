using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace DungeonsGame
{
   public  class Trap
    {
        Timer timer;
        int secondsLeft = 4;
        PictureBox[,] mapPic;
        public Point trapPlace { get; private set; }
        delSplash splash;
        
        public Trap(PictureBox[,] mapPic , Point trapPlace, delSplash splash )
        {
            this.trapPlace = trapPlace;
            this.mapPic = mapPic;
            this.splash = splash;

            CreateTimer();
            timer.Enabled = true;
        }

        private void CreateTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(secondsLeft <= 0)
            {
                
                timer.Enabled = false;
               
                splash(this);
                return;
            }
            WriteTime(--secondsLeft);
        }

        private void WriteTime(int sec)

        {
            mapPic[trapPlace.X, trapPlace.Y].Image = Properties.Resources.iceball;
            mapPic[trapPlace.X, trapPlace.Y].Refresh();
            using (Graphics myGraphics = mapPic[trapPlace.X, trapPlace.Y].CreateGraphics())
            {
                PointF point = new PointF(
                    mapPic[trapPlace.X, trapPlace.Y].Size.Width / 3,
                    mapPic[trapPlace.X, trapPlace.Y].Size.Height / 3 - 3);

                myGraphics.DrawString(
                    sec.ToString(),
                    new Font("Arial", 10),
                    Brushes.Red, point);
            }
        }

        public void TrapReaction()
        {
            secondsLeft = 0;
        }
    }
}

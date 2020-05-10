using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsGame
{

    public enum PrizeList
    {
        empty,
        trapQntIncrease,
        trapQntDecrease,
        splashRangeIncrease,
        splashRangeDecrease,
        speedIncrease,
        speedDecrease
    }
   public static class Bonus
    {
        static Dictionary<PrizeList, int> percent;
        static List<PrizeList> bonusList;
        static Random rand = new Random();
        static int bonusQuantity = 7;


        public static void Prepare()
        {
            PreparePercentage();
            PrepareBonus();
        }

        private static void PreparePercentage()
        {
            percent = new Dictionary<PrizeList, int>(); 
            percent.Add(PrizeList.trapQntIncrease, 90);
            percent.Add(PrizeList.trapQntDecrease, 30);
            percent.Add(PrizeList.splashRangeIncrease, 60);
            percent.Add(PrizeList.splashRangeDecrease, 20);
         
        }

        private static void PrepareBonus()
        {
            bonusList = new List<PrizeList>();
            int sum = 0;

            foreach (int item in percent.Values)
            {
                sum += item;
            }
            do
            {
                int bonusNumber = rand.Next(0, sum);
                int tBonus = 0;
                foreach (PrizeList prize in percent.Keys)
                {
                    tBonus += percent[prize];
                    if (tBonus>bonusNumber)
                    {
                        bonusList.Add(prize);
                        break;
                    }
                }
            } while (bonusList.Count < bonusQuantity);
        }

        public static PrizeList GetBonus()
        {
            if ( bonusList.Count == 0)
            {
                return PrizeList.empty;
            }
            PrizeList prize = bonusList[0];
            bonusList.Remove(prize);
            return prize;

        }
    }
}

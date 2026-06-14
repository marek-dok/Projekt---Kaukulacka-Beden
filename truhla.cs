using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace kalkulacka_bednicek
{
    internal class Truhla //informace o bedně
    {
        private double profit_chance;
        private double lose_chance;
        private double profit2_chance;
        private double mid_chance;
        private double cena_bedny;
        public double Profit_Chance
        {
            get
            {
                return profit_chance;
            }
            set
            {
                if (Profit_Chance < 0)
                {
                    profit_chance = 0;
                }
                if (Profit_Chance > 100)
                {
                    profit_chance = 100;
                }
                else
                {
                    profit_chance = value;
                }
            }
        }
        public double Lose_Chance
        {
            get
            {
                return lose_chance;
            }
            set
            {
                if (Lose_Chance < 0)
                {
                    lose_chance = 0;
                }
                if (Lose_Chance > 100)
                {
                    lose_chance = 100;
                }
                else
                {
                    lose_chance = value;
                }
            }
        }
        public double Profit2_Chance
        {
            get
            {
                return profit2_chance;
            }
            set
            {
                if (Profit2_Chance < 0)
                {
                    profit2_chance = 0;
                }
                if (Profit2_Chance > 100)
                {
                    profit2_chance = 100;
                }
                else
                {
                    profit2_chance = value;
                }
            }
        }
        public double Mid_Chance
        {
            get
            {
                return mid_chance;
            }
            set
            {
                if (Mid_Chance < 0)
                {
                    mid_chance = 0;
                }
                if (Mid_Chance > 100)
                {
                    mid_chance = 100;
                }
                else
                {
                    mid_chance = value;
                }
            }
        }
        public double Cena_Bedny
        {
            get
            {
                return cena_bedny;
            }
            set
            {
                if (value < 0)
                {
                    cena_bedny = 0;
                }
                else
                {
                    cena_bedny = value;
                }
            }
        }
            
        public string Nazev { get; set; }

    }
}

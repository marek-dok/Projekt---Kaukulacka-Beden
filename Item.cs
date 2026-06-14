using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalkulacka_bednicek
{

        
    internal class Item //informace o skinech v bedně
    {
        public string Name { get; set; }
        public string Color { get; set; }
        private double cost;
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                if (value < 0)
                {
                    cost = 0;
                }
                else
                {
                    cost = value;
                }
            }
        }

        private double chance;
        public double Chance
        {
            get
            {
                return chance;
            }
            set
            {
                if (Chance < 0)
                {
                    chance = 0;
                }
                else if (Chance > 100)
                {
                    chance = 100;
                }
                else
                {
                    chance = value;
                }
            }
        }
       
        public string Stats()
        {
            return ($"NAME: {Name}, COST: {Cost}$, CHANCE: {Chance}%, COLOR: {Color}");
        }
        

        
        
    }
}

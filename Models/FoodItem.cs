using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fränder.Models
{
    internal class FoodItem
    {
        public string Name { get; set; }        // Namn på maten
        public int Price { get; set; }          // Pris för maten
        public int HungerIncrease { get; set; } // Hur mycket hungern ökar

        public FoodItem(string name, int price, int hungerIncrease)
        {
            Name = name;
            Price = price;
            HungerIncrease = hungerIncrease;
        }
    }
}

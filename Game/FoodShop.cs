using Fränder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fränder.Game
{
    internal class FoodShop
    {
        private static FoodShop _instance;
        private static readonly object _lock = new object(); // Lås för trådsäkerhet

        // Lista med matvaror
        private List<FoodItem> _foodItems;

        // Referens till NeedsLogic
        private NeedsLogic _needsLogic;

        // Privat konstruktor för att förhindra externa instanser
        private FoodShop(NeedsLogic needsLogic)
        {
            _needsLogic = needsLogic;

            // Initialisera lista med matvaror
            _foodItems = new List<FoodItem>
            {
                new FoodItem("Billig Mat", 10, 20), // Billig mat
                new FoodItem("Dyr Mat", 30, 50)     // Dyr mat
            };
        }

        // Offentlig statisk egenskap för att få den unika instansen
        public static FoodShop Instance(NeedsLogic needsLogic)
        {
            lock (_lock) // Säkerställer trådsäker instansiering
            {
                if (_instance == null)
                {
                    _instance = new FoodShop(needsLogic);
                }
                return _instance;
            }
        }

        // Metod för att köpa mat från listan
        public bool BuyFood(string foodName)
        {
            // Hitta matvaran i listan baserat på namn
            var foodItem = _foodItems.FirstOrDefault(f => f.Name == foodName);

            // Kontrollera om matvaran finns och om användaren har tillräckligt med pengar
            if (foodItem != null && _needsLogic.GetMoney() >= foodItem.Price && _needsLogic.hunger < 100)
            {
                // Öka hungern och se till att den inte överskrider 100
                _needsLogic.hunger += foodItem.HungerIncrease;
                if (_needsLogic.hunger > 100)
                    _needsLogic.hunger = 100;

                // Minska pengar och uppdatera UI
                _needsLogic.Money -= foodItem.Price;

                _needsLogic.TriggerHungerChanged();
                _needsLogic.TriggerMoneyChanged();

                Console.WriteLine($"Köpt {foodItem.Name}! Hungern är nu: {_needsLogic.hunger}");
                return true;
            }
            else
            {
                Console.WriteLine($"Kunde inte köpa {foodName}. Antingen inte tillräckligt med pengar eller hungern är full.");
                return false;
            }
        }

        // Metod för att lista alla tillgängliga matvaror i affären
        public List<FoodItem> GetFoodItems()
        {
            return _foodItems;
        }
    }
}

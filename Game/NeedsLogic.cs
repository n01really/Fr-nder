using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Fränder.Game
{
    public class NeedsLogic
    {


        private int health = 100;
        public int hunger = 100;
        private int sleepines = 0;
        private int fun = 100;
        private int clenlines = 100;
        public int Money = 500;

        private Timer hungerTimer;
        private Timer cleanTimer;
        private Timer healthTimer;
        private Timer sleepTimer;

        public event Action OnHungerChanged;
        public event Action OnCleanChanged;
        public event Action OnHealthChanged;
        public event Action OnMoneyChanged;
        public event Action OnSleepChanged;
        public NeedsLogic()
        {
            hungerTimer = new Timer(1000);
            hungerTimer.Elapsed += (sender, e) =>
            DecreasseHunger();
            hungerTimer.Start();

            cleanTimer = new Timer(1000);
            cleanTimer.Elapsed += (sender, e) =>
            DecresseClean();
            cleanTimer.Start();

            healthTimer = new Timer(1000);
            healthTimer.Elapsed += (sender, e) =>
            DecreasseHealth();
            healthTimer.Start();

            sleepTimer = new Timer(1000);
            sleepTimer.Elapsed += (sender, e) =>
            IncreasseSleep();
            healthTimer.Start();
        }

        public void TriggerHungerChanged()
        {
            OnHungerChanged?.Invoke();
        }

        // Metod för att trigga money-händelsen
        public void TriggerMoneyChanged()
        {
            OnMoneyChanged?.Invoke();
        }



        public void DecreasseHealth()
        {
            if (hunger == 0)
            {
                health -= 5;
                OnHealthChanged?.Invoke();
            }
            if (health < 0)
                health = 0;
            
        }

        public void DecreasseHunger()
        {
            hunger -= 1;
            if (hunger < 0) hunger = 0;
            OnHungerChanged?.Invoke();

        }

        public void IncreasseSleep()
        {
            sleepines += 1;
            if (sleepines < 100)
            {
                sleepines = 100;
            }
        }

        public void Fun()
        {
            fun -= 1;
            if (fun < 0)
            {
                fun = 0;
            }
        }

        public void DecresseClean()
        {
            clenlines -= 1;
            if (clenlines < 0) clenlines = 0;
            OnCleanChanged?.Invoke();

        }

        public int GetMoney()
        {
            return Money;
        }

        public string GetImage()
        {
            if (hunger >= 75)
                return "hunger.png";

            else if (hunger >= 50)
                return "hunger_med.png";
            else if (hunger >= 1)
                return "hunger_low.png";
            else
                return "hunger_empty.png";
        }

        public string GetCleanImage()
        {
            if (clenlines >= 75)
                return "clean_full.png";
            else if (clenlines >= 50)
                return "clean_med.png";
            else if (clenlines >= 1)
                return "clean_low.png";
            else
                return "clean_empty.png";
        }

        public string GetHealthImage()
        {
            if (health >= 75)
                return "liv_full.png";
            else if (health >= 50)
                return "liv_med.png";
            else if (health >= 1)
                return "liv_low.png";
            else
                return "liv_dead.png";
        }
    }


}

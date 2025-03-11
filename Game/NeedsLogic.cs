using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Fränder.Game
{
    internal class NeedsLogic
    {
        //private int Hunger = 100;
        //private int Sleep = 100;
        //private int Clenlines = 100;
        //private int Fun = 100;
        //private int Health = 100;
        //private Timer timer;
        //public void Needs()
        //{
        //    timer = new Timer(1000);
        //    timer.Elapsed += UpdateNeeds;
        //    timer.Start();
        //}
        //private void UpdateNeeds(object sender, ElapsedEventArgs e)
        //{
        //    Hunger = Math.Max(0, Hunger - 1);
        //    Sleep = Math.Max(0, Sleep - 1);
        //    Fun = Math.Max(0, Fun - 1);

        //    if (Hunger == 0 || Sleep == 0)
        //    {
        //        Health = Math.Max(0, Health - 1);
        //    }
        //}

        private int health = 100;
        public int hunger = 100;
        private int sleepines = 0;
        private int fun = 100;
        private int clenlines = 100;

        private Timer hungerTimer;
        private Timer cleanTimer;
        private Timer healthTimer;

        public event Action OnHungerChanged;
        public event Action OnCleanChanged;
        public event Action OnHealthChanged;
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

        public void Sleep()
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
                return "liv_empty.png";
        }
    }


}

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

        public event Action OnHungerChanged;
        public event Action OnCleanChanged;

        public NeedsLogic()
        {
            hungerTimer = new Timer(1000);
            hungerTimer.Elapsed += (sender, e) =>
            DecreasseHunger();
            hungerTimer.Start();

            cleanTimer = new Timer(10000);
            cleanTimer.Elapsed += (sender, e) =>
            DecresseClean();
            cleanTimer.Start();
        }



        public void Health()
        {
            health -= 1;
            if (health < 0)
            {
                health = 0;
            }
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
            if (hunger >= 60)
                return "hunger.png";

            else if (hunger >= 59)
                return "hunger_med.png";
            else if (hunger >= 10)
                return "hunger_low.png";
            else
                return "hunger_empty.png";
        }

        public string GetCleanImage()
        {
            if (clenlines >= 60)
                return "clean_full.png";
            else if (clenlines >= 59)
                return "clean_med.png";
            else if (clenlines >= 10)
                return "clean_low.png";
            else
                return "clean_empty.png";
        }
    }


}

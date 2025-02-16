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
       private int Hunger = 100;
       private int Sleep = 100;
       private int Clenlines = 100;
       private int Fun = 100;
       private int Health = 100;
       private Timer timer;
        public void Needs()
        {
            timer = new Timer(1000);
            timer.Elapsed += UpdateNeeds;
            timer.Start();
        }
        private void UpdateNeeds(object sender, ElapsedEventArgs e)
        {
            Hunger = Math.Max(0, Hunger - 1);
            Sleep = Math.Max(0, Sleep - 1);
            Fun = Math.Max(0, Fun - 1);

            if (Hunger == 0 || Sleep == 0)
            {
                Health = Math.Max(0, Health - 1);
            }
        }

    }
}

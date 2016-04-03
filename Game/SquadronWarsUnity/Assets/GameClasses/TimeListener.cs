using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Assets.GameClasses
{
    public class TimeListener
    {
        private Timer _timer;
        internal delegate void Method(ref Stats stats);
        internal Method ExecutionMethod { get; set; }
        internal Method FinishingMethod { get; set; }
        private Stats Stats;
        private int RemainingDuration;

        public TimeListener(int remainingDuration, Stats stats, int frequency = 1)
        {
            RemainingDuration = remainingDuration;
            Stats = stats;
            _timer = new Timer(frequency * 1000);
            _timer.Elapsed += Tick;
        }

        public void Start()
        {
            if (ExecutionMethod == null)
                return;

            _timer.Start();
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            ExecutionMethod(ref Stats);
            RemainingDuration--;
            if (RemainingDuration <= 0)
            {
                _timer.Stop();
                FinishingMethod(ref Stats);
            }
        }
    }
}

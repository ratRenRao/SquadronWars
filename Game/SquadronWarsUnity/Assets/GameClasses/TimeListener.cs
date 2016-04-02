using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Assets.GameClasses
{
    public class TimeListener
    {
        private Timer _timer; // From System.Timers
        internal delegate void Method(Stats stats);
        internal Method ExecutionMethod { get; set; }
        private Stats Stats;
        private int Frequency;

        public TimeListener(int seconds, Stats stats, int frequency = 1)
        {
            Frequency = frequency;
            Stats = stats;
            _timer = new Timer(seconds * 1000);
            _timer.Elapsed += new ElapsedEventHandler(Tick);
        }

        public void Start()
        {
            if (ExecutionMethod == null)
                return;
            
            _timer.Enabled = true;
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            ExecutionMethod(Stats);
        }
    }
}

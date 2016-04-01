using System;
using System.Collections.Generic;

namespace Assets.GameClasses
{
    public abstract class Effect : IEffectable
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public bool HasInitialEffect = false;
        private List<Stats> _affectedCharacterStats { get; set; }
        private Stats _executionerStats { get; set; }

        public virtual void Execute(List<Stats> affectedCharacterStats, ref Stats executionserStats)
        {
            _affectedCharacterStats = affectedCharacterStats;
            _executionerStats = executionserStats;

            ImmediateEffect();
           
            if (Duration > 0)
            {
                LingeringEffect();
            }
            else if (Duration == 0)
            {
                RemoveEffect();
            }
        }

        public static int ValidateStat(int stat, int minStat, int maxStat)
        {
            return (stat < minStat) ? minStat : (stat > maxStat) ? maxStat : stat;
        }

        public virtual void ImmediateEffect()
        {
        }

        public  virtual void RemoveEffect()
        {
        }

        public virtual void LingeringEffect()
        {
        }
    }
}

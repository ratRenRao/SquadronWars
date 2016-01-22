using System;

namespace SquadronWars2
{
    public abstract class Effect // : IEffectable
    {
        public int Duration { get; set; }
        public bool HasInitialEffect = false;
        public Stats CasterStats { get; set; }
        public Stats TargetStats { get; set; }

        public void Execute(Stats charStats)
        {
            if (HasInitialEffect)
            {
                //initialEffect();
                HasInitialEffect = false;
            }
            else if (Duration > 0)
            {
                LingeringEffect(ref charStats);
            }
            else if (Duration == 0)
            {
                RemoveEffect(ref charStats);
            }
        }

        public static int ValidateStat(int stat, int minStat, int maxStat)
        {
            return (stat < minStat) ? minStat : (stat > maxStat) ? maxStat : stat;
        }

        public virtual void ImmediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public virtual void LingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }
    }
}

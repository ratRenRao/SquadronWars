using System;

namespace Assets.GameClasses
{
    abstract public class Effect : IEffectable
    {
        public int duration { get; set; }
        public bool hasInitialEffect = false;
        public Stats casterStats { get; set; }
        public Stats targetStats { get; set; }

        public void execute(Stats charStats)
        {
            if (hasInitialEffect)
            {
                //initialEffect();
                hasInitialEffect = false;
            }
            else if (duration > 0)
            {
                lingeringEffect();
            }
            else if (duration == 0)
            {
                removeEffect();
            }
        }

        public static int ValidateStat(int stat, int minStat, int maxStat)
        {
            return (stat < minStat) ? minStat : (stat > maxStat) ? maxStat : stat;
        }

        public void lingeringEffect() { }

        public void immediateEffect() { }

        public void removeEffect() { }

        public void immediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public void removeEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public void lingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace Assets.GameClasses
{
    abstract public class Effect : IEffectable
    {
        public int duration { get; set; }
        public bool hasInitialEffect = false;
        public Stats casterStats { get; set; }
        public Stats targetStats { get; set; }

        public virtual void execute(ref Stats charStats)
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

        public virtual void lingeringEffect() { }

        public virtual void immediateEffect() { }

        public virtual void removeEffect() { }

        public virtual void immediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public  virtual void removeEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public virtual void lingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }
    }
}

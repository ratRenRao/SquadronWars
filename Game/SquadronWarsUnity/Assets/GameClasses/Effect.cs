using System;

namespace Assets.GameClasses
{
    abstract public class Effect : IEffectable
    {
        public int Duration { get; set; }
        public bool HasInitialEffect = false;
        public Stats Caster { get; set; }
        public Stats Target { get; set; }

        public virtual void Execute(ref Stats characterStats)
        {
            if (HasInitialEffect)
            {
                //initialEffect();
                HasInitialEffect = false;
            }
            else if (Duration > 0)
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

        public virtual void LingeringEffect() { }

        public virtual void ImmediateEffect() { }

        public virtual void RemoveEffect() { }

        public virtual void ImmediateEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public  virtual void RemoveEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public virtual void LingeringEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }
    }
}

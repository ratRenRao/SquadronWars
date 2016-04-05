using System;
using System.Collections.Generic;

namespace Assets.GameClasses
{
    class Heal : Ability 
    {
        private readonly int _healthRestored = 50;
        public Heal(int duration)
        {
            Duration = duration;
        }

        public override void ImmediateEffect(Stats stats)
        {
            //Target.HitPoints = ValidateStat(Target.HitPoints + _healthRestored, 0, Target.HitPoints);
        }

        public override void RemoveEffect(ref Stats stats)
        {
            throw new NotImplementedException();
        }

        public override void LingeringEffect(ref Stats stats)
        {
            throw new NotImplementedException();
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }
    }
}
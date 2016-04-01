using System;
using System.Collections.Generic;

namespace Assets.GameClasses
{
    class Heal : Effect
    {
        private readonly int _healthRestored = 50;
        public Heal(int duration)
        {
            Duration = duration;
        }

        public override void ImmediateEffect()
        {
            //Target.HitPoints = ValidateStat(Target.HitPoints + _healthRestored, 0, Target.HitPoints);
        }

        public override void Execute(List<Stats> affectedCharacterStats, ref Stats executionserStats)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEffect()
        {
            throw new NotImplementedException();
        }

        public override void LingeringEffect()
        {
            throw new NotImplementedException();
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }
    }
}
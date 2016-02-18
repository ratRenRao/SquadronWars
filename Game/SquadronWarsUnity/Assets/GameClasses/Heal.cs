using System;

namespace Assets.GameClasses
{
    class Heal : Effect
    {
        private readonly int _healthRestored = 50;
        public Heal(Stats caster, Stats target, bool initial, int duration)
        {
            Target = target;
            Caster = caster;
            HasInitialEffect = initial;
            Duration = duration;
        }

        public override void ImmediateEffect()
        {
            Target.HitPoints = ValidateStat(Target.HitPoints + _healthRestored, 0, Target.HitPoints);
        }

        public override void Execute(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public override void ImmediateEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public override void LingeringEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }
    }
}
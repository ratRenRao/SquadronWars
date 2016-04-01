using Assets.GameClasses;
using UnityEngine;

namespace Assets.GameClasses
{
    class Weaken : Effect
    {
        private int _decreasePercent;
        private int _hpRemoved;

        public Weaken(Stats caster, Stats target, bool hasInitialEffect, int duration)
        {
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public override void ImmediateEffect()
        {
           // _hpRemoved = Target.HitPoints * (_decreasePercent / 100);
           // Target.HitPoints -= _hpRemoved;
        }

        public override void RemoveEffect()
        {
            //Target.HitPoints = ValidateStat(Target.HitPoints + _hpRemoved, 0, Target.HitPoints);
        }

    }
}

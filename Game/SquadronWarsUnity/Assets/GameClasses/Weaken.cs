using Assets.GameClasses;
using UnityEngine;

namespace Assets.GameClasses
{
    class Weaken : Ability
    {
        private int _decreasePercent;
        private int _hpRemoved;

        public Weaken(Stats caster, Stats target, bool hasInitialEffect, int duration)
        {
            Duration = duration;
        }

        public override void ImmediateEffect(Stats stats)
        {
           // _hpRemoved = Target.HitPoints * (_decreasePercent / 100);
           // Target.HitPoints -= _hpRemoved;
        }

        public override void RemoveEffect(Stats stats)
        {
            //Target.HitPoints = ValidateStat(Target.HitPoints + _hpRemoved, 0, Target.HitPoints);
        }

    }
}

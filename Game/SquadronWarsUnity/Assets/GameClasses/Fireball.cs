using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SquadronWars2
{
    class Fireball : Effect, IEffectable
    {
        private readonly int _fireballDamage;

        public Fireball(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration, int fireballDamage)
        {
            _fireballDamage = fireballDamage;
            TargetStats = targetStats;
            CasterStats = casterStats;
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public new void ImmediateEffect()
        {
            TargetStats.currentHP -= _fireballDamage;
        }
    }
}
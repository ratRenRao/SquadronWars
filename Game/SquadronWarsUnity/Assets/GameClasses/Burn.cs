using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{

    class Burn : Effect, IEffectable
    {
        private readonly int _burnDamage;

        public Burn(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration, int burnDamage)
        {
            _burnDamage = burnDamage;
            TargetStats = targetStats;
            CasterStats = casterStats;
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public void lingeringEffect()
        {
            TargetStats.currentHP -= _burnDamage;
        }
    }
}
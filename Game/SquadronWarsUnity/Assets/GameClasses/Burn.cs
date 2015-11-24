using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{

    class Burn : Effect, IEffectable
    {
        private int burnDamage;

        public Burn(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration)
        {
            this.targetStats = targetStats;
            this.casterStats = casterStats;
            this.hasInitialEffect = hasInitialEffect;
            this.duration = duration;
        }

        public void lingeringEffect()
        {
            targetStats.currentHP -= burnDamage;
        }
    }
}
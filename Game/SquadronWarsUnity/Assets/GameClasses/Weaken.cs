using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    class Weaken : Effect, IEffectable
    {
        private int decreasePercent;
        private int hpRemoved;

        public Weaken(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration)
        {
            this.targetStats = targetStats;
            this.casterStats = casterStats;
            this.hasInitialEffect = hasInitialEffect;
            this.duration = duration;
        }

        public void immediateEffect()
        {
            hpRemoved = targetStats.currentHP * (decreasePercent / 100);
            targetStats.currentHP -= hpRemoved;
        }

        public void removeEffect()
        {
            targetStats.currentHP = ValidateStat(targetStats.currentHP + hpRemoved, 0, targetStats.maxHP);
        }

    }
}

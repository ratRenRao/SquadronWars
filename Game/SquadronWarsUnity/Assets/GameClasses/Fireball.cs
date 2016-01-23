using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SquadronWars2
{
    class Fireball : Effect, IEffectable
    {
        private int fireballDamage;

        public Fireball(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration)
        {
            this.targetStats = targetStats;
            this.casterStats = casterStats;
            this.hasInitialEffect = hasInitialEffect;
            this.duration = duration;
        }

        public void immediateEffect()
        {
            targetStats.currentHP -= fireballDamage;
        }
    }
}
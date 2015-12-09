using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    class Heal : Effect, IEffectable
    {
        private int healthRestored = 50;
        public Heal(Stats casterStats, Stats targetStats, bool initial, int duration)
        {
            this.targetStats = targetStats;
            this.casterStats = casterStats;
            this.hasInitialEffect = initial;
            this.duration = duration;
        }

        public void immediateEffect()
        {
            targetStats.currentHP = ValidateStat(targetStats.currentHP + healthRestored, 0, targetStats.maxHP);
        }

        public void execute(ref Stats charStats)
        {
            throw new NotImplementedException();
        }

        public void immediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public void removeEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public void lingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }
    }
}
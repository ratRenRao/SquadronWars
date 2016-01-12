using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    internal class Heal : Effect, IEffectable
    {
        private const int HealthRestored = 50;

        public Heal(Stats casterStats, Stats targetStats, bool initial, int duration)
        {
            TargetStats = targetStats;
            CasterStats = casterStats;
            HasInitialEffect = initial;
            Duration = duration;
        }

        public void immediateEffect()
        {
            TargetStats.currentHP = ValidateStat(TargetStats.currentHP + HealthRestored, 0, TargetStats.maxHP);
        }

        public void Execute(ref Stats charStats)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    internal class Heal : Effect //, IEffectable
    {
        private const int HealthRestored = 50;

        public Heal(Stats casterStats, Stats targetStats, bool initial, int duration)
        {
            TargetStats = targetStats;
            CasterStats = casterStats;
            HasInitialEffect = initial;
            Duration = duration;
        }

        public override void ImmediateEffect(ref Stats charStats)
        {
            charStats.CurrentHp = ValidateStat(charStats.CurrentHp + HealthRestored, 0, charStats.MaxHp);
        }

        public void Execute(ref Stats charStats)
        {
        }

        public override void RemoveEffect(ref Stats charStat)
        {
        }

        public override void LingeringEffect(ref Stats charStats)
        {
        }
    }
}
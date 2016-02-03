using System;

namespace Assets.GameClasses
{
    class Heal : Effect
    {
        private int healthRestored = 50;
        public Heal(Stats casterStats, Stats targetStats, bool initial, int duration)
        {
            this.targetStats = targetStats;
            this.casterStats = casterStats;
            this.hasInitialEffect = initial;
            this.duration = duration;
        }

        public override void immediateEffect()
        {
            targetStats.currentHP = ValidateStat(targetStats.currentHP + healthRestored, 0, targetStats.maxHP);
        }

        public override void execute(ref Stats charStats)
        {
            throw new NotImplementedException();
        }

        public override void immediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public override void removeEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public override void lingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }
    }
}
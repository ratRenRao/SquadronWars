namespace Assets.GameClasses
{
    class Weaken : Effect
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

        public override void immediateEffect()
        {
            hpRemoved = targetStats.currentHP * (decreasePercent / 100);
            targetStats.currentHP -= hpRemoved;
        }

        public override void removeEffect()
        {
            targetStats.currentHP = ValidateStat(targetStats.currentHP + hpRemoved, 0, targetStats.maxHP);
        }

    }
}

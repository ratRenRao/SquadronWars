namespace Assets.GameClasses
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

        public override void lingeringEffect()
        {
            targetStats.currentHP -= burnDamage;
        }
    }
}
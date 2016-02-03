namespace Assets.GameClasses
{
    class Fireball : Effect
    {
        private int fireballDamage;

        public Fireball(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration)
        {
            this.targetStats = targetStats;
            this.casterStats = casterStats;
            this.hasInitialEffect = hasInitialEffect;
            this.duration = duration;
        }

        public override void immediateEffect()
        {
            targetStats.currentHP -= fireballDamage;
        }
    }
}
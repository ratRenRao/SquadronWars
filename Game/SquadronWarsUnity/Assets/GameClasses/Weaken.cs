namespace SquadronWars2
{
    internal class Weaken : Effect
    {
        private readonly int _decreasePercent;
        private int _hpRemoved;

        public Weaken(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration, int decreasePercent)
        {
            _decreasePercent = decreasePercent;
            TargetStats = targetStats;
            CasterStats = casterStats;
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public override void ImmediateEffect(ref Stats charStat)
        {
            _hpRemoved = TargetStats.currentHP * (_decreasePercent / 100);
            TargetStats.currentHP -= _hpRemoved;
        }

        public override void RemoveEffect(ref Stats charStat)
        {
            TargetStats.currentHP = ValidateStat(TargetStats.currentHP + _hpRemoved, 0, TargetStats.maxHP);
        }

    }
}

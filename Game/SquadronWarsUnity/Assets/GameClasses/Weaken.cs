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

        public override void ImmediateEffect(ref Stats charStats)
        {
            _hpRemoved = charStats.CurrentHp * (_decreasePercent / 100);
            charStats.CurrentHp -= _hpRemoved;
        }

        public override void RemoveEffect(ref Stats charStats)
        {
            charStats.CurrentHp = ValidateStat(charStats.CurrentHp + _hpRemoved, 0, TargetStats.MaxHp);
        }

    }
}

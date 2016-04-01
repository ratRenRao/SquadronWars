namespace Assets.GameClasses
{

    class Burn : Effect
    {
        private int _burnDamage;

        public Burn(Stats caster, Stats target, bool hasInitialEffect, int duration)
        {
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public override void LingeringEffect()
        {
//            Target.HitPoints -= _burnDamage;
        }
    }
}
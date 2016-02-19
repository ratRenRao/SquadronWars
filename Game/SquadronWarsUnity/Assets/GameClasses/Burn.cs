namespace Assets.GameClasses
{

    class Burn : Effect, IEffectable
    {
        private int _burnDamage;

        public Burn(Stats caster, Stats target, bool hasInitialEffect, int duration)
        {
            Target = target;
            Caster = caster;
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public override void LingeringEffect()
        {
            Target.HitPoints -= _burnDamage;
        }
    }
}
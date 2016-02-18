namespace Assets.GameClasses
{
    class Fireball : Effect
    {
        private int _fireballDamage;

        public Fireball(Stats caster, Stats target, bool hasInitialEffect, int duration)
        {
            Target = target;
            Caster = caster;
            HasInitialEffect = hasInitialEffect;
            Duration = duration;
        }

        public override void ImmediateEffect()
        {
            Target.HitPoints -= _fireballDamage;
        }
    }
}
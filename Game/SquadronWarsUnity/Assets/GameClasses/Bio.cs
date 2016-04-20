using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class Bio : Ability
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tiles, ref executioner, ref executionerTile);
            ImmediateBaseDamage = 10;
            LingeringBaseDamage = 5;
            //Duration = 5;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
            //ResultingEffects = new List<Effect> {new Burn()};
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateImmediateDamage();
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            Executioner.CharacterClassObject.CurrentStats.CurMP -= mpCost;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Cast("Bio");
        }

        public override void LingeringEffect(Stats stats)
        {
            Damage = (int)CalculateLingeringDamage();
            stats.CurHP -= Damage;
            base.LingeringEffect(stats);
            AnimationManager.SetDamage(Damage);
            AnimationManager.ExecuteLingeringEffect();
        }

        private double CalculateImmediateDamage()
        {
            return ImmediateBaseDamage + (int)(Executioner.CharacterClassObject.CurrentStats.MagicAttack * 0.3);
        }

        private double CalculateLingeringDamage()
        {
            return (Executioner.CharacterClassObject.CurrentStats.MagicAttack * 0.08) + (AbilityLevel * 0.1) + (LingeringBaseDamage / Duration);
        }

        private int CalculateDuration()
        {
            return Executioner.CharacterClassObject.CurrentStats.MagicAttack / 10;
        }
    }
}

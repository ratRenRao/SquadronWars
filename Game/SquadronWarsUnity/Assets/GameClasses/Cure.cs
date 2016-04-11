using System;
using System.Collections.Generic;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class Cure : Ability 
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tiles, ref executioner, ref executionerTile);
            ImmediateBaseDamage = -30;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateHeal(stats);
            stats.CurHP = stats.CurHP - Damage > stats.HitPoints ? stats.HitPoints : stats.CurHP - Damage;
            stats.CurMP -= mpCost;
            AnimationManager.Cast("Cure");
        }

        private double CalculateHeal(Stats stats)
        {
            return ImmediateBaseDamage - (Executioner.CharacterClassObject.CurrentStats.MagicAttack * 0.5);
        }
    }
}
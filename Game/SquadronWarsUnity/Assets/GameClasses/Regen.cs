using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class Regen : Ability
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tiles, ref executioner, ref executionerTile);
            Duration = 5;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateRegen();
            stats.CurHP = stats.CurHP - Damage > stats.HitPoints ? stats.HitPoints : stats.CurHP - Damage;
            Executioner.CharacterClassObject.CurrentStats.CurMP -= mpCost;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Cast("Regen");
        }

        private double CalculateRegen()
        {
            return Executioner.CharacterClassObject.CurrentStats.MagicAttack * 0.1 + AbilityLevel * 0.1;
        }

        public override void LingeringEffect(Stats stats)
        {
            stats.CurHP = stats.CurHP - Damage > stats.HitPoints ? stats.HitPoints : stats.CurHP - Damage;
            base.LingeringEffect(stats);
            AnimationManager.SetDamage(Damage);
            AnimationManager.ExecuteLingeringEffect();
        }
    }
}

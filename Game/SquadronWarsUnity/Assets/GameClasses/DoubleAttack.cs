using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class DoubleAttack : Attack
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tiles, ref executioner, ref executionerTile);
            ImmediateBaseDamage = Executioner.CharacterClassObject.Equipment.Weapon != null
               ? Executioner.CharacterClassObject.Equipment.Weapon.Damage : 0;
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateAttack(stats);
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            Executioner.CharacterClassObject.CurrentStats.CurMP -= mpCost;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Attack("DoubleAttack");
        }

        private double CalculateAttack(Stats stats)
        {
            return (ImmediateBaseDamage + (Executioner.CharacterClassObject.CurrentStats.Str * 0.5)) * 2;
        }
    }
}

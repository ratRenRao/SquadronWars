using System.Collections.Generic;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine;

namespace Assets.GameClasses
{
    class ArmorBreak : Ability
    {
        private Stats _initialStats;

        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tiles, ref executioner, ref executionerTile);
            ImmediateBaseDamage = Executioner.CharacterClassObject.Equipment.Weapon != null
               ? Executioner.CharacterClassObject.Equipment.Weapon.Damage / 2 : 0;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
        }

        public override void ImmediateEffect(Stats stats)
        {
            _initialStats = stats;
            Damage = CalculateWeaken(stats);
            stats.Defense -= Damage;
            Damage = (int) CalculateAttack(stats);
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            Executioner.CharacterClassObject.CurrentStats.CurMP -= mpCost;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Attack("Armor Break");
        }

        public override void RemoveEffect(ref Stats stats)
        {
            stats.Defense = _initialStats.Defense;
        }

        private int CalculateWeaken(Stats stats)
        {
            return (int) ((Executioner.CharacterClassObject.CurrentStats.Str * 0.001) + .15) * stats.Defense + Executioner.CharacterClassObject.Equipment.Weapon.Damage;
        }

        private double CalculateAttack(Stats stats)
        {
            return ImmediateBaseDamage + (Executioner.CharacterClassObject.CurrentStats.Str * 0.25);
        }
    }
}

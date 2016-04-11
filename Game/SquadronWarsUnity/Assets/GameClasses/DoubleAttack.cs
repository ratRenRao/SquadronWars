﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class DoubleAttack : Attack
    {
        public override void Initialize(ref Dictionary<CharacterGameObject, Tile> tileDictionary, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tileDictionary, ref executioner, ref executionerTile);
            ImmediateBaseDamage = Executioner.CharacterClassObject.Equipment.Weapon != null
               ? Executioner.CharacterClassObject.Equipment.Weapon.Damage : 0;
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateAttack(stats);
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Attack("Double Attack");
        }

        private double CalculateAttack(Stats stats)
        {
            return (ImmediateBaseDamage + (Executioner.CharacterClassObject.CurrentStats.Str * 0.5)) * 2;
        }
    }
}

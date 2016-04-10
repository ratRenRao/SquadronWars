using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.GameClasses
{
    class Fire : Ability
    {
        public override void Initialize(ref Dictionary<CharacterGameObject, Tile> tileDictionary, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tileDictionary, ref executioner, ref executionerTile);
            ImmediateBaseDamage = 10;
            LingeringBaseDamage = 3;
            //Duration = 5;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
            //ResultingEffects = new List<Effect> {new Burn()};
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateImmediateDamage();
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Cast("fire");
        }

        public override void LingeringEffect(ref Stats stats)
        {
            stats.CurHP -= (int)CalculateLingeringDamage();
        }

        private double CalculateImmediateDamage()
        {
            return ImmediateBaseDamage + (int)(Executioner.CharacterClassObject.CurrentStats.MagicAttack * 0.25);
        }

        private double CalculateLingeringDamage()
        {
            return Executioner.CharacterClassObject.CurrentStats.MagicAttack*0.5 + (AbilityLevel*0.1) + ImmediateBaseDamage;
        }

        private int CalculateDuration()
        {
            return Executioner.CharacterClassObject.CurrentStats.MagicAttack/10;
        }
    }
}
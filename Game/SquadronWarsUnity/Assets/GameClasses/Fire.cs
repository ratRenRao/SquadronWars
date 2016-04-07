using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.GameClasses
{
    class Fire : Ability
    {
        public override void Initialize(ref Dictionary<Character, Tile> tileDictionary, ref Character executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tileDictionary, ref executioner, ref executionerTile);
            ImmediateBaseDamage = 10;
            LingeringBaseDamage = 3;
            Duration = 5;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
            //ResultingEffects = new List<Effect> {new Burn()};
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateImmediateDamage(stats);
            stats.HitPoints -= Damage;
      //      AnimationManager.Cast(Name);
        }

        public override void LingeringEffect(ref Stats stats)
        {
            stats.HitPoints -= (int)CalculateLingeringDamage();
        }

        private double CalculateImmediateDamage(Stats stats)
        {
            return ImmediateBaseDamage;
        }

        private double CalculateLingeringDamage()
        {
            return Executioner.CurrentStats.MagicAttack*0.5*AbilityLevel*0.1*ImmediateBaseDamage;
        }

        private int CalculateDuration()
        {
            return Executioner.CurrentStats.MagicAttack/10;
        }
    }
}
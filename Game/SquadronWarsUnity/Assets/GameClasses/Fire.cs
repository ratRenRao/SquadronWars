using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.GameClasses
{
    class Fire : Ability
    {
        public override void Initialize(ref List<Stats> affectedCharacterStats, ref Stats executionserStats)
        {
            base.Initialize(ref affectedCharacterStats, ref executionserStats);

            ImmediateBaseDamage = 10;
            LingeringBaseDamage = 3;
            //ResultingEffects = new List<Effect> {new Burn()};
        }

        public override void ImmediateEffect(Stats stats)
        {
            stats.HitPoints -= (int)CalculateImmediateDamage(stats);
        }

        public override void LingeringEffect(Stats stats)
        {
            stats.HitPoints -= (int)CalculateLingeringDamage();
        }

        private double CalculateImmediateDamage(Stats stats)
        {
            return ImmediateBaseDamage;
        }

        private double CalculateLingeringDamage()
        {
            return AbilityLevel*0.1*ImmediateBaseDamage;
        }

        private int CalculateDuration()
        {
            return ExecutionerStats.MagicAttack/10;
        }
    }
}
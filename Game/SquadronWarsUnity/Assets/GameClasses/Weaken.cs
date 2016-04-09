using System.Collections.Generic;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine;

namespace Assets.GameClasses
{
    class Weaken : Ability
    {
        private Stats _initialStats;

        public override void Initialize(ref Dictionary<CharacterGameObject, Tile> tileDictionary, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tileDictionary, ref executioner, ref executionerTile);
            ImmediateBaseDamage = 0;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
        }

        public override void ImmediateEffect(Stats stats)
        {
            _initialStats = stats;
            Damage = (int)CalculateWeaken();
            stats.Defense -= Damage;
            AnimationManager.Cast("weaken");
        }

        public override void RemoveEffect(ref Stats stats)
        {
            stats.Defense = _initialStats.Defense;
        }

        private int CalculateWeaken()
        {
            return 5 + (int) (Executioner.CharacterClassObject.CurrentStats.MagicAttack*0.1);
        }

    }
}

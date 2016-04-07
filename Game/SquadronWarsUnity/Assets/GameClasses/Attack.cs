using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class Attack : Action 
    {
        public override void Initialize(ref Dictionary<CharacterGameObject, Tile> tileDictionary, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tileDictionary, ref executioner, ref executionerTile);
            ImmediateBaseDamage = Executioner.CharacterClassObject.Equipment.Weapon1.Damage;
        }

        public override void ImmediateEffect(Stats stats)
        {
            Damage = (int)CalculateAttack(stats);
            stats.HitPoints -= Damage;
            AnimationManager.Cast("attack");
        }

        private double CalculateAttack(Stats stats)
        {
            return ImmediateBaseDamage + (Executioner.CharacterClassObject.CurrentStats.Str * 0.5);
        }
    }
}

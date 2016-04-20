using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class Attack : Action
    {
        private bool crit = false;

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
            AnimationManager.SetDamage(Damage);
            AnimationManager.Attack("attack", crit);
        }

        private double CalculateAttack(Stats stats)
        {
            var damage = ImmediateBaseDamage + (Executioner.CharacterClassObject.CurrentStats.Str * 2.5);

            if (CriticalHit(stats))
                    damage += (Executioner.CharacterClassObject.CurrentStats.Str * 2);

            return damage;
        }

        private bool CriticalHit(Stats stats)
        {
            Random rand = new Random();
            var roll = rand.Next(600);
            if (roll <= stats.CritRate)
                crit = true;

            return crit;
        }
    }
}

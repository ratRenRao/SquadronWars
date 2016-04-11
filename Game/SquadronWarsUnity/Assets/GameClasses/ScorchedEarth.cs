using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class ScorchedEarth : Ability
    {
        public override void Initialize(ref Dictionary<CharacterGameObject, Tile> tileDictionary, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            for(int i = 0; i <= 5; i++)
            {
                tileDictionary.Add(new CharacterGameObject(), new Tile()
                {
                    x = tileDictionary.First().Key.X + i,
                    y = tileDictionary.First().Key.Y + i
                });
            }

            base.Initialize(ref tileDictionary, ref executioner, ref executionerTile);
            ImmediateBaseDamage = 10;
            LingeringBaseDamage = 3;
            //Duration = 5;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
            //ResultingEffects = new List<Effect> {new Burn()};
        }

        public override void ImmediateEffect(Stats stats)
        {
            if (stats.CurHP == 0)
            {
                Damage = (int) CalculateImmediateDamage();
                stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
                stats.CurMP -= mpCost;
                AnimationManager.SetDamage(Damage);
            }
            AnimationManager.Cast("Fire");
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
            return Executioner.CharacterClassObject.CurrentStats.MagicAttack * 0.5 + (AbilityLevel * 0.1) + ImmediateBaseDamage;
        }

        private int CalculateDuration()
        {
            return Executioner.CharacterClassObject.CurrentStats.MagicAttack / 10;
        }
    }
}

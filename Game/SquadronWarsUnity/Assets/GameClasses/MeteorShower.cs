using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
namespace Assets.GameClasses
{
    class MeteorShower : Ability
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            var firstX = tiles.First().x;
            var firstY = tiles.First().y;

            tiles.Clear();
            if(GlobalConstants.GameController.myCharacters.Select(character => character.GetComponent<CharacterGameObject>()).Contains(executioner))
            {
                foreach (GameObject GO in GlobalConstants.GameController.enemyCharacters)
                {
                    tiles.Add(GlobalConstants.GameController.tileMap.tileArray[GO.GetComponent<CharacterGameObject>().X, GO.GetComponent<CharacterGameObject>().Y]);
                }
            }
            else
            {
                foreach (GameObject GO in GlobalConstants.GameController.myCharacters)
                {
                    tiles.Add(GlobalConstants.GameController.tileMap.tileArray[GO.GetComponent<CharacterGameObject>().X, GO.GetComponent<CharacterGameObject>().Y]);
                }
            }
            
            System.Random rand = new System.Random();
            var randomizedTiles= tiles.OrderBy(tile => rand.Next()).ToList();

            base.Initialize(ref randomizedTiles, ref executioner, ref executionerTile);
            ImmediateBaseDamage = 10;
            LingeringBaseDamage = 3;
            //Duration = 5;
            AbilityLevel = AbilityLevel <= 0 ? 1 : AbilityLevel;
            //ResultingEffects = new List<Effect> {new Burn()};
        }

        public override void ImmediateEffect(Stats stats)
        {

            Damage = 999;
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            Executioner.CharacterClassObject.CurrentStats.CurMP -= mpCost;
            AnimationManager.SetDamage(Damage);
            AnimationManager.Cast("MeteorShower");
        }

        public override void LingeringEffect(Stats stats)
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

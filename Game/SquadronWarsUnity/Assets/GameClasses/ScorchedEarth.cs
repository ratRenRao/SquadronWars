using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
namespace Assets.GameClasses
{
    class ScorchedEarth : Ability
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            var firstX = tiles.First().x;
            var firstY = tiles.First().y;

            /*for (int i = 0; i <= 8; i++)
            {
                if (firstX + i > 19
                    || firstY + i > 19)
                    break;

                var tileX = GlobalConstants.GameController.tileMap.tiles
                    .Select(tile => tile.GetComponent<Tile>())
                    .Single(tile => tile.x == firstX + i && tile.y == firstY + i);

                if(tileX != null)
                    tiles.Add(tileX);
            }

            for (int i = 0; i <= 8; i++)
            {
                if (firstX - i < 0 
                    || firstY - i < 0)
                    break;

                var tileY = GlobalConstants.GameController.tileMap.tiles
                    .Select(tile => tile.GetComponent<Tile>())
                    .Single(tile => tile.x == firstX - i && tile.y == firstY - i);

                if (tileY != null)
                    tiles.Add(tileY);
            }*/
            tiles.Clear();
            var tileMap = GlobalConstants.GameController.tileMap;
            for (var i = 0; i < 5; i++)
            {
                for(var j = 0; j < 5; j++)
                {
                    if (firstX + i < tileMap.xLength && firstY + j < tileMap.yLength)
                    {
                        Debug.Log("X: " + (firstX + i) + " Y: " + (firstY + j));
                        tiles.Add(GlobalConstants.GameController.tileMap.tileArray[firstX + i, firstY + j]);
                    }
                }
            }
            for (var i = 1; i < 5; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    if (firstX - i >= 0 && firstY - j >= 0)
                    {
                        Debug.Log("X: " + (firstX - i) + " Y: " + (firstY - j));
                        tiles.Add(GlobalConstants.GameController.tileMap.tileArray[firstX - i, firstY - j]);
                    }
                }
            }

            var rand = new System.Random();
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

            Damage = (int) CalculateImmediateDamage();
            stats.CurHP = stats.CurHP - Damage < 0 ? 0 : stats.CurHP - Damage;
            Executioner.CharacterClassObject.CurrentStats.CurMP -= MpCost;
            AnimationManager.SetDamage(Damage);
            Debug.Log("Scorched " + Damage);
            AnimationManager.Cast("Fire");
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

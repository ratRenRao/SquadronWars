﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.GameClasses
{
    class ScorchedEarth : Ability
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            var firstX = tiles.First().x;
            var firstY = tiles.First().y;

            for (int i = 0; i <= 8; i++)
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
            }

            Random rand = new Random();
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Assets.Scripts;
using UnityEditor;

namespace Assets.GameClasses
{
    public abstract class Effect : IEffectable, IAnimateable
    {
        public string Name { get; set; }
        public double ImmediateBaseDamage = 0;
        public double LingeringBaseDamage = 0; 
        public bool complete = false;
        internal int Duration = 0;
        internal int Damage = 0;
        internal CharacterGameObject Executioner { get; private set; }
        internal Tile ExecutionerTile { get; private set; }
        internal Stopwatch Stopwatch = new Stopwatch();
        internal TimeListener TimeListener;
        internal List<Effect> ResultingEffects;
        internal List<Tile> Tiles = new List<Tile>();
        internal AnimationManager AnimationManager;
        internal Action.ActionType ActionType;

        public virtual void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            Tiles = tiles;
            Executioner = executioner;
            ExecutionerTile = executionerTile;
        }

        public virtual void Execute()
        {
            foreach (var tile in Tiles)
            {
                var character = tile.characterObject.GetComponent<CharacterGameObject>();

                AnimationManager = new AnimationManager(Executioner, character, ExecutionerTile, tile, ActionType, Damage);
                ImmediateEffect(character.CharacterClassObject.CurrentStats);

                if (Duration > 0)
                {
                    TimeListener = new TimeListener(Duration, character.CharacterClassObject.CurrentStats)
                    {
                        ExecutionMethod = LingeringEffect,
                        FinishingMethod = RemoveEffect
                    };

                    TimeListener.Start();
                    GlobalConstants.TimeListeners.Add(character.CharacterClassObject.CharacterId, TimeListener);
                    //LingeringEffect(stats);
                }
                else if (Duration == 0)
                {
                    RemoveEffect();
                }
            }
        }

        public void SetEffectVariables(int immediateBaseDamage, int lingeringBaseDamage, int duration)
        {
            ImmediateBaseDamage = immediateBaseDamage;
            LingeringBaseDamage = lingeringBaseDamage;
            Duration = duration;
        }

        public static int ValidateStat(int stat, int minStat, int maxStat)
        {
            return (stat < minStat) ? minStat : (stat > maxStat) ? maxStat : stat;
        }

        public virtual void ImmediateEffect(Stats stats)
        {
            
        }

        public virtual void RemoveEffect()
        {
            complete = true;
        }

        public  virtual void RemoveEffect(ref Stats stats)
        {
            complete = true;
        }

        public virtual void LingeringEffect(ref Stats stats)
        {
        }
    }
}

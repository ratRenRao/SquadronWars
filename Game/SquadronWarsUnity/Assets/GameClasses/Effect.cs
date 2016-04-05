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
        internal Character Executioner { get; private set; }
        internal Tile ExecutionerTile { get; private set; }
        internal Stopwatch Stopwatch = new Stopwatch();
        internal TimeListener TimeListener;
        internal List<Effect> ResultingEffects;
        internal List<Tile> Tiles = new List<Tile>();
        internal Dictionary<Character, Tile> TileDictionary; 
        internal AnimationManager AnimationManager;
        internal Action.ActionType ActionType;

        public virtual void Initialize(ref Dictionary<Character, Tile> tileDictionary , ref Character executioner, ref Tile executionerTile)
        {
            TileDictionary = tileDictionary;
            Executioner = executioner;
            ExecutionerTile = executionerTile;
        }

        public virtual void Execute()
        {
            foreach (var character in TileDictionary) 
            {
                AnimationManager = new AnimationManager(ExecutionerTile, character.Value, ActionType);
                ImmediateEffect(character.Key.CurrentStats);

                if (Duration > 0)
                {
                    TimeListener = new TimeListener(Duration, character.Key.CurrentStats)
                    {
                        ExecutionMethod = LingeringEffect,
                        FinishingMethod = RemoveEffect
                    };

                    TimeListener.Start();
                    GlobalConstants.TimeListeners.Add(character.Key.CharacterId, TimeListener);
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

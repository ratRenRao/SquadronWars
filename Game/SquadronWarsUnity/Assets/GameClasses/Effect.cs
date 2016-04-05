using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEditor;

namespace Assets.GameClasses
{
    public abstract class Effect : IEffectable
    {
        public string Name { get; set; }
        public double ImmediateBaseDamage = 0;
        public double LingeringBaseDamage = 0; 
        public bool complete = false;
        internal int Duration = 0; 
        internal Dictionary<int, Stats> AffectedCharacterStats { get; private set; }
        internal Stats ExecutionerStats { get; private set; }
        internal Stopwatch Stopwatch = new Stopwatch();
        internal TimeListener TimeListener;
        internal List<Effect> ResultingEffects;


        public virtual void Initialize(ref List<Character> affectedCharacters, ref Stats executionserStats)
        {
            AffectedCharacterStats = affectedCharacters.ToDictionary(
                character => character.CharacterId,
                character => character.CurrentStats
            );
            ExecutionerStats = executionserStats;
        }

        public virtual void Execute()
        {
            foreach (var stats in AffectedCharacterStats)
            {
                ImmediateEffect(stats.Value);

                if (Duration > 0)
                {
                    TimeListener = new TimeListener(Duration, stats.Value)
                    {
                        ExecutionMethod = LingeringEffect,
                        FinishingMethod = RemoveEffect
                    };

                    TimeListener.Start();
                    GlobalConstants.TimeListeners.Add(stats.Key, TimeListener);
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

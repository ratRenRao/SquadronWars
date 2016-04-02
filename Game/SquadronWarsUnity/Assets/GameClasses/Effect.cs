using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        internal List<Stats> AffectedCharacterStats { get; private set; }
        internal Stats ExecutionerStats { get; private set; }
        internal Stopwatch Stopwatch = new Stopwatch();
        internal TimeListener TimeListener;
        internal List<Effect> ResultingEffects;


        public virtual void Initialize(ref List<Stats> affectedCharacterStats, ref Stats executionserStats)
        {
            AffectedCharacterStats = affectedCharacterStats;
            ExecutionerStats = executionserStats;
        }

        public virtual void Execute()
        {
            foreach (var stats in AffectedCharacterStats)
            {
                ImmediateEffect(stats);

                if (Duration > 0 && TimeListener == null && TimeListener.ExecutionMethod != null)
                {
                    TimeListener = new TimeListener(Duration, stats)
                    {
                        ExecutionMethod = LingeringEffect
                    };

                    TimeListener.Start();
                    //LingeringEffect(stats);
                }
                else if (Duration == 0)
                {
                    RemoveEffect(stats);
                }
            }
        }


        public static int ValidateStat(int stat, int minStat, int maxStat)
        {
            return (stat < minStat) ? minStat : (stat > maxStat) ? maxStat : stat;
        }

        public virtual void ImmediateEffect(Stats stats)
        {
            
        }

        public  virtual void RemoveEffect(Stats stats)
        {
            complete = true;
        }

        public virtual void LingeringEffect(Stats stats)
        {
        }
    }
}

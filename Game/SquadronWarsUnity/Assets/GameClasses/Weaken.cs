using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Weaken : Effect, IEffectable
{
    private int decreasePercent;
    private int hpRemoved;

    public Weaken(Stats casterStats, Stats targetStats, bool immediate, int duration)
    {
        this.targetStats = targetStats;
        this.casterStats = casterStats;
        this.immediate = immediate;
        this.duration = duration;
    }

    public void immediateEffect()
    {
        hpRemoved = targetStats.Health * (decreasePercent / 100);
        targetStats.Health -= hpRemoved; 
    }

    public void removeEffect()
    {
        targetStats.Health = ValidateStat(targetStats.Health + hpRemoved, 0, targetStats.MaxHealth);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Heal : Effect, IEffectable
{
    private int healthRestored;

    public Heal(Stats casterStats, Stats targetStats, bool immediate, int duration)
    {
        this.targetStats = targetStats;
        this.casterStats = casterStats;
        this.immediate = immediate;
        this.duration = duration;
    }

    public void immediateEffect()
    {
        targetStats.Health = ValidateStat(targetStats.Health + healthRestored, 0, targetStats.MaxHealth); 
    }

}

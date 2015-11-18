using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Fireball : Burn, IEffectable
{
    private int fireballDamage;

    public Fireball(Stats casterStats, Stats targetStats, bool hasInitialEffect, int duration)
    {
        this.targetStats = targetStats;
        this.casterStats = casterStats;
        this.hasInitialEffect = hasInitialEffect;
        this.duration = duration;
    }

    public override void immediateEffect()
    {
        targetStats.Health -= fireballDamage;
    }
}

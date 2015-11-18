using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Effect : IEffectable
{
    private int duration { get; set; }
    private bool hasInitialEffect = false;
    private Stats casterStats { get; set; }
    private Stats targetStats { get; set; }

    public void execute(ref Stats charStats)
    {
        if (hasInitialEffect)
        {
            initialEffect();
            hasInitialEffect = false;
        }
        else if (duration > 0)
        {
            lingeringEffect();
        }
        else if (duration == 0)
        {
            removeEffect();
        }
    }

    public static int Clamp(int stat, int minStat, int maxStat)
    {
        return (stat < minStat) ? minStat : (stat > maxStat) ? maxStat : stat;
    }

    public void lingeringEffect() { }

    public void immediateEffect() { }

    public void removeEffect() { }

}

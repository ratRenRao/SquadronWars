using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Effect : IEffectable
{
    private int timeRemaining { get; set; }

    public Effect()
    { }

    public Effect(int timeRemaining)
    {
        this.timeRemaining = timeRemaining;
    }

    public void applyEffect(ref int charStat, int statChange)
    {
        charStat += statChange;
    }

    public void removeEffect(ref int charStat, int statChange)
    {
        charStat -= statChange;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Ablilty : IEffectable
{
    public void applyEffect(ref int charStat, int statChange)
    {
        charStat += statChange;
    }

    public void removeEffect(ref int charStat, int statChange)
    {
        charStat -= statChange;
    }
}

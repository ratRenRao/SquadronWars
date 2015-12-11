using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    interface IEffectable
    {
        void execute(Stats charStats);
        void immediateEffect(ref Stats charStat);
        void removeEffect(ref Stats charStat);
        void lingeringEffect(ref Stats charStats);
    }
}

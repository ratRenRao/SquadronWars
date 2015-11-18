using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadronWars2
{
    interface IEffectable
    {
        public void execute(ref Stats charStats);
        public void immediateEffect(ref Stats charStat);
        public void removeEffect(ref Stats charStat);
        public void lingeringEffect(ref Stats charStats);
    }
}

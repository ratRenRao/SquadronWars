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
        public void applyEffect(ref int charStat, int statChange);
        public void removeEffect(ref int charStat, int statChange);
    }
}

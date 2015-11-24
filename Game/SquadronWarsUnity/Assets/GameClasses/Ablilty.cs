using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SquadronWars2
{
    class Ablilty : IEffectable
    {
        public void execute(Stats charStats)
        {
            throw new NotImplementedException();
        }

        public void immediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public void lingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }

        public void removeEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Heal : Effect, IEffectable
{
    private int healthRestored;

    public Heal(int healthRestored)
    {
        this.healthRestored = healthRestored;
    }

    public void execute(ref Stats charStats)
    {
        applyEffect(ref charStats.health, healthRestored);
    }

}

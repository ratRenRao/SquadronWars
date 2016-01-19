using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    public class Equipment : Item
    {
        public Stats Stats;

        public Equipment(string name, ItemType itemType, Stats stats)
        {
            Stats = stats;
            this.Name = name;
            this.ItemType = itemType;
        }

    }


}

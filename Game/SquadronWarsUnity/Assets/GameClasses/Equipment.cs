using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    public class Equipment : Item
    {
        public Stats stats;

        public Equipment(string name, ItemType itemType, Stats stats)
        {
            this.stats = stats;
            this.name = name;
            this.itemType = itemType;
        }

    }

   
}
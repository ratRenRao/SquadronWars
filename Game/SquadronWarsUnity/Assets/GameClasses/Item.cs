using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    public enum ItemType
    {
        HELM,
        CHEST,
        GLOVES,
        LEGS,
        SHOULDERS,
        BOOTS,
        ACCESSORY1,
        ACCESSORY2,
    };

    public class Item : IWearable
    {

        public ItemType itemType { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public int itemListId { get; set; }

    }
}
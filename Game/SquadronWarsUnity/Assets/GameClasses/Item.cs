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

    class Item : IWearable
    {

        private ItemType itemType { get; set; }
        private string name { get; set; }
        private int id { get; set; }
        private int itemListId { get; set; }

    }
}
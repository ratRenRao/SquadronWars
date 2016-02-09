using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameClasses
{
    public class Equipment
    {
        //public Stats stats { get; set; }
        public Item helm { get; set; }
        public Item chest { get; set; }
        public Item gloves{ get; set; }
        public Item pants{ get; set; }
        public Item shoulders{ get; set; }
        public Item boots{ get; set; }
        public Item accessory1{ get; set; }
        public Item accessory2{ get; set; }

        public Equipment()
        {
            
        }

        public List<Item> GetEquipmentItems()
        {
            return new List<Item>()
            {
                helm,
                chest,
                gloves,
                pants,
                shoulders,
                boots,
                accessory1,
                accessory2
            };
        }
    }
}

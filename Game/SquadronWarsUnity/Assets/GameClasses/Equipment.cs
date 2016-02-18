using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameClasses
{
    public class Equipment
    {
        //public Stats stats { get; set; }
        public Item Helm { get; set; }
        public Item Chest { get; set; }
        public Item Gloves{ get; set; }
        public Item Pants{ get; set; }
        public Item Shoulders{ get; set; }
        public Item Boots{ get; set; }
        public Item Accessory1{ get; set; }
        public Item Accessory2{ get; set; }

        public Equipment(Item helm, Item chest, Item gloves, Item pants, Item shoulders, Item boots,
            Item accessory1, Item accessory2)
        {
            Helm = helm;
            Chest = chest;
            Gloves = gloves;
            Pants = pants;
            Shoulders = shoulders;
            Boots = boots;
            Accessory1 = accessory1;
            Accessory2 = accessory2;
        }

        public List<Item> GetItemList()
        {
            return new List<Item>() { 
                Helm,
                Chest,
                Gloves,
                Pants,
                Shoulders,
                Boots,
                Accessory1,
                Accessory2
           };
       } 
   }
}

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

        public void SetItemByType(Item item)
        {
            switch (item.ItemType)
            {
                case ItemType.Helm:
                    Helm = item;
                    break;
                case ItemType.Boots:
                    Boots = item;
                    break;
                case ItemType.Chest:
                    Chest = item;
                    break;
                case ItemType.Gloves:
                    Gloves = item;
                    break;
                case ItemType.Legs:
                    Pants = item;
                    break;
                case ItemType.Shoulders:
                    Shoulders = item;
                    break;
                case ItemType.Accessory1:
                    Accessory1 = item;
                    break;
                case ItemType.Accessory2:
                    Accessory2 = item;
                    break;
            }
            
        }

   }
}

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.GameClasses
{
    public class Equipment : IJsonable
    {
        //public Stats stats { get; set; }
        public Item Helm { get; set; }
        public Item Chest { get; set; }
        public Item Gloves { get; set; }
        public Item Pants { get; set; }
        public Item Shoulders { get; set; }
        public Item Boots { get; set; }
        public Item Weapon { get; set; }
        public Item Offhand { get; set; }
        public Item Accessory1 { get; set; }
        public Item Accessory2 { get; set; }

        public Equipment()
        {
            
        }

        public Equipment(Item helm = null, Item chest = null, Item gloves = null, Item pants = null, Item shoulders = null, Item boots = null,
            Item accessory1 = null, Item accessory2 = null, Item weapon = null, Item offhand = null)
        {
            Helm = helm;
            Chest = chest;
            Gloves = gloves;
            Pants = pants;
            Shoulders = shoulders;
            Boots = boots;
            Accessory1 = accessory1;
            Accessory2 = accessory2;
            Weapon = weapon;
            Offhand = offhand;
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
                Accessory2,
                Weapon,
                Offhand
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
                case ItemType.MainHand:
                    Weapon = item;
                    break;
                case ItemType.OffHand:
                    Offhand = item;
                    break;
                    
                case ItemType.Accessory:
                    if (Accessory1 == null)
                    {
                        Accessory1 = item;
                    }
                    else
                    {
                        Accessory2 = item;
                    }
                    break;
            }
            
        }

        public string GetJSONString()
        {            
            return "{ \"Helm\" : \"" + Helm.ItemId + "\", \"Chest\" : \"" + Chest.ItemId + "\", \"Gloves\" : \"" + Gloves.ItemId + "\", \"Pants\" : \"" + Pants.ItemId + "\", \"Shoulders\" : \"" + Shoulders.ItemId
                + "\", \"Boots\" : \"" + Boots.ItemId + "\", \"Weapon\" : \"" + Weapon.ItemId + "\", \"Offhand\" : \"" + Offhand.ItemId + "\", \"Accessory1\" : \"" + Accessory1.ItemId + "\", \"Accessory2\" : \"" + Accessory2.ItemId + "\" }";
        }

        public string GetJsonObjectName()
        {
            return "Equipment";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new System.NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}

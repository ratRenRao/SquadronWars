using UnityEngine;

namespace Assets.GameClasses
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

    public class Item : Equipment, IWearable, IJsonable
    {

        public ItemType itemType { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public int itemListId { get; set; }
        public Stats stats { get; set; }

        public Item()
        { 
        }

        public Item(string name, ItemType itemType, Stats stats)
        {
            this.name = name;
            this.itemType = itemType;
            this.stats = stats;
        }

        public string GetJsonObjectName()
        {
            throw new System.NotImplementedException();
        }
    }
}
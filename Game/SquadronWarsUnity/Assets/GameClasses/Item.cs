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
        ACCESSORY,
    };

    public class Item : Equipment, IWearable, IJsonable
    {
        public int helm { get; set; }
        public int chest { get; set; }
        public int gloves { get; set; }
        public int pants { get; set; }
        public int shoulders { get; set; }
        public int boots { get; set; }
        public int accessory1 { get; set; }
        public int accessory2 { get; set; }
        public int ItemId { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public string Slot { get; set; }
        public int RequiredLevel { get; set; }
        public int RequiredStr { get; set; }
        public int RequiredDex { get; set; }
        public int RequiredInt { get; set; }
        public int Str { get; set; }
        public int Intl { get; set; }
        public int Agi { get; set; }
        public int Wis { get; set; }
        public int Vit { get; set; }
        public int Dex { get; set; }
        public int HitPoints { get; set; }
        public int Dmg { get; set; }
        public int AbilityPoints { get; set; }
        public int Speed { get; set; }
        public int Defense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int HitRate { get; set; }
        public int CritRate { get; set; }
        public int DodgeRate { get; set; }
        public ItemType itemType { get; set; }
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
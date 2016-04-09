using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.GameClasses
{
    public enum ItemType
    {
        Helm,
        Chest,
        Gloves,
        Legs,
        Shoulders,
        Boots,
        Accessory,
        MainHand,
        OffHand,
        Unique
    };

    public class Item : Effect, IWearable, IJsonable
    {
        public int ItemId { get; set; }
        public int Consumeable { get; set; }

        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

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
        public int Luck { get; set; }
        public Stats Stats = new Stats();
        public ItemType ItemType;

        public void BuildStats()
        {
            Stats = new Stats(
                Str,
                Agi,
                Intl,
                Vit,
                Wis,
                Dex,
                Luck,
                HitPoints,
                Dmg,
                MagicAttack,
                Speed,
                Defense,
                MagicDefense,
                HitRate,
                DodgeRate,
                CritRate);
        }

        public string GetJsonObjectName()
        {
            return "Items";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
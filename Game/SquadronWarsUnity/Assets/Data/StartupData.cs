using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameClasses;

namespace Assets.Data
{
    class StartupData : IJsonable
    {
        public Player Player { get; set; }
        public List<CharacterData> Characters { get; set; }
        public List<AbilityData> CharacterAbilities { get; set; }
        public List<InventoryData> Inventory { get; set; }
        public List<EquipmentData> Equipment { get; set; }
        public List<ItemData> Items { get; set; }

        public string GetJsonObjectName()
        {
            return GlobalConstants.StartupDataJsonName;
        }

        public class ItemData : IJsonable, IIdable
        {
            public int ItemId { get; set; }
            public bool ConsumeId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public string GetJsonObjectName()
            {
                return "Items";
            }

            public int GetId()
            {
                return ItemId;
            }
        }


        public class EquipmentData : IJsonable, IIdable
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
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

            public string GetJsonObjectName()
            {
                return "Equipment";
            }

            public int GetId()
            {
                return ItemId;
            }
        }

        public class InventoryData : IJsonable, IIdable
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }

            public string GetJsonObjectName()
            {
                return "Inventory";
            }

            public int GetId()
            {
                return ItemId;
            }
        }

        public class AbilityData : IJsonable, IIdable
        {
            public int AbilityId { get; set; }
            public int CharacterId { get; set; }
            public int AbilityLevel { get; set; }

            public string GetJsonObjectName()
            {
                return "CharacterAbilities";
            }

            public int GetId()
            {
                return AbilityId;
            }
        }

        public class CharacterData : IJsonable, IIdable
        {
            public int CharacterId { get; set; }
            public int StatId { get; set; }
            public int StatPoints { get; set; }
            public int SkillPoints { get; set; }
            public int LevelId { get; set; }
            public string Name { get; set; }
            public int Experience { get; set; }
            public int Helm { get; set; }
            public int Chest { get; set; }
            public int Gloves { get; set; }
            public int Pants { get; set; }
            public int Shoulders { get; set; }
            public int Boots { get; set; }
            public int Accessory1 { get; set; }
            public int Accessory2 { get; set; }
            public bool IsStandard { get; set; }

            public string GetJsonObjectName()
            {
                return "";
            }

            public int GetId()
            {
                return CharacterId;
            }
        }

        /*
        public class StartupObjects
        {
            public static Player Player { get; set; }
            public static Squad Squad { get; set; }
            public static Dictionary<int, EquipmentData> EquipmentDictionary { get; set; }
            public static Dictionary<int, AbilityData> AbilitiesDictionary { get; set; }
            public static Dictionary<int, InventoryData> InventoryDictionary { get; set; }
            public static Dictionary<int, ItemData> ItemDictionary { get; set; }

            public void PopulateObjects()
            {
                EquipmentDictionary = ConvertToDictionary(StartupData.Equipment);
                AbilitiesDictionary = ConvertToDictionary(CharacterAbilities);
                InventoryDictionary = ConvertToDictionary(Inventory);
                ItemDictionary = ConvertToDictionary(Items);

                PopulateObjects();
                Player = StartupData.Player;
              /*  if (Player.squad == null)
                {
                    foreach(var charData in Characters)
                        Squad.characterList.Add(PopulateObject<Character>(typeof (Character)));
                }
                
            }

            public static T PopulateObject<T>(Type type) where T : new()
            {
                var obj = new T();
                return obj;
            }

            private static Dictionary<int, T> ConvertToDictionary<T>(List<T> list) where T : IIdable
            {
                return list.ToDictionary(key => key.GetId(), value => value); 
            }  
        }
*/

        public interface IIdable
        {
            int GetId();
        }

        string IJsonable.GetJsonObjectName()
        {
            return "";
        }
    }
}

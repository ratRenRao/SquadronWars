using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Assets.GameClasses;

namespace Assets.Data
{
    public class StartupData : IJsonable
    {
        public static Player Player { get; set; }
        public static List<InventoryElement> Inventory { get; set; }
        public static List<CharacterData> Characters { get; set; }
        public static List<Ability> Abilities { get; set; }
        public static List<AbilityPreReq> AbilityPreReqs { get; set; }
        public static List<Item> Items { get; set; }


        //private Player _inventory { get; set; }

        public string GetJsonObjectName()
        {
            return "GameObject";
        }

        public static void BuildAndDistributeData()
        {
            BuildGlobalData();
            BuildPlayerData();
        }

        public static void SetItemType()
        {
            foreach (var item in Player.Inventory)
            {
                item.Item.ItemType = GetType(item.Item.Slot);
            }        
        }

        public static void BuildPlayerData()
        {
            // Fix this hack. Probably an issue w/ it being partially populated in Utilities.BuildObject
            Player.Characters = new List<Character>();

            Player.Characters = BuildCharacterObjects();
            GlobalConstants.Player = Player;
            GlobalConstants.CharacterLoadReady = true;
        }

        public static void BuildGlobalData()
        {
            // Fix this hack. Probably and issue w/ it being populated in Utilities.BuildObject
            Player.Inventory = new List<InventoryElement>();

            PopulateGlobalConstants();
            BuildInventoryList();
            SetItemType();
        }

        private static ItemType GetType(string typeString)
        {
            switch (typeString)
            {
                case "H":
                    return ItemType.Helm;
                case "C":
                    return ItemType.Chest;
                case "L":
                    return ItemType.Legs;
                case "G":
                    return ItemType.Gloves;
                case "B":
                    return ItemType.Boots;
                case "S":
                    return ItemType.Shoulders;
                case "M":
                    return ItemType.MainHand;
                case "O":
                    return ItemType.OffHand;
                case "A":
                    return ItemType.Accessory;
                default:
                    return ItemType.Unique;
            }
        }

        public static void BuildInventoryList()
        {
            foreach (var invItem in Inventory)
            {
                invItem.Item = GlobalConstants.ItemsMasterList.SingleOrDefault(item => item.ItemId == invItem.ItemId);
            }

            Player.Inventory = Inventory;
        }

        public static void PopulateGlobalConstants()
        {
            GlobalConstants.AbilityPreReqs = AbilityPreReqs;
            GlobalConstants.ItemsMasterList = Items.Where(item => item != null).ToList();
            GlobalConstants.AbilityMasterList = Abilities;
        }

        public static List<Character> BuildCharacterObjects()
        {
            var tempCharacterData = Characters;
            List<Character> characterList = new List<Character>();

            foreach (var character in tempCharacterData)
            {
                var characterBuilder = new Character();
                foreach (var property in Utilities.GetParameterList(typeof(Character))
                    .Where(param => Utilities.GetParameterList(typeof(CharacterData))
                    .Select(x => x.Name).Contains(param.Name)).ToList())
                {
                    characterBuilder.GetType().GetProperty(property.Name).SetValue(characterBuilder, character.GetType().GetProperty(property.Name).GetValue(character, null), null);
                }

                characterBuilder.Equipment = character.BuildEquipment();
                characterBuilder.BaseStats = character.BuildBaseStats();
                characterBuilder.CurrentStats = AddItemStats(characterBuilder.Equipment.GetItemList(), characterBuilder.BaseStats);

               // characterBuilder.BaseStats.AbilityPoints = 3;
               // characterBuilder.BaseStats.SkillPoints = 2;
               
                //characterBuilder.CurrentStats = character.BuildBaseStats();
                character.CharacterAbilities.ForEach(ability => ability.Name = Abilities.Single(x => ability.AbilityId == x.AbilityId).Name);
                characterBuilder.Abilities = character.CharacterAbilities;

                characterList.Add(characterBuilder);
                //Player.Characters.Add(characterBuilder);
            }

            return characterList;
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public static Stats AddItemStats(List<Item> items, Stats stats)
        {
            Stats newStats = new Stats();
            var itemList = items.Where(item => item != null).Where(item => item.Stats != null).ToList();
            foreach (var item in itemList)
            {
                newStats = item.Stats.ConcatStats(stats, item.Stats);
            }

            return newStats;
        }

        public class CharacterAbility : IJsonable
        {
            public int AbilityId { get; set; }
            public int CharacterId { get; set; }
            public int AbilityLevel { get; set; }
        
            public string GetJsonObjectName()
            {
                return "CharacterAbilities";
            }

            public List<PropertyInfo> GetJsonObjectParameters()
            {
                throw new NotImplementedException();
            }

            public void SetJsonObjectParameters(Dictionary<string, object> parameters)
            {
                throw new NotImplementedException();
            }
        }

        public class CharacterData : IJsonable
        {
            public int CharacterId { get; set; }
            public int LevelId { get; set; }
            public string Name { get; set; }
            public int Helm { get; set; }
            public int Chest { get; set; }
            public int Gloves { get; set; }
            public int Pants { get; set; }
            public int Shoulders { get; set; }
            public int Boots { get; set; }
            public int Accessory1 { get; set; }
            public int Accessory2 { get; set; }
            public int IsStandard { get; set; }
            public int StatPoints { get; set; }
            public int SkillPoints { get; set; }
            public int Experience { get; set; }
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
            public int SpriteId { get; set; }
            public List<Ability> CharacterAbilities { get; set; } 

            private Equipment _equipment { get; set; }
            private Stats _stats { get; set; }

            public Stats BuildBaseStats()
            {
                _stats = new Stats(
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
                    CritRate,
                    StatPoints,
                    SkillPoints);

                return _stats;
            }

            public Equipment BuildEquipment()
            {
                Func<int, Item> getItemFunc = (x) => GlobalConstants.ItemsMasterList.SingleOrDefault(item => item.ItemId == x);

                _equipment = new Equipment(
                    getItemFunc(Helm),
                    getItemFunc(Chest),
                    getItemFunc(Gloves),
                    getItemFunc(Pants),
                    getItemFunc(Shoulders),
                    getItemFunc(Boots),
                    getItemFunc(Accessory1),
                    getItemFunc(Accessory2));

                return _equipment;
            }

            public Equipment GetEquipment()
            {
                return _equipment;
            }

            public Stats GetStats()
            {
                return _stats;
            }

            public string GetJsonObjectName()
            {
                return "Characters";
            }

            public List<PropertyInfo> GetJsonObjectParameters()
            {
                throw new NotImplementedException();
            }

            public void SetJsonObjectParameters(Dictionary<string, object> parameters)
            {
                throw new NotImplementedException();
            }
        }

        public class InventoryElement : IJsonable
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
            public Item Item { get; set; }

            public string GetJsonObjectName()
            {
                return "Inventory";
            }

            public List<PropertyInfo> GetJsonObjectParameters()
            {
                throw new NotImplementedException();
            }

            public void SetJsonObjectParameters(Dictionary<string, object> parameters)
            {
                throw new NotImplementedException();
            }
        }
    }
}

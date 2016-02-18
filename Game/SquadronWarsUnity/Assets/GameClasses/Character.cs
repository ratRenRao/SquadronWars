using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.GameClasses;
using UnityEngine;
//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    //"Characters":[{"characterId":"1","statId":"1","statPoints":"5","skillPoints":"1","LevelID":"1","name":"Lancelot Test","experience":"100","helm":"1","chest":"1000","gloves":"3000","pants":"2000","shoulders":"4000","boots":"5000","accessory1":null,"accessory2":null,"IsStandard":null}
    public class Character : IJsonable
    {
        public int X;
        public int Y;

        public int CharacterId { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        /*
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
        */
        public Sprite Sprite { get; set; }

        public bool Updated = false;
        public Stats BaseStats { get; set; }
        public Stats CurrentStats { get; set; }
        public Equipment Equipment { get; set; }
        public List<Ability> Abilities { get; set; } 

        //private List<Item> _items { get; set; }

        /*
        public int StatId { get; set; }
        //public Stats baseStats { get; set; }
        //public Stats alteredStats { get; set; }
        //public Equipment equipment { get; set; }
        public Dictionary<string, int> skillList { get; set; }
        public int level { get; set; }
        //public List<Effect> effects  { get; set; }
        
        //public readonly bool Updated = false;
        
        public int spriteId { get; set; }
        public bool Updated = false;
        public Stats stats { get; set; }
        public Stats baseStats { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        public List<Ability> abilities { get; set; }
        public List<Effect> effects { get; set; }
        public Equipment equipment { get; set; }
        public Stats alteredStats { get; set; }
        public int strength { get; set; }
        public int agility { get; set; }
        public int intelligence { get; set; }
        public int vitality { get; set; }
        public int dexterity { get; set; }
        public int wisdom { get; set; }
        public int currentHP { get; set; }
        public int maxHP { get; set; }
        public int damage { get; set; }
        public int currentMP { get; set; }
        public int maxMP { get; set; }
        public int magicDef { get; set; }
        public int magicDmg { get; set; }
        */

        public Character()
        {
        }

        
/*
        public Character(int characterId, Stats baseStats, int characterListId, string characterName,
            int level, int experience, Equipment equipment)
        {
            this.characterId = characterId;
            this.baseStats = baseStats;
            this.equipment = equipment;
            this.characterListId = characterListId;
            this.characterName = characterName;
            this.level = level;
            this.experience = experience;
        }
        */

            /*
        public void addEffect(Effect effect)
        {
            var tempStats = baseStats;
            effect.Execute(ref tempStats);
            baseStats = tempStats;
            effects.Add(effect);
        }
        */

        public void checkEffects()
        {
            /*
            foreach (Effect effect in effects)
            {
            }
            */
        }

        public int startExperience()
        {
            if (LevelId == 1)
            {
                return 0;
            }
            return 200 + ((int)Math.Pow(LevelId - 1, 2) * 50);
        }

        public int experienceNeeded()
        {
            if (LevelId == 1)
            {
                return 200;
            }
            return 200 + ((int)Math.Pow(LevelId, 2) * 50);
        }

        public int percentToNextLevel()
        {
            int startExp = startExperience();
            int finishExp = experienceNeeded();
            double percentComplete = ((double)(BaseStats.Experience - startExp) / (finishExp - startExp) * 100);
            return Convert.ToInt32(percentComplete);
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        /*
        public void BuildStats()
        {
            BaseStats = new Stats(
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

            CurrentStats = BaseStats;
        }

        public void BuildEquipment()
        {
            Func<int, Item> getItemFunc = x => GlobalConstants.ItemsMasterList.Single(item => item.ItemId == x);

            Equipment = new Equipment(
                    getItemFunc(Helm),
                    getItemFunc(Chest),
                    getItemFunc(Gloves),
                    getItemFunc(Pants),
                    getItemFunc(Shoulders),
                    getItemFunc(Boots),
                    getItemFunc(Accessory1),
                    getItemFunc(Accessory2)
                );
        }

        public List<Item> GetEquipedItems()
        {
            Func<int, Item> getItemFunc = x => GlobalConstants.ItemsMasterList.Single(item => item.ItemId == x);

            return new List<Item>()
            {
                getItemFunc(Helm),
                getItemFunc(Chest),
                getItemFunc(Gloves),
                getItemFunc(Pants),
                getItemFunc(Shoulders),
                getItemFunc(Boots),
                getItemFunc(Accessory1),
                getItemFunc(Accessory2)
            };
        }

        */

        /*public async Task UpdateCharacterFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl);
            Character dbCharacter = dbConnection.DeserializeData<Character>(this);

            this.stats = dbCharacter.stats;
            this.characterListId = dbCharacter.characterListId;
            this.name = dbCharacter.name;
            this.LevelId = dbCharacter.LevelId;
            this.experience = dbCharacter.experience;
        }*/
    }
}
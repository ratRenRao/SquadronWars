using System;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine;
//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    //l,\"accessory2\":null,\"IsStandard\":null},{\"characterId\":\"2\",\"statId\":\"2\",\"statPoints\":\"0\",\"skillPoints\":\"0\",\"LevelID\":\"1\",\"name\":\"TestChar2\",\"experience\":\"0\",\"helm\":\"1\",\"chest\":\"1000\",\"gloves\":\"3000\",\"pants\":\"2000\",\"shoulders\":\"4000\",\"boots\":null,\"accessory1\":null,\"accessory2\":null,\"IsStandard\"
    public class Character : MonoBehaviour, IJsonable
    {
        public int helm;
        public int chest;
        public int gloves;
        public int pants;
        public int shoulders;
        public int boots;
        public int accessory1;
        public int accessory2;
        public bool IsStandard;
        public int x;
        public int y;
        //DBConnection dbConnection = new DBConnection();
        public new string name;
        public int characterId { get; set; }
        public int statId;
        public Stats baseStats { get; set; }
        public Stats alteredStats { get; set; }
        public int characterListId { get; set; }
        public string characterName { get; set; }
        public Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>();
        public Dictionary<string, int> skillList = new Dictionary<string, int>();
        public int level { get; set; }
        public int experience { get; set; }
        public int statPoints { get; set; }
        public int skillPoints { get; set; }
        public List<Effect> effects = new List<Effect>();
        public int spriteId;

        public Character()
        {
            Initialize(0, null, 0, null, 0, 0, null); 
        }

        public Character(int characterId, Stats baseStats, int characterListId, string characterName,
            int level, int experience, Dictionary<ItemType, Item> equipment)
        {
            Initialize(characterId, baseStats, characterListId, characterName, level, experience, equipment);
        }

        private void Initialize(int characterId, Stats baseStats, int characterListId, string characterName,
            int level, int experience, Dictionary<ItemType, Item> equipment)
        {
            this.characterId = characterId;
            this.baseStats = baseStats;
            this.equipment = equipment;
            this.characterListId = characterListId;
            this.characterName = characterName;
            this.level = level;
            this.experience = experience;
        }

        public void addEffect(Effect effect)
        {
            var tempStats = baseStats;
            effect.execute(ref tempStats);
            baseStats = tempStats;
            effects.Add(effect);
        }

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
            if (level == 1)
            {
                return 0;
            }
            return 200 + ((int)Math.Pow(level - 1, 2) * 50);
        }

        public int experienceNeeded()
        {
            if (level == 1)
            {
                return 200;
            }
            return 200 + ((int)Math.Pow(level, 2) * 50);
        }

        public int percentToNextLevel()
        {
            int startExp = startExperience();
            int finishExp = experienceNeeded();
            double percentComplete = ((double)(experience - startExp) / (finishExp - startExp) * 100);
            return Convert.ToInt32(percentComplete);
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }


        /*public async Task UpdateCharacterFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl);
            Character dbCharacter = dbConnection.DeserializeData<Character>(this);

            this.stats = dbCharacter.stats;
            this.characterListId = dbCharacter.characterListId;
            this.name = dbCharacter.name;
            this.level = dbCharacter.level;
            this.experience = dbCharacter.experience;
        }*/
    }
}
using System;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine;
//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    //"Characters":[{"characterId":"1","statId":"1","statPoints":"5","skillPoints":"1","LevelID":"1","name":"Lancelot Test","experience":"100","helm":"1","chest":"1000","gloves":"3000","pants":"2000","shoulders":"4000","boots":"5000","accessory1":null,"accessory2":null,"IsStandard":null}
    public class Character : IJsonable
    {
        
        public bool IsStandard;
        public int x;
        public int y;
        //DBConnection dbConnection = new DBConnection();
        public new string name { get; set; }
        public int characterId { get; set; }
        public int statId { get; set; }
        public Stats baseStats { get; set; }
        public Stats alteredStats { get; set; }
        public int characterListId { get; set; }
        public string characterName { get; set; }
        public Equipment equipment { get; set; }
        public Dictionary<string, int> skillList { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public int statPoints { get; set; }
        public int skillPoints { get; set; }
        public List<Effect> effects  { get; set; }
        public Sprite sprite { get; set; }
        public readonly bool Updated = false;

        public Character()
        {
        }

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
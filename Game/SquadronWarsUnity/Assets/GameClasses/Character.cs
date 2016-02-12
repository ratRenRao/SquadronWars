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
        public int x;
        public int y;
        
        public new string name { get; set; }
        public int characterId { get; set; }
        public int statId { get; set; }
        //public Stats baseStats { get; set; }
        //public Stats alteredStats { get; set; }
        public int characterListId { get; set; }
        public string characterName { get; set; }
        //public Equipment equipment { get; set; }
        public Dictionary<string, int> skillList { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public int statPoints { get; set; }
        public int skillPoints { get; set; }
        //public List<Effect> effects  { get; set; }
        
        //public readonly bool Updated = false;
        
        public Sprite sprite { get; set; }
        public bool Updated = false;
        public int CharacterId { get; set; }
        public Stats stats { get; set; }
        public Stats baseStats { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }
        public int helm { get; set; }
        public int chest { get; set; }
        public int gloves { get; set; }
        public int pants { get; set; }
        public int shoulders { get; set; }
        public int boots { get; set; }
        public int accessory1 { get; set; }
        public int accessory2 { get; set; }
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
        public int luck { get; set; }
        public int currentHP { get; set; }
        public int maxHP { get; set; }
        public int damage { get; set; }
        public int defense { get; set; }
        public int currentMP { get; set; }
        public int maxMP { get; set; }
        public int speed { get; set; }
        public int magicDef { get; set; }
        public int magicDmg { get; set; }
        public int hitRate { get; set; }
        public int critRate { get; set; }
        public int dodgeRate { get; set; }
        public int SkillPoints { get; set; }
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
            effect.execute(ref tempStats);
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
            double percentComplete = ((double)(stats.Experience - startExp) / (finishExp - startExp) * 100);
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
            this.LevelId = dbCharacter.LevelId;
            this.experience = dbCharacter.experience;
        }*/
    }
}
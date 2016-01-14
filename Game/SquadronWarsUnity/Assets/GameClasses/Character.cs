using System;
using UnityEngine;
using System.Collections.Generic;
using DbConnection = SquadronWars2.Game.SquadronWarsUnity.Repo.DbConnection;

namespace SquadronWars2
{
    public class Character : MonoBehaviour
    {
        private DbConnection _dbConnection;
        public int CharacterId { get; set; }
        public Stats BaseStats { get; set; }
        public Stats AlteredStats { get; set; }
        public int CharacterListId { get; set; }
        public string CharacterName { get; set; }
        public Dictionary<ItemType, Item> Equipment;
        public int Level { get; set; }
        public int Experience { get; set; }
        public int StatPoints { get; set; }
        public int SkillPoints { get; set; }
        public List<Effect> Effects = new List<Effect>();
        public Sprite Sprite;

        public Character(int characterId, Stats baseStats, int characterListId, string characterName,
            int level, int experience, Dictionary<ItemType, Item> equipment)
        {
            _dbConnection = new DbConnection();
            CharacterId = characterId;
            BaseStats = baseStats;
            Equipment = equipment;
            CharacterListId = characterListId;
            CharacterName = characterName;
            Level = level;
            Experience = experience;
        }

        public void AddEffect(Effect effect)
        {
            effect.Execute(BaseStats);
            Effects.Add(effect);
        }

        public void CheckEffects()
        {
            foreach (var effect in Effects)
            {

            }
        }

        public int StartExperience()
        {
            if (Level == 1)
            {
                return 0;
            }
            return 200 + ((int)Math.Pow(Level - 1, 2) * 50);
        }

        public int ExperienceNeeded()
        {
            if (Level == 1)
            {
                return 200;
            }
            return 200 + ((int)Math.Pow(Level, 2) * 50);
        }

        public int PercentToNextLevel()
        {
            var startExp = StartExperience();
            var finishExp = ExperienceNeeded();
            var percentComplete = ((double)(Experience - startExp) / (finishExp - startExp) * 100);
            return Convert.ToInt32(percentComplete);
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
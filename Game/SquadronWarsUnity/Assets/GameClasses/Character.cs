using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquadronWars.Game.SquadronWarsUnity.Repo

namespace SquadronWars2
{
    public class Character : MonoBehaviour
    {
        DbConnection dbConnection = new DbConnection();
        public int characterId { get; set; }
        public Stats baseStats { get; set; }
        public Stats alteredStats { get; set; }
        public int characterListId { get; set; }
        public string characterName { get; set; }
        public Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>();
        public int level { get; set; }
        public int experience { get; set; }
        public int statPoints { get; set; }
        public int skillPoints { get; set; }
        public List<Effect> effects = new List<Effect>();
        public Sprite sprite;

        public Character(int characterId, Stats baseStats, int characterListId, string characterName,
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
            effect.execute(baseStats);
            effects.Add(effect);
        }

        public void checkEffects()
        {
            foreach (Effect effect in effects)
            {

            }
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
            return 200 + ((int)Math.Pow(level,2) * 50) ;
        }

        public int percentToNextLevel()
        {
            int startExp = startExperience();
            int finishExp = experienceNeeded();
            double percentComplete = ((double)(experience - startExp) / (finishExp - startExp) * 100);
            return Convert.ToInt32(percentComplete);
        }

        public async Task UpdateCharacterFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl);
            Character dbCharacter = dbConnection.DeserializeData<Character>(this);

            this.stats = dbCharacter.stats;
            this.characterListId = dbCharacter.characterListId;
            this.name = dbCharacter.name;
            this.level = dbCharacter.level;
            this.experience = dbCharacter.experience;
     }
}
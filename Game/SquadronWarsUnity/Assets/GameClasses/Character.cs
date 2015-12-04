using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    public class Character
    {
        private int characterId { get; set; }
        private Stats stats { get; set; }
        private int characterListId { get; set; }
        public string name { get; set; }
        private int level { get; set; }
        private int experience { get; set; }
        private List<Effect> effects = new List<Effect>();

        public Character(int characterId, Stats stats, int characterListId, string name,
            int level, int experience)
        {
            this.characterId = characterId;
            this.stats = stats;
            this.characterListId = characterListId;
            this.name = name;
            this.level = level;
            this.experience = experience;
        }

        public void addEffect(Effect effect)
        {
            effect.execute(stats);
            effects.Add(effect);
        }

        public void checkEffects()
        {
            foreach (Effect effect in effects)
            {

            }
        }
    }
}
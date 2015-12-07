using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    public class Character
    {
        public int characterId { get; set; }
        public Stats stats { get; set; }
        public int characterListId { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public List<Effect> effects = new List<Effect>();
        public Sprite sprite;
        public Button.ButtonClickedEvent buildCharacterSheet;

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
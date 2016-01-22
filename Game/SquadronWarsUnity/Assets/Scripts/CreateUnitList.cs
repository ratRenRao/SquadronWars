﻿using UnityEngine;
using SquadronWars2;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
﻿using System.Linq;

namespace SquadronWars2
{
    public class CreateUnitList : MonoBehaviour
    {
        public GameObject SampleButton;
        public List<Character> Characters;
        public Transform ContentPanel;
        // Use this for initialization
        void Start()
        {
            PopulateList();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void PopulateList()
        {
            Characters = new List<Character>();
            var equipment = new Dictionary<ItemType, Item>
        {
            {ItemType.Helm, GlobalConstants.ItemList["Cloth Helm"] },
            {ItemType.Shoulders, GlobalConstants.ItemList["Cloth Shoulders"] },
            {ItemType.Chest, GlobalConstants.ItemList["Cloth Chest"] },
            {ItemType.Gloves, GlobalConstants.ItemList["Cloth Gloves"] },
            {ItemType.Legs, GlobalConstants.ItemList["Cloth Legs"] },
            {ItemType.Boots, GlobalConstants.ItemList["Cloth Boots"] },
        };
            var equipment1 = new Dictionary<ItemType, Item>
        {
            {ItemType.Helm, GlobalConstants.ItemList["Leather Helm"] },
            {ItemType.Shoulders, GlobalConstants.ItemList["Leather Shoulders"] },
            {ItemType.Chest, GlobalConstants.ItemList["Leather Chest"] },
            {ItemType.Gloves, GlobalConstants.ItemList["Leather Gloves"] },
            {ItemType.Legs, GlobalConstants.ItemList["Leather Legs"] },
            {ItemType.Boots, GlobalConstants.ItemList["Leather Boots"] },
        };
            var equipment2 = new Dictionary<ItemType, Item>
        {
            {ItemType.Helm, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.Shoulders, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.Chest, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.Gloves, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.Legs, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.Boots, GlobalConstants.ItemList["None(Feet)"] },
        };
            var equipment3 = new Dictionary<ItemType, Item>
        {
            {ItemType.Helm, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.Shoulders, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.Chest, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.Gloves, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.Legs, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.Boots, GlobalConstants.ItemList["None(Feet)"] },
        };
            var equipment4 = new Dictionary<ItemType, Item>
        {
            {ItemType.Helm, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.Shoulders, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.Chest, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.Gloves, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.Legs, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.Boots, GlobalConstants.ItemList["None(Feet)"] },
        };
            var equipment5 = new Dictionary<ItemType, Item>
        {
            {ItemType.Helm, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.Shoulders, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.Chest, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.Gloves, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.Legs, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.Boots, GlobalConstants.ItemList["None(Feet)"] },
        };
            var stat1 = new Stats(5, 4, 6, 3, 2, 9, 5);
            var stat2 = new Stats(15, 4, 10, 3, 8, 19, 3);
            var stat3 = new Stats(3, 15, 5, 2, 4, 3, 3);
            var stat4 = new Stats(12, 7, 6, 4, 4, 4, 5);
            var stat5 = new Stats(3, 3, 15, 5, 3, 4, 2);
            var stat6 = new Stats(16, 20, 45, 20, 22, 15, 12);
            Characters.Add(new Character(1, stat1, 1, "Saint Lancelot", 1, 75, equipment));
            Characters.Add(new Character(1, stat2, 1, "Sir Charles", 3, 450, equipment1));
            Characters.Add(new Character(1, stat3, 1, "Devil Raider", 2, 275, equipment2));
            Characters.Add(new Character(1, stat4, 1, "King Arthur", 4, 900, equipment3));
            Characters.Add(new Character(1, stat5, 1, "Angel", 4, 975, equipment4));
            Characters.Add(new Character(1, stat6, 1, "Arnold", 25, 30000, equipment5));
            foreach (var character in Characters)
            {
                character.AlteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
                character.AlteredStats = GetBonusStats(character);
                var newButton = Instantiate(SampleButton) as GameObject;
                var tempButton = newButton.GetComponent<SampleButton>();
                tempButton.NameLabel.text = character.CharacterName;
                tempButton.Character = character;
                newButton.transform.SetParent(ContentPanel);

            }
        }
        public Stats GetBonusStats(Character character)
        {
            foreach (var equipment in character.Equipment.Values.Cast<Equipment>())
            {
                character.AlteredStats = character.AlteredStats.ConcatStats(character.AlteredStats, equipment.Stats);
            }
            return character.AlteredStats;
        }
    }
}
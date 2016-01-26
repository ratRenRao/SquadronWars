﻿using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine;

namespace Assets.Scripts
{
    public class CreateUnitList : MonoBehaviour
    {
        public GameObject sampleButton;
        public List<Character> characters;
        public Transform contentPanel;
        // Use this for initialization
        void Start()
        {
            populateList();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void populateList()
        {
            characters = new List<Character>();
            Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["Cloth Helm"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["Cloth Shoulders"] },
            {ItemType.CHEST, GlobalConstants.ItemList["Cloth Chest"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["Cloth Gloves"] },
            {ItemType.LEGS, GlobalConstants.ItemList["Cloth Legs"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["Cloth Boots"] },
        };
            Dictionary<ItemType, Item> equipment1 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["Leather Helm"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["Leather Shoulders"] },
            {ItemType.CHEST, GlobalConstants.ItemList["Leather Chest"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["Leather Gloves"] },
            {ItemType.LEGS, GlobalConstants.ItemList["Leather Legs"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["Leather Boots"] },
        };
            Dictionary<ItemType, Item> equipment2 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["None(Feet)"] },
        };
            Dictionary<ItemType, Item> equipment3 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["None(Feet)"] },
        };
            Dictionary<ItemType, Item> equipment4 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["None(Feet)"] },
        };
            Dictionary<ItemType, Item> equipment5 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.ItemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.ItemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["None(Feet)"] },
        };
            Stats stat1 = new Stats(5, 4, 6, 3, 2, 9, 5);
            Stats stat2 = new Stats(15, 4, 10, 3, 8, 19, 3);
            Stats stat3 = new Stats(3, 15, 5, 2, 4, 3, 3);
            Stats stat4 = new Stats(12, 7, 6, 4, 4, 4, 5);
            Stats stat5 = new Stats(3, 3, 15, 5, 3, 4, 2);
            Stats stat6 = new Stats(16, 20, 45, 20, 22, 15, 12);
            characters.Add(new Character(1, stat1, 1, "Saint Lancelot", 1, 75, equipment));
            characters.Add(new Character(1, stat2, 1, "Sir Charles", 3, 450, equipment1));
            characters.Add(new Character(1, stat3, 1, "Devil Raider", 2, 275, equipment2));
            characters.Add(new Character(1, stat4, 1, "King Arthur", 4, 900, equipment3));
            characters.Add(new Character(1, stat5, 1, "Angel", 4, 975, equipment4));
            characters.Add(new Character(1, stat6, 1, "Arnold", 25, 30000, equipment5));
            foreach (Character character in characters)
            {
                character.statPoints = 10;
                character.skillPoints = 1;
                character.alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
                character.alteredStats = GetBonusStats(character);
                GameObject newButton = Instantiate(sampleButton) as GameObject;
                SampleButton tempButton = newButton.GetComponent<SampleButton>();
                tempButton.nameLabel.text = character.characterName;
                tempButton.character = character;
                newButton.transform.SetParent(contentPanel, false);

            }
        }
        public Stats GetBonusStats(Character character)
        {
            foreach (Equipment equipment in character.equipment.Values)
            {
                character.alteredStats = character.alteredStats.concatStats(character.alteredStats, equipment.stats);
            }
            return character.alteredStats;
        }
    }
}
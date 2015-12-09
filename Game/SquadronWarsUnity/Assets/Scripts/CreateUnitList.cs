<<<<<<< .mine
﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
||||||| .r93
﻿using UnityEngine;
using SquadronWars2;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
=======
﻿using UnityEngine;
using SquadronWars;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
>>>>>>> .r96
using System.Collections.Generic;

<<<<<<< .mine
namespace SquadronWars2
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
||||||| .r93
public class CreateUnitList : MonoBehaviour {

    public GameObject sampleButton;
    public List<Character> characters;
    public Transform contentPanel;
	// Use this for initialization
	void Start () {
        populateList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void populateList()
    {
        characters = new List<Character> ();
        Stats newStat = new Stats(5, 4, 6, 3, 2, 9, 5);
        newStat.level = 1;
        newStat.experience = 75;
        characters.Add(new Character(1, newStat, 1, "Saint Lancelot", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Sir Charles", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Devil Raider", 1, 0));
        characters.Add(new Character(1, newStat, 1, "King Arthur", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Angel", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Arnold", 1, 0));
        foreach (Character character in characters)
        {
            GameObject newButton = Instantiate(sampleButton) as GameObject;
            SampleButton tempButton = newButton.GetComponent<SampleButton>();
            tempButton.nameLabel.text = character.name;
            tempButton.character = character;
            newButton.transform.SetParent(contentPanel);

        }
=======
public class CreateUnitList : MonoBehaviour {

    public GameObject sampleButton;
    public List<Character> characters;
    public Transform contentPanel;
	// Use this for initialization
	void Start () {
        populateList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void populateList()
    {
        characters = new List<Character> ();
        Stats newStat = new Stats(5, 4, 6, 3, 2, 9, 5);
        newStat.level = 1;
        newStat.experience = 75;
        characters.Add(new Character(1, newStat, 1, "Saint Lancelot", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Sir Charles", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Devil Raider", 1, 0));
        characters.Add(new Character(1, newStat, 1, "King Arthur", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Angel", 1, 0));
        characters.Add(new Character(1, newStat, 1, "Arnold", 1, 0));
        foreach (Character character in characters)
        {
            GameObject newButton = Instantiate(sampleButton) as GameObject;
            SampleButton tempButton = newButton.GetComponent<SampleButton>();
            tempButton.nameLabel.text = character.name;
            tempButton.character = character;
            newButton.transform.SetParent(contentPanel);

        }
>>>>>>> .r96

        void populateList()
        {
            characters = new List<Character>();
            Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.itemList["Cloth Helm"] },
            {ItemType.SHOULDERS, GlobalConstants.itemList["Cloth Shoulders"] },
            {ItemType.CHEST, GlobalConstants.itemList["Cloth Chest"] },
            {ItemType.GLOVES, GlobalConstants.itemList["Cloth Gloves"] },
            {ItemType.LEGS, GlobalConstants.itemList["Cloth Legs"] },
            {ItemType.BOOTS, GlobalConstants.itemList["Cloth Boots"] },
        };
            Dictionary<ItemType, Item> equipment1 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.itemList["Leather Helm"] },
            {ItemType.SHOULDERS, GlobalConstants.itemList["Leather Shoulders"] },
            {ItemType.CHEST, GlobalConstants.itemList["Leather Chest"] },
            {ItemType.GLOVES, GlobalConstants.itemList["Leather Gloves"] },
            {ItemType.LEGS, GlobalConstants.itemList["Leather Legs"] },
            {ItemType.BOOTS, GlobalConstants.itemList["Leather Boots"] },
        };
            Dictionary<ItemType, Item> equipment2 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.itemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.itemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.itemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.itemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.itemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.itemList["None(Feet)"] },
        };
            Dictionary<ItemType, Item> equipment3 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.itemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.itemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.itemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.itemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.itemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.itemList["None(Feet)"] },
        };
            Dictionary<ItemType, Item> equipment4 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.itemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.itemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.itemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.itemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.itemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.itemList["None(Feet)"] },
        };
            Dictionary<ItemType, Item> equipment5 = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.itemList["None(Head)"] },
            {ItemType.SHOULDERS, GlobalConstants.itemList["None(Shoulder)"] },
            {ItemType.CHEST, GlobalConstants.itemList["None(Chest)"] },
            {ItemType.GLOVES, GlobalConstants.itemList["None(Hands)"] },
            {ItemType.LEGS, GlobalConstants.itemList["None(Legs)"] },
            {ItemType.BOOTS, GlobalConstants.itemList["None(Feet)"] },
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
                character.alteredStats = new Stats(0,0,0,0,0,0,0);
                GameObject newButton = Instantiate(sampleButton) as GameObject;
                SampleButton tempButton = newButton.GetComponent<SampleButton>();
                tempButton.nameLabel.text = character.characterName;
                tempButton.character = character;
                newButton.transform.SetParent(contentPanel);

            }
        }
    }
}
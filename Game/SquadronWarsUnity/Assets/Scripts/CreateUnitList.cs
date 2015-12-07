﻿using UnityEngine;
using SquadronWars;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

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
        characters.Add(new Character(1, new Stats(5, 4, 6, 3, 2, 9, 5), 1, "Saint Lancelot", 1, 0));
        characters.Add(new Character(1, new Stats(5, 4, 6, 3, 2, 9, 5), 1, "Sir Charles", 1, 0));
        characters.Add(new Character(1, new Stats(5, 4, 6, 3, 2, 9, 5), 1, "Devil Raider", 1, 0));
        characters.Add(new Character(1, new Stats(5, 4, 6, 3, 2, 9, 5), 1, "King Arthur", 1, 0));
        characters.Add(new Character(1, new Stats(5, 4, 6, 3, 2, 9, 5), 1, "Angel", 1, 0));
        characters.Add(new Character(1, new Stats(5, 4, 6, 3, 2, 9, 5), 1, "Arnold", 1, 0));
        foreach (Character character in characters)
        {
            GameObject newButton = Instantiate(sampleButton) as GameObject;
            SampleButton tempButton = newButton.GetComponent<SampleButton>();
            tempButton.nameLabel.text = character.name;
            tempButton.character = character;
            newButton.transform.SetParent(contentPanel);

        }
    }
}

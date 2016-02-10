﻿using UnityEngine;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {

        public int menuID = 0;
        public GameObject[] menuPanels;
        public GameObject loginPanel;
        public GameObject mainMenuPanel;
        public GameObject registerPanel;
        public GameObject homePanel;
        public GameObject squadPanel;
        public GameObject squadScreenPanel;
        public GameObject characterList;
        public GameObject characterScreenPanel;
        public GameObject shopPanel;
        public GameObject shop;
        // Use this for initialization
        void Start()
        {
            menuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");

            /*mainMenuPanel = GameObject.Find("HomeScreen");
        loginPanel = GameObject.Find("LoginScreen");
        homePanel = GameObject.Find("HomePanel");
        squadPanel = GameObject.Find("SquadPanel");
        squadScreenPanel = GameObject.Find("SquadScreen");
        characterScreenPanel = GameObject.Find("CharacterScreen");
        shop = GameObject.Find("Shop");
        shopPanel = GameObject.Find("ShopPanel");
        characterList = GameObject.Find("CharacterList");*/
            setup();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void setup()
        {
            //loginPanel.SetActive(false);
            mainMenuPanel.SetActive(false);
            //homePanel.SetActive(false);
            squadPanel.SetActive(false);
            characterScreenPanel.SetActive(false);
            shopPanel.SetActive(false);
            registerPanel.SetActive(false);
        }

   
    }
}
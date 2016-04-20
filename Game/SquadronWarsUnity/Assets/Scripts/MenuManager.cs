using UnityEngine;
using Assets.GameClasses;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {

        public int MenuId = 0;
        public GameObject[] MenuPanels;
        public GameObject LoginPanel;
        public GameObject MainMenuPanel;
        public GameObject RegisterPanel;
        public GameObject HomePanel;
        public GameObject SquadPanel;
        public GameObject SquadScreenPanel;
        public GameObject CharacterList;
        public GameObject CharacterScreenPanel;
        public GameObject ShopPanel;
        public GameObject Shop;
        public GameObject CreateCharacterPanel;

        // Use this for initialization
        void Start()
        {
            
            MenuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");
            HomePanel = GameObject.FindGameObjectWithTag("HomePanel");

            /*mainMenuPanel = GameObject.Find("HomeScreen");
            loginPanel = GameObject.Find("LoginScreen");
            homePanel = GameObject.Find("HomePanel");
            squadPanel = GameObject.Find("SquadPanel");
            squadScreenPanel = GameObject.Find("SquadScreen");
            characterScreenPanel = GameObject.Find("CharacterScreen");
            shop = GameObject.Find("Shop");
            shopPanel = GameObject.Find("ShopPanel");
            characterList = GameObject.Find("CharacterList");*/

            Setup();
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Setup()
        {
            //LoginPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            //HomePanel.SetActive(false);
            SquadPanel.SetActive(false);
            CharacterScreenPanel.SetActive(false);
            ShopPanel.SetActive(false);
            RegisterPanel.SetActive(false);
            CreateCharacterPanel.SetActive(false);
            if (GlobalConstants.IsLoggedIn)
            {
                GlobalConstants.ResetCharacters();
                GlobalConstants.EndGame();
                foreach(Character character in GlobalConstants.Player.Characters)
                {
                    GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateCharacterUrl, new UpdateCharacterPostObject(null, character));
                }
                MainMenuPanel.gameObject.SetActive(true);
            }
        }

   
    }
}
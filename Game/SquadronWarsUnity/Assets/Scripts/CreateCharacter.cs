using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.GameClasses;
using Assets.Data;
namespace Assets.Scripts
{
    public class CreateCharacter : MonoBehaviour
    {
        public Text spriteId;
        public InputField characterName;
        public GameObject HomePanel, SquadPanel, ShopPanel, CharacterScreenPanel, CreateCharacterPanel;
        private static DbConnection _dbConnection;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateNewCharacter()
        {
            //Debug.Log(GlobalConstants.Player);
            //Debug.Log(characterName.text);
            //Debug.Log(spriteId.text);

            SetDbConnection();

            int spriteid = int.Parse(spriteId.text);

            CreateCharacterPostObject create = new CreateCharacterPostObject(GlobalConstants.Player.logins, spriteid, characterName.text);

           // Debug.Log(create);

            var www = _dbConnection.SendPostData(GlobalConstants.CreateCharacterUrl, create);
           // Debug.Log(www);

            if(!www.text.Equals("Failed"))
            {
                //Debug.Log(www.text);
                LoadCharacterScreen();
            }           
        }

        public void LoadCreateCharacterScreen()
        {
            HomePanel.SetActive(false);
            SquadPanel.SetActive(false);
            ShopPanel.SetActive(false);
            CharacterScreenPanel.SetActive(false);
            CreateCharacterPanel.SetActive(true);
        }

        public void LoadCharacterScreen()
        {
            HomePanel.SetActive(false);
            SquadPanel.SetActive(false);
            ShopPanel.SetActive(false);
            CreateCharacterPanel.SetActive(false);
            CharacterScreenPanel.SetActive(true);
        }

        private void SetDbConnection()
        {
            gameObject.AddComponent<DbConnection>();
            _dbConnection = gameObject.GetComponent<DbConnection>();
        }
    }
    
}

public class CreateCharacterPostObject
{
    public string username { get; set; }
    public string password { get; set; }
    public int spriteId { get; set; }
    public string charactername { get; set; }

    public CreateCharacterPostObject(Player.Logins p, int sprite, string character)
    {
        username = p.username;
        password = p.password;
        spriteId = sprite;
        charactername = character;
    }
}

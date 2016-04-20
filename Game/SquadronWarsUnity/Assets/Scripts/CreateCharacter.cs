using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.GameClasses;
using Assets.Data;
using System.Linq;
using System;
using System.Reflection;

namespace Assets.Scripts
{
    public class CreateCharacter : MonoBehaviour
    {
        public Text spriteId;
        public int id;
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

        public void SetCharacterId()
        {
            spriteId.text = id.ToString();
        }

        public void CreateNewCharacter()
        {
            //Debug.Log(GlobalConstants.Player);
            //Debug.Log(characterName.text);
            //Debug.Log(spriteId.text);

            //SetDbConnection();
            characterName.selectionColor = Color.white;
            if (characterName.text.Equals("") || characterName.text == null)
            {
                characterName.selectionColor = Color.red;
                return;
            }

            int spriteid = int.Parse(spriteId.text);

            CreateCharacterPostObject create = new CreateCharacterPostObject(GlobalConstants.Player.logins, spriteid, characterName.text);

            // Debug.Log(create);

            var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.CreateCharacterUrl, create);
            // Debug.Log(www);

            if (!www.text.Equals("Failed"))
            {
                characterName.text = "";
                var newCharacterList = GlobalConstants.Utilities.BuildObjectFromJsonData<CreatedCharacters>(www.text);
                List<Character> rebuiltList = RebuildCharacterObjects(newCharacterList.Characters);
                GlobalConstants.Player.Characters = rebuiltList;

                //Debug.Log(www.text);
                GlobalConstants.ResetCharacters();
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
            ShopPanel.SetActive(false);
            CreateCharacterPanel.SetActive(false);
            CharacterScreenPanel.SetActive(false);
            SquadPanel.SetActive(true);
        }

        private void SetDbConnection()
        {
            gameObject.AddComponent<DbConnection>();
            _dbConnection = gameObject.GetComponent<DbConnection>();
        }

        public static Stats AddItemStats(List<Item> items, Stats stats)
        {
            Stats newStats = new Stats();
            var itemList = items.Where(item => item != null).Where(item => item.Stats != null).ToList();
            foreach (var item in itemList)
            {
                newStats = item.Stats.ConcatStats(stats, item.Stats);
            }

            return newStats;
        }

        public List<Character> RebuildCharacterObjects(List<StartupData.CharacterData> Characters)
        {
            var tempCharacterData = Characters;
            List<Character> characterList = new List<Character>();

            foreach (var character in tempCharacterData)
            {
                var characterBuilder = new Character();
                foreach (var property in Utilities.GetParameterList(typeof(Character))
                    .Where(param => Utilities.GetParameterList(typeof(StartupData.CharacterData))
                    .Select(x => x.Name).Contains(param.Name)).ToList())
                {
                    characterBuilder.GetType().GetProperty(property.Name).SetValue(characterBuilder, character.GetType().GetProperty(property.Name).GetValue(character, null), null);
                }

                characterBuilder.Equipment = character.BuildEquipment();
                characterBuilder.BaseStats = character.BuildBaseStats();
                characterBuilder.CurrentStats = AddItemStats(characterBuilder.Equipment.GetItemList(), characterBuilder.BaseStats);

                // characterBuilder.BaseStats.AbilityPoints = 3;
                // characterBuilder.BaseStats.SkillPoints = 2;

                //characterBuilder.CurrentStats = character.BuildBaseStats();
                character.CharacterAbilities.ForEach(ability => ability.Name = GlobalConstants.AbilityMasterList.Single(x => ability.AbilityId == x.AbilityId).Name);
                characterBuilder.Abilities = character.CharacterAbilities;

                characterList.Add(characterBuilder);
                //Player.Characters.Add(characterBuilder);
            }

            return characterList;

        }

    }
}


public class CreatedCharacters : IJsonable
{
    public List<StartupData.CharacterData> Characters { get; set; }


    public CreatedCharacters()
    {
        Characters = new List<StartupData.CharacterData>();
    }

    public string GetJsonObjectName()
    {
        return "CreatedCharacters";
    }

    public List<PropertyInfo> GetJsonObjectParameters()
    {
        throw new NotImplementedException();
    }

    public void SetJsonObjectParameters(Dictionary<string, object> parameters)
    {
        throw new NotImplementedException();
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

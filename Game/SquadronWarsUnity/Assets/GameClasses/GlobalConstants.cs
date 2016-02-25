using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Data;
using Assets.Scripts;

namespace Assets.GameClasses
{
    public static class GlobalConstants
    {
        public const string WebServerUrl = "http://squadronwars.ddns.net";
        public const string SquadDbUrl = WebServerUrl + "temp/temp";
        public const string CharacterDbUrl = WebServerUrl + "/api/getchars";
        public const string PlayerDbUrl = WebServerUrl + "/api/auth";
        public const string RegistrationUrl = WebServerUrl + "";

        public const string PlayerJsonObjectName = "PlayerInfo";
        public const string CharacterJsonObjectName = "Characters";
        public const string StartupDataJsonName = "PlayerDetails";

        public static bool CharacterLoadReady = false;

        public static List<CharacterGameObject> MatchCharacters = new List<CharacterGameObject>();
        public static List<AbilityPreReq> AbilityPreReqs { get; set; } 
        public static List<Item> ItemsMasterList { get; set; }
        public static Player Player { get; set; }
        public static List<Ability> AbilityMasterList { get; set; }

        public static Utilities Utilities = new Utilities();

        public static CharacterGameObject ActiveCharacterGameObject { get; set; }
        //public static List<Character> PlayerCharacters { get; set; } 

        /*
		public static Dictionary<string,Item> ItemList = new Dictionary<string,Item>()
        {
            //Helmets
        { "None(Head)",new Item("None(Head)", ItemType.Helm, new Stats(0, 0, 0, 0, 0, 0, 0))},
        { "Cloth Helm",new Item("Cloth Helm", ItemType.Helm, new Stats(0,0,1,0,1,0,0)) },
        { "Leather Helm",new Item("Leather Helm", ItemType.Helm, new Stats(0,1,0,0,0,1,0)) },
        { "Bronze Helm",new Item("Bronze Helm", ItemType.Helm, new Stats(1,0,0,1,0,0,0)) },
        //Chest
        { "None(Chest)",new Item("None(Chest)", ItemType.Chest, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Chest",new Item("Cloth Chest", ItemType.Chest, new Stats(0,1,2,0,1,0,0)) },
        { "Leather Chest",new Item("Leather Chest", ItemType.Chest, new Stats(1,2,0,0,0,1,0)) },
        { "Bronze Chest",new Item("Bronze Chest", ItemType.Chest, new Stats(2,0,0,2,0,0,0)) },
        //Shoulder
        { "None(Shoulder)",new Item("None(Shoulder)", ItemType.Shoulders, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Shoulders",new Item("Cloth Shoulders", ItemType.Shoulders, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Shoulders",new Item("Leather Shoulders", ItemType.Shoulders, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Shoulders",new Item("Bronze Shoulders", ItemType.Shoulders, new Stats(1,0,0,0,0,0,0)) },
        //Gloves
        { "None(Hands)",new Item("None(Hands)", ItemType.Gloves, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Gloves",new Item("Cloth Gloves", ItemType.Gloves, new Stats(0,0,2,1,1,0,0)) },
        { "Leather Gloves",new Item("Leather Gloves", ItemType.Gloves, new Stats(0,1,0,0,0,3,0)) },
        { "Bronze Gloves",new Item("Bronze Gloves", ItemType.Gloves, new Stats(2,0,0,1,0,1,0)) },
        //Legs
        { "None(Legs)", new Item("None(Legs)", ItemType.Legs, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Legs",new Item("Cloth Legs", ItemType.Legs, new Stats(0,1,1,0,1,0,0)) },
        { "Leather Legs",new Item("Leather Legs", ItemType.Legs, new Stats(0,2,0,0,1,0,0)) },
        { "Bronze Legs",new Item("Bronze Legs", ItemType.Legs, new Stats(1,1,0,1,0,1,0)) },
        //Boots
        { "None(Feet)",new Item("None(Feet)", ItemType.Boots, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Boots",new Item("Cloth Boots", ItemType.Boots, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Boots",new Item("Leather Boots", ItemType.Boots, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Boots",new Item("Bronze Boots", ItemType.Boots, new Stats(1,0,0,0,0,0,0)) },
        //Accessory
        { "None(Accessory)",new Item("None(Accessory)", ItemType.ACCESSORY, new Stats(0, 0, 0, 0, 0, 0, 0)) }
        };
        inventory.Add(new Item("None", ItemType.HELM, new Stats(0, 0, 0, 0, 0, 0, 0)));
        inventory.Add(new Equipment("Leather Helm", ItemType.HELM, new Stats(1,1,1,0,0,0,0)));
        */

        public static string GetJsonObjectName(string property)
        {
            var name = property;

            switch (property)
            {
                case "player":
                    name = PlayerJsonObjectName;
                    break;
             //   case "squad":
              //      name = SquadJsonObjectName;
              //      break;
                case "characterlist":
                    name = CharacterJsonObjectName;
                    break;
                case "startupdata" :
                case "squad":
                    name = StartupDataJsonName;
                    break;
            }

            return name;
        }
    }

    /*
    public class AbilityPreReqs
    {
        public int AbilityId { get; set; }
        public int PrereqAbilityId { get; set; }
        public int PrereqAbilityLevel { get; set; }
        
        private static Dictionary<int, KeyValuePair<int, int>> _abilityDictionary;

        public AbilityPreReqs()
        {
            _abilityDictionary = new Dictionary<int, KeyValuePair<int, int>>();
        }

        public void Add(int preReqId, int abilityId, int preReqLevel)
        {
            _abilityDictionary.Add(preReqId, new KeyValuePair<int, int>(abilityId, preReqLevel));
        }

        public Dictionary<int, KeyValuePair<int, int>> GetAbilityPreReqsDictionary()
        {
            return _abilityDictionary;
        }

        public int GetLevelPreReqByAbilityId(int id)
        {
            return _abilityDictionary.Single(x => x.Value.Key == id).Value.Value;
        }
    }
    */
}    

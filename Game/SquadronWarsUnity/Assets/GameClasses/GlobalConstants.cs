using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Data;
using Assets.Scripts;
using UnityEngine;

namespace Assets.GameClasses
{
    public static class GlobalConstants
    {
        //Primary Webserver URL
        //public const string WebServerUrl = "http://squadronwars.ddns.net";
        public const string WebServerUrl = "ec2-52-27-154-55.us-west-2.compute.amazonaws.com";
        //Standard Communication URLs
        public const string PlayerDbUrl = WebServerUrl + "/api/auth";
        public const string CreateCharacterUrl = WebServerUrl + "/api/CreateCharacter";
        public const string CreatePlayerUrl = WebServerUrl + "/api/CreatePlayer";
        public const string CreateSquadUrl = WebServerUrl + "/api/CreateSquad"; //not implemented yet
        public const string UpdateCharacterUrl = WebServerUrl + "/api/UpdateCharacter";
        public const string UpdatePlayerUrl = WebServerUrl + "/api/UpdatePlayer";
        public const string UpdateSquadUrl = WebServerUrl + "/api/UpdateSquad"; //not implemented yet
        //Battle Related URLs
        public const string StartGameUrl = WebServerUrl + "/api/StartGame";
        public const string CheckGameStatusUrl = WebServerUrl + "/api/CheckGameInfo";
        public const string PlaceCharacterUrl = WebServerUrl + "/api/PlaceCharacters";
        public const string UpdateGameStatusUrl = WebServerUrl + "/api/UpdateGameInfo";

        public const string PlayerJsonObjectName = "PlayerInfo";
        public const string CharacterJsonObjectName = "Characters";
        public const string StartupDataJsonName = "PlayerDetails";
        public const string ActionsJsonName = "Actions";

        public static bool CharacterLoadReady = false;
        public static List<CharacterGameObject> MatchCharacters = new List<CharacterGameObject>();
        public static List<AbilityPreReq> AbilityPreReqs { get; set; } 
        public static List<Item> ItemsMasterList { get; set; }
        public static Player Player { get; set; }
        public static int PlayerNum { get; set; }
        public static List<Ability> AbilityMasterList { get; set; }
        
        public static readonly List<Type> EffectTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                                from assemblyType in domainAssembly.GetTypes()
                                                where typeof(Action).IsAssignableFrom(assemblyType)
                                                select assemblyType).ToList();

        public static List<Effect> EffectMasterList = new List<Effect>(); 
        public static List<Effect> ActiveEffects = new List<Effect>(); 
        public static Utilities Utilities = new Utilities();

        public static CharacterGameObject ActiveCharacterGameObject { get; set; }

        public static Character curSelectedCharacter { get; set; }

        public static DbConnection _dbConnection = new GameObject().GetComponent<DbConnection>();
        public static GameInfo GameInfo;

        //Battle related constants for managing a game
        public static int GameId = 0;
        public static List<Character> player1Characters = new List<Character>();
        public static List<Character> player2Characters = new List<Character>();
        public static BattleAction currentActions = new BattleAction();
        public static int myPlayerId = 0;
        public static int opponentId = 0;
        public static bool Updated = true;
        public static Dictionary<int, TimeListener> TimeListeners = new Dictionary<int, TimeListener>(); 
        public static ActionAnimator ActionAnimator { get; set; }
        //Game data for calculating payout
        public static int DamageAndHealingDone = 0;
        public static DateTime StartGameTime { get; set; }
        public static DateTime EndGameTime { get; set; }

        public static string GetJsonObjectName<T>(T obj) where T : IJsonable
        {
            return obj.GetJsonObjectName();
        }

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
                case "character1list":
                case "character2list":
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

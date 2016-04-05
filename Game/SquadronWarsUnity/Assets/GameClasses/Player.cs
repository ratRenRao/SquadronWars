using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Data;
using UnityEngine;

//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    public class Player : IJsonable
    {
        //DBConnection dbConnection = new DBConnection();
        
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int LevelId { get; set; }
        public int playerId { get; set; }
        public int gold { get; set; }
        //private DateTime? lastLogin { get; set; }
        //public Squad squad { get; set; }
        public List<Character> Characters { get; set; }
        public List<StartupData.InventoryElement> Inventory { get; set; } 
        public Logins logins = new Logins();
        public readonly bool Updated = false;

        /*
        public Player()
        {
            Initialize(null, null, null, null, null, null);
        }

        public Player(string firstName, string lastName, string email, DateTime? lastLogin,
            int? itemListId, int? squadListId)
        {
            Initialize(firstName, lastName, email, lastLogin, itemListId, squadListId);
        }

        private void Initialize(string firstName, string lastName, string email, DateTime? lastLogin,
            int? itemListId, int? squadListId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.lastLogin = lastLogin;
            this.itemList = itemList;
        }
        */

        void Update()
        {

        }

        public class PlayerDetails
        {
            public List<Character> Characters;

        }

        public class PlayerInfo
        {
            public int playerId;
            public string userName;
            public string firstName;
            public string lastName;
            public string email;
            public DateTime? lastLogin;
            public string SessionID;


        //"lastlogin\":null,\"SessionID\":\"test\"},\"PlayerDetails\":{\"Characters\":[{:null}],\"CharacterAbilities\":[],\"Inventory\":[],\"Equipment\":[{\"itemId\":\"1\",\"name\":\"cloth cap\",\"description\":\"a basic cloth cap\",\"slot\":\"H\",\"requiredLevel\":\"0\",\"requiredStr\":\"0\",\"requiredDex\":\"0\",\"requiredInt\":\"0\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"1\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"1000\",\"name\":\"sturdy cloth shirt\",\"description\":\"a particularly sturdy cloth shirt\",\"slot\":\"C\",\"requiredLevel\":\"0\",\"requiredStr\":\"0\",\"requiredDex\":\"0\",\"requiredInt\":\"0\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"1\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"2000\",\"name\":\"stury cloth pants\",\"description\":\"everyone needs pants\",\"slot\":\"P\",\"requiredLevel\":\"0\",\"requiredStr\":\"0\",\"requiredDex\":\"0\",\"requiredInt\":\"0\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"1\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"3000\",\"name\":\"worn gloves\",\"description\":\"you look like a bum with these on\",\"slot\":\"G\",\"requiredLevel\":\"0\",\"requiredStr\":\"0\",\"requiredDex\":\"0\",\"requiredInt\":\"0\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"1\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"5000\",\"name\":\"worn boots\",\"description\":\"has holes in it\",\"slot\":\"B\",\"requiredLevel\":\"0\",\"requiredStr\":\"0\",\"requiredDex\":\"0\",\"requiredInt\":\"0\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"1\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"4000\",\"name\":\"fluffy shoulder pads\",\"description\":\"used for dresses\",\"slot\":\"S\",\"requiredLevel\":\"0\",\"requiredStr\":\"0\",\"requiredDex\":\"0\",\"requiredInt\":\"0\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"1\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"2\",\"name\":\"leather helmet\",\"description\":\"leather helmet\",\"slot\":\"H\",\"requiredLevel\":\"3\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"5\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"1001\",\"name\":\"leather chest piece\",\"description\":\" leather chest piece\",\"slot\":\"C\",\"requiredLevel\":\"3\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"5\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"2001\",\"name\":\"leather pants\",\"description\":\"leather pants\",\"slot\":\"P\",\"requiredLevel\":\"3\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"5\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"4001\",\"name\":\"leather shoulders\",\"description\":\"leather shoulders\",\"slot\":\"S\",\"requiredLevel\":\"3\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"5\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"3001\",\"name\":\"leather gloves\",\"description\":\"leather gloves\",\"slot\":\"G\",\"requiredLevel\":\"3\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"5\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"5001\",\"name\":\"leather boots\",\"description\":\"leather boots\",\"slot\":\"B\",\"requiredLevel\":\"3\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"3\",\"name\":\"hardened leather helmet\",\"description\":\"hardened leather helm\",\"slot\":\"H\",\"requiredLevel\":\"5\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"1002\",\"name\":\"hardened leather chest piece\",\"description\":\"hardened leather chest piece\",\"slot\":\"C\",\"requiredLevel\":\"5\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"3002\",\"name\":\"hardened leather gloves\",\"description\":\"hardened leather gloves\",\"slot\":\"G\",\"requiredLevel\":\"5\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"2002\",\"name\":\"hardened leather pants\",\"description\":\"hardened leather pants\",\"slot\":\"P\",\"requiredLevel\":\"5\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"4002\",\"name\":\"hardened leather shoulders\",\"description\":\"hardened leather shoulders\",\"slot\":\"S\",\"requiredLevel\":\"5\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"5002\",\"name\":\"hardened leather boots\",\"description\":\"hardened leather boots\",\"slot\":\"B\",\"requiredLevel\":\"5\",\"requiredStr\":\"12\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"8\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"4\",\"name\":\"chain helmet\",\"description\":\"chain helmet\",\"slot\":\"H\",\"requiredLevel\":\"10\",\"requiredStr\":\"20\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"12\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"1003\",\"name\":\"chain mail chest armor\",\"description\":\"chain mail chest armor\",\"slot\":\"C\",\"requiredLevel\":\"10\",\"requiredStr\":\"20\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"12\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"2003\",\"name\":\"chain mail pants\",\"description\":\"chain mail pants\",\"slot\":\"P\",\"requiredLevel\":\"10\",\"requiredStr\":\"20\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"12\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"3003\",\"name\":\"chain gloves\",\"description\":\"chain gloves\",\"slot\":\"G\",\"requiredLevel\":\"10\",\"requiredStr\":\"20\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"12\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"4003\",\"name\":\"chain shoulders\",\"description\":\"chain shoulders\",\"slot\":\"S\",\"requiredLevel\":\"10\",\"requiredStr\":\"20\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"12\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"5003\",\"name\":\"chain boots\",\"description\":\"chain boots\",\"slot\":\"B\",\"requiredLevel\":\"10\",\"requiredStr\":\"20\",\"requiredDex\":\"6\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"12\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"5\",\"name\":\"steel helmet\",\"description\":\"steel helmet\",\"slot\":\"H\",\"requiredLevel\":\"15\",\"requiredStr\":\"30\",\"requiredDex\":\"12\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"20\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"1004\",\"name\":\"steel chest plate\",\"description\":\"steel chest plate\",\"slot\":\"C\",\"requiredLevel\":\"15\",\"requiredStr\":\"30\",\"requiredDex\":\"12\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"20\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"2004\",\"name\":\"steel leg armor\",\"description\":\"steel leg armor \",\"slot\":\"P\",\"requiredLevel\":\"15\",\"requiredStr\":\"30\",\"requiredDex\":\"12\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"20\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"3004\",\"name\":\"steel gauntlets\",\"description\":\"steel gauntlets\",\"slot\":\"G\",\"requiredLevel\":\"15\",\"requiredStr\":\"30\",\"requiredDex\":\"12\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"20\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"4004\",\"name\":\"steel shoulders\",\"description\":\"steel shoulders\",\"slot\":\"S\",\"requiredLevel\":\"15\",\"requiredStr\":\"30\",\"requiredDex\":\"12\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"20\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"},{\"itemId\":\"5004\",\"name\":\"steel boots\",\"description\":\"steel boots\",\"slot\":\"B\",\"requiredLevel\":\"15\",\"requiredStr\":\"30\",\"requiredDex\":\"12\",\"requiredInt\":\"10\",\"str\":\"0\",\"int\":\"0\",\"agi\":\"0\",\"wis\":\"0\",\"vit\":\"0\",\"dex\":\"0\",\"hitPoints\":\"0\",\"dmg\":\"0\",\"abilityPoints\":\"0\",\"speed\":\"0\",\"defense\":\"20\",\"magicDefense\":\"0\",\"magicAttack\":\"0\",\"hitRate\":\"0\",\"critRate\":\"0\",\"dodgeRate\":\"0\"}],\"Squads\":[{\"SquadID\":\"1\",\"playerId\":\"9\",\"Capasity\":\"3\",\"SquadName\":\"Test1\"},{\"SquadID\":\"2\",\"playerId\":\"9\",\"Capasity\":\"5\",\"SquadName\":\"Test2\"}],\"Items\":[{\"itemId\":\"1\",\"consumeId\":null,\"name\":\"cloth cap\",\"description\":\"a basic cloth cap\"},{\"itemId\":\"2\",\"consumeId\":null,\"name\":\"leather helmet\",\"description\":\"leather helmet\"},{\"itemId\":\"3\",\"consumeId\":null,\"name\":\"hardened leather helmet\",\"description\":\"hardened leather helm\"},{\"itemId\":\"4\",\"consumeId\":null,\"name\":\"chain helmet\",\"description\":\"chain helmet\"},{\"itemId\":\"5\",\"consumeId\":null,\"name\":\"steel helmet\",\"description\":\"steel helmet\"},{\"itemId\":\"1000\",\"consumeId\":null,\"name\":\"sturdy cloth shirt\",\"description\":\"a particularly sturdy cloth shirt\"},{\"itemId\":\"1001\",\"consumeId\":null,\"name\":\"leather chest piece\",\"description\":\" leather chest piece\"},{\"itemId\":\"1002\",\"consumeId\":null,\"name\":\"hardened leather chest piece\",\"description\":\"hardened leather chest piece\"},{\"itemId\":\"1003\",\"consumeId\":null,\"name\":\"chain mail chest armor\",\"description\":\"chain mail chest armor\"},{\"itemId\":\"1004\",\"consumeId\":null,\"name\":\"steel chest plate\",\"description\":\"steel chest plate\"},{\"itemId\":\"2000\",\"consumeId\":null,\"name\":\"stury cloth pants\",\"description\":\"everyone needs pants\"},{\"itemId\":\"2001\",\"consumeId\":null,\"name\":\"leather pants\",\"description\":\"leather pants\"},{\"itemId\":\"2002\",\"consumeId\":null,\"name\":\"hardened leather pants\",\"description\":\"hardened leather pants\"},{\"itemId\":\"2003\",\"consumeId\":null,\"name\":\"chain mail pants\",\"description\":\"chain mail pants\"},{\"itemId\":\"2004\",\"consumeId\":null,\"name\":\"steel leg armor\",\"description\":\"steel leg armor \"},{\"itemId\":\"3000\",\"consumeId\":null,\"name\":\"worn gloves\",\"description\":\"you look like a bum with these on\"},{\"itemId\":\"3001\",\"consumeId\":null,\"name\":\"leather gloves\",\"description\":\"leather gloves\"},{\"itemId\":\"3002\",\"consumeId\":null,\"name\":\"hardened leather gloves\",\"description\":\"hardened leather gloves\"},{\"itemId\":\"3003\",\"consumeId\":null,\"name\":\"chain gloves\",\"description\":\"chain gloves\"},{\"itemId\":\"3004\",\"consumeId\":null,\"name\":\"steel gauntlets\",\"description\":\"steel gauntlets\"},{\"itemId\":\"4000\",\"consumeId\":null,\"name\":\"fluffy shoulder pads\",\"description\":\"used for dresses\"},{\"itemId\":\"4001\",\"consumeId\":null,\"name\":\"leather shoulders\",\"description\":\"leather shoulders\"},{\"itemId\":\"4002\",\"consumeId\":null,\"name\":\"hardened leather shoulders\",\"description\":\"hardened leather shoulders\"},{\"itemId\":\"4003\",\"consumeId\":null,\"name\":\"chain shoulders\",\"description\":\"chain shoulders\"},{\"itemId\":\"4004\",\"consumeId\":null,\"name\":\"steel shoulders\",\"description\":\"steel shoulders\"},{\"itemId\":\"5000\",\"consumeId\":null,\"name\":\"worn boots\",\"description\":\"has holes in it\"},{\"itemId\":\"5001\",\"consumeId\":null,\"name\":\"leather boots\",\"description\":\"leather boots\"},{\"itemId\":\"5002\",\"consumeId\":null,\"name\":\"hardened leather boots\",\"description\":\"hardened leather boots\"},{\"itemId\":\"5003\",\"consumeId\":null,\"name\":\"chain boots\",\"description\":\"chain boots\"},{\"itemId\":\"5004\",\"consumeId\":null,\"name\":\"steel boots\",\"description\":\"steel boots\"}]}}"
        }

        public class Logins
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        /*public async Task UpdatePlayerFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl);
            Player dbPlayer = dbConnection.DeserializeData<Player>(this);

            this.firstName = dbPlayer.firstName;
            this.lastName = dbPlayer.lastName;
            this.email = dbPlayer.email;
            this.lastLogin = dbPlayer.lastLogin;
            this.squad = dbPlayer.squad;
            this.itemList = dbPlayer.itemList;
        }*/

        public static void BuildFromJson(JSONObject jsonObject)
        {
            
        }

        public string GetJsonObjectName()
        {
            return "PlayerInfo";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        /*
        public class Inventory
        {
            // Only contains data for the last added element.  Needs to be public for parameter reflection
            public int ItemId { get; set; }
            public int Quantity { get; set; }

            private Dictionary<int, int> _inventoryDictionary;

            public Inventory()
            {
                _inventoryDictionary = new Dictionary<int, int>();
            }

            public void Add(KeyValuePair<int, int> inventoryPair)
            {
                _inventoryDictionary.Add(inventoryPair.Key, inventoryPair.Value);
            }

            public Dictionary<int, int> GetInventoryData()
            {
                return _inventoryDictionary;
            }

            public int? GetItemQuantity(int itemId)
            {
                return _inventoryDictionary.Single(x => x.Key == itemId).Value;
            }
        }
        */
    }
}

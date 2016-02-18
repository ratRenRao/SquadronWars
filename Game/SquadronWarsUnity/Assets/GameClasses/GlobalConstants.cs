using System;
using System.Collections.Generic;

namespace Assets.GameClasses
{
    public static class GlobalConstants
    {
        public const string WebServerUrl = "http://squadronwars.ddns.net";
        public const string SquadDbUrl = WebServerUrl + "temp/temp";
        public const string CharacterDbUrl = WebServerUrl + "/api/getchars";
        public const string PlayerDbUrl = WebServerUrl + "/api/auth";
        public static List<CharacterGameObject> matchCharacters = new List<CharacterGameObject>();
        public const string PlayerJsonObjectName = "PlayerInfo";
        public const string CharacterJsonObjectName = "Characters";
        public const string StartupDataJsonName = "PlayerDetails";
		
		public static Dictionary<string,Item> ItemList = new Dictionary<string,Item>()
        {
            //Helmets
        { "None(Head)",new Item("None(Head)", ItemType.HELM, new Stats(0, 0, 0, 0, 0, 0, 0))},
        { "Cloth Helm",new Item("Cloth Helm", ItemType.HELM, new Stats(0,0,1,0,1,0,0)) },
        { "Leather Helm",new Item("Leather Helm", ItemType.HELM, new Stats(0,1,0,0,0,1,0)) },
        { "Bronze Helm",new Item("Bronze Helm", ItemType.HELM, new Stats(1,0,0,1,0,0,0)) },
        //Chest
        { "None(Chest)",new Item("None(Chest)", ItemType.CHEST, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Chest",new Item("Cloth Chest", ItemType.CHEST, new Stats(0,1,2,0,1,0,0)) },
        { "Leather Chest",new Item("Leather Chest", ItemType.CHEST, new Stats(1,2,0,0,0,1,0)) },
        { "Bronze Chest",new Item("Bronze Chest", ItemType.CHEST, new Stats(2,0,0,2,0,0,0)) },
        //Shoulder
        { "None(Shoulder)",new Item("None(Shoulder)", ItemType.SHOULDERS, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Shoulders",new Item("Cloth Shoulders", ItemType.SHOULDERS, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Shoulders",new Item("Leather Shoulders", ItemType.SHOULDERS, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Shoulders",new Item("Bronze Shoulders", ItemType.SHOULDERS, new Stats(1,0,0,0,0,0,0)) },
        //Gloves
        { "None(Hands)",new Item("None(Hands)", ItemType.GLOVES, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Gloves",new Item("Cloth Gloves", ItemType.GLOVES, new Stats(0,0,2,1,1,0,0)) },
        { "Leather Gloves",new Item("Leather Gloves", ItemType.GLOVES, new Stats(0,1,0,0,0,3,0)) },
        { "Bronze Gloves",new Item("Bronze Gloves", ItemType.GLOVES, new Stats(2,0,0,1,0,1,0)) },
        //Legs
        { "None(Legs)", new Item("None(Legs)", ItemType.LEGS, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Legs",new Item("Cloth Legs", ItemType.LEGS, new Stats(0,1,1,0,1,0,0)) },
        { "Leather Legs",new Item("Leather Legs", ItemType.LEGS, new Stats(0,2,0,0,1,0,0)) },
        { "Bronze Legs",new Item("Bronze Legs", ItemType.LEGS, new Stats(1,1,0,1,0,1,0)) },
        //Boots
        { "None(Feet)",new Item("None(Feet)", ItemType.BOOTS, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Boots",new Item("Cloth Boots", ItemType.BOOTS, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Boots",new Item("Leather Boots", ItemType.BOOTS, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Boots",new Item("Bronze Boots", ItemType.BOOTS, new Stats(1,0,0,0,0,0,0)) },
        //Accessory
        { "None(Accessory)",new Item("None(Accessory)", ItemType.ACCESSORY, new Stats(0, 0, 0, 0, 0, 0, 0)) }
        };
        //inventory.Add(new Item("None", ItemType.HELM, new Stats(0, 0, 0, 0, 0, 0, 0)));
        //inventory.Add(new Equipment("Leather Helm", ItemType.HELM, new Stats(1,1,1,0,0,0,0)));

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
}    

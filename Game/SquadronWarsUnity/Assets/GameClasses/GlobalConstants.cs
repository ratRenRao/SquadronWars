using System.Collections.Generic;

namespace SquadronWars2
{
    public static class GlobalConstants
    {
        public const string ServerUrl = "https://ec2-user@ec2-52-27-154-55.us-west-2.compute.amazonaws.com";

        public const string SquadDbUrl = "temp/temp";
        public const string SquadKeyName = "squadid";

        public const string CharacterDbUrl = "/api/getchars";
        public const string CharacterKeyName = "characterid";

        public const string PlayerDbUrl = "/api/auth";
        public const string PlayerKeyName = "playerid";
		
		public static Dictionary<string,Item> ItemList = new Dictionary<string,Item>()
        {
            //Helmets
        { "None(Head)",new Equipment("None(Head)", ItemType.Helm, new Stats(0, 0, 0, 0, 0, 0, 0))},
        { "Cloth Helm",new Equipment("Cloth Helm", ItemType.Helm, new Stats(0,0,1,0,1,0,0)) },
        { "Leather Helm",new Equipment("Leather Helm", ItemType.Helm, new Stats(0,1,0,0,0,1,0)) },
        { "Bronze Helm",new Equipment("Bronze Helm", ItemType.Helm, new Stats(1,0,0,1,0,0,0)) },
        //Chest
        { "None(Chest)",new Equipment("None(Chest)", ItemType.Chest, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Chest",new Equipment("Cloth Chest", ItemType.Chest, new Stats(0,1,2,0,1,0,0)) },
        { "Leather Chest",new Equipment("Leather Chest", ItemType.Chest, new Stats(1,2,0,0,0,1,0)) },
        { "Bronze Chest",new Equipment("Bronze Chest", ItemType.Chest, new Stats(2,0,0,2,0,0,0)) },
        //Shoulder
        { "None(Shoulder)",new Equipment("None(Shoulder)", ItemType.Shoulders, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Shoulders",new Equipment("Cloth Shoulders", ItemType.Shoulders, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Shoulders",new Equipment("Leather Shoulders", ItemType.Shoulders, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Shoulders",new Equipment("Bronze Shoulders", ItemType.Shoulders, new Stats(1,0,0,0,0,0,0)) },
        //Gloves
        { "None(Hands)",new Equipment("None(Hands)", ItemType.Gloves, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Gloves",new Equipment("Cloth Gloves", ItemType.Gloves, new Stats(0,0,2,1,1,0,0)) },
        { "Leather Gloves",new Equipment("Leather Gloves", ItemType.Gloves, new Stats(0,1,0,0,0,3,0)) },
        { "Bronze Gloves",new Equipment("Bronze Gloves", ItemType.Gloves, new Stats(2,0,0,1,0,1,0)) },
        //Legs
        { "None(Legs)", new Equipment("None(Legs)", ItemType.Legs, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Legs",new Equipment("Cloth Legs", ItemType.Legs, new Stats(0,1,1,0,1,0,0)) },
        { "Leather Legs",new Equipment("Leather Legs", ItemType.Legs, new Stats(0,2,0,0,1,0,0)) },
        { "Bronze Legs",new Equipment("Bronze Legs", ItemType.Legs, new Stats(1,1,0,1,0,1,0)) },
        //Boots
        { "None(Feet)",new Equipment("None(Feet)", ItemType.Boots, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Boots",new Equipment("Cloth Boots", ItemType.Boots, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Boots",new Equipment("Leather Boots", ItemType.Boots, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Boots",new Equipment("Bronze Boots", ItemType.Boots, new Stats(1,0,0,0,0,0,0)) },
        };
        //inventory.Add(new Equipment("None", ItemType.HELM, new Stats(0, 0, 0, 0, 0, 0, 0)));
        //inventory.Add(new Equipment("Leather Helm", ItemType.HELM, new Stats(1,1,1,0,0,0,0)));
    }
}    

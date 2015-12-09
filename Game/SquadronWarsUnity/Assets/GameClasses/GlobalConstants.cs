using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars2
{
    static public class GlobalConstants
    {
		public static string squadDbUrl = "temp/temp";
        public static string characterDbUrl = "/api/getchars";
        public static string playerDbUrl = "/api/auth"
		
		 static public Dictionary<string,Item> itemList = new Dictionary<string,Item>()
        {
            //Helmets
        { "None(Head)",new Equipment("None(Head)", ItemType.HELM, new Stats(0, 0, 0, 0, 0, 0, 0))},
        { "Cloth Helm",new Equipment("Cloth Helm", ItemType.HELM, new Stats(0,0,1,0,1,0,0)) },
        { "Leather Helm",new Equipment("Leather Helm", ItemType.HELM, new Stats(0,1,0,0,0,1,0)) },
        { "Bronze Helm",new Equipment("Bronze Helm", ItemType.HELM, new Stats(1,0,0,1,0,0,0)) },
        //Chest
        { "None(Chest)",new Equipment("None(Chest)", ItemType.CHEST, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Chest",new Equipment("Cloth Chest", ItemType.CHEST, new Stats(0,1,2,0,1,0,0)) },
        { "Leather Chest",new Equipment("Leather Chest", ItemType.CHEST, new Stats(1,2,0,0,0,1,0)) },
        { "Bronze Chest",new Equipment("Bronze Chest", ItemType.CHEST, new Stats(2,0,0,2,0,0,0)) },
        //Shoulder
        { "None(Shoulder)",new Equipment("None(Shoulder)", ItemType.SHOULDERS, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Shoulders",new Equipment("Cloth Shoulders", ItemType.SHOULDERS, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Shoulders",new Equipment("Leather Shoulders", ItemType.SHOULDERS, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Shoulders",new Equipment("Bronze Shoulders", ItemType.SHOULDERS, new Stats(1,0,0,0,0,0,0)) },
        //Gloves
        { "None(Hands)",new Equipment("None(Hands)", ItemType.GLOVES, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Gloves",new Equipment("Cloth Gloves", ItemType.GLOVES, new Stats(0,0,2,1,1,0,0)) },
        { "Leather Gloves",new Equipment("Leather Gloves", ItemType.GLOVES, new Stats(0,1,0,0,0,3,0)) },
        { "Bronze Gloves",new Equipment("Bronze Gloves", ItemType.GLOVES, new Stats(2,0,0,1,0,1,0)) },
        //Legs
        { "None(Legs)", new Equipment("None(Legs)", ItemType.LEGS, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Legs",new Equipment("Cloth Legs", ItemType.LEGS, new Stats(0,1,1,0,1,0,0)) },
        { "Leather Legs",new Equipment("Leather Legs", ItemType.LEGS, new Stats(0,2,0,0,1,0,0)) },
        { "Bronze Legs",new Equipment("Bronze Legs", ItemType.LEGS, new Stats(1,1,0,1,0,1,0)) },
        //Boots
        { "None(Feet)",new Equipment("None(Feet)", ItemType.BOOTS, new Stats(0, 0, 0, 0, 0, 0, 0)) },
        { "Cloth Boots",new Equipment("Cloth Boots", ItemType.BOOTS, new Stats(0,0,1,0,0,0,0)) },
        { "Leather Boots",new Equipment("Leather Boots", ItemType.BOOTS, new Stats(0,1,0,0,0,0,0)) },
        { "Bronze Boots",new Equipment("Bronze Boots", ItemType.BOOTS, new Stats(1,0,0,0,0,0,0)) },
        };
        //inventory.Add(new Equipment("None", ItemType.HELM, new Stats(0, 0, 0, 0, 0, 0, 0)));
        //inventory.Add(new Equipment("Leather Helm", ItemType.HELM, new Stats(1,1,1,0,0,0,0)));
    }
}    }
}
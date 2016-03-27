using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.GameClasses;


public class BattlePostObject
{
    public string username { get; set; }
    public string password { get; set; }
    public int gameId { get; set; }
    public string player1 { get; set; }
    //public List<Character> player1 { get; set; }
    public string player2 { get; set; }
    //public List<Character> player2 { get; set; }
    public string gameInfo { get; set; }
    //public BattleAction gameJSON { get; set; }
    public int myPlayerId { get; set; }
    
    /**/
    public BattlePostObject()
    {
        username = GlobalConstants.Player.logins.username;
        password = GlobalConstants.Player.logins.password;
        gameId = GlobalConstants.GameId;
        player1 = GetPlayerCharacters(GlobalConstants.player1Characters, 1);
        player2 = GetPlayerCharacters(GlobalConstants.player2Characters, 2);
        gameInfo = GlobalConstants.currentActions.GetJSONString();
        myPlayerId = GlobalConstants.myPlayerId;            
    }
    /**

    public BattlePostObject()
    {
        username = GlobalConstants.Player.logins.username;
        password = GlobalConstants.Player.logins.password;
        gameId = GlobalConstants.GameId;
        player1 = GlobalConstants.Player.Characters;
        player2 = GlobalConstants.Player.Characters;
        gameJSON = GlobalConstants.currentActions;
        myPlayerId = GlobalConstants.myPlayerId;
    }
    /**/
    private string GetPlayerCharacters(List<Character> playercharacters, int characterId)
    {
        string returnString = "player\", \"player" + characterId + "characters\" : [ ";
        int index = 0;
        foreach(Character character in playercharacters)
        {
            if (index != 0)
            {
                returnString += ", ";
            }
            returnString += character.GetJSONString();
            index++;
        }
        returnString += "], \"end\" : \"end";

        return returnString;
     }

     // Use this for initialization
     void Start() {

     }

     // Update is called once per frame
     void Update() {

     }
}
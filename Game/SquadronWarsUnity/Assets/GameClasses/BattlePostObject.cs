using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.GameClasses;


public class BattlePostObject
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int GameId { get; set; }
    public int Finished { get; set; }
    public string Player1 { get; set; }
    public string Player2 { get; set; }
    public string GameInfo { get; set; }
    public int MyPlayerId { get; set; }
    
    /**/
    public BattlePostObject()
    {
        Username = GlobalConstants.Player.login.Username;
        Password = GlobalConstants.Player.login.Password;
        GameId = GlobalConstants.GameId;
        Finished = 0;
        Player1 = GetPlayerCharacters(GlobalConstants.player1Characters, 1);
        Player2 = GetPlayerCharacters(GlobalConstants.player2Characters, 2);
        GameInfo = GlobalConstants.currentActions.GetJSONString();
        MyPlayerId = GlobalConstants.myPlayerId;            
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
        var returnString = "player\", \"player" + characterId + "characters\" : [ ";
        var index = 0;
        foreach(var character in playercharacters)
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
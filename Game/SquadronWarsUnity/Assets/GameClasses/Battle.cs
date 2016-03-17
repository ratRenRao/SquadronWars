using UnityEngine;
using System.Collections;
using Assets.GameClasses;

public class Battle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        GlobalConstants.GameId = 0;
        GlobalConstants.myPlayerId = 0;
        GlobalConstants.player1Characters.Clear();
        GlobalConstants.player2Characters.Clear();
        GlobalConstants.currentActions.ResetBattleActions();
        var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.StartGameUrl, new BattlePostObject());
        UpdateGameInfo(www);
    }

    public void CheckGame()
    {
        var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.CheckGameStatusUrl, new BattlePostObject());
        UpdateGameInfo(www);                
    }

    public void PlaceCharacters()
    {
        var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.PlaceCharacterUrl, new BattlePostObject());
        UpdateGameInfo(www);
    }

    public void EndGame()
    {
        GlobalConstants.GameId = 0;
        GlobalConstants.myPlayerId = 0;
        GlobalConstants.player1Characters.Clear();
        GlobalConstants.player2Characters.Clear();
        GlobalConstants.currentActions.ResetBattleActions();
        //Call additional functions for end game.
    }

    public void UpdateGameInfo(WWW www)
    {
        //parse www for JSON information and update all the global constants from it.
        /*
        GlobalConstants.GameId
        GlobalConstants.myPlayerId
        GlobalConstants.player1Characters
        GlobalConstants.player2Characters
        GlobalConstants.currentActions
        */
    }
}

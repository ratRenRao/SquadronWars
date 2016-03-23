using System;
using UnityEngine;
using System.Collections;
using Assets.Data;
using Assets.GameClasses;

public class Battle : MonoBehaviour
{
    private DateTime lastChecked, lastModified;

	// Use this for initialization
	void Start ()
	{
	    lastChecked = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if ((DateTime.Now - lastChecked).TotalSeconds >= 4)
	    {
	        if (GlobalConstants.GameId == null)
	        {
	            StartCoroutine(StartGameCoroutine());
                
	        }
	        else
	        {
	            StartCoroutine(UpdateGameCoroutine());
	        }
	    }
	}

    public IEnumerator StartGameCoroutine()
    {
        var gameInfo = GlobalConstants.Utilities.GetGameInfo(GlobalConstants.StartGameUrl);
        StartGame();
        GlobalConstants.Updated = true;
        yield return UpdateGame(gameInfo);
    }

    public IEnumerator UpdateGameCoroutine()
    {
        var gameInfo = GlobalConstants.Utilities.GetGameInfo();

        if (!lastModified.Equals(gameInfo.ModifyTime))
        {
                yield return UpdateGame(gameInfo);
                lastModified = gameInfo.ModifyTime;
                // Used to determine if changes have been made to data
                GlobalConstants.Updated = true;
        }
    }

    public GameInfo UpdateGame(GameInfo gameInfo)
    {
        GlobalConstants.Utilities.SetGlobalDataFromGameInfo(gameInfo);
        return gameInfo;
        // Add methods to do things like moving characters, taking damage, etc. 
    }

    public void StartGame()
    {
        GlobalConstants.GameId = 0;
        GlobalConstants.myPlayerId = 0;
        GlobalConstants.player1Characters.Clear();
        GlobalConstants.player2Characters.Clear();
        GlobalConstants.currentActions.ResetBattleActions();
        //var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.StartGameUrl, new BattlePostObject());
        //UpdateGameInfo(www);
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

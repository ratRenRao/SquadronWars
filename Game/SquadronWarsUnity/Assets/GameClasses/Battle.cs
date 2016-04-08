using System;
using UnityEngine;
using System.Collections;
using Assets.Data;
using Assets.GameClasses;

public class Battle : MonoBehaviour
{
    private DateTime lastChecked, lastModified;
    private bool checkUpdate = true;

	// Use this for initialization
	void Start ()
	{
        //StartGameCoroutine();
	    lastChecked = DateTime.Now;
	    //running = true;
	}
	
	// Update is called once per frame
	void Update ()
	{        
        if (!GlobalConstants.isMyTurn && !GlobalConstants.isAnimating && checkUpdate && (DateTime.Now - lastChecked).TotalSeconds > 1f)
        {            
            checkUpdate = false;
            lastChecked = DateTime.Now;
            //Debug.Log("checkUpdate called");
            StartCoroutine(BattleWaitForLoad());
        } 
	}

    public void StartGameCoroutine()
    {
        //StartCoroutine(GlobalConstants.Utilities.GetGameInfo(GlobalConstants.StartGameUrl));
        var gameInfo = GlobalConstants.Utilities.GetGameInfo(GlobalConstants.StartGameUrl);

        //var gameInfo = GlobalConstants.GameInfo;
        if (gameInfo != null)
        {
            StartGame();
            UpdateGame(gameInfo);
        }
    }

    public void UpdateGameCoroutine()
    {
        //StartCoroutine(GlobalConstants.Utilities.GetGameInfo());
        var gameInfo = GlobalConstants.Utilities.GetGameInfo();
        //Debug.Log("Update Game Coroutine called");
        //var gameInfo = GlobalConstants.GameInfo;
        if (gameInfo != null && !lastModified.Equals(gameInfo.ModifyTime))
        {
                UpdateGame(gameInfo);
                lastModified = gameInfo.ModifyTime;
                // Used to determine if changes have been made to data
                GlobalConstants.Updated = true;
                //checkUpdate = true;
        }
        checkUpdate = true;
    }

    public void UpdateGame(GameInfo gameInfo)
    {
        GlobalConstants.Utilities.SetGlobalDataFromGameInfo(gameInfo);
        // Add methods to do things like moving characters, taking damage, etc. 
    }

    public void StartGame()
    {
        GlobalConstants.GameId = 0;
        GlobalConstants.myPlayerId = 0;
        GlobalConstants.opponentId = 0;
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
        GlobalConstants.opponentId = 0;
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

    /*IEnumerator WaitForLoad()
    {
        Debug.Log("WaitUpdate called");
        
        yield return new WaitForSeconds(.5f);
        checkUpdate = true;
    }*/

    IEnumerator BattleWaitForLoad()
    {
        //Debug.Log("WaitUpdate called");
        yield return new WaitForSeconds(0);
        UpdateGameCoroutine();
        
        
    }
}

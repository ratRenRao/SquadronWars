﻿using UnityEngine;
using System.Collections;
using Assets.Data;
using Assets.GameClasses;
using UnityEngine.SceneManagement;

public class StartQueue : MonoBehaviour
{

    public GameObject homeScreen;
    public GameObject gameScreen;
    public GameObject queueScreen;
    private DbConnection _dbConnection = new DbConnection();
    public bool waitForLoading = true;
    public bool setQueueScreen = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	

    public void StartFindingMatch()
    {
        //homeScreen.SetActive(false);
        queueScreen.SetActive(true);
        //homeScreen.SetActive(false);
        WaitForGameInfoReturned();
        //StartCoroutine(WaitForOpponent());
        WaitForOpponent();
        //StartCoroutine("FindPlayer");

    }

    void Update()
    {
        /*if (setQueueScreen)
        {
            setQueueScreen = false;
        }*/

    }

    public IEnumerator WaitForOpponent()
    {
        while(!CheckForMatchedPlayer())
        {
            WaitOneSecond();
            GetGameStatus();

        }
        SceneManager.LoadScene("BattleMap2");
        yield return true;
    }


    public bool CheckForMatchedPlayer()
    {
        if(GlobalConstants.opponentId != 0)
        {
            return true;
        }
        return false;
    }

    /**/
    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
    }
    /**/

    public void ButtonClickCancel()
    {
        homeScreen.SetActive(true);
        queueScreen.SetActive(false);
        
    }


    public void WaitForGameInfoReturned()
    {
        //StartCoroutine(GlobalConstants.Utilities.GetGameInfo(GlobalConstants.StartGameUrl));
        var gameInfo = GlobalConstants.Utilities.GetGameInfo(GlobalConstants.StartGameUrl, GlobalConstants._dbConnection);

        //var gameInfo = GlobalConstants.GameInfo;
        if (gameInfo != null)
        {
            GlobalConstants.Utilities.UpdateGame(gameInfo);
            GlobalConstants.Updated = true;
        }
    }

    public void GetGameStatus()
    {
        var gameInfo = GlobalConstants.Utilities.GetGameInfo(GlobalConstants.CheckGameStatusUrl, GlobalConstants._dbConnection);

        //var gameInfo = GlobalConstants.GameInfo;
        if (gameInfo != null)
        {
            GlobalConstants.Utilities.UpdateGame(gameInfo);
            GlobalConstants.Updated = true;
        }
    }
}

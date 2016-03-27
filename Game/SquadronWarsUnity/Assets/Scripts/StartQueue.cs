using UnityEngine;
using System.Collections;
using Assets.Data;
using Assets.GameClasses;
using UnityEngine.SceneManagement;

public class StartQueue : MonoBehaviour {

    public GameObject gameScreen;
    public GameObject queueScreen;
    private DbConnection _dbConnection = new DbConnection();
    public bool waitForLoading = true;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	

    public void StartFindingMatch()
    {
        //homeScreen.SetActive(false);
        queueScreen.SetActive(true);
        WaitForGameInfoReturned();
        WaitForOpponent();      
        //StartCoroutine("FindPlayer");
        
    }

    void Update()
    {
        
    }

    public void WaitForOpponent()
    {
        while(!CheckForMatchedPlayer())
        {
            WaitOneSecond();
            GetGameStatus();
        }
        SceneManager.LoadScene("BattleMap2");
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

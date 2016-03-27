using UnityEngine;
using System.Collections;
using Assets.Data;
using Assets.GameClasses;
using UnityEngine.SceneManagement;

public class StartQueue : MonoBehaviour {

    public GameObject gameScreen;
    public GameObject queueScreen;
    private DbConnection _dbConnection = new DbConnection();
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        	
	}

    public void StartFindingMatch()
    {
        //homeScreen.SetActive(false);
        queueScreen.SetActive(true);
        WaitForGameInfoReturned();
        SceneManager.LoadScene("BattleMap2");
        //StartCoroutine("FindPlayer");
        
    }

    public bool CheckForMatchedPlayer()
    {
        if(GlobalConstants.myPlayerId == 1)
        {
            if(GlobalConstants.player2Characters.Count > 0)
            {
                return true;
            }
            return false;
        }
        if(GlobalConstants.player1Characters.Count > 0)
        {
            return true;
        }
        return false;
    }

    /*
    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("BattleMap2");
    }
    */

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
}

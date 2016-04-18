using System;
using System.Collections;
using Assets.GameClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class StartQueue : MonoBehaviour
    {

        public GameObject homeScreen;
        public GameObject gameScreen;
        public GameObject queueScreen;
        public bool waitForLoading = true;
        public bool setQueueScreen = false;
        private static bool queued = true;

        // Use this for initialization
        void Start ()
        {
        }
	
        // Update is called once per frame
        public void StartFindingMatch()
        {
            queueScreen.SetActive(true);
            WaitForGameInfoReturned();
            StartCoroutine(ShowQueueScreenWaitForMatch());

        }

        void Update()
        {
        }

        IEnumerator ShowQueueScreenWaitForMatch()
        {
            while(!CheckForMatchedPlayer() && queued)
            {
                yield return new WaitForSeconds(2f);
                GetGameStatus();
            }
            if (CheckForMatchedPlayer())
                SceneManager.LoadScene("BattleMap1");
        }

        public bool CheckForMatchedPlayer()
        {
            if(GlobalConstants.opponentId != 0)
            {
                queued = false;
                return true;
            }
            return false;
        }

        IEnumerator WaitForTwoSeconds()
        {
            yield return new WaitForSeconds(2f);
        }

        public void ButtonClickCancel()
        {
            queued = false;
            StopCoroutine(ShowQueueScreenWaitForMatch());
            homeScreen.SetActive(true);
            queueScreen.SetActive(false);
        }

        public void WaitForGameInfoReturned()
        {
            var gameInfo = GlobalConstants.Utilities.GetGameInfo(GlobalConstants.StartGameUrl, GlobalConstants._dbConnection);
            if (gameInfo != null)
            {
                Debug.Log("WaitForGameInfo not null Called");
                GlobalConstants.Utilities.UpdateGame(gameInfo);
                GlobalConstants.Updated = true;
                GlobalConstants.StartGameTime = DateTime.Now;
            }
        }

        public void GetGameStatus()
        {
            var gameInfo = GlobalConstants.Utilities.GetGameInfo(GlobalConstants.CheckGameStatusUrl, GlobalConstants._dbConnection);
            if (gameInfo != null)
            {
                GlobalConstants.Utilities.UpdateGame(gameInfo);
                GlobalConstants.Updated = true;
            }
        }
    }
}

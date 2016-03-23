using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartQueue : MonoBehaviour {

    public GameObject homeScreen;
    public GameObject queueScreen;
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
        StartCoroutine("FindPlayer");
        
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("BattleMap2");
    }
}

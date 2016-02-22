using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartQueue : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartFindingMatch()
    {
        SceneManager.LoadScene("BattleMapTest2");
    }
}

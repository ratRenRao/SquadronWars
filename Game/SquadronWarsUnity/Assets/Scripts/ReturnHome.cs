using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReturnToStart()
    {
        SceneManager.LoadScene("Login");
    }
}

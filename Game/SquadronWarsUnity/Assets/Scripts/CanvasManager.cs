using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour
{
    public static GameObject LoginScreen, MenuScreen; 
	// Use this for initialization
	void Start () {
	    LoginScreen = GameObject.Find("LoginScreen");
	    MenuScreen = GameObject.Find("MenuScreen");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

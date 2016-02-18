using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour
{
    public static GameObject LoginScreen, HomeScreen; 
	// Use this for initialization
	void Start () {
	    LoginScreen = GameObject.Find("LoginScreen");
	    HomeScreen = GameObject.Find("HomeScreen");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

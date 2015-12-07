using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

    public int menuID = 0;
    public GameObject[] menuPanels;
    public GameObject loginPanel;
    public GameObject mainMenuPanel;
    public GameObject homePanel;
    public GameObject squadPanel;
    public GameObject squadScreenPanel;
    public GameObject characterScreenPanel;
    public GameObject shopPanel;
    public GameObject shop;
    // Use this for initialization
    void Start()
    {
        menuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");

        mainMenuPanel = GameObject.Find("HomeScreen");
        loginPanel = GameObject.Find("LoginScreen");
        homePanel = GameObject.Find("HomePanel");
        squadPanel = GameObject.Find("SquadPanel");
        squadScreenPanel = GameObject.Find("SquadScreen");
        characterScreenPanel = GameObject.Find("CharacterScreen");
        shop = GameObject.Find("Shop");
        shopPanel = GameObject.Find("ShopPanel");
        setup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setup()
    {
        //loginPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        //homePanel.SetActive(false);
        squadPanel.SetActive(false);
        characterScreenPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

   
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Data;
using System.Collections.Generic;
using Assets.GameClasses;

public class RegisterButton : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public InputField confirmPass;
    public InputField lastName;
    public InputField firstName;
    public InputField email;

    private static DbConnection _dbConnection;
    public GameObject HomeScreen, RegisterScreen;
    private StartupData _startupData { get; set; }
    private Player _player { get; set; }
    private Registration _register { get; set; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterUser()
    {
        SetDbConnection();
        if (!password.text.Equals(confirmPass.text))
        {
            var cb = password.colors;
            cb.normalColor = Color.red;
            password.colors = cb;
            confirmPass.colors = cb;
            return;
        }

        username.colors = email.colors;
        password.colors = username.colors;
        confirmPass.colors = password.colors;

        _register = new Registration(username.text, password.text, lastName.text, firstName.text, email.text);
        var www = _dbConnection.SendPostData(GlobalConstants.CreatePlayerUrl, _register);

        if (www.text.Equals("Failed"))
        {
            var cb = username.colors;
            cb.normalColor = Color.red;
            username.colors = cb;
            return;
        }

        GetDbData();

        StartupData.BuildAndDistributeData();

        RegisterScreen.gameObject.SetActive(false);
        HomeScreen.gameObject.SetActive(true);

    }

    private void SetDbConnection()
    {
        gameObject.AddComponent<DbConnection>();
        _dbConnection = gameObject.GetComponent<DbConnection>();
    }

    private void GetDbData()
    {
        _startupData = _dbConnection.PopulateObjectFromDb<StartupData>(GlobalConstants.PlayerDbUrl, _register);
    }
}

public class Registration
{
    public string username { get; set; }
    public string password { get; set; }
    public string lastName { get; set; }
    public string firstName { get; set; }
    public string email { get; set; }

    public Registration(string user, string pass, string last, string first, string email)
    {
        this.username = user;
        this.password = pass;
        this.lastName = last;
        this.firstName = first;
        this.email = email;
    }
}
using UnityEngine;
using System.Collections;
using Assets.Data;
using Assets.GameClasses;

public class RegisterButton : MonoBehaviour
{
    public RegistrationData registrationData;
    private DbConnection _dbConnection = new DbConnection();

	void Start () {
	    registrationData = new RegistrationData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        registrationData.username = "newbie";
        registrationData.password = "secret";
        registrationData.confirmPassword = "secret";
        registrationData.email = "newbie@newbie.com";
        registrationData.firstName = "Newbie";
        registrationData.lastName = "McNewberson";

        _dbConnection.SendPostData(GlobalConstants.RegistrationUrl, registrationData);
    }
}

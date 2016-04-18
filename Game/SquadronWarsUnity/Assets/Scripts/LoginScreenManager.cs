using Assets.Data;
using Assets.GameClasses;
using UnityEngine;

namespace Assets.Scripts
{
    public class LoginScreenManager : MonoBehaviour
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private static DbConnection _dbConnection;
        private StartupData _startupData {get; set; }
        private Player _player { get; set; }
        private static Player.Logins _logins = new Player.Logins();
        public GameObject HomeScreen, LoginScreen;
        // Use this for initialization
        void Start()
        {
            //if (GlobalConstants.Player != null)
            //{
            //not working correctly yet.
            //LoginScreen.gameObject.SetActive(false);
            HomeScreen.SetActive(true);
            //}
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateLogins()
        {

        }

        public void LoginClicked()
        {
            SetDbConnection();
            SetLoginInfo();
            GetDbData();

            if (_startupData == null)
            {
                // Change to display error message
                Debug.Log("Invalid Credentials");
                return;
            }
            
            StartupData.BuildAndDistributeData();

            _player = GlobalConstants.Player;

            GlobalConstants.Player.logins = _logins;

            //LoginScreen.gameObject.SetActive(false);
            HomeScreen.gameObject.SetActive(true);
            GlobalConstants.IsLoggedIn = true;
            //CanvasManager.HomeScreen.SetActive(true);
            //CanvasManager.LoginScreen.SetActive(false);
            //MenuManager.GetComponent<MenuManager>().HomePanel.SetActive(true);
            //MenuManager.GetComponent<MenuManager>().LoginPanel.SetActive(false);

#if DEBUG
            new Tests().Run();
#endif
        }

        private void GetDbData()
        {
            _startupData = _dbConnection.PopulateObjectFromDb<StartupData>(GlobalConstants.PlayerDbUrl, _logins);
        }

        private void SetLoginInfo()
        {
#if DEBUG
            if (Username == "" && Password == "")
            {
                _logins.username = "test";
                _logins.password = "testing123";
            }
            else
            {
                _logins.username = Username;
                _logins.password = Password;
            }
#else
            _logins.username = Username;
            _logins.password = Password;
#endif
        }

        private void SetDbConnection()
        {
            gameObject.AddComponent<DbConnection>();
            _dbConnection = gameObject.GetComponent<DbConnection>();
            //GlobalConstants._dbConnection = gameObject.GetComponent<DbConnection>();
            GlobalConstants._dbConnection = new GameObject("DbConnection").AddComponent<DbConnection>();
            DontDestroyOnLoad(GlobalConstants._dbConnection);
            DontDestroyOnLoad(GlobalConstants.Utilities);
        }
    }
}

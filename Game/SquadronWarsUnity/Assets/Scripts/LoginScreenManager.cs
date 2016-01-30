using Assets.Data;
using Assets.GameClasses;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts
{
    public class LoginScreenManager : MonoBehaviour
    {

        public string Username { get; set; }
        public string Password { get; set; }
        private static DbConnection _dbConnection;
        private static Player _player;
        private static Player.Logins _logins = new Player.Logins();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateLogins()
        {

        }

        public bool ValidateLogins()
        {
            SetDbConnection();
            SetLoginInfo();
            
            _player = (Player) _dbConnection.PopulateObjectFromDb<Player>(GlobalConstants.PlayerDbUrl, _logins);
            Debug.Log(_player.ToString());

            // Change to only continue if player object is populated
            if (true)
            {
                CanvasManager.LoginScreen.SetActive(false);
                CanvasManager.MenuScreen.SetActive(true);
            }

            return false;
        }

        private static void SetLoginInfo()
        {
#if DEBUG
            _logins.username = "test";
            _logins.password = "testing123";
#else
            _logins.username = Username;
            _logins.password = Password;
#endif
        }

        private void SetDbConnection()
        {
            gameObject.AddComponent<DbConnection>();
            _dbConnection = gameObject.GetComponent<DbConnection>();
        }
    }
}

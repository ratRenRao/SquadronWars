using Assets.Data;
using Assets.GameClasses;
using UnityEngine;

namespace Assets.Scripts
{
    public class LoginScreenManager : MonoBehaviour
    {

        public string Username { get; set; }
        public string Password { get; set; }
        private static DBConnection _dbConnection;
        private static Player _player = new Player();

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
            // 
            Debug.Log(Username);
            Debug.Log(Password);
        }

        public bool ValidateLogins()
        {
            SetDbConnection();
            SetLoginInfo();
            
            _player = _dbConnection.PopulateObjectFromDb<Player>(GlobalConstants.PlayerDbUrl, _player);
            return false;
        }

        private void SetLoginInfo()
        {
            _player.username = Username;
            _player.password = Password;
        }

        private void SetDbConnection()
        {
            gameObject.AddComponent<DBConnection>();
            _dbConnection = gameObject.GetComponent<DBConnection>();
        }
    }
}

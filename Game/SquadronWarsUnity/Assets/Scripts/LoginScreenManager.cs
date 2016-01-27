using Assets.GameClasses;
using UnityEngine;
using DbConnection = Assets.Data.DbConnection;

namespace Assets.Scripts
{
    public class LoginScreenManager : MonoBehaviour
    {

        public string Username { get; set; }
        public string Password { get; set; }
        private static DbConnection _dbConnection;
        private Player _player;

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
            _player = _dbConnection.PopulateObjectFromDb<Player>(GlobalConstants.PlayerDbUrl);
            return false;
        }

        private void SetDbConnection()
        {
            gameObject.AddComponent<DbConnection>();
            _dbConnection = gameObject.GetComponent<DbConnection>();
        }
    }
}

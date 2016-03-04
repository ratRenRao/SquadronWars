using UnityEngine;

namespace Assets.Scripts
{
    class LoginButton : MonoBehaviour
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        private LoginScreenManager LoginScreenManager { get; set; }

        public void Start()
        {
            LoginScreenManager = GetComponentInParent<LoginScreenManager>();
        }

        public void OnClick()
        {
            LoginScreenManager.LoginClicked();
        }
    }
}

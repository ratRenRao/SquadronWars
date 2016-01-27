using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SetLoginPassword : MonoBehaviour {

        private LoginScreenManager Manager { get; set; }

        void Start()
        {
            Manager = GetComponentInParent<LoginScreenManager>();
        }

        public void Update()
        {
            Manager.Password = GetComponent<InputField>().text;
        }
    }
}

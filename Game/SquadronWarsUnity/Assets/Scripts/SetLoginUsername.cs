using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SetLoginUsername : MonoBehaviour
    {
        private LoginScreenManager Manager { get; set; }

        void Start()
        {
            Manager = GetComponentInParent<LoginScreenManager>();
        }

        public void Update()
        {
            Manager.Username = GetComponent<InputField>().text;
        }
    }
}

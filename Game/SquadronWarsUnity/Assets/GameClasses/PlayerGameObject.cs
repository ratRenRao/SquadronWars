using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameClasses
{
    class PlayerGameObject : MonoBehaviour
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime? lastLogin { get; set; }
        public Squad squad { get; set; }
        public Player.Logins logins { get; set; }
        public readonly Player playerClassObject;

        void Update()
        {
            if (playerClassObject == null)
                return;

            if (playerClassObject.Updated)
                UpdatePlayerGameObject();
        }

        private void UpdatePlayerGameObject()
        {

        }
    }
}

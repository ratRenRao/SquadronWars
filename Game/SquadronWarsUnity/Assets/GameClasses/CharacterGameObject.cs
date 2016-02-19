using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;

namespace Assets.GameClasses
{
    public class CharacterGameObject : MonoBehaviour
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool hasAttacked = false;
        public bool hasMoved = false;

        public Character CharacterClassObject { get; set; }

        public CharacterGameObject(Character characterClassObject = null, int x = 0, int y = 0)
        {
            CharacterClassObject = characterClassObject;
            X = x;
            Y = y;
        }

        // Updating shouldnt be necessary with ref instantiation
        void Update()
        {
            /*
            if (CharacterClassObject == null)
                return;

            if (CharacterClassObject.Updated)
                UpdateCharacterGameObject();
           */
        }

        public void SetValues(int x, int y, Character character = null) 
        {
            if (character != null)
                CharacterClassObject = character;
            X = x;
            Y = y;
        }

        private void UpdateCharacterGameObject()
        {
            
        }
    }
}

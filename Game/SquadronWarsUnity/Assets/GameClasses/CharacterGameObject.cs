using System;
using System.Collections.Generic;
using System.Linq;
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

        public readonly Character CharacterClassObject;

        public CharacterGameObject(ref Character characterClassObject, int x, int y)
        {
            CharacterClassObject = characterClassObject;
            X = x;
            Y = y;
        }

        // Updating shouldnt be necessary with ref instantiation
        void Update()
        {
            if (CharacterClassObject == null)
                return;

            if (CharacterClassObject.Updated)
                UpdateCharacterGameObject();
        }

        private void UpdateCharacterGameObject()
        {
            
        }
    }
}

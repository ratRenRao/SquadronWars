using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assets.Data;
using UnityEngine;

namespace Assets.GameClasses
{
    public class CharacterGameObject : MonoBehaviour
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool hasAttacked = false;
        public bool hasMoved = false;
        public bool isDead = false;
        public bool isAlly = false;

        private int _characterId { get; set; }

        public Character CharacterClassObject { get; set; }

        public void Initialize(Character characterClassObject, int x = 0, int y = 0)
        {
            CharacterClassObject = characterClassObject;
            X = x;
            Y = y;
        }

        public void SetCharacterId(int id)
        {
            _characterId = id;
            CharacterClassObject =
                GlobalConstants.Player.Characters.Single(character => character.CharacterId == _characterId);
        }

        public int GetCharacterId()
        {
            return _characterId;
        }

        void Update()
        {

        }

        public void SetValues(int x, int y, Character character = null) 
        {
            if (character != null)
                CharacterClassObject = character;
            X = x;
            Y = y;
        }

        public CharacterGameObject cloneCharacter(CharacterGameObject c)
        {
            var character = new CharacterGameObject
            {
                X = c.X,
                Y = c.Y,
                _characterId = c._characterId,
                CharacterClassObject = c.CharacterClassObject,
                hasAttacked = c.hasAttacked,
                hasMoved = c.hasMoved,
                isDead = c.isDead
            };
            return character;
        }
        private void UpdateCharacterGameObject()
        {
            
        }
    }
}

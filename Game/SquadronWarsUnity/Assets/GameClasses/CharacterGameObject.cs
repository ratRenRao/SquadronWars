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
        public int curHP { get; set; }
        public int curMP { get; set; }
        public bool isDead { get; set; }

        private int _characterId { get; set; }

        public Character CharacterClassObject { get; set; }

        public void Initialize(Character characterClassObject, int x = 0, int y = 0)
        {
            CharacterClassObject = characterClassObject;
            curHP = characterClassObject.CurrentStats.HitPoints;
            curMP = characterClassObject.CurrentStats.MagicPoints;

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

        private void UpdateCharacterGameObject()
        {
            
        }
    }
}

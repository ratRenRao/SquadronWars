using UnityEngine;
using System.Collections;
using Assets.GameClasses;

namespace Assets.Scripts
{
    public class GameCharacter : MonoBehaviour
    {

        public CharacterGameObject character;
        public bool hasAttacked = false;
        public bool hasMoved = false;
        public int x
        {
            get;
            set;
        }
        public int y
        {
            get;
            set;
        }

        public GameCharacter(CharacterGameObject character, int x, int y)
        {
            this.character = character;
            this.x = x;
            this.y = y;
        }

        void Start()
        {
            character.alteredStats.InitializeStats(character.level);
            character.alteredStats.currentHP = character.alteredStats.maxHP;
            character.alteredStats.currentMP = character.alteredStats.maxMP;
        }
    }
}

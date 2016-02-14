using UnityEngine;
using System.Collections;
using Assets.GameClasses;

namespace Assets.Scripts
{
    public class GameCharacter : MonoBehaviour
    {

        public Character character;
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

        public GameCharacter(Character character, int x, int y)
        {
            this.character = character;
            this.x = x;
            this.y = y;
        }

        void Start()
        {
            character.alteredStats.currentHP = character.alteredStats.calculateHP(character.level);
            character.alteredStats.currentMP = character.alteredStats.calculateMP(character.level);
            Debug.Log(character.alteredStats.currentHP);
            Debug.Log(character.alteredStats.currentMP);
        }
    }
}

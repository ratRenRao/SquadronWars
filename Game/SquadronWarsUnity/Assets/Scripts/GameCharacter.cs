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
            character.alteredStats.InitializeStats(character.level);
            character.alteredStats.currentHP = character.alteredStats.maxHP;
            character.alteredStats.currentMP = character.alteredStats.maxMP;
        }
    }
}

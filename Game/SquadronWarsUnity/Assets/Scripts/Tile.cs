using UnityEngine;
using System.Collections;
using Assets.GameClasses;
namespace Assets.Scripts
{
    public class Tile : MonoBehaviour
    {
        public GameObject highlight;
        public GameObject characterObject;
        public CharacterGameObject character;
        //public GameCharacter gameCharacter;
        public int x { get; set; }
        public int y { get; set; }
        public float positionX { get; set; }
        public float positionY { get; set; }
        public bool isValidMove { get; set; }
        public bool isObstructed { get; set; }
        public bool isOccupied { get; set; }

        // This represents the amount of damage taken for any character on this tile.
        public int amount { get; set; }

        public string GetJSONString()
        {
            return "{ \"x\" : \"" + x + "\", \"y\" : \"" + y + "\"}";
        }
    }

    
}

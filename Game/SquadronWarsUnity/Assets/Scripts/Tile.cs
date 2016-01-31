using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Tile : MonoBehaviour
    {

        public int x { get; set; }
        public int y { get; set; }
        public float positionX { get; set; }
        public float positionY { get; set; }
        public bool isValidMove { get; set; }
        public bool isObstructed { get; set; }
        public GameObject highlight;
    }
}

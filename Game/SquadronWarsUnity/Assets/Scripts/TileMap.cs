using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TileMap : MonoBehaviour
    {

        public enum Objective
        {
            Pathfinder,
            Timed,
            Collector
        }

        public List<GameObject> tiles = new List<GameObject>();
        public Tile[,] tileArray { get; set; }
        public int xLength;
        public int yLength;


        // Use this for initialization
        void Start()
        {
            CreateTileArray();            
            //setTileArray();
            //addHighlightObjects();

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CreateTileArray()
        {
            var count = 0;
            foreach (Transform child in transform)
            {
                //if (positionY != child.localPosition.y)
                //{
                //    positionY = child.localPosition.y;
                //    x = 0;
                //    y += 1;
                //}
                var tile = child.gameObject.GetComponent<Tile>();
                var floatX = Mathf.Abs((child.localPosition.x / 3.2f));
                var floatY = Mathf.Abs((child.localPosition.y / 3.2f));

                tile.x = (int)floatX;
                tile.y = (int)floatY;
                tile.isValidMove = false;

                var tagName = child.gameObject.tag;
                if (tagName == "grass" || tagName == "bridge" || tagName == "ground" || tagName == "lava_crack")
                {
                    tile.isOccupied = false;
                    tile.isObstructed = false;
                }
                else
                {
                    tile.isOccupied = true;
                    tile.isObstructed = true;
                }
                //tiles.Add(child.gameObject);
                tiles.Add(child.gameObject);
                count++;
            }
            
        }
        public void setTileArray()
        {
            var count = 0;
            for (var i = 0; i < xLength; i++)
            {
                for (var j = 0; j < yLength; j++)
                {
                    var tempTile = tiles[count].GetComponent<Tile>();
                    while (count < 410)
                    {
                        if (tiles[count].GetComponent<Tile>().x == i && tiles[count].GetComponent<Tile>().y == j) {
                            tempTile = tiles[count].GetComponent<Tile>();
                            break;
                        }
                        count++;
                    }
                    
                    tileArray[i, j] = tiles[count].GetComponent<Tile>();
                    
                }
            }

        }

        public void addHighlightObjects()
        {
            tileArray = new Tile[xLength, yLength];
            
            foreach (var t in tiles)
            {
                
                var temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                var highlight = GameObject.Instantiate(temp, new Vector3(t.transform.position.x + 1.6f, t.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                highlight.transform.SetParent(t.transform, true);
                t.GetComponent<Tile>().highlight = highlight;
                t.GetComponent<Tile>().highlight.SetActive(false);
                highlight.transform.localScale = new Vector3(0.072f, 0.072f, 0.0f);
                tileArray[t.GetComponent<Tile>().x, t.GetComponent<Tile>().y] = t.GetComponent<Tile>();
                //Debug.Log(tileArray[t.GetComponent<Tile>().x, t.GetComponent<Tile>().y]);
            }
            
        }
    }
}

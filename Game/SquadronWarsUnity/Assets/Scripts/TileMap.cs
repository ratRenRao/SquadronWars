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
            addHighlightObjects();

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CreateTileArray()
        {
            int count = 0;
            foreach (Transform child in transform)
            {
                //if (positionY != child.localPosition.y)
                //{
                //    positionY = child.localPosition.y;
                //    x = 0;
                //    y += 1;
                //}
                Tile tile = child.gameObject.GetComponent<Tile>();
                float floatX = Mathf.Abs((child.localPosition.x / 3.2f));
                float floatY = Mathf.Abs((child.localPosition.y / 3.2f));

                tile.x = (int)floatX;
                tile.y = (int)floatY;
                tile.isValidMove = false;
                
                //tile.x = x;
                //tile.y = y;

                //tiles.Add(child.gameObject);
                tiles.Add(child.gameObject);
                count++;
            }
            
            Debug.Log(count);
            
        }
        public void setTileArray()
        {
            int count = 0;
            
            Debug.Log(xLength + ", " + yLength);
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    Tile tempTile = tiles[count].GetComponent<Tile>();
                    tileArray[tempTile.x, tempTile.y] = tiles[count].GetComponent<Tile>();
                    count++;
                }
            }
            
        }

        public void addHighlightObjects()
        {
            tileArray = new Tile[xLength, yLength];
            foreach (GameObject t in tiles)
            {
                
                GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                GameObject highlight = GameObject.Instantiate(temp, new Vector3(t.transform.position.x + 1.6f, t.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                highlight.transform.parent = t.transform;
                t.GetComponent<Tile>().highlight = highlight;
                t.GetComponent<Tile>().highlight.SetActive(false);
                highlight.transform.localScale = new Vector3(0.072f, 0.072f, 0.0f);
                //Debug.Log(t.GetComponent<Tile>().x + ", " + t.GetComponent<Tile>().y);
                tileArray[t.GetComponent<Tile>().x, t.GetComponent<Tile>().y] = t.GetComponent<Tile>();
                //Debug.Log(tileArray[t.GetComponent<Tile>().x, t.GetComponent<Tile>().y]);
            }      
        }
    }
}

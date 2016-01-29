﻿using UnityEngine;
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

        List<GameObject> tiles = new List<GameObject>();
        public Tile[,] tileArray { get; set; }
        public int xLength;
        public int yLength;


        // Use this for initialization
        void Start()
        {
            CreateTileArray();
            xLength = tileArray.GetLength(0);
            yLength = tileArray.GetLength(1);
            setTileArray();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CreateTileArray()
        {
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
                //tile.x = x;
                //tile.y = y;

                //tiles.Add(child.gameObject);
                Debug.Log("Tile X:" + tile.x + ", Tile Y:" + tile.y);
                tiles.Add(child.gameObject);
            }
            tileArray = new Tile[xLength, yLength];
        }
        public void setTileArray()
        {
            int count = 0;
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    Tile tempTile = tiles[count].GetComponent<Tile>();
                    tileArray[tempTile.x, tempTile.y] = tiles[count].GetComponent<Tile>();
                    count++;
                }
            }
            //Debug.Log(tiles.Count);
        }
    }
}

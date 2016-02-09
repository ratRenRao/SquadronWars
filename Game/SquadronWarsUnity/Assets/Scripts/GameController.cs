using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.GameClasses;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public enum Action
        {
            IDLE,
            MOVE
        }

        Vector3 hitDown;
        RaycastHit2D hit;
        Animator anim;
        Tile targetTile;
        public TileMap tileMap;
        Tile tile = null;
        Tile prevTile = null;
        Tile lastTile;
        Tile[,] tileArray;
        Dictionary<string, int> inventory;
        public GameCharacter currentCharacter;
        Character character;
        Action action = Action.IDLE;
        bool isWalking;
        bool isCharacter;
        bool reachedPosition = true;
        bool lifeLost = false;
        int count = 0;
        List<Tile> validMoves = new List<Tile>();
        List<Tile> path = new List<Tile>();


        // Use this for initialization
        void Start()
        {
            Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>
        {
            {ItemType.HELM, GlobalConstants.ItemList["Cloth Helm"] },
            {ItemType.SHOULDERS, GlobalConstants.ItemList["Cloth Shoulders"] },
            {ItemType.CHEST, GlobalConstants.ItemList["Cloth Chest"] },
            {ItemType.GLOVES, GlobalConstants.ItemList["Cloth Gloves"] },
            {ItemType.LEGS, GlobalConstants.ItemList["Cloth Legs"] },
            {ItemType.BOOTS, GlobalConstants.ItemList["Cloth Boots"] },
        };
            //screen = ScreenOrientation.Landscape;
            Stats stat1 = new Stats(5, 4, 6, 3, 2, 9, 5);
            //character = new Character(1, stat1, 1, "Saint Lancelot", 1, 75, equipment);
            character.alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character.alteredStats = GetBonusStats(character);
            character.alteredStats.speed = 4;
            targetTile = GameObject.FindGameObjectWithTag("start").GetComponent<Tile>();
            currentCharacter.x = 0;
            currentCharacter.y = 0;
            prevTile = targetTile.GetComponent<Tile>();
            tile = prevTile;
           // string obj = this.name;
            anim = GetComponent<Animator>();
            
        }

        public Stats GetBonusStats(Character character)
        {
            /*
            foreach (Equipment equipment in character.equipment.Values)
            {
                character.alteredStats = character.alteredStats.concatStats(character.alteredStats, equipment.stats);
            }
            */
            return character.alteredStats;
            
        }

        void Update()
        {
            tileArray = tileMap.tileArray;
            if (reachedPosition == false && count < path.Count)
            {
                    isWalking = true;
                    anim.SetBool("isWalking", isWalking);
                    float currentX = (float)(System.Math.Round(transform.localPosition.x, 2));
                    float currentY = (float)(System.Math.Round(transform.localPosition.y, 2));
                    float targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
                    float targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
                  //  Transform targetLocation = targetTile.transform;
                    if (currentX - targetX > 0)
                    {
                        anim.SetFloat("x", -1);
                        anim.SetFloat("y", 0);
                        transform.position += new Vector3(-0.1f, 0);
                    }
                    if (currentX - targetX < 0)
                    {
                        anim.SetFloat("x", 1);
                        anim.SetFloat("y", 0);

                        transform.position += new Vector3(0.1f, 0);
                    }
                    if (currentY - targetY > 0)
                    {
                        anim.SetFloat("x", 0);
                        anim.SetFloat("y", -1);


                        transform.position += new Vector3(0, -0.1f);
                    }

                    if (currentY - targetY < 0)
                    {
                        anim.SetFloat("x", 0);
                        anim.SetFloat("y", 1);

                        transform.position += new Vector3(0, 0.1f);
                    }

                    if (currentY - targetY == 0 && currentX - targetX == 0)
                    {
                    Debug.Log((count + 1) + " " + path.Count);
                    if ((count + 1) == path.Count)
                    {

                        isWalking = false;
                        anim.SetBool("isWalking", isWalking);
                        reachedPosition = true;
                        path.Clear();
                        Debug.Log(targetTile.x + "," + targetTile.y);
                        count = 0;
                        action = Action.IDLE;
                    }
                    else {
                        count++;
                        targetTile = path[count];
                    }
                    }
            }
            else
            {

                if (Input.GetMouseButtonUp(0) && reachedPosition && action == Action.MOVE)
                {

                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (action == Action.MOVE)
                    {
                        if (hit.collider != null)
                        {
                            lastTile = targetTile;
                            targetTile = hit.collider.gameObject.GetComponent<Tile>();
                            Tile tempTile = targetTile;
                            if (tempTile.isValidMove)
                            {
                                tile = tempTile;
                                prevTile = lastTile.GetComponent<Tile>();
                                currentCharacter.x = tile.x;
                                currentCharacter.y = tile.y;
                                path = buildPath(prevTile, tile);
                                Debug.Log(path.Count);
                                targetTile = path[0];
                                reachedPosition = false;                    
                                clearHighlights(validMoves);
                            }
                            else
                            {
                                targetTile = lastTile;
                            }
                        }
                        //Debug.Log("Tile: " + hit.collider.gameObject.name);
                    }
                    else
                    {
                        //Debug.Log("Invaliad hit");
                    }
                }

            }
        }


        public void showMoves()
        {
           // int currentX = currentCharacter.x;
          //  int currentY = currentCharacter.y;
            int tileX = tile.x;
            int tileY = tile.y;
            Tile[,] tileArray = tileMap.tileArray;
            //tileArray[1, 0].isObstructed = true;
            int move = character.alteredStats.speed;
            
            if (action == Action.IDLE)
            {                
                validMoves = new List<Tile>();
                //Create Bottom Move Tiles
                for (int i = 1; i <= character.alteredStats.speed; i++)
                {

                    if (tileY + i < tileArray.GetLength(1))
                    {
                        Debug.Log(tileArray.GetLength(1));
                        Tile tempTile = tileArray[tileX, tileY + i];
                        tempTile.highlight.SetActive(true);
                        tempTile.isValidMove = true;
                        validMoves.Add(tempTile);
                        //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));

                        // GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                        //highlight.transform.parent = tempTile.transform;
                    }
                }
                //Create Top Move Tiles
                for (int i = 1; i <= character.alteredStats.speed; i++)
                {
                    if (tileY - i >= 0)
                    {
                        Tile tempTile = tileArray[tileX, tileY - i];
                        tempTile.highlight.SetActive(true);
                        tempTile.isValidMove = true;
                        validMoves.Add(tempTile);
                        //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                        //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                        //highlight.transform.parent = tempTile.transform;
                    }
                }
                //Create Left Move Tiles
                for (int i = 1; i <= character.alteredStats.speed; i++)
                {
                    if (tileX - i >= 0)
                    {
                        Tile tempTile = tileArray[tileX - i, tileY];
                        tempTile.highlight.SetActive(true);
                        tempTile.isValidMove = true;
                        validMoves.Add(tempTile);
                        //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                        //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                        //highlight.transform.parent = tempTile.transform;
                    }
                }
                //Create Right Move Tiles
                move = character.alteredStats.speed;
                for (int i = 1; i <= move; i++)
                {
                    if (tileX + i < tileArray.GetLength(0))
                    {
                        Tile tempTile = tileArray[tileX + i, tileY];
                        if (tempTile.isObstructed)
                        {
                            move = move - 2;
                        }
                        else { 
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                        }
                        //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                        //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                        //highlight.transform.parent = tempTile.transform;
                    }
                    else
                    {
                        break;
                    }
                }
                //Create Top Left Move Tiles
                move = character.alteredStats.speed;
                for (int i = 1; i < character.alteredStats.speed; i++)
                {
                    for (int j = 1; j < move; j++)
                    {
                        if (tileX - i >= 0 && tileY - j >= 0)
                        {
                            Tile tempTile = tileArray[tileX - i, tileY - j];
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                        else
                        {
                            break;
                        }
                    }
                    move--;
                }
                //Create Top Right Move Tiles
                move = character.alteredStats.speed;
                for (int i = 1; i < character.alteredStats.speed; i++)
                {
                    for (int j = 1; j < move; j++)
                    {
                        if (tileX + i < tileArray.GetLength(0) && tileY - j >= 0)
                        {
                            Tile tempTile = tileArray[tileX + i, tileY - j];
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                        else
                        {
                            break;
                        }
                    }
                    move--;
                }
                //Create Bottom Left Move Tiles
                move = character.alteredStats.speed;
                for (int i = 1; i < character.alteredStats.speed; i++)
                {
                    for (int j = 1; j < move; j++)
                    {
                        if (tileX - i >= 0 && tileY + j < tileArray.GetLength(1))
                        {
                            Tile tempTile = tileArray[tileX - i, tileY + j];
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                        else
                        {
                            break;
                        }
                    }
                    move--;
                }
                //Create Bottom Left Move Tiles
                move = character.alteredStats.speed;
                for (int i = 1; i < character.alteredStats.speed; i++)
                {
                    for (int j = 1; j < move; j++)
                    {
                        if (tileX + i < tileArray.GetLength(0) && tileY + j < tileArray.GetLength(1))
                        {
                            Tile tempTile = tileArray[tileX + i, tileY + j];
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                        else
                        {
                            break;
                        }
                    }
                    move--;
                }
                action = Action.MOVE;
            }
            else
            {                
                clearHighlights(validMoves);
                
            }
        }

        IEnumerator AttackAnimation()
        {
            yield return new WaitForSeconds(.5f);
            Debug.Log("WAITED FOR 3 SECONDS");
            anim.SetBool("isAttacking", false);
        }

        public void clearHighlights(List<Tile> tiles)
        {
            action = Action.IDLE;
            Debug.Log(tiles.Count);
            foreach (Tile tile in tiles)
            {
                tile.highlight.SetActive(false);
                tile.isValidMove = false;
            }
            tiles.Clear();
        }
        public bool checkMove(Tile tile)
        {
            int currentX = currentCharacter.x;
            int currentY = currentCharacter.y;
          //  int tileX = tile.x;
          //  int tileY = tile.y;
            //top left
            if (currentX - 1 == tile.x && currentY - 1 == tile.y)
            {
                return true;
            }
            //top middle
            if (currentX == tile.x && currentY - 1 == tile.y)
            {
                return true;
            }
            //top right
            if (currentX + 1 == tile.x && currentY - 1 == tile.y)
            {
                return true;
            }
            //middle left
            if (currentX - 1 == tile.x && currentY == tile.y)
            {
                return true;
            }
            //middle right
            if (currentX + 1 == tile.x && currentY == tile.y)
            {
                return true;
            }
            //bottom left
            if (currentX - 1 == tile.x && currentY + 1 == tile.y)
            {
                return true;
            }
            //bottom middle
            if (currentX == tile.x && currentY + 1 == tile.y)
            {
                return true;
            }
            //bottom right
            if (currentX + 1 == tile.x && currentY + 1 == tile.y)
            {
                return true;
            }
            return false;
        }

        public List<Tile> buildPath(Tile start, Tile end)
        {
            int currentX = start.x;
            int currentY = start.y;
            int endX = end.x;
            int endY = end.y;
            List<Tile> movePath = new List<Tile>();
            for (int i = 0; i < 4; i++)
            {
                
                if(currentX == endX && currentY == endY)
                {
                    break;
                }
                //Path to right
                else if (currentX < endX && tileArray[currentX + 1, currentY].isValidMove)
                {
                    Tile tempTile = tileArray[currentX + 1, currentY];
                    movePath.Add(tempTile);
                    Debug.Log("X:" + (currentX + 1) + "Y:" + currentY);                    
                    currentX++;
                }//path to left
                else if (currentX > endX && tileArray[currentX - 1, currentY].isValidMove)
                {
                    Tile tempTile = tileArray[currentX - 1, currentY];
                    movePath.Add(tempTile);
                    Debug.Log("X:" + (currentX - 1) + "Y:" + currentY);
                    currentX--;
                }//path to bottom
                else if (currentY < endY && tileArray[currentX, currentY + 1].isValidMove)
                {
                    Tile tempTile = tileArray[currentX, currentY + 1];
                    movePath.Add(tempTile);
                    Debug.Log("X:" + currentX + "Y:" + (currentY + 1));
                    currentY++;
                }//path to top
                else if (currentY > endY && tileArray[currentX, currentY - 1].isValidMove)
                {
                    Tile tempTile = tileArray[currentX, currentY - 1];
                    movePath.Add(tempTile);
                    Debug.Log("X:" + currentX + "Y:" + (currentY - 1));
                    currentY--;
                }
                else
                {
                    Debug.Log("X:" + currentX + "Y:" + (currentY - 1));
                }
            }
            return movePath;
        }
        public void SetAction(string newAction)
        {
            Debug.Log(newAction + " Action Called");


        }

        public void Attack()
        {
            anim.SetBool("isAttacking", true);
            StartCoroutine("AttackAnimation");
        }
    }

}
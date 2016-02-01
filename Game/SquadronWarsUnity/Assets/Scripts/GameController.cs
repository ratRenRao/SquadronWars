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
            MOVE,
            Attack
        }
        public TileMap tileMap;
        public GameObject currentGameCharacter;
        public GameObject targetGameCharacter;
        Vector3 hitDown;
        RaycastHit2D hit;
        Animator anim;
        Animator tarAnim;
        Tile targetTile;
        Tile tile = null;
        Tile prevTile = null;
        Tile lastTile;
        Tile[,] tileArray;
        Dictionary<string, int> inventory;
        GameCharacter curGameCharacter;
        GameCharacter tarGameCharacter;
        Character character;
        Character targetCharacter;
        Action action = Action.IDLE;
        bool isWalking;
        bool isCharacter;
        bool reachedPosition = true;
        bool lifeLost = false;
        bool arraySet = false;
        int count = 0;
        List<Tile> validMoves = new List<Tile>();
        List<Tile> path = new List<Tile>();


        // Use this for initialization
        void Start()
        {
            PrepTest();
            prevTile = targetTile.GetComponent<Tile>();
            tile = prevTile;
            // string obj = this.name;
            anim = currentGameCharacter.GetComponent<Animator>();
            tarAnim = targetGameCharacter.GetComponent<Animator>();
        }

        public Stats GetBonusStats(Character character)
        {
            foreach (Equipment equipment in character.equipment.Values)
            {
                character.alteredStats = character.alteredStats.concatStats(character.alteredStats, equipment.stats);
            }
            return character.alteredStats;
        }

        void Update()
        {
            if (!arraySet)
            {
                tileArray = tileMap.tileArray;
                tileArray[0, 0].character = currentGameCharacter;
                tileArray[0, 0].isOccupied = true;
                tileArray[1, 0].character = targetGameCharacter;
                tileArray[1, 0].isOccupied = true;
                arraySet = true;
            }
            if (reachedPosition == false && count < path.Count)
            {
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
                Debug.Log("Transform: " + currentGameCharacter.transform.localPosition.x + " " + currentGameCharacter.transform.localPosition.y);
                float currentX = (float)(System.Math.Round(currentGameCharacter.transform.localPosition.x, 2));
                float currentY = (float)(System.Math.Round(currentGameCharacter.transform.localPosition.y, 2));
                float targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
                float targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
                //  Transform targetLocation = targetTile.transform;
                if (currentX - targetX > 0)
                {
                    anim.SetFloat("x", -1);
                    anim.SetFloat("y", 0);
                    currentGameCharacter.transform.position += new Vector3(-0.1f, 0);
                }
                if (currentX - targetX < 0)
                {
                    anim.SetFloat("x", 1);
                    anim.SetFloat("y", 0);

                    currentGameCharacter.transform.position += new Vector3(0.1f, 0);
                }
                if (currentY - targetY > 0)
                {
                    anim.SetFloat("x", 0);
                    anim.SetFloat("y", -1);


                    currentGameCharacter.transform.position += new Vector3(0, -0.1f);
                }

                if (currentY - targetY < 0)
                {
                    anim.SetFloat("x", 0);
                    anim.SetFloat("y", 1);

                    currentGameCharacter.transform.position += new Vector3(0, 0.1f);
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
                        targetTile.isOccupied = true;
                        targetTile.character = currentGameCharacter;
                    }
                    else {
                        count++;
                        targetTile = path[count];
                    }
                }
            }
            else
            {

                if (Input.GetMouseButtonUp(0) && reachedPosition)
                {

                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (action == Action.MOVE)
                    {
                        Move();
                    }
                    if (action == Action.Attack)
                    {
                        if (hit.collider != null)
                        {
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                Attack(tempTile);
                                clearHighlights(validMoves);
                            }
                        }
                    }
                }

            }
        }

        private void Move()
        {
            if (hit.collider != null)
            {
                lastTile = targetTile;
                targetTile = hit.collider.gameObject.GetComponent<Tile>();
                Tile tempTile = targetTile;
                if (tempTile.isValidMove)
                {
                    tile = tempTile;
                    prevTile = lastTile;
                    Debug.Log("Move Location: " + curGameCharacter.x + " " + curGameCharacter.y);
                    curGameCharacter.x = tile.x;
                    curGameCharacter.y = tile.y;
                    path = buildPath(prevTile, tile);
                    prevTile.isOccupied = false;
                    prevTile.character = null;
                    targetTile = path[0];
                    reachedPosition = false;
                    clearHighlights(validMoves);
                }
                else
                {
                    targetTile = lastTile;
                }
            }
        }

        public void buttonMove()
        {

        }
        public void showMoves()
        {
           // int currentX = currentCharacter.x;
          //  int currentY = currentCharacter.y;
            int tileX = tile.x;
            int tileY = tile.y;
            Tile[,] tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
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
                        tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                //Create Top Left Move Tiles
                move = character.alteredStats.speed;
                for (int i = 1; i < character.alteredStats.speed; i++)
                {
                    for (int j = 1; j < move; j++)
                    {
                        if (tileX - i >= 0 && tileY - j >= 0)
                        {
                            Tile tempTile = tileArray[tileX - i, tileY - j];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                            tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                            tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                            tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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


        public void clearHighlights(List<Tile> tiles)
        {
            action = Action.IDLE;
            foreach (Tile tile in tiles)
            {
                tile.highlight.SetActive(false);
                tile.isValidMove = false;
            }
            tiles.Clear();
        }
        public bool checkMove(Tile tile)
        {
            int currentX = curGameCharacter.x;
            int currentY = curGameCharacter.y;
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
                    currentX++;
                }//path to left
                else if (currentX > endX && tileArray[currentX - 1, currentY].isValidMove)
                {
                    Tile tempTile = tileArray[currentX - 1, currentY];
                    movePath.Add(tempTile);
                    currentX--;
                }//path to bottom
                else if (currentY < endY && tileArray[currentX, currentY + 1].isValidMove)
                {
                    Tile tempTile = tileArray[currentX, currentY + 1];
                    movePath.Add(tempTile);
                    currentY++;
                }//path to top
                else if (currentY > endY && tileArray[currentX, currentY - 1].isValidMove)
                {
                    Tile tempTile = tileArray[currentX, currentY - 1];
                    movePath.Add(tempTile);
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

        public void ShowAttackMoves()
        {
            int tileX = tile.x;
            int tileY = tile.y;
            Tile[,] tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            if (tileY - 1 >= 0)
            {
                Tile tempTile = tileArray[tileX, tileY - 1];
                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                tempTile.highlight.SetActive(true);
                tempTile.isValidMove = true;
                validMoves.Add(tempTile);

            }
            if (tileY + 1 < tileArray.GetLength(1))
            {
                Tile tempTile = tileArray[tileX, tileY + 1];
                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                tempTile.highlight.SetActive(true);
                tempTile.isValidMove = true;
                validMoves.Add(tempTile);

            }
            if (tileX - 1 >= 0)
            {
                Tile tempTile = tileArray[tileX - 1, tileY];
                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                tempTile.highlight.SetActive(true);
                tempTile.isValidMove = true;
                validMoves.Add(tempTile);

            }
            if (tileY + 1 < tileArray.GetLength(0))
            {
                Tile tempTile = tileArray[tileX + 1, tileY];
                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                tempTile.highlight.SetActive(true);
                tempTile.isValidMove = true;
                validMoves.Add(tempTile);

            }
            action = Action.Attack;
        }

        public void Attack(Tile targetTile)
        {
            action = Action.IDLE;            
            anim.SetBool("isAttacking", true);
            float currentX = (float)(System.Math.Round(tile.transform.localPosition.x, 2));
            float currentY = (float)(System.Math.Round(tile.transform.localPosition.y, 2));
            float targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
            float targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
            //  Transform targetLocation = targetTile.transform;
            if (currentX - targetX > 0)
            {
                anim.SetFloat("x", -1);
                anim.SetFloat("y", 0);
            }
            if (currentX - targetX < 0)
            {
                anim.SetFloat("x", 1);
                anim.SetFloat("y", 0);
            }
            if (currentY - targetY > 0)
            {
                anim.SetFloat("x", 0);
                anim.SetFloat("y", -1);
            }

            if (currentY - targetY < 0)
            {
                anim.SetFloat("x", 0);
                anim.SetFloat("y", 1);
            }
            if (targetTile.isOccupied)
            {
                tarAnim = targetTile.character.GetComponent<Animator>();
                StartCoroutine("AttackAnimation");
            }
            else {
                StartCoroutine("AttackAnimationNothing");
            }
        }

        public void Injured()
        {
            tarAnim = targetGameCharacter.GetComponent<Animator>();
            tarAnim.SetBool("isAttacked", true);
            StartCoroutine("InjuredAnimation");
        }

        IEnumerator AttackAnimation()
        {
            yield return new WaitForSeconds(.2f);
            tarAnim.SetBool("isAttacked", true);
            yield return new WaitForSeconds(.4f);
            anim.SetBool("isAttacking", false);
            tarAnim.SetBool("isAttacked", false);
        }
        IEnumerator AttackAnimationNothing()
        {
            yield return new WaitForSeconds(.5f);
            anim.SetBool("isAttacking", false);
        }
        IEnumerator InjuredAnimation()
        {
            yield return new WaitForSeconds(.4f);
            tarAnim.SetBool("isAttacked", false);
        }

        public void EndTurn()
        {
            int newX = tarGameCharacter.x;
            int newY = tarGameCharacter.y;
            Debug.Log(newX + " " + newY);
            GameObject tempGameCharacter = currentGameCharacter;
            GameCharacter tempCurGameCharacter = curGameCharacter;
            Character tempCharacter = character;
            Animator tempAnim = anim;
            currentGameCharacter = targetGameCharacter;
            targetGameCharacter = tempGameCharacter;
            curGameCharacter = tarGameCharacter;
            tarGameCharacter = tempCurGameCharacter;
            character = targetCharacter;            
            targetCharacter = tempCharacter;
            anim = tarAnim;
            tarAnim = tempAnim;
            
            prevTile = tileArray[newX, newY];
            tile = prevTile;
            targetTile = tile;
        }
        private void PrepTest()
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
            character = new Character(1, stat1, 1, "Saint Lancelot", 1, 75, equipment);
            character.alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character.alteredStats = GetBonusStats(character);
            character.alteredStats.speed = 4;
            Stats stat2 = new Stats(3, 3, 3, 3, 3, 3, 2);
            targetCharacter = new Character(1, stat2, 1, "Orc", 1, 75, equipment);
            targetCharacter.alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            targetCharacter.alteredStats = GetBonusStats(targetCharacter);
            targetCharacter.alteredStats.speed = 3;
            targetTile = GameObject.FindGameObjectWithTag("start").GetComponent<Tile>();
            curGameCharacter = currentGameCharacter.GetComponent<GameCharacter>();
            tarGameCharacter = targetGameCharacter.GetComponent<GameCharacter>();
            curGameCharacter.x = 0;
            curGameCharacter.y = 0;
            tarGameCharacter.x = 1;
            tarGameCharacter.y = 0;
        }
    }

}
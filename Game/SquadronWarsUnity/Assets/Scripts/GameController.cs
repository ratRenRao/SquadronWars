﻿using UnityEngine;
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
            Attack,
            AttackAbility,
            CastAbility,
            Occupy
        }
        public TileMap tileMap;
        GameObject currentGameCharacter;
        GameObject targetGameCharacter;
        public GameObject characterStatsPanel;
        public CharacterStatsPanel statsPanel;
        public GameObject actionPanel;
        public AudioSource battlesong;
        public Button attackButton;
        public Button abilityButton;
        public Button moveButton;
        public int player1SpawnXStart;
        public int player1SpawnXEnd;
        public int player1SpawnY;
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
        CharacterGameObject currentCharacterGameObject;
        CharacterGameObject targetCharacterGameObject;
        List<CharacterGameObject> characters = new List<CharacterGameObject>();
        List<CharacterGameObject> gCharacters = GlobalConstants.MatchCharacters; 
        List<GameObject> myCharacters = new List<GameObject>();
        //Character character;
        //Character targetCharacter;
        Action action = Action.IDLE;
        bool isWalking;
        bool isCharacter;
        bool reachedPosition = true;
        bool lifeLost = false;
        bool arraySet = false;
        bool placeCharacterPhase = false;
        bool hidePanel = true;
        string selectedAbility;
        int count = 0;
        int unitPlacedCount = 0;
        List<Tile> walkableTiles = new List<Tile>();
        List<Tile> validMoves = new List<Tile>();
        List<Tile> path = new List<Tile>();
        List<GameObject> turnQueue = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            // string obj = this.name;
            //anim = currentCharacterGameObject.GetComponent<Animator>();
            //tarAnim = targetCharacterGameObject.GetComponent<Animator>();
            //tarAnim.SetFloat("x", 0);
            //tarAnim.SetFloat("y", -1);
            //hidePanel = false;
            battlesong.playOnAwake = true;
            placeCharacterPhase = true;
            characters = GlobalConstants.MatchCharacters;
            //statsPanel.charName.text = characters[0].CharacterClassObject.Name;
            statsPanel.hp.text = characters[0].CharacterClassObject.CurrentStats.HitPoints + " / " + characters[0].CharacterClassObject.CurrentStats.HitPoints;
            statsPanel.mp.text = characters[0].CharacterClassObject.CurrentStats.MagicPoints + " / " + characters[0].CharacterClassObject.CurrentStats.MagicPoints;
            tileArray = tileMap.tileArray;
            highlightSpawn();
            arraySet = true;
        }

        

        void Update()
        {
            if (!arraySet)
            {
                
                
            }
            if (hidePanel)
            {
                actionPanel.SetActive(false);
                if (!placeCharacterPhase)
                {
                    characterStatsPanel.SetActive(false);
                }
            }
            if (!hidePanel)
            {
                actionPanel.SetActive(true);
                characterStatsPanel.SetActive(true);
            }
            if (reachedPosition == false && count < path.Count)
            {
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
                
                float currentX = (float)(System.Math.Round(currentCharacterGameObject.transform.localPosition.x, 2));
                float currentY = (float)(System.Math.Round(currentCharacterGameObject.transform.localPosition.y, 2));
                float targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
                float targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
                //  Transform targetLocation = targetTile.transform;
                if (currentX - targetX > 0)
                {
                    anim.SetFloat("x", -1);
                    anim.SetFloat("y", 0);
                    currentCharacterGameObject.transform.position += new Vector3(-0.1f, 0);
                }
                if (currentX - targetX < 0)
                {
                    anim.SetFloat("x", 1);
                    anim.SetFloat("y", 0);

                    currentCharacterGameObject.transform.position += new Vector3(0.1f, 0);
                }
                if (currentY - targetY > 0)
                {
                    anim.SetFloat("x", 0);
                    anim.SetFloat("y", -1);


                    currentCharacterGameObject.transform.position += new Vector3(0, -0.1f);
                }

                if (currentY - targetY < 0)
                {
                    anim.SetFloat("x", 0);
                    anim.SetFloat("y", 1);

                    currentCharacterGameObject.transform.position += new Vector3(0, 0.1f);
                }

                if (currentY - targetY == 0 && currentX - targetX == 0)
                {
                    if ((count + 1) == path.Count)
                    {

                        isWalking = false;
                        anim.SetBool("isWalking", isWalking);
                        reachedPosition = true;
                        path.Clear();
                        count = 0;
                        action = Action.IDLE;
                        currentCharacterGameObject.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
                        targetTile.isOccupied = true;
                        targetTile.character = currentCharacterGameObject;
                        targetTile.characterObject = currentGameCharacter;
                        PositionPanels();
                        hidePanel = false;
                    }
                    else {
                        count++;                        
                        currentCharacterGameObject.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
                        targetTile = path[count];
                        
                    }
                }
            }
            else
            {

                if (Input.GetMouseButtonUp(0) && reachedPosition && !placeCharacterPhase)
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (action == Action.MOVE)
                    {
                        moveButton.interactable = false;                
                        Move();
                        currentCharacterGameObject.hasMoved = true;
                    }
                    if (action == Action.Attack)
                    {
                        if (hit.collider != null)
                        {
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                attackButton.interactable = false;
                                abilityButton.interactable = false;
                                hidePanel = true;
                                GetTarget(tempTile);
                                Attack(tempTile, null);
                                clearHighlights(validMoves);
                                currentCharacterGameObject.hasAttacked = true;
                                //hidePanel = false;
                            }
                        }
                    }
                    if (action == Action.AttackAbility)
                    {
                        if (hit.collider != null)
                        {
                            attackButton.interactable = false;
                            abilityButton.interactable = false;
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                hidePanel = true;
                                GetTarget(tempTile);
                                clearHighlights(validMoves);
                                Attack(tempTile, selectedAbility);
                                currentCharacterGameObject.hasAttacked = true;
                            }              
                        }
                    }
                    if (action == Action.CastAbility)
                    {
                        attackButton.interactable = false;
                        abilityButton.interactable = false;
                        if (hit.collider != null)
                        {                            
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                hidePanel = true;
                                GetTarget(tempTile);
                                clearHighlights(validMoves);
                                Cast(tempTile, selectedAbility);
                                currentCharacterGameObject.hasAttacked = true;
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0) && placeCharacterPhase)
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider != null)
                    {
                        
                        Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                        if (tempTile.isValidMove)
                        {
                            placeCharacter(tempTile, gCharacters[unitPlacedCount]);
                            tempTile.isValidMove = false;
                            tempTile.highlight.SetActive(false);
                            tempTile.isOccupied = true;
                            if ((unitPlacedCount + 1) == characters.Count)
                            {
                                StartCoroutine(WaitForClick("characterload"));                                
                                
                                clearHighlights(validMoves);
                                hidePanel = false;
                            }
                            unitPlacedCount++;
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0) && action == Action.Occupy)
                {
                    if (hit.collider != null)
                    {

                        Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                        tempTile.isOccupied = true;
                        hidePanel = false;
                        action = Action.IDLE;
                    }
                }
                if (Input.GetKeyDown("escape") && action != Action.IDLE)
                {
                    Debug.Log("Escape key called");
                    hidePanel = false;
                    clearHighlights(validMoves);
                    action = Action.IDLE;
                }
            }
        }

        public void OccupyTiles()
        {
            //anim.SetBool("isDead", true);
            //Debug.Log(anim.GetBool("isDead"));
            /*foreach (Tile t in tileArray)
            {
                Debug.Log(t.x + ", " + t.y);
                GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));

                GameObject highlight = GameObject.Instantiate(temp, new Vector3(t.transform.position.x + 1.6f, t.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                highlight.transform.parent = t.transform;
                t.highlight = highlight;
                t.highlight.SetActive(false);
                highlight.transform.localScale = new Vector3(0.072f, 0.072f, 0.0f);
            }*/
            
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
                    hidePanel = true;
                    tile = tempTile;
                    prevTile = lastTile;
                    currentCharacterGameObject.X = tile.x;
                    currentCharacterGameObject.Y = tile.y;
                    path = buildPath(prevTile, tile);
                    prevTile.isOccupied = false;
                    prevTile.character = null;
                    targetTile = path[0];
                    reachedPosition = false;
                    clearHighlights(validMoves);
                    currentCharacterGameObject.hasMoved = true;
                    
                }
                else
                {
                    targetTile = lastTile;
                }
            }
        }

        public void AttackAbility(string ability)
        {
            if (!currentCharacterGameObject.hasAttacked)
            {
                ShowAttackMoves("ability");
                selectedAbility = ability;
            }
        }

        public void CastAbility(string ability)
        {
            if (!currentCharacterGameObject.hasAttacked)
            {
                showCastMoves();
                selectedAbility = ability;
            }
        }

        public void showMoves()
        {
            //FindPossibleMoves(tile);
            foreach (Tile t in walkableTiles)
            {
                t.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
                t.highlight.SetActive(true);
                t.isValidMove = true;
                validMoves.Add(t);
            }
            hidePanel = true;
            StartCoroutine(WaitForClick("move"));            
        }

        public List<Tile> buildPath(Tile start, Tile end)
        {
            int currentX = start.x;
            int currentY = start.y;
            int endX = end.x;
            int endY = end.y;
            int checkCount = 0;
            int moveVal = Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
            bool pathFound = false;
            List<List<Tile>> openPath = new List<List<Tile>>();
            List<List<Tile>> curPathList = new List<List<Tile>>();
            List<Tile> openPathClone = new List<Tile>();
            List<Tile> movePath = new List<Tile>();
            walkableTiles = new List<Tile>();
            int moves = currentCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
            movePath.Add(start);
            openPath.Add(movePath);
            while (!pathFound)
            {

                foreach (List<Tile> lt in openPath)
                {
                    openPathClone = new List<Tile>();
                    Tile t = lt[lt.Count - 1];
                    List<Tile> tempList = GetPossiblePaths(t, false);
                    foreach (Tile til in tempList)
                    {
                        if (til.Equals(end))
                        {
                            movePath = lt;
                            pathFound = true;
                            break;
                        }
                        else
                        {
                            checkCount++;
                            moveVal = Mathf.Abs(til.x - end.x) + Mathf.Abs(til.y - end.y);
                            if (moveVal < moves)
                            {
                                openPathClone = new List<Tile>(lt);
                                openPathClone.Add(til);
                                walkableTiles.Add(til);
                                curPathList.Add(openPathClone);
                            }
                        }
                    }

                }

                if (!pathFound)
                {
                    openPath = curPathList;
                    curPathList = new List<List<Tile>>();
                    movePath = new List<Tile>();
                }
                else
                {
                    movePath.Add(end);
                }
            }
            //}
            return movePath;
        }

        private List<Tile> GetPossiblePaths(Tile t, bool isPathSearch)
        {
            
            List<Tile> tempList = new List<Tile>();            
            if (t.y - 1 >= 0)
            {
                if ((tileArray[t.x, t.y - 1].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x, t.y - 1]) && !tileArray[t.x, t.y - 1].isOccupied)
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x, t.y - 1]);
                }
            }
            if (t.y + 1 < tileMap.yLength)
            {
                if ((tileArray[t.x, t.y + 1].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x, t.y + 1]) && !tileArray[t.x, t.y + 1].isOccupied)
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x, t.y + 1]);
                }
            }
            if (t.x - 1 >= 0)
            {
                if ((tileArray[t.x - 1, t.y].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x - 1, t.y]) && !tileArray[t.x - 1, t.y].isOccupied)
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x - 1, t.y]);
                }
            }
            if (t.x + 1 < tileMap.xLength)
            {
                if ((tileArray[t.x + 1, t.y].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x + 1, t.y]) && !tileArray[t.x + 1, t.y].isOccupied)
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x + 1, t.y]);
                }
            }
            return tempList;
        }
        private void FindPossibleMoves(Tile tile)
        {
            currentCharacterGameObject.CharacterClassObject.CurrentStats.Speed = 5;
            int move = currentCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
            int countCheck = 0;
            List<Tile> openPath = new List<Tile>();
            List<Tile> openPathTemp = new List<Tile>();
            walkableTiles = new List<Tile>();            
            openPath.Add(tile);
            for (int i = 0; i < move; i++)
            {
                openPathTemp = new List<Tile>();
                foreach (Tile t in openPath)
                {
                    List<Tile> temp = GetPossiblePaths(t, true);
                    foreach (Tile til in temp)
                    {
                        countCheck++;               
                        openPathTemp.Add(til);
                        walkableTiles.Add(til);
                        
                    }                    
                }
                openPath = new List<Tile>(openPathTemp);
                
            }
        }

        public void showCastMoves()
        {
            // int currentX = currentCharacter.x;
            //  int currentY = currentCharacter.y;
            int tileX = tile.x;
            int tileY = tile.y;
            Tile[,] tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            //tileArray[1, 0].isObstructed = true;
            int range = 4;
            if (!currentCharacterGameObject.hasAttacked)
            {
                if (action == Action.IDLE)
                {
                    hidePanel = true;
                    validMoves = new List<Tile>();
                    //Create Bottom Move Tiles
                    for (int i = 1; i <= range; i++)
                    {

                        if (tileY + i < tileArray.GetLength(1))
                        {
                            Tile tempTile = tileArray[tileX, tileY + i];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));

                            // GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    }
                    //Create Top Move Tiles
                    for (int i = 1; i <= range; i++)
                    {
                        if (tileY - i >= 0)
                        {
                            Tile tempTile = tileArray[tileX, tileY - i];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    }
                    //Create Left Move Tiles
                    for (int i = 1; i <= range; i++)
                    {
                        if (tileX - i >= 0)
                        {
                            Tile tempTile = tileArray[tileX - i, tileY];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    }
                    //Create Right Move Tiles
                    for (int i = 1; i <= range; i++)
                    {
                        if (tileX + i < tileArray.GetLength(0))
                        {
                            Tile tempTile = tileArray[tileX + i, tileY];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                    range = 4;
                    var tempRange = range;
                    for (int i = 1; i < range; i++)
                    {                        
                        for (int j = 1; j < tempRange; j++)
                        {
                            
                            if (tileX - i >= 0 && tileY - j >= 0)
                            {
                                Tile tempTile = tileArray[tileX - i, tileY - j];
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    //Create Top Right Move Tiles
                    range = 4;
                    tempRange = range;
                    for (int i = 1; i <= range; i++)
                    {
                        for (int j = 1; j < tempRange; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY - j >= 0)
                            {
                                Tile tempTile = tileArray[tileX + i, tileY - j];
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    //Create Bottom Left Move Tiles
                    range = 4;
                    tempRange = range;
                    for (int i = 1; i <= range; i++)
                    {
                        for (int j = 1; j < tempRange; j++)
                        {
                            if (tileX - i >= 0 && tileY + j < tileArray.GetLength(1))
                            {
                                Tile tempTile = tileArray[tileX - i, tileY + j];
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    //Create Bottom Left Move Tiles
                    range = 4;
                    tempRange = range;
                    for (int i = 1; i <= range; i++)
                    {
                        for (int j = 1; j < tempRange; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY + j < tileArray.GetLength(1))
                            {
                                Tile tempTile = tileArray[tileX + i, tileY + j];
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    StartCoroutine(WaitForClick("cast"));

                }
                else
                {
                    clearHighlights(validMoves);
                }
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
            int currentX = currentCharacterGameObject.X;
            int currentY = currentCharacterGameObject.Y;
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

       
        public void SetAction(string newAction)
        {


        }

        public void ShowAttackMoves(string type)
        {
            int tileX = tile.x;
            int tileY = tile.y;
            Tile[,] tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            if (!currentCharacterGameObject.hasAttacked)
            {
                hidePanel = true;
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
                StartCoroutine(WaitForClick(type));                
            }
        }

        public void GetTarget(Tile tile) {
            if (tile.isOccupied)
            {
                tarAnim = tile.characterObject.GetComponent<Animator>();
                targetCharacterGameObject = tile.character;
                targetCharacterGameObject = tile.character;
            }
            //targetCharacterGameObject = tile.GetComponent<CharacterGameObject>();
            //targetCharacterGameObject = targetCharacterGameObject.transform.parent.gameObject;

        }

        public void Cast(Tile targetTile, string ability)
        {
            action = Action.IDLE;
            anim.SetBool("isCasting", true);
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
                StartCoroutine(CastAnimation(targetTile, ability));
            }
            else
            {
                StartCoroutine("CastAnimationNothing");
            }
        }

        public void Attack(Tile targetTile, string ability)
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

                StartCoroutine(AttackAnimation(targetTile, ability));
            }
            else {
                StartCoroutine("AttackAnimationNothing");
            }
        }

        public void Injured()
        {
            tarAnim = targetCharacterGameObject.GetComponent<Animator>();
            tarAnim.SetBool("isAttacked", true);
            StartCoroutine("InjuredAnimation");
        }

        int CalculateMagicDamage(string ability)
        {
            int mDmg = currentCharacterGameObject.CharacterClassObject.CurrentStats.MagicAttack;
            int mDef = targetCharacterGameObject.CharacterClassObject.CurrentStats.MagicDefense;
            if (ability == "fire")
            {
                mDmg = mDmg * 2;
            }
            return mDmg - mDef;
        }

        int CalculateDamage()
        {
            int dmg = currentCharacterGameObject.CharacterClassObject.CurrentStats.Dmg;
            int def = targetCharacterGameObject.CharacterClassObject.CurrentStats.Defense;
            Debug.Log("Damage: " + dmg);
            Debug.Log("Def: " + def);
            return dmg - def;
        }

        IEnumerator AttackAnimation(Tile tempTile, string ability)
        {
            yield return new WaitForSeconds(.2f);
            tarAnim.SetBool("isAttacked", true);            
            yield return new WaitForSeconds(.3f);
            anim.SetBool("isAttacking", false);
            tarAnim.SetBool("isAttacked", false);
            int damage = 0;
            if (ability != null)
            {
                GameObject temp = (GameObject)Resources.Load((ability), typeof(GameObject));
                GameObject spell = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (tempTile.y * 2);
                spell.transform.parent = tempTile.transform;
                damage = CalculateMagicDamage(ability);
            }
            else
            {
                damage = CalculateDamage();
            }
            GameObject particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
            GameObject damageText = (GameObject)Resources.Load(("Prefabs/DamageText"), typeof(GameObject));
            GameObject dmgObject = GameObject.Instantiate(damageText, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y + 3.2f), Quaternion.identity) as GameObject;
            dmgObject.transform.parent = particleCanvas.transform;
            dmgObject.GetComponent<Text>().text = damage.ToString();
            targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP -= damage;
            yield return new WaitForSeconds(.4f);
            if (targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP < 0)
            {
                targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP = 0;
                targetCharacterGameObject.isDead = true;
                //myCharacters.Remove(targetCharacterGameObject.gameObject);
                tarAnim.SetBool("isDead", true);
                yield return new WaitForSeconds(.8f);
            }
            hidePanel = false;
        }
        IEnumerator AttackAnimationNothing()
        {
            yield return new WaitForSeconds(.5f);
            anim.SetBool("isAttacking", false);
            hidePanel = false;
        }
        IEnumerator CastAnimation(Tile tempTile, string ability)
        {
            yield return new WaitForSeconds(.5f);
            
            anim.SetBool("isCasting", false);
            float wait = 0;
            int damage = 0;
            if (ability != null)
            {
                Debug.Log(ability);
                GameObject temp = (GameObject)Resources.Load((ability), typeof(GameObject));
                GameObject spell = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (tempTile.y * 2);                
                spell.transform.parent = tempTile.transform;
                spell.transform.localScale = new Vector3(1, 1, 0.0f);
                yield return new WaitForSeconds(.2f);
                tarAnim.SetBool("isAttacked", true);
                yield return new WaitForSeconds(.4f);
                tarAnim.SetBool("isAttacked", false);
                wait = spell.GetComponent<AutoDestroy>().animTime + .4f;
                damage = CalculateMagicDamage(ability);

                yield return new WaitForSeconds(wait);
                GameObject particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
                GameObject damageText = (GameObject)Resources.Load(("Prefabs/DamageText"), typeof(GameObject));
                GameObject dmgObject = GameObject.Instantiate(damageText, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y + 3.2f), Quaternion.identity) as GameObject;
                dmgObject.transform.parent = particleCanvas.transform;
                dmgObject.GetComponent<Text>().text = damage.ToString();
                targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP -= damage;
                yield return new WaitForSeconds(.4f);
                if (targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP < 0)
                {
                    targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP = 0;
                    targetCharacterGameObject.isDead = true;
                    //myCharacters.Remove(targetCharacterGameObject.gameObject);
                    tarAnim.SetBool("isDead", true);
                    yield return new WaitForSeconds(.8f);
                }
            }
            hidePanel = false;
        }
        IEnumerator CastAnimationNothing()
        {
            yield return new WaitForSeconds(.5f);
            anim.SetBool("isCasting", false);
            hidePanel = false;
        }
        IEnumerator InjuredAnimation()
        {
            yield return new WaitForSeconds(.4f);
            tarAnim.SetBool("isAttacked", false);
            hidePanel = false;
        }
        IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(.5f);
        }
        IEnumerator WaitForClick(string act)
        {
            yield return new WaitForSeconds(.1f);
            if (act == "move")
            {
                action = Action.MOVE;
            }
            if (act == "attack")
            {
                action = Action.Attack;
            }
            if (act == "ability")
            {
                action = Action.AttackAbility;
            }
            if (act == "cast")
            {
                action = Action.CastAbility;
            }
            if (act == "characterload")
            {
                SelectNextCharacter();
            }
        }

        private void PrepTest()
        {
            
           /*Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>
                {
                    {ItemType.Helm, GlobalConstants.ItemList["Cloth Helm"] },
                    {ItemType.SHOULDERS, GlobalConstants.ItemList["Cloth Shoulders"] },
                    {ItemType.CHEST, GlobalConstants.ItemList["Cloth Chest"] },
                    {ItemType.GLOVES, GlobalConstants.ItemList["Cloth Gloves"] },
                    {ItemType.LEGS, GlobalConstants.ItemList["Cloth Legs"] },
                    {ItemType.BOOTS, GlobalConstants.ItemList["Cloth Boots"] },
                };
            Equipment equipment = new Equipment();
            equipment.helmObject = GlobalConstants.ItemList["Cloth Helm"];
            equipment.shouldersObject = GlobalConstants.ItemList["Cloth Shoulders"];
            equipment.chestObject = GlobalConstants.ItemList["Cloth Chest"];
            equipment.glovesObject = GlobalConstants.ItemList["Cloth Gloves"];
            equipment.pantsObject = GlobalConstants.ItemList["Cloth Legs"];
            equipment.bootsObject = GlobalConstants.ItemList["Cloth Boots"];
            equipment.accessory1Object = GlobalConstants.ItemList["None(Accessory)"];
            equipment.accessory2Object = GlobalConstants.ItemList["None(Accessory)"];
            //screen = ScreenOrientation.Landscape;*/
            /*Stats stat1 = new Stats(5, 4, 6, 3, 2, 9, 5);
            CharacterGameObject character1 = new CharacterGameObject(1, stat1, 1, "Saint Lancelot", 1, 75, equipment);
            character1.CurrentStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character1.CurrentStats = GetBonusStats(character1);
            character1.CurrentStats.Speed = 4;
            character1.SpriteId = 1;
            Stats stat2 = new Stats(3, 3, 3, 4, 3, 3, 2);
            CharacterGameObject character2 = new CharacterGameObject(1, stat2, 1, "Ragthar", 1, 75, equipment);
            character2.CurrentStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character2.CurrentStats = GetBonusStats(character2);
            character2.CurrentStats.Speed = 3;
            character2.SpriteId = 2;
            GlobalConstants.MatchCharacters.Add(character1);
            GlobalConstants.MatchCharacters.Add(character2);
            characters = GlobalConstants.MatchCharacters;
            */
            
        }

        public void highlightSpawn()
        {
            for(int i = player1SpawnXStart; i < player1SpawnXEnd; i++)
            {
                for(int j = 0; j < player1SpawnY; j++)
                {
                    Tile tempTile = tileArray[i, j];
                    //Debug.Log(tempTile);
                    if (!tempTile.isOccupied)
                    {
                        tempTile.highlight.SetActive(true);
                        tempTile.isValidMove = true;
                        validMoves.Add(tempTile);
                    }
                }
            }
            /*foreach (GameObject t in tileMap.tiles)
            {
                t.GetComponent<Tile>().highlight.SetActive(true);
                t.GetComponent<Tile>().isValidMove = true;
                validMoves.Add(t.GetComponent<Tile>());                
            }*/
        }

        //new CharacterGameObject(characters[unitPlacedCount], tempTile.x, tempTile.y);
        /*
        public CharacterGameObject createCharacter(CharacterGameObject gc)
        {
            return new CharacterGameObject(ref gc, 0, 0);
        }*/

        public void placeCharacter(Tile tempTile, CharacterGameObject gameCharacter)
        {
            GameObject tileMap = GameObject.FindGameObjectWithTag("map");
            tempTile.isOccupied = true;
            gameCharacter.X = tempTile.x;
            gameCharacter.Y = tempTile.y;
            tempTile.character = gameCharacter;

            GameObject temp = (GameObject)Resources.Load(("Prefabs/Character1" /*+ characters[unitPlacedCount].CharacterClassObject.SpriteId*/), typeof(GameObject));
            //gameCharacter.gameObject.transform.position = new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y);
            //gameCharacter.gameObject.transform.rotation = Quaternion.identity;
            GameObject tempchar = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y), Quaternion.identity) as GameObject;
            tempchar.GetComponent<SpriteRenderer>().sortingOrder = 6 + (tempTile.y * 2);
            tempchar.transform.parent = tileMap.transform;
            tempchar.transform.localScale = new Vector3(1, 1, 0.0f);
            tempchar.AddComponent<CharacterGameObject>();
            tempchar.GetComponent<CharacterGameObject>().CharacterClassObject = gameCharacter.CharacterClassObject;
            tempchar.GetComponent<CharacterGameObject>().X = gameCharacter.X;
            tempchar.GetComponent<CharacterGameObject>().Y = gameCharacter.Y;
            tempTile.characterObject = tempchar;

            //tempGC = gameChar;
            Animator tempAnim = tempchar.GetComponent<Animator>();
            tempAnim.SetFloat("x", 0);
            tempAnim.SetFloat("y", -1);
            myCharacters.Add(tempchar);
            if (unitPlacedCount + 1 < characters.Count)
            {
                statsPanel.charName.text = characters[unitPlacedCount + 1].CharacterClassObject.Name;
            }
        }

        public void SelectNextCharacter()
        {
            if (placeCharacterPhase)
            {                
                placeCharacterPhase = false;
                foreach(GameObject g in myCharacters){
                    CharacterGameObject tempGC = g.GetComponent<CharacterGameObject>();
                    Tile t = tileArray[tempGC.X, tempGC.Y];
                    tempGC.CharacterClassObject.CurrentStats.CurHP = tempGC.CharacterClassObject.CurrentStats.HitPoints;
                    tempGC.CharacterClassObject.CurrentStats.CurMP = tempGC.CharacterClassObject.CurrentStats.MagicPoints;
                }
            }
            else
            {
                bool getNextAvailableCharacter = false;
                while (!getNextAvailableCharacter)
                {
                    myCharacters.Add(myCharacters[0]);
                    myCharacters.RemoveAt(0);
                    Tile t = tileArray[myCharacters[0].GetComponent<CharacterGameObject>().X, myCharacters[0].GetComponent<CharacterGameObject>().Y];
                    if (!t.character.isDead)
                    {
                        getNextAvailableCharacter = true;
                    }
                }
            }
            currentGameCharacter = myCharacters[0];            
            currentCharacterGameObject = currentGameCharacter.GetComponent<CharacterGameObject>();
            targetTile = tileArray[currentCharacterGameObject.X, currentCharacterGameObject.Y];
            prevTile = targetTile;
            tile = prevTile;
            anim = currentCharacterGameObject.GetComponent<Animator>();
            currentCharacterGameObject.hasAttacked = false;
            currentCharacterGameObject.hasMoved = false;            
            //Debug.Log(tile);
            FindPossibleMoves(tile);
            moveButton.interactable = true;
            attackButton.interactable = true;
            abilityButton.interactable = true;
            statsPanel.charName.text = currentCharacterGameObject.CharacterClassObject.Name;
            currentCharacterGameObject.CharacterClassObject.CurrentStats.Dmg = 20;
            statsPanel.hp.text = currentCharacterGameObject.CharacterClassObject.CurrentStats.CurHP + " / " + currentCharacterGameObject.CharacterClassObject.CurrentStats.HitPoints;
            statsPanel.mp.text = currentCharacterGameObject.CharacterClassObject.CurrentStats.CurMP + " / " + currentCharacterGameObject.CharacterClassObject.CurrentStats.MagicPoints;            
            PositionPanels();
        }

        public void PositionPanels()
        {
            characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x, currentCharacterGameObject.transform.position.y + 8, 0);
            actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 15, currentCharacterGameObject.transform.position.y - 7, 0);
            if (tile.x < 3)
            {
                characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 10, currentCharacterGameObject.transform.position.y + 8, 0);
                if(tile.y < 3)
                {
                    characterStatsPanel.transform.position = new Vector3(characterStatsPanel.transform.position.x, currentCharacterGameObject.transform.position.y - 21, 0);
                }
                
            }
            else if (tile.y < 3)
            {
                characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x, currentCharacterGameObject.transform.position.y - 21, 0);
                if (tile.x > 6)
                {
                    actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x - 10, actionPanel.transform.position.y, 0);
                }
            }
            else if (tile.x > 6)
            {
                actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x - 10, actionPanel.transform.position.y, 0);
            }
        }
    }
}
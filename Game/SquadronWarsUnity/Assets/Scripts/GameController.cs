﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.GameClasses;
using Assets.Scripts;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        /*
        public enum Action
        {
            IDLE,
            MOVE,
            Attack,
            AttackAbility,
            CastAbility
        }
        public TileMap tileMap;
        GameObject currentCharacterGameObject;
        GameObject targetCharacterGameObject;
        public GameObject characterStatsPanel;
        public CharacterStatsPanel statsPanel;
        public GameObject actionPanel;
        public AudioSource battlesong;
        public Button attackButton;
        public Button abilityButton;
        public Button moveButton;
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
        CharacterGameObject curCharacterGameObject;
        CharacterGameObject tarCharacterGameObject;
        List<CharacterGameObject> characters = new List<CharacterGameObject>();
        List<CharacterGameObject> gCharacters = new List<CharacterGameObject>();
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
        }

        public void GetBonusStats(ref Character character)
        {
            
            foreach (var item in character.Equipment.GetItemList())
            {
                character.CurrentStats = character.CurrentStats.ConcatStats(character.BaseStats, item.Stats);
            }
        }

        void Update()
        {
            if (!arraySet)
            {
                //PrepTest();                
                tileArray = tileMap.tileArray;
                highlightSpawn();
                arraySet = true;
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
                Debug.Log("current x:" + currentX + "; current y:" + currentY);
                Debug.Log("target x:" + targetX + "; target y:" + targetY);
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
                        curCharacterGameObject.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
                        targetTile.isOccupied = true;
                        targetTile.characterObject = currentCharacterGameObject;
                        targetTile.character = curCharacterGameObject;
                        PositionPanels();
                        hidePanel = false;
                    }
                    else {
                        count++;                        
                        curCharacterGameObject.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
                        targetTile = path[count];
                        
                    }
                }
            }
            else
            {

                if (Input.GetMouseButtonUp(0) && reachedPosition && !placeCharacterPhase)
                {
                    Debug.Log("move click called");
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (action == Action.MOVE)
                    {
                        moveButton.interactable = false;                
                        Move();
                        curCharacterGameObject.hasMoved = true;
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
                                curCharacterGameObject.hasAttacked = true;
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
                                curCharacterGameObject.hasAttacked = true;
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
                                curCharacterGameObject.hasAttacked = true;
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0) && placeCharacterPhase)
                {
                    Debug.Log("Place char called");
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider != null)
                    {

                        Debug.Log("Place char called: Valid hit");
                        Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                        if (tempTile.isValidMove)
                        {
                            foreach(CharacterGameObject c in characters)
                            {
                                gCharacters.Add(createCharacter(c));
                            }
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
                if (Input.GetKeyDown("escape") && action != Action.IDLE)
                {
                    Debug.Log("Escape key called");
                    hidePanel = false;
                    clearHighlights(validMoves);
                    action = Action.IDLE;
                }
            }
        }

        private void Move()
        {
            if (hit.collider != null)
            {
                Debug.Log(targetTile.x + " ," + targetTile.y);
                lastTile = targetTile;
                targetTile = hit.collider.gameObject.GetComponent<Tile>();
                Tile tempTile = targetTile;
                if (tempTile.isValidMove)
                {
                    hidePanel = true;
                    tile = tempTile;
                    prevTile = lastTile;
                    curCharacterGameObject.X = tile.x;
                    curCharacterGameObject.Y = tile.y;
                    path = buildPath(prevTile, tile);
                    prevTile.isOccupied = false;
                    prevTile.character = null;
                    targetTile = path[0];
                    reachedPosition = false;
                    clearHighlights(validMoves);
                    curCharacterGameObject.hasMoved = true;
                    
                }
                else
                {
                    targetTile = lastTile;
                }
            }
        }

        public void AttackAbility(string ability)
        {
            if (!curCharacterGameObject.hasAttacked)
            {
                ShowAttackMoves("ability");
                selectedAbility = ability;
            }
        }
        public void CastAbility(string ability)
        {
            if (!curCharacterGameObject.hasAttacked)
            {
                showCastMoves();
                selectedAbility = ability;
            }
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
            Debug.Log(curCharacterGameObject.CharacterClassObject.CurrentStats.Speed);
            int move = curCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
            if (!curCharacterGameObject.hasMoved)
            {
                if (action == Action.IDLE)
                {
                    hidePanel = true;
                    validMoves = new List<Tile>();
                    //Create Bottom Move Tiles
                for (int i = 1; i <= curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {

                        if (tileY + i < tileArray.GetLength(1))
                        {
                            Tile tempTile = tileArray[tileX, tileY + i];
                            if (!tempTile.isOccupied)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));

                            // GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    }
                    //Create Top Move Tiles
                for (int i = 1; i <= curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {
                        if (tileY - i >= 0)
                        {
                            Tile tempTile = tileArray[tileX, tileY - i];
                            if (!tempTile.isOccupied)
                            {                                
                                tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    }
                    //Create Left Move Tiles
                for (int i = 1; i <= curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {
                        if (tileX - i >= 0)
                        {
                            Tile tempTile = tileArray[tileX - i, tileY];
                            if (!tempTile.isOccupied)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    }
                    //Create Right Move Tiles
                move = curCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
                    for (int i = 1; i <= move; i++)
                    {
                        if (tileX + i < tileArray.GetLength(0))
                        {
                            Tile tempTile = tileArray[tileX + i, tileY];
                            if (!tempTile.isOccupied)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                move = curCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
                for (int i = 1; i < curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {
                        for (int j = 1; j < move; j++)
                        {
                            if (tileX - i >= 0 && tileY - j >= 0)
                            {
                                Tile tempTile = tileArray[tileX - i, tileY - j];
                                if (!tempTile.isOccupied)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        move--;
                    }
                    //Create Top Right Move Tiles
                move = curCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
                for (int i = 1; i < curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {
                        for (int j = 1; j < move; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY - j >= 0)
                            {
                                Tile tempTile = tileArray[tileX + i, tileY - j];
                                if (!tempTile.isOccupied)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        move--;
                    }
                    //Create Bottom Left Move Tiles
                move = curCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
                for (int i = 1; i < curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {
                        for (int j = 1; j < move; j++)
                        {
                            if (tileX - i >= 0 && tileY + j < tileArray.GetLength(1))
                            {
                                Tile tempTile = tileArray[tileX - i, tileY + j];
                                if (!tempTile.isOccupied)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        move--;
                    }
                    //Create Bottom Left Move Tiles
                move = curCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
                for (int i = 1; i < curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
                    {
                        for (int j = 1; j < move; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY + j < tileArray.GetLength(1))
                            {
                                Tile tempTile = tileArray[tileX + i, tileY + j];
                                if (!tempTile.isOccupied)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
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
                        move--;
                    }
                    StartCoroutine(WaitForClick("move"));
                    
                }
                else
                {
                    clearHighlights(validMoves);
                }
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
            if (!curCharacterGameObject.hasAttacked)
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
                                Debug.Log(range);
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
            int currentX = curCharacterGameObject.X;
            int currentY = curCharacterGameObject.Y;
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
            List<Tile> openPath = new List<Tile>();
            List<Tile> closedPath = new List<Tile>();
            List<Tile> movePath = new List<Tile>();
            for (int i = 0; i < curCharacterGameObject.CharacterClassObject.CurrentStats.Speed; i++)
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
                Debug.Log("X:" + currentX + "Y:" + (currentY - 1));
            }
            return movePath;
        }
        public void SetAction(string newAction)
        {
            Debug.Log(newAction + " Action Called");


        }

        public void ShowAttackMoves(string type)
        {
            int tileX = tile.x;
            int tileY = tile.y;
            Tile[,] tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            if (!curCharacterGameObject.hasAttacked)
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
                targetCharacterGameObject = tile.characterObject;
                tarCharacterGameObject = tile.character;
            }
            //tarCharacterGameObject = tile.GetComponent<CharacterGameObject>();
            //targetCharacterGameObject = tarCharacterGameObject.transform.parent.gameObject;

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

        int CalculateDamage()
        {
            int dmg = curCharacterGameObject.CharacterClassObject.CurrentStats.Dmg;
            int def = tarCharacterGameObject.CharacterClassObject.CurrentStats.Defense;
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
            if (ability != null)
            {
                GameObject temp = (GameObject)Resources.Load((ability), typeof(GameObject));
                GameObject spell = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (tempTile.y * 2);
                spell.transform.parent = tempTile.transform;

            }
            int damage = CalculateDamage();
            tarCharacterGameObject.CharacterClassObject.CurrentStats.HitPoints -= damage;
            Debug.Log(tarCharacterGameObject.CharacterClassObject.CurrentStats.HitPoints);
            GameObject dmgObject = GameObject.Instantiate(damageText, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y + 3.2f), Quaternion.identity) as GameObject;
            dmgObject.transform.parent = particleCanvas.transform;
            dmgObject.GetComponent<Text>().text = damage.ToString();
            {
                tarCharacterGameObject.CharacterClassObject.CurrentStats.HitPoints = 0;
                myCharacters.Remove(targetCharacterGameObject);
            }
            yield return new WaitForSeconds(.4f);
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
            if (ability != null)
            {
                GameObject temp = (GameObject)Resources.Load((ability), typeof(GameObject));
                GameObject spell = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (tempTile.y * 2);
                Debug.Log(spell.GetComponent<SpriteRenderer>().sortingOrder);
                spell.transform.parent = tempTile.transform;
                yield return new WaitForSeconds(.2f);
                tarAnim.SetBool("isAttacked", true);
                yield return new WaitForSeconds(.4f);
                tarAnim.SetBool("isAttacked", false);
                wait = spell.GetComponent<AutoDestroy>().animTime + .4f;
            }
            yield return new WaitForSeconds(wait);
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
        /*
        IEnumerator WaitForClick(string act)
            yield return new WaitForSeconds(.1f);
            if (act == "move")
            {
                action = Action.MOVE;
            }
            if (act == "attack")
            {
                action = GameController.Action.Attack;
            }
            if (act == "ability")
            {
                action = GameController.Action.AttackAbility;
            }
            if (act == "cast")
            {
                action = GameController.Action.CastAbility;
            }
            if (act == "characterload")
            {
                SelectNextCharacter();
            }
        }
        private void PrepTest()
        {
            Dictionary<ItemType, Item> equipment = new Dictionary<ItemType, Item>
                {
                    {ItemType.Helm, GlobalConstants.ItemList["Cloth Helm"] },
                    {ItemType.Shoulders, GlobalConstants.ItemList["Cloth Shoulders"] },
                    {ItemType.Chest, GlobalConstants.ItemList["Cloth Chest"] },
                    {ItemType.Gloves, GlobalConstants.ItemList["Cloth Gloves"] },
                    {ItemType.Legs, GlobalConstants.ItemList["Cloth Legs"] },
                    {ItemType.Boots, GlobalConstants.ItemList["Cloth Boots"] },
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
            //screen = ScreenOrientation.Landscape;
            Stats stat1 = new Stats(5, 4, 6, 3, 2, 9, 5);
            CharacterGameObject character1 = new CharacterGameObject(1, stat1, 1, "Saint Lancelot", 1, 75, equipment);
            character1.CurrentStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character1.CurrentStats = GetBonusStats(character1);
            character1.CurrentStats.Speed = 4;
            character1.spriteId = 1;
            Stats stat2 = new Stats(3, 3, 3, 4, 3, 3, 2);
            CharacterGameObject character2 = new CharacterGameObject(1, stat2, 1, "Ragthar", 1, 75, equipment);
            character2.CurrentStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character2.CurrentStats = GetBonusStats(character2);
            character2.CurrentStats.Speed = 3;
            character2.spriteId = 2;
            GlobalConstants.matchCharacters.Add(character1);
            GlobalConstants.matchCharacters.Add(character2);
            characters = GlobalConstants.matchCharacters;
            placeCharacterPhase = true;
            statsPanel.charName.text = characters[0].characterName;
        }
        

        public void highlightSpawn()
        {
            for(int i = 0; i < tileMap.xLength; i++)
            {
                for(int j = 0; j < tileMap.yLength; j++)
                {
                    Tile tempTile = tileArray[i, j];
                    tempTile.highlight.SetActive(true);
                    tempTile.isValidMove = true;
                    validMoves.Add(tempTile);
                }
            }
        }
        //new CharacterGameObject(characters[unitPlacedCount], tempTile.x, tempTile.y);
        public CharacterGameObject createCharacter(CharacterGameObject gc)
        {
            return new CharacterGameObject(ref gc, 0, 0);
        }
        public void placeCharacter(Tile tempTile, CharacterGameObject gameCharacter)
        {
            GameObject tileMap = GameObject.FindGameObjectWithTag("map");
            tempTile.isOccupied = true;
            gameCharacter.X = tempTile.x;
            gameCharacter.Y = tempTile.y;
            tempTile.character = gameCharacter;

            GameObject temp = (GameObject)Resources.Load(("Prefabs/Character" + characters[unitPlacedCount].Sprite), typeof(GameObject));
            GameObject tempchar = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y), Quaternion.identity) as GameObject;
            tempchar.GetComponent<SpriteRenderer>().sortingOrder = 6 + (tempTile.y * 2);
            tempchar.transform.parent = tileMap.transform;
            Debug.Log(tempchar.transform.localPosition);
            
            tempchar.AddComponent<CharacterGameObject>();
            tempchar.GetComponent<CharacterGameObject>().character = gameCharacter.character;
            tempchar.GetComponent<CharacterGameObject>().x = gameCharacter.x;
            tempchar.GetComponent<CharacterGameObject>().y = gameCharacter.y;
            tempTile.characterObject = tempchar;
            Debug.Log(tempTile.character.x);
            //tempGC = gameChar;
            Animator tempAnim = tempchar.GetComponent<Animator>();
            tempAnim.SetFloat("x", 0);
            tempAnim.SetFloat("y", -1);
            myCharacters.Add(tempchar);
            if (unitPlacedCount + 1 < characters.Count)
            {
                statsPanel.charName.text = characters[unitPlacedCount + 1].characterName;
            }
        }

        public void SelectNextCharacter()
        {
            if (placeCharacterPhase)
            {
                placeCharacterPhase = false;
            }
            else
            {
                myCharacters.Add(myCharacters[0]);
                myCharacters.RemoveAt(0);
            }
            currentCharacterGameObject = myCharacters[0];
            curCharacterGameObject = currentCharacterGameObject.GetComponent<CharacterGameObject>();
            targetTile = tileArray[curCharacterGameObject.x, curCharacterGameObject.y];
            prevTile = targetTile;
            tile = prevTile;
            anim = currentCharacterGameObject.GetComponent<Animator>();
            curCharacterGameObject.hasAttacked = false;
            curCharacterGameObject.hasMoved = false;
            moveButton.interactable = true;
            attackButton.interactable = true;
            abilityButton.interactable = true;
            statsPanel.charName.text = curCharacterGameObject.character.characterName;
            statsPanel.hp.text = curCharacterGameObject.character.alteredStats.currentHP.ToString() + " / " + curCharacterGameObject.character.alteredStats.maxHP.ToString();
            statsPanel.mp.text = curCharacterGameObject.character.alteredStats.currentMP.ToString() + " / " + curCharacterGameObject.character.alteredStats.maxMP.ToString();
            battlesong.playOnAwake = true;
            PositionPanels();
        }

        public void PositionPanels()
        {
            Debug.Log(curCharacterGameObject.transform.position.x);
            Debug.Log(curCharacterGameObject.transform.position.y);
            characterStatsPanel.transform.position = new Vector3(curCharacterGameObject.transform.position.x, curCharacterGameObject.transform.position.y + 8, 0);
            actionPanel.transform.position = new Vector3(curCharacterGameObject.transform.position.x + 15, curCharacterGameObject.transform.position.y - 7, 0);
            if (tile.x < 3)
            {
                characterStatsPanel.transform.position = new Vector3(curCharacterGameObject.transform.position.x + 10, curCharacterGameObject.transform.position.y + 8, 0);
                if(tile.y < 3)
                {
                    characterStatsPanel.transform.position = new Vector3(characterStatsPanel.transform.position.x, curCharacterGameObject.transform.position.y - 21, 0);
                }
                
            }
            else if (tile.y < 3)
            {
                characterStatsPanel.transform.position = new Vector3(curCharacterGameObject.transform.position.x, curCharacterGameObject.transform.position.y - 21, 0);
                if (tile.x > 6)
                {
                    actionPanel.transform.position = new Vector3(curCharacterGameObject.transform.position.x - 10, actionPanel.transform.position.y, 0);
                }
            }
            else if (tile.x > 6)
            {
                actionPanel.transform.position = new Vector3(curCharacterGameObject.transform.position.x - 10, actionPanel.transform.position.y, 0);
            }
        }
        */
    }
}
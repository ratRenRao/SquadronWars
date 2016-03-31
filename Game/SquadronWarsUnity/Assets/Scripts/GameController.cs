using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine.SceneManagement;

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
            Occupy,
            WaitForGameInfo
        }

        public enum WaitGameState
        {
            Wait,
            Place,
            Action,
            WaitForQueue
        }
        public TileMap tileMap;
        GameObject currentGameCharacter;
        GameObject targetGameCharacter;
        public GameObject DisplayVictory;
        public GameObject characterStatsPanel;
        public CharacterStatsPanel statsPanel;
        public GameObject actionPanel;
        public AudioSource battlesong;
        public Button attackButton;
        public Button abilityButton;
        public Button moveButton;
        public int player1SpawnXStart;
        public int player1SpawnXEnd;
        public int player1SpawnYStart;
        public int player1SpawnYEnd;
        public int player2SpawnXStart;
        public int player2SpawnXEnd;
        public int player2SpawnYStart;
        public int player2SpawnYEnd;
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
        List<Character> characterList = new List<Character>();
        List<GameObject> myCharacters = new List<GameObject>();
        List<GameObject> enemyCharacters = new List<GameObject>();
        //Character character;
        //Character targetCharacter;
        Action action = Action.IDLE;
        WaitGameState waitGameState = WaitGameState.Place;
        bool isWalking;
        bool isCharacter;
        bool reachedPosition = true;
        bool lifeLost = false;
        bool arraySet = false;
        bool placeCharacterPhase = false;
        bool hidePanel = true;
        string selectedAbility;
        int count = 0;
        int idCount = 1;
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
            //GlobalConstants.myPlayerId = 1;
            Debug.Log(GlobalConstants._dbConnection);
            battlesong.playOnAwake = true;
            placeCharacterPhase = true;
            characters = GlobalConstants.MatchCharacters;
            statsPanel.charName.text = characters[0].CharacterClassObject.Name;
            statsPanel.hp.text = characters[0].CharacterClassObject.CurrentStats.HitPoints + " / " + characters[0].CharacterClassObject.CurrentStats.HitPoints;
            statsPanel.mp.text = characters[0].CharacterClassObject.CurrentStats.MagicPoints + " / " + characters[0].CharacterClassObject.CurrentStats.MagicPoints;
            tileMap.addHighlightObjects();
            tileArray = tileMap.tileArray;            
            highlightSpawn();
            arraySet = true;
        }

        

        void Update()
        {
            if(action == Action.WaitForGameInfo)
            {
                //Append Characters to player # character on global constants
                if (GlobalConstants.Updated)
                {
                    GlobalConstants.Updated = false;
                    if (waitGameState == WaitGameState.Place)
                    {                        
                        if (GlobalConstants.player1Characters.Count > 0 && GlobalConstants.player2Characters.Count > 0)
                        {
                            placeCharacterPhase = false;
                            //Debug.Log("Player 1 list: " + GlobalConstants.player1Characters.Count);
                            //Debug.Log("Player 2 list: " + GlobalConstants.player2Characters.Count);
                            //Debug.Log("All Characters placed");
                            if (GlobalConstants.myPlayerId == 1)
                            {
                                PlaceEnemyCharacters(GlobalConstants.player2Characters);
                                CreateTurnQueue();
                                GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
                            }
                            else
                            {
                                PlaceEnemyCharacters(GlobalConstants.player1Characters);
                                waitGameState = WaitGameState.WaitForQueue;                                               
                            }
                            //PlaceEnemyCharacters();                            
                        }
                    }
                    if (waitGameState == WaitGameState.WaitForQueue)
                    {
                        Debug.Log("Waiting for queue");
                        Debug.Log(GlobalConstants.currentActions.CharacterQueue.Count);
                        Debug.Log(GlobalConstants.currentActions.AffectedTiles);
                        Debug.Log(GlobalConstants.currentActions.ActionOrder);
                        if (GlobalConstants.currentActions.CharacterQueue.Count > 0)
                        {
                            Debug.Log("Queue recieved");
                        }
                    }
                }
                else
                {
                    StartCoroutine(WaitForGameInformation());
                }
            }

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
                            if ((unitPlacedCount + 1) == 5 /*|| (unitPlacedCount + 1) == 6*/)
                            {
                                if (GlobalConstants.myPlayerId == 1)
                                {
                                    GlobalConstants.player1Characters = characterList;
                                }
                                else
                                {
                                    GlobalConstants.player2Characters = characterList;
                                    Debug.Log("I was player 2");
                                }                                
                                clearHighlights(validMoves);
                                action = Action.WaitForGameInfo;
                                Debug.Log("all my characters placed");
                                
                                var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.PlaceCharacterUrl, new BattlePostObject());
                                /*if (GlobalConstants.myPlayerId == 2)
                                {
                                    StartCoroutine(WaitForClick("characterload"));

                                    clearHighlights(validMoves);
                                    hidePanel = false;
                                }
                                else
                                {
                                    clearHighlights(validMoves);                                    
                                    GlobalConstants.myPlayerId = 2;
                                    highlightSpawn();
                                    CloneGameCharacter();
                                }*/
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
            int range = 5;
            int rangeConst = 5;
            Tile[,] tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            string weaponType = "";
            if (currentCharacterGameObject.CharacterClassObject.SpriteId == 1)
            {
                weaponType = "isAttacking";
            }
            else if (currentCharacterGameObject.CharacterClassObject.SpriteId == 2)
            {
                weaponType = "isAttackingBow";
            }
            else
            {
                weaponType = "isAttackingSpear";
            }
            if (!currentCharacterGameObject.hasAttacked)
            {
                hidePanel = true;
                validMoves = new List<Tile>();
                //Create Bottom Move Tiles
                if (weaponType == "isAttacking")
                {
                        if (tileY + 1 < tileArray.GetLength(1))
                        {
                            Tile tempTile = tileArray[tileX, tileY + 1];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));

                            // GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    
                    //Create Top Move Tiles

                        if (tileY - 1 >= 0)
                        {
                            Tile tempTile = tileArray[tileX, tileY - 1];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }

                    //Create Left Move Tiles
                        if (tileX - 1 >= 0)
                        {
                            Tile tempTile = tileArray[tileX - 1, tileY];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                    //Create Right Move Tiles
                        if (tileX + 1 < tileArray.GetLength(0))
                        {
                            Tile tempTile = tileArray[tileX + 1, tileY];
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                            //GameObject temp = (GameObject)Resources.Load(("Prefabs/highlightmove"), typeof(GameObject));
                            //GameObject highlight = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - 1.6f), Quaternion.identity) as GameObject;
                            //highlight.transform.parent = tempTile.transform;
                        }
                }
                if (weaponType == "isAttackingSpear")
                {
                    for (int i = 1; i <= 2; i++)
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
                    for (int i = 1; i <= 2; i++)
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
                    for (int i = 1; i <= 2; i++)
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
                    for (int i = 1; i <= 2; i++)
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
                    }
                }
                if (weaponType == "isAttackingBow")
                {
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
                    range = rangeConst;
                    for (int i = 1; i <= rangeConst; i++)
                    {
                        for (int j = 1; j < range; j++)
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
                        range--;
                    }
                    //Create Top Right Move Tiles
                    range = rangeConst;
                    for (int i = 1; i <= rangeConst; i++)
                    {
                        for (int j = 1; j < range; j++)
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
                        range--;
                    }
                    //Create Bottom Left Move Tiles
                    range = rangeConst;
                    for (int i = 1; i <= rangeConst; i++)
                    {
                        for (int j = 1; j < range; j++)
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
                        range--;
                    }
                    //Create Bottom Left Move Tiles
                    range = rangeConst;
                    for (int i = 1; i <= rangeConst; i++)
                    {
                        for (int j = 1; j < range; j++)
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
                        range--;
                    }
                }
                StartCoroutine(WaitForClick("ability"));

            }
            else
            {
                clearHighlights(validMoves);
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
            string weaponType = "";
            if (currentCharacterGameObject.CharacterClassObject.SpriteId == 1)
            {
                weaponType = "isAttacking";
            }else if (currentCharacterGameObject.CharacterClassObject.SpriteId == 2)
            {
                weaponType = "isAttackingBow";
            }
            else
            {
                weaponType = "isAttackingSpear";
            }
                    
            anim.SetBool(weaponType, true);
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
                StartCoroutine(AttackAnimation(targetTile, ability, weaponType));
            }
            else {
                StartCoroutine(AttackAnimationNothing(weaponType));
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
            //Debug.Log("Damage: " + dmg);
            //Debug.Log("Def: " + def);
            return dmg - (def / 10);
        }

        IEnumerator AttackAnimation(Tile tempTile, string ability, string weaponType)
        {
            yield return new WaitForSeconds(.2f);
            tarAnim.SetBool("isAttacked", true);
            if (weaponType == "isAttackingSpear")
            {
                yield return new WaitForSeconds(.55f);
            }
            if (weaponType == "isAttacking")
            {
                yield return new WaitForSeconds(.3f);
            }
            if (weaponType == "isAttackingBow")
            {
                yield return new WaitForSeconds(.7f);
            }
            anim.SetBool(weaponType, false);
            tarAnim.SetBool("isAttacked", false);
            int damage = 0;
            if (ability != null)
            {
                GameObject temp = (GameObject)Resources.Load((ability), typeof(GameObject));
                Debug.Log(temp);
                GameObject spell = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (tempTile.y * 2);
                spell.transform.parent = tempTile.transform;
                spell.transform.localScale = new Vector3(1, 1, 0.0f);
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
            damage = (damage <= 0) ? 1 : damage;
            dmgObject.GetComponent<Text>().text = damage.ToString();
            targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP -= damage;
            yield return new WaitForSeconds(.4f);
            if (targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP <= 0)
            {
                targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP = 0;
                targetCharacterGameObject.isDead = true;
                //myCharacters.Remove(targetCharacterGameObject.gameObject);
                tarAnim.SetBool("isDead", true);
                yield return new WaitForSeconds(.8f);
                Debug.Log(targetCharacterGameObject.CharacterClassObject.Name);
            }            
            selectedAbility = null;
            hidePanel = false;
        }
        IEnumerator AttackAnimationNothing(string weaponType)
        {
            yield return new WaitForSeconds(.5f);
            anim.SetBool(weaponType, false);
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

                yield return new WaitForSeconds(.2f);
                GameObject particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
                GameObject damageText = (GameObject)Resources.Load(("Prefabs/DamageText"), typeof(GameObject));
                GameObject dmgObject = GameObject.Instantiate(damageText, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y + 3.2f), Quaternion.identity) as GameObject;
                dmgObject.transform.parent = particleCanvas.transform;                
                damage = (damage <= 0) ? 1 : damage;
                dmgObject.GetComponent<Text>().text = damage.ToString();
                targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP -= damage;
                yield return new WaitForSeconds(wait);
                if (targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP <= 0)
                {
                    targetCharacterGameObject.CharacterClassObject.CurrentStats.CurHP = 0;
                    targetCharacterGameObject.isDead = true;
                    //myCharacters.Remove(targetCharacterGameObject.gameObject);
                    tarAnim.SetBool("isDead", true);
                    yield return new WaitForSeconds(.8f);
                    Debug.Log(targetCharacterGameObject.CharacterClassObject.Name);
                    if (targetCharacterGameObject.CharacterClassObject.Name.Equals("Kelly"))
                    {
                        DisplayVictory.SetActive(true);
                        hidePanel = true;
                        yield return new WaitForSeconds(500f);
                    }
                }
            }
            selectedAbility = null;
            hidePanel = false;
        }
        IEnumerator CastAnimationNothing()
        {
            yield return new WaitForSeconds(.5f);
            anim.SetBool("isCasting", false);
            selectedAbility = null;
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
        IEnumerator WaitForGameInformation()
        {
            yield return new WaitForSeconds(4f);
            //var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.CheckGameStatusUrl, new BattlePostObject());
            
        }       

        public void highlightSpawn()
        {
            if (GlobalConstants.myPlayerId == 1)
            {
                for (int i = player1SpawnXStart; i < player1SpawnXEnd; i++)
                {
                    for (int j = player1SpawnYStart; j < player1SpawnYEnd; j++)
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
            }
            else
            {
                for (int i = player2SpawnXStart; i < player2SpawnXEnd; i++)
                {
                    for (int j = player2SpawnYStart; j < player2SpawnYEnd; j++)
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
            int spriteId = gameCharacter.CharacterClassObject.SpriteId;
            GameObject temp = (GameObject)Resources.Load(("Prefabs/Character" + spriteId /*+ characters[unitPlacedCount].CharacterClassObject.SpriteId*/), typeof(GameObject));
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
            gameCharacter.CharacterClassObject.X = tempTile.x;
            gameCharacter.CharacterClassObject.Y = tempTile.y;
            characterList.Add(gameCharacter.CharacterClassObject);
            myCharacters.Add(tempchar);
            if (unitPlacedCount + 1 < characters.Count)
            {
                statsPanel.charName.text = characters[unitPlacedCount + 1].CharacterClassObject.Name;
            }
            idCount++;
        }

        public void PlaceEnemyCharacters(List<Character> enemyList)
        {
            foreach(Character c in enemyList)
            {
                Tile tempTile = tileArray[c.X, c.Y];
                tempTile.isOccupied = true;
                CharacterGameObject gameCharacter = new CharacterGameObject();
                gameCharacter.Initialize(c, c.X, c.Y);
                tempTile.character = gameCharacter;
                int spriteId = gameCharacter.CharacterClassObject.SpriteId;
                GameObject temp = (GameObject)Resources.Load(("Prefabs/Character" + spriteId /*+ characters[unitPlacedCount].CharacterClassObject.SpriteId*/), typeof(GameObject));
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
                gameCharacter.CharacterClassObject.X = tempTile.x;
                gameCharacter.CharacterClassObject.Y = tempTile.y;
                enemyCharacters.Add(tempchar);
            }
        }

        public void CreateTurnQueue()
        {
            for(int i = 0; i < myCharacters.Count; i++)
            {
                turnQueue.Add(myCharacters[i]);
                turnQueue.Add(enemyCharacters[i]);
                GlobalConstants.currentActions.CharacterQueue.Add(myCharacters[i].GetComponent<CharacterGameObject>().CharacterClassObject.CharacterId);
                GlobalConstants.currentActions.CharacterQueue.Add(enemyCharacters[i].GetComponent<CharacterGameObject>().CharacterClassObject.CharacterId);
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
            Debug.Log(currentCharacterGameObject.CharacterClassObject.CharacterId);
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

        public void CloneGameCharacter()
        {
            List<CharacterGameObject> cgo = new List<CharacterGameObject>();
            foreach(CharacterGameObject c in gCharacters)
            {
                cgo.Add(c.cloneCharacter(c));
            }
            gCharacters = cgo;
        }
        //TODO
        //AddAction()
        //CheckAction()
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine.SceneManagement;
using System.Linq;
using Animator = UnityEngine.Animator;
using System;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public enum Action
        {
            Idle,
            Move,
            Attack,
            AttackAbility,
            CastAbility,
            Occupy,
            WaitForGameInfo,
            View,
            Victory,
            Defeat

        }

        public enum WaitGameState
        {
            Wait,
            Place,
            Action,
            WaitForQueue,
            WaitForOtherPlayer
        }
        public TileMap tileMap;
        GameObject currentGameCharacter;
        GameObject targetGameCharacter;
        public GameObject characterStatsPanel;
        public GameObject selectedCharcterStatsPanel;
        public CharacterStatsPanel statsPanel;
        public CharacterStatsPanel selectedCharcterStats;
        public AbilityList abilityList;
        public GameObject actionPanel;
        public Battle battle;
        public AudioSource battlesong;
        public AudioSource funsong;
        public AudioSource defeatsong;
        public AudioSource victorysong;
        public AudioSource placecharactersong;
        public Text playersTurnText;
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
        UnityEngine.Animator anim;
        UnityEngine.Animator tarAnim;
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
        public List<GameObject> myCharacters = new List<GameObject>();
        public List<GameObject> enemyCharacters = new List<GameObject>();
        //Character character;
        //Character targetCharacter;
        Action action = Action.Idle;
        WaitGameState waitGameState = WaitGameState.Place;
        bool isWalking;
        bool isCharacter;
        bool reachedPosition = true;
        bool lifeLost = false;
        bool arraySet = false;
        bool placeCharacterPhase = false;
        bool viewMode = false;
        bool hidePanel = true;
        bool endcalled = false;
        string selectedAbility;
        int count = 0;
        int idCount = 1;
        int unitPlacedCount = 0;
        List<Tile> walkableTiles = new List<Tile>();
        List<Tile> validMoves = new List<Tile>();
        List<Tile> path = new List<Tile>();
        List<GameObject> turnQueue = new List<GameObject>();
        public ActionAnimator ActionAnimator;

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
            //Debug.Log(GlobalConstants._dbConnection);
            GlobalConstants.ActionAnimator = ActionAnimator;
            GlobalConstants.GameController = this;
            placecharactersong.playOnAwake = true;
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
            if (action == Action.WaitForGameInfo)
            {
                //Append Characters to player # character on global constants
                if (GlobalConstants.currentActions.CharacterQueue.Count > 0 && waitGameState == WaitGameState.WaitForQueue)
                {
                    GlobalConstants.Updated = true;
                }
                if (GlobalConstants.Updated)
                {
                    Debug.Log("Game was updated");
                    GlobalConstants.Updated = false;
                    if (waitGameState == WaitGameState.Place)
                    {
                        if (GlobalConstants.player1Characters.Count > 0 && GlobalConstants.player2Characters.Count > 0)
                        {
                            //placeCharacterPhase = false;
                            //Debug.Log("Player 1 list: " + GlobalConstants.player1Characters.Count);
                            //Debug.Log("Player 2 list: " + GlobalConstants.player2Characters.Count);
                            //Debug.Log("All Characters placed");
                            if (GlobalConstants.myPlayerId == 1)
                            {
                                PlaceEnemyCharacters(GlobalConstants.player2Characters);
                                CreateTurnQueue();
                                GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
                                waitGameState = WaitGameState.Wait;
                                Debug.Log("Selecting next character being called");
                                StartCoroutine(WaitSecToStart());
                                
                            }
                            else
                            {
                                PlaceEnemyCharacters(GlobalConstants.player1Characters);
                                if (GlobalConstants.currentActions.CharacterQueue.Count > 0)
                                {
                                    CreateTurnQueueP2();
                                }
                                else
                                {
                                    waitGameState = WaitGameState.WaitForQueue;
                                }
                            }
                            //PlaceEnemyCharacters();                            
                        }
                    }
                    if (waitGameState == WaitGameState.WaitForQueue)
                    {
                        //Debug.Log("Waiting for queue");
                        //Debug.Log(GlobalConstants.currentActions.CharacterQueue.Count);
                        //Debug.Log(GlobalConstants.currentActions.AffectedTiles);
                        //Debug.Log(GlobalConstants.currentActions.ActionOrder);
                        if (GlobalConstants.currentActions.CharacterQueue.Count > 0)
                        {
                            //Debug.Log("Queue recieved");
                            CreateTurnQueueP2();
                        }
                    }
                    if (waitGameState == WaitGameState.Wait)
                    {
                        foreach (var act in GlobalConstants.currentActions.ActionOrder)
                        {
                            //Debug.Log(GlobalConstants.currentActions.ActionOrder.Count);
                            if (act.ActionType == ActionType.Move && !currentCharacterGameObject.hasMoved)
                            {
                                if (act.ActionTiles.Count > 0)
                                {
                                    Debug.Log("Move Called");
                                    tile = tileArray[act.ActionTiles[0].x, act.ActionTiles[0].y];
                                    prevTile = tile;
                                    currentCharacterGameObject.X = tile.x;
                                    currentCharacterGameObject.Y = tile.y;
                                    for (var j = 0; j < act.ActionTiles.Count; j++)
                                    {
                                        path.Add(tileArray[act.ActionTiles[j].x, act.ActionTiles[j].y]);
                                    }
                                    prevTile.isOccupied = false;
                                    prevTile.characterObject = null;
                                    prevTile.character = null;
                                    targetTile = path[0];
                                    reachedPosition = false;
                                    GlobalConstants.isAnimating = true;
                                    currentCharacterGameObject.hasMoved = true;
                                    break;
                                }
                            }

                            if (act.ActionType == ActionType.CastAbility && !currentCharacterGameObject.hasAttacked)
                            {
                                Debug.Log("Cast Ability P2 Called");
                                if (act.ActionTiles.Count > 0)
                                {
                                    Debug.Log("Cast Called");
                                    foreach (var t in act.ActionTiles)
                                    {
                                        Debug.Log("X: " + t.x + " Y: " + t.y);
                                        GlobalConstants.isAnimating = true;
                                        var tempTile = tileArray[t.x, t.y];
                                        GetTarget(tempTile);
                                        clearHighlights(validMoves);
                                        //Cast(tempTile, selectedAbility);
                                        GameClasses.Action gameAction = null;
                                        var actionType = GlobalConstants.EffectTypes.SingleOrDefault(ability => ability.Name.Equals(act.PerformedAction));
                                        if (actionType != null)
                                        {
                                            gameAction = (GameClasses.Action)Activator.CreateInstance(actionType);
                                        }
                                        //Dictionary<CharacterGameObject, Tile> effectedCharacterDictionary = new Dictionary<CharacterGameObject, Tile>();
                                        //effectedCharacterDictionary.Add(targetCharacterGameObject, tempTile);
                                        var effectedTiles = new List<Tile> { tempTile };
                                        gameAction.Initialize(ref effectedTiles, ref currentCharacterGameObject, ref targetTile);
                                        gameAction.Execute();
                                        currentCharacterGameObject.hasAttacked = true;
                                        break;
                                    }
                                }
                            }

                            if (act.ActionType == ActionType.AttackAbility && !currentCharacterGameObject.hasAttacked)
                            {
                                foreach (var t in act.ActionTiles)
                                {
                                    Debug.Log("AttackType Called");
                                    hidePanel = true;
                                    var tempTile = tileArray[t.x, t.y];
                                    GetTarget(tempTile);
                                    clearHighlights(validMoves);
                                    GameClasses.Action gameAction = null;
                                    GlobalConstants.isAnimating = true;
                                    selectedAbility = act.PerformedAction;
                                    var actionType = GlobalConstants.EffectTypes.SingleOrDefault(ability => ability.Name.Equals(selectedAbility));
                                    if (actionType != null)
                                    {
                                        gameAction = (GameClasses.Action)Activator.CreateInstance(actionType);
                                    }
                                    else
                                    {
                                        selectedAbility = "attack";
                                        actionType = GlobalConstants.EffectTypes.SingleOrDefault(ability => ability.Name.Equals("Attack"));
                                        gameAction = (GameClasses.Action)Activator.CreateInstance(actionType);
                                    }
                                    //Dictionary<CharacterGameObject, Tile> effectedCharacterDictionary = new Dictionary<CharacterGameObject, Tile>();
                                    //effectedCharacterDictionary.Add(targetCharacterGameObject, tempTile);
                                    //gameAction.Initialize(ref effectedCharacterDictionary, ref currentCharacterGameObject, ref targetTile);
                                    var effectedTiles = new List<Tile> { tempTile };
                                    gameAction.Initialize(ref effectedTiles, ref currentCharacterGameObject, ref targetTile);
                                    gameAction.Execute();
                                    currentCharacterGameObject.hasAttacked = true;
                                    break;
                                }
                                //Debug.Log(act.actionType);                                
                            }
                            if (act.ActionType == ActionType.Endturn)
                            {
                                //GameClasses.Action tempAction = new GameClasses.Action(GameClasses.Action.ActionType.Reset, new List<Tile>(), "reset");
                                //GlobalConstants.currentActions.AddAction(tempAction);                                
                                StartCoroutine(WaitForPlayer1());
                                break;
                            }
                        }

                    }

                    //Debug.Log("waiting for other player");
                    //Debug.Log(GlobalConstants.currentActions.ActionOrder.Count);

                }
                if (waitGameState == WaitGameState.WaitForOtherPlayer)
                {
                    //Debug.Log(GlobalConstants.currentActions.ActionOrder.Count);
                    if (GlobalConstants.currentActions.ActionOrder.Count == 1)
                    {
                        Debug.Log(GlobalConstants.currentActions.ActionOrder[0].ActionType);
                    }

                    if (GlobalConstants.currentActions.ActionOrder.Count == 0)
                    {
                        Debug.Log("End Turn Called by Current Player");
                        waitGameState = WaitGameState.Wait;
                        //Debug.Log("Selecting next character");
                        SelectNextCharacter();
                    }
                }
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

                var currentX = (float)(System.Math.Round(currentCharacterGameObject.transform.localPosition.x, 2));
                var currentY = (float)(System.Math.Round(currentCharacterGameObject.transform.localPosition.y, 2));
                var targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
                var targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
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
                        GlobalConstants.isAnimating = false;
                        path.Clear();
                        count = 0;
                        currentCharacterGameObject.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
                        targetTile.isOccupied = true;
                        currentCharacterGameObject.X = targetTile.x;
                        currentCharacterGameObject.X = targetTile.y;
                        turnQueue[0].GetComponent<CharacterGameObject>().X = targetTile.x;
                        turnQueue[0].GetComponent<CharacterGameObject>().Y = targetTile.y;
                        targetTile.character = turnQueue[0].GetComponent<CharacterGameObject>();
                        targetTile.characterObject = turnQueue[0];
                        PositionPanels();
                        if (action != Action.WaitForGameInfo)
                        {
                            //Debug.Log("Show Panels called");
                            action = Action.Idle;
                            targetTile.isAlly = true;
                            hidePanel = false;
                        }
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
                    if (action == Action.Move)
                    {
                        Move();
                    }
                    if (action == Action.AttackAbility)
                    {
                        if (hit.collider != null)
                        {
                            Debug.Log("Attack Ability Called");
                            attackButton.interactable = false;
                            abilityButton.interactable = false;
                            var tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                hidePanel = true;
                                GetTarget(tempTile);
                                clearHighlights(validMoves);
                                GameClasses.Action gameAction = null;
                                GlobalConstants.isAnimating = true;
                                var actionType = GlobalConstants.EffectTypes.SingleOrDefault(ability => ability.Name.Equals(selectedAbility));
                                if (actionType != null)
                                {
                                    gameAction = (GameClasses.Action)Activator.CreateInstance(actionType);
                                }
                                else
                                {
                                    selectedAbility = "attack";
                                    actionType = GlobalConstants.EffectTypes.SingleOrDefault(ability => ability.Name.Equals("Attack"));
                                    gameAction = (GameClasses.Action)Activator.CreateInstance(actionType);
                                }
                                //Dictionary<CharacterGameObject, Tile> effectedCharacterDictionary = new Dictionary<CharacterGameObject, Tile>();
                                Debug.Log(currentCharacterGameObject.CharacterClassObject.Equipment.Weapon);
                                //effectedCharacterDictionary.Add(targetCharacterGameObject.GetComponent<CharacterGameObject>(), tempTile);
                                //gameAction.Initialize(ref effectedCharacterDictionary, ref currentCharacterGameObject, ref tile);
                                var effectedTiles = new List<Tile> { tempTile };
                                gameAction.Initialize(ref effectedTiles, ref currentCharacterGameObject, ref targetTile);
                                gameAction.Execute();
                                GlobalConstants.currentActions.AddAction(new GameClasses.Action(ActionType.AttackAbility, new List<Tile>() { tempTile }, selectedAbility));
                                Debug.Log(GlobalConstants.currentActions.ActionOrder[0].ActionType + " " + GlobalConstants.currentActions.ActionOrder[0].PerformedAction);
                                GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
                                currentCharacterGameObject.hasAttacked = true;
                                /*Attack(tempTile, selectedAbility);
                                currentCharacterGameObject.hasAttacked = true;*/
                            }
                        }
                    }
                    if (action == Action.CastAbility)
                    {
                        if (hit.collider != null)
                        {
                            var tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                attackButton.interactable = false;
                                abilityButton.interactable = false;
                                hidePanel = true;
                                GetTarget(tempTile);
                                clearHighlights(validMoves);
                                //Cast(tempTile, selectedAbility);
                                GameClasses.Action gameAction = null;
                                GlobalConstants.isAnimating = true;
                                var actionType = GlobalConstants.EffectTypes.SingleOrDefault(ability => ability.Name.Equals(selectedAbility));
                                if (actionType != null)
                                {
                                    gameAction = (GameClasses.Action)Activator.CreateInstance(actionType);
                                }
                                //Dictionary<CharacterGameObject, Tile> effectedCharacterDictionary = new Dictionary<CharacterGameObject, Tile>();
                                //effectedCharacterDictionary.Add(targetCharacterGameObject.GetComponent<CharacterGameObject>(), tempTile);
                                //gameAction.Initialize(ref effectedCharacterDictionary, ref currentCharacterGameObject, ref tile);
                                var effectedTiles = new List<Tile> { tempTile };
                                gameAction.Initialize(ref effectedTiles, ref currentCharacterGameObject, ref targetTile);
                                gameAction.Execute();
                                GlobalConstants.currentActions.AddAction(new GameClasses.Action(ActionType.CastAbility, new List<Tile>() { tempTile }, selectedAbility));
                                GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
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

                        var tempTile = hit.collider.gameObject.GetComponent<Tile>();
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
                                    //Debug.Log("I was player 2");
                                }
                                clearHighlights(validMoves);
                                action = Action.WaitForGameInfo;
                                //Debug.Log("all my characters placed");

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

                        var tempTile = hit.collider.gameObject.GetComponent<Tile>();
                        tempTile.isOccupied = true;
                        hidePanel = false;
                        action = Action.Idle;
                    }
                }

                if (Input.GetMouseButtonUp(0) && (action == Action.Idle || action == Action.View) && viewMode)
                {
                    if (hit.collider != null)
                    {

                        var tempTile = hit.collider.gameObject.GetComponent<Tile>();

                        if (null != tempTile.characterObject)
                        {
                            var c = tempTile.characterObject.GetComponent<CharacterGameObject>();
                            selectedCharcterStatsPanel.SetActive(true);
                            selectedCharcterStats.charName.text = c.CharacterClassObject.Name;
                            selectedCharcterStats.hp.text = c.CharacterClassObject.CurrentStats.CurHP.ToString() + "/" + c.CharacterClassObject.CurrentStats.HitPoints.ToString();
                            selectedCharcterStats.mp.text = c.CharacterClassObject.CurrentStats.CurMP.ToString() + "/" + c.CharacterClassObject.CurrentStats.MagicPoints.ToString();
                            PositionSelectedPanel(c, tempTile);
                            //characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x, currentCharacterGameObject.transform.position.y + 8, 0);
                        }
                        else
                        {
                            selectedCharcterStatsPanel.SetActive(false);
                        }
                    }
                }

                if (Input.GetKeyDown("escape") && action != Action.WaitForGameInfo && !placeCharacterPhase)
                {
                    //Debug.Log("Escape key called");
                    if (action == Action.Idle)
                    {
                        viewMode = true;
                        hidePanel = true;
                        action = Action.View;
                    }
                    else
                    {
                        hidePanel = false;
                        viewMode = false;
                        selectedCharcterStatsPanel.SetActive(false);
                        clearHighlights(validMoves);
                        action = Action.Idle;
                    }
                }

                if (Input.GetKeyDown("m") && action != Action.WaitForGameInfo && !placeCharacterPhase)
                {
                    //Debug.Log("Escape key called");
                    abilityList.ShowAllAbilities();
                }
                if (Input.GetKeyDown("j") && action != Action.WaitForGameInfo && !placeCharacterPhase)
                {
                    //Debug.Log("Escape key called");
                    abilityList.ShowSuperAbilities();
                }

                if (action == Action.Victory && !endcalled)
                {
                    Debug.Log("YOU WIN");
                    endcalled = true;
                    StartCoroutine(DisplayVictory());
                }
                if (action == Action.Defeat && !endcalled)
                {
                    Debug.Log("YOU LOSE");
                    endcalled = true;
                    StartCoroutine(DisplayDefeat());
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
                var tempTile = targetTile;
                if (tempTile.isValidMove && !targetTile.isAlly && !targetTile.isDead)
                {
                    moveButton.interactable = false;
                    currentCharacterGameObject.hasMoved = true;
                    hidePanel = true;
                    tile = tempTile;
                    prevTile = lastTile;
                    currentCharacterGameObject.X = tile.x;
                    currentCharacterGameObject.Y = tile.y;
                    path = buildPath(prevTile, tile);
                    prevTile.isOccupied = false;
                    prevTile.isAlly = false;
                    prevTile.characterObject = null;
                    prevTile.character = null;
                    targetTile = path[0];
                    reachedPosition = false;
                    GlobalConstants.isAnimating = true;
                    clearHighlights(validMoves);
                    currentCharacterGameObject.hasMoved = true;
                    var tempAction = new GameClasses.Action(ActionType.Move, path, "move");
                    GlobalConstants.currentActions.AddAction(tempAction);
                    GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
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
                ShowAttackMoves(ability);                

            }
        }

        public void CastAbility(string ability)
        {
            if (!currentCharacterGameObject.hasAttacked)
            {
                showCastMoves(ability);                
            }
        }

        public void showMoves()
        {
            //FindPossibleMoves(tile);
            foreach (var t in walkableTiles)
            {
                t.isValidMove = true;
                validMoves.Add(t);
                if (!t.isAlly && !t.isDead)
                {
                    t.highlight.GetComponent<Image>().color = new Color32(99, 178, 255, 165);
                    t.highlight.SetActive(true);                    
                    
                }
            }
            hidePanel = true;
            StartCoroutine(WaitForClick("move"));
        }

        public List<Tile> buildPath(Tile start, Tile end)
        {
            var currentX = start.x;
            var currentY = start.y;
            var endX = end.x;
            var endY = end.y;
            var checkCount = 0;
            var moveVal = Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
            var pathFound = false;
            var openPath = new List<List<Tile>>();
            var curPathList = new List<List<Tile>>();
            var openPathClone = new List<Tile>();
            var movePath = new List<Tile>();
            walkableTiles = new List<Tile>();
            var moves = currentCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
            movePath.Add(start);
            openPath.Add(movePath);
            while (!pathFound)
            {

                foreach (var lt in openPath)
                {
                    openPathClone = new List<Tile>();
                    var t = lt[lt.Count - 1];
                    var tempList = GetPossiblePaths(t, false);
                    foreach (var til in tempList)
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

            var tempList = new List<Tile>();
            if (t.y - 1 >= 0)
            {
                if ((tileArray[t.x, t.y - 1].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x, t.y - 1]) && (!tileArray[t.x, t.y - 1].isOccupied || tileArray[t.x, t.y - 1].isAlly || tileArray[t.x, t.y - 1].isDead))
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x, t.y - 1]);
                }
            }
            if (t.y + 1 < tileMap.yLength)
            {
                if ((tileArray[t.x, t.y + 1].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x, t.y + 1]) && (!tileArray[t.x, t.y + 1].isOccupied || tileArray[t.x, t.y + 1].isAlly || tileArray[t.x, t.y + 1].isDead))
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x, t.y + 1]);
                }
            }
            if (t.x - 1 >= 0)
            {
                if ((tileArray[t.x - 1, t.y].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x - 1, t.y]) && (!tileArray[t.x - 1, t.y].isOccupied || tileArray[t.x - 1, t.y].isAlly || tileArray[t.x - 1, t.y].isDead))
                {
                    //Debug.Log(t);
                    tempList.Add(tileArray[t.x - 1, t.y]);
                }
            }
            if (t.x + 1 < tileMap.xLength)
            {
                if ((tileArray[t.x + 1, t.y].isValidMove || isPathSearch) && !walkableTiles.Contains(tileArray[t.x + 1, t.y]) && (!tileArray[t.x + 1, t.y].isOccupied || tileArray[t.x + 1, t.y].isAlly || tileArray[t.x + 1, t.y].isDead))
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
            var move = currentCharacterGameObject.CharacterClassObject.CurrentStats.Speed;
            var countCheck = 0;
            var openPath = new List<Tile>();
            var openPathTemp = new List<Tile>();
            walkableTiles = new List<Tile>();
            openPath.Add(tile);
            for (var i = 0; i < move; i++)
            {
                openPathTemp = new List<Tile>();
                foreach (var t in openPath)
                {
                    var temp = GetPossiblePaths(t, true);
                    foreach (var til in temp)
                    {
                        countCheck++;
                        openPathTemp.Add(til);
                        walkableTiles.Add(til);   
                    }
                }
                openPath = new List<Tile>(openPathTemp);

            }
        }

        public void showCastMoves(string ability)
        {
            // int currentX = currentCharacter.x;
            //  int currentY = currentCharacter.y;
            var tileX = tile.x;
            var tileY = tile.y;
            var tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            //tileArray[1, 0].isObstructed = true;
            selectedAbility = ability;
            var range = 4;
            if (!currentCharacterGameObject.hasAttacked)
            {
                if (action == Action.Idle)
                {
                    hidePanel = true;
                    validMoves = new List<Tile>();
                    //Create Bottom Move Tiles
                    var startTile = tileArray[tileX, tileY];
                    startTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                    startTile.highlight.SetActive(true);
                    startTile.isValidMove = true;
                    validMoves.Add(startTile);

                    for (var i = 1; i <= range; i++)
                    {

                        if (tileY + i < tileArray.GetLength(1))
                        {
                            var tempTile = tileArray[tileX, tileY + i];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                    for (var i = 1; i <= range; i++)
                    {
                        if (tileY - i >= 0)
                        {
                            var tempTile = tileArray[tileX, tileY - i];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                    for (var i = 1; i <= range; i++)
                    {
                        if (tileX - i >= 0)
                        {
                            var tempTile = tileArray[tileX - i, tileY];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                    for (var i = 1; i <= range; i++)
                    {
                        if (tileX + i < tileArray.GetLength(0))
                        {
                            var tempTile = tileArray[tileX + i, tileY];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                    range = 4;
                    var tempRange = range;
                    for (var i = 1; i < range; i++)
                    {
                        for (var j = 1; j < tempRange; j++)
                        {

                            if (tileX - i >= 0 && tileY - j >= 0)
                            {
                                var tempTile = tileArray[tileX - i, tileY - j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    //Create Top Right Move Tiles
                    range = 4;
                    tempRange = range;
                    for (var i = 1; i <= range; i++)
                    {
                        for (var j = 1; j < tempRange; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY - j >= 0)
                            {
                                var tempTile = tileArray[tileX + i, tileY - j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    //Create Bottom Left Move Tiles
                    range = 4;
                    tempRange = range;
                    for (var i = 1; i <= range; i++)
                    {
                        for (var j = 1; j < tempRange; j++)
                        {
                            if (tileX - i >= 0 && tileY + j < tileArray.GetLength(1))
                            {
                                var tempTile = tileArray[tileX - i, tileY + j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
                        tempRange--;
                    }
                    //Create Bottom Left Move Tiles
                    range = 4;
                    tempRange = range;
                    for (var i = 1; i <= range; i++)
                    {
                        for (var j = 1; j < tempRange; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY + j < tileArray.GetLength(1))
                            {
                                var tempTile = tileArray[tileX + i, tileY + j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
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
            if (action != Action.WaitForGameInfo)
            {
                action = Action.Idle;
            }
            foreach (var tile in tiles)
            {
                tile.highlight.SetActive(false);
                tile.isValidMove = false;
            }
            tiles.Clear();
        }
        public bool checkMove(Tile tile)
        {
            var currentX = currentCharacterGameObject.X;
            var currentY = currentCharacterGameObject.Y;
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
            var tileX = tile.x;
            var tileY = tile.y;
            var range = 5;
            var rangeConst = 5;
            var tileArray = tileMap.tileArray;
            clearHighlights(validMoves);
            var weaponId = currentCharacterGameObject.CharacterClassObject.Equipment.Weapon.ItemId;
            var weaponType = "";
            selectedAbility = type;
            if (weaponId == 0 || (weaponId >= 8000 && weaponId < 9000))
            {
                weaponType = "isAttacking";
            }
            else if (weaponId >= 10000)
            {
                weaponType = "isAttackingBow";
            }
            else if (weaponId >= 9000 && weaponId < 10000)
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
                        var tempTile = tileArray[tileX, tileY + 1];
                        if (!tempTile.isObstructed)
                        {
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                        }
                    }

                    //Create Top Move Tiles

                    if (tileY - 1 >= 0)
                    {
                        var tempTile = tileArray[tileX, tileY - 1];
                        if (!tempTile.isObstructed)
                        {
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                        }
                    }

                    //Create Left Move Tiles
                    if (tileX - 1 >= 0)
                    {
                        var tempTile = tileArray[tileX - 1, tileY];
                        if (!tempTile.isObstructed)
                        {
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                        }
                    }
                    //Create Right Move Tiles
                    if (tileX + 1 < tileArray.GetLength(0))
                    {
                        var tempTile = tileArray[tileX + 1, tileY];
                        if (!tempTile.isObstructed)
                        {
                            tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                            tempTile.highlight.SetActive(true);
                            tempTile.isValidMove = true;
                            validMoves.Add(tempTile);
                        }
                    }
                }
                if (weaponType == "isAttackingSpear")
                {
                    for (var i = 1; i <= 2; i++)
                    {

                        if (tileY + i < tileArray.GetLength(1))
                        {
                            var tempTile = tileArray[tileX, tileY + i];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                    //Create Top Move Tiles
                    for (var i = 1; i <= 2; i++)
                    {
                        if (tileY - i >= 0)
                        {
                            var tempTile = tileArray[tileX, tileY - i];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                    //Create Left Move Tiles
                    for (var i = 1; i <= 2; i++)
                    {
                        if (tileX - i >= 0)
                        {
                            var tempTile = tileArray[tileX - i, tileY];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                    //Create Right Move Tiles
                    for (var i = 1; i <= 2; i++)
                    {
                        if (tileX + i < tileArray.GetLength(0))
                        {
                            var tempTile = tileArray[tileX + i, tileY];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                }
                if (weaponType == "isAttackingBow")
                {
                    for (var i = 1; i <= range; i++)
                    {

                        if (tileY + i < tileArray.GetLength(1))
                        {
                            var tempTile = tileArray[tileX, tileY + i];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                    //Create Top Move Tiles
                    for (var i = 1; i <= range; i++)
                    {
                        if (tileY - i >= 0)
                        {
                            var tempTile = tileArray[tileX, tileY - i];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                    //Create Left Move Tiles
                    for (var i = 1; i <= range; i++)
                    {
                        if (tileX - i >= 0)
                        {
                            var tempTile = tileArray[tileX - i, tileY];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                    }
                    //Create Right Move Tiles
                    for (var i = 1; i <= range; i++)
                    {
                        if (tileX + i < tileArray.GetLength(0))
                        {
                            var tempTile = tileArray[tileX + i, tileY];
                            if (!tempTile.isObstructed)
                            {
                                tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                tempTile.highlight.SetActive(true);
                                tempTile.isValidMove = true;
                                validMoves.Add(tempTile);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    //Create Top Left Move Tiles
                    range = rangeConst;
                    for (var i = 1; i <= rangeConst; i++)
                    {
                        for (var j = 1; j < range; j++)
                        {

                            if (tileX - i >= 0 && tileY - j >= 0)
                            {
                                var tempTile = tileArray[tileX - i, tileY - j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                    tempTile.highlight.SetActive(true);
                                    tempTile.isValidMove = true;
                                    validMoves.Add(tempTile);
                                }
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
                    for (var i = 1; i <= rangeConst; i++)
                    {
                        for (var j = 1; j < range; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY - j >= 0)
                            {
                                var tempTile = tileArray[tileX + i, tileY - j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                    tempTile.highlight.SetActive(true);
                                    tempTile.isValidMove = true;
                                    validMoves.Add(tempTile);
                                }
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
                    for (var i = 1; i <= rangeConst; i++)
                    {
                        for (var j = 1; j < range; j++)
                        {
                            if (tileX - i >= 0 && tileY + j < tileArray.GetLength(1))
                            {
                                var tempTile = tileArray[tileX - i, tileY + j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                    tempTile.highlight.SetActive(true);
                                    tempTile.isValidMove = true;
                                    validMoves.Add(tempTile);
                                }
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
                    for (var i = 1; i <= rangeConst; i++)
                    {
                        for (var j = 1; j < range; j++)
                        {
                            if (tileX + i < tileArray.GetLength(0) && tileY + j < tileArray.GetLength(1))
                            {
                                var tempTile = tileArray[tileX + i, tileY + j];
                                if (!tempTile.isObstructed)
                                {
                                    tempTile.highlight.GetComponent<Image>().color = new Color32(255, 32, 32, 165);
                                    tempTile.highlight.SetActive(true);
                                    tempTile.isValidMove = true;
                                    validMoves.Add(tempTile);
                                }
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
                tarAnim = tile.characterObject.GetComponent<UnityEngine.Animator>();
                targetCharacterGameObject = tile.characterObject.GetComponent<CharacterGameObject>();
            }
            //targetCharacterGameObject = tile.GetComponent<CharacterGameObject>();
            //targetCharacterGameObject = targetCharacterGameObject.transform.parent.gameObject;

        }

        public CharacterGameObject GetCharacterGameObject(Tile tile)
        {
            return tile.isOccupied ? tile.character : null;
        }

        public Animator GetAnimator(Tile tile)
        {
            if (tile.characterObject == null)
                return null;
            return tile.isOccupied ? tile.characterObject.GetComponent<UnityEngine.Animator>() : null;
        }

        public void Cast(Tile targetTile, string ability)
        {
            action = Action.Idle;
            anim.SetBool("isCasting", true);
            var currentX = (float)(System.Math.Round(tile.transform.localPosition.x, 2));
            var currentY = (float)(System.Math.Round(tile.transform.localPosition.y, 2));
            var targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
            var targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
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
            action = Action.Idle;
            var weaponType = "";
            if (currentCharacterGameObject.CharacterClassObject.SpriteId == 1)
            {
                weaponType = "isAttacking";
            } else if (currentCharacterGameObject.CharacterClassObject.SpriteId == 2)
            {
                weaponType = "isAttackingBow";
            }
            else
            {
                weaponType = "isAttackingSpear";
            }

            anim.SetBool(weaponType, true);
            var currentX = (float)(System.Math.Round(tile.transform.localPosition.x, 2));
            var currentY = (float)(System.Math.Round(tile.transform.localPosition.y, 2));
            var targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
            var targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
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
            tarAnim = targetCharacterGameObject.GetComponent<UnityEngine.Animator>();
            tarAnim.SetBool("isAttacked", true);
            StartCoroutine("InjuredAnimation");
        }

        int CalculateMagicDamage(string ability)
        {
            var mDmg = currentCharacterGameObject.CharacterClassObject.CurrentStats.MagicAttack;
            var mDef = targetCharacterGameObject.CharacterClassObject.CurrentStats.MagicDefense;
            if (ability == "fire")
            {
                mDmg = mDmg * 2;
            }
            return mDmg - mDef;
        }

        int CalculateDamage()
        {
            var dmg = currentCharacterGameObject.CharacterClassObject.CurrentStats.Dmg;
            var def = targetCharacterGameObject.CharacterClassObject.CurrentStats.Defense;
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
            var damage = 0;
            if (ability != null)
            {
                var temp = (GameObject)Resources.Load((ability), typeof(GameObject));
                //Debug.Log(temp);
                var spell = GameObject.Instantiate(temp.gameObject, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (tempTile.y * 2);
                spell.transform.parent = tempTile.transform;
                spell.transform.localScale = new Vector3(1, 1, 0.0f);
                damage = CalculateMagicDamage(ability);
            }
            else
            {
                damage = CalculateDamage();
            }
            var particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
            var damageText = (GameObject)Resources.Load(("Prefabs/DamageText"), typeof(GameObject));
            var dmgObject = GameObject.Instantiate(damageText.gameObject, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y + 3.2f), Quaternion.identity) as GameObject;
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
                //Debug.Log(targetCharacterGameObject.CharacterClassObject.Name);
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

        IEnumerator DisplayVictory()
        {
            GlobalConstants.EarnedGold = 200;
            GlobalConstants.EarnedExp = 750;
            yield return new WaitForSeconds(.5f);
            battlesong.mute = true;
            victorysong.Play();
            var temp = (GameObject)Resources.Load(("SpellPrefabs/Victory"), typeof(GameObject));
            var message = GameObject.Instantiate(temp.gameObject, new Vector3(targetTile.transform.parent.transform.position.x + 35, targetTile.transform.parent.transform.position.y - 35), Quaternion.identity) as GameObject;
            message.GetComponent<SpriteRenderer>().sortingOrder = 50;
            message.transform.parent = targetTile.transform.parent.transform;
            message.transform.localScale = new Vector3(4, 4, 0.0f);
            var endgamepost = new BattlePostObject();
            endgamepost.Finished = 1;
            //GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, endgamepost);
            //battle.EndGame();
            GlobalConstants.isMyTurn = false;
            Debug.Log("End Game Sent");
            yield return new WaitForSeconds(6f);
            SceneManager.LoadScene("BattleSummary");
        }

        IEnumerator DisplayDefeat()
        {
            GlobalConstants.EarnedGold = 50;
            GlobalConstants.EarnedExp = 375;
            yield return new WaitForSeconds(.5f);
            battlesong.mute = true;
            defeatsong.Play();
            var temp = (GameObject)Resources.Load(("SpellPrefabs/Defeat"), typeof(GameObject));
            var message = GameObject.Instantiate(temp.gameObject, new Vector3(targetTile.transform.parent.transform.position.x + 35, targetTile.transform.parent.transform.position.y - 35), Quaternion.identity) as GameObject;
            message.GetComponent<SpriteRenderer>().sortingOrder = 50;
            message.transform.parent = targetTile.transform.parent.transform;
            message.transform.localScale = new Vector3(4, 4, 0.0f);
            battle.EndGame();
            yield return new WaitForSeconds(6f);
            SceneManager.LoadScene("BattleSummary");
        }

        IEnumerator CastAnimation(Tile tempTile, string ability)
        {
            var tempTileList = new List<Tile>();
            tempTileList.Add(tempTile);

            yield return new WaitForSeconds(.5f);
            /*yield return new WaitForSeconds(.5f);
            
            anim.SetBool("isCasting", false);
            float wait = 0;
            int damage = 0;
            if (ability != null)
            {
                //Debug.Log(ability);
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
                    //Debug.Log(targetCharacterGameObject.CharacterClassObject.Name);
                    if (targetCharacterGameObject.CharacterClassObject.Name.Equals("Kelly"))
                    {
                        DisplayVictory.SetActive(true);
                        hidePanel = true;
                        yield return new WaitForSeconds(500f);
                    }
                }
            }*/
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
                action = Action.Move;
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
        IEnumerator WaitForPlayer1()
        {
            yield return new WaitForSeconds(2f);
            GlobalConstants.currentActions = new BattleAction();
            GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
            yield return new WaitForSeconds(2f);
            Debug.Log("End Turn Called by Waiting Player");
            SelectNextCharacter();
            //var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.CheckGameStatusUrl, new BattlePostObject());

        }
        IEnumerator WaitSecToStart()
        {
            yield return new WaitForSeconds(1f);
            SelectNextCharacter();
            //var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.CheckGameStatusUrl, new BattlePostObject());

        }
        
        public void ResetData()
        {
            selectedAbility = null;
            GlobalConstants.isAnimating = false;
            //Debug.Log(action);
            if (action != Action.WaitForGameInfo && action != Action.Victory && action != Action.Defeat)
            {
                hidePanel = false;
            }
        }


        public void highlightSpawn()
        {
            if (GlobalConstants.myPlayerId == 1)
            {
                for (var i = player1SpawnXStart; i < player1SpawnXEnd; i++)
                {
                    for (var j = player1SpawnYStart; j < player1SpawnYEnd; j++)
                    {
                        var tempTile = tileArray[i, j];
                        //Debug.Log(tempTile);
                        if (!tempTile.isObstructed)
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
                for (var i = player2SpawnXStart; i < player2SpawnXEnd; i++)
                {
                    for (var j = player2SpawnYStart; j < player2SpawnYEnd; j++)
                    {
                        var tempTile = tileArray[i, j];
                        //Debug.Log(tempTile);
                        if (!tempTile.isObstructed)
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
            var tileMap = GameObject.FindGameObjectWithTag("map");
            tempTile.isOccupied = true;
            tempTile.isAlly = true;
            gameCharacter.X = tempTile.x;
            gameCharacter.Y = tempTile.y;
            tempTile.character = gameCharacter;
            var spriteId = gameCharacter.CharacterClassObject.SpriteId;
            var temp = (GameObject)Resources.Load(("Prefabs/Character" + spriteId), typeof(GameObject));
            var tempchar = Instantiate(temp.gameObject, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y), Quaternion.identity) as GameObject;
            tempchar.GetComponent<SpriteRenderer>().sortingOrder = 6 + (tempTile.y * 2);
            tempchar.transform.parent = tileMap.transform;
            tempchar.transform.localScale = new Vector3(1, 1, 0.0f);
            tempchar.AddComponent<CharacterGameObject>();
            tempchar.GetComponent<CharacterGameObject>().CharacterClassObject = gameCharacter.CharacterClassObject;
            tempchar.GetComponent<CharacterGameObject>().X = gameCharacter.X;
            tempchar.GetComponent<CharacterGameObject>().Y = gameCharacter.Y;
            tempTile.characterObject = tempchar;

            //tempGC = gameChar;
            var tempAnim = tempchar.GetComponent<UnityEngine.Animator>();
            tempAnim.SetFloat("x", 0);
            tempAnim.SetFloat("y", -1);
            gameCharacter.CharacterClassObject.X = tempTile.x;
            gameCharacter.CharacterClassObject.Y = tempTile.y;
            characterList.Add(gameCharacter.CharacterClassObject);
            myCharacters.Add(tempchar);
            if (unitPlacedCount + 1 < characters.Count)
            {
                statsPanel.charName.text = characters[unitPlacedCount + 1].CharacterClassObject.Name;
                statsPanel.hp.text = characters[unitPlacedCount + 1].CharacterClassObject.CurrentStats.HitPoints + " / " + characters[unitPlacedCount + 1].CharacterClassObject.CurrentStats.HitPoints;
                statsPanel.mp.text = characters[unitPlacedCount + 1].CharacterClassObject.CurrentStats.MagicPoints + " / " + characters[unitPlacedCount + 1].CharacterClassObject.CurrentStats.MagicPoints;
            }
            idCount++;
        }

        public void PlaceEnemyCharacters(List<Character> enemyList)
        {
            foreach(var c in enemyList)
            {
                var tempTile = tileArray[c.X, c.Y];
                tempTile.isOccupied = true;
                var gameCharacter = new CharacterGameObject();
                gameCharacter.Initialize(c, c.X, c.Y);
                tempTile.character = gameCharacter;
                var spriteId = gameCharacter.CharacterClassObject.SpriteId;
                var temp = (GameObject)Resources.Load(("Prefabs/Character" + spriteId /*+ characters[unitPlacedCount].CharacterClassObject.SpriteId*/), typeof(GameObject));
                //gameCharacter.gameObject.transform.position = new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y);
                //gameCharacter.gameObject.transform.rotation = Quaternion.identity;
                var tempchar = GameObject.Instantiate(temp.gameObject, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y), Quaternion.identity) as GameObject;
                tempchar.GetComponent<SpriteRenderer>().sortingOrder = 6 + (tempTile.y * 2);
                tempchar.transform.parent = tileMap.transform;
                tempchar.transform.localScale = new Vector3(1, 1, 0.0f);
                tempchar.AddComponent<CharacterGameObject>();
                tempchar.GetComponent<CharacterGameObject>().GetComponent<SpriteRenderer>().color = new Color32(255, 195, 195, 255);
                tempchar.GetComponent<CharacterGameObject>().CharacterClassObject = gameCharacter.CharacterClassObject;
                tempchar.GetComponent<CharacterGameObject>().X = gameCharacter.X;
                tempchar.GetComponent<CharacterGameObject>().Y = gameCharacter.Y;
                tempTile.characterObject = tempchar;

                //tempGC = gameChar;
                var tempAnim = tempchar.GetComponent<UnityEngine.Animator>();
                tempAnim.SetFloat("x", 0);
                tempAnim.SetFloat("y", -1);
                gameCharacter.CharacterClassObject.X = tempTile.x;
                gameCharacter.CharacterClassObject.Y = tempTile.y;
                enemyCharacters.Add(tempchar);
            }
        }

        public void CreateTurnQueue()
        {
            for(var i = 0; i < myCharacters.Count; i++)
            {
                turnQueue.Add(myCharacters[i]);
                turnQueue.Add(enemyCharacters[i]);
                GlobalConstants.currentActions.CharacterQueue.Add(myCharacters[i].GetComponent<CharacterGameObject>().CharacterClassObject.CharacterId);
                GlobalConstants.currentActions.CharacterQueue.Add(enemyCharacters[i].GetComponent<CharacterGameObject>().CharacterClassObject.CharacterId);
            }
        }

        public void CreateTurnQueueP2()
        {
            //Debug.Log("Create TurnQueue P2");
            var tempList = new List<GameObject>();
            tempList.AddRange(myCharacters);
            tempList.AddRange(enemyCharacters);
            for (var i = 0; i < GlobalConstants.currentActions.CharacterQueue.Count; i++)
            {                
                var temp = GlobalConstants.currentActions.CharacterQueue[i];
                turnQueue.Add(tempList.Single(character => character.GetComponent<CharacterGameObject>().CharacterClassObject.CharacterId == temp));
            }
            //waitGameState = WaitGameState.Wait;
            SelectNextCharacter();
        }

        public void EndTurn()
        {
            Debug.Log("End Turn() Called");
            hidePanel = true;
            var tempAction = new GameClasses.Action(ActionType.Endturn, new List<Tile>(), "endturn");
            GlobalConstants.currentActions.AddAction(tempAction);
            GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateGameStatusUrl, new BattlePostObject());
            action = Action.WaitForGameInfo;
            GlobalConstants.isMyTurn = false;
            waitGameState = WaitGameState.WaitForOtherPlayer;
        }

        public void SelectNextCharacter()
        {
            Debug.Log("Select Next Character Called");
            ExecuteLingeringEffects();

            if (placeCharacterPhase)
            {
                placecharactersong.mute = true;
                battlesong.Play();
                placeCharacterPhase = false;
                foreach (var g in turnQueue)
                {
                    var tempGC = g.GetComponent<CharacterGameObject>();
                    var t = tileArray[tempGC.X, tempGC.Y];
                    tempGC.CharacterClassObject.CurrentStats.CurHP = tempGC.CharacterClassObject.CurrentStats.HitPoints;
                    tempGC.CharacterClassObject.CurrentStats.CurMP = tempGC.CharacterClassObject.CurrentStats.MagicPoints;
                }
            }
            else
            {
                var getNextAvailableCharacter = false;
                if (enemyCharacters.Select(character => character).Contains(turnQueue[0]))
                {
                    currentCharacterGameObject.GetComponent<SpriteRenderer>().color =  new Color32(255, 195, 195, 255);
                }
                else
                {
                    currentCharacterGameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                }
                while (!getNextAvailableCharacter)
                {
                    turnQueue.Add(turnQueue[0]);
                    turnQueue.RemoveAt(0);
                    var t = tileArray[turnQueue[0].GetComponent<CharacterGameObject>().X, turnQueue[0].GetComponent<CharacterGameObject>().Y];
                    if (!turnQueue[0].GetComponent<CharacterGameObject>().isDead)
                    {
                        getNextAvailableCharacter = true;
                    }
                }
                //Debug.Log(turnQueue[0].GetComponent<CharacterGameObject>().CharacterClassObject.Name);
            }
            
            if (myCharacters.Select(character => character).Contains(turnQueue[0]))
            {
                hidePanel = false;
                GlobalConstants.isMyTurn = true;
                playersTurnText.text = "Player " + GlobalConstants.myPlayerId + "s turn";
                action = Action.Idle;
            }
            else
            {
                GlobalConstants.isMyTurn = false;
                hidePanel = true;
                if (GlobalConstants.myPlayerId == 1) {
                    playersTurnText.text = "Player " + 2 + "s turn";
                }
                else
                {
                    playersTurnText.text = "Player " + 1 + "s turn";
                }
                waitGameState = WaitGameState.Wait;
                action = Action.WaitForGameInfo;
                
            }
                
                currentGameCharacter = turnQueue[0];
                currentCharacterGameObject = currentGameCharacter.GetComponent<CharacterGameObject>();
                currentCharacterGameObject.GetComponent<SpriteRenderer>().color = new Color32(161, 221, 255, 255);
                targetTile = tileArray[currentCharacterGameObject.X, currentCharacterGameObject.Y];
                Debug.Log(currentCharacterGameObject.X + " " + currentCharacterGameObject.Y);
                prevTile = targetTile;
                tile = prevTile;
                anim = currentCharacterGameObject.GetComponent<UnityEngine.Animator>();
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
                HideUsableAbilities();
                SetUsableAbilities();
        }

        public void ExecuteLingeringEffects()
        {
            foreach (var effect in GlobalConstants.ActiveEffects)
            {
                Debug.Log("!!! Effect : " + effect.Value.ToString() + " !!!");
                effect.Value.LingeringEffect(effect.Key.CharacterClassObject.CurrentStats);
                if (effect.Value.IsComplete() || effect.Key.isDead)
                {
                    effect.Value.RemoveEffect(effect.Key.CharacterClassObject.CurrentStats);
                }
            }

            GlobalConstants.ActiveEffects = GlobalConstants.ActiveEffects.Where( kvp => !kvp.Value.IsComplete()
                && !kvp.Key.isDead)
                  .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public void PositionSelectedPanel(CharacterGameObject selCharacter, Tile tempTile)
        {
            selectedCharcterStatsPanel.transform.position = new Vector3(selCharacter.transform.position.x, selCharacter.transform.position.y + 8, 0);
            if (tempTile.y < 4)
            {
                selectedCharcterStatsPanel.transform.position = new Vector3(selCharacter.transform.position.x, selCharacter.transform.position.y - 8, 0);
            }
        }
        
        public void PositionPanels()
        {
            characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x, currentCharacterGameObject.transform.position.y + 8, 0);
            actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 15, currentCharacterGameObject.transform.position.y - 7, 0);
            if (tile.x < 3)
            {
                //Debug.Log("tile x: " + tile.x + " is < 3");
                characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 10, currentCharacterGameObject.transform.position.y + 8, 0);
                if(tile.y < 3)
                {
                    //Debug.Log("tile y: " + tile.y + " is < 3");
                    characterStatsPanel.transform.position = new Vector3(characterStatsPanel.transform.position.x, currentCharacterGameObject.transform.position.y - 21, 0);
                }
                if (tile.y > tileMap.yLength - 5)
                {
                    //Debug.Log("tile y: " + tile.y + " is > 15");
                    actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 30, currentCharacterGameObject.transform.position.y + 14, 0);
                }

            }
            else if (tile.y < 3)
            {
                //Debug.Log("tile y: " + tile.y + " is < 3");
                characterStatsPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x, currentCharacterGameObject.transform.position.y - 21, 0);
                if (tile.x > 6)
                {
                    Debug.Log("tile x: " + tile.x + " is > 6");
                    actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x - 10, actionPanel.transform.position.y, 0);
                }
            }
            else if (tile.x > 6)
            {
                Debug.Log("tile x: " + tile.x + " is > 6");
                actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x - 10, actionPanel.transform.position.y, 0);
                if (tile.y < 3)
                {
                    Debug.Log("tile y: " + tile.y + " is < 3");
                    characterStatsPanel.transform.position = new Vector3(characterStatsPanel.transform.position.x, currentCharacterGameObject.transform.position.y - 21, 0);
                }
                if (tile.y > tileMap.yLength - 5)
                {
                    Debug.Log("tile y: " + tile.y + " is > 15");
                    actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 15, currentCharacterGameObject.transform.position.y + 14, 0);
                }
            }
            else if (tile.y > tileMap.yLength - 5)
            {
                Debug.Log("tile y: " + tile.y + " is > 15");
                actionPanel.transform.position = new Vector3(currentCharacterGameObject.transform.position.x + 15, currentCharacterGameObject.transform.position.y + 14, 0);
            }
        }

        public void CloneGameCharacter()
        {
            var cgo = new List<CharacterGameObject>();
            foreach(var c in gCharacters)
            {
                cgo.Add(c.cloneCharacter(c));
            }
            gCharacters = cgo;
        }

        private void SetUsableAbilities()
        {
            foreach(var a in currentCharacterGameObject.CharacterClassObject.Abilities)
            {
                abilityList.ShowAbility(a.Name);
            }
        }

        private void HideUsableAbilities()
        {
            abilityList.HideAbilities();
        }

        public bool CheckGameOver()
        {
            foreach(var g in myCharacters)
            {
                if(!g.GetComponent<CharacterGameObject>().isDead){
                    return false;
                }
            }
            action = Action.Defeat;
            return true;
        }

        public bool CheckVictory()
        {
            foreach (var g in enemyCharacters)
            {
                if (!g.GetComponent<CharacterGameObject>().isDead)
                {
                    return false;
                }
            }
            action = Action.Victory;
            return true;
        }
    }
}
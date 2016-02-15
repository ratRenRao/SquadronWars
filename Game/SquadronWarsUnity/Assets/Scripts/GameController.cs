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
            Attack,
            AttackAbility,
            CastAbility
        }
        public TileMap tileMap;
        GameObject currentGameCharacter;
        GameObject targetGameCharacter;
        public GameObject actionPanel;
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
        List<Character> characters = new List<Character>();
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
            //anim = currentGameCharacter.GetComponent<Animator>();
            //tarAnim = targetGameCharacter.GetComponent<Animator>();
            //tarAnim.SetFloat("x", 0);
            //tarAnim.SetFloat("y", -1);
        }

        public Stats GetBonusStats(Character character)
        {
            foreach (Equipment equipment in character.equipment.Values)
            {
                character.alteredStats = character.alteredStats.concatStats(character.baseStats, equipment.stats);
            }
            return character.alteredStats;
        }

        void Update()
        {
            if (!arraySet)
            {
                PrepTest();                
                tileArray = tileMap.tileArray;
                highlightSpawn();
                arraySet = true;
            }
            if (hidePanel)
            {
                Debug.Log("Hide Panel Called");
                actionPanel.SetActive(false);
            }
            if (!hidePanel)
            {
                actionPanel.SetActive(true);
            }
            if (reachedPosition == false && count < path.Count)
            {
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
                
                float currentX = (float)(System.Math.Round(currentGameCharacter.transform.localPosition.x, 2));
                float currentY = (float)(System.Math.Round(currentGameCharacter.transform.localPosition.y, 2));
                float targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
                float targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
                Debug.Log("current x:" + currentX + "; current y:" + currentY);
                Debug.Log("target x:" + targetX + "; target y:" + targetY);
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
                    if ((count + 1) == path.Count)
                    {

                        isWalking = false;
                        anim.SetBool("isWalking", isWalking);
                        reachedPosition = true;
                        path.Clear();
                        count = 0;
                        action = Action.IDLE;
                        curGameCharacter.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
                        targetTile.isOccupied = true;
                        targetTile.characterObject = currentGameCharacter;
                        targetTile.character = curGameCharacter;
                        hidePanel = false;
                    }
                    else {
                        count++;                        
                        curGameCharacter.GetComponent<SpriteRenderer>().sortingOrder = 6 + (targetTile.y * 2);
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
                        Move();
                    }
                    if (action == Action.Attack)
                    {
                        if (hit.collider != null)
                        {
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                hidePanel = true;
                                GetTarget(tempTile);
                                Attack(tempTile, null);
                                clearHighlights(validMoves);
                                //hidePanel = false;
                            }
                        }
                    }
                    if (action == Action.AttackAbility)
                    {
                        if (hit.collider != null)
                        {                            
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                hidePanel = true;
                                GetTarget(tempTile);
                                clearHighlights(validMoves);
                                Attack(tempTile, selectedAbility);
                            }              
                        }
                    }
                    if (action == Action.CastAbility)
                    {
                        if (hit.collider != null)
                        {                            
                            Tile tempTile = hit.collider.gameObject.GetComponent<Tile>();
                            if (tempTile.isValidMove)
                            {
                                hidePanel = true;
                                GetTarget(tempTile);
                                clearHighlights(validMoves);
                                Cast(tempTile, selectedAbility);
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
                            placeCharacter(tempTile);
                            tempTile.isValidMove = false;
                            tempTile.highlight.SetActive(false);
                            tempTile.isOccupied = true;
                            if ((unitPlacedCount + 1) == characters.Count)
                            {
                                placeCharacterPhase = false;
                                selectNextCharacter();
                                clearHighlights(validMoves);
                                hidePanel = false;
                            }
                            unitPlacedCount++;
                        }
                    }
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

        public void AttackAbility(string ability)
        {
            ShowAttackMoves();
            action = Action.AttackAbility;
            selectedAbility = ability;
            
        }
        public void CastAbility(string ability)
        {
            ShowAttackMoves();
            action = Action.CastAbility;
            selectedAbility = ability;

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
            Debug.Log(curGameCharacter.character.alteredStats.speed);
            int move = curGameCharacter.character.alteredStats.speed;
            
            if (action == Action.IDLE)
            {                
                validMoves = new List<Tile>();
                //Create Bottom Move Tiles
                for (int i = 1; i <= curGameCharacter.character.alteredStats.speed; i++)
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
                for (int i = 1; i <= curGameCharacter.character.alteredStats.speed; i++)
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
                for (int i = 1; i <= curGameCharacter.character.alteredStats.speed; i++)
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
                move = curGameCharacter.character.alteredStats.speed;
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
                move = curGameCharacter.character.alteredStats.speed;
                for (int i = 1; i < curGameCharacter.character.alteredStats.speed; i++)
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
                move = curGameCharacter.character.alteredStats.speed;
                for (int i = 1; i < curGameCharacter.character.alteredStats.speed; i++)
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
                move = curGameCharacter.character.alteredStats.speed;
                for (int i = 1; i < curGameCharacter.character.alteredStats.speed; i++)
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
                move = curGameCharacter.character.alteredStats.speed;
                for (int i = 1; i < curGameCharacter.character.alteredStats.speed; i++)
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
                Debug.Log("X:" + currentX + "Y:" + (currentY - 1));
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

        public void GetTarget(Tile tile) {
            if (tile.isOccupied)
            {
                tarAnim = tile.characterObject.GetComponent<Animator>();
                targetGameCharacter = tile.characterObject;
                tarGameCharacter = tile.character;
            }
            //tarGameCharacter = tile.GetComponent<GameCharacter>();
            //targetGameCharacter = tarGameCharacter.transform.parent.gameObject;

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
            tarAnim = targetGameCharacter.GetComponent<Animator>();
            tarAnim.SetBool("isAttacked", true);
            StartCoroutine("InjuredAnimation");
        }

        int CalculateDamage()
        {
            int dmg = curGameCharacter.character.alteredStats.damage;
            int def = tarGameCharacter.character.alteredStats.defense;
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
            tarGameCharacter.character.alteredStats.currentHP -= damage;
            Debug.Log(tarGameCharacter.character.alteredStats.currentHP);
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
            Character character1 = new Character(1, stat1, 1, "Saint Lancelot", 1, 75, equipment);
            character1.alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character1.alteredStats = GetBonusStats(character1);
            character1.alteredStats.speed = 4;
            character1.spriteId = 1;
            Stats stat2 = new Stats(3, 3, 3, 4, 3, 3, 2);
            Character character2 = new Character(1, stat2, 1, "Ragthar", 1, 75, equipment);
            character2.alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0);
            character2.alteredStats = GetBonusStats(character2);
            character2.alteredStats.speed = 3;
            character2.spriteId = 2;
            GlobalConstants.matchCharacters.Add(character1);
            GlobalConstants.matchCharacters.Add(character2);
            characters = GlobalConstants.matchCharacters;
            placeCharacterPhase = true;
            
        }

        public void highlightSpawn()
        {
            for(int i = 0; i < 5; i++)
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
        public void placeCharacter(Tile tempTile)
        {
            GameObject tileMap = GameObject.FindGameObjectWithTag("map");
            tempTile.isOccupied = true;            
            tempTile.character = new GameCharacter(characters[unitPlacedCount], tempTile.x, tempTile.y);
            
            GameObject temp = (GameObject)Resources.Load(("Prefabs/Character" + characters[unitPlacedCount].spriteId), typeof(GameObject));
            GameObject tempchar = GameObject.Instantiate(temp, new Vector3(tempTile.transform.position.x + 1.6f, tempTile.transform.position.y), Quaternion.identity) as GameObject;
            tempchar.GetComponent<SpriteRenderer>().sortingOrder = 6 + (tempTile.y * 2);
            tempchar.transform.parent = tileMap.transform;
            Debug.Log(tempchar.transform.localPosition);
            GameCharacter gameChar = tempTile.character;
            tempchar.AddComponent<GameCharacter>();
            tempchar.GetComponent<GameCharacter>().character = gameChar.character;
            tempchar.GetComponent<GameCharacter>().x = gameChar.x;
            tempchar.GetComponent<GameCharacter>().y = gameChar.y;
            tempTile.characterObject = tempchar;
            Debug.Log(tempTile.character.x);
            //tempGC = gameChar;
            myCharacters.Add(tempchar);
        }

        public void selectNextCharacter()
        {
            myCharacters.Add(myCharacters[0]);
            myCharacters.RemoveAt(0);         
            currentGameCharacter = myCharacters[0];
            curGameCharacter = currentGameCharacter.GetComponent<GameCharacter>();
            targetTile = tileArray[curGameCharacter.x, curGameCharacter.y];
            prevTile = targetTile;
            tile = prevTile;
            anim = currentGameCharacter.GetComponent<Animator>();
        }
    }

}
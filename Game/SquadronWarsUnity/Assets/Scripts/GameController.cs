using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.GameClasses;

public class GameController : MonoBehaviour
{
    public enum Action
    {
        FLAG,
        MOVE,
        EXCAVATION
    }

    Vector3 hitDown;
    RaycastHit2D hit;
    Animator anim;
    GameObject targetTile;
    TileMap tileMap = null;
    Tile tile = null;
    Tile prevTile = null;
    GameObject lastTile;
    Dictionary<string, int> inventory;
    public GameCharacter currentCharacter;
    Character character;
    Action action = Action.MOVE;
    bool isWalking;
    bool isCharacter;
    bool reachedPosition = true;
    bool lifeLost = false;




    // Use this for initialization
    void Start()
    {
        //screen = ScreenOrientation.Landscape;
        targetTile = GameObject.FindGameObjectWithTag("start");
        currentCharacter.x = 0;
        currentCharacter.y = 0;
        prevTile = targetTile.GetComponent<Tile>();
        tile = prevTile;
        string obj = this.name;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
            if (reachedPosition == false)
            {
                Debug.Log("test");
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
                float currentX = (float)(System.Math.Round(transform.localPosition.x, 2));
                float currentY = (float)(System.Math.Round(transform.localPosition.y, 2));
                float targetX = (float)(System.Math.Round(targetTile.transform.localPosition.x + 1.6f, 2));
                float targetY = (float)(System.Math.Round(targetTile.transform.localPosition.y, 2));
                Transform targetLocation = targetTile.transform;
                if (currentX - targetX > 0)
                {
                    Debug.Log("Move Left");
                    anim.SetFloat("x", -1);
                    anim.SetFloat("y", 0);
                    transform.position += new Vector3(-0.1f, 0);
                }
                if (currentX - targetX < 0)
                {

                    Debug.Log("Move Right");
                    anim.SetFloat("x", 1);
                    anim.SetFloat("y", 0);

                    transform.position += new Vector3(0.1f, 0);
                }
                if (currentY - targetY > 0)
                {

                    Debug.Log("Move Up");
                    anim.SetFloat("x", 0);
                    anim.SetFloat("y", -1);


                    transform.position += new Vector3(0, -0.1f);
                }

                if (currentY - targetY < 0)
                {

                    Debug.Log("Move Down");
                    anim.SetFloat("x", 0);
                    anim.SetFloat("y", 1);

                    transform.position += new Vector3(0, 0.1f);
                }

                if (currentY - targetY == 0 && currentX - targetX == 0)
                {
                    
                    isWalking = false;
                    anim.SetBool("isWalking", isWalking);
                    reachedPosition = true;               
                }

            }
            else
            {
            
                if (Input.GetMouseButtonUp(0) && reachedPosition)
                {
                
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (action == Action.MOVE)
                    {
                    if (hit.collider != null)
                        {
                        GameObject revertTargetTile = targetTile;
                            lastTile = targetTile;
                            targetTile = hit.collider.gameObject;
                            Tile tempTile = targetTile.GetComponent<Tile>();
                            Debug.Log(tempTile);
                        if (checkMove(tempTile))
                                {
                                    tile = tempTile;
                                    prevTile = lastTile.GetComponent<Tile>();
                                    currentCharacter.x = tile.x;
                                    currentCharacter.y = tile.y;
                                    reachedPosition = false;
                                }
                                 else
                                {
                                targetTile = revertTargetTile;
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
    



    public bool checkMove(Tile tile)
    {
        int currentX = currentCharacter.x;
        int currentY = currentCharacter.y;
        int tileX = tile.x;
        int tileY = tile.y;

        
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

        Debug.Log("Tile X:" + tile.x + ", Tile Y:" + tile.y);
        return false;
    }


    public void SetAction(string newAction)
    {
        Debug.Log(newAction + " Action Called");


    }


}
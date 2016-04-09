using UnityEngine;
using System.Collections;

public class ExperienceBar : MonoBehaviour
{
    public Texture2D bar;
    Rect bgRect;
    Rect barRect;
    Rect labelRect;
    int exp = 0;
    public int maxExp = 1000;

    void Awake()
    {
        bgRect = new Rect(Screen.width - 450, Screen.height - 440, 180, 25);
        barRect = new Rect(Screen.width - 448, Screen.height - 438, 0, 21);
        labelRect = new Rect(Screen.width - 390, Screen.height - 438, 160, 24);
    }

    void Update()
    {
        if (exp > 0)
            barRect.width = exp * bgRect.width / maxExp;
        
        //TEST
        if (exp < maxExp)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                exp += 5;
        }
    }

    void OnGUI()
    {
        GUI.Box(bgRect, GUIContent.none);
        GUI.DrawTexture(barRect, bar);
        GUI.Label(labelRect, exp.ToString() + " / " + maxExp.ToString());
    }
} 

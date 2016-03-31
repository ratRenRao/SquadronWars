using UnityEngine;
using System.Collections;

public class ExperienceBar : MonoBehaviour
{
    public Texture2D bar;
    Rect bgRect, bgRect2, bgRect3, bgRect4, bgRect5;
    Rect barRect, barRect2, barRect3, barRect4, barRect5;
    Rect labelRect, labelRect2, labelRect3, labelRect4, labelRect5;
    int exp = 0;
    public int maxExp = 1000;

    void Awake()
    {
        bgRect = new Rect(300, 155, 200, 24);
        barRect = new Rect(302, 157, 0, 20);
        labelRect = new Rect(380, 155, 128, 24);

        bgRect2 = new Rect(300, 245, 200, 24);
        barRect2 = new Rect(302, 247, 0, 20);
        labelRect2 = new Rect(380, 245, 128, 24);

        bgRect3 = new Rect(300, 335, 200, 24);
        barRect3 = new Rect(302, 337, 0, 20);
        labelRect3 = new Rect(380, 335, 128, 24);

        bgRect4 = new Rect(300, 425, 200, 24);
        barRect4 = new Rect(302, 427, 0, 20);
        labelRect4 = new Rect(380, 425, 128, 24);

        bgRect5 = new Rect(300, 515, 200, 24);
        barRect5 = new Rect(302, 517, 0, 20);
        labelRect5 = new Rect(380, 515, 128, 24);
    }

    void Update()
    {
        if (exp > 0)
            barRect.width = exp * bgRect.width / maxExp;
            barRect2.width = exp * bgRect2.width / maxExp;
            barRect3.width = exp * bgRect3.width / maxExp;
            barRect4.width = exp * bgRect4.width / maxExp;
            barRect5.width = exp * bgRect5.width / maxExp;

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

        GUI.Box(bgRect2, GUIContent.none);
        GUI.DrawTexture(barRect2, bar);
        GUI.Label(labelRect2, exp.ToString() + " / " + maxExp.ToString());

        GUI.Box(bgRect3, GUIContent.none);
        GUI.DrawTexture(barRect3, bar);
        GUI.Label(labelRect3, exp.ToString() + " / " + maxExp.ToString());

        GUI.Box(bgRect4, GUIContent.none);
        GUI.DrawTexture(barRect4, bar);
        GUI.Label(labelRect4, exp.ToString() + " / " + maxExp.ToString());

        GUI.Box(bgRect5, GUIContent.none);
        GUI.DrawTexture(barRect5, bar);
        GUI.Label(labelRect5, exp.ToString() + " / " + maxExp.ToString());
    }

}

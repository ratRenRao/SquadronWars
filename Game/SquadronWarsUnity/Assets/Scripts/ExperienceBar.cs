using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Text experience;
    public Text level;
    public Image bar;
    int lvl;
    int index;
    int exp = 0;
    int maxExp;
    public int[] expLevel = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    void Start()
    {
        Image bar = GetComponent<Image>();
        lvl = 1;
        index = 0;
        maxExp = expLevel[index];
        experience.text = exp.ToString() + " / " + maxExp.ToString();
        level.text = lvl.ToString();
    }

    void Update()
    {
        //TEST
        if (exp < maxExp)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                exp += 1;
                experience.text = exp.ToString() + " / " + maxExp.ToString();
                bar.fillAmount = exp * 1.0f / maxExp;
            }
        }

        if (exp == maxExp && maxExp != 1000)
        {
            lvl++;
            index++;
            level.text = lvl.ToString();
            exp = 0;
            maxExp = expLevel[index];
        }
    }
} 

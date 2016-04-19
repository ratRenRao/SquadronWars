using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.GameClasses;

public class ExperienceBar : MonoBehaviour
{
    public Text experience;
    public Text level;
    public Text charName;
    public GameObject characterModel;
    public Image bar;
    public int slot;
    int lvl;
    int index;
    int exp = 0;
    int maxExp;
    public int[] expLevel = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    void Start()
    {
        CharacterGameObject chararacter = GlobalConstants.MatchCharacters[slot];
        var temp = (GameObject)Resources.Load(("Prefabs/Character" + chararacter.CharacterClassObject.SpriteId), typeof(GameObject));
        var sprite = temp.GetComponent<SpriteRenderer>();
        Image bar = GetComponent<Image>();
        characterModel.GetComponent<Image>().sprite = sprite.sprite;
        lvl = chararacter.CharacterClassObject.LevelId;
        exp = chararacter.CharacterClassObject.BaseStats.Experience;
        maxExp = expLevel[lvl - 1];
        experience.text = exp.ToString() + " / " + maxExp.ToString();
        level.text = lvl.ToString();
        charName.text = chararacter.CharacterClassObject.Name;
    }

    void Update()
    {
        //TEST
        if (0 < GlobalConstants.EarnedExp)
        {
            exp += 1;
            experience.text = exp.ToString() + " / " + maxExp.ToString();
            bar.fillAmount = exp * 1.0f / maxExp;
            GlobalConstants.EarnedExp -= 1;
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

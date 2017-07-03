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
        var character = GlobalConstants.MatchCharacters[slot];
        var temp = (GameObject)Resources.Load(("Prefabs/Character" + character.CharacterClassObject.SpriteId), typeof(GameObject));
        var sprite = temp.GetComponent<SpriteRenderer>();
        var bar = GetComponent<Image>();
        characterModel.GetComponent<Image>().sprite = sprite.sprite;
        lvl = character.CharacterClassObject.LevelId;
        exp = character.CharacterClassObject.BaseStats.Experience;
        maxExp = expLevel[lvl - 1];
        experience.text = exp.ToString() + " / " + maxExp.ToString();
        level.text = lvl.ToString();
        charName.text = character.CharacterClassObject.Name;
        GlobalConstants.updateCharacters = true;
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
            for(var i = 0; i < GlobalConstants.Player.Characters.Count; i++)
            {
                if(GlobalConstants.Player.Characters[i].CharacterId == GlobalConstants.MatchCharacters[slot].CharacterClassObject.CharacterId)
                {
                    GlobalConstants.Player.Characters[i].LevelId++;
                    GlobalConstants.Player.Characters[i].BaseStats.SkillPoints++;
                    GlobalConstants.Player.Characters[i].BaseStats.StatPoints += 5;
                }
            }
            index++;
            level.text = lvl.ToString();
            exp = 0;
            maxExp = expLevel[index];
        }

        if(0 == GlobalConstants.EarnedExp && GlobalConstants.updateCharacters)
        {
            for (var i = 0; i < GlobalConstants.Player.Characters.Count; i++)
            {
                if (GlobalConstants.Player.Characters[i].CharacterId == GlobalConstants.MatchCharacters[slot].CharacterClassObject.CharacterId)
                {
                    GlobalConstants.Player.Characters[i].BaseStats.Experience = exp;
                    GlobalConstants.Player.Characters[i].Experience = exp;
                    GlobalConstants.allFiveCharacters++;
                    if(GlobalConstants.allFiveCharacters == 5)
                    {
                        GlobalConstants.updateCharacters = false;
                    }
                }
            }
        }
    }
} 

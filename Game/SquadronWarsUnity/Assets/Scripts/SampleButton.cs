using UnityEngine;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class SampleButton : MonoBehaviour
{

    public Button button;
    public Character character;
    public Text nameLabel;
    public CharacterScreen menuStats;

    public void BuildCharacterScreen()
    {        
        GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        SampleButton button = gameObject.GetComponent<SampleButton>();
        MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        GameObject temp = (GameObject)Resources.Load(("Prefabs/Character1"), typeof(GameObject));
        SpriteRenderer sprite = temp.GetComponent<SpriteRenderer>();
        //   Stats baseStats = character.baseStats;
        stats.sampleButton = button;
        menu.squadScreenPanel.SetActive(false);
        menu.characterScreenPanel.SetActive(true);
        stats.characterSprite.sprite = sprite.sprite;
        stats.characterName.text = character.Name;
        
        UpdateStats(character);
        stats.levelStat.text = character.LevelId.ToString();
        int expToNextLevel = character.experienceNeeded();
     //   int startExp = character.startExperience();
        stats.experienceStat.text = string.Format("{0} / {1}", character.BaseStats.Experience.ToString(), expToNextLevel.ToString());
        int progBar = character.percentToNextLevel();
        stats.ProgressBar.value = progBar;
        //Debug.Log(character.equipment[ItemType.HELM].name);
        Debug.Log(character.BaseStats.Intl);
      //  BuildDropdowns(stats);
      //  EvaluateSkills();
    }

    public void ReevaluateStats(Text labelText)
    {
   //     GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character = stats.sampleButton.character;
        string itemName = labelText.text;
        Equipment equipment = character.Equipment; 
        Equipment prevItem = null;
        /*
        foreach (var item in equipment.GetEquipmentItems())
        {
            if (item.itemType == item.itemType)
            {
                prevItem = charEquipment;
                break;
            }
            character.alteredStats = item.stats.removeAlteredStats(character.alteredStats, prevItem.stats);
            character.alteredStats = item.stats.concatStats(character.alteredStats, item.stats);
            character.equipment. = item;
            UpdateStats(character);
        }
        */
    }

    public void UpdateStats(Character character)
    {
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
        CharacterScreen menuStats = statsManager.GetComponent<CharacterScreen>();
        Stats stats = character.BaseStats;
        Stats bonusStats = character.CurrentStats;
        Stats concatStats = stats.ConcatStats(character.BaseStats, character.CurrentStats);
        menuStats.strengthStat.text = formatStats(stats.Str, bonusStats.Str);
        menuStats.agilityStat.text = formatStats(stats.Agi, bonusStats.Agi);
        menuStats.intelligenceStat.text = formatStats(stats.Intl, bonusStats.Intl);
        menuStats.vitalityStat.text = formatStats(stats.Vit, bonusStats.Vit);
        menuStats.dexterityStat.text = formatStats(stats.Dex, bonusStats.Dex);
        menuStats.wisdomStat.text = formatStats(stats.Wis, bonusStats.Wis);
        menuStats.luckStat.text = formatStats(stats.Luck, bonusStats.Luck);
        menuStats.hitPointsStat.text = concatStats.CalculateHp(character.LevelId).ToString();
        menuStats.manaStat.text = concatStats.CalculateMp(character.LevelId).ToString();
        menuStats.damageStat.text = concatStats.CalculateDamage(character.LevelId).ToString();
        menuStats.magicDamageStat.text = concatStats.CalculateMagicDamage(character.LevelId).ToString();
        menuStats.speedStat.text = concatStats.CalculateSpeed(character.LevelId).ToString();
        menuStats.defenseStat.text = concatStats.CalculateDefense(character.LevelId).ToString();
        menuStats.magicDefenseStat.text = concatStats.CalculateMagicDefense(character.LevelId).ToString();
        menuStats.hitRateStat.text = concatStats.CalculateHitRate(character.LevelId).ToString();
        menuStats.dodgeRateStat.text = concatStats.CalculateDodgeRate(character.LevelId).ToString();
        menuStats.criticalRateStat.text = concatStats.CalculateCritRate(character.LevelId).ToString();
        menuStats.remainingStatPoints.text = character.BaseStats.StatPoints.ToString();
        menuStats.remainingSkillPoints.text = character.BaseStats.SkillPoints.ToString();
    }

    
    public string formatStats(int stats, int bonusStats)
    {
        if (bonusStats == 0)
        {
            return stats.ToString();
        }
        else
        {
            return string.Format("{0} + {1}", stats.ToString(), bonusStats.ToString());
        }
    }

    /*
    public void incrementStat(string stat)
    {
     //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character = stats.sampleButton.character;
        if (character.StatPoints > 0)
        {
            if (stat.Equals("strength"))
            {
                character.BaseStats.strength++;                
            }
            if (stat.Equals("agility"))
            {
                character.BaseStats.agility++;
            }
            if (stat.Equals("intelligence"))
            {
                character.BaseStats.intelligence++;
            }
            if (stat.Equals("vitality"))
            {
                character.BaseStats.vitality++;
            }
            if (stat.Equals("dexterity"))
            {
                character.BaseStats.dexterity++;
            }
            if (stat.Equals("wisdom"))
            {
                character.BaseStats.wisdom++;
            }
            if (stat.Equals("luck"))
            {
                character.BaseStats.luck++;
            }
            character.StatPoints--;
            UpdateStats(character);

        }
    }

    public void BuildDropdowns(CharacterScreen dropdowns)
    {
        Debug.Log("called");
        dropdowns.helmSlot.options.Clear();        
        dropdowns.shoulderSlot.options.Clear();
        dropdowns.chestSlot.options.Clear();
        dropdowns.glovesSlot.options.Clear();
        dropdowns.legsSlot.options.Clear();
        dropdowns.bootsSlot.options.Clear();
        foreach(Item item in GlobalConstants.ItemList.Values)
        {
            if (item.itemType == ItemType.Helm)
            {
                dropdowns.helmSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.itemType == ItemType.Shoulders)
            {
                dropdowns.shoulderSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.itemType == ItemType.Chest)
            {
                dropdowns.chestSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.itemType == ItemType.Legs)
            {
                dropdowns.legsSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.itemType == ItemType.Gloves)
            {
                dropdowns.glovesSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.itemType == ItemType.Boots)
            {
                dropdowns.bootsSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else
            {

            }
        }
        Debug.Log(character.equipment[ItemType.HELM].name);
        dropdowns.helmSlot.GetComponentsInChildren<Text>()[0].text = character.equipment[ItemType.HELM].name;
        dropdowns.shoulderSlot.GetComponentsInChildren<Text>()[0].text = character.equipment[ItemType.SHOULDERS].name;
        dropdowns.chestSlot.GetComponentsInChildren<Text>()[0].text = character.equipment[ItemType.CHEST].name;
        dropdowns.glovesSlot.GetComponentsInChildren<Text>()[0].text = character.equipment[ItemType.GLOVES].name;
        dropdowns.legsSlot.GetComponentsInChildren<Text>()[0].text = character.equipment[ItemType.LEGS].name;
        dropdowns.bootsSlot.GetComponentsInChildren<Text>()[0].text = character.equipment[ItemType.BOOTS].name;
    }

    public void LevelSkill(string skill)
    {
     //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
    //    SampleButton button = gameObject.GetComponent<SampleButton>();
    //    MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character = stats.sampleButton.character;
        if (character.SkillPoints > 0)
        {
            if (character.skillList.ContainsKey(skill))
            {
                character.skillList[skill]++;
            }
            else
            {
                character.skillList.Add(skill, 1);
            }
            if (skill.Equals("fire"))
            {                                
                stats.fireLvl.text = "L" + character.skillList[skill];
            }
            if (skill.Equals("cure"))
            {
                stats.cureLvl.text = "L" + character.skillList[skill];
            }
            if (skill.Equals("focus"))
            {
                stats.focusLvl.text = "L" + character.skillList[skill];
            }
            if (skill.Equals("bash"))
            {
                stats.bashLvl.text = "L" + character.skillList[skill];
            }
            character.SkillPoints--;
            UpdateStats(character);

        }
    }

    public void EvaluateSkills()
    {
      //  GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character = stats.sampleButton.character;
        if (character.skillList.ContainsKey("fire")){
            stats.fireLvl.text = "L" + character.skillList["fire"].ToString();
        }
        else {
            stats.fireLvl.text = "";
        }
        if (character.skillList.ContainsKey("bash"))
        {
            stats.bashLvl.text = "L" + character.skillList["bash"].ToString();
        }
        else
        {
            stats.bashLvl.text = "";
        }
        if (character.skillList.ContainsKey("cure"))
        {
            stats.cureLvl.text = "L" + character.skillList["cure"].ToString();
        }
        else
        {
            stats.cureLvl.text = "";
        }
        if (character.skillList.ContainsKey("focus"))
        {
            stats.focusLvl.text = "L" + character.skillList["focus"].ToString();
        }
        else
        {
            stats.focusLvl.text = "";
        }
        stats.hasteLvl.text = "";
        stats.regenLvl.text = "";
        stats.bioLvl.text = "";
        stats.iceLvl.text = "";
        stats.flameStrikeLvl.text = "";
        stats.armorBreakLvl.text = "";
        stats.chargeLvl.text = "";
        stats.doubleAttackLvl.text = "";
    }
    */
}

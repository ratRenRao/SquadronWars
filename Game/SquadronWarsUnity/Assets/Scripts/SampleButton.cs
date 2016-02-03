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
        SpriteRenderer sprite = GameObject.FindGameObjectWithTag("Character1").GetComponent<SpriteRenderer>();
     //   Stats baseStats = character.baseStats;
        character.sprite = sprite.sprite;
        stats.sampleButton = button;
        menu.squadScreenPanel.SetActive(false);
        menu.characterScreenPanel.SetActive(true);
        stats.characterSprite.sprite = character.sprite;
        stats.characterName.text = character.characterName;
        
        UpdateStats(character);
        stats.levelStat.text = character.level.ToString();
        int expToNextLevel = character.experienceNeeded();
     //   int startExp = character.startExperience();
        stats.experienceStat.text = string.Format("{0} / {1}", character.experience.ToString(), expToNextLevel.ToString());
        int progBar = character.percentToNextLevel();
        stats.ProgressBar.value = progBar;
        Debug.Log(character.equipment[ItemType.HELM].name);
        Debug.Log(character.baseStats.intelligence);
        BuildDropdowns(stats);
        EvaluateSkills();
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
        Equipment item = (Equipment)GlobalConstants.ItemList[itemName];
        Equipment prevItem = null;
        foreach(Equipment charEquipment in character.equipment.Values)
        {
            if(charEquipment.itemType == item.itemType)
            {
                prevItem = charEquipment;
                break;
            }
        }
        character.alteredStats = item.stats.removeAlteredStats(character.alteredStats, prevItem.stats);
        character.alteredStats = item.stats.concatStats(character.alteredStats, item.stats);
        character.equipment[item.itemType] = item;
        UpdateStats(character);
    }

    public void UpdateStats(Character character)
    {
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
        CharacterScreen menuStats = statsManager.GetComponent<CharacterScreen>();
        Stats stats = character.baseStats;
        Stats bonusStats = character.alteredStats;
        Stats concatStats = stats.concatStats(character.baseStats, character.alteredStats);
        menuStats.strengthStat.text = formatStats(stats.strength, bonusStats.strength);
        menuStats.agilityStat.text = formatStats(stats.agility, bonusStats.agility);
        menuStats.intelligenceStat.text = formatStats(stats.intelligence, bonusStats.intelligence);
        menuStats.vitalityStat.text = formatStats(stats.vitality, bonusStats.vitality);
        menuStats.dexterityStat.text = formatStats(stats.dexterity, bonusStats.dexterity);
        menuStats.wisdomStat.text = formatStats(stats.wisdom, bonusStats.wisdom);
        menuStats.luckStat.text = formatStats(stats.luck, bonusStats.luck);
        menuStats.hitPointsStat.text = concatStats.calculateHP(character.level).ToString();
        menuStats.manaStat.text = concatStats.calculateMP(character.level).ToString();
        menuStats.damageStat.text = concatStats.calculateDamage(character.level).ToString();
        menuStats.magicDamageStat.text = concatStats.calculateMagicDamage(character.level).ToString();
        menuStats.speedStat.text = concatStats.calculateSpeed(character.level).ToString();
        menuStats.defenseStat.text = concatStats.calculateDefense(character.level).ToString();
        menuStats.magicDefenseStat.text = concatStats.calculateMagicDefense(character.level).ToString();
        menuStats.hitRateStat.text = concatStats.calculateHitRate(character.level).ToString();
        menuStats.dodgeRateStat.text = concatStats.calculateDodgeRate(character.level).ToString();
        menuStats.criticalRateStat.text = concatStats.calculateCritRate(character.level).ToString();
        menuStats.remainingStatPoints.text = character.statPoints.ToString();
        menuStats.remainingSkillPoints.text = character.skillPoints.ToString();
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

    public void incrementStat(string stat)
    {
     //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character = stats.sampleButton.character;
        if (character.statPoints > 0)
        {
            if (stat.Equals("strength"))
            {
                character.baseStats.strength++;                
            }
            if (stat.Equals("agility"))
            {
                character.baseStats.agility++;
            }
            if (stat.Equals("intelligence"))
            {
                character.baseStats.intelligence++;
            }
            if (stat.Equals("vitality"))
            {
                character.baseStats.vitality++;
            }
            if (stat.Equals("dexterity"))
            {
                character.baseStats.dexterity++;
            }
            if (stat.Equals("wisdom"))
            {
                character.baseStats.wisdom++;
            }
            if (stat.Equals("luck"))
            {
                character.baseStats.luck++;
            }
            character.statPoints--;
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
            if (item.itemType == ItemType.HELM)
            {
                dropdowns.helmSlot.options.Add(new Dropdown.OptionData() { text = item.name });
            }
            else if (item.itemType == ItemType.SHOULDERS)
            {
                dropdowns.shoulderSlot.options.Add(new Dropdown.OptionData() { text = item.name });
            }
            else if (item.itemType == ItemType.CHEST)
            {
                dropdowns.chestSlot.options.Add(new Dropdown.OptionData() { text = item.name });
            }
            else if (item.itemType == ItemType.LEGS)
            {
                dropdowns.legsSlot.options.Add(new Dropdown.OptionData() { text = item.name });
            }
            else if (item.itemType == ItemType.GLOVES)
            {
                dropdowns.glovesSlot.options.Add(new Dropdown.OptionData() { text = item.name });
            }
            else if (item.itemType == ItemType.BOOTS)
            {
                dropdowns.bootsSlot.options.Add(new Dropdown.OptionData() { text = item.name });
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
        if (character.skillPoints > 0)
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
            character.skillPoints--;
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
}

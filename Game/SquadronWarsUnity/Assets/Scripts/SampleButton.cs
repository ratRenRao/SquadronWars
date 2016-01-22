using UnityEngine;
using SquadronWars2;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Mime;

public class SampleButton : MonoBehaviour
{

    public Button Button;
    public Character Character;
    public Text NameLabel;

    public void BuildCharacterScreen()
    {        
        GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        SampleButton button = gameObject.GetComponent<SampleButton>();
        MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        SpriteRenderer sprite = GameObject.FindGameObjectWithTag("Character1").GetComponent<SpriteRenderer>();
        Stats baseStats = Character.BaseStats;
        //Character.Sprite = sprite.sprite;
        stats.sampleButton = button;
        menu.squadScreenPanel.SetActive(false);
        menu.characterScreenPanel.SetActive(true);
        //stats.characterSprite.sprite = Character.Sprite;
        stats.characterName.text = Character.CharacterName;
        
        UpdateStats(Character);
        stats.levelStat.text = Character.Level.ToString();
        int expToNextLevel = Character.ExperienceNeeded();
        int startExp = Character.StartExperience();
        stats.experienceStat.text = string.Format("{0} / {1}", Character.Experience.ToString(), expToNextLevel.ToString());
        int progBar = Character.PercentToNextLevel();
        stats.ProgressBar.value = progBar;
        Debug.Log(Character.Equipment[ItemType.Helm].Name);
        Debug.Log(Character.BaseStats.Intelligence);
        BuildDropdowns(stats);
    }

    public void ReevaluateStats(Text labelText)
    {
        GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        SampleButton button = gameObject.GetComponent<SampleButton>();
        MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        Character = stats.sampleButton.Character;
        string itemName = labelText.text;        
        Equipment item = (Equipment)GlobalConstants.ItemList[itemName];
        Equipment prevItem = null;
        foreach(Equipment charEquipment in Character.Equipment.Values)
        {
            if(charEquipment.ItemType == item.ItemType)
            {
                prevItem = charEquipment;
                break;
            }
        }
        Character.AlteredStats = item.Stats.RemoveAlteredStats(Character.AlteredStats, prevItem.Stats);
        Character.AlteredStats = item.Stats.ConcatStats(Character.AlteredStats, item.Stats);
        Character.Equipment[item.ItemType] = item;
        UpdateStats(Character);
    }

    public void UpdateStats(Character character)
    {
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        SampleButton button = gameObject.GetComponent<SampleButton>();
        CharacterScreen menuStats = statsManager.GetComponent<CharacterScreen>();
        Stats stats = character.BaseStats;
        Stats bonusStats = character.AlteredStats;
        Stats concatStats = stats.ConcatStats(character.BaseStats, character.AlteredStats);
        menuStats.strengthStat.text = formatStats(stats.Strength, bonusStats.Strength);
        menuStats.agilityStat.text = formatStats(stats.Agility, bonusStats.Agility);
        menuStats.intelligenceStat.text = formatStats(stats.Intelligence, bonusStats.Intelligence);
        menuStats.vitalityStat.text = formatStats(stats.Vitality, bonusStats.Vitality);
        menuStats.dexterityStat.text = formatStats(stats.Dexterity, bonusStats.Dexterity);
        menuStats.wisdomStat.text = formatStats(stats.Wisdom, bonusStats.Wisdom);
        menuStats.luckStat.text = formatStats(stats.Luck, bonusStats.Luck);
        menuStats.hitPointsStat.text = concatStats.CalculateHp(character.Level).ToString();
        menuStats.manaStat.text = concatStats.CalculateMp(character.Level).ToString();
        menuStats.damageStat.text = concatStats.CalculateDamage(character.Level).ToString();
        menuStats.magicDamageStat.text = concatStats.CalculateMagicDamage(character.Level).ToString();
        menuStats.speedStat.text = concatStats.CalculateSpeed(character.Level).ToString();
        menuStats.defenseStat.text = concatStats.CalculateDefense(character.Level).ToString();
        menuStats.magicDefenseStat.text = concatStats.CalculateMagicDefense(character.Level).ToString();
        menuStats.hitRateStat.text = concatStats.CalculateHitRate(character.Level).ToString();
        menuStats.dodgeRateStat.text = concatStats.CalculateDodgeRate(character.Level).ToString();
        menuStats.criticalRateStat.text = concatStats.CalculateCritRate(character.Level).ToString();
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
            if (item.ItemType == ItemType.Helm)
            {
                dropdowns.helmSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.Shoulders)
            {
                dropdowns.shoulderSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.Chest)
            {
                dropdowns.chestSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.Legs)
            {
                dropdowns.legsSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.Gloves)
            {
                dropdowns.glovesSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.Boots)
            {
                dropdowns.bootsSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else
            {

            }
        }
        Debug.Log(Character.Equipment[ItemType.Helm].Name);
        dropdowns.helmSlot.GetComponentsInChildren<Text>()[0].text = Character.Equipment[ItemType.Helm].Name;
        dropdowns.shoulderSlot.GetComponentsInChildren<Text>()[0].text = Character.Equipment[ItemType.Shoulders].Name;
        dropdowns.chestSlot.GetComponentsInChildren<Text>()[0].text = Character.Equipment[ItemType.Chest].Name;
        dropdowns.glovesSlot.GetComponentsInChildren<Text>()[0].text = Character.Equipment[ItemType.Gloves].Name;
        dropdowns.legsSlot.GetComponentsInChildren<Text>()[0].text = Character.Equipment[ItemType.Legs].Name;
        dropdowns.bootsSlot.GetComponentsInChildren<Text>()[0].text = Character.Equipment[ItemType.Boots].Name;

    }

    
}

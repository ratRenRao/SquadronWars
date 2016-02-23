using UnityEngine;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class SampleButton : MonoBehaviour
{

    public Button button;
    public CharacterGameObject character;
    public Text nameLabel;
    public CharacterScreen menuStats;

    public void BuildCharacterScreen()
    {        
        var menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        var statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        var button = gameObject.GetComponent<SampleButton>();
        var menu = menuManager.GetComponent<MenuManager>();
        var stats = statsManager.GetComponent<CharacterScreen>();
        var temp = (GameObject)Resources.Load(("Prefabs/Character1"), typeof(GameObject));
        var sprite = temp.GetComponent<SpriteRenderer>();

        stats.sampleButton = button;
        menu.SquadScreenPanel.SetActive(false);
        menu.CharacterScreenPanel.SetActive(true);
        stats.characterSprite.sprite = sprite.sprite;
        stats.characterName.text = character.CharacterClassObject.Name;
        
        UpdateStats(character.CharacterClassObject);

        stats.levelStat.text = character.CharacterClassObject.ToString();
        int expToNextLevel = character.CharacterClassObject.ExperienceNeeded();
     //   int startExp = character.CharacterClassObject.startExperience();
        stats.experienceStat.text = string.Format("{0} / {1}", character.CharacterClassObject.BaseStats.Experience.ToString(), expToNextLevel.ToString());
        int progBar = character.CharacterClassObject.PercentToNextLevel();
        stats.ProgressBar.value = progBar;
        //Debug.Log(character.CharacterClassObject.equipment[ItemType.HELM].name);
        Debug.Log(character.CharacterClassObject.BaseStats.Intl);
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
        character.CharacterClassObject = stats.sampleButton.character.CharacterClassObject;
        string itemName = labelText.text;
        Equipment equipment = character.CharacterClassObject.Equipment; 
        Equipment prevItem = null;

        foreach (var item in equipment.GetItemList())
        {
            /*
            if (item.ItemType == item.ItemType)
            {
                prevItem = charEquipment;
                break;
            }
            */
            character.CharacterClassObject.BaseStats = item.Stats.RemoveAlteredStats(character.CharacterClassObject.CurrentStats, item.Stats);
            character.CharacterClassObject.BaseStats = item.Stats.ConcatStats(character.CharacterClassObject.CurrentStats, item.Stats);
            UpdateStats(character.CharacterClassObject);
        }
    }

    public void UpdateStats(Character character)
    {
        var statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
        var menuStats = statsManager.GetComponent<CharacterScreen>();
        var stats = character.BaseStats;
        var bonusStats = character.CurrentStats;
        var concatStats = stats.ConcatStats(character.BaseStats, character.CurrentStats);
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

    public void incrementStat(string stat)
    {
     //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        var statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        var stats = statsManager.GetComponent<CharacterScreen>();
        character.CharacterClassObject = stats.sampleButton.character.CharacterClassObject;
        if (character.CharacterClassObject.BaseStats.StatPoints > 0)
        {
            if (stat.Equals("strength"))
            {
                character.CharacterClassObject.BaseStats.Str++;                
            }
            if (stat.Equals("agility"))
            {
                character.CharacterClassObject.BaseStats.Agi++;
            }
            if (stat.Equals("intelligence"))
            {
                character.CharacterClassObject.BaseStats.Intl++;
            }
            if (stat.Equals("vitality"))
            {
                character.CharacterClassObject.BaseStats.Vit++;
            }
            if (stat.Equals("dexterity"))
            {
                character.CharacterClassObject.BaseStats.Dex++;
            }
            if (stat.Equals("wisdom"))
            {
                character.CharacterClassObject.BaseStats.Wis++;
            }
            if (stat.Equals("luck"))
            {
                character.CharacterClassObject.BaseStats.Luck++;
            }
            character.CharacterClassObject.BaseStats.StatPoints--;
            UpdateStats(character.CharacterClassObject);

        }
    }

    public void BuildDropdowns(CharacterScreen dropdowns)
    {
        dropdowns.helmSlot.options.Clear();        
        dropdowns.shoulderSlot.options.Clear();
        dropdowns.chestSlot.options.Clear();
        dropdowns.glovesSlot.options.Clear();
        dropdowns.legsSlot.options.Clear();
        dropdowns.bootsSlot.options.Clear();
        foreach(var element in GlobalConstants.Player.Inventory)
        {
            var item = element.Item;

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
        Debug.Log(character.CharacterClassObject.Equipment.Helm.Name);
        dropdowns.helmSlot.GetComponentsInChildren<Text>()[0].text = character.CharacterClassObject.Equipment.Helm.Name;
        dropdowns.shoulderSlot.GetComponentsInChildren<Text>()[0].text = character.CharacterClassObject.Equipment.Shoulders.Name;
        dropdowns.chestSlot.GetComponentsInChildren<Text>()[0].text = character.CharacterClassObject.Equipment.Chest.Name;
        dropdowns.glovesSlot.GetComponentsInChildren<Text>()[0].text = character.CharacterClassObject.Equipment.Gloves.Name;
        dropdowns.legsSlot.GetComponentsInChildren<Text>()[0].text = character.CharacterClassObject.Equipment.Pants.Name;
        dropdowns.bootsSlot.GetComponentsInChildren<Text>()[0].text = character.CharacterClassObject.Equipment.Boots.Name;
    }

    public void LevelSkill(string skill)
    {
     //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
    //    SampleButton button = gameObject.GetComponent<SampleButton>();
    //    MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character.CharacterClassObject = stats.sampleButton.character.CharacterClassObject;
        if (character.CharacterClassObject.BaseStats.SkillPoints > 0)
        {
            var ability = character.CharacterClassObject.Abilities.SingleOrDefault(x => x.Name == skill);
            if (ability != null)
            {
                character.CharacterClassObject.Abilities.Single(x => x.Name == skill).AbilityLevel++;
            }
            else
            {
                character.CharacterClassObject.Abilities.Add(GlobalConstants.AbilityMasterList.Single(x => x.Name == skill));
            }
            if (skill.Equals("fire"))
            {                                
                stats.fireLvl.text = "L" + ability.AbilityLevel;
            }
            if (skill.Equals("cure"))
            {
                stats.cureLvl.text = "L" + ability.AbilityLevel;
            }
            if (skill.Equals("focus"))
            {
                stats.focusLvl.text = "L" + ability.AbilityLevel;
            }
            if (skill.Equals("bash"))
            {
                stats.bashLvl.text = "L" + ability.AbilityLevel;
            }
            character.CharacterClassObject.BaseStats.SkillPoints--;
            UpdateStats(character.CharacterClassObject);

        }
    }

    public void EvaluateSkills()
    {
      //  GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character.CharacterClassObject = stats.sampleButton.character.CharacterClassObject;
        foreach (var ability in character.CharacterClassObject.Abilities)
        {
            if (ability.Name == "fire")
            {
                stats.fireLvl.text = "L" + ability.AbilityLevel;
            }
            else
            {
                stats.fireLvl.text = "";
            }
            if (ability.Name == "bash")
            {
                stats.bashLvl.text = "L" + ability.AbilityLevel;
            }
            else
            {
                stats.bashLvl.text = "";
            }
            if (ability.Name == "cure")
            {
                stats.cureLvl.text = "L" + ability.AbilityLevel;
            }
            else
            {
                stats.cureLvl.text = "";
            }
            if (ability.Name == "focus")
            {
                stats.focusLvl.text = "L" + ability.AbilityLevel;
            }
            else
            {
                stats.focusLvl.text = "";
            }
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

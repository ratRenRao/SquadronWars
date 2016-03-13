using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine.UI;

public class ModifyCharacterStats : MonoBehaviour
{
    public CharacterGameObject characterGameObject { get; set; }
    public Character character { get; set; }
    private  CharacterScreen characterScreen { get; set; }
    private Stats modifiedStats;

    void Start()
    {
        character = characterGameObject.CharacterClassObject;
        characterScreen = GameObject.FindGameObjectWithTag("CharacterStats").GetComponent<CharacterScreen>();
    }

    public void BuildCharacterScreen()
    {
        var temp = (GameObject)Resources.Load(("Prefabs/Character1"), typeof(GameObject));
        var sprite = temp.GetComponent<SpriteRenderer>();
        UpdateStats();

        int expToNextLevel = character.ExperienceNeeded();
        characterScreen.experienceStat.text = string.Format("{0} / {1}", character.BaseStats.Experience.ToString(), expToNextLevel.ToString());
        int progBar = character.PercentToNextLevel();
        characterScreen.ProgressBar.value = progBar;
        Debug.Log(character.BaseStats.Intl);
    }

    public void ReevaluateStats(Text labelText)
    {
        var equipment = character.Equipment;
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
            character.BaseStats = item.Stats.RemoveAlteredStats(character.CurrentStats, item.Stats);
            character.BaseStats = item.Stats.ConcatStats(character.CurrentStats, item.Stats);
            UpdateStats();
        }
    }

    public void UpdateStats()
    {
        Stats stats = modifiedStats == null ? character.BaseStats : modifiedStats;

        var bonusStats = character.CurrentStats;
        var concatStats = stats.ConcatStats(character.BaseStats, character.CurrentStats);
        characterScreen.strengthStat.text = formatStats(stats.Str, bonusStats.Str);
        characterScreen.agilityStat.text = formatStats(stats.Agi, bonusStats.Agi);
        characterScreen.intelligenceStat.text = formatStats(stats.Intl, bonusStats.Intl);
        characterScreen.vitalityStat.text = formatStats(stats.Vit, bonusStats.Vit);
        characterScreen.dexterityStat.text = formatStats(stats.Dex, bonusStats.Dex);
        characterScreen.wisdomStat.text = formatStats(stats.Wis, bonusStats.Wis);
        characterScreen.luckStat.text = formatStats(stats.Luck, bonusStats.Luck);
        characterScreen.hitPointsStat.text = concatStats.CalculateHp(character.LevelId).ToString();
        characterScreen.manaStat.text = concatStats.CalculateMp(character.LevelId).ToString();
        characterScreen.damageStat.text = concatStats.CalculateDamage(character.LevelId).ToString();
        characterScreen.magicDamageStat.text = concatStats.CalculateMagicDamage(character.LevelId).ToString();
        characterScreen.speedStat.text = concatStats.CalculateSpeed(character.LevelId).ToString();
        characterScreen.defenseStat.text = concatStats.CalculateDefense(character.LevelId).ToString();
        characterScreen.magicDefenseStat.text = concatStats.CalculateMagicDefense(character.LevelId).ToString();
        characterScreen.hitRateStat.text = concatStats.CalculateHitRate(character.LevelId).ToString();
        characterScreen.dodgeRateStat.text = concatStats.CalculateDodgeRate(character.LevelId).ToString();
        characterScreen.criticalRateStat.text = concatStats.CalculateCritRate(character.LevelId).ToString();
        characterScreen.remainingStatPoints.text = character.BaseStats.StatPoints.ToString();
        characterScreen.remainingSkillPoints.text = character.BaseStats.SkillPoints.ToString();
    }


    public string formatStats(int stats, int bonusStats)
    {
        return bonusStats == 0 ? stats.ToString() : string.Format("{0} + {1}", stats.ToString(), bonusStats.ToString());
    }

    public void incrementStat(string stat)
    {
        //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        //   SampleButton button = gameObject.GetComponent<SampleButton>();
        //   MenuManager menu = menuManager.GetComponent<MenuManager>();

        modifiedStats = character.BaseStats;

        if (modifiedStats.StatPoints <= 0) return;

        switch (stat)
        {
            case "strength":
                modifiedStats.Str++;
                break;
            case "agility":
                modifiedStats.Agi++;
                break;
            case "intelligence":
                modifiedStats.Intl++;
                break;
            case "vitality":
                modifiedStats.Vit++;
                break;
            case "dexterity":
                modifiedStats.Dex++;
                break;
            case "wisdom":
                modifiedStats.Wis++;
                break;
            case "luck":
                modifiedStats.Luck++;
                break;
        }
        modifiedStats.StatPoints--;
        UpdateStats();
    }

    public void ConfirmStatChanges()
    {
        character.BaseStats = modifiedStats;

    }

    public void RevertStatChanges()
    {
        character.CurrentStats = character.BaseStats;
        UpdateStats();
    }

    /*
    public void BuildDropdowns(CharacterScreen dropdowns)
    {
        dropdowns.helmSlot.options.Clear();
        dropdowns.shoulderSlot.options.Clear();
        dropdowns.chestSlot.options.Clear();
        dropdowns.glovesSlot.options.Clear();
        dropdowns.legsSlot.options.Clear();
        dropdowns.bootsSlot.options.Clear();
        foreach (var element in GlobalConstants.Player.Inventory)
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
        Debug.Log(character.Equipment.Helm.Name);
        dropdowns.helmSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Helm.Name;
        dropdowns.shoulderSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Shoulders.Name;
        dropdowns.chestSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Chest.Name;
        dropdowns.glovesSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Gloves.Name;
        dropdowns.legsSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Pants.Name;
        dropdowns.bootsSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Boots.Name;
    }

    public void LevelSkill(string skill)
    {
        //   GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        //    SampleButton button = gameObject.GetComponent<SampleButton>();
        //    MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        character = stats.sampleButton.character;
        if (character.BaseStats.SkillPoints > 0)
        {
            var ability = character.Abilities.SingleOrDefault(x => x.Name == skill);
            if (ability != null)
            {
                character.Abilities.Single(x => x.Name == skill).AbilityLevel++;
            }
            else
            {
                character.Abilities.Add(GlobalConstants.AbilityMasterList.Single(x => x.Name == skill));
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
            character.BaseStats.SkillPoints--;
            UpdateStats();

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
        foreach (var ability in character.Abilities)
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
    */
    
}

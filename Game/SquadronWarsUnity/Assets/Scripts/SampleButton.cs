using UnityEngine;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using Assets.Data;

public class SampleButton : MonoBehaviour
{

    public Button button;
    public CharacterGameObject characterGameObject;
    public Character character { get; set; }
    public CharacterScreen characterScreen;
    private Stats modifiedStats { get; set; }
    public Text nameLabel;

    /*void Start()
    {
        character = characterGameObject.CharacterClassObject;
        characterScreen = GameObject.FindGameObjectWithTag("CharacterStats").GetComponent<CharacterScreen>();
    }*/

    public void BuildCharacterScreen()
    {
        characterScreen = GameObject.FindGameObjectWithTag("CharacterStats").GetComponent<CharacterScreen>();
        var temp = (GameObject)Resources.Load(("Prefabs/Character1"), typeof(GameObject));
        var button = gameObject.GetComponent<SampleButton>();
        var menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        var sprite = temp.GetComponent<SpriteRenderer>();

        SetActiveCharacter();
        UpdateStats();

        characterScreen.sampleButton = button;
        menu.SquadScreenPanel.SetActive(false);
        menu.CharacterScreenPanel.SetActive(true);
        characterScreen.characterSprite.sprite = sprite.sprite;
        characterScreen.characterName.text = characterGameObject.CharacterClassObject.Name;



        int expToNextLevel = character.ExperienceNeeded();
        characterScreen.experienceStat.text = string.Format("{0} / {1}", character.BaseStats.Experience.ToString(), expToNextLevel.ToString());
        int progBar = character.PercentToNextLevel();
        characterScreen.ProgressBar.value = progBar;
        BuildDropdowns(characterScreen);
        GlobalConstants.curSelectedCharacter = character;
        Debug.Log(character);
    }

    public void SetActiveCharacter()
    {
        //var unityCharacter = GameObject.FindGameObjectWithTag("Character1").GetComponent<CharacterGameObject>();
        //unityCharacter = characterGameObject;
        //unityCharacter.CharacterClassObject = character;

        GlobalConstants.ActiveCharacterGameObject = characterGameObject;
    }

    public void GetActiveCharacter()
    {
        characterGameObject = GlobalConstants.ActiveCharacterGameObject;
        character = characterGameObject.CharacterClassObject;
    }

    public void ReevaluateStats(Text labelText)
    {
        GetActiveCharacter();
        var item = GlobalConstants.ItemsMasterList.Single(x => x.Name == labelText.text);

        character.Equipment.SetItemByType(item);
        character.CurrentStats = StartupData.AddItemStats(character.Equipment.GetItemList(), character.BaseStats);
        /*
        foreach (var equipedItem in character.Equipment.GetItemList())
        {
            character.CurrentStats = item.Stats.ConcatStats(character.BaseStats, equipedItem.Stats);
        }

            character.CurrentStats = item.Stats.RemoveAlteredStats(character.CurrentStats, item.Stats);
            character.CurrentStats = item.Stats.ConcatStats(character.CurrentStats, item.Stats);
            UpdateStats();
        }
        */
    }

    public void UpdateStats()
    {
        var characterStats = GameObject.FindGameObjectWithTag("CharacterStats");
        var menuScreen = characterStats.GetComponent <CharacterScreen>();

        var stats = modifiedStats == null ? character.BaseStats : modifiedStats;
        var bonusStats = character.CurrentStats;
        var concatStats = stats.ConcatStats(stats, character.CurrentStats);
        menuScreen.strengthStat.text = formatStats(stats.Str, bonusStats.Str);
        menuScreen.agilityStat.text = formatStats(stats.Agi, bonusStats.Agi);
        menuScreen.intelligenceStat.text = formatStats(stats.Intl, bonusStats.Intl);
        menuScreen.vitalityStat.text = formatStats(stats.Vit, bonusStats.Vit);
        menuScreen.dexterityStat.text = formatStats(stats.Dex, bonusStats.Dex);
        menuScreen.wisdomStat.text = formatStats(stats.Wis, bonusStats.Wis);
        menuScreen.luckStat.text = formatStats(stats.Luck, bonusStats.Luck);
        menuScreen.hitPointsStat.text = concatStats.CalculateHp(character.LevelId).ToString();
        menuScreen.manaStat.text = concatStats.CalculateMp(character.LevelId).ToString();
        menuScreen.damageStat.text = concatStats.CalculateDamage(character.LevelId).ToString();
        menuScreen.magicDamageStat.text = concatStats.CalculateMagicDamage(character.LevelId).ToString();
        menuScreen.speedStat.text = concatStats.CalculateSpeed(character.LevelId).ToString();
        menuScreen.defenseStat.text = concatStats.CalculateDefense(character.LevelId).ToString();
        menuScreen.magicDefenseStat.text = concatStats.CalculateMagicDefense(character.LevelId).ToString();
        menuScreen.hitRateStat.text = concatStats.CalculateHitRate(character.LevelId).ToString();
        menuScreen.dodgeRateStat.text = concatStats.CalculateDodgeRate(character.LevelId).ToString();
        menuScreen.criticalRateStat.text = concatStats.CalculateCritRate(character.LevelId).ToString();
        menuScreen.remainingStatPoints.text = stats.StatPoints.ToString();
        menuScreen.remainingSkillPoints.text = stats.SkillPoints.ToString();
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

        //characterGameObject = gameObject.GetComponent<SampleButton>().characterGameObject;
        //character = GlobalConstants.Player.Characters.Single(x => x.CharacterId == characterGameObject.GetCharacterId());

        //characterGameObject = GameObject.FindGameObjectWithTag("Character1").GetComponent<CharacterGameObject>();
        //character = characterGameObject.CharacterClassObject;

        GetActiveCharacter();
        if(modifiedStats == null)
            modifiedStats = character.BaseStats.Clone();

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
        modifiedStats = character.BaseStats.Clone();
    }

    public void RevertStatChanges()
    {
        modifiedStats = character.BaseStats.Clone();
        //character.CurrentStats = character.BaseStats;
        UpdateStats();
    }

    public void BuildDropdowns(CharacterScreen dropdowns)
    {
        GetActiveCharacter();

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
        SampleButton button = gameObject.GetComponent<SampleButton>();
    //    MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        //character = stats.sampleButton.character;

        GetActiveCharacter();
        if (character.BaseStats.SkillPoints > 0)
        {
            var ability = character.Abilities.SingleOrDefault(x => x.Name.ToLower() == skill.ToLower());
            if (ability != null)
            {
                character.Abilities.Single(x => x.Name.ToLower() == skill.ToLower()).AbilityLevel++;
            }
            else
            {
                ability = GlobalConstants.AbilityMasterList.Single(x => x.Name.ToLower() == skill.ToLower());
                ability.AbilityLevel++;
                character.Abilities.Add(ability);
            }
            if (skill.Equals("fire"))
            {
                characterScreen.fireLvl.text = "L" + ability.AbilityLevel;
                //stats.fireLvl.text = "L" + ability.AbilityLevel;
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

    public void SaveCharacter()
    {
        Debug.Log(GlobalConstants.curSelectedCharacter);
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
}

using UnityEngine;
using Assets.GameClasses;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using Assets.Data;
using UnityEngine.SceneManagement;

public class SampleButton : MonoBehaviour
{

    public Button button;
    public CharacterGameObject characterGameObject;
    
    public Character character { get; set; }
    public CharacterScreen characterScreen;
    private Stats modifiedStats { get; set; }
    public Text nameLabel;
    public GameObject CharacterScreenPanel, SquadPanel, SquadScreen;

    /*void Start()
    {
        character = characterGameObject.CharacterClassObject;
        characterScreen = GameObject.FindGameObjectWithTag("CharacterStats").GetComponent<CharacterScreen>();
    }*/

    public void BuildCharacterScreen()
    {
        characterScreen = GameObject.FindGameObjectWithTag("CharacterStats").GetComponent<CharacterScreen>();
        var temp = (GameObject)Resources.Load(("Prefabs/Character" + characterGameObject.CharacterClassObject.SpriteId), typeof(GameObject));
        var button = gameObject.GetComponent<SampleButton>();
        var menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        var sprite = temp.GetComponent<SpriteRenderer>();        
        SetActiveCharacter();
        UpdateStats(character.BaseStats);

        characterScreen.sampleButton = button;
        menu.SquadScreenPanel.SetActive(false);
        menu.CharacterScreenPanel.SetActive(true);
        characterScreen.characterSprite.sprite = sprite.sprite;
        characterScreen.characterName.text = characterGameObject.CharacterClassObject.Name;
        characterScreen.levelStat.text = characterGameObject.CharacterClassObject.LevelId.ToString();



        int expToNextLevel = character.ExperienceNeeded();
        characterScreen.experienceStat.text = string.Format("{0} / {1}", character.BaseStats.Experience.ToString(), expToNextLevel.ToString());
        Debug.Log(character.PercentToNextLevel());
        int progBar = character.PercentToNextLevel();
        characterScreen.ProgressBar.value = progBar;
        BuildDropdowns(characterScreen);
        GlobalConstants.curSelectedCharacter = character;
        modifiedStats = character.BaseStats.Clone();
        //Debug.Log(character);
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
        /*Debug.Log(item.Stats.Intl);
        Debug.Log(item.Stats.Str);        
        Debug.Log(item.Stats.Vit);
        Debug.Log(item.Stats.Wis);
        Debug.Log(item.Name);
        Debug.Log(item.Stats.Dex);
        Debug.Log(item.Stats.Agi);
        Debug.Log(item.Stats.Luck);
        //Debug.Log(GlobalConstants.Player.Characters[0].BaseStats.Str);*/

 
        foreach (var equipedItem in character.Equipment.GetItemList())
        {
            character.CurrentStats = item.Stats.ConcatStats(character.BaseStats, equipedItem.Stats);
        }

            character.CurrentStats = item.Stats.RemoveAlteredStats(character.CurrentStats, item.Stats);
            character.CurrentStats = item.Stats.ConcatStats(character.CurrentStats, item.Stats);
            UpdateStats(character.BaseStats);
        
    }

    public void UpdateStats(Stats concatStats)
    {
        var characterStats = GameObject.FindGameObjectWithTag("CharacterStats");
        var menuScreen = characterStats.GetComponent <CharacterScreen>();
        if(GlobalConstants.ChangeStatsObject)
        {
            GlobalConstants.ChangeStatsObject = false;
            GlobalConstants.curSelectedCharacter = character;
            GlobalConstants.CurrentModifiedStats = GlobalConstants.curSelectedCharacter.BaseStats.Clone();
            EvaluateSkills();
            
        }

        var baseStats = character.BaseStats;//stats.ConcatStats(stats, character.CurrentStats);
        menuScreen.strengthStat.text = formatStats(concatStats.Str, concatStats.Str);
        menuScreen.agilityStat.text = formatStats(concatStats.Agi, concatStats.Agi);
        menuScreen.intelligenceStat.text = formatStats(concatStats.Intl, concatStats.Intl);
        menuScreen.vitalityStat.text = formatStats(concatStats.Vit, concatStats.Vit);
        menuScreen.dexterityStat.text = formatStats(concatStats.Dex, concatStats.Dex);
        menuScreen.wisdomStat.text = formatStats(concatStats.Wis, concatStats.Wis);
        menuScreen.luckStat.text = formatStats(concatStats.Luck, concatStats.Luck);
        Debug.Log(concatStats.CalculateHp(character.LevelId).ToString());
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
        menuScreen.remainingStatPoints.text = concatStats.StatPoints.ToString();
        menuScreen.remainingSkillPoints.text = concatStats.SkillPoints.ToString();
       // Debug.Log("MenuScreen" + stats.SkillPoints.ToString());
    }


    public string formatStats(int stats, int bonusStats)
    {
        return stats.ToString();
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
        //if(modifiedStats == null)
        // modifiedStats = character.BaseStats.Clone();
        modifiedStats = GlobalConstants.CurrentModifiedStats;

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
        
        UpdateStats(modifiedStats);
    }

    public void ConfirmStatChanges()
    {
        if (modifiedStats != null)
        {
            character.BaseStats = modifiedStats;
            modifiedStats = character.BaseStats.Clone();
            for (int i = 0; i < GlobalConstants.Player.Characters.Count; i++)
            {
                if (GlobalConstants.Player.Characters[i].CharacterId == character.CharacterId)
                {
                    GlobalConstants.Player.Characters[i] = character;
                }
            }
        }
    }

    public void RevertStatChanges()
    {
        //modifiedStats = character.BaseStats.Clone();
        GlobalConstants.ChangeStatsObject = true;
        //character.CurrentStats = character.BaseStats;
        //UpdateStats(modifiedStats);
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
        dropdowns.mainHandSlot.options.Clear();
        dropdowns.offHandSlot.options.Clear();
        dropdowns.accessory1Slot.options.Clear();
        dropdowns.accessory2Slot.options.Clear();
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
            else if (item.ItemType == ItemType.MainHand)
            {
                dropdowns.mainHandSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.OffHand)
            {
                dropdowns.offHandSlot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else if (item.ItemType == ItemType.Accessory)
            {
                dropdowns.accessory1Slot.options.Add(new Dropdown.OptionData() { text = item.Name });
                dropdowns.accessory2Slot.options.Add(new Dropdown.OptionData() { text = item.Name });
            }
            else
            {
                //empty for now
            }
        }
        //Debug.Log(character.Equipment.Helm.Name);
        dropdowns.helmSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Helm.Name;
        dropdowns.shoulderSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Shoulders.Name;
        dropdowns.chestSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Chest.Name;
        dropdowns.glovesSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Gloves.Name;
        dropdowns.legsSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Pants.Name;
        dropdowns.bootsSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Boots.Name;
        dropdowns.mainHandSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Weapon.Name;
        dropdowns.offHandSlot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Offhand.Name;
        dropdowns.accessory1Slot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Accessory1.Name;
        dropdowns.accessory2Slot.GetComponentsInChildren<Text>()[0].text = character.Equipment.Accessory2.Name;
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
                GlobalConstants.curSelectedCharacter.Abilities.Single(x => x.Name.ToLower() == skill.ToLower()).AbilityLevel++;
            }
            else
            {
                ability = GlobalConstants.AbilityMasterList.Single(x => x.Name.ToLower() == skill.ToLower());
                ability.AbilityLevel++;
                ability.CharacterId = GlobalConstants.curSelectedCharacter.CharacterId;
                GlobalConstants.curSelectedCharacter.Abilities.Add(ability);
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
            UpdateStats(character.BaseStats);

        }
    }

    public void SaveCharacter()
    {
        //Debug.Log(GlobalConstants.curSelectedCharacter);
        //SetDbConnection();
        var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.UpdateCharacterUrl, new UpdateCharacterPostObject(modifiedStats));

        if (!www.text.Equals("Failed"))
        {
            ConfirmStatChanges();            
            GlobalConstants.ResetCharacters();
            GlobalConstants.ChangeStatsObject = true;
            CharacterScreenPanel.SetActive(false);
            SquadPanel.SetActive(true);
            SquadScreen.SetActive(true);
        }
    }

    private void SetDbConnection()
    {
        gameObject.AddComponent<DbConnection>();
        GlobalConstants._dbConnection = gameObject.GetComponent<DbConnection>();
    }

    /*
    public void SendBattleMessage()
    {
        /**
        //GlobalConstants.GameId = 14;
        //GlobalConstants.myPlayerId = 1;
        //GlobalConstants.player1Characters = GlobalConstants.Player.Characters;
        GlobalConstants.currentActions = new BattleAction();
        Tile testTile = new Tile();
        testTile.x = 1;
        testTile.y = 1;
        GlobalConstants.currentActions.AddAction(new Action(Action.ActionType.Move, new System.Collections.Generic.List<Tile> { testTile }, "test Action"));
        GlobalConstants.currentActions.CharacterQueue = new System.Collections.Generic.List<int> { 1, 2 };
        GlobalConstants.currentActions.AddAffectedTile(testTile, 1);

        BattlePostObject test = new BattlePostObject();

        var www = GlobalConstants._dbConnection.SendPostData(GlobalConstants.StartGameUrl, test);
        Debug.Log(www);
        /**
        //SceneManager.LoadScene("Login");
    }
    */

    public void EvaluateSkills()
    {
      //  GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
     //   SampleButton button = gameObject.GetComponent<SampleButton>();
     //   MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        //character = stats.sampleButton.character;
        //foreach (var ability in character.Abilities)
        stats.hasteLvl.text = "";
        stats.regenLvl.text = "";
        stats.bioLvl.text = "";
        stats.iceLvl.text = "";
        stats.flameStrikeLvl.text = "";
        stats.armorBreakLvl.text = "";
        stats.chargeLvl.text = "";
        stats.doubleAttackLvl.text = "";
        stats.fireLvl.text = "";
        stats.bashLvl.text = "";
        stats.cureLvl.text = "";
        stats.focusLvl.text = "";
        foreach (var ability in GlobalConstants.curSelectedCharacter.Abilities)
        {
            if (ability.Name.Equals("Fire"))
            {
                stats.fireLvl.text = "L" + ability.AbilityLevel;
                continue;
            }
            if (ability.Name.Equals("Bash"))
            {
                stats.bashLvl.text = "L" + ability.AbilityLevel;
                continue;
            }
            if (ability.Name.Equals("Cure"))
            {
                stats.cureLvl.text = "L" + ability.AbilityLevel;
                continue;
            }
            if (ability.Name.Equals("Focus"))
            {
                stats.focusLvl.text = "L" + ability.AbilityLevel;
                continue;
            }
        }
        
    }

}

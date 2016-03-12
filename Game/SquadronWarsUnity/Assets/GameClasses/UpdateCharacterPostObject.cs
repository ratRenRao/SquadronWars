using UnityEngine;
using System.Collections;
using Assets.GameClasses;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class UpdateCharacterPostObject : IJsonable
{

    public string username { get; set; }
    public string password { get; set; }
    public string name { get; set; }
    public int characterId { get; set; }
    public int statPoints { get; set; }
    public int skillPoints { get; set; }
    public int luck { get; set; }
    public int LevelID { get; set; }
    public int experience { get; set; }
    public int helm { get; set; }
    public int chest { get; set; }
    public int gloves { get; set; }
    public int pants { get; set; }
    public int shoulders { get; set; }
    public int boots { get; set; }
    public int accessory1 { get; set; }
    public int accessory2 { get; set; }
    public int IsStandard { get; set; }
    public int strength { get; set; }
    public int intelligence { get; set; }
    public int agility { get; set; }
    public int wisdom { get; set; }
    public int vitality { get; set; }
    public int dexterity { get; set; }
    public int spriteId { get; set; }
    public Character character { get; set; }
    public Stats modified { get; set; }

public string GetJsonObjectName()
    {
        return "UpdateCharacterPostObject";
    }

    public List<PropertyInfo> GetJsonObjectParameters()
    {
        return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
    }

    public void SetJsonObjectParameters(Dictionary<string, object> parameters)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public UpdateCharacterPostObject()
    {
        username = GlobalConstants.Player.logins.username;
        password = GlobalConstants.Player.logins.password;
        name = GlobalConstants.curSelectedCharacter.Name;
        characterId = GlobalConstants.curSelectedCharacter.CharacterId;
        statPoints = GlobalConstants.curSelectedCharacter.BaseStats.StatPoints;
        skillPoints = GlobalConstants.curSelectedCharacter.BaseStats.SkillPoints;
        luck = GlobalConstants.curSelectedCharacter.BaseStats.Luck;
        LevelID = GlobalConstants.curSelectedCharacter.LevelID;
        experience = GlobalConstants.curSelectedCharacter.BaseStats.Experience;
        helm = GlobalConstants.curSelectedCharacter.Equipment.Helm.ItemId;
        chest = GlobalConstants.curSelectedCharacter.Equipment.Chest.ItemId;
        gloves = GlobalConstants.curSelectedCharacter.Equipment.Gloves.ItemId;
        pants = GlobalConstants.curSelectedCharacter.Equipment.Pants.ItemId;
        shoulders = GlobalConstants.curSelectedCharacter.Equipment.Shoulders.ItemId;
        boots = GlobalConstants.curSelectedCharacter.Equipment.Boots.ItemId;
        accessory1 = GlobalConstants.curSelectedCharacter.Equipment.Accessory1.ItemId;
        accessory2 = GlobalConstants.curSelectedCharacter.Equipment.Accessory2.ItemId;
        IsStandard = 0;
        strength = GlobalConstants.curSelectedCharacter.BaseStats.Str;
        intelligence = GlobalConstants.curSelectedCharacter.BaseStats.Intl;
        agility = GlobalConstants.curSelectedCharacter.BaseStats.Agi;
        wisdom = GlobalConstants.curSelectedCharacter.BaseStats.Wis;
        vitality = GlobalConstants.curSelectedCharacter.BaseStats.Vit;
        dexterity = GlobalConstants.curSelectedCharacter.BaseStats.Dex;
        spriteId = GlobalConstants.curSelectedCharacter.SpriteId;
        character = GlobalConstants.curSelectedCharacter;
    }

    public UpdateCharacterPostObject(Stats modifiedstats)
    {
        username = GlobalConstants.Player.logins.username;
        password = GlobalConstants.Player.logins.password;
        name = GlobalConstants.curSelectedCharacter.Name;
        characterId = GlobalConstants.curSelectedCharacter.CharacterId;
        statPoints = GlobalConstants.curSelectedCharacter.BaseStats.StatPoints;
        skillPoints = GlobalConstants.curSelectedCharacter.BaseStats.SkillPoints;
        luck = GlobalConstants.curSelectedCharacter.BaseStats.Luck;
        LevelID = GlobalConstants.curSelectedCharacter.LevelID;
        experience = GlobalConstants.curSelectedCharacter.BaseStats.Experience;
        helm = GlobalConstants.curSelectedCharacter.Equipment.Helm.ItemId;
        chest = GlobalConstants.curSelectedCharacter.Equipment.Chest.ItemId;
        gloves = GlobalConstants.curSelectedCharacter.Equipment.Gloves.ItemId;
        pants = GlobalConstants.curSelectedCharacter.Equipment.Pants.ItemId;
        shoulders = GlobalConstants.curSelectedCharacter.Equipment.Shoulders.ItemId;
        boots = GlobalConstants.curSelectedCharacter.Equipment.Boots.ItemId;
        accessory1 = GlobalConstants.curSelectedCharacter.Equipment.Accessory1.ItemId;
        accessory2 = GlobalConstants.curSelectedCharacter.Equipment.Accessory2.ItemId;
        IsStandard = 0;
        strength = GlobalConstants.curSelectedCharacter.BaseStats.Str;
        intelligence = GlobalConstants.curSelectedCharacter.BaseStats.Intl;
        agility = GlobalConstants.curSelectedCharacter.BaseStats.Agi;
        wisdom = GlobalConstants.curSelectedCharacter.BaseStats.Wis;
        vitality = GlobalConstants.curSelectedCharacter.BaseStats.Vit;
        dexterity = GlobalConstants.curSelectedCharacter.BaseStats.Dex;
        spriteId = GlobalConstants.curSelectedCharacter.SpriteId;
        character = GlobalConstants.curSelectedCharacter;
        modified = modifiedstats;
    }

}

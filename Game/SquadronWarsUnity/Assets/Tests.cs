﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using Assets.GameClasses;

namespace Assets
{
    class Tests
    {
        public void Run()
        {
            CheckJsonPostData();
        }

        public void CheckJsonPostData()
        {
            //string json = "{\"GameInfo\":{\"gameID\":\"14\",\"player1Id\":\"9\",\"player2Id\":\"17\",\"GameJSON\":{\"ActionOrder\":{\"0\":{\"actionType\":\"Move\",\"performedAction\":\"test Action\",\"actionTiles\":{\"0\":{\"x\":\"1\",\"y\":\"1\"}}}},\"CharacterQueue\":{\"0\":1,\"1\":2},\"AffectedTiles\":{\"0\":{\"Tile\":{\"x\":\"1\",\"y\":\"1\"},\"Amount\":1}}},\"CreateTime\":\"2016-02-26 23:02:54\",\"ModifyTime\":\"2016-03-17 06:36:20\",\"Finished\":\"\u0000\",\"character1Info\":\"{\"0\":{\"CharacterId\":\"1\",\"LevelId\":\"1\",\"Name\":\"lancelot\",\"SpriteId\":\"1\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":7,\"Agi\":6,\"Intl\":6,\"Vit\":6,\"Wis\":6,\"Dex\":6,\"Luck\":6,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":18,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":7,\"Agi\":6,\"Intl\":6,\"Vit\":6,\"Wis\":6,\"Dex\":6,\"Luck\":6,\"HitPoints\":79,\"Dmg\":56,\"MagicAttack\":55,\"Speed\":5,\"Defense\":45,\"MagicDefense\":39,\"HitRate\":100,\"DodgeRate\":88,\"CritRate\":55,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":1,\"Chest\":1000,\"Gloves\":3000,\"Pants\":2000,\"Shoulders\":4000,\"Boots\":5000,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{\"0\":{\"Name\":\"Fire\",\"AbilityId\":1,\"AbilityLevel\":1,\"CharacterId\":1},\"1\":{\"Name\":\"Cure\",\"AbilityId\":2,\"AbilityLevel\":1,\"CharacterId\":1}}},\"1\":{\"CharacterId\":\"2\",\"LevelId\":\"1\",\"Name\":\"Arthur\",\"SpriteId\":\"2\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":1,\"Chest\":1000,\"Gloves\":3000,\"Pants\":2000,\"Shoulders\":4000,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{\"0\":{\"Name\":\"Fire\",\"AbilityId\":1,\"AbilityLevel\":1,\"CharacterId\":2},\"1\":{\"Name\":\"Cure\",\"AbilityId\":2,\"AbilityLevel\":1,\"CharacterId\":2}}},\"2\":{\"CharacterId\":\"4\",\"LevelId\":\"1\",\"Name\":\"Pat\",\"SpriteId\":\"3\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}},\"3\":{\"CharacterId\":\"5\",\"LevelId\":\"1\",\"Name\":\"Kelly\",\"SpriteId\":\"4\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}},\"4\":{\"CharacterId\":\"14\",\"LevelId\":\"1\",\"Name\":\"Quenavier\",\"SpriteId\":\"1\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}},\"5\":{\"CharacterId\":\"15\",\"LevelId\":\"1\",\"Name\":\"Johnny Boy\",\"SpriteId\":\"1\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}}}\",\"character2Info\":\"testing\"}}";
            string json =
                "{\"GameInfo\":{\"gameID\":\"14\",\"player1Id\":\"9\",\"player2Id\":\"17\",\"GameJSON\":{\"ActionOrder\":[{\"actionType\":\"Move\",\"performedAction\":\"test Action\",\"actionTiles\":[{\"x\":\"1\",\"y\":\"1\"}]]},\"CharacterQueue\":[{1},{2}],\"AffectedTiles\":{\"0\":{\"Tile\":{\"x\":\"1\",\"y\":\"1\"},\"Amount\":1}}},\"CreateTime\":\"2016-02-26 23:02:54\",\"ModifyTime\":\"2016-03-17 06:36:20\",\"Finished\":\"\u0000\",\"character1Info\":\"{\"0\":{\"CharacterId\":\"1\",\"LevelId\":\"1\",\"Name\":\"lancelot\",\"SpriteId\":\"1\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":7,\"Agi\":6,\"Intl\":6,\"Vit\":6,\"Wis\":6,\"Dex\":6,\"Luck\":6,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":18,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":7,\"Agi\":6,\"Intl\":6,\"Vit\":6,\"Wis\":6,\"Dex\":6,\"Luck\":6,\"HitPoints\":79,\"Dmg\":56,\"MagicAttack\":55,\"Speed\":5,\"Defense\":45,\"MagicDefense\":39,\"HitRate\":100,\"DodgeRate\":88,\"CritRate\":55,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":1,\"Chest\":1000,\"Gloves\":3000,\"Pants\":2000,\"Shoulders\":4000,\"Boots\":5000,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{\"0\":{\"Name\":\"Fire\",\"AbilityId\":1,\"AbilityLevel\":1,\"CharacterId\":1},\"1\":{\"Name\":\"Cure\",\"AbilityId\":2,\"AbilityLevel\":1,\"CharacterId\":1}}},\"1\":{\"CharacterId\":\"2\",\"LevelId\":\"1\",\"Name\":\"Arthur\",\"SpriteId\":\"2\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":1,\"Chest\":1000,\"Gloves\":3000,\"Pants\":2000,\"Shoulders\":4000,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{\"0\":{\"Name\":\"Fire\",\"AbilityId\":1,\"AbilityLevel\":1,\"CharacterId\":2},\"1\":{\"Name\":\"Cure\",\"AbilityId\":2,\"AbilityLevel\":1,\"CharacterId\":2}}},\"2\":{\"CharacterId\":\"4\",\"LevelId\":\"1\",\"Name\":\"Pat\",\"SpriteId\":\"3\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}},\"3\":{\"CharacterId\":\"5\",\"LevelId\":\"1\",\"Name\":\"Kelly\",\"SpriteId\":\"4\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}},\"4\":{\"CharacterId\":\"14\",\"LevelId\":\"1\",\"Name\":\"Quenavier\",\"SpriteId\":\"1\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}},\"5\":{\"CharacterId\":\"15\",\"LevelId\":\"1\",\"Name\":\"Johnny Boy\",\"SpriteId\":\"1\",\"X\":\"0\",\"Y\":\"0\",\"BaseStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":0,\"Dmg\":0,\"MagicAttack\":0,\"Speed\":0,\"Defense\":0,\"MagicDefense\":0,\"HitRate\":0,\"DodgeRate\":0,\"CritRate\":0,\"StatPoints\":25,\"SkillPoints\":1},\"CurrentStats\":{\"Str\":1,\"Agi\":1,\"Intl\":1,\"Vit\":1,\"Wis\":1,\"Dex\":1,\"Luck\":0,\"HitPoints\":17,\"Dmg\":10,\"MagicAttack\":10,\"Speed\":5,\"Defense\":9,\"MagicDefense\":9,\"HitRate\":60,\"DodgeRate\":58,\"CritRate\":37,\"StatPoints\":0,\"SkillPoints\":0},\"Equipment\":{\"Helm\":0,\"Chest\":0,\"Gloves\":0,\"Pants\":0,\"Shoulders\":0,\"Boots\":0,\"Accessory1\":0,\"Accessory2\":0},\"Abilities\":{}}}\",\"character2Info\":\"testing\"}}";

            var turnData = GlobalConstants.Utilities.BuildObjectFromJsonData<GameInfo>(json.ToString());
            //var turnData = GlobalConstants._dbConnection.PopulateObjectFromDb<GameInfo>(GlobalConstants.CheckGameStatusUrl, GlobalConstants.Player.logins);
        }
    }
}

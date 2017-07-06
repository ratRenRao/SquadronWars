DELIMITER &&
 
CREATE PROCEDURE sp_CreateCharacter(OUT p_characterId int,IN p_SquadID int ,  IN p_LevelID int , IN p_name varchar(45) , IN p_experience int , IN p_helm int , IN p_chest int , IN p_gloves int , IN p_pants int ,IN  p_shoulders int ,IN  p_boots int , IN p_accessory1 int , IN p_accessory2 int , IN p_IsStandard tinyint, IN p_str int , IN p_int int , IN p_agi int , IN p_wis int ,IN  p_vit int ,IN  p_dex int ,IN  p_hitPoints int ,IN  p_dmg int ,IN  p_abilityPoints int , IN p_speed int ,IN  p_defense int , IN p_magicDefense int ,IN  p_magicAttack int ,IN  p_hitRate int ,IN  p_critRate int , IN p_dodgeRate int)
BEGIN	
    
    declare v_StatId int;
    
    INSERT INTO dbo.Stats (str , `int`, agi, wis, vit, dex, hitPoints, dmg, abilityPoints, speed, defense, magicDefense, magicAttack, hitRate, critRate, dodgeRate)
    VALUES (p_str, p_int, p_agi, p_wis, p_vit, p_dex, p_hitPoints, p_dmg, p_abilityPoints, p_speed, p_defense, p_magicDefense, p_magicAttack, p_hitRate, p_critRate, p_dodgeRate);
    
    SELECT LAST_INSERT_ID() INTO v_StatId;
    
    INSERT INTO dbo.`Character` (SquadID, statId, LevelID, `name`, experience, helm, chest, gloves, pants, shoulders, boots, accessory1, accessory2, IsStandard)
    VALUES  (p_SquadID, v_StatId, p_LevelID, p_name, p_experience, p_helm, p_chest, p_gloves, p_pants, p_shoulders, p_boots, p_accessory1, p_accessory2, p_IsStandard);
    
    SELECT LAST_INSERT_ID() INTO p_characterId;
   
   END&&

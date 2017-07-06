DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_UpdateCharacter
-- -----------------------------------------------------


CREATE PROCEDURE sp_UpdateCharacter(IN p_characterId int,IN p_SquadID int ,  IN p_LevelID int , IN p_name varchar(45) , IN p_experience int , IN p_helm int , IN p_chest int , IN p_gloves int , IN p_pants int ,IN  p_shoulders int ,IN  p_boots int , IN p_accessory1 int , IN p_accessory2 int , IN p_IsStandard tinyint, IN p_str int , IN p_int int , IN p_agi int , IN p_wis int ,IN  p_vit int ,IN  p_dex int ,IN  p_hitPoints int ,IN  p_dmg int ,IN  p_abilityPoints int , IN p_speed int ,IN  p_defense int , IN p_magicDefense int ,IN  p_magicAttack int ,IN  p_hitRate int ,IN  p_critRate int , IN p_dodgeRate int)
BEGIN	
    
    declare v_StatId int;
    
    select v_StatId = statId from dbo.`Character` where characterId = p_characterId;

    
    UPDATE dbo.`Character`
    set SquadID = p_SquadID, 
    LevelID = LevelID, 
    name = p_name, 
    experience = p_experience, 
    helm= p_helm, 
    chest= p_chest, 
    gloves= p_gloves, 
    pants= p_pants, 
    shoulders= p_shoulders, 
    boots= p_boots, 
    accessory1= p_accessory1, 
    accessory2= p_accessory2, 
    IsStandard =  p_IsStandard
    WHERE characterId = p_characterId;
    
    
    
    UPDATE sbo.Stats
    SET str = p_str , 
    `int` = p_int, 
    agi= p_agi,
    wis= p_wis,
    vit= p_vit,
    dex= p_dex,
    hitPoints= p_hitPoints,
    dmg= p_dmg,
    abilityPoints= p_abilityPoints,
    speed= p_speed,
    defense= p_defense,
    magicDefense= p_magicDefense,
    magicAttack= p_magicAttack,
    hitRate= p_hitRate, 
    critRate= p_critRate,
    dodgeRate= p_dodgeRate
    where StatId = v_StatId;
    
    
  
    
END&&

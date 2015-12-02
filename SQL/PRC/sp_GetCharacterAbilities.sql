-- -----------------------------------------------------
-- procedure sp_GetCharacterAbilities
-- -----------------------------------------------------


CREATE PROCEDURE sp_GetCharacterAbilities(IN p_playerId int)
BEGIN	
    
    
 select cha.abilityId, cha.characterId from
    (select * from dbo.Squad where playerId = p_playerId) sq
    left join
    (select * from dbo.Character) ch
    on sq.SquadID = ch.SquadId
    left join
    (select * From dbo.CharacterAbilities)cha
    on ch.characterId = cha.characterId;
 
    
END
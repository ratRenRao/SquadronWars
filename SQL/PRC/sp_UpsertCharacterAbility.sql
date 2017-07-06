DELIMITER &&
 
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpsertCharacterAbility`(in p_characterId INT, in p_abilityId INT, in p_abilityLevel INT)
BEGIN
	
    if (p_abilityLevel = 0) then
		delete from dbo.`CharacterAbilities` where characterId = p_characterId and abilityId = p_abilityId;
    
    
    else
		insert into dbo.`CharacterAbilities` (abilityId, characterId, abilityLevel)
		values (p_abilityId , p_characterId , p_abilityLevel)
		on duplicate key update
			abilityId = values(p_abilityId),
			characterId = values (p_characterId),
			abilityLevel = values (p_abilityLevel);
	end if;
    
END
&&

DELIMITER &&
CREATE  PROCEDURE sp_GetAbilities()
BEGIN	
    select abilityId, name, description from dbo.Ability;
END&&

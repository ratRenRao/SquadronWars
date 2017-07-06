DELIMITER &&
CREATE PROCEDURE `sp_GetAbilitiesPreReqs`()
BEGIN	
    select abilityId, prereqAbilityId, prereqAbilityLevel from dbo.AbilityPreReq;
END&&

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAbilitiesPreReqs`()
BEGIN	
    

    
    select abilityId, prereqAbilityId, prereqAbilityLevel from dbo.AbilityPreReq;
    
        
    
END$$
DELIMITER ;

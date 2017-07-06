DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_GetCharacters
-- -----------------------------------------------------


CREATE PROCEDURE sp_GetCharacters(IN p_SquadID int)
BEGIN	
    
    
SELECT characterId, SquadID, c.statId, LevelID, name, experience, helm, chest, gloves, pants, shoulders, boots, accessory1, accessory2, IsStandard 
from
(select * from dbo.Character where SquadID = p_SquadID)c
left join
(select * from dbo.Stats)s
on c.StatId = s.StatId;
    
    
    
END&&

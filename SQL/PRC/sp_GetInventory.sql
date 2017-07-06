DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_GetInventory
-- -----------------------------------------------------


CREATE  PROCEDURE sp_GetInventory(IN p_playerId int)
BEGIN	
    
    select  playerId,itemId , quantity from dbo.inventory where playerId = p_playerId;
    
    
END&&

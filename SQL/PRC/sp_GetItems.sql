DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_GetItems
-- -----------------------------------------------------

CREATE  PROCEDURE sp_GetItems()
BEGIN	
    
    

    select itemId, equipmentId, consumeId, name, description from dbo.Item;
    
    
    
END&&

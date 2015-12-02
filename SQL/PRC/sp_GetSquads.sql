-- -----------------------------------------------------
-- procedure sp_GetSquads
-- -----------------------------------------------------


CREATE  PROCEDURE sp_GetSquads(IN p_playerId int)
BEGIN	
    
    SELECT SquadID, playerId, Capasity, SquadName from Squad where playerId = p_playerId;
    
    
END
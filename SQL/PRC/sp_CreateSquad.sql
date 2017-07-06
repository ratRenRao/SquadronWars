DELIMITER &&
 
CREATE  PROCEDURE sp_CreateSquad(IN p_playerId int, IN p_Capasity int , IN p_SquadName varchar(45), OUT p_SquadID int)
BEGIN	
    
    INSERT INTO dbo.Squad (playerId, Capasity, SquadName)
    VALUES (p_playerId , p_Capasity , p_SquadName);
    
    SELECT LAST_INSERT_ID() INTO p_SquadID;
    
END&&

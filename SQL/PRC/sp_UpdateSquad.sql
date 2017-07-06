DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_UpdateSquad
-- -----------------------------------------------------


CREATE  PROCEDURE sp_UpdateSquad(IN p_SquadID int ,IN p_Capasity int , IN p_SquadName varchar(45))
BEGIN	
    
    UPDATE dbo.Squad 
    set Capasity = p_Capasity,
    SquadName = p_SquadName
    where SquadID = p_SquadID;
   
    
END&&

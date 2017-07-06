DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_UpdatePlayer
-- -----------------------------------------------------


CREATE  PROCEDURE sp_UpdatePlayer(IN p_username varchar(45) , IN p_password varchar(45), IN p_firstName varchar(45) , IN p_lastname varchar(45), IN p_email varchar(254), OUT p_playerId int)
BEGIN	
    
    update dbo.Player
    set password = p_password,
    firstName = p_firstName,
    lastName = p_lastName,
    email = p_email
    where userName = p_username;
    
    
END&&

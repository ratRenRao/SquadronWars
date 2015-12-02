-- -----------------------------------------------------
-- procedure sp_AuthorizePlayer
-- -----------------------------------------------------

CREATE PROCEDURE sp_AuthorizePlayer(IN p_username varchar(45) , IN p_password varchar(45))
BEGIN	
    
    SELECT playerId, userName, firstName, lastName, email, lastlogin from Player where userName = p_username and password = p_Password;    
    
END
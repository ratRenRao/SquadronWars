DELIMITER &&
 
-- -----------------------------------------------------
-- procedure sp_CreatePlayer
-- -----------------------------------------------------


CREATE  PROCEDURE sp_CreatePlayer(IN p_username varchar(45) , IN p_password varchar(45), IN p_firstName varchar(45) , IN p_lastname varchar(45), IN p_email varchar(254), OUT p_playerId int)
BEGIN	
    INSERT INTO Player (userName , password,firstName, lastName, email) 
    VALUES (p_username,p_password,p_firstName,p_lastname,p_email);
    
    SELECT LAST_INSERT_ID() INTO p_playerId;
    
    
    
END&&


-- -----------------------------------------------------
-- Table `dbo`.`Squad`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Squad` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Squad` (
  `SquadID` INT(11) NOT NULL AUTO_INCREMENT ,
  `playerId` INT(11) NOT NULL COMMENT 'FK to Player table' ,
  `Capasity` INT(11) NULL DEFAULT NULL COMMENT 'Amount of characters in the Squad' ,
  `SquadName` VARCHAR(45) NULL DEFAULT NULL COMMENT 'Name of Squad' ,
  PRIMARY KEY (`SquadID`) ,
  INDEX `FK_Squad_Player_idx` (`playerId` ASC) ,
  CONSTRAINT `FK_Squad_Player`
    FOREIGN KEY (`playerId` )
    REFERENCES `dbo`.`Player` (`playerId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- -----------------------------------------------------
-- Table `dbo`.`Levels`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Levels` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Levels` (
  `LevelID` INT(11) NOT NULL ,
  `LevelDescription` VARCHAR(45) NULL DEFAULT NULL ,
  PRIMARY KEY (`LevelID`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1
COMMENT = 'Levels for characters';